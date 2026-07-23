using BloodDonationManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationManagement.Controllers;

public class LinqController : Controller
{
    private readonly BloodBankDbContext db;

    public LinqController(BloodBankDbContext context)
    {
        db = context;
    }

    public IActionResult FilterByBloodGroup(string? bloodGroup)
    {
        ViewBag.BloodGroups = db.Donors
            .Select(d => d.BloodGroup)
            .Distinct()
            .OrderBy(g => g)
            .ToList();

        ViewBag.SelectedBloodGroup = bloodGroup;

        IQueryable<Donor> query = db.Donors;

        if (!string.IsNullOrWhiteSpace(bloodGroup))
            query = query.Where(d => d.BloodGroup == bloodGroup);

        return View(query.OrderBy(d => d.FullName).ToList());
    }

    public IActionResult SortedByLastDonation()
    {
        List<Donor> donors = db.Donors
            .OrderByDescending(d => d.LastDonationDate)
            .ToList();

        return View(donors);
    }

    public IActionResult DonationCounts()
    {
        List<DonorDonationCountViewModel> result = db.Donors
            .Select(d => new DonorDonationCountViewModel
            {
                DonorId = d.DonorId,
                FullName = d.FullName,
                BloodGroup = d.BloodGroup,
                TotalDonations = d.Donations.Count()
            })
            .OrderByDescending(d => d.TotalDonations)
            .ThenBy(d => d.FullName)
            .ToList();

        return View(result);
    }

    public IActionResult TotalVolume()
    {
        int totalVolume = db.Donations.Sum(d => (int?)d.VolumeMl) ?? 0;
        return View(totalVolume);
    }
}
