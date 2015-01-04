using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using LoginSystem.IO;
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
        /// <summary>
        /// For now it just loads the Remember me information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            INIFile IniPraser = new INIFile("config.ini");
            Username_Box.Text = IniPraser.GetValue("Remember", "username", "");
            Password_Box.Text = IniPraser.GetValue("Remember", "password", "");
        }

        /// <summary>
        /// Opens the register UI with this login UI as its parent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Button_Click(object sender, EventArgs e)
        {
            Program.UIs.RegisterUi = new RegisterUI();
            Program.UIs.RegisterUi.ShowDialog(this);
        }

        /// <summary>
        /// Checks for valid information and then sends it to the server.
        /// This will also tell if the Username_Box contains an Email or Username.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// The Delegate to how with cross threading.
        /// </summary>
        public delegate void InvokeAction();
        /// <summary>
        /// The void that is called so that we can access the UI like we
        /// are on the same thread.
        /// </summary>
        /// <param name="call">The actions we are telling it to do.</param>
        public void CrossThreadAction(InvokeAction call)
        {
            if (IsDisposed)
                return;
            
            if (InvokeRequired)
            {
                try{ Invoke(call); }

                catch (InvalidOperationException)
                {
                    // TODO Handle Errors.
                }
            }
            else
                call();
            
        }

    }
}
