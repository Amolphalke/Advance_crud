using Advance_crud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Advance_crud.Controllers
{
    public class CityController : Controller
    {
        private readonly AdvanceCrudDbContext _context;
        public CityController(AdvanceCrudDbContext context)
        {
            _context = context;
          
        }

        // GET: CountryController
        public ActionResult Index()
        {
            var CityList = _context.Cities.Include(s => s.State).ToList();
            return View(CityList);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            var citylist = _context.States.ToList();
            var Statelist = citylist.OrderBy(u => u.state_name).Select(u => new State
            {
                state_id = u.state_id,
                state_name = u.state_name,
            }).ToList();

            ViewBag.Statelists = Statelist;
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            try
            {
                if (city == null)
                {
                    return NotFound();
                }
                _context.Cities.Add(city);
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
            var cityToEdit = _context.Cities.Find(id);

            if (cityToEdit == null)
            {
                return NotFound(); 
            }

            var cityList = _context.States
                                        .OrderBy(u => u.state_name)
                                        .Select(u => new State
                                        {
                                            state_id = u.state_id,
                                            state_name = u.state_name
                                        })
                                        .ToList();
            ViewBag.Statelists = cityList;
            return View(cityToEdit);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _context.Cities.Update(city);
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
                var countryToDelete = _context.Cities.Find(id);
                if (countryToDelete == null)
                {
                    return NotFound();
                }
                _context.Cities.Remove(countryToDelete);
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
