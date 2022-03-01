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
        public static List<Bubble> WaitList { get; } = new List<Bubble>();
        public static List<ItemCounter> CounterList { get; } = new List<ItemCounter>();
        public string Name { get; set; }
        public char Letter { get; set; }
        public int Number { get; set; }
        public Bitmap Image { get; set; }
        public Effect BubbleEffect { get; set; }
        public BubbleType Type;
        public PictureBox VisualComponent;

        private BubbleState _state;        
        private int _waitListPlace;        

        //Constructor when all Bubbles in grid are initialized. No new Bubble objects can be created or destroyed, only changed.
        public Bubble(char letter, int number, PictureBox box)
        {
            VisualComponent = box;
            Letter = letter;
            Number = number;
            Name = box.Name;
            BubbleEffect = new Effect();
            ImageUpdate("_", true);
        }        

        //Basic method for "spawning" a bubble. It actually checks if the picture box of the bubble object is null or not and then changes the image to something else.
        public int SpawnBubble(string type)
        {
            if (Image != null)
                return -1;

            ImageUpdate(type, true);

            return Number;
        }

        //Basic method for implementing a dropping mechanism when there is space beneath a bubble to "fall" to.
        public int BubbleFall()
        {
            if (!BubbleGrid.ContainsKey("Bubble" + Letter.ToString() + (Number + 1).ToString()))
                return -1;

            Bubble bubbleDown = BubbleGrid["Bubble" + Letter.ToString() + (Number + 1).ToString()];
            if (bubbleDown.Image != null)
                return bubbleDown.Number;

            bubbleDown.ImageUpdate(Type.ToString(), true);
            ImageUpdate("_", true);         

            return bubbleDown.Number;
        }

        //Updates the image of the bubble object as well as the picture box it is tied to in one method.
        public void ImageUpdate(string type, bool changeTypeFlag)
        {
            Bitmap image = SpawnType(type);
            if (changeTypeFlag)
                BubbleEffect.EffectType = Type;
            Image = image;
            VisualComponent.Image = image;

            if (_state == BubbleState.Active && changeTypeFlag == true)
                _state = BubbleState.Empty;
            else if (_state == BubbleState.Active && changeTypeFlag == false)
            {                
                _state = BubbleState.Destroyed;
                if (BubbleEffect.EffectType != BubbleType.Blank && BubbleEffect.EffectType != BubbleType.Block)
                {
                    WaitList.Add(this);
                    _waitListPlace = WaitList.Count;
                }                
            }
            else if (_state == BubbleState.Destroyed)
            {
                _state = BubbleState.Empty;
                if (BubbleEffect.EffectType != BubbleType.Blank && BubbleEffect.EffectType != BubbleType.Block)
                {
                    WaitList.Remove(this);
                    _waitListPlace = 0;
                }
            }
            else if (_state == BubbleState.Empty && type != "_")
                _state = BubbleState.Active;
        }

        //Sets a bubble to default values. Returns an int describing the result, whether an Action Button press was used or not.
        public int DestroyBubble()
        {
            int bubbleType;

            if (Type == BubbleType._)
                bubbleType = -1;
            else if (Type == BubbleType.Block)
                bubbleType = 0;
            else
            {
                Application.OpenForms["GameRoom"].Controls["EventText"].Text = BubbleEffect.ChooseEffect(this);
                ImageUpdate("_", true);
                RefreshGrid(500);
                bubbleType = 1;
            }                       

            return bubbleType;
        }

        //Redraws all bubbles after a major action occurs.
        public static void RefreshGrid(int ticks)
        {
            foreach (KeyValuePair<string, Bubble> entry in BubbleGrid)
                entry.Value.VisualComponent.Refresh();

            foreach (Spawner spawner in Spawner.SpawnerList)
                spawner.PercentLabel.Refresh();

            foreach (ItemCounter counter in CounterList)
                counter.CounterLabel.Refresh();

            Application.OpenForms["GameRoom"].Controls["EventText"].Refresh();

            Thread.Sleep(ticks);
        }

        //Static method for completing all waitlisted effects after an action is taken.
        public static void CompleteAllEffects()
        {
            Bubble bubble;

            while (WaitList.Count > 0)
            {
                bubble = WaitList[0];
                if (bubble != null)
                {
                    Application.OpenForms["GameRoom"].Controls["EventText"].Text = bubble.BubbleEffect.ChooseEffect(bubble);
                    bubble.ImageUpdate("_", true);
                    RefreshGrid(500);
                }                    
            }
        }                

        //Method that returns an image reference when spawning bubbles.
        private Bitmap SpawnType(string type)
        {
            if (type != "Random")
                Enum.TryParse(type, out Type);

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
                        Type = BubbleType.Blank;
                        return Properties.Resources.blank;
                    }
                    else if (rng > blankChance && rng <= blankChance + powerChance)
                    {
                        Type = BubbleType.Power;
                        return Properties.Resources.power;
                    }
                    else if (rng > blankChance + powerChance && rng <= blankChance + powerChance + blockChance)
                    {
                        Type = BubbleType.Block;
                        return Properties.Resources.block;
                    }
                    else if (rng > blankChance + powerChance + blockChance && rng <= blankChance + powerChance + blockChance + arrowChance)
                    {
                        rng = random.Next(1, 3);
                        switch (rng)
                        {
                            case 1:
                                Type = BubbleType.LeftRight;
                                return Properties.Resources.leftright;
                            case 2:
                                Type = BubbleType.UpDown;
                                return Properties.Resources.updown;
                            default:
                                Type = BubbleType._;
                                return null;
                        }
                    }
                    else if (rng > blankChance + powerChance + blockChance + arrowChance && rng <= blankChance + powerChance + blockChance + arrowChance + letterChance)
                    {
                        rng = random.Next(1, 7);
                        switch (rng)
                        {
                            case 1:
                                Type = BubbleType.A;
                                return Properties.Resources.a;
                            case 2:
                                Type = BubbleType.B;
                                return Properties.Resources.b;
                            case 3:
                                Type = BubbleType.C;
                                return Properties.Resources.c;
                            case 4:
                                Type = BubbleType.D;
                                return Properties.Resources.d;
                            case 5:
                                Type = BubbleType.E;
                                return Properties.Resources.e;
                            case 6:
                                Type = BubbleType.F;
                                return Properties.Resources.f;
                            default:
                                Type = BubbleType._;
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
        }

        //Enums for the status of a bubble.
        public enum BubbleState
        {
            Empty,            
            Destroyed,
            Active
        }
    }
}
