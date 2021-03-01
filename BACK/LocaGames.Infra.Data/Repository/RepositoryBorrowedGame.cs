using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Infra.Data.Context;
using LocaGames.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocaGames.Infra.Data.Repository
{
    public class RepositoryBorrowedGame : RepositoryBase<BorrowedGame>, IRepositoryBorrowedGame
    {
        private readonly SqlContext _sqlContext;
        public RepositoryBorrowedGame(SqlContext sqlContext)
            : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public new async Task Update(BorrowedGame borrowedGame)
        {
            _sqlContext.BorrowedGames.Update(borrowedGame);

            await _sqlContext.SaveChangesAsync();

        }

        public new async Task<BorrowedGame> GetById(int id)
        {
            return await _sqlContext.BorrowedGames.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> ValidateAvailability(int idGame)
        {
            return !await _sqlContext.BorrowedGames.AnyAsync(x => x.IdGame == idGame);
        }

        public async Task<BorrowedGame> GetByGameAndFriend(int idGame, int idFriend)
        {
            return await _sqlContext.BorrowedGames.Where(x => x.IdGame == idGame && x.IdFriend == idFriend).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}

