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
        public GameRoom()
        {
            InitializeComponent();
            MatchSpawner();

            foreach (PictureBox box in MainBox.Controls)
                _boxList.Add(box);

            foreach (PictureBox box in BufferBox.Controls)
                _boxList.Add(box);
        }

        //Method that fills the _spawnerList list with all 7 spawner labels.
        private void MatchSpawner()
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
            ItemCounter blasts = new ItemCounter(Controls.OfType<Label>().FirstOrDefault(x => x.Name == "Ammo") ,"Ammo", 100);
            ItemCounter score = new ItemCounter(Controls.OfType<Label>().FirstOrDefault(x => x.Name == "Score"), "Score", 0);
            ItemCounter transforms = new ItemCounter(PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Name == "TransformUp"), "Transform", 0);
            ItemCounter funnels = new ItemCounter(PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Name == "FunnelUp"), "Funnel", 0);
            ItemCounter snipes = new ItemCounter(PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Name == "SnipeUp"), "Snipe", 0);

            Bubble.CounterList.Add(blasts);
            Bubble.CounterList.Add(score);
            Bubble.CounterList.Add(transforms);
            Bubble.CounterList.Add(funnels);
            Bubble.CounterList.Add(snipes);
        }

        //Method that drops all bubbles in a column.
        private void DropAllColumn(char letter, int startRow)
        {
            int row = startRow;            

            while (row > -1)
            {
                row = Bubble.BubbleGrid["Bubble" + letter.ToString() + row.ToString()].BubbleFall();
                if (row > 0)
                    row -= 2;                
            }

            Bubble.RefreshGrid(0); 
        }

        //Method that drops all bubbles in the entire grid.
        private void DropAllGrid()
        {
            foreach (char letter in LetterList)
            {
                Bubble checkBubble = Bubble.BubbleGrid["Bubble" + letter.ToString() + "1"];
                int returnValue = 0;
                DropAllColumn(letter, 4);

                while (returnValue != -1)
                {
                    returnValue = checkBubble.SpawnBubble("Random");
                    DropAllColumn(letter, 4);
                }
            }
        }

        //Action button click event. Handles destroying bottommost bubble of column targetted, activates effect of destroyed bubble, drops all bubbles down,
        //and finally spawns a new bubble at the top.
        private void ActionButton_Click(object sender, EventArgs e)
        {           
            if (PowerUpButton.Text == "Applied")
            {
                RadioButton powerUp = PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);

                switch (powerUp.Name)
                {
                    case "TransformUp":
                        EventText.Text = Effect.PowerTransform();
                        break;
                    case "FunnelUp":                        
                        EventText.Text = Effect.PowerFunnel();
                        break;
                    case "SnipeUp":                        
                        EventText.Text = Effect.PowerSnipe();
                        break;
                    default:
                        EventText.Text = "Error, no power up\nselected.";
                        break;
                }

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
                RadioButton columnChoice = this.ColumnGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);

                if (columnChoice == null)
                {
                    EventText.Text = "No column selected.";
                    ActionButton.Text = "Error";
                    return;
                }
                else
                    ActionButton.Text = "Fire";

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
    }
}
