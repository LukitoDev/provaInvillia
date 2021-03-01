using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocaGames.Domain.Interfaces.Repositories
{
    public interface IRepositoryBorrowedGame : IRepositoryBase<BorrowedGame>
    {
        Task<bool> ValidateAvailability(int idGame);

        Task<BorrowedGame> GetByGameAndFriend(int idGame, int idFriend);
    }
}
