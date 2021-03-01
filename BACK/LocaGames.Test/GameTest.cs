using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Services;
using LocaGames.Domain.Response;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;
using static LocaGames.Domain.Enum.Enum;

namespace LocaGames.Test
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class GameTest : IClassFixture<IntegrationTestFixture>
    {

        private IServiceGame _serviceGame;

        public GameTest(IntegrationTestFixture fixture)
        {
            _serviceGame = fixture.ServiceProvider.GetRequiredService<IServiceGame>();
        }

        [Theory, Priority(0)]
        [InlineData("Teste Nome JOGO", Platform.PC, Genre.History)]
        public async Task Add(string name, Platform platform, Genre genre)
        {
            var result = await _serviceGame.Add(new Domain.Dtos.GameDto
            {
                Name = name,
                Platform = platform,
                Genre = genre
            });

            Assert.True(result.Success);
        }

        [Theory, Priority(2)]
        [InlineData("Teste Nome JOGO", Platform.PC, Genre.History)]
        public async Task Delete(string name, Platform platform, Genre genre)
        {
            Expression<Func<Game, bool>> expression = x => x.Name == name && x.Platform == platform && x.Genre == genre;

            List<Game> lista = await _serviceGame.GetList(expression);
            bool result = false;

            foreach (var item in lista)
            {
                try
                {
                    await _serviceGame.Remove(item);
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                    continue;
                }
            }

            Assert.True(result);
        }
    }
}
