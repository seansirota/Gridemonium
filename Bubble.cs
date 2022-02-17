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
        //The main container of bubble objects. Used for wayfinding the bubble grid and manipulating each bubble object.
        public static Dictionary<string, Bubble> BubbleGrid { get; } = new Dictionary<string, Bubble>();        
        public string Name { get; set; }
        public Image Image { get; set; }
        public BubbleState State { get; set; }

        private char _letter;
        private int _number;
        private string _type;
        private PictureBox VisualComponent;

        //Constructor when all Bubbles in grid are initialized. No new Bubble objects can be created or destroyed, only changed.
        public Bubble(char letter, int number, PictureBox box)
        {
            VisualComponent = box;
            _letter = letter;
            _number = number;
            Name = box.Name;
            ImageUpdate("null");
        }

        //Basic method for "spawning" a bubble. It actually checks if the picture box of the bubble object is null or not and then changes the image to something else.
        public int SpawnBubble(bool waitFlag, string type)
        {
            if (Image != null)
                return -1;

            if (waitFlag)
                Thread.Sleep(1000);

            ImageUpdate(type);

            return _number;
        }

        //Basic method for implementing a dropping mechanism when there is space beneath a bubble to "fall" to.
        public int BubbleFall(bool waitFlag)
        {
            if (!BubbleGrid.ContainsKey("Bubble" + _letter.ToString() + (_number + 1).ToString()))
                return -1;

            Bubble bubbleDown = BubbleGrid["Bubble" + _letter.ToString() + (_number + 1).ToString()];
            if (bubbleDown.Image != null)
                return -1;

            if (waitFlag)
                Thread.Sleep(1000);

            bubbleDown.ImageUpdate(_type);
            ImageUpdate("null");         

            return bubbleDown._number;
        }

        //Updates the image of the bubble object as well as the picture box it is tied to in one method.
        public void ImageUpdate(string type)
        {
            Image image = SpawnType(type);
            Image = image;
            VisualComponent.Image = image;
            if (_type != "null" || _type != null)
                State = BubbleState.Active;
            else
                State = BubbleState.Null;
        }

        //Sets a bubble to default values. Returns an int describing the result, whether an Action Button press was used or not.
        public int DestroyBubble(bool waitFlag, bool buttonPress)
        {
            int bubbleType;

            if (waitFlag)
                Thread.Sleep(1000);

            if (_type == "null")
                bubbleType = -1;
            else if (_type == "block" && buttonPress == true)
                bubbleType = 0;
            else
            {
                ImageUpdate("null");
                bubbleType = 1;
            }

            return bubbleType;
        }

        //Enums for the status of a bubble.
        public enum BubbleState
        {
            Null,
            Active
        }

        //Method that returns an image reference when spawning bubbles.
        private Image SpawnType(string type)
        {
            if (type != "random")
                _type = type;

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
                    {
                        _type = "blank";
                        return Properties.Resources.blank;
                    }
                    else if (rng > blankChance && rng <= blankChance + powerChance)
                    {
                        _type = "power";
                        return Properties.Resources.power;
                    }
                    else if (rng > blankChance + powerChance && rng <= blankChance + powerChance + nullChance)
                    {
                        _type = "block";
                        return Properties.Resources.block;
                    }
                    else if (rng > blankChance + powerChance + nullChance && rng <= blankChance + powerChance + nullChance + arrowChance)
                    {
                        rng = random.Next(1, 3);
                        switch (rng)
                        {
                            case 1:
                                _type = "leftright";
                                return Properties.Resources.leftright;
                            case 2:
                                _type = "updown";
                                return Properties.Resources.updown;
                            default:
                                _type = "null";
                                return null;
                        }
                    }
                    else if (rng > blankChance + powerChance + nullChance + arrowChance && rng <= blankChance + powerChance + nullChance + arrowChance + letterChance)
                    {
                        rng = random.Next(1, 7);
                        switch (rng)
                        {
                            case 1:
                                _type = "a";
                                return Properties.Resources.a;
                            case 2:
                                _type = "b";
                                return Properties.Resources.b;
                            case 3:
                                _type = "c";
                                return Properties.Resources.c;
                            case 4:
                                _type = "d";
                                return Properties.Resources.d;
                            case 5:
                                _type = "e";
                                return Properties.Resources.e;
                            case 6:
                                _type = "f";
                                return Properties.Resources.f;
                            default:
                                _type = "null";
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
                case "block":
                    return Properties.Resources.block;
                case "power":
                    return Properties.Resources.power;
                case "a":
                    return Properties.Resources.a;
                case "b":
                    return Properties.Resources.b;
                case "c":
                    return Properties.Resources.c;
                case "d":
                    return Properties.Resources.d;
                case "e":
                    return Properties.Resources.e;
                case "f":
                    return Properties.Resources.f;
                default:
                    return null;
            }
        }
    }
}
