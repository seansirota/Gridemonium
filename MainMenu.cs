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

        //Button click events for each difficulty mode.
        //All mode specific parameters are also set here before starting the game.
        private void EasyMode_Click(object sender, EventArgs e)
        {
            StartUpGameRoom("Easy");
        }

        private void NormalMode_Click(object sender, EventArgs e)
        {
            StartUpGameRoom("Normal");
        }

        private void HardMode_Click(object sender, EventArgs e)
        {
            StartUpGameRoom("Hard");
        }

        //Method that takes you to guide form.
        private void Guide_Click(object sender, EventArgs e)
        {
            this.Hide();
            Guide guide = new Guide();
            guide.Show();            
        } 
        
        //Method for starting up game room form.
        private void StartUpGameRoom(string mode)
        {
            Form form = Application.OpenForms["GameRoom"];

            if (Application.OpenForms["GameRoom"] == null)
            {
                this.Hide();
                GameRoom game = new GameRoom(mode);
                game.Show();
                game.MatchSpawner();                
                game.AssignBubbles();
                game.InitiateGrid();
                game.SetUpCounters();
                game.SetUpLetterBubbles();                
            }
            else
            {                
                this.Hide();
                GameRoom game = (GameRoom)form;
                form.Show();
                game.ClearGrid();
                game.RestartForm(mode);
                game.SetUpCounters();
                game.AssignBubbles();
                game.InitiateGrid();                                                           
            }            
        }
    }
}
