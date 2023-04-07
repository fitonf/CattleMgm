﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CattleMgm.Data.Entities
{
    public partial class praktikadbContext : DbContext
    {
        public praktikadbContext()
        {
        }

        public praktikadbContext(DbContextOptions<praktikadbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; } = null!;
        public virtual DbSet<Breed> Breed { get; set; } = null!;
        public virtual DbSet<Cattle> Cattle { get; set; } = null!;
        public virtual DbSet<Farm> Farm { get; set; } = null!;
        public virtual DbSet<Farmer> Farmer { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Role)
                    .WithMany(p => p.User)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRoles",
                        l => l.HasOne<AspNetRoles>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUsers>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<Breed>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedBy).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Cattle>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedBy).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.HasOne(d => d.Breed)
                    .WithMany(p => p.Cattle)
                    .HasForeignKey(d => d.BreedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cattle_Breed");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Cattle)
                    .HasForeignKey(d => d.FarmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cattle_Farm");
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedBy).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Place).HasMaxLength(150);

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.Farm)
                    .HasForeignKey(d => d.FarmerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Farm_Farmer");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedBy).HasMaxLength(256);

                entity.Property(e => e.PersonalNumber).HasMaxLength(10);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
