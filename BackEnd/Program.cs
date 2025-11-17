using Microsoft.EntityFrameworkCore;
using BackEnd.Domain.Models;

namespace BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1?? CORS hozzáadása
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 2?? DB context regisztrálása (ez legyen itt!)
            var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

            builder.Services.AddAutoMapper(typeof(BackEnd.Application.Mappers.AutoMapperProfile).Assembly);


            var app = builder.Build();

            // 3?? CORS engedélyezése
            app.UseCors("AllowAll");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run(); ;
        }
    }
}
