using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocaGames.Domain.Interfaces.Services
{
    public interface IServiceBorrowedGame : IServiceBase<BorrowedGame>
    {
        new Task<BorrowedGameResponse> GetById(int id);

        Task<BorrowedGameResponse> Add(BorrowedGameDto borrowedGameDtb);

        Task<BorrowedGameResponse> Update(BorrowedGameDto borrowedGameDtb);

        Task<BorrowedGameResponse> Delete(int id);

        Task<BorrowedGameResponse> BorrowGame(BorrowDto borrowDto);

        Task<BorrowedGameResponse> ReturnGame(BorrowDto borrowDto);
    }
}