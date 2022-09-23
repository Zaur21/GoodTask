using Coop.Data;
using Coop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Coop.Controllers
{
    public class AssortmentController : Controller
    {
        private readonly ApplicationDBContecxt db;

        public AssortmentController(ApplicationDBContecxt db)
        {
            this.db = db;
        }

        //public Product _Product { get; set; }

        public IActionResult Index()
        {
            IEnumerable<Assortment> objAssorttment = db.Assortments.ToList();
            return View(objAssorttment);
        }

        //GET
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Assortment a)
        {
            if (a.Name != null && a.ActiveFrom != new DateTime())
            {
                db.Assortments.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var assort = db.Assortments.Find(id);
            if (assort == null)
            {
                return NotFound();
            }

            return View(assort);

        }

        [HttpPost]
        public IActionResult Edit(Assortment a)
        {
            if (ModelState.IsValid)
            {
                db.Entry(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var assort = db.Assortments.Find(id);
            if (assort == null)
            {
                return NotFound();
            }

            return View(assort);
        }

        [HttpPost]
        public IActionResult Delete(Assortment a)
        {
            if (a != null)
            {
                db.Assortments.Remove(a);
                db.SaveChanges();
            }
            

            return RedirectToAction("Index");

        }

        public   IActionResult Details(int? id, int? idProDel, int? idProAdd)
        {
            if (id != null && id != 0 && idProDel != null && idProDel != 0)
            {
                var requestDel = db.AssortmentProducts.Where(x => x.AssortmentId == id).Where(x => x.ProductId == idProDel).FirstOrDefault();
                db.AssortmentProducts.Remove(requestDel);
                db.SaveChanges(true);
            }

            if (id != null && id != 0 && idProAdd != null && idProAdd != 0)
            {
                var prodAdd = db.Products.Find(idProAdd);
                var assAdd = db.Assortments.Find(id);

                if (prodAdd != null && assAdd != null)
                {
                    var requestAdd = new AssortmentProduct { AssortmentId = assAdd.Id, Assortments = assAdd, ProductId = prodAdd.Id, Products = prodAdd };
                    db.AssortmentProducts.Add(requestAdd);
                    db.SaveChanges();
                }

               
            }

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var assort = db.Assortments.Find(id);
            
            if (assort == null)
            {
                return NotFound();
            }



            var item =  db.AssortmentProducts.Select(x => new AssortmentProduct {Products = x.Products, Assortments = x.Assortments, AssortmentId = x.AssortmentId, ProductId = x.ProductId }).Where(x => x.AssortmentId == id);
            var test2 = from p in db.Products join ap in db.AssortmentProducts on p.Id equals ap.ProductId into joined from j in joined.DefaultIfEmpty() select new { tets = p, tets2 = j };
            var result = test2.Select(x => x.tets).Except(item.Select(x=>x.Products));

            

            ViewBag.NotIncludProd = result;
            ViewData["AssortName"] = assort.Name;
            ViewData["AssortId"] = assort.Id;

            if (item != null ) return View(item);

            return View();

 
        }
    }
}
