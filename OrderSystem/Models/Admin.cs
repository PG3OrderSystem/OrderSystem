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


/*
 
 CREATE TABLE [dbo].[Admins] (
    [AdminId]  NVARCHAR (10) NOT NULL,
    [Password] NVARCHAR (20) NOT NULL,
    [Name]     NVARCHAR (20) NULL,
    [Position] NVARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([AdminId] ASC)
);


 
 */