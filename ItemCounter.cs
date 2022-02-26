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

        private Control _counterLabel;
        private int _value;

        //ItemCounter class constructor. Five are created for each of the counting labels.
        public ItemCounter(Control control, string name, int amount)
        {
            Name = name;
            _counterLabel = control;            
            _value = amount;
            _counterLabel.Text = Name + ": " + _value;
        }

        //Used to update any of the item counters with a single amount parameter.
        public void UpdateCounter(int changeAmount)
        {
            _value += changeAmount;
            _counterLabel.Text = Name + ": " + _value;
        }
    }
}
