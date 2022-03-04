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
        private static List<int> _numberList = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        public static Dictionary<int, Bubble.BubbleType> LetterBubbleMatch { get; } = new Dictionary<int, Bubble.BubbleType>();
        public Bubble.BubbleType EffectType { get; set; }

        //Hub method that takes access a bubble object and uses its effect type to activate a method associated with it.
        public string ChooseEffect(Bubble bubble)
        {
            switch (EffectType)
            {
                case Bubble.BubbleType.LeftRight:
                    ActivateLeftRight(bubble.Number, bubble.Letter);
                    return "Destroyed bubbles\nhorizontally.";
                case Bubble.BubbleType.UpDown:
                    ActivateUpDown(bubble.Number, bubble.Letter);
                    return "Destroyed bubbles\nvertically.";
                case Bubble.BubbleType.Power:
                    ActivatePower();
                    return "Recieved power up.";
                case Bubble.BubbleType.A:
                case Bubble.BubbleType.B:
                case Bubble.BubbleType.C:
                case Bubble.BubbleType.D:
                case Bubble.BubbleType.E:
                case Bubble.BubbleType.F:
                    int key = LetterBubbleMatch.FirstOrDefault(x => x.Value == EffectType).Key + 1;

                    switch (key)
                    {
                        case 1:
                            Activate1(bubble.Number, bubble.Letter);
                            return "Destroyed bubbles in\na plus formation.";
                        case 2:
                            Activate2();
                            return "Damaged a random\nspawner by 25%.";
                        case 3:
                            Activate3();
                            return "Damaged 3 random\nspawners by 10%\neach.";
                        case 4:
                            Activate4();
                            return "Received 1 more\nblast.";
                        case 5:
                            Activate5(bubble.Number, bubble.Letter);
                            return "Destroyed bubbles\naround the blasted\nbubble.";
                        case 6:
                            Activate6();
                            return "Transformed all block\nbubbles into blank\nbubbles.";
                        default:
                            return "An error occured.\nNo letter bubble\nchosen.";
                    }
                case Bubble.BubbleType.Blank:                    
                case Bubble.BubbleType.Block:
                    return "";
                default:
                    return "ChooseEffect error.";
            }
        }

        private void ActivateLeftRight(int row, char col)
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Number == row))
            {
                if (entry.Value.Letter != col)
                {
                    entry.Value.ImageUpdate("_", false);
                    Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(15 * Settings.RetrieveValue("ScoreMultiplier"));
                }
            }                
        }

        private void ActivateUpDown(int row, char col)
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Letter == col))
            {
                if (entry.Value.Number != row)
                {
                    entry.Value.ImageUpdate("_", false);
                    Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(15 * Settings.RetrieveValue("ScoreMultiplier"));
                }
            }                

            if (Spawner.AllSpawnersDestroyed())
                return;

            if (!Spawner.SpawnerList[col - 65].CheckPercentValue())
                _numberList.Remove(col - 65);
            else
                Spawner.DamageSpawner((char)(col - 1), 10);
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

            Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(5 * Settings.RetrieveValue("ScoreMultiplier"));
        }

        private void Activate1(int row, char col)
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Number == 3))
            {
                if (entry.Value.Letter == col && entry.Value.Number == row)
                    continue;

                entry.Value.ImageUpdate("_", false);
                Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(15 * Settings.RetrieveValue("ScoreMultiplier"));
            }
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Letter == 'D'))
            {
                if (entry.Value.Letter == col && entry.Value.Number == row)
                    continue;

                entry.Value.ImageUpdate("_", false);
                Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(15 * Settings.RetrieveValue("ScoreMultiplier"));
            }
        }

        private void Activate2()
        {
            if (Spawner.AllSpawnersDestroyed())
                return;

            Random random = new Random();
            int rng;
            char letter;
            bool rollAgain;

            do
            {
                rng = _numberList[random.Next(0, _numberList.Count)];
                if (!Spawner.SpawnerList[rng].CheckPercentValue())
                {
                    _numberList.Remove(rng);
                    rollAgain = true;
                }             
                else
                    rollAgain = false;
            } while (rollAgain);

            letter = (char)(rng + 64);
            Spawner.DamageSpawner(letter, 25);
            Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(15 * Settings.RetrieveValue("ScoreMultiplier"));
        }

        private void Activate3()
        {
            if (Spawner.AllSpawnersDestroyed())
                return;

            Random random = new Random();
            int rng;
            char letter;
            bool rollAgain;

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    if (Spawner.AllSpawnersDestroyed())
                        return;

                    rng = _numberList[random.Next(0, _numberList.Count)];
                    if (!Spawner.SpawnerList[rng].CheckPercentValue())
                    {
                        _numberList.Remove(rng);
                        rollAgain = true;
                    }
                    else
                        rollAgain = false;                           
                } while (rollAgain);

                letter = (char)(rng + 64);
                Spawner.DamageSpawner(letter, 10);
                Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(5 * Settings.RetrieveValue("ScoreMultiplier"));
            }            
        }

        private void Activate4()
        {
            Bubble.CounterList.FirstOrDefault(x => x.Name == "Ammo").UpdateCounter(1);
            Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(5 * Settings.RetrieveValue("ScoreMultiplier"));
        }

        private void Activate5(int row, char col)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    if (Bubble.BubbleGrid.ContainsKey("Bubble" + ((char)(col + i)).ToString() + (row + j).ToString()))
                    {
                        Bubble.BubbleGrid.FirstOrDefault(x => x.Value.Name == "Bubble" + ((char)(col + i)).ToString() + (row + j).ToString()).Value.ImageUpdate("_", false);
                        Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(15 * Settings.RetrieveValue("ScoreMultiplier"));
                    }
                }
            }
        }

        private void Activate6()
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.BubbleEffect.EffectType == Bubble.BubbleType.Block))
            {
                entry.Value.ImageUpdate("Blank", true);
                Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(10 * Settings.RetrieveValue("ScoreMultiplier"));
            }
        }

        //Method for effect of Tranform power up.
        public static string PowerTransform()
        {
            foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.BubbleEffect.EffectType == Bubble.BubbleType.Block))
            {
                entry.Value.ImageUpdate("Random", true);
                Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(10 * Settings.RetrieveValue("ScoreMultiplier"));
                Bubble.RefreshGrid(0);
            }

            return "Transformed all block\nbubbles into random\nbubbles.";
        }

        //Method for effect of Funnel power up.
        public static string PowerFunnel()
        {
            foreach (char letter in new List<char> { 'C', 'D', 'E' }) 
            {
                foreach (KeyValuePair<string, Bubble> entry in Bubble.BubbleGrid.Where(x => x.Value.Letter == letter))
                {
                    entry.Value.ImageUpdate("_", false);
                    Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(5 * Settings.RetrieveValue("ScoreMultiplier"));
                }
            }

            return "Destroyed all bubbles\nwithin the three\nmiddle columns.";
        }

        //Method for effect of Snipe power up.
        public static string PowerSnipe()
        {
            if (Spawner.AllSpawnersDestroyed())
                return "All Spawners have\nbeen destroyed";

            char letter;

            for (int i = 0; i < 7; i++)
            {
                if (!Spawner.SpawnerList[i].CheckPercentValue())
                    _numberList.Remove(i);
                else
                {
                    letter = (char)(i + 64);
                    Spawner.DamageSpawner(letter, 5);
                    Bubble.CounterList.FirstOrDefault(x => x.Name == "Score").UpdateCounter(5 * Settings.RetrieveValue("ScoreMultiplier"));
                }                
            }

            return "Damaged all spawners\nby 5%.";
        }
    }
}
