using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocaGames.Domain.Interfaces.Repositories
{
    public interface IRepositoryGame : IRepositoryBase<Game>
    {
        Task<PaginatedList<GameDto>> GetAllPagination(int page, int size);

        Task<bool> ValidateLoanedGameWithFriend(int idGame);
    }
}
