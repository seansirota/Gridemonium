using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridemonium
{
    public class Event
    {
        //Dictionary containing a subset of BubbleGrid where only event bubbles spawn.
        public static Dictionary<string, Bubble> EventBubbleList { get; } = new Dictionary<string, Bubble>();

        //Main hub method for the event class. Chooses the correct effect based on a random roll.
        public static string EventHub()
        {
            foreach (KeyValuePair<string, Bubble> entry in EventBubbleList)
            {
                if (entry.Value.Type != Bubble.BubbleType._)
                    return "";
            }

            Random random = new Random();
            int rng = random.Next(1, 101);
            int randomChance = Settings.RetrieveValue("EventRandomChance");
            int blockChance = Settings.RetrieveValue("EventBlockChance");
            int blankChance = Settings.RetrieveValue("EventBlankChance");
            int letterChance = Settings.RetrieveValue("EventLetterChance");

            string eventType;
            string returnText;

            if (rng > 0 && rng <= randomChance)
            {
                eventType = "Random";
                returnText = "Event: Random bubbles\nhave been spawned.";
            }
            else if (rng > randomChance && rng <= randomChance + blockChance)
            {
                eventType = "Block";
                returnText = "Event: Block bubbles\nhave been spawned.";
            }
            else if (rng > randomChance + blockChance && rng <= randomChance + blockChance + blankChance)
            {
                eventType = "Blank";
                returnText = "Event: Blank bubbles\nhave been spawned.";
            }
            else if (rng > randomChance + blockChance + blankChance && rng <= randomChance + blockChance + blankChance + letterChance)
            {
                eventType = "Letter";
                returnText = "Event: Letter bubbles\nhave been spawned.";
            }
            else
            {
                eventType = "";
                returnText = "";
            }

            if (eventType == "")
                return returnText;

            foreach (KeyValuePair<string, Bubble> entry in EventBubbleList)
            {
                entry.Value.SpawnBubble(eventType);
                Bubble.RefreshGrid(0);
            }                

            return returnText;
        }
    }
}
