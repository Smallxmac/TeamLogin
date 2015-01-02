using System;
using System.Windows.Forms;
using LoginSystem.Enums;
using LoginSystem.Handlers;
using LoginSystem.Properties;

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
            var registerUi = new RegisterUI();
            registerUi.ShowDialog(this);
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            String user = Username_Box.Text;
            String pass = Password_Box.Text;

            if (user.Length > 3 && pass.Length > 3)
            {
                if (user.Contains("@") && user.Contains("."))
                {
                    AH = new AccountHandler();
                    var status = AH.CheckAccountEmail(user, pass, false);
                    switch (status)
                    {
                        case AccountStatus.AccountAuthenicated:
                        {
                            // Do main window later..
                            MessageBox.Show(this, "Welcome", "Welcome to le login");
                            break;
                        }
                        case AccountStatus.AccountInvalid:
                        {
                            MessageBox.Show(this, Resources.ACCOUNT_INVALID, @"Account Info Invalid", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        }
                        case AccountStatus.AccountBanned:
                        {
                            MessageBox.Show(this, Resources.ACCOUNT_BANNED+ AH.BanDateTime.ToShortDateString(), @"Account Banned", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        }
                        case AccountStatus.AccountNotActivated:
                        {
                            MessageBox.Show(this, Resources.ACCOUNT_NOT_ACTIVATED, @"Account Not Activated", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        }
                        case AccountStatus.ServerError:
                        {
                            MessageBox.Show(this, Resources.SERVER_ERROR+ AH.LastError, @"Our Bad", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        }
                        default:
                        {
                            MessageBox.Show(this, Resources.UNKNOWN_ERROR, @"I Really Dont Know What Just Happened", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        }

                    }

                }
                else
                {
                    AH = new AccountHandler();
                    var status = AH.CheckAccount(user, pass, false);
                    switch (status)
                    {
                        case AccountStatus.AccountAuthenicated:
                            {
                                MessageBox.Show(this, "Welcome", "Welcome to le login");
                                break;
                            }
                        case AccountStatus.AccountInvalid:
                            {
                                MessageBox.Show(this, Resources.ACCOUNT_INVALID, @"Account Info Invalid", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                            }
                        case AccountStatus.AccountBanned:
                            {
                                MessageBox.Show(this, Resources.ACCOUNT_BANNED, @"Account Banned", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                            }
                        case AccountStatus.AccountNotActivated:
                            {
                                MessageBox.Show(this, Resources.ACCOUNT_NOT_ACTIVATED, @"Account Not Activated", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                            }
                        case AccountStatus.ServerError:
                            {
                                MessageBox.Show(this, Resources.SERVER_ERROR + AH.LastError, @"Our Bad", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show(this, Resources.UNKNOWN_ERROR, @"I Really Dont Know What Just Happened", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                            }
                    }
                }   
            }
            else 
                MessageBox.Show(this, Resources.SHORT_DATA, @"Too Few Characters", MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);     
            
        }
    }
}
