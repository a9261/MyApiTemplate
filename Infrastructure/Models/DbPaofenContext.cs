using System;
using System.Collections.Generic;
using Domain.Orders.Deposit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class DbPaofenContext : DbContext
{
    public DbPaofenContext()
    {
    }

    public DbPaofenContext(DbContextOptions<DbPaofenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepositOrder> OrderDeposits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepositOrder>(entity =>
        {
            entity.ToTable("OrderDeposit");

            entity.Property(e => e.AccountName).HasMaxLength(50);
            entity.Property(e => e.AccountNo)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy).HasDefaultValueSql("((99))");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiredTime).HasColumnType("datetime");
            entity.Property(e => e.Ip)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("IP");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MemberCode)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.MerchantId);
            entity.Property(e => e.MerchantOrderNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SystemOrderNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderTime).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.ReturnUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ReturnURL");
            entity.Property(e => e.UpdateBy).HasDefaultValueSql("((99))");
        });

        //OnModelCreatingPartial(modelBuilder);
    }

    //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}