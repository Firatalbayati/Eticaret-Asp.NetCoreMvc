using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Firat.Eticaret.Entity;
using ImageResizer;

namespace Firat.Eticaret.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        public object FitMode { get; private set; }
        public object ImageBuilder { get; private set; }

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.OrderByDescending(x => x.Id).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Stock,Image,IsHome,IsApproved,ProductCode,Tarih,CategoryId")] Product product, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                string fileName = Path.GetFileName(Image.FileName);
                string path = Path.Combine(Server.MapPath("~/Upload/ProductImage"), fileName);
                string pathModal = Path.Combine(Server.MapPath("~/Upload/ProductImage/modal"), fileName);
                string pathPreview = Path.Combine(Server.MapPath("~/Upload/ProductImage/preview"), fileName);
                string yol = ("/Upload/ProductImage/" + fileName);
                Image.SaveAs(path);


                ResizeSettings modal = new ResizeSettings
                {
                    Width = 500,
                    Height = 500,
                };

                ResizeSettings preview = new ResizeSettings
                {
                    Width = 300,
                    Height = 300,
                };

                product.Image = yol;
            }
            else
            {
                string defPath = "/Upload/default.png";
                product.Image = defPath;
            }

            if (product.Tarih != null)
            {
                product.Tarih = DateTime.Now;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    NewMethod();
                    return RedirectToAction("Index");

                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        private void NewMethod()
        {
            db.SaveChanges();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories.OrderByDescending(x => x.Id).ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,Stock,Image,IsHome,IsApproved,ProductCode,Tarih,CategoryId")] Product product, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                if (System.IO.File.Exists(Server.MapPath(product.Image)))
                {
                    System.IO.File.Delete(Server.MapPath(product.Image));
                }
                string fileName = Path.GetFileName(Image.FileName);
                string path = Path.Combine(Server.MapPath("~/Upload/ProductImage"), fileName);
                string pathModal = Path.Combine(Server.MapPath("~/Upload/ProductImage/modal"), fileName);
                string pathPreview = Path.Combine(Server.MapPath("~/Upload/ProductImage/preview"), fileName);
                string yol = ("/Upload/ProductImage/" + fileName);
                Image.SaveAs(path);


                ResizeSettings modal = new ResizeSettings
                {
                    Width = 500,
                    Height = 500,
                };

                ResizeSettings preview = new ResizeSettings
                {
                    Width = 300,
                    Height = 300,
                };

                product.Image = yol;
            }
            else
            {
                product.Image = IsImageExists(product.Id);
            }
            if (product.Tarih != null)
            {
                product.Tarih = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string IsImageExists(int id)
        {
            var p = db.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (p != null)
            {
                return p.Image.ToString();
            }
            else
            {
                return "/Upload/default.png";
            }
        }
    }
}
