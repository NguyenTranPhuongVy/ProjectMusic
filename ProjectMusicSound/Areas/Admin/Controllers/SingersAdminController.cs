using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Areas.Admin.Controllers
{
    public class SingersAdminController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Admin/SingersAdmin
        public ActionResult Index()
        {
            return View(db.Singers.Where(n => n.singer_bin == false).ToList());
        }

        public ActionResult Del(int ? id)
        {
            Singer singer = db.Singers.Find(id);
            singer.singer_bin = ! singer.singer_bin;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DelSinger()
        {
            return View(db.Singers.Where(n => n.singer_bin == true).ToList());
        }

        // GET: Admin/SingersAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singers.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // GET: Admin/SingersAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SingersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "singer_id,singer_name,singer_active,singer_bin,singer_note")] Singer singer, HttpPostedFileBase img)
        {
            Random random = new Random();
            ViewBag.Random = random.Next(0, 1000);
            var fileimg = "";
            var pa = "";
            db.Singers.Add(singer);
            if (img == null)
            {
                ViewBag.Checkimg = "Không Có Hình Ảnh";
                singer.singer_img = "userImg.png";
            }
            else
            {
                fileimg = Path.GetFileName(img.FileName);
                //Đưa tên ảnh vào file
                pa = Path.Combine(Server.MapPath("~/Images/"), ViewBag.Random + fileimg);
                img.SaveAs(pa);
                singer.singer_img = ViewBag.Random + img.FileName;
            }
            singer.singer_active = true;
            singer.singer_bin = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/SingersAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singers.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // POST: Admin/SingersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "singer_id,singer_name,singer_active,singer_bin,singer_note")] Singer singer, HttpPostedFileBase img)
        {
            db.Entry(singer).State = EntityState.Modified;
            Random random = new Random();
            ViewBag.Random = random.Next(0, 1000);
            var fileimg = "";
            var pa = "";
            db.Singers.Add(singer);
            if (img == null)
            {
                ViewBag.Checkimg = "Không Có Hình Ảnh";
                singer.singer_img = singer.singer_img;
            }
            else
            {
                fileimg = Path.GetFileName(img.FileName);
                //Đưa tên ảnh vào file
                pa = Path.Combine(Server.MapPath("~/Images/"), ViewBag.Random + fileimg);
                img.SaveAs(pa);
                singer.singer_img = ViewBag.Random + img.FileName;
            }
            db.SaveChanges();
            return View(singer);
        }

        // GET: Admin/SingersAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singers.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // POST: Admin/SingersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Singer singer = db.Singers.Find(id);
            db.Singers.Remove(singer);
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

        public JsonResult Active(int ? id)
        {
            Singer singer = db.Singers.Find(id);
            singer.singer_active = !singer.singer_active;
            db.SaveChanges();
            return Json(singer, JsonRequestBehavior.AllowGet);
        }
    }
}
