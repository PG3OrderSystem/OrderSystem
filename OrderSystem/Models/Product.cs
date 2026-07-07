using System;
using System.Collections.Generic;

namespace OrderSystem.Models;

public partial class Products
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int Price { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
