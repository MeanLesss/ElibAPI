using System;
using System.Collections.Generic;

namespace ElibAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Token { get; set; }

    public int? GroupId { get; set; }

    public string? RemoteAddr { get; set; }

    public string? ForwardAddr { get; set; }

    public string? Image { get; set; }
}
