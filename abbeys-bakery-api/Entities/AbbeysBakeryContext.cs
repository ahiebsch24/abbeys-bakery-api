﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace abbeys_bakery_api.Entities
{
    public partial class AbbeysBakeryContext : DbContext
    {
        public AbbeysBakeryContext()
        {
        }

        public AbbeysBakeryContext(DbContextOptions<AbbeysBakeryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(e => e.CartItemId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.MenuItemGuid)
                    .HasName("PK_MenuItemGuid");

                entity.Property(e => e.MenuItemGuid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AllergyNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FileLocation)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}