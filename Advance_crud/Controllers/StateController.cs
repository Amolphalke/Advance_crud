using Advance_crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Advance_crud.Controllers
{
    public class StateController : Controller
    {
        private readonly AdvanceCrudDbContext _context;
        public StateController(AdvanceCrudDbContext context)
        {
            _context = context;
        }

        // GET: CountryController
        public ActionResult Index()
        {
            var stateList = _context.States.Include(s => s.Country).ToList();
            return View(stateList);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
           var countrylist = _context.Countries.ToList();
           var countryList = countrylist.OrderBy(u=>u.country_name).Select(u=>new Country
            {
                country_id = u.country_id,
                country_name = u.country_name,
            }).ToList();         
            ViewBag.Countrylist = countryList;
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(State state)
        {
            try
            {
                if (state == null)
                {
                    return NotFound();
                }
                _context.States.Add(state);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            var countryToEdit = _context.States.Find(id);
            if (countryToEdit == null)
            {
                return NotFound(); 
            }
            var countryList = _context.Countries
                                        .OrderBy(u => u.country_name)
                                        .Select(u => new Country
                                        {
                                            country_id = u.country_id,
                                            country_name = u.country_name
                                        })
                                        .ToList();
            ViewBag.Countrylist = countryList;
            return View(countryToEdit);
        }


        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(State state)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _context.States.Update(state);
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
                var countryToDelete = _context.States.Find(id);
                if (countryToDelete == null)
                {
                    return NotFound();
                }
                _context.States.Remove(countryToDelete);
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
                    TempData["ErrorMessage"] = "Already used States so not allowed for Delete";
                }
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
