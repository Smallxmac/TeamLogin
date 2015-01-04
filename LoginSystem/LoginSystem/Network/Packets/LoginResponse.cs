using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.Enums;
using LoginSystem.IO.Flatfile;
using LoginSystem.ObjectModels;
using LoginSystem.Properties;

namespace LoginSystem.Network.Packets
{
    public class LoginResponse : PacketWriter
    {
        public LoginResponse(short PacketLenght, PacketTypes PacketType)
            : base(PacketLenght, PacketType)
        {
            WriteHeader(PacketLenght, PacketType);
        }

        public LoginResponse(byte[] buffer) : base(buffer)
        {

        }

        public int UID
        {
            set { WriteInt32(value, 4); }
            get { return ReadInt32(4); }
        }

        public AccountStatus LoginStatus
        {
            set { WriteInt16((short) value.GetTypeCode(), 8); }
            get { return (AccountStatus) Enum.ToObject(typeof (AccountStatus), ReadInt16(8)); }
        }

        public DateTime BaneDateExpire
        {
            set { WriteInt64(value.ToFileTimeUtc(), 10); }
            get { return DateTime.FromFileTimeUtc(ReadInt64(10)); }
        }

        public void Handle(byte[] buffer)
        {
            Program.UIs.LoginUi.DoUI(() =>
            {
                switch (LoginStatus)
            {
                case AccountStatus.AccountAuthenicated:
                {
                    // Do main window later..
                    MessageBox.Show(Program.UIs.LoginUi, "Welcome", "Welcome to le login");
                    break;
                }
                case AccountStatus.AccountActivated:
                {
                    if (Program.UIs.LoginUi.Remember_Check.Checked)
                    {
                        IniParser.WriteValue("Remember", "username", Program.UIs.LoginUi.Username_Box.Text,"config.ini");
                        IniParser.WriteValue("Remember", "password", Program.UIs.LoginUi.Password_Box.Text, "config.ini");
                    }
                    MessageBox.Show(Program.UIs.LoginUi, Resources.ACCOUNT_ACTIVATED, @"Account Activated",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                case AccountStatus.AccountInvalid:
                {
                    MessageBox.Show(Program.UIs.LoginUi, Resources.ACCOUNT_INVALID, @"Account Info Invalid",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }
                case AccountStatus.AccountBanned:
                {
                    MessageBox.Show(Program.UIs.LoginUi, Resources.ACCOUNT_BANNED + BaneDateExpire.ToShortDateString(),
                        @"Account Banned", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }
                case AccountStatus.AccountNotActivated:
                {
                    var reply = MessageBox.Show(Program.UIs.LoginUi, Resources.ACCOUNT_NOT_ACTIVATED, @"Account Not Activated",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error);

                    if (reply == DialogResult.Yes)
                    {
                        var validaterequest = new ValidationRequest(8, PacketTypes.ValidationRequest);
                        validaterequest.UID = UID;
                        Program.passport.ClientSocket.SendtoServer(validaterequest.Build());
                    }
                    break;
                }
                case AccountStatus.ServerError:
                {
                    MessageBox.Show(Program.UIs.LoginUi, Resources.SERVER_ERROR, @"Our Bad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }
                default:
                {
                    MessageBox.Show(Program.UIs.LoginUi, Resources.UNKNOWN_ERROR, @"I Really Don't Know What Just Happened",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }

            }
            
                Program.UIs.LoginUi.Login_Button.Enabled = true;
                Program.UIs.LoginUi.Register_Button.Enabled = true;
            });
        }

    }
}
