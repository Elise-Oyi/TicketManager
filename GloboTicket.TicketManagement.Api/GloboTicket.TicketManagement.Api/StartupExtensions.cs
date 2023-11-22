using GloboTicket.TicketManagement.Api.Middleware;
using GloboTicket.TicketManagement.Api.Services;
using GloboTicket.TicketManagement.Api.Utility;
using GloboTicket.TicketManagement.Application;
using GloboTicket.TicketManagement.Application.Contracts;
using GloboTicket.TicketManagement.Infrastructure;
using GloboTicket.TicketManagement.Persistence;
using GloboTicket.TicketManagement.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GloboTicket.TicketManagement.Api
{
    public static class StartupExtensions
    {
        
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            AddSwagger(builder.Services);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod().AllowAnyHeader());
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeLine(this WebApplication app)
        {

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GloboTicket Ticket Management API");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GloboTicket Ticket Management API"
                });

                c.OperationFilter<FileResultContentTypeOperationFilter>();
            });
        }

        //public static async Task ResetDatabaseAsync(this WebApplication app)
        //{
        //    using var scope = app.Services.CreateScope();

        //    try
        //    {
        //        var contexxt = scope.ServiceProvider.GetService<GloboTicketDbContext>();
        //        if(contexxt != null)
        //        {
        //            await contexxt.Database.EnsureDeletedAsync();
        //            await contexxt.Database.MigrateAsync(); 
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
        //        logger.LogError(ex, "An error occured while migrating the database");
        //    }
        //}


    }
}
