namespace ysi.tools.email.webapi.App_BL.emailproviders
{
    public class EmailServiceProvider
    {
        IEmailServiceProvider _emailServiceProvider;

        public void SetEmailServiceProvider(IEmailServiceProvider emailServiceProvider)
        {
            _emailServiceProvider = emailServiceProvider;
        }

        public void SendEmail()
        {
            _emailServiceProvider.SendEmail();
        }
    }
}