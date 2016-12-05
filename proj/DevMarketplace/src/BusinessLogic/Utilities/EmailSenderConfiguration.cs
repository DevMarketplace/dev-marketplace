#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion
using System.Collections.Generic;
using MailKit.Security;
using MimeKit.Text;

namespace BusinessLogic.Utilities
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
        public EmailSenderConfiguration()
        {
            From = new FromConfiguration();
            To = new ToConfiguration();
        }

        public FromConfiguration From { get; }

        public ToConfiguration To { get; set; }

        public TextFormat EmailFormat { get; set; }

        public string EmailBody { get; set; }

        public int SmtpPort { get; set; } = 25;

        public string Domain { get; set; }

        public string SmtpServer { get; set; }

        public SecureSocketOptions SecureSocketOption { get; set; }
    }
}
