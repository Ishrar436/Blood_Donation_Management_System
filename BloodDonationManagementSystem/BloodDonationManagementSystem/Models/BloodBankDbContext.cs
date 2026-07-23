using BloodDonationManagement.Models;
using System.Reflection.Emit;

namespace BloodDonationManagement.Data;

public partial class BloodBankDbContext : DbContext
{
    public BloodBankDbContext(
        DbContextOptions<BloodBankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationId);

            entity.ToTable("Donation");

            entity.Property(e => e.CampName)
                .HasMaxLength(100);

            entity.Property(e => e.DonationDate)
                .HasColumnType("date");

            entity.HasOne(d => d.Donor)
                .WithMany(p => p.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Donation_Donor");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.HasKey(e => e.DonorId);

            entity.ToTable("Donor");

            entity.Property(e => e.BloodGroup)
                .HasMaxLength(5);

            entity.Property(e => e.City)
                .HasMaxLength(50);

            entity.Property(e => e.ContactNo)
                .HasMaxLength(20);

            entity.Property(e => e.FullName)
                .HasMaxLength(100);

            entity.Property(e => e.LastDonationDate)
                .HasColumnType("date");
        });
    }
}