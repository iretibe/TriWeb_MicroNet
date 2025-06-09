namespace MicroNet.User.Core.Repositories
{
    //This would be moved to the Email/Notification service later
    public interface IEmailRepository
    {
        void EmailMethodToUsers(string strFrom, string strSubject, string strBody, string strServer,
            int iPort, string strUser, string strPassword, bool bSSL, bool bStartTls, string strFullName,
            string strUserName, string strUserPassword, string strUserEmail);

        void EmailMethodToSysAdmins(string strFrom, string strSubject, string strBody, string strServer,
            int iPort, string strUser, string strPassword, bool bSSL, bool bStartTls, string strAdminFullName,
            string strCreatedFullName, string strCreatedUserName, string strSysAdminEmail);

        void ReloggingEmailMethodToUsers(string strFrom, string strSubject, string strBody, string strServer,
            int iPort, string strUser, string strPassword, bool bSSL, bool bStartTls, string strFullName, string strUserEmail);
    }
}
