using AutoMapper;
using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;

namespace LocaGames.Infra.Shared.AutoMapper
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<GameDto, Game>();
            CreateMap<Game, GameDto>();

            CreateMap<FriendDto, Friend>();
            CreateMap<Friend, FriendDto>();

            CreateMap<BorrowedGameDto, BorrowedGame>();
            CreateMap<BorrowedGame, BorrowedGameDto>();
        }
    }
}
