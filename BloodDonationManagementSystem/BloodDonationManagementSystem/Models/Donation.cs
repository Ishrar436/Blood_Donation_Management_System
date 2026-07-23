using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagement.Models;

public partial class Donation
{
    [Key]
    public int DonationId { get; set; }
    public int DonorId { get; set; }
    public DateOnly DonationDate { get; set; }
    public int VolumeMl { get; set; }

    [StringLength(100)]
    public string CampName { get; set; } = null!;

    public virtual Donor Donor { get; set; } = null!;
}
