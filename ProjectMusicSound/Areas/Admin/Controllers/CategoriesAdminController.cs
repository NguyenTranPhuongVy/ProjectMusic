using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Areas.Admin.Controllers
{
    public class CategoriesAdminController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Admin/CategoriesAdmin
        public ActionResult Index()
        {
            return View(db.Categories.Where(n => n.category_bin == false).ToList());
        }

        public ActionResult Del(int ? id)
        {
            Category category = db.Categories.Find(id);
            category.category_bin = !category.category_bin;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCate(int ? id)
        {
            return View(db.Categories.Where(n => n.category_bin == true).ToList());
        }

        //Thay đổi trạng thái
        public JsonResult Active(int ? id)
        {
            Category category = db.Categories.Find(id);
            category.category_active = !category.category_active;
            db.SaveChanges();
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/CategoriesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/CategoriesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoriesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "category_id,category_name,category_active,category_bin,category_note,category_view")] Category category)
        {
            db.Categories.Add(category);
            category.category_view = 0;
            category.category_bin = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/CategoriesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoriesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,category_name,category_active,category_bin,category_note,category_view")] Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/CategoriesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoriesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
