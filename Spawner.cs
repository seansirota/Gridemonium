﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gridemonium
{
    public class Spawner
    {
        private int _value;
        private Label _percentLabel;

        //List containing all spawner labels.
        public static List<Spawner> SpawnerList { get; } = new List<Spawner>();

        public Spawner(Label label)
        {
            _value = 100;
            _percentLabel = label;
            _percentLabel.Text = _value.ToString() + '%'.ToString();
        }        

        //Method that damages a spawner by a given percentage.
        public static void DamageSpawner(char letter, int percent)
        {
            int columnNumber = Array.FindIndex(GameRoom.LetterList, x => x == letter);
            Spawner spawner = SpawnerList[columnNumber];

            spawner._value -= percent;
            if (spawner._value < 0)
                spawner._value = 0;
            spawner._percentLabel.Text = spawner._value.ToString() + '%'.ToString();
        }
    }
}
