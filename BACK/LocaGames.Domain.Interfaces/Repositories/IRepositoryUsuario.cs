using Admin.Domain.Entities;
using System.Threading.Tasks;

namespace Admin.Domain.Interfaces.Repositories
{
    public interface IRepositoryUsuario
    {
        Task<ApplicationUser> Obter(string email, string password);
    }
}
