using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using LoginSystem.IO;
using LoginSystem.IO.Flatfile;
using LoginSystem.Network.Packets;
using LoginSystem.Network.Sockets;
using LoginSystem.ObjectModels;
using LoginSystem.Properties;

namespace LoginSystem.UI
{
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Username_Box.Text = IniParser.ReadValue("Remember", "username", "config.ini");
            Password_Box.Text = IniParser.ReadValue("Remember", "password", "config.ini");
        }

        private void Register_Button_Click(object sender, EventArgs e)
        {
            Program.UIs.RegisterUi = new RegisterUI();
            Program.UIs.RegisterUi.ShowDialog(this);
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            String user = Username_Box.Text;
            String pass = Password_Box.Text;

            if (user.Length > 3 && pass.Length > 3)
            {
                if (user.Contains("@") && user.Contains("."))
                {
                    if (Program.passport.ClientSocket == null)
                    {
                        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Program.passport.ClientSocket = new ClientSocket(socket);
                        Program.passport.ClientSocket.Connect(Settings.Default.ServerIP, Settings.Default.ServerPort);
                    }
                    LoginRequest request = new LoginRequest((short) (user.Length + pass.Length + 8 + 3),
                        PacketTypes.LoginRequest);
                    
                    request.Username = "NONE";
                    request.Password = pass;
                    request.Email = user;
                    Program.passport.ClientSocket.SendtoServer(request.Build());
                    Login_Button.Enabled = false;
                }
                else
                {
                    if (Program.passport.ClientSocket == null)
                    {
                        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Program.passport.ClientSocket = new ClientSocket(socket);
                        Program.passport.ClientSocket.Connect(Settings.Default.ServerIP, Settings.Default.ServerPort);
                    }
                    LoginRequest request = new LoginRequest((short) (user.Length + pass.Length + 8 + 3),
                        PacketTypes.LoginRequest);

                    request.Username = user;
                    request.Password = pass;
                    request.Email = "NONE";
                    Thread.Sleep(50);// I cannot find out why it won't accept my data atm, This is a temp fix...
                    Program.passport.ClientSocket.SendtoServer(request.Build());
                    Login_Button.Enabled = false;
                }   
            }
            else 
                MessageBox.Show(this, Resources.SHORT_DATA, @"Too Few Characters", MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);     
            
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
