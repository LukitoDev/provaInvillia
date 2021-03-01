using Admin.Domain.Dtos;
using Admin.Domain.Entities;
using System.Threading.Tasks;

namespace Admin.Domain.Interfaces.Services
{
    public interface IServiceCliente : IServiceBase<Cliente>
    {
        Task<ClienteUrlDto> ObterClientePorUrl(string clienteUrl);

        Task<bool> ValidaEmailExiste(string email);

        Task<bool> ValidaNomeUrlExiste(string nomeUrl);

        Task<bool> ValidaNomeUrlDuplicado(string nomeUrl, int idCliente);

        Task MudarStatusCliente(int id);

        Task<ClienteCadastrarRetornoDto> Add(ClienteDto expositorDto);
    }
}