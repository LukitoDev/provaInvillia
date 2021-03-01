using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LocaGames.Infra.CrossCutting.IOC
{
    public static class RepositoryDependency
    {
        public static void AddRepositoryDependency(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryGame, RepositoryGame>();
            services.AddScoped<IRepositoryBorrowedGame, RepositoryBorrowedGame>();
            services.AddScoped<IRepositoryFriend, RepositoryFriend>();
        }
    }
}
