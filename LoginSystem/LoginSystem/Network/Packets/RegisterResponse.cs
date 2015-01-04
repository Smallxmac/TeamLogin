using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.Enums;
using LoginSystem.Properties;

namespace LoginSystem.Network.Packets
{
    public class RegisterResponse : PacketWriter
    {
        public RegisterResponse(byte[] buffer)
            : base(buffer)
        {
        }

        public RegisterResponse(short PacketLength, PacketTypes PacketType)
            : base(PacketLength, PacketType)
        {
            WriteHeader(PacketLength, PacketType);
        }

        public AccountStatus RegisterStatus
        {
            set { WriteInt16((short) value, 4); }
            get { return (AccountStatus) ReadInt16(4); }
        }


        public void Handle(byte[] buffer)
        {
            var reply = new RegisterResponse(buffer);
            Program.UIs.RegisterUi.DoUI(() =>
            {
                switch (reply.RegisterStatus)
                {
                    case AccountStatus.AccountCreated:
                        {
                            MessageBox.Show(Program.UIs.RegisterUi, Resources.ACCOUNT_CREATED,
                                @"Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    case AccountStatus.AccountNameUsed:
                        {
                            MessageBox.Show(Program.UIs.RegisterUi, Resources.ACCOUNT_NAME_EXIST,
                                @"Account User name Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case AccountStatus.AccountEmailUsed:
                        {
                            MessageBox.Show(Program.UIs.RegisterUi, Resources.ACCOUNT_EMAIL_EXIST,
                                @"Account Email Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case AccountStatus.ServerError:
                        {
                            MessageBox.Show(Program.UIs.RegisterUi, Resources.SERVER_ERROR,
                                @"Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show(Program.UIs.RegisterUi, Resources.UNKNOWN_ERROR,
                                @"Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                }
                Program.UIs.RegisterUi.Register_button.Enabled = true;
            });
        }

    }
}

