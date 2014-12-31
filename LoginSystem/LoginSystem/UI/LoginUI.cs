using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.UI;

namespace LoginSystem
{
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO Load info from remember me
        }

        private void Register_Button_Click(object sender, EventArgs e)
        {
            // Tucker I went ahead and added this so that you know what I did and can do it for aboutUI.
            // Basicaly it is called Modal window which means you cannot access the parent window untill the other window is close.
            // I did this becuase I do not think that they should access both UIs at the same time.'
            var registerUi = new RegisterUI();
            registerUi.ShowDialog(this);
        }
    }
}
