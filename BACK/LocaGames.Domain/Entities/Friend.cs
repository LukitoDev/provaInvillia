using FluentValidation;
using LocaGames.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LocaGames.Domain.Entities
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
    }

    public class FriendValidator : AbstractValidator<Friend>
    {
        readonly IRepositoryFriend _repositoryFriend;

        public FriendValidator(IRepositoryFriend repositoryFriend)
        {
            _repositoryFriend = repositoryFriend;

            RuleFor(x => x.Name).Length(0, 100).WithMessage("O Nome não pode ser maior que 100");
            RuleFor(x => x.Name).NotEmpty().WithMessage("O Nome do amigo é obrigatório.");
            RuleFor(x => x.LastName).Length(0, 100).WithMessage("O SobreNome não pode ser maior que 100");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("O SobreNome do amigo é obrigatório.");
        }
    }
    public class FriendDeleteValidator : AbstractValidator<Friend>
    {
        readonly IRepositoryFriend _repositoryFriend;

        public FriendDeleteValidator(IRepositoryFriend repositoryFriend)
        {
            _repositoryFriend = repositoryFriend;

            RuleFor(x => x.Id).MustAsync(async (friend, id, cancellation) =>
                            await _repositoryFriend.ValidateFriendWithBorrowedGame(friend.Id)).WithMessage("Não é possivel deletar o amigo está com algum jogo emprestado.");
        }
    }
}
