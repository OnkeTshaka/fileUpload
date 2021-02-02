using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Upload.Models;

namespace Upload.Controllers
{
    public class AudiosController : Controller
    {
        // GET: Audios
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Videos
        public ActionResult Index(string searching)
        {
            var audioList = db.Audios.OrderByDescending(x => x.AudioID).Where(m => m.name.Contains(searching) || searching == null).ToList();
            return View(audioList);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audio audio = db.Audios.Find(id);
            if (audio == null)
            {
                return HttpNotFound();
            }
            return View(audio);
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
        public ActionResult Create([Bind(Include = "AudioID,name,Data")]Audio audio, HttpPostedFileBase img_upload)
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
                    audio.name = Path.GetFileName(img_upload.FileName);
                    audio.Data = bytes;
                }
                db.Audios.Add(audio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(audio);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audio audio = db.Audios.Find(id);
            if (audio == null)
            {
                return HttpNotFound();
            }
            return View(audio);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Audio audio = db.Audios.Find(id);
            db.Audios.Remove(audio);
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