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
        /// <summary>
        /// The constructor that is used to read the byte array.
        /// </summary>
        /// <param name="buffer">The byte array</param>
        public RegisterResponse(byte[] buffer)
            : base(buffer)
        {
        }

        /// <summary>
        /// Constructor that is used to create a new packet.
        /// </summary>
        /// <param name="packetLength">The Packet Length</param>
        /// <param name="packetType">The Packet Type.</param>
        public RegisterResponse(short packetLength, PacketTypes packetType)
            : base(packetLength, packetType)
        {
            WriteHeader(packetLength, packetType);
        }

        /// <summary>
        /// The register status of the account.
        /// </summary>
        public AccountStatus RegisterStatus
        {
            set { WriteInt16((short)value, 4); }
            get { return (AccountStatus)ReadInt16(4); }
        }

        /// <summary>
        /// Handles the response from the server.
        /// </summary>
        /// <param name="buffer">The byte array from the server.</param>
        public void Handle(byte[] buffer)
        {
            var reply = new RegisterResponse(buffer);
            Program.UIs.RegisterUi.CrossThreadAction(() =>
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

