using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.UI;

namespace LoginSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new RegisterUI());
            Application.Run(new LoginUI());
            //Remove comments from the LoginUI and Add them to register for your UI to show.
        }
    }
}
