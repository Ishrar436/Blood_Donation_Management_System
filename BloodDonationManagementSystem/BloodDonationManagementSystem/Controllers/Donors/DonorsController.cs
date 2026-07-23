using BloodDonationManagement.Data;
using BloodDonationManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationManagement.Controllers;

public class DonorsController : Controller
{
    private readonly BloodBankDbContext db;

    public DonorsController(BloodBankDbContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        return View(db.Donors.OrderBy(d => d.DonorId).ToList());
    }

    public IActionResult Details(int? id)
    {
        if (id == null) return BadRequest();

        var donor = db.Donors.FirstOrDefault(d => d.DonorId == id);
        if (donor == null) return NotFound();

        return View(donor);
    }

    

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        [Bind("FullName,BloodGroup,ContactNo,City,LastDonationDate")] Donor donor)
    {
        if (ModelState.IsValid)
        {
            db.Donors.Add(donor);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(donor);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return BadRequest();

        var donor = db.Donors.Find(id);
        if (donor == null) return NotFound();

        return View(donor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(
        int id,
        [Bind("DonorId,FullName,BloodGroup,ContactNo,City,LastDonationDate")] Donor donor)
    {
        if (id != donor.DonorId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                db.Update(donor);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.Donors.Any(d => d.DonorId == donor.DonorId))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(donor);
    }

    public IActionResult Delete(int id)
    {
        if (id == null) return BadRequest();

        var donor = db.Donors.FirstOrDefault(d => d.DonorId == id);
        if (donor == null) return NotFound();

        return View(donor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var donor = db.Donors.Find(id);

        if (donor != null)
        {
            db.Donors.Remove(donor);
            db.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }
}
