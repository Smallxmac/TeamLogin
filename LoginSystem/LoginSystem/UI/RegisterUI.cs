using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.Enums;
using LoginSystem.Handlers;
using LoginSystem.Properties;

namespace LoginSystem.UI
{
    public partial class RegisterUI : Form
    {
        private AccountHandler accountHandler;
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
                            accountHandler = new AccountHandler();
                        var results = accountHandler.RegisterAccount(username, password, email);
                        switch (results)
                        {
                            case AccountStatus.AccountCreated:
                            {
                                MessageBox.Show(this, Resources.ACCOUNT_CREATED,
                                    @"Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            case AccountStatus.AccountNameUsed:
                            {
                                MessageBox.Show(this, Resources.ACCOUNT_NAME_EXIST,
                                    @"Account User name Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            case AccountStatus.AccountEmailUsed:
                            {
                                MessageBox.Show(this, Resources.ACCOUNT_EMAIL_EXIST,
                                    @"Account Email Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            case AccountStatus.ServerError:
                            {
                                MessageBox.Show(this, Resources.SERVER_ERROR + accountHandler.LastError,
                                    @"Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            default:
                            {
                                MessageBox.Show(this, Resources.UNKNOWN_ERROR,
                                    @"Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }

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
    }
}
