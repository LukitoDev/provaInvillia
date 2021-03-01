using AutoMapper;
using LocaGames.Infra.CrossCutting.IOC;
using LocaGames.Infra.Data.Context;
using LocaGames.Infra.Shared.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocaGames.Test
{
    public class IntegrationTestFixture
    {
        public IntegrationTestFixture()
        {
            var serviceCollection = new ServiceCollection();

            var connection = "Server=host.docker.internal,1433;Database=locagames;User Id=sa;Password=12345678Ab@;;ConnectRetryCount=30";

            serviceCollection.AddDbContext<SqlContext>(options =>
            {
                options.UseSqlServer(connection, options => options.EnableRetryOnFailure());
            });

            serviceCollection.AddRepositoryDependency();
            serviceCollection.AddServiceDependency();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOMapperProfile());
            });

            IMapper mapper = config.CreateMapper();
            serviceCollection.AddSingleton(mapper);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
