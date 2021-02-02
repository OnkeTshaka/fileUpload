using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Upload.Models;

namespace Upload.Controllers
{
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Videos
        public ActionResult Index(string searching)
        {
            var videoList = db.Videos.OrderByDescending(x => x.Vid).Where(m => m.Vname.Contains(searching) || searching == null).ToList();
            return View(videoList);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vid,Vname,Data")] Video video, HttpPostedFileBase img_upload)
        {
            if (ModelState.IsValid)
            {
                if (img_upload != null && img_upload.ContentLength < 1048576000)
                {
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(img_upload.InputStream))
                    {
                        bytes = br.ReadBytes(img_upload.ContentLength);
                    }
                    video.Vname = Path.GetFileName(img_upload.FileName);
                    video.Data = bytes;
                }
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(video);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
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
