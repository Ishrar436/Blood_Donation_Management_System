using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagementSystem.EF.Tables;

public partial class Donor
{
    public int DonorId { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters" )]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Blood group is required")]
    [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Enter a valid blood group such as A+, B-, AB+, or O-" )]
    public string BloodGroup { get; set; } = null!;

    [Required(ErrorMessage = "Contact number is required")]
    [RegularExpression( @"^01[3-9][0-9]{8}$", ErrorMessage = "Enter a valid 11-digit Bangladeshi contact number")]
    public string ContactNo { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters" )]
    public string City { get; set; } = null!;

    public DateOnly? LastDonationDate { get; set; }

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
}
