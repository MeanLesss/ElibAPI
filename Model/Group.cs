using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace ElibAPI.Models;

public partial class Group
{
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Group_For { get; set; }

}
