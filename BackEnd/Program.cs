using BackEnd.Application.Helpers;
using BackEnd.Application.Mappers;
using BackEnd.Application.Services;
using BackEnd.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace BackEnd;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "{JWT token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
                    NameClaimType = JwtRegisteredClaimNames.UniqueName,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));
        });

        var conn = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

        builder.Services.AddScoped<UploadHelper>();
        builder.Services.AddScoped<ProfilePictureHelper>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        builder.Services.AddScoped<IScoreboardService, ScoreboardServices>();
        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddHttpClient<IAuthApiClient, AuthApiClient>();

        var app = builder.Build();

        app.UsePathBase("/api");
        app.UseRouting();
        app.UseCors("AllowAll");
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

}
