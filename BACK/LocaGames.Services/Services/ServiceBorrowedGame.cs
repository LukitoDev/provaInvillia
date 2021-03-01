using AutoMapper;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LocaGames.Domain.Dtos;
using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Service.Services;
using LocaGames.Domain.Interfaces.Services;
using LocaGames.Domain.Response;
using LocaGames.Domain.Entities;
using static LocaGames.Domain.Entities.BorrowedGame;

namespace LocaGames.Service.Services
{
    public class ServiceBorrowedGame : ServiceBase<BorrowedGame>, IServiceBorrowedGame
    {
        private readonly IRepositoryBorrowedGame _repositoryBorrowedGame;
        private readonly IMapper _mapper;

        public ServiceBorrowedGame(IRepositoryBorrowedGame repositoryBorrowedGame, IMapper mapper)
             : base(repositoryBorrowedGame)
        {
            _repositoryBorrowedGame = repositoryBorrowedGame;
            _mapper = mapper;
        }

        public new async Task<BorrowedGameResponse> GetById(int id)
        {
            BorrowedGame game = await _repositoryBorrowedGame.GetById(id);

            if (game == null)
                return new BorrowedGameResponse(new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Emprestimo não encontrado!"), true);
            else
                return new BorrowedGameResponse(_mapper.Map<BorrowedGame, BorrowedGameDto>(game));
        }

        public async Task<BorrowedGameResponse> Add(BorrowedGameDto borrowedGameDto)
        {
            BorrowedGame game = _mapper.Map<BorrowedGameDto, BorrowedGame>(borrowedGameDto);

            ValidationResult result = new BorrowedGameValidator(_repositoryBorrowedGame).Validate(game);

            if (!result.IsValid)
            {
                return new BorrowedGameResponse(result);
            }

            await _repositoryBorrowedGame.Add(game);

            BorrowedGameDto dto = _mapper.Map<BorrowedGame, BorrowedGameDto>(game);

            return new BorrowedGameResponse(dto, new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Jogo emprestado com sucesso!"));
        }

        public async Task<BorrowedGameResponse> Update(BorrowedGameDto borrowedGameDto)
        {
            BorrowedGame game = _mapper.Map<BorrowedGameDto, BorrowedGame>(borrowedGameDto);

            ValidationResult result = new BorrowedGameValidator(_repositoryBorrowedGame).Validate(game);

            if (!result.IsValid)
            {
                return new BorrowedGameResponse(result);
            }

            await _repositoryBorrowedGame.Update(game);

            return new BorrowedGameResponse(_mapper.Map<BorrowedGame, BorrowedGameDto>(game), new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "BorrowedGame atualizada com sucesso!"));
        }

        public async Task<BorrowedGameResponse> Delete(int id)
        {
            BorrowedGame game = await _repositoryBorrowedGame.GetById(id);

            ValidationResult result = new BorrowedGameDeleteValidator(_repositoryBorrowedGame).Validate(game);

            if (!result.IsValid)
            {
                return new BorrowedGameResponse(result);
            }

            await _repositoryBorrowedGame.Remove(game);

            return new BorrowedGameResponse(new List<NotificacaoDto>() { new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Jogo devolvido com sucesso!") }, true);
        }

        public async Task<BorrowedGameResponse> BorrowGame(BorrowDto borrowDto)
        {
            BorrowedGameDto borrowedGameDto = new BorrowedGameDto
            {
                IdFriend = borrowDto.IdFriend,
                IdGame = borrowDto.IdGame
            };

            return await Add(borrowedGameDto);
        }

        public async Task<BorrowedGameResponse> ReturnGame(BorrowDto borrowDto)
        {
            BorrowedGame borrowedGame = await _repositoryBorrowedGame.GetByGameAndFriend(borrowDto.IdGame, borrowDto.IdFriend);

            if (borrowedGame != null)
            {
                return await Delete(borrowedGame.Id);
            }

            return new BorrowedGameResponse(new List<NotificacaoDto>() { new NotificacaoDto(NotificacaoDto.TipoMensagem.Atencao, "Nenhum emprestimo encontrado!") }, true);
        }
    }
}