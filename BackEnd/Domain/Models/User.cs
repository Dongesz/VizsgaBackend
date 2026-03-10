using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace BackEnd.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string AuthUserId { get; set; } = null!;
    public string? Name { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? Bio { get; set; } = "";

    public string UserType { get; set; } = "User";

    public int? DefaultPictureUrl { get; set; } = null;

    public string? CustomPictureUrl { get; set; } = null;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Scoreboard? Scoreboard { get; set; }

    public virtual DefaultPicture? DefaultPicture { get; set; }

}
