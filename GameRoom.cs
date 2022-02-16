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

        //The main container of bubble objects. Used for wayfinding the bubble grid and manipulating each bubble object.
        public Dictionary<string, Bubble> BubbleGrid { get; set; } = new Dictionary<string, Bubble>();

        //Useful for looping through each column without converting int to char.
        private readonly char[] LetterList = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };

        //Instantiate all objects in GameRoom form and add all the picture boxes to a list so they can be added to the BubbleGrid after.
        public GameRoom()
        {            
            InitializeComponent();
            foreach (PictureBox box in MainBox.Controls)
                BoxList.Add(box);

            foreach (PictureBox box in BufferBox.Controls)
                BoxList.Add(box);
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
                    BubbleGrid.Add(bubble.Name, bubble);
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
                    spawnRow = BubbleGrid["Bubble" + letter.ToString() + "1"].SpawnBubble(BubbleGrid, letter, 1, false, "blank");

                    if (spawnRow > -1)
                    {
                        fallRow = spawnRow;
                        do
                            fallRow = BubbleGrid["Bubble" + letter.ToString() + fallRow.ToString()].BubbleFall(BubbleGrid, letter, fallRow, false);
                        while (fallRow > -1);
                    }
                } while (spawnRow > -1);
            }
        }

        //Method that drops all bubbles down starting from the bottom.
        public void DropAll(char letter, int startRow, bool waitFlag)
        {
            int row = startRow;

            while (row > 0)
            {
                row = BubbleGrid["Bubble" + letter.ToString() + row.ToString()].BubbleFall(BubbleGrid, letter, row, waitFlag);
                if (row > 1)
                    row -= 2;
            }               
        }

        //Action button click event. Handles destroying bottommost bubble of column targetted, activates effect of destroyed bubble, drops all bubbles down,
        //and finally spawns a new bubble at the top.
        private void ActionButton_Click(object sender, EventArgs e)
        {
            RadioButton columnChoice = this.ColumnGroup.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);

            if (columnChoice == null)
            {
                EventText.Text = "No column selected.";
                return;
            }
                
            char columnLetter = columnChoice.Name.Last<char>();

            int returnValue = BubbleGrid["Bubble" + columnLetter.ToString() + "5"].DestroyBubble(BubbleGrid, columnLetter, 5, false, true);

            switch (returnValue)
            {
                case -1:
                    this.EventText.Text = "You can't destroy a null\n bubble.";
                    break;
                case 0:
                    this.EventText.Text = "Block bubble can't be\n destroyed by Fire button.";
                    break;
                case 1:
                    //ActivateEffect();
                    foreach (char letter in LetterList)
                    {
                        DropAll(letter, 4, false);
                        BubbleGrid["Bubble" + letter.ToString() + "1"].SpawnBubble(BubbleGrid, letter, 1, false, "random");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
