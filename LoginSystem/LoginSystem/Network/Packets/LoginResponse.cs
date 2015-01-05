using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.Enums;
using LoginSystem.IO;
using LoginSystem.ObjectModels;
using LoginSystem.Properties;

namespace LoginSystem.Network.Packets
{
    public class LoginResponse : PacketWriter
    {
        /// <summary>
        /// Constructor for building the packet.
        /// </summary>
        /// <param name="packetLenght">Packet Length</param>
        /// <param name="packetType">Packet Type</param>
        public LoginResponse(short packetLenght, PacketTypes packetType)
            : base(packetLenght, packetType)
        {
            WriteHeader(packetLenght, packetType);
        }

        /// <summary>
        /// Constructor for reading the packet.
        /// </summary>
        /// <param name="buffer">The byte array that is read from.</param>
        public LoginResponse(byte[] buffer) : base(buffer)
        {
            
        }
        /// <summary>
        /// Account UID value. 
        /// </summary>
        public int Uid
        {
            set { WriteInt32(value, 4); }
            get { return ReadInt32(4); }
        }

        /// <summary>
        /// Account login status
        /// </summary>
        public AccountStatus LoginStatus
        {
            set { WriteInt16((short)value, 8); }
            get { return (AccountStatus)ReadInt16(8); }
        }

        /// <summary>
        /// Account BanExpire date
        /// </summary>
        public DateTime BanDateExpire
        {
            set { WriteInt64(value.ToFileTimeUtc(), 10); }
            get { return DateTime.FromFileTimeUtc(ReadInt64(10)); }
        }
        /// <summary>
        /// Handles the packet information and acts accordingly.
        /// </summary>
        /// <param name="buffer">The byte Array of the packet.</param>
        public void Handle(byte[] buffer)
        {
            Program.UIs.LoginUi.CrossThreadAction(() =>
            {
                switch (LoginStatus)
            {
                case AccountStatus.AccountAuthenicated:
                {
                    if (Program.UIs.LoginUi.Remember_Check.Checked)
                    {
                        var iniPraser = new INIFile("config.ini");
                        iniPraser.SetValue("Remember", "username", Program.UIs.LoginUi.Username_Box.Text);
                        iniPraser.SetValue("Remember", "password", Program.UIs.LoginUi.Password_Box.Text);
                        iniPraser.Flush();
                    }
                    // Do main window later..
                    MessageBox.Show(Program.UIs.LoginUi, "Welcome", "Welcome to le login");
                    break;
                }
                case AccountStatus.AccountActivated:
                {
                    
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
                    MessageBox.Show(Program.UIs.LoginUi, Resources.ACCOUNT_BANNED + BanDateExpire.ToShortDateString(),
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
                        validaterequest.Uid = Uid;
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
