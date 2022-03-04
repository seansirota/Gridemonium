using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gridemonium
{
    public class ItemCounter
    {
        public string Name { get; set; }
        public Control CounterLabel;
        public int Value;

        //ItemCounter class constructor. Five are created for each of the counting labels.
        public ItemCounter(Control control, string name, int amount)
        {
            Name = name;
            CounterLabel = control;
            Value = amount;
            CounterLabel.Text = Name + ": " + Value;
        }

        //Used to update any of the item counters with a single amount parameter.
        public void UpdateCounter(int changeAmount)
        {
            Value += changeAmount;
            if (Value < 0)
                Value = 0;

            CounterLabel.Text = Name + ": " + Value;
        }
    }
}
