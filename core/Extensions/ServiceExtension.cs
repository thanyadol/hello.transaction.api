using System;
using System.Configuration;
using System.Linq;
using System.Text;
using hello.transaction.core.Models;
using hello.transaction.core.Repositories;
using hello.transaction.core.Services;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hello.transaction.core.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection Add2C2PTransactionSynce(this IServiceCollection services)
        {
            //in memory
            services.AddDbContext<NorthwindContext>(options =>
                options.UseInMemoryDatabase("Northwind"));

            //services.AddDbContext<NorthwindContext>(options =>
            //options.UseMySql(this._configuration.GetConnectionString("NorthwindConnection"),
            // x => x.MigrationsHistoryTable("__EFMigrationsHistory", "dbo")));

            //add an APIs Service
            //services.AddHttpClient<IGoogleService, GoogleService>().SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //for http request information
            services.AddHttpContextAccessor();

            //http client factory
            //Set 5 min as the lifetime for the HttpMessageHandler objects in the pool used for the Catalog Typed Client 
            //services.AddHttpClient<IClientService, ClientService>()
            //.SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //register DI
            //services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            //services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;

        }

    }

}
