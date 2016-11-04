using System.Threading.Tasks;

namespace BusinessLogic.Utilities
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailSenderConfiguration configuration);
    }
}
