using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace BackEnd.Domain.Models;

public partial class Scoreboard
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? TotalScore { get; set; }

    public DateTime? LastUpdated { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
