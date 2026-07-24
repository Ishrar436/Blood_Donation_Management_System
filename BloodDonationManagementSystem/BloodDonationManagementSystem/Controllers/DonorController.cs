using BloodDonationManagementSystem.EF;
using BloodDonationManagementSystem.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using BloodDonationManagementSystem.Models;
namespace BloodDonationManagementSystem.Controllers
{
    public class DonorController : Controller
    {
        private readonly BloodBankDbContext _context;

        public DonorController(BloodBankDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var data = _context.Donors.ToList();

            return View(data);
        }

        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var donor = _context.Donors.Find(id);

            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Donor());
        }

        
        [HttpPost]
        public IActionResult Create(Donor donor)
        {
            

            if (!ModelState.IsValid)
            {
                return View(donor);
            }

            _context.Donors.Add(donor);

            if (_context.SaveChanges() > 0)
            {
                TempData["Msg"] = "Donor added successfully";
                return RedirectToAction("Index");
            }

            TempData["Msg"] = "Donor could not be added";

            return View(donor);
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var donor = _context.Donors.Find(id);

            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        
        [HttpPost]
        public IActionResult Edit(Donor donor)
        {
            

            if (!ModelState.IsValid)
            {
                return View(donor);
            }

            var existingDonor = _context.Donors.Find(donor.DonorId);

            if (existingDonor == null)
            {
                return NotFound();
            }

            existingDonor.FullName = donor.FullName;
            existingDonor.BloodGroup = donor.BloodGroup;
            existingDonor.ContactNo = donor.ContactNo;
            existingDonor.City = donor.City;

            if (_context.SaveChanges() > 0)
            {
                TempData["Msg"] =
                    "Donor updated successfully";

                return RedirectToAction("Index");
            }

            TempData["Msg"] = "No changes were made";

            return View(donor);
        }

        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var donor = _context.Donors.Find(id);

            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var donor = _context.Donors.Find(id);

            if (donor == null)
            {
                return NotFound();
            }

            _context.Donors.Remove(donor);

            if (_context.SaveChanges() > 0)
            {
                TempData["Msg"] = "Donor deleted successfully";
            }
            else
            {
                TempData["Msg"] = "Donor could not be deleted";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FilterByBloodGroup(string bloodGroup)
        {
            List<Donor> donors = new List<Donor>();

            if (!string.IsNullOrEmpty(bloodGroup))
            {
                donors = _context.Donors.Where(d => d.BloodGroup == bloodGroup).ToList();
            }

            ViewBag.SelectedBloodGroup = bloodGroup;

            return View(donors);
        }

        [HttpGet]
        public IActionResult SortedByLastDonation()
        {
            var donors = _context.Donors.OrderByDescending(d => d.LastDonationDate).ToList();

            return View(donors);
        }

        [HttpGet]
        public IActionResult DonationCounts()
        {
            var data = _context.Donors.Select(d => new DonorDonationCount
            {
                    DonorId = d.DonorId,
                    FullName = d.FullName,
                    BloodGroup = d.BloodGroup,
                    TotalDonations = d.Donations.Count()
             }).ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult TotalBloodVolume()
        {
            int totalVolume = _context.Donations.Sum(d => (int?)d.VolumeMl) ?? 0;

            return View(totalVolume);
        }
    }
}