using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace BackEnd.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? PasswordHash { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Scoreboard? Scoreboard { get; set; }
}
