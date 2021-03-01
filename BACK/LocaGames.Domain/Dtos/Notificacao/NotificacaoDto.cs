using System;
using System.Collections.Generic;
using System.Text;

namespace LocaGames.Domain.Dtos
{
    public class NotificacaoDto
    {
        public NotificacaoDto(TipoMensagem tipo, string mensagem)
        {
            Tipo = tipo;
            Mensagem = mensagem;
        }

        public TipoMensagem Tipo { get; set; }

        public string Mensagem { get; set; }

        public static NotificacaoDto ErroPadrao => new NotificacaoDto(TipoMensagem.Erro, "Erro inesperado, favor entrar em contato com o Administrador do Sistema");

        public enum TipoMensagem
        {
            Sucesso,
            Erro,
            Atencao
        }
    }
}
