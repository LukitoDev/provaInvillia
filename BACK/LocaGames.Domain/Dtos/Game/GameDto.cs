using LocaGames.Domain.Entities;
using System;
using System.Linq.Expressions;
using static LocaGames.Domain.Enum.Enum;

namespace LocaGames.Domain.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Platform Platform { get; set; }
        public Genre Genre { get; set; }
        public string GameLendsWith { get; set; }
        public int? IdFriend { get; set; }

        public static Expression<Func<Game, GameDto>> Default
        {
            get
            {
                return x => new GameDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Platform = x.Platform,
                    Genre = x.Genre,
                    GameLendsWith = x.BorrowedGame.Friend.Name + " " + x.BorrowedGame.Friend.LastName,
                    IdFriend = x.BorrowedGame.IdFriend
                };
            }
        }
    }
}
