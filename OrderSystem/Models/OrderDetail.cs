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

    public virtual Product Product { get; set; } = null!;
}



/*
 
 CREATE TABLE [dbo].[OrderDetail] (
    [OrderDetailId] INT           IDENTITY (1, 1) NOT NULL,
    [ProductId]     NVARCHAR (10) NOT NULL,
    [OrderId]       INT           NOT NULL,
    [Quantity]      INT           NOT NULL,
    [Subtotal]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderDetailId] ASC),
    CONSTRAINT [FK_OrderDetail_ToOrders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]),
    CONSTRAINT [FK_OrderDetail_ToProducts] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
);
 
 */