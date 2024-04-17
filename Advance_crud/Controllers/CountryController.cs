using Advance_crud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Advance_crud.Controllers
{
   
    public class CountryController : Controller
    {
        private readonly AdvanceCrudDbContext _context;
        public CountryController(AdvanceCrudDbContext context)
        {
            _context = context;
        }

        // GET: CountryController
        public ActionResult Index()
        {
            var CountryList = _context.Countries.ToList();
            return View(CountryList);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            var Coutrylist = _context.Countries.Find(id);
            return View(Coutrylist);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country)
        {
            try
            {
                if(country == null)
                {
                    return NotFound();
                }
                _context.Countries.Add(country);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                return View();
            }
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var CountryEdit = _context.Countries.Find(id);
            if(CountryEdit == null)
            {
                return NotFound();
            }

            return View(CountryEdit);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   
                    _context.Countries.Update(country);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View();
             
            }
            catch
            {
                return View();
            }
        }

 [HttpPost]
 [ValidateAntiForgeryToken]    
public ActionResult Delete(int id)
    {
        try
        {
            var countryToDelete = _context.Countries.Find(id);
            if (countryToDelete == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(countryToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            var sqlException = ex.InnerException?.InnerException as SqlException;
            if (sqlException != null && (sqlException.Number == 547 || sqlException.Number == 2601))
            {
                TempData["ErrorMessage"] = "Failed to delete the country. Related records exist.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the country.";
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
}
