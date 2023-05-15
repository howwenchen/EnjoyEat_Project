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

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Authority> Authorities { get; set; } = null!;
        public virtual DbSet<AuthorityUse> AuthorityUses { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CustomerService> CustomerServices { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeesLogin> EmployeesLogins { get; set; } = null!;
        public virtual DbSet<EmployeesSalary> EmployeesSalaries { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MemberLevel> MemberLevels { get; set; } = null!;
        public virtual DbSet<MemberLogin> MemberLogins { get; set; } = null!;
        public virtual DbSet<MemberPoint> MemberPoints { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Personnel> Personnel { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<ReservationInformation> ReservationInformations { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;
        public virtual DbSet<TransactionRecord> TransactionRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SQL8005.site4now.net;Initial Catalog=db_a989fe_thm101team6;User Id=db_a989fe_thm101team6_admin;Password=THM101TEAM6");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.Date });

                entity.ToTable("Attendance");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ActualEndTime).HasColumnType("datetime");

                entity.Property(e => e.ActualStartTime).HasColumnType("datetime");

                entity.Property(e => e.ExpectedEndTime).HasColumnType("datetime");

                entity.Property(e => e.ExpectedStartTime).HasColumnType("datetime");

                entity.Property(e => e.LateTime).HasComputedColumnSql("(case when datediff(minute,[ExpectedStartTime],[ActualStartTime])<(0) then (0) else datediff(minute,[ExpectedStartTime],[ActualStartTime]) end)", false);

                entity.Property(e => e.LeaveTotalHours).HasComputedColumnSql("((coalesce([SickLeave],(0))+coalesce([PersonalLeave],(0)))+coalesce([FuneralLeave],(0)))", false);

                entity.Property(e => e.OverTime).HasComputedColumnSql("(case when datediff(minute,[ExpectedEndTime],[ActualEndTime])<(0) then (0) else datediff(minute,[ExpectedEndTime],[ActualEndTime]) end)", false);

                entity.Property(e => e.WorkTotalHours)
                    .HasColumnType("datetime")
                    .HasComputedColumnSql("([ActualEndTime]-[ActualStartTime])", false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Attendance");
            });

            modelBuilder.Entity<Authority>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("Authority");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.Property(e => e.Role).HasMaxLength(10);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Authorities)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permissions_Authority");
            });

            modelBuilder.Entity<AuthorityUse>(entity =>
            {
                entity.HasKey(e => e.EmployeesId);

                entity.ToTable("AuthorityUse");

                entity.Property(e => e.EmployeesId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeesID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(20);

                entity.Property(e => e.Description).HasColumnType("text");
            });

            modelBuilder.Entity<CustomerService>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.QuestionDatetime });

                entity.ToTable("CustomerService");

                entity.Property(e => e.QuestionId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QuestionID");

                entity.Property(e => e.QuestionDatetime).HasColumnType("datetime");

                entity.Property(e => e.AnswerContent).HasColumnType("text");

                entity.Property(e => e.AnswerDatetime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.QuestionContent).HasColumnType("text");

                entity.Property(e => e.QuestionKeynote).HasMaxLength(30);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CustomerServices)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_CustomerService");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Department1)
                    .HasMaxLength(10)
                    .HasColumnName("Department");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Education).HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Gender).HasColumnType("text");

                entity.Property(e => e.IdentityId)
                    .HasMaxLength(10)
                    .HasColumnName("IdentityID")
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(6);

                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<EmployeesLogin>(entity =>
            {
                entity.HasKey(e => e.EmployeesId)
                    .HasName("PK_Logins");

                entity.ToTable("EmployeesLogin");

                entity.Property(e => e.EmployeesId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeesID");

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(15);
            });

            modelBuilder.Entity<EmployeesSalary>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_Salary");

                entity.ToTable("EmployeesSalary");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.BasicSalary).HasColumnType("money");

                entity.Property(e => e.Bonus).HasColumnType("money");

                entity.Property(e => e.OverTime).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.Performance).HasColumnType("money");

                entity.Property(e => e.TotalSalary).HasColumnType("money");

                entity.Property(e => e.Wage).HasColumnType("money");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(e => e.LevelName);

                entity.Property(e => e.LevelName).HasMaxLength(10);
            });

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

            modelBuilder.Entity<MemberLevel>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_MemberLevel_1");

                entity.ToTable("MemberLevel");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.LevelName).HasMaxLength(10);

                entity.Property(e => e.PointsId).HasColumnName("PointsID");

                entity.HasOne(d => d.LevelNameNavigation)
                    .WithMany(p => p.MemberLevels)
                    .HasForeignKey(d => d.LevelName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Levels_MemberLevel");

                entity.HasOne(d => d.Points)
                    .WithMany(p => p.MemberLevels)
                    .HasForeignKey(d => d.PointsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberLevel_MemberPoints");
            });

            modelBuilder.Entity<MemberLogin>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("MemberLogin");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.Account).HasMaxLength(20);

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.HasOne(d => d.Member)
                    .WithOne(p => p.MemberLogin)
                    .HasForeignKey<MemberLogin>(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberLogin_Members");
            });

            modelBuilder.Entity<MemberPoint>(entity =>
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

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Orders_Members");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
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

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId)
                    .ValueGeneratedNever()
                    .HasColumnName("PaymentID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaymentTypes).HasMaxLength(20);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Orders");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.PermissonId)
                    .HasName("PK_Permissons");

                entity.Property(e => e.PermissonId)
                    .ValueGeneratedNever()
                    .HasColumnName("PermissonID");

                entity.Property(e => e.PermissionContent).HasMaxLength(50);
            });

            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.DepartmentId });

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.Posting).HasMaxLength(10);

                entity.Property(e => e.ResignationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Personnel)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Personnel");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Personnel)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Personnel");

                entity.HasOne(d => d.ManagerNavigation)
                    .WithMany(p => p.Personnel)
                    .HasForeignKey(d => d.Manager)
                    .HasConstraintName("FK_AuthorityUse_Personnel");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductName).HasMaxLength(30);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.PhoneNumber);

                entity.ToTable("Reservation");

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

                entity.ToTable("ReservationInformation");

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
                entity.ToTable("Table");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Location).HasMaxLength(10);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("(N'空閒')");
            });

            modelBuilder.Entity<TransactionRecord>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Member)
                    .WithOne(p => p.TransactionRecord)
                    .HasForeignKey<TransactionRecord>(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Members_TransactionRecords");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TransactionRecords)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_TransactionRecords");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
