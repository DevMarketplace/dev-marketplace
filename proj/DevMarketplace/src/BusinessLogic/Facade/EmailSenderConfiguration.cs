using System.Collections.Generic;
using MailKit.Security;
using MimeKit.Text;

namespace BusinessLogic.Facade
{
    public class FromConfiguration
    {
        public virtual string Name { get; set; }

        public virtual string EmailAddress { get; set; }
    }

    public class ToConfiguration : FromConfiguration
    {
        public ToConfiguration()
        {
            Recipients = new Dictionary<string, string>();
            CarbonCopyRecipients = new Dictionary<string, string>();
            BlindCarbonCopyRecipients = new Dictionary<string, string>();
        }

        public string Subject { get; set; }

        public Dictionary<string, string> Recipients { get; }

        public Dictionary<string, string> BlindCarbonCopyRecipients { get; set; }

        public Dictionary<string, string> CarbonCopyRecipients { get; set; }
    }

    public class EmailSenderConfiguration
    {
        public FromConfiguration From { get; set; }

        public ToConfiguration To { get; set; }

        public TextFormat EmailFormat { get; set; }

        public string EmailBody { get; set; }

        public int SmtpPort { get; set; } = 25;

        public string Domain { get; set; }

        public string SmtpServer { get; set; }

        public SecureSocketOptions SecureSocketOption { get; set; }
    }
}
