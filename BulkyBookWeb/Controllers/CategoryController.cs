using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();  
        }


        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(category obj) 
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Category Name and DisplayOrder cannot be the Same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category successfully created";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		// GET
		public IActionResult Edit(int? id)
		{
            if (id == null || id == 0) {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);

            if(categoryFromDb == null) { 
                
                return NotFound();
            }

			return View(categoryFromDb);
		}


		// POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("CustomError", "Category Name and DisplayOrder cannot be the Same");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
                TempData["success"] = "Category successfully updated";
                return RedirectToAction("Index");
			}
			return View(obj);
		}


        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {

                return NotFound();
            }

            return View(categoryFromDb);
        }


        // POST 
        [HttpPost]   // This function also by Delete , ActionName("Delete")
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
