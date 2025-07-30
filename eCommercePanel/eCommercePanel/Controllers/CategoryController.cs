using eCommercePanel.DATA.Context;
using eCommercePanel.DATA.Entity;
using eCommercePanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommercePanel.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyCategory()
        {
            CategoryModel model = new CategoryModel();
            eCommerceDBContext db = new eCommerceDBContext();

            model.Categories = db.Categories.Include(p => p.Products).ToList();
            return View(model);
        }

       
        public IActionResult AddCategory()
        {
            CategoryModel model = new CategoryModel();
            eCommerceDBContext context = new eCommerceDBContext();
            model.Categories = context.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddCategory(string CategoryName,string CategoryDesc)
        {
           eCommerceDBContext db = new eCommerceDBContext();
            Category category = new Category();
            category.CategoryName = CategoryName;
            category.Description = CategoryDesc;


            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("MyCategory", "Category");

           
        }
        public IActionResult EditCategory(int id)
{
    eCommerceDBContext db = new eCommerceDBContext();
    var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
    if (category == null)
        return NotFound();

    return View(category);
}

[HttpPost]
public IActionResult EditCategory(Category category)
{
    eCommerceDBContext db = new eCommerceDBContext();
    db.Categories.Update(category);
    db.SaveChanges();

    return RedirectToAction("MyCategory");
}

public IActionResult DeleteCategory(int id)
{
    eCommerceDBContext db = new eCommerceDBContext();
    var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
    if(category != null)
    {
        db.Categories.Remove(category);
        db.SaveChanges();
    }
    return RedirectToAction("MyCategory");
}

       
            
            
    }
}
