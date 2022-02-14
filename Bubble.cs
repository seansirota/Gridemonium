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
        public string State { get; set; }
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
            State = "Destroyed";
        }
    }
}
