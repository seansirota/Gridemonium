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
        public List<PictureBox> BoxList = new List<PictureBox>();
        public Dictionary<string, Bubble> BubbleGrid { get; set; } = new Dictionary<string, Bubble>();
        private readonly char[] LetterList = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
        public GameRoom()
        {
            //Instantiate all objects in GameRoom form and add all the picture boxes to a list so they can be added to the BubbleGrid after.
            InitializeComponent();
            foreach (PictureBox box in MainBox.Controls)
                BoxList.Add(box);

            foreach (PictureBox box in BufferBox.Controls)
                BoxList.Add(box);
        }

        public void AssignBubbles()
        {
            //Creates a new Bubble object for each coordinate on the grid and links Bubble with picture box of the same name.
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

        public void InitiateGrid()
        {
            int spawnRow;
            int fallRow;

            //This section goes through a nested loop that first spawns a Bubble, has it fall all the way down to the bottom of the grid, and then
            //repeat until every space in a column is filled with bubbles. Then, it moves on to the next column until each column is filled.
            foreach (char letter in LetterList)
            {                
                do
                {
                    spawnRow = BubbleGrid["Bubble" + letter.ToString() + "1"].SpawnBubble(BubbleGrid, letter, false);                    

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
    }
}
