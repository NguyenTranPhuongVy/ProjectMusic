using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Index()
        {
            var musics = db.Musics.Include(m => m.User);
            return View(musics.ToList());
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
        public ActionResult Create([Bind(Include = "music_id,music_name,music_img,music_lyric,music_time,music_view,music_dowload,music_love,user_id,music_linkdow,music_datecreate,music_dateedit,music_active,music_bin,music_option")] Music music, int[] category)
        {

            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            db.Musics.Add(music);
            music.music_datecreate = DateTime.Now;
            music.music_dateedit = DateTime.Now;
            music.music_bin = false;
            music.music_love = 0;
            music.music_view = 0;
            music.user_id = user.user_id;
            db.SaveChanges();

            foreach(var item in category)
            {
                Music_Category music_Category = new Music_Category()
                {
                    music_id = 1,
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
        public ActionResult Edit([Bind(Include = "music_id,music_name,music_img,music_lyric,music_time,music_view,music_dowload,music_love,user_id,music_linkdow,music_datecreate,music_dateedit,music_active,music_bin,music_option")] Music music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", music.user_id);
            return View(music);
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
    }
}
