using System;
using System.Configuration;
using System.Linq;
using System.Text;
using AutoMapper;
using FluentValidation;
using hello.transaction.core.Mapper;
using hello.transaction.core.Models;
using hello.transaction.core.Repositories;
using hello.transaction.core.Services;
using hello.transaction.core.Validator;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            //enable Cross origin

            //register memory cache
            services.AddMemoryCache();

            // Add application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TransactionXmlMapper());
                mc.AddProfile(new TransactionPaymentMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.TryAddSingleton(mapper);

            //validator
            services.AddScoped<AbstractValidator<Transaction>, TransactionValidator>();


            services.AddTransient<IGenericRepository<Attachment, string>, GenericRepository<Attachment, string>>();
            services.AddTransient<IGenericRepository<Transaction, string>, GenericRepository<Transaction, string>>();

            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;

        }

    }

}
