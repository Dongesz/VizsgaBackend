using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BackEnd.Domain.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Scoreboard> Scoreboards { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=s58.tarhely.com;port=3306;database=dongeszh_CastL;uid=dongeszh_dongesz;pwd=DorinaMate1;charset=utf8mb4;sslmode=None", ServerVersion.Parse("10.11.14-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("Users");
            b.HasKey(u => u.Id);
            b.Property(u => u.Id).HasColumnName("Id");
            b.Property(u => u.Name).HasColumnName("Name").HasMaxLength(100);
            b.Property(u => u.Email).HasColumnName("Email").HasMaxLength(255).IsRequired();
            b.Property(u => u.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(255);
            b.Property(u => u.UserType).HasColumnName("UserType");
            b.Property(u => u.CreatedAt).HasColumnName("CreatedAt");
            b.Property(u => u.UpdatedAt).HasColumnName("UpdatedAt");
        });

        modelBuilder.Entity<Scoreboard>(b =>
        {
            b.ToTable("Scoreboard");
            b.HasKey(s => s.Id);
            b.Property(s => s.Id).HasColumnName("Id");
            b.Property(s => s.UserId).HasColumnName("UserId");
            b.Property(s => s.TotalScore).HasColumnName("TotalScore").HasDefaultValue(0);
            b.Property(s => s.LastUpdated).HasColumnName("LastUpdate");
            b.HasOne(s => s.User)
             .WithOne(u => u.Scoreboard)
             .HasForeignKey<Scoreboard>(s => s.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
