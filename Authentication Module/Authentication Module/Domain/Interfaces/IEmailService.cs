using System.Threading.Tasks;

namespace Authentication_Module.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string toEmail, string resetToken);
    }
}
