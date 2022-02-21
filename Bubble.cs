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
        public static List<Bubble> WaitList { get; set; } = new List<Bubble>();
        public static List<ItemCounter> CounterList { get; set; } = new List<ItemCounter>();
        public string Name { get; set; }
        public char Letter { get; set; }
        public int Number { get; set; }
        public Image Image { get; set; }
        public Effect BubbleEffect { get; set; }

        private BubbleState _state { get; set; }        
        private string _type;
        private int _waitListPlace;
        private PictureBox _visualComponent;

        //Constructor when all Bubbles in grid are initialized. No new Bubble objects can be created or destroyed, only changed.
        public Bubble(char letter, int number, PictureBox box)
        {
            _visualComponent = box;
            Letter = letter;
            Number = number;
            Name = box.Name;
            BubbleEffect = new Effect();
            ImageUpdate("null");
        }        

        //Basic method for "spawning" a bubble. It actually checks if the picture box of the bubble object is null or not and then changes the image to something else.
        public int SpawnBubble(string type)
        {
            if (Image != null)
                return -1;

            ImageUpdate(type);

            return Number;
        }

        //Basic method for implementing a dropping mechanism when there is space beneath a bubble to "fall" to.
        public int BubbleFall()
        {
            if (!BubbleGrid.ContainsKey("Bubble" + Letter.ToString() + (Number + 1).ToString()))
                return -1;

            Bubble bubbleDown = BubbleGrid["Bubble" + Letter.ToString() + (Number + 1).ToString()];
            if (bubbleDown.Image != null)
                return -1;

            bubbleDown.ImageUpdate(_type);
            ImageUpdate("null");         

            return bubbleDown.Number;
        }

        //Updates the image of the bubble object as well as the picture box it is tied to in one method.
        public void ImageUpdate(string type)
        {
            Image image = SpawnType(type);
            BubbleEffect.EffectType = _type;
            Image = image;
            _visualComponent.Image = image;

            if (_type != "null" || _type != null || _type != "destroyed")
                _state = BubbleState.Active;
            else if (_type == "destroyed")
            {
                _state = BubbleState.Destroyed;
                WaitList.Add(this);
                _waitListPlace = WaitList.Count;
            }                
            else
            {
                _state = BubbleState.Empty;
                WaitList.Remove(this);
                _waitListPlace = 0;
            }                
        }

        //Sets a bubble to default values. Returns an int describing the result, whether an Action Button press was used or not.
        public int DestroyBubble(bool buttonPress)
        {
            int bubbleType;

            if (_type == "null")
                bubbleType = -1;
            else if (_type == "block" && buttonPress == true)
                bubbleType = 0;
            else
            {
                BubbleEffect.ChooseEffect(this);
                ImageUpdate("null");
                RefreshGrid(500);
                bubbleType = 1;
            }                       

            return bubbleType;
        }

        //Static method for completing all waitlisted effects after an action is taken.
        public static void CompleteAllEffects()
        {
            while (WaitList.Count > 0)
            {

            }
        }

        //Redraws all bubbles after a major action occurs.
        public static void RefreshGrid(int ticks)
        {
            foreach (KeyValuePair<string, Bubble> entry in BubbleGrid)
                entry.Value._visualComponent.Refresh();

            Thread.Sleep(ticks);
        }

        //Enums for the status of a bubble.
        public enum BubbleState
        {
            Active,
            Destroyed,
            Empty
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
                    int blockChance = 12;
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
                    else if (rng > blankChance + powerChance && rng <= blankChance + powerChance + blockChance)
                    {
                        _type = "block";
                        return Properties.Resources.block;
                    }
                    else if (rng > blankChance + powerChance + blockChance && rng <= blankChance + powerChance + blockChance + arrowChance)
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
                    else if (rng > blankChance + powerChance + blockChance + arrowChance && rng <= blankChance + powerChance + blockChance + arrowChance + letterChance)
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
