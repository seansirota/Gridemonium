using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gridemonium
{
    public class Spawner
    {
        public int Value;
        public Label PercentLabel;
        //List containing all spawner labels.
        public static List<Spawner> SpawnerList { get; } = new List<Spawner>();

        //Constructor for Spawner. No new spawner objects can be created after initialization of the game room.
        public Spawner(Label label)
        {
            Value = 100;
            PercentLabel = label;
            PercentLabel.Text = Value.ToString() + '%'.ToString();
        }        

        //Method that damages a spawner by a given percentage.
        public static void DamageSpawner(char letter, int percent)
        {
            //int columnNumber = Array.FindIndex(GameRoom.LetterList, x => x == letter);
            int columnNumber = letter - 64;
            Spawner spawner = SpawnerList[columnNumber];

            spawner.Value -= percent;
            if (spawner.Value < 0)
                spawner.Value = 0;
            spawner.PercentLabel.Text = spawner.Value.ToString() + '%'.ToString();
        }

        public bool CheckPercentValue()
        {
            if (Value == 0)
                return false;
            else
                return true;
        }

        public static bool AllSpawnersDestroyed()
        {
            int totalPercent = 0;

            foreach (Spawner spawner in SpawnerList)
            {
                totalPercent += spawner.Value;
            }

            if (totalPercent != 0)
                return false;
            else
                return true;
        }
    }
}
