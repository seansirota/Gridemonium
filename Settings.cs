using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridemonium
{
    public class Settings
    {
        //Dictionary holding all setting values.
        private readonly static Dictionary<string, int> _difficultyValues = new Dictionary<string, int>();

        //Method for initiating all values based on the mode chosen.
        public static void InitiateSettings
            (int BubbleArrowChance, int BubbleBlockChance, int BubblePowerChance, int BubbleBlankChance,
             int EventLetterChance, int EventBlockChance, int EventRandomChance, int EventBlankChance,
             int StartingAmmo, int ScoreMultiplier)
        {
            if (_difficultyValues.Count != 0)
                _difficultyValues.Clear();

            _difficultyValues.Add("BubbleArrowChance", BubbleArrowChance);
            _difficultyValues.Add("BubbleBlockChance", BubbleBlockChance);
            _difficultyValues.Add("BubblePowerChance", BubblePowerChance);
            _difficultyValues.Add("BubbleBlankChance", BubbleBlankChance);

            _difficultyValues.Add("EventLetterChance", EventLetterChance);
            _difficultyValues.Add("EventBlockChance", EventBlockChance);
            _difficultyValues.Add("EventRandomChance", EventRandomChance);
            _difficultyValues.Add("EventBlankChance", EventBlankChance);

            _difficultyValues.Add("StartingAmmo", StartingAmmo);
            _difficultyValues.Add("ScoreMultiplier", ScoreMultiplier);
        }

        //Method for retrieving a value from the dictionary.
        public static int RetrieveValue(string key)
        {
            int value = _difficultyValues.FirstOrDefault(x => x.Key == key).Value;
            return value;
        }
    }
}
