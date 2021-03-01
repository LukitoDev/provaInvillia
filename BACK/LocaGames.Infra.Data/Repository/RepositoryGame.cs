using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocaGames.Infra.Data.Repository
{
    public class RepositoryGame : RepositoryBase<Game>, IRepositoryGame
    {
        private readonly SqlContext _sqlContext;
        public RepositoryGame(SqlContext sqlContext)
            : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<PaginatedList<GameDto>> GetAllPagination(int page, int size)
        {
            var query = _sqlContext.Games.Select(GameDto.Default).AsQueryable();
            return await PaginatedList<GameDto>.CreateAsync(query, page, size);
        }

        public new async Task Update(Game sala)
        {
            _sqlContext.Games.Update(sala);

            await _sqlContext.SaveChangesAsync();

        }

        public new async Task<Game> GetById(int id)
        {
            return await _sqlContext.Games.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> ValidateLoanedGameWithFriend(int idGame)
        {
            return !await _sqlContext.BorrowedGames.AnyAsync(x => x.IdGame == idGame);
        }
    }
}

