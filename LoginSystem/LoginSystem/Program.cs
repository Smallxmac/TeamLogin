using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.IO;
using LoginSystem.ObjectModels;
using LoginSystem.UI;

namespace LoginSystem
{
    static class Program
    {
        public static Passport passport;
        public static UIs UIs;
     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UIs = new UIs();
            passport = new Passport();
            UIs.LoginUi = new LoginUI();
            UIs.AboutUi = new AboutUI();
            UIs.MainUi = new MainUI();
            UIs.RegisterUi = new RegisterUI();
            
            Application.EnableVisualStyles();
            Application.Run(UIs.LoginUi);  
        }
    }
}
