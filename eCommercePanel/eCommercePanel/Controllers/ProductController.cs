using eCommercePanel.DATA.Context;
using eCommercePanel.DATA.Entity;
using eCommercePanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace eCommercePanel.Controllers
{
    public class ProductController : Controller
    {
        private readonly eCommerceDBContext db;

        public ProductController()
        {
            db = new eCommerceDBContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProducts()
        {
            ProductModel model = new ProductModel();
            model.Categories = db.Categories.ToList();
            return View(model);
        }

        public IActionResult MyProducts()
        {
            ProductModel model = new ProductModel();
            model.Products = db.Products.Include(p => p.Category).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(string ProductName, string ProductDesc, int CategoryID, int Price, string ImageUrl, int Stock)
        {
            Product product = new Product();

            product.ProductName = ProductName;
            product.Description = ProductDesc;
            product.CategoryId = CategoryID;
            product.Price = Price;
            product.Stock = Stock;
            product.ImageUrl = ImageUrl;

            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("MyProducts");
        }

        public IActionResult EditProduct(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = db.Categories.ToList();

            return View(product); // sadece Product gönderiyoruz
        }


        [HttpPost]
        public IActionResult EditProduct(Product product, IFormFile ImageFile)
        {
            var existingProduct = db.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct == null)
                return NotFound();

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                product.ImageUrl = "/img/" + fileName;
            }
            else
            {
                product.ImageUrl = existingProduct.ImageUrl;
            }

            db.Products.Update(product);
            db.SaveChanges();

            return RedirectToAction("MyProducts");
        }

    }
}   






