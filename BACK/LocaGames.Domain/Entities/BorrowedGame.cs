using FluentValidation;
using LocaGames.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocaGames.Domain.Entities
{
    public class BorrowedGame
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Game")]
        public int IdGame { get; set; }

        [ForeignKey("Friend")]
        public int IdFriend { get; set; }
        public virtual Game Game { get; set; }
        public virtual Friend Friend { get; set; }

        public class BorrowedGameValidator : AbstractValidator<BorrowedGame>
        {
            readonly IRepositoryBorrowedGame _repositoryBorrowedGame;

            public BorrowedGameValidator(IRepositoryBorrowedGame repositoryBorrowedGame)
            {
                _repositoryBorrowedGame = repositoryBorrowedGame;

                RuleFor(x => x.IdGame).NotEmpty().WithMessage("O jogo é obrigatório.");
                RuleFor(x => x.IdFriend).NotEmpty().WithMessage("O amigo é obrigatório.");
                RuleFor(x => x.Id).MustAsync(async (borrowedGame, id, cancellation) =>
                     await _repositoryBorrowedGame.ValidateAvailability(borrowedGame.IdGame)).WithMessage("O jogo já esta emprestado.");
            }
        }

        public class BorrowedGameDeleteValidator : AbstractValidator<BorrowedGame>
        {
            readonly IRepositoryBorrowedGame _repositoryBorrowedGame;

            public BorrowedGameDeleteValidator(IRepositoryBorrowedGame repositoryBorrowedGame)
            {
                _repositoryBorrowedGame = repositoryBorrowedGame;
            }
        }
    }
}
