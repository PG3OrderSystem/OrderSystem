using System;
using System.Collections.Generic;

namespace OrderSystem.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime DateTime { get; set; }

    public int TotalAmount { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}


/*
 
 CREATE TABLE [dbo].[Orders] (
    [OrderId]     INT      IDENTITY (1, 1) NOT NULL,
    [DateTime]    DATETIME NOT NULL,
    [TotalAmount] INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC)
);


 
 */