using LocaGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LocaGames.Domain.Dtos
{
    public class SelectPadraoDto
    {
        public string Text { get; set; }
        public int Id { get; set; }

        public static Expression<Func<Friend, SelectPadraoDto>> ListOfFriends
        {
            get
            {
                return x => new SelectPadraoDto
                {
                    Text = x.Name + " " + x.LastName,
                    Id = x.Id
                };
            }
        }
    }
}
