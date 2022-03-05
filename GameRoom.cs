using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gridemonium
{
    public partial class GameRoom : Form
    {
        //Initially used to contain every picture box control.
        private List<PictureBox> _boxList = new List<PictureBox>();        

        //Useful for looping through each column without converting int to char.
        public static readonly char[] LetterList = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };        

        //Instantiate all objects in GameRoom form and add all the picture boxes to a list so they can be added to the BubbleGrid after.
        public GameRoom(string mode)
        {
            InitializeComponent();            
            SetSettings(mode);

            foreach (PictureBox box in MainBox.Controls)
                _boxList.Add(box);

            foreach (PictureBox box in BufferBox.Controls)
                _boxList.Add(box);
        }

        //Method for clearing the bubble grid when playing another game.
        public void ClearGrid()
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid)
                entry.Value.ImageUpdate("_", true);
            foreach (KeyValuePair<string, Bubble> entry in Event.EventBubbleList)
                entry.Value.ImageUpdate("_", true);

            Bubble.BubbleGrid.Clear();
            Event.EventBubbleList.Clear();
        }

        //Method used to switch the difficulty when replaying the game.
        public void RestartForm(string mode)
        {
            EventText.Text = "";
            PowerUpButton.Text = "Apply";
            ActionButton.Text = "Fire";

            foreach (Spawner spawner in Spawner.SpawnerList)
                spawner.Value = 100;

            SetSettings(mode);
        }

        //Method to set the parameters for bubble spawning, score multipliers, and events.
        private void SetSettings(string mode)
        {
            //Switch statement that chooses the correct difficulty settings based on the chosen mode. See below for parameters of InitiateSettings method.
            //BubbleArrowChance, BubbleBlockChance, BubblePowerChance, BubbleBlankChance,
            //EventLetterChance, EventBlockChance, EventRandomChance, EventBlankChance,
            //StartingAmmo, ScoreMultiplier
            switch (mode)
            {
                case "Easy":
                    Settings.InitiateSettings(14, 6, 14, 42, 15, 15, 15, 15, 60, 5);
                    break;
                case "Normal":
                    Settings.InitiateSettings(10, 12, 10, 50, 15, 15, 15, 15, 45, 10);
                    break;
                case "Hard":
                    Settings.InitiateSettings(8, 18, 8, 54, 12, 21, 15, 12, 35, 15);
                    break;
                default:
                    break;
            }
        }

        //Method that fills the _spawnerList list with all 7 spawner labels.
        public void MatchSpawner()
        {
            foreach (char letter in LetterList)
            {                
                Label label = Controls.OfType<Label>().FirstOrDefault(x => x.Name == "Percent" + letter.ToString());            
                Spawner spawner = new Spawner(label);
                Spawner.SpawnerList.Add(spawner);
            }
        }

        //Method that assigns randomly a numbered effect to each letter bubble type.
        public void SetUpLetterBubbles()
        {
            int rng;
            Bubble.BubbleType value;
            Random random = new Random();            
            List<int> numberList = new List<int> { 1, 2, 3, 4, 5, 6 };

            for (int i = 0; i < 6; i++)
            {
                rng = numberList[random.Next(0, numberList.Count)];

                switch (rng)
                {
                    case 1:
                        value = Bubble.BubbleType.A;                        
                        break;
                    case 2:
                        value = Bubble.BubbleType.B;
                        break;
                    case 3:
                        value = Bubble.BubbleType.C;
                        break;
                    case 4:
                        value = Bubble.BubbleType.D;
                        break;
                    case 5:
                        value = Bubble.BubbleType.E;
                        break;
                    case 6:
                        value = Bubble.BubbleType.F;
                        break;
                    default:
                        value = Bubble.BubbleType._;
                        break;
                }

                Effect.LetterBubbleMatch.Add(i, value);
                numberList.Remove(rng);
            }
        }

        //Creates a new Bubble object for each coordinate on the grid and links Bubble with picture box of the same name.
        public void AssignBubbles()
        {            
            for (int i = 0; i < LetterList.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    PictureBox box = _boxList.Find(x => x.Name == "Bubble" + LetterList[i].ToString() + j.ToString());
                    Bubble bubble = new Bubble(LetterList[i], j, box);
                    Bubble.BubbleGrid.Add(bubble.Name, bubble);
                    if (j == 0)
                        Event.EventBubbleList.Add(bubble.Name, bubble);
                }
            }
        }

        //This section goes through a nested loop that first spawns a Bubble, has it fall all the way down to the bottom of the grid, and then
        //repeat until every space in a column is filled with bubbles. Then, it moves on to the next column until each column is filled.
        public void InitiateGrid()
        {
            foreach (char letter in LetterList)
            {
                int spawnRow;
                int fallRow;

                do
                {
                    spawnRow = Bubble.BubbleGrid["Bubble" + letter.ToString() + "1"].SpawnBubble("Random");
                    Bubble.RefreshGrid(0);

                    if (spawnRow > -1)
                    {
                        fallRow = spawnRow;
                        do
                            fallRow = Bubble.BubbleGrid["Bubble" + letter.ToString() + fallRow.ToString()].BubbleFall();
                        while (fallRow > -1);
                    }
                } while (spawnRow > -1);
            }            
        }

        //Method that instantiates all counters in the game room including the blasts, score, and power up counts.
        public void SetUpCounters()
        {
            Bubble.CounterList.Clear();

            ItemCounter ammo = new ItemCounter(Controls.OfType<Label>().FirstOrDefault(x => x.Name == "Ammo"), "Ammo", Settings.RetrieveValue("StartingAmmo"));
            ItemCounter score = new ItemCounter(Controls.OfType<Label>().FirstOrDefault(x => x.Name == "Score"), "Score", 0);
            ItemCounter transforms = new ItemCounter(PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Name == "TransformUp"), "Transform", 0);
            ItemCounter funnels = new ItemCounter(PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Name == "FunnelUp"), "Funnel", 0);
            ItemCounter snipes = new ItemCounter(PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Name == "SnipeUp"), "Snipe", 0);       

            Bubble.CounterList.Add(ammo);
            Bubble.CounterList.Add(score);
            Bubble.CounterList.Add(transforms);
            Bubble.CounterList.Add(funnels);
            Bubble.CounterList.Add(snipes);
        }

        //Method that drops all bubbles in a column.
        private void DropAllColumn(char letter, int startRow)
        {
            int row = startRow;
            bool runAgain = true;

            do
            {
                runAgain = false;
                while (row > -1)
                    row = Bubble.BubbleGrid["Bubble" + letter.ToString() + row.ToString()].BubbleFall() - 2;

                if (Bubble.BubbleGrid.Where(x => x.Value.Letter == letter & x.Value.VisualComponent == null).Any())
                {
                    runAgain = true;
                    row = startRow;
                }
            } while (runAgain);

            Bubble.RefreshGrid(0); 
        }

        //Method that drops all bubbles in the entire grid.
        private void DropAllGrid()
        {
            foreach (char letter in LetterList)
            {
                Bubble checkBubble = Bubble.BubbleGrid["Bubble" + letter.ToString() + "1"];
                Spawner spawner = Spawner.SpawnerList.Find(x => x.PercentLabel.Name == "Percent" + letter.ToString());
                Random random = new Random();
                
                int rng;
                int returnValue = 0;
                DropAllColumn(letter, 4);

                while (returnValue != -1)
                {
                    rng = random.Next(1, 101);
                    if (rng <= spawner.Value)
                    {
                        returnValue = checkBubble.SpawnBubble("Random");
                        DropAllColumn(letter, 4);
                    }
                    else
                        returnValue = -1;            
                }
            }
        }

        //Action button click event. Handles destroying bottommost bubble of column targetted, activates effect of destroyed bubble, drops all bubbles down,
        //and finally spawns a new bubble at the top.
        private void ActionButton_Click(object sender, EventArgs e)
        {           
            //First step of the action button click event is to see if a power up was applied or not.
            if (PowerUpButton.Text == "Applied")
            {
                //Cancels power up use if that power up is out of stock.
                ItemCounter powerUp = Bubble.CounterList.FindAll(x => x.CounterLabel.GetType().Equals(typeof(RadioButton))).FirstOrDefault(x => ((RadioButton)x.CounterLabel).Checked);
                if (powerUp.Value == 0)
                {
                    PowerUpButton.Text = "Apply";
                    ActionButton.Text = "Fire";
                    EventText.Text = "You are out of that\npower up.";
                    return;
                }

                switch (powerUp.Name)
                {
                    case "Transform":
                        EventText.Text = Effect.PowerTransform();
                        break;
                    case "Funnel":                        
                        EventText.Text = Effect.PowerFunnel();
                        break;
                    case "Snipe":                        
                        EventText.Text = Effect.PowerSnipe();
                        break;
                    default:
                        EventText.Text = "Error, no power up\nselected.";
                        break;
                }
                powerUp.UpdateCounter(-1);
                Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(5 * Settings.RetrieveValue("ScoreMultiplier"));

                //Uses power up and refreshes grid to show changes during the action.
                Bubble.RefreshGrid(500);
                Bubble.CompleteAllEffects();

                DropAllGrid();
                EventText.Text = Event.EventHub();
                DropAllGrid();

                Bubble.RefreshGrid(500);

                PowerUpButton.Text = "Apply";
                ActionButton.Text = "Fire";
                EventText.Text = "Power up used.";
            }
            else
            {
                //Cancels blast if you are out of ammo.
                ItemCounter ammo = Bubble.CounterList.FirstOrDefault(x => x.Name == "Ammo");
                if (ammo.Value == 0)
                {
                    EventText.Text = "You are out of ammo.";
                    return;
                }

                ammo.UpdateCounter(-1);
                RadioButton columnChoice = this.ColumnGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);                

                //Cancels blast if no column selected. Unused since first column is selected by default.
                if (columnChoice == null)
                {
                    EventText.Text = "No column selected.";
                    ActionButton.Text = "Error";
                    return;
                }                

                //Destroys bubble with action and runs through all potential effects.
                char columnLetter = columnChoice.Name.Last();
                int returnValue = Bubble.BubbleGrid["Bubble" + columnLetter.ToString() + "5"].DestroyBubble();  

                switch (returnValue)
                {
                    case -1:
                        EventText.Text = "You can't destroy a\nnull bubble.";
                        break;
                    case 0:
                        EventText.Text = "Block bubble can't\nbe destroyed by Fire\nbutton.";
                        break;
                    case 1:                        
                        Bubble.CompleteAllEffects();

                        DropAllGrid();
                        EventText.Text = Event.EventHub();
                        DropAllGrid();

                        Bubble.RefreshGrid(500);

                        EventText.Text = "Successful blast.";
                        break;
                    default:
                        break;
                }
            }

            //At the end of each completed action, a victory or loss check is done to decide whether the game should continue or not.
            CheckWinOrLoss();
        }

        //Event that updates button texts when Power Up button is clicked.
        private void PowerUpButton_Click(object sender, EventArgs e)
        {            
            if (PowerUpButton.Text == "Apply")
            {
                PowerUpButton.Text = "Applied";
                ActionButton.Text = "Use Power";
            }            
            else
            {
                PowerUpButton.Text = "Apply";
                ActionButton.Text = "Fire";
            }
        }

        //Method that checks the status of the game to see if the player won or lost after every action.
        private void CheckWinOrLoss()
        {
            ItemCounter score = Bubble.CounterList.FirstOrDefault(x => x.Name == "Score");
            ItemCounter ammo = Bubble.CounterList.FirstOrDefault(x => x.Name == "Ammo");
            ItemCounter transforms = Bubble.CounterList.FirstOrDefault(x => x.Name == "Transform");
            ItemCounter funnels = Bubble.CounterList.FirstOrDefault(x => x.Name == "Funnel");
            ItemCounter snipes = Bubble.CounterList.FirstOrDefault(x => x.Name == "Snipe");

            //Win condition check comes first. Checks if all spawners are down to 0% before continuing.
            if (Spawner.AllSpawnersDestroyed())
            {                
                //Extra points earned depending on difficulty and remaining blasts.
                score.UpdateCounter(10 * ammo.Value * Settings.RetrieveValue("ScoreMultiplier"));

                //Message box construction and display. Takes the user back to the main menu after clicking "OK".
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string message = "You have won the game with a total score of " + score.Value + ". You will now be taken back to the main menu.";
                string caption = "Winner!";
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.OK)
                {
                    Form form = Application.OpenForms["MainMenu"];
                    if (form != null)
                    {
                        this.Hide();
                        form.Show(this);
                        StreamOps.AddScore(score.Value);
                    }
                }
            }

            //Loss condition check occurs after win check. Game is lost if player runs out of ammo and power ups.
            if (ammo.Value == 0)
            {
                if (transforms.Value == 0 && funnels.Value == 0 && snipes.Value == 0)
                {
                    //Message box construction and display. Takes the user back to the main menu after clicking "OK".
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    string message = "You lost the game with a total score of " + score.Value + ". You will now be taken back to the main menu.";
                    string caption = "Loser. Try again next time.";
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons);
                    if (result == DialogResult.OK)
                    {
                        Form form = Application.OpenForms["MainMenu"];
                        if (form != null)
                        {
                            this.Hide();
                            form.Show(this);
                            StreamOps.AddScore(score.Value);
                        }
                    }
                }
            }            
        }
    }
}
