using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrderSystem.Models;

public partial class OrderDBContext : DbContext
{
    public OrderDBContext()
    {
    }

    public OrderDBContext(DbContextOptions<OrderDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=np:\\\\.\\pipe\\LOCALDB#SHC645BE\\tsql\\query;Initial Catalog=OrderDB;Integrated Security=True;Trust Server Certificate=True");
                                                            //Data Source = (localdb)\MSSQLLocalDB;Initial Catalog=OrderDB;Integrated Security=True;Trust Server Certificate=True
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__tmp_ms_x__C3905BCF9546C0DB");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36CE74436D9");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.ProductId).HasMaxLength(10);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_ToOrders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_ToProducts");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__tmp_ms_x__B40CC6CDC2EE6AAD");

            entity.Property(e => e.ProductId).HasMaxLength(10);
            entity.Property(e => e.Category).HasMaxLength(20);
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
