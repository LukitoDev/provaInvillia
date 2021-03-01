using LocaGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Text;

namespace LocaGames.Domain.Dtos
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public static Expression<Func<Friend, FriendDto>> Default
        {
            get
            {
                return x => new FriendDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName
                };
            }
        }
    }
}
