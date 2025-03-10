using System;
using System.Collections.Generic;

namespace ElibAPI.Models;

public partial class TeachersGroup
{
    public int Id { get; set; }

    public int? TeacherId { get; set; }

    public int? GroupId { get; set; }
}
