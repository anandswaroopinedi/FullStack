using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer;

public partial class EmployeeDirectoryContext : DbContext
{
    public EmployeeDirectoryContext()
    {
    }

    public EmployeeDirectoryContext(DbContextOptions<EmployeeDirectoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentEntity> Departments { get; set; }

    public virtual DbSet<EmployeeEntity> Employees { get; set; }

    public virtual DbSet<LocationEntity> Locations { get; set; }

    public virtual DbSet<ProjectEntity> Projects { get; set; }

    public virtual DbSet<RoleEntity> Roles { get; set; }

    public virtual DbSet<RoleDeptLocLinkEntity> RoleDeptLocLinks { get; set; }

    public virtual DbSet<StatusEntity> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.\\SQLEXPRESS; database=EmployeeDirectory;TrustServerCertificate=True;User=sa;Password=P@ssw0rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07B25F6284");

            entity.ToTable("Department");

            entity.Property(e => e.IsDeleted).HasDefaultValue(0);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07C2CF9C51");

            entity.ToTable("Employee");

            entity.Property(e => e.Id)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JoinDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ManagerId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImage).IsUnicode(false);

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Employee__Manage__06CD04F7");

            entity.HasOne(d => d.Project).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Projec__08B54D69");

            entity.HasOne(d => d.RoleDeptLoc).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleDeptLocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__RoleDe__07C12930");

            entity.HasOne(d => d.Status).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Status__09A971A2");
        });

        modelBuilder.Entity<LocationEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC07834ED7F3");

            entity.ToTable("Location");

            entity.Property(e => e.IsDeleted).HasDefaultValue(0);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Project__3214EC07F32D0BE8");

            entity.ToTable("Project");

            entity.Property(e => e.IsDeleted).HasDefaultValue(0);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC072C55AABF");

            entity.ToTable("Role");

            entity.Property(e => e.IsDeleted).HasDefaultValue(0);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleDeptLocLinkEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleDept__3214EC07EAAD3711");

            entity.ToTable("RoleDeptLocLink");

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(0);

            entity.HasOne(d => d.Department).WithMany(p => p.RoleDeptLocLinks)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleDeptL__Depar__02084FDA");

            entity.HasOne(d => d.Location).WithMany(p => p.RoleDeptLocLinks)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleDeptL__Locat__02FC7413");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleDeptLocLinks)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleDeptL__RoleI__01142BA1");
        });

        modelBuilder.Entity<StatusEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC074FB47C7A");

            entity.ToTable("Status");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
