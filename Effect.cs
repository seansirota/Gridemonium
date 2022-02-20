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
        public string EffectType { get; set; }        

        public void ChooseEffect(Bubble bubble)
        {
            if (EffectType == "leftright")
                ActivateLeftRight(bubble.Number);
            else if (EffectType == "updown")
                ActivateUpDown(bubble.Letter);
            else if (EffectType == "power")
                ActivatePower();
            else if (EffectType == "a" || EffectType == "b" || EffectType == "c" || EffectType == "d" || EffectType == "e" || EffectType == "f")
            {
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
                        MessageBox.Show("Error: No effect activated.");
                        break;
                }
            }
            else if (EffectType == "blank" || EffectType == "block")
            {

            }                
        }

        private void ActivateLeftRight(int row)
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Number == row))
                entry.Value.ImageUpdate("destroyed");
        }

        private void ActivateUpDown(char col)
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Letter == col))
                entry.Value.ImageUpdate("destroyed");
        }

        private void ActivatePower()
        {

        }

        private void Activate1()
        {

        }

        private void Activate2()
        {

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

        private void PowerTransform()
        {

        }

        private void PowerFunnel()
        {

        }

        private void PowerSnipe()
        {

        }
    }
}
