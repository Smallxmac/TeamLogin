using System;
using System.Windows.Forms;
using LoginSystem.Enums;

namespace LoginSystem.UI
{
    public partial class LoginUI : Form
    {
        Handlers.AccountHandler AH;
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
            // Basicaly it is called Modal window which means you cannot access the parent window until the other window is close.
            // I did this becuase I do not think that they should access both UIs at the same time.'
            var registerUi = new RegisterUI();
            registerUi.ShowDialog(this);
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            AH = new Handlers.AccountHandler();
            var results = AH.CheckAccount("test", "test", false);
            AccountStatus stat = AH.CheckAccount("", "", false);
            String user = Username_Box.Text;
            String pass = Password_Box.Text;
        }

    }
}
