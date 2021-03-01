using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocaGames.Domain.Interfaces.Services
{
    public interface IServiceGame : IServiceBase<Game>
    {
        Task<PaginatedList<GameDto>> GetAllPagination(int page, int size);

        new Task<GameResponse> GetById(int id);

        Task<GameResponse> Add(GameDto gameDto);

        Task<GameResponse> Update(GameDto gameDto);

        Task<GameResponse> Delete(int id);
    }
}