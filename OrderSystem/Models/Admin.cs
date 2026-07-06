using System;
using System.Collections.Generic;

namespace OrderSystem.Models;

public partial class Admin
{
    public string AdminId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public string? Position { get; set; }
}
