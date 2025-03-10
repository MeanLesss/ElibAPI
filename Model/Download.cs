using System;
using System.Collections.Generic;

namespace ElibAPI.Models;

public partial class Download
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? BookId { get; set; }
}
