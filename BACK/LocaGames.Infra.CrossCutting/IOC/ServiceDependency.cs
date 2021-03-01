using LocaGames.Domain.Interfaces.Services;
using LocaGames.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LocaGames.Infra.CrossCutting.IOC
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection service)
        {
            service.AddScoped<IServiceGame, ServiceGame>();
            service.AddScoped<IServiceBorrowedGame, ServiceBorrowedGame>();
            service.AddScoped<IServiceFriend, ServiceFriend>();
        }
    }
}
