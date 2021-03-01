using FluentValidation.Results;
using LocaGames.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocaGames.Domain.Response
{
    public abstract class BaseResponse<T>
    {
        public bool Success { get; set; }
        public List<NotificacaoDto> Message { get; set; }
        public object Resource { get; set; }

        public BaseResponse(bool success)
        {
            Success = success;
            Message = new List<NotificacaoDto>();
            Resource = default;
        }

        protected BaseResponse(T resource)
        {
            Success = true;
            Message = new List<NotificacaoDto>();
            Resource = resource;
        }

        protected BaseResponse(T resource, List<NotificacaoDto> message)
        {
            Success = true;
            Message = message;
            Resource = resource;
        }

        protected BaseResponse(T resource, NotificacaoDto message)
        {
            Success = true;
            Message = new List<NotificacaoDto> { message };
            Resource = resource;
        }

        protected BaseResponse(List<T> resource)
        {
            Success = true;
            Message = new List<NotificacaoDto>();
            Resource = resource;
        }

        protected BaseResponse(List<NotificacaoDto> message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }

        protected BaseResponse(List<NotificacaoDto> message, bool success)
        {
            Success = success;
            Message = message;
            Resource = default;
        }

        protected BaseResponse(NotificacaoDto message)
        {
            Success = false;
            Message = new List<NotificacaoDto> { message };
            Resource = default;
        }

        protected BaseResponse(NotificacaoDto message, bool success)
        {
            Success = success;
            Message = new List<NotificacaoDto> { message };
            Resource = default;
        }

        protected BaseResponse(ValidationResult validationResult)
        {
            Message = new List<NotificacaoDto>();

            foreach (var item in validationResult.Errors)
            {
                Message.Add(new NotificacaoDto(NotificacaoDto.TipoMensagem.Erro, item.ErrorMessage));
            }

            Success = false;
            Resource = default;
        }
    }
}
