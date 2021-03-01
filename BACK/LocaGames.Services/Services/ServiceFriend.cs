using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Domain.Interfaces.Services;
using LocaGames.Domain.Response;
using AutoMapper;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LocaFriends.Domain.Response;
using LocaGames.Service.Services;

namespace LocaGames.Service.Services
{
    public class ServiceFriend : ServiceBase<Friend>, IServiceFriend
    {
        private readonly IRepositoryFriend _repositoryFriend;
        private readonly IMapper _mapper;

        public ServiceFriend(IRepositoryFriend repositoryFriend, IMapper mapper)
             : base(repositoryFriend)
        {
            _repositoryFriend = repositoryFriend;
            _mapper = mapper;
        }

        public async Task<PaginatedList<FriendDto>> GetAllPagination(int page, int size)
        {
            return await _repositoryFriend.GetAllPagination(page, size);
        }

        public new async Task<FriendResponse> GetById(int id)
        {
            Friend friend = await _repositoryFriend.GetById(id);

            if (friend == null)
                return new FriendResponse(new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Friend não encontrada!"), true);
            else
                return new FriendResponse(_mapper.Map<Friend, FriendDto>(friend));
        }

        public async Task<List<SelectPadraoDto>> GetFriendsSelectList()
        {
            return await _repositoryFriend.GetFriendsSelectList();
        }

        public async Task<FriendResponse> Add(FriendDto friendDto)
        {
            Friend friend = _mapper.Map<FriendDto, Friend>(friendDto);

            ValidationResult result = new FriendValidator(_repositoryFriend).Validate(friend);

            if (!result.IsValid)
            {
                return new FriendResponse(result);
            }

            await _repositoryFriend.Add(friend);

            var dto = _mapper.Map<Friend, FriendDto>(friend);

            return new FriendResponse(dto, new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Amigo cadastrado com sucesso!"));
        }

        public async Task<FriendResponse> Update(FriendDto friendDto)
        {
            Friend friend = _mapper.Map<FriendDto, Friend>(friendDto);

            ValidationResult result = new FriendValidator(_repositoryFriend).Validate(friend);

            if (!result.IsValid)
            {
                return new FriendResponse(result);
            }

            await _repositoryFriend.Update(friend);

            return new FriendResponse(_mapper.Map<Friend, FriendDto>(friend), new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Amigo atualizado com sucesso!"));
        }

        public async Task<FriendResponse> Delete(int id)
        {
            Friend friend = await _repositoryFriend.GetById(id);

            ValidationResult result = new FriendDeleteValidator(_repositoryFriend).Validate(friend);

            if (!result.IsValid)
            {
                return new FriendResponse(result);
            }

            await _repositoryFriend.Remove(friend);

            return new FriendResponse(new List<NotificacaoDto>() { new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "O amigo foi deletado com sucesso!") }, true);
        }
    }
}