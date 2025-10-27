using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Scoreboard
{
    public ulong UserId { get; set; }

    public ulong? TotalScore { get; set; }

    public uint? Wins { get; set; }

    public uint? Rounds { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual User User { get; set; } = null!;
}
