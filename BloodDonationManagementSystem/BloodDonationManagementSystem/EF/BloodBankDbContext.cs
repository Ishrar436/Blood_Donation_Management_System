using System;
using System.Collections.Generic;
using BloodDonationManagementSystem.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationManagementSystem.EF;

public partial class BloodBankDbContext : DbContext
{
    public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__Donation__C5082EFB577D0FDE");

            entity.ToTable("Donation");

            entity.Property(e => e.CampName).HasMaxLength(100);

            entity.HasOne(d => d.Donor).WithMany(p => p.Donations)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("FK_Donation_Donor");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.HasKey(e => e.DonorId).HasName("PK__Donor__052E3F784C79EDA1");

            entity.ToTable("Donor");

            entity.Property(e => e.BloodGroup).HasMaxLength(5);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.ContactNo).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
