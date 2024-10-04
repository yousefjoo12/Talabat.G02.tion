
using Microsoft.EntityFrameworkCore;
using Talabat.APIS.Helpers;
using Talabat.Core.Reposeitories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfiles)));



            var app = builder.Build();

            using var scop = app.Services.CreateScope();

            var Services = scop.ServiceProvider;
            var _dbcontext = Services.GetRequiredService<StoreContext>();

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();     // update database
                await StoreContextSeed.SeedAsync(_dbcontext);//Data seeding
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occurred during migration");

            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
