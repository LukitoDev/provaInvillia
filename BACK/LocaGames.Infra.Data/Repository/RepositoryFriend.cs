using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Infra.Data.Context;
using LocaGames.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocaGames.Infra.Data.Repository
{
    public class RepositoryFriend : RepositoryBase<Friend>, IRepositoryFriend
    {
        private readonly SqlContext _sqlContext;
        public RepositoryFriend(SqlContext sqlContext)
            : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<PaginatedList<FriendDto>> GetAllPagination(int page, int size)
        {
            var query = _sqlContext.Friends.Select(FriendDto.Default).AsQueryable();
            return await PaginatedList<FriendDto>.CreateAsync(query, page, size);
        }

        public new async Task Update(Friend sala)
        {
            _sqlContext.Friends.Update(sala);

            await _sqlContext.SaveChangesAsync();

        }

        public new async Task<Friend> GetById(int id)
        {
            return await _sqlContext.Friends.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> ValidateFriendWithBorrowedGame(int idFriend)
        {
            return !await _sqlContext.BorrowedGames.AnyAsync(x => x.IdFriend == idFriend);
        }

        public async Task<List<SelectPadraoDto>> GetFriendsSelectList()
        {
            return await _sqlContext.Friends.Select(SelectPadraoDto.ListOfFriends).OrderBy(x => x.Text).ToListAsync();
        }
    }
}

