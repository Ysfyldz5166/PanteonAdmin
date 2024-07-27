using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Panteon.DataAcces.UnitOfWork;
using Panteon.Repository.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Panteon.API.Behaviors;
using Panteon.API.Mapping;
using Panteon.DataAcces.GenericRepository;
using Panteon.DataAcces.PanteonDbContexts;
using Panteon.DataAcces.MongoDbContexts;
using Panteon.Repository.Buildings;

namespace Panteon.API.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // AutoMapper configuration
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // MediatR configuration
            var assembly = AppDomain.CurrentDomain.Load("Panteon.Business");
            services.AddMediatR(assembly);
            // FluentValidation configuration
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Add other services
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<PanteonDbContext>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            // MongoDB context and repository
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
        }
    }
}
