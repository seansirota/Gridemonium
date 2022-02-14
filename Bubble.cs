using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Gridemonium
{
    public class Bubble
    {
        public PictureBox VisualComponent { get; set; }
        public string Name { get; set; }
        public object Image { get; set; }
        public BubbleState State { get; set; }
        public char Letter { get; set; }
        public int Number { get; set; }

        public Bubble(char letter, int number, PictureBox box)
        {
            VisualComponent = box;
            Letter = letter;
            Number = number;
            Name = box.Name;
            box.Image = null;
            Image = box.Image;
            State = BubbleState.Null;
        }

        public void SpawnBubble(Dictionary<string, Bubble> BubbleGrid, char col)
        {
            Bubble bubble = BubbleGrid["Bubble" + col.ToString() + "1"];
            if (bubble.Image == null)
            {
                bubble.Image = Gridemonium.Properties.Resources.blank;
                bubble.State = BubbleState.Active;
            }
        }

        public void BubbleFall(Dictionary<string, Bubble> BubbleGrid, char col, int row)
        {
            Bubble bubbleUp = BubbleGrid["Bubble" + col.ToString() + row.ToString()];
            if (!BubbleGrid.ContainsKey("Bubble" + col.ToString() + (row + 1).ToString()))
                return;

            Bubble bubbleDown = BubbleGrid["Bubble" + col.ToString() + (row + 1).ToString()];

        }
    }

    public enum BubbleState
    {
        Null,
        Active
    }
}
