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

        private BubbleState _state;
        private BubbleType _type;
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
            ImageUpdate("_");
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

            bubbleDown.ImageUpdate(_type.ToString());
            ImageUpdate("_");         

            return bubbleDown.Number;
        }

        //Updates the image of the bubble object as well as the picture box it is tied to in one method.
        public void ImageUpdate(string type)
        {
            Image image = SpawnType(type);
            BubbleEffect.EffectType = _type;
            Image = image;
            _visualComponent.Image = image;

            if (_type != BubbleType._ || _type != BubbleType.Destroyed)
                _state = BubbleState.Active;
            else if (_type == BubbleType.Destroyed)
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

            if (_type == BubbleType._)
                bubbleType = -1;
            else if (_type == BubbleType.Block && buttonPress == true)
                bubbleType = 0;
            else
            {
                BubbleEffect.ChooseEffect(this);
                ImageUpdate("_");
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

        //Method that returns an image reference when spawning bubbles.
        private Image SpawnType(string type)
        {
            if (type != "Random")
                Enum.TryParse(type, out _type);

            switch (type)
            {
                case "Random":                    
                    int letterChance = 18;
                    int arrowChance = 10;
                    int blockChance = 12;
                    int powerChance = 10;
                    int blankChance = 50;

                    Random random = new Random();
                    int rng = random.Next(1, 101);

                    if (rng > 0 && rng <= blankChance)
                    {
                        _type = BubbleType.Blank;
                        return Properties.Resources.blank;
                    }
                    else if (rng > blankChance && rng <= blankChance + powerChance)
                    {
                        _type = BubbleType.Power;
                        return Properties.Resources.power;
                    }
                    else if (rng > blankChance + powerChance && rng <= blankChance + powerChance + blockChance)
                    {
                        _type = BubbleType.Block;
                        return Properties.Resources.block;
                    }
                    else if (rng > blankChance + powerChance + blockChance && rng <= blankChance + powerChance + blockChance + arrowChance)
                    {
                        rng = random.Next(1, 3);
                        switch (rng)
                        {
                            case 1:
                                _type = BubbleType.LeftRight;
                                return Properties.Resources.leftright;
                            case 2:
                                _type = BubbleType.UpDown;
                                return Properties.Resources.updown;
                            default:
                                _type = BubbleType._;
                                return null;
                        }
                    }
                    else if (rng > blankChance + powerChance + blockChance + arrowChance && rng <= blankChance + powerChance + blockChance + arrowChance + letterChance)
                    {
                        rng = random.Next(1, 7);
                        switch (rng)
                        {
                            case 1:
                                _type = BubbleType.A;
                                return Properties.Resources.a;
                            case 2:
                                _type = BubbleType.B;
                                return Properties.Resources.b;
                            case 3:
                                _type = BubbleType.C;
                                return Properties.Resources.c;
                            case 4:
                                _type = BubbleType.D;
                                return Properties.Resources.d;
                            case 5:
                                _type = BubbleType.E;
                                return Properties.Resources.e;
                            case 6:
                                _type = BubbleType.F;
                                return Properties.Resources.f;
                            default:
                                _type = BubbleType._;
                                return null;
                        }
                    }
                    else
                        return null;
                case "Blank":
                    return Properties.Resources.blank;
                case "LeftRight":
                    return Properties.Resources.leftright;
                case "UpDown":
                    return Properties.Resources.updown;
                case "Block":
                    return Properties.Resources.block;
                case "Power":
                    return Properties.Resources.power;
                case "A":
                    return Properties.Resources.a;
                case "B":
                    return Properties.Resources.b;
                case "C":
                    return Properties.Resources.c;
                case "D":
                    return Properties.Resources.d;
                case "E":
                    return Properties.Resources.e;
                case "F":
                    return Properties.Resources.f;
                default:
                    return null;
            }
        }

        //Enums for type of bubble.
        public enum BubbleType
        {
            Blank,
            Block,
            Power,
            LeftRight,
            UpDown,
            A,
            B,
            C,
            D,
            E,
            F,
            _,
            Destroyed
        }

        //Enums for the status of a bubble.
        public enum BubbleState
        {
            Active,
            Destroyed,
            Empty
        }
    }
}
