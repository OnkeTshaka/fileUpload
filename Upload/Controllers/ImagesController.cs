using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Upload.Models;

namespace Upload.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Images
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Images
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            Image b = new Image();
            return View(b);
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageID,Data")] Image image, HttpPostedFileBase file)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (file != null)
            {
                image.Data = new byte[file.ContentLength];
                file.InputStream.Read(image.Data, 0, file.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,Data")] Image image, HttpPostedFileBase file)
        {
            byte[] data = null;
            data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            image.Data = data;
            image.Data = file != null ? data : image.Data;
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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
    }
}
