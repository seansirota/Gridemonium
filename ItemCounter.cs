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

        private int _value;

        //ItemCounter class constructor. Five are created for each of the counting labels.
        public ItemCounter(Control control, string name, int amount)
        {
            Name = name;
            CounterLabel = control;            
            _value = amount;
            CounterLabel.Text = Name + ": " + _value;
        }

        //Used to update any of the item counters with a single amount parameter.
        public void UpdateCounter(int changeAmount)
        {
            _value += changeAmount;
            CounterLabel.Text = Name + ": " + _value;
        }
    }
}
