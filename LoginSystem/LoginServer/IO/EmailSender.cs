using System.Net;
using System.Net.Mail;
using LoginServer.Objects;

namespace LoginServer.IO
{
    public static class EmailSender
    {
        /// <summary>
        /// Send the user the validation code based on the account information that is sent to it.
        /// </summary>
        /// <param name="account">The account information.</param>
        public static void SendVerification(Account account)
        {
            var message = new MailMessage();
            message.To.Add(account.AccountEmail);
            message.From = new MailAddress(Program.Config.ConfigDictionary["SMTPEmail"], Program.Config.ConfigDictionary["SMTPDisplayName"]);
            message.Subject = "Account Verification Code";
            message.Body = "Dear " + account.AccountUsername +
                           ", \nThank you for taking the time to make an account, but to keep member accounts valid we do require" +
                           " that all users be validated to keep member count low. Lucky for you it is not hard at all to get validated.\nAll you have to do is " +
                           "login like you normally what but instead of putting you password put your validation code which is: " +
                           account.AccountActivationCode;
            var smtp = new SmtpClient(Program.Config.ConfigDictionary["SMTPHost"]);
            var credential = new NetworkCredential(Program.Config.ConfigDictionary["SMTPEmail"], Program.Config.ConfigDictionary["SMTPPassword"]);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;
            smtp.Send(message);
        }
    }
}
