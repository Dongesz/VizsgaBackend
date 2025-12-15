using Microsoft.EntityFrameworkCore;
using BackEnd.Infrastructure.Database;
using BackEnd.Application.Services;
using BackEnd.Application.Mappers;

namespace BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Allow all cors, fejleszteshez megfelel, deployra javitani!
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // kulso eleresi ut az apihoz

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Conn string lekerese, Dbcontext - Database kapcsolat felallitasa
            var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

            // Seged osztalyok registralasa
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            // Interfacek registralasa (DI)
            builder.Services.AddScoped<IScoreboardService, ScoreboardServices>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            var app = builder.Build();

            // Cors aktivalasa
            app.UseCors("AllowAll");

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
