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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Pass_Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO Load info from remember me
        }

        private void Register_Button_Click(object sender, EventArgs e)
        {
            var registerUi = new RegisterUI();
            registerUi.ShowDialog(this);
        }
    }
}
