using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace BackEnd.Domain.Models;

// Egyszeru adat osztaly, a DbContext ezt koti ossze az adatbazis megfelelo osztalyaval
public partial class Scoreboard
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? TotalScore { get; set; }

    public int? TotalXp { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual User User { get; set; } = null!;
}
