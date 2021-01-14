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

namespace ProjectMusicSound.Controllers
{
    public class MusicsController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Musics
        public ActionResult Index(int ? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));
            var musics = db.Musics.Include(m => m.User);
            return View(musics.Where(n => n.music_active == true && n.music_bin == false && n.music_option == true && n.user_id == user.user_id).ToList());
        }

        public ActionResult ActiveDel(int ? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));
            var musics = db.Musics.Include(m => m.User);
            return View(musics.Where(n => n.music_option == false && n.music_bin == false  && n.user_id == user.user_id).ToList());
        }

        // GET: Musics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // GET: Musics/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name");
            return View();
        }

        // POST: Musics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "music_id,music_name,music_img,music_lyric,music_time,music_view,music_dowload,music_love,user_id,music_linkdow,music_datecreate,music_dateedit,music_active,music_bin,music_option")] Music music, int[] category, HttpPostedFileBase filemp3, HttpPostedFileBase img)
        {

            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            var fileimg = Path.GetFileName(img.FileName);
            //Đưa tên ảnh vào file
            var pa = Path.Combine(Server.MapPath("~/Images"), fileimg);
            img.SaveAs(pa);

            var mp3 = Path.GetFileName(filemp3.FileName);
            var pathmp3 = Path.Combine(Server.MapPath("~/Content/LinkMusic/"), mp3);

            if (filemp3 == null)
            {
                return View();
            }
            else if (System.IO.File.Exists(pathmp3))
            {
                ViewBag.Img = "File had exists";
            }    
            else
            {
                filemp3.SaveAs(pathmp3);
            }
            music.music_linkdow = filemp3.FileName;
            music.music_img = img.FileName;
            music.music_datecreate = DateTime.Now;
            music.music_dateedit = DateTime.Now;
            music.music_bin = false;
            music.music_love = 0;
            music.music_view = 0;
            music.music_dowload = 0;
            music.user_id = user.user_id;
            db.Musics.Add(music);
            db.SaveChanges();
            Music msi = db.Musics.Where(n => n.user_id == user.user_id).OrderByDescending(n => n.music_datecreate).First();

            foreach (var item in category)
            {
                Music_Category music_Category = new Music_Category()
                {
                    music_id = msi.music_id,
                    category_id = item
                };
                db.Music_Category.Add(music_Category);
            }
            db.SaveChanges();

            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", music.user_id);
            return View(music);
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", music.user_id);
            return View(music);
        }

        // POST: Musics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "music_id,music_name,music_img,music_lyric,music_time,music_view,music_dowload,music_love,user_id,music_linkdow,music_datecreate,music_dateedit,music_active,music_bin,music_option")] Music music, HttpPostedFileBase mp3, HttpPostedFileBase img)
        {
            db.Entry(music).State = EntityState.Modified;
            var fileimg = Path.GetFileName(img.FileName);
            //Đưa tên ảnh vào file
            var pa = Path.Combine(Server.MapPath("~/Images"), fileimg);
            img.SaveAs(pa);

            var filemp3 = Path.GetFileName(mp3.FileName);
            var pathmp3 = Path.Combine(Server.MapPath("~/Content/LinkMusic/"), filemp3);

            if (mp3 == null)
            {
                return View();
            }
            if (System.IO.File.Exists(pathmp3))
            {
                ViewBag.Img = "File had exists";
            }
            else
            {
                mp3.SaveAs(pathmp3);
            }
            music.music_img = img.FileName;
            music.music_dateedit = DateTime.Now;
            music.music_linkdow = mp3.FileName;
            music.music_bin = false;
            db.SaveChanges();
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", music.user_id);
            return Redirect("Index");
        }

        // GET: Musics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Musics.Find(id);
            db.Musics.Remove(music);
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

        //Xoá
        public ActionResult Del(int ? id)
        {
            Music music = db.Musics.Find(id);
            music.music_bin = true;
            db.SaveChanges();
            return Redirect("Index");
        }
    }
}
