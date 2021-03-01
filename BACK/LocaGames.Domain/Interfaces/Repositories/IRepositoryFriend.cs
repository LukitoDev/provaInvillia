using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocaGames.Domain.Interfaces.Repositories
{
    public interface IRepositoryFriend : IRepositoryBase<Friend>
    {
        Task<PaginatedList<FriendDto>> GetAllPagination(int page, int size);

        Task<bool> ValidateFriendWithBorrowedGame(int idFriend);

        Task<List<SelectPadraoDto>> GetFriendsSelectList();
    }
}
