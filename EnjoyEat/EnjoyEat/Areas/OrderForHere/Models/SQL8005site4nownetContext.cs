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
        public virtual DbSet<MemberLogin> MemberLogin { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ReservationInformation> ReservationInformation { get; set; }
        public virtual DbSet<SubCategories> SubCategories { get; set; }
        public virtual DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CustomerService>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK_CustomerService_1");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.AnswerContent).HasColumnType("text");

                entity.Property(e => e.AnswerDatetime).HasColumnType("datetime");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.QuestionContent)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.QuestionDatetime).HasColumnType("datetime");

                entity.Property(e => e.QuestionKeynote)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ServiceOption)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Levels>(entity =>
            {
                entity.HasKey(e => e.LevelName);

                entity.Property(e => e.LevelName).HasMaxLength(10);
            });

            modelBuilder.Entity<MemberLogin>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Member)
                    .WithOne(p => p.MemberLogin)
                    .HasForeignKey<MemberLogin>(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberLogin_Members");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_Member");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Gender).HasMaxLength(2);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LevelName).HasMaxLength(10);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.RegisterDay).HasColumnType("datetime");

                entity.HasOne(d => d.LevelNameNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.LevelName)
                    .HasConstraintName("FK_Members_Levels");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.OrderDetialId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderDetialID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

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

                entity.Property(e => e.FinalPrice).HasComputedColumnSql("(([TotalPrice]-[CampaignDiscount])*[LevelDiscount])", false);

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
                    .HasConstraintName("FK_Table_Orders");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.MealImg).IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK_Reservation_1");

                entity.Property(e => e.ConfirmationDate).HasColumnType("date");

                entity.Property(e => e.NumberofAdultGuest)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NumberofKidGuest)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ReservationDate).HasColumnType("date");

                entity.Property(e => e.ReservationTime).HasMaxLength(10);
            });

            modelBuilder.Entity<ReservationInformation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK_ReservationInformation_1");

                entity.Property(e => e.ReserveId).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ReservationName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Reserve)
                    .WithOne(p => p.ReservationInformation)
                    .HasForeignKey<ReservationInformation>(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationInformation_Reservation");
            });

            modelBuilder.Entity<SubCategories>(entity =>
            {
                entity.HasKey(e => e.SubCategoryId)
                    .HasName("PK_SubCategories_1");

                entity.Property(e => e.SubCategoriesName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categories _SubCategories");
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