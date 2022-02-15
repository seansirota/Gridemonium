using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gridemonium
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();            
        }

        //Normal Mode button click event. Builds the room and instantiates all Bubble objects.
        //All mode specific parameters are also set here before starting the game.
        private void NormalMode_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameRoom game = new GameRoom();
            game.Show();
            game.AssignBubbles();
            game.InitiateGrid();
        }
    }
}
