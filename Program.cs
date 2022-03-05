using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gridemonium
{
    internal static class Program
    {
        //Main method that starts the MainMenu form.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StreamOps.CreateFile();
            MainMenu mainMenu = new MainMenu();            
            Application.Run(mainMenu);
        }
    }
}
