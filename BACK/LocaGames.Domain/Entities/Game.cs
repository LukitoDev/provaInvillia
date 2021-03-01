using LocaGames.Domain.Interfaces.Repositories;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using static LocaGames.Domain.Enum.Enum;

namespace LocaGames.Domain.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public Platform Platform { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public virtual BorrowedGame BorrowedGame { get; set; }
    }

    public class GameValidator : AbstractValidator<Game>
    {
        readonly IRepositoryGame _repositoryGame;

        public GameValidator(IRepositoryGame repositoryGame)
        {
            _repositoryGame = repositoryGame;

            RuleFor(x => x.Name).Length(0, 100).WithMessage("Nome não pode ser maior que 100");
            RuleFor(x => x.Name).NotEmpty().WithMessage("O Nome do jogo é obrigatório.");
            RuleFor(x => x.Platform).NotNull().WithMessage("A Plataforma do jogo é obrigatória.");
            RuleFor(x => x.Genre).NotNull().WithMessage("O gênero do jogo é obrigatório.");
        }
    }
    public class GameDeleteValidator : AbstractValidator<Game>
    {
        readonly IRepositoryGame _repositoryGame;

        public GameDeleteValidator(IRepositoryGame repositoryGame)
        {
            _repositoryGame = repositoryGame;

            RuleFor(x => x.Id).MustAsync(async (game, id, cancellation) =>
                await _repositoryGame.ValidateLoanedGameWithFriend(game.Id)).WithMessage("Não é possivel deletar o jogo, pois o mesmo está emprestado.");
        }
    }
}
