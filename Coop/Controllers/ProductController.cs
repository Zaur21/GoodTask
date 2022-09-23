using Coop.Data;
using Coop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDBContecxt db;

        public ProductController(ApplicationDBContecxt db)
        {
            this.db = db;
        }



        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = db.Products.ToList();
            return View(objProductList);
        }

        //GET
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (p.Name != null && p.EANCode != null)
            {
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }


        public IActionResult Edit(int? id)
        {

            if (id == null && id == 0)
            {
                return NotFound();
            }

            var Prod_uct = db.Products.Find(id);
            if(Prod_uct == null)
            {
                return NotFound();
            }

            return View(Prod_uct);
        }

        [HttpPost]
        public IActionResult Edit(Product p )
        {

            if(ModelState.IsValid)
            {
                db.Products.Update(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var Prod_uct = db.Products.Find(id);
            if (Prod_uct == null)
            {
                return NotFound();
            }

            return View(Prod_uct);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = db.Products.Find(id);

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
   
        }

    }
}
