using System;
using System.Collections.Generic;

namespace OrderSystem.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int Price { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}


/*
 
 CREATE TABLE [dbo].[Products] (
    [ProductId]   NVARCHAR (10) NOT NULL,
    [ProductName] NVARCHAR (50) NOT NULL,
    [Price]       INT           NOT NULL,
    [Category]    NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC)
);
 
 */