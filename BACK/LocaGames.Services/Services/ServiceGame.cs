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

namespace LocaGames.Service.Services
{
    public class ServiceGame : ServiceBase<Game>, IServiceGame
    {
        private readonly IRepositoryGame _repositoryGame;
        private readonly IMapper _mapper;

        public ServiceGame(IRepositoryGame repositoryGame, IMapper mapper)
             : base(repositoryGame)
        {
            _repositoryGame = repositoryGame;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GameDto>> GetAllPagination(int page, int size)
        {
            return await _repositoryGame.GetAllPagination(page, size);
        }

        public new async Task<GameResponse> GetById(int id)
        {
            Game game = await _repositoryGame.GetById(id);

            if (game == null)
                return new GameResponse(new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Game não encontrada!"), true);
            else
                return new GameResponse(_mapper.Map<Game, GameDto>(game));
        }

        public async Task<GameResponse> Add(GameDto gameDto)
        {
            Game game = _mapper.Map<GameDto, Game>(gameDto);

            ValidationResult result = new GameValidator(_repositoryGame).Validate(game);

            if (!result.IsValid)
            {
                return new GameResponse(result);
            }

            await _repositoryGame.Add(game);

            var dto = _mapper.Map<Game, GameDto>(game);

            return new GameResponse(dto, new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Game cadastrada com sucesso!"));
        }

        public async Task<GameResponse> Update(GameDto gameDto)
        {
            Game game = _mapper.Map<GameDto, Game>(gameDto);

            ValidationResult result = new GameValidator(_repositoryGame).Validate(game);

            if (!result.IsValid)
            {
                return new GameResponse(result);
            }

            await _repositoryGame.Update(game);

            return new GameResponse(_mapper.Map<Game, GameDto>(game), new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Game atualizada com sucesso!"));
        }

        public async Task<GameResponse> Delete(int id)
        {
            Game game = await _repositoryGame.GetById(id);

            ValidationResult result = new GameDeleteValidator(_repositoryGame).Validate(game);

            if (!result.IsValid)
            {
                return new GameResponse(result);
            }

            await _repositoryGame.Remove(game);

            return new GameResponse(new List<NotificacaoDto>() { new NotificacaoDto(NotificacaoDto.TipoMensagem.Sucesso, "Game deletada com sucesso!") }, true);
        }

    }
}