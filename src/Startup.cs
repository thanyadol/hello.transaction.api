using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.OpenApi.Models;
using System.Net.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using hello.transaction.core.Extensions;
using hello.transaction.core.Middleware;

namespace hello.transaction.api
{
    public class Startup
    {

        public Microsoft.Extensions.Configuration.IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the 
        public void ConfigureServices(IServiceCollection services)
        {

            services.Add2C2PTransactionSynce();
            services.AddApiVersioning();

            // Adds Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "hello", Version = "v1" });
            });


            //enable Cross origin
            services.AddCors(o => o.AddPolicy("AllowCors", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(_configuration.GetValue<string>("AppSettings:CorsUrl", string.Empty))
                    //.WithOrigins(Environment.Configuration.Instance.GetCorsURL())
                    .AllowCredentials();
            }));

            services.AddHttpContextAccessor();

            services.AddMvc(options =>
            {
                // options.OutputFormatters.Insert(0, new CsvMediaTypeFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //app.UseAuthentication();
            app.UseCors("AllowCors");

            app.UseHsts();

            // Adds Swagger
            if (_configuration.GetValue<bool>("AppSettings:Swagger:IsEnabled", false) == true)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello V1");
                });
            }

            //for dependency injection service
            app.ApplicationServices.GetService<IDisposable>();
            app.ConfigureCustomExceptionMiddleware();

            //Add our new middleware to the pipeline
            app.UseMiddleware<LoggingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();

            // Execute the endpoint selected by the routing middleware
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
