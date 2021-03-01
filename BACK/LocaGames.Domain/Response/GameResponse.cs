using LocaGames.Domain.Dtos;
using FluentValidation.Results;
using System.Collections.Generic;

namespace LocaGames.Domain.Response
{
    public class GameResponse : BaseResponse<GameDto>
    {
        public GameResponse(GameDto obj) : base(obj) { }

        public GameResponse(GameDto obj, List<NotificacaoDto> message) : base(obj, message) { }

        public GameResponse(GameDto obj, NotificacaoDto message) : base(obj, message) { }

        public GameResponse(List<GameDto> obj) : base(obj) { }

        public GameResponse(List<NotificacaoDto> message) : base(message) { }

        public GameResponse(List<NotificacaoDto> message, bool success) : base(message, success) { }

        public GameResponse(NotificacaoDto message) : base(message) { }

        public GameResponse(NotificacaoDto message, bool success) : base(message, success) { }

        public GameResponse(ValidationResult validationResult) : base(validationResult) { }
    }
}
