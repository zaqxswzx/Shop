﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Shop.Models;

public partial class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activity");

            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.HasIndex(e => e.MemberId, "IX_Cart_MemberId");

            entity.HasOne(d => d.Member).WithMany(p => p.Carts).HasForeignKey(d => d.MemberId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("CartItem");

            entity.HasIndex(e => e.CartId, "IX_CartItem_CartId");

            entity.HasIndex(e => e.ProductId, "IX_CartItem_ProductId");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasForeignKey(d => d.CartId);

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NewTable_PK");

            entity.ToTable("Game");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.ToTable("Level");

            entity.HasIndex(e => e.MemberId, "IX_Level_MemberId");

            entity.Property(e => e.Name).IsRequired();

            entity.HasOne(d => d.Member).WithMany(p => p.Levels).HasForeignKey(d => d.MemberId);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");

            entity.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Name).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}