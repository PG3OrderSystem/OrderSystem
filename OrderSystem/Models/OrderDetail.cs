using System;
using System.Collections.Generic;

namespace OrderSystem.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public string ProductId { get; set; } = null!;

    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public int Subtotal { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Products Product { get; set; } = null!;
}
