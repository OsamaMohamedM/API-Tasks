using System.Threading.Tasks;

namespace Authentication_Module.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string toEmail, string resetToken);
    }
}
