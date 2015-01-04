using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.Enums;
using LoginSystem.Network.Packets;
using LoginSystem.Network.Sockets;
using LoginSystem.Properties;

namespace LoginSystem.UI
{
    public partial class RegisterUI : Form
    {
        public RegisterUI()
        {
            InitializeComponent();
        }

        private void RegisterUI_Load(object sender, EventArgs e)
        {

        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Register_button_Click(object sender, EventArgs e)
        {
            string username = Username_Box.Text;
            string password = Password_Box.Text;
            string passwordAgain = PasswordAgain_box.Text;
            string email = Email_Box.Text;

            if (username.Length > 3 && password.Length > 3 && passwordAgain.Length > 3 && email.Length > 3)
            {
                if (password.Equals(passwordAgain))
                {
                    if (email.Contains('@') && email.Contains('.'))
                    {
                        Register_button.Enabled = false;
                        if (Program.passport.ClientSocket == null)
                        {
                            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            Program.passport.ClientSocket = new ClientSocket(socket);
                            Program.passport.ClientSocket.Connect(Settings.Default.ServerIP, Settings.Default.ServerPort);
                        }
                        var request = new RegiesterRequest((short) (7 + email.Length + username.Length + password.Length),
                            PacketTypes.RegisterRequest);
                        request.Username = username;
                        request.Password = password;
                        request.Email = email;
                        Thread.Sleep(50);// Another temp fix
                        Program.passport.ClientSocket.SendtoServer(request.Build());

                    }
                    else
                    {
                        MessageBox.Show(this, Resources.INVALID_EMAIL,
                            @"Invalid E-Mail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, Resources.PASSWORD_NO_MATCH,
                        @"Passwords Do Not Match", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this, Resources.SHORT_DATA,
                    @"Invaild Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public delegate void InvokeAction();

        public void DoUI(InvokeAction call)
        {
            if (IsDisposed)
            {
                return;
            }
            if (InvokeRequired)
            {
                try
                {
                    Invoke(call);
                }
                catch (InvalidOperationException)
                {
                    // Handle error
                }
            }
            else
            {
                call();
            }
        }
    }
}
