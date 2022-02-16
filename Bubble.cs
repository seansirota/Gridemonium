using System;
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

        //Constructor when all Bubbles in grid are initialized. No new Bubble objects can be created or destroyed, only changed.
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

        //Basic method for "spawning" a bubble. It actually checks if the picture box of the bubble object is null or not and then changes the image to something else.
        public int SpawnBubble(Dictionary<string, Bubble> BubbleGrid, char col, int row, bool waitFlag, string type)
        {
            Bubble bubble = BubbleGrid["Bubble" + col.ToString() + row.ToString()];
            if (bubble.Image != null)
                return -1;

            ImageUpdate(bubble, SpawnType(type));
            bubble.State = BubbleState.Active;

            if (waitFlag)
                Thread.Sleep(1000);

            return bubble.Number;
        }

        //Basic method for implementing a dropping mechanism when there is space beneath a bubble to "fall" to.
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

        //Updates the image of the bubble object as well as the picture box it is tied to in one method.
        public void ImageUpdate(Bubble bubble, Image image)
        {
            bubble.Image = image;
            bubble.VisualComponent.Image = image;
        }

        public int DestroyBubble(Dictionary<string, Bubble> BubbleGrid, char col, int row, bool waitFlag, bool buttonPress)
        {
            int bubbleType;
            Bubble bubble = BubbleGrid["Bubble" + col.ToString() + row.ToString()];

            if (bubble.Image == null)
                bubbleType = -1;
            else if (bubble.Image == Properties.Resources._null && buttonPress == true)
                bubbleType = 0;
            else
            {
                ImageUpdate(bubble, null);
                bubble.State = BubbleState.Null;
                bubbleType = 1;
            }

            if (waitFlag)
                Thread.Sleep(1000);

            return bubbleType;
        }

        public enum BubbleState
        {
            Null,
            Active
        }

        //Method that returns an image reference when spawning bubbles.
        private Image SpawnType(string type)
        {           
            switch (type)
            {
                case "random":                    
                    int letterChance = 18;
                    int arrowChance = 10;
                    int nullChance = 12;
                    int powerChance = 10;
                    int blankChance = 50;

                    Random random = new Random();
                    int rng = random.Next(1, 101);

                    if (rng > 0 && rng <= blankChance)
                        return Properties.Resources.blank;
                    else if (rng > blankChance && rng <= blankChance + powerChance)
                        return Properties.Resources.power;
                    else if (rng > blankChance + powerChance && rng <= blankChance + powerChance + nullChance)
                        return Properties.Resources._null;
                    else if (rng > blankChance + powerChance + nullChance && rng <= blankChance + powerChance + nullChance + arrowChance)
                    {
                        rng = random.Next(1, 3);
                        switch (rng)
                        {
                            case 1:
                                return Properties.Resources.leftright;
                            case 2:
                                return Properties.Resources.updown;
                            default:
                                return null;
                        }                        
                    }
                    else if (rng > blankChance + powerChance + nullChance + arrowChance && rng <= blankChance + powerChance + nullChance + arrowChance + letterChance)
                    {
                        rng = random.Next(1, 7);
                        switch (rng)
                        {
                            case 1:
                                return Properties.Resources.a;
                            case 2:
                                return Properties.Resources.b;
                            case 3:
                                return Properties.Resources.c;
                            case 4:
                                return Properties.Resources.d;
                            case 5:
                                return Properties.Resources.e;
                            case 6:
                                return Properties.Resources.f;
                            default:
                                return null;
                        }
                    }
                    else
                        return null;
                case "blank":
                    return Properties.Resources.blank;
                case "leftright":
                    return Properties.Resources.leftright;
                case "updown":
                    return Properties.Resources.updown;
                case "null":
                    return Properties.Resources._null;
                case "power":
                    return Properties.Resources.power;
                default:
                    return null;
            }
        }
    }
}
