﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Gridemonium
{
    public class Bubble
    {
        public PictureBox VisualComponent { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
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

        public int SpawnBubble(Dictionary<string, Bubble> BubbleGrid, char col, bool waitFlag)
        {
            Bubble bubble = BubbleGrid["Bubble" + col.ToString() + "1"];
            if (bubble.Image != null)
                return -1;

            ImageUpdate(bubble, Properties.Resources.blank);
            bubble.State = BubbleState.Active;

            if (waitFlag)
                Thread.Sleep(1000);

            return bubble.Number;
        }

        public int BubbleFall(Dictionary<string, Bubble> BubbleGrid, char col, int row, bool waitFlag)
        {
            Bubble bubbleUp = BubbleGrid["Bubble" + col.ToString() + row.ToString()];
            if (!BubbleGrid.ContainsKey("Bubble" + col.ToString() + (row + 1).ToString()))
                return -1;

            Bubble bubbleDown = BubbleGrid["Bubble" + col.ToString() + (row + 1).ToString()];
            if (bubbleDown.Image != null)
                return -1;

            ImageUpdate(bubbleDown, bubbleUp.Image);
            ImageUpdate(bubbleUp, null);
            bubbleDown.State = BubbleState.Active;
            bubbleUp.State = BubbleState.Null;

            if (waitFlag)
                Thread.Sleep(1000);

            return bubbleDown.Number;
        }

        public void ImageUpdate(Bubble bubble, Image image)
        {
            bubble.Image = image;
            bubble.VisualComponent.Image = image;
        }

        public enum BubbleState
        {
            Null,
            Active
        }
    }
}
