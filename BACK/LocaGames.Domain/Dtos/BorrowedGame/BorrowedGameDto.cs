using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LocaGames.Domain.Dtos
{
    public class BorrowedGameDto
    {
        public int Id { get; set; }

        public int IdGame { get; set; }

        public int IdFriend { get; set; }
    }
}
