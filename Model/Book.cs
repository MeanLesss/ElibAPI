using System;
using System.Collections.Generic;

namespace ElibAPI.Models;

public partial class Book
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Path { get; set; }

    public int? UserId { get; set; }

    public int? GroupId { get; set; }
}
