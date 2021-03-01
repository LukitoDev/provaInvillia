using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;
using static LocaGames.Domain.Enum.Enum;

namespace LocaGames.Test
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class BorrowedGameTest : IClassFixture<IntegrationTestFixture>
    {

        private IServiceBorrowedGame _serviceBorrowedGame;
        private IServiceGame _serviceGame;
        private static IServiceFriend _serviceFriend;

        public BorrowedGameTest(IntegrationTestFixture fixture)
        {
            _serviceBorrowedGame = fixture.ServiceProvider.GetRequiredService<IServiceBorrowedGame>();
            _serviceGame = fixture.ServiceProvider.GetRequiredService<IServiceGame>();
            _serviceFriend = fixture.ServiceProvider.GetRequiredService<IServiceFriend>();
        }

        [Theory, Priority(-10)]
        [InlineData("Teste Nome AMIGO Emprestimo", "Teste sobrenome Emprestimo", "Teste Nome JOGO Emprestimo", Platform.PC, Genre.History)]
        public async Task BorrowGame(string name, string lastName, string nameGame, Platform platform, Genre genre)
        {

            #region AddGame
            var game = _serviceGame.Add(new GameDto
            {
                Name = nameGame,
                Platform = platform,
                Genre = genre
            }).Result;
            #endregion

            #region AddFriend
            var friend = _serviceFriend.Add(new FriendDto
            {
                Name = name,
                LastName = lastName
            }).Result;
            #endregion           

            var result = await _serviceBorrowedGame.BorrowGame(new BorrowDto
            {
                IdFriend = ((FriendDto)friend.Resource).Id,
                IdGame = ((GameDto)game.Resource).Id
            });

            Assert.True(result.Success);
        }

        [Theory, Priority(50)]
        [InlineData("Teste Nome AMIGO Emprestimo", "Teste sobrenome Emprestimo", "Teste Nome JOGO Emprestimo", Platform.PC, Genre.History)]
        public async Task ReturnGame(string name, string lastName, string nameGame, Platform platform, Genre genre)
        {
            Thread.Sleep(4000);
            #region Game
            Expression<Func<Game, bool>> expressionGame = x => x.Name == nameGame && x.Platform == platform && x.Genre == genre;

            Game game = await _serviceGame.GetFirst(expressionGame);
            #endregion

            #region Friend
            Expression<Func<Friend, bool>> expressionFriend = x => x.Name == name && x.LastName == lastName;

            Friend friend = await _serviceFriend.GetFirst(expressionFriend);
            #endregion

            var result = await _serviceBorrowedGame.ReturnGame(new BorrowDto
            {
                IdFriend = friend.Id,
                IdGame = game.Id
            });

            Assert.True(result.Success);
        }
    }
}
