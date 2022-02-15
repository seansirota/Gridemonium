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
            InitializeComponent();
            foreach (PictureBox box in MainBox.Controls)
                BoxList.Add(box);

            foreach (PictureBox box in BufferBox.Controls)
                BoxList.Add(box);
        }

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

        public void InitiateGrid()
        {
            int spawnRow;
            int fallRow;

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
