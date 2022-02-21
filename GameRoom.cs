using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gridemonium
{
    public partial class GameRoom : Form
    {
        //Initially used to contain every picture box control.
        public List<PictureBox> BoxList = new List<PictureBox>();        

        //Useful for looping through each column without converting int to char.
        public static readonly char[] LetterList = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };        

        //Instantiate all objects in GameRoom form and add all the picture boxes to a list so they can be added to the BubbleGrid after.
        public GameRoom()
        {
            InitializeComponent();
            MatchSpawner();

            foreach (PictureBox box in MainBox.Controls)
                BoxList.Add(box);

            foreach (PictureBox box in BufferBox.Controls)
                BoxList.Add(box);
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

        //Creates a new Bubble object for each coordinate on the grid and links Bubble with picture box of the same name.
        public void AssignBubbles()
        {            
            for (int i = 0; i < LetterList.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    PictureBox box = BoxList.Find(x => x.Name == "Bubble" + LetterList[i].ToString() + j.ToString());
                    Bubble bubble = new Bubble(LetterList[i], j, box);
                    Bubble.BubbleGrid.Add(bubble.Name, bubble);
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
                    spawnRow = Bubble.BubbleGrid["Bubble" + letter.ToString() + "1"].SpawnBubble("blank");

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

        //Method that drops all bubbles down starting from the bottom.
        public void DropAll(char letter, int startRow)
        {
            int row = startRow;            

            while (row > -1)
            {
                row = Bubble.BubbleGrid["Bubble" + letter.ToString() + row.ToString()].BubbleFall();
                if (row > 0)
                    row -= 2;
            }
        }

        //Action button click event. Handles destroying bottommost bubble of column targetted, activates effect of destroyed bubble, drops all bubbles down,
        //and finally spawns a new bubble at the top.
        private void ActionButton_Click(object sender, EventArgs e)
        {           
            if (PowerUpButton.Text == "Applied")
            {
                RadioButton powerUp = PowerUpGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);

                //ActivatePowerEffect(powerUp.Name);
                Bubble.RefreshGrid(500);

                switch (powerUp.Name)
                {
                    case "TransformUp":
                        EventText.Text = "Transformed all block\nbubbles into random\nbubbles.";
                        break;
                    case "FunnelUp":
                        EventText.Text = "Destroyed all bubbles\nwithin the three\nmiddle columns.";
                        break;
                    case "SnipeUp":
                        EventText.Text = "Damaged all spawners\nby 5%.";
                        break;
                    default:
                        EventText.Text = "Error, no power up\nselected.";
                        break;
                }

                PowerUpButton.Text = "Apply";
                ActionButton.Text = "Fire";
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
                int returnValue = Bubble.BubbleGrid["Bubble" + columnLetter.ToString() + "5"].DestroyBubble(true);  

                switch (returnValue)
                {
                    case -1:
                        this.EventText.Text = "You can't destroy a\nnull bubble.";
                        break;
                    case 0:
                        this.EventText.Text = "Block bubble can't\nbe destroyed by Fire\nbutton.";
                        break;
                    case 1:

                        //CompleteAllEffects();                        
                        
                        foreach (char letter in LetterList)
                        {
                            Bubble checkBubble = Bubble.BubbleGrid["Bubble" + letter.ToString() + "1"];
                            returnValue = 0;
                            DropAll(letter, 4);

                            while (returnValue != -1)
                            {                                
                                returnValue = checkBubble.SpawnBubble("random");
                                DropAll(letter, 4);                                
                            }                            

                        }
                        
                        this.EventText.Text = "Successful fire.";
                        break;
                    default:
                        break;
                }
            }            
        }

        //Event that updates button texts when Power Up button is clicked.
        private void PowerUpButton_Click(object sender, EventArgs e)
        {            
            PowerUpButton.Text = "Applied";
            ActionButton.Text = "Use Power";            
        }
    }
}
