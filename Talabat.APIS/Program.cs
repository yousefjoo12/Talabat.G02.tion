
using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;
using Talabat.APIS.Erorrs;
using Talabat.APIS.Extensions;
using Talabat.APIS.Helpers;
using Talabat.APIS.MiddleWare;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Reposeitories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;
using Talabat.Service;

namespace Talabat.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            //    Options =>
            //{
            //    Options.SerializerSettings.ReferenceLoopHandling= ReferenceLoopHandling.Ignore;
            //});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });//redis server

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentity<AppUser, IdentityRole>(Options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssure"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Authkey"] ?? string.Empty))
                };
            });


            var app = builder.Build();

            using var scop = app.Services.CreateScope();
            var Services = scop.ServiceProvider;
            var _dbcontext = Services.GetRequiredService<StoreContext>(); // Explicitly StoreContext
            var _IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>(); // Explicitly AppIdentityDbContext
            var _userManger = Services.GetRequiredService<UserManager<AppUser>>();
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();     // update database
                await StoreContextSeed.SeedAsync(_dbcontext);//Data seeding
                await _IdentityDbContext.Database.MigrateAsync();     // update Identity database
                await AppIdentityDbContextSeed.SeedUserAsync(_userManger);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occurred during migration");

            }


            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExcptionMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
