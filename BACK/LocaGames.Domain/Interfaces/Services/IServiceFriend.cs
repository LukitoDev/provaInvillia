using LocaFriends.Domain.Response;
using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocaGames.Domain.Interfaces.Services
{
    public interface IServiceFriend : IServiceBase<Friend>
    {
        Task<PaginatedList<FriendDto>> GetAllPagination(int page, int size);

        Task<List<SelectPadraoDto>> GetFriendsSelectList();

        new Task<FriendResponse> GetById(int id);

        Task<FriendResponse> Add(FriendDto friendDto);

        Task<FriendResponse> Update(FriendDto friendDto);

        Task<FriendResponse> Delete(int id);
    }
}