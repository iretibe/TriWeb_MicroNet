using MailKit.Net.Smtp;
using MicroNet.User.Core.Repositories;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MicroNet.User.Infrastructure.Repositories
{
    //This would be moved to the Email/Notification service later
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EmailMethodToSysAdmins(string strFrom, string strSubject, string strBody,
            string strServer, int iPort, string strUser, string strPassword, bool bSSL, bool bStartTls,
            string strAdminFullName, string strCreatedFullName, string strCreatedUserName, string strSysAdminEmail)
        {
            bool bSslValue = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("UseSSL").Value);
            Boolean blnSSl1;

            strSubject = $"MicroNet User Registration";
            strBody = $"Dear {strAdminFullName}, \r\n " +
                            $"\r\n" +
                            $"A new user has been created and assigned to you. Please activate their account. \r\n " +
                            $"Below are his/her details: \r\n " +
                            $"Full Name: {strCreatedFullName} \r\n " +
                            $"Username: {strCreatedUserName} \r\n " +
                            $"\r\n" +
                            $"Thank you," +
                            $"\r\n " +
                            $"MicroNet System";

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("", strFrom));
            mailMessage.To.Add(new MailboxAddress("", strSysAdminEmail));
            mailMessage.Subject = strSubject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = strBody
            };

            using (var client = new SmtpClient())
            {
                if (bSslValue is false)
                {
                    blnSSl1 = false;
                }
                else
                {
                    blnSSl1 = true;
                }

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(strServer, iPort, blnSSl1);
                client.Authenticate(strUser, strPassword);
                client.Send(mailMessage);
                client.Disconnect(true);
            }
        }

        [Obsolete]
        public void EmailMethodToUsers(string strFrom, string strSubject, string strBody,
            string strServer, int iPort, string strUser, string strPassword, bool bSSL,
            bool bStartTls, string strFullName, string strUserName, string strUserPassword, string strUserEmail)
        {
            bool bSslValue = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("UseSSL").Value);
            Boolean blnSSl1;

            strSubject = $"MicroNet User Registration";
            strBody = $"Dear {strFullName}, \r\n " +
                            $"\r\n" +
                            $"An account has been created on your behalf. Please contact your system administrator to activate it for you. \r\n " +
                            $"Below are your credentials: \r\n " +
                            $"Username: {strUserName}  \r\n " +
                            $"Password: {strUserPassword}  \r\n " +
                            $"\r\n" +
                            $"Thank you," +
                            $"\r\n " +
                            $"MicroNet System";

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("", strFrom));
            mailMessage.To.Add(new MailboxAddress("", strUserEmail));
            mailMessage.Subject = strSubject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = strBody
            };

            using (var client = new SmtpClient())
            {
                if (bSslValue is false)
                {
                    blnSSl1 = false;
                }
                else
                {
                    blnSSl1 = true;
                }

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(strServer, iPort, blnSSl1);
                client.Authenticate(strUser, strPassword);
                client.Send(mailMessage);
                client.Disconnect(true);
            }
        }

        public void ReloggingEmailMethodToUsers(string strFrom, string strSubject, string strBody,
            string strServer, int iPort, string strUser, string strPassword, bool bSSL, bool bStartTls,
            string strFullName, string strUserEmail)
        {
            bool bSslValue = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("UseSSL").Value);
            Boolean blnSSl1;

            strSubject = $"MicroNet User Re-login";
            strBody = $"Dear {strFullName}, \r\n " +
                            $"\r\n" +
                            $"Your account has been set up successfully. Please re-login to the system. \r\n " +
                            $"\r\n" +
                            $"Thank you," +
                            $"\r\n " +
                            $"MicroNet System";

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("", strFrom));
            mailMessage.To.Add(new MailboxAddress("", strUserEmail));
            mailMessage.Subject = strSubject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = strBody
            };

            using (var client = new SmtpClient())
            {
                if (bSslValue is false)
                {
                    blnSSl1 = false;
                }
                else
                {
                    blnSSl1 = true;
                }

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(strServer, iPort, blnSSl1);
                client.Authenticate(strUser, strPassword);
                client.Send(mailMessage);
                client.Disconnect(true);
            }
        }
    }
}
