using Admin.Domain.Dtos.Email;
using System.Threading.Tasks;

namespace Admin.Domain.Interfaces.Services
{
    public interface IServiceEmail
    {
        Task SendEmail(EmailSettings emailSettings);
    }
}