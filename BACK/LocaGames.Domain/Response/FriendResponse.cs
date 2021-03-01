using LocaGames.Domain.Dtos;
using FluentValidation.Results;
using System.Collections.Generic;
using LocaGames.Domain.Response;

namespace LocaFriends.Domain.Response
{
    public class FriendResponse : BaseResponse<FriendDto>
    {
        public FriendResponse(FriendDto obj) : base(obj) { }

        public FriendResponse(FriendDto obj, List<NotificacaoDto> message) : base(obj, message) { }

        public FriendResponse(FriendDto obj, NotificacaoDto message) : base(obj, message) { }

        public FriendResponse(List<FriendDto> obj) : base(obj) { }

        public FriendResponse(List<NotificacaoDto> message) : base(message) { }

        public FriendResponse(List<NotificacaoDto> message, bool success) : base(message, success) { }

        public FriendResponse(NotificacaoDto message) : base(message) { }

        public FriendResponse(NotificacaoDto message, bool success) : base(message, success) { }

        public FriendResponse(ValidationResult validationResult) : base(validationResult) { }
    }
}
