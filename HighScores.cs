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
    public partial class HighScores : Form
    {
        public HighScores()
        {
            InitializeComponent();
        }

        private void ReturnButton2_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["MainMenu"];
            if (form != null)
            {
                this.Hide();
                form.Show();
            }
        }
    }
}
