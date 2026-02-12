using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace BackEnd.Domain.Models;

public partial class DefaultPicture
{
    public int Id { get; set; }

    public string DefaultPictureUrl { get; set; } = null!;
}
