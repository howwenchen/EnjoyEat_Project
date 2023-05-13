using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnjoyEat.Models
{
    public partial class db_a989fe_thm101team6Context : DbContext
    {
        public db_a989fe_thm101team6Context()
        {
        }

        public db_a989fe_thm101team6Context(DbContextOptions<db_a989fe_thm101team6Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Member> Members { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings").Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("EnjoyEat"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.Account).HasMaxLength(20);

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.Gender).HasColumnType("text");

                entity.Property(e => e.LastName).HasMaxLength(10);

                entity.Property(e => e.LevelName).HasMaxLength(10);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.RegisterDay).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
