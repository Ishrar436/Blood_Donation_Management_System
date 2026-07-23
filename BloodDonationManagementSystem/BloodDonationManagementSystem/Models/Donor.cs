using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagement.Models;

public partial class Donor
{
    [Key]
    public int DonorId { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(3)]
    public string BloodGroup { get; set; } = null!;

    [StringLength(20)]
    public string ContactNo { get; set; } = null!;

    [StringLength(50)]
    public string City { get; set; } = null!;

    public DateOnly? LastDonationDate { get; set; }

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
}
