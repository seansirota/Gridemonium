using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gridemonium
{
    public class Effect
    {
        private int _score;
        public Bubble.BubbleType EffectType { get; set; }        

        //Hub method that takes access a bubble object and uses its effect type to activate a method associated with it.
        public void ChooseEffect(Bubble bubble)
        {
            switch (EffectType)
            {
                case Bubble.BubbleType.LeftRight:
                    ActivateLeftRight(bubble.Number);
                    break;
                case Bubble.BubbleType.UpDown:
                    ActivateUpDown(bubble.Letter);
                    break;
                case Bubble.BubbleType.Power:
                    ActivatePower();
                    break;
                case Bubble.BubbleType.A:
                case Bubble.BubbleType.B:
                case Bubble.BubbleType.C:
                case Bubble.BubbleType.D:
                case Bubble.BubbleType.E:
                case Bubble.BubbleType.F:
                    Random random = new Random();
                    int randomEffect = random.Next(1, 7);
                    switch (randomEffect)
                    {
                        case 1:
                            Activate1();
                            break;
                        case 2:
                            Activate2();
                            break;
                        case 3:
                            Activate3();
                            break;
                        case 4:
                            Activate4();
                            break;
                        case 5:
                            Activate5();
                            break;
                        case 6:
                            Activate6();
                            break;
                        default:
                            break;
                    }
                    break;
                case Bubble.BubbleType.Blank:
                case Bubble.BubbleType.Block:
                    break;
            }
        }

        private void ActivateLeftRight(int row)
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Number == row))
                entry.Value.ImageUpdate("Destroyed");
        }

        private void ActivateUpDown(char col)
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Letter == col))
                entry.Value.ImageUpdate("Destroyed");
        }

        private void ActivatePower()
        {
            Random random = new Random();
            int rng = random.Next(1, 4);

            switch (rng)
            {
                case 1:
                    Bubble.CounterList.FirstOrDefault(x => x.Name == "Transform").UpdateCounter(1);                    
                    break;
                case 2:
                    Bubble.CounterList.FirstOrDefault(x => x.Name == "Funnel").UpdateCounter(1);
                    break;
                case 3:
                    Bubble.CounterList.FirstOrDefault(x => x.Name == "Snipe").UpdateCounter(1);
                    break;
                default:
                    break;
            }
        }

        private void Activate1()
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Number == 3))
                entry.Value.ImageUpdate("Destroyed");
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Letter == 'D'))
                entry.Value.ImageUpdate("Destroyed");
        }

        private void Activate2()
        {
            Random random = new Random();
            int rng = random.Next(1, 8);
            char letter = (char)(rng + 64);

            Spawner.DamageSpawner(letter, 25);
        }

        private void Activate3()
        {

        }

        private void Activate4()
        {

        }

        private void Activate5()
        {

        }

        private void Activate6()
        {

        }
    }
}
