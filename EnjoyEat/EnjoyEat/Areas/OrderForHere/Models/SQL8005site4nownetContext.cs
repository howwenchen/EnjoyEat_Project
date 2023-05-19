﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnjoyEat.Areas.OrderForHere.Models
{
    public partial class SQL8005site4nownetContext : DbContext
    {
        public SQL8005site4nownetContext()
        {
        }

        public SQL8005site4nownetContext(DbContextOptions<SQL8005site4nownetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CustomerService> CustomerService { get; set; }
        public virtual DbSet<Levels> Levels { get; set; }
        public virtual DbSet<MemberLevel> MemberLevel { get; set; }
        public virtual DbSet<MemberLogin> MemberLogin { get; set; }
        public virtual DbSet<MemberPoints> MemberPoints { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ReservationInformation> ReservationInformation { get; set; }
        public virtual DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Description).HasColumnType("text");
            });

            modelBuilder.Entity<CustomerService>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.QuestionDatetime });

                entity.Property(e => e.QuestionId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QuestionID");

                entity.Property(e => e.QuestionDatetime).HasColumnType("datetime");

                entity.Property(e => e.AnswerContent)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.AnswerDatetime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.QuestionContent)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.QuestionKeynote)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Levels>(entity =>
            {
                entity.HasKey(e => e.LevelName);

                entity.Property(e => e.LevelName).HasMaxLength(10);
            });

            modelBuilder.Entity<MemberLevel>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_MemberLevel_1");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PointsId).HasColumnName("PointsID");

                entity.HasOne(d => d.LevelNameNavigation)
                    .WithMany(p => p.MemberLevel)
                    .HasForeignKey(d => d.LevelName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Levels_MemberLevel");

                entity.HasOne(d => d.Points)
                    .WithMany(p => p.MemberLevel)
                    .HasForeignKey(d => d.PointsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberLevel_MemberPoints");
            });

            modelBuilder.Entity<MemberLogin>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Member)
                    .WithOne(p => p.MemberLogin)
                    .HasForeignKey<MemberLogin>(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberLogin_Members");
            });

            modelBuilder.Entity<MemberPoints>(entity =>
            {
                entity.HasKey(e => e.PointsId);

                entity.Property(e => e.PointsId)
                    .ValueGeneratedNever()
                    .HasColumnName("PointsID");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.GetDate).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberPoints)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_MembersPoints");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_Member");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.RegisterDay).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId);

                entity.HasIndex(e => e.OrderDetailId, "IX_OrderDetails");

                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Orders_Members");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("FK_Orders_Orders");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.PhoneNumber);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ConfirmationDate).HasColumnType("date");

                entity.Property(e => e.NumberofGuest)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ReservationDate).HasColumnType("date");

                entity.Property(e => e.ReservationTime).HasColumnType("date");
            });

            modelBuilder.Entity<ReservationInformation>(entity =>
            {
                entity.HasKey(e => e.PhoneNumber);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EMail)
                    .HasMaxLength(10)
                    .HasColumnName("E-mail")
                    .IsFixedLength();

                entity.Property(e => e.ReservationName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.PhoneNumberNavigation)
                    .WithOne(p => p.ReservationInformation)
                    .HasForeignKey<ReservationInformation>(d => d.PhoneNumber)
                    .HasConstraintName("FK_ReservationInformation_ReservationInformation");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("(N'空閒')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}