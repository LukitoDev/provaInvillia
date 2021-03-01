using LocaFriends.Domain.Response;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Services;
using LocaGames.Domain.Response;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;
using Xunit.Priority;

namespace LocaGames.Test
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class FriendTest : IClassFixture<IntegrationTestFixture>
    {

        private static IServiceFriend _serviceFriend;

        public FriendTest(IntegrationTestFixture fixture)
        {
            _serviceFriend = fixture.ServiceProvider.GetRequiredService<IServiceFriend>();
        }

        [Theory, Priority(0)]
        [InlineData("Teste Nome AMIGO", "Teste sobrenome")]
        public async Task Add(string name, string lastName)
        {
            var result = await _serviceFriend.Add(new Domain.Dtos.FriendDto
            {
                Name = name,
                LastName = lastName
            });

            Assert.True(result.Success);
        }

        [Theory, Priority(2)]
        [InlineData("Teste Nome AMIGO", "Teste sobrenome")]
        public async Task Delete(string name, string lastName)
        {
            Expression<Func<Friend, bool>> expression = x => x.Name == name && x.LastName == lastName;

            List<Friend> lista = await _serviceFriend.GetList(expression);
            bool result = false;

            foreach (var item in lista)
            {
                try
                {
                    await _serviceFriend.Remove(item);
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
