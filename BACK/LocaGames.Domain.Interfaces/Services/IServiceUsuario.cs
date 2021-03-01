using Admin.Domain.Entities;
using System.Threading.Tasks;

namespace Admin.Domain.Interfaces.Services
{
    public interface IServiceUsuario
    {
        Task<ApplicationUser> Obter(string email, string password);
    }
}