using LocaGames.Domain.Dtos;
using FluentValidation.Results;
using System.Collections.Generic;

namespace LocaGames.Domain.Response
{
    public class BorrowedGameResponse : BaseResponse<BorrowedGameDto>
    {
        public BorrowedGameResponse(BorrowedGameDto obj) : base(obj) { }

        public BorrowedGameResponse(BorrowedGameDto obj, List<NotificacaoDto> message) : base(obj, message) { }

        public BorrowedGameResponse(BorrowedGameDto obj, NotificacaoDto message) : base(obj, message) { }

        public BorrowedGameResponse(List<BorrowedGameDto> obj) : base(obj) { }

        public BorrowedGameResponse(List<NotificacaoDto> message) : base(message) { }

        public BorrowedGameResponse(List<NotificacaoDto> message, bool success) : base(message, success) { }

        public BorrowedGameResponse(NotificacaoDto message) : base(message) { }

        public BorrowedGameResponse(NotificacaoDto message, bool success) : base(message, success) { }

        public BorrowedGameResponse(ValidationResult validationResult) : base(validationResult) { }
    }
}
