using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Areas.Admin.Controllers
{
    public class JSonAdminController : Controller
    {
        MusicDataEntities db = new MusicDataEntities();
        // GET: Admin/JSonAdmin
        public JsonResult UserList()
        {
            List<User> user = db.Users.Where(n => n.user_bin == false && n.role_id == 1).ToList();
            List<UserJson> list = user.Select(n => new UserJson
            {
                user_code = n.user_code,
                user_active = n.user_active,
                user_datecreate = n.user_datecreate.Value.ToShortDateString(),
                user_datelogin = n.user_datelogin.Value.ToShortDateString(),
                user_email = n.user_email,
                user_name = n.user_name,
                user_bin = n.user_bin,
                user_img = n.user_img,
                user_id = n.user_id,
                user_option = n.user_option,
                user_pass = n.user_pass,
                user_token = n.user_token,
                role_id = n.role_id
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserDel()
        {
            List<User> user = db.Users.Where(n => n.user_bin == true && n.role_id == 1).ToList();
            List<UserJson> list = user.Select(n => new UserJson
            {
                user_code = n.user_code,
                user_active = n.user_active,
                user_datecreate = n.user_datecreate.Value.ToShortDateString(),
                user_datelogin = n.user_datelogin.Value.ToShortDateString(),
                user_email = n.user_email,
                user_name = n.user_name,
                user_bin = n.user_bin,
                user_img = n.user_img,
                user_id = n.user_id,
                user_option = n.user_option,
                user_pass = n.user_pass,
                user_token = n.user_token,
                role_id = n.role_id
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CategoryList()
        {
            List<Category> categories = db.Categories.Where(n => n.category_bin == false).ToList();
            List<CategoryJson> listcate = categories.Select(n => new CategoryJson 
            {
                category_active = n.category_active,
                category_bin = n.category_bin,
                category_id = n.category_id,
                category_name = n.category_name,
                category_note = n.category_note,
                category_view = n.category_view
            }).ToList();
            return Json(listcate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CategoryDel()
        {
            List<Category> categories = db.Categories.Where(n => n.category_bin == true).ToList();
            List<CategoryJson> listcate = categories.Select(n => new CategoryJson
            {
                category_active = n.category_active,
                category_bin = n.category_bin,
                category_id = n.category_id,
                category_name = n.category_name,
                category_note = n.category_note,
                category_view = n.category_view
            }).ToList();
            return Json(listcate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SingerList()
        {
            List<Singer> singerlist = db.Singers.Where(n => n.singer_bin == false).ToList();
            List<SingerJson> list = singerlist.Select(n => new SingerJson 
            {
                singer_id = n.singer_id,
                singer_active = n.singer_active,
                singer_bin = n.singer_bin,
                singer_name = n.singer_name,
                singer_note = n.singer_note,
                singer_img = n.singer_img
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SingerDel()
        {
            List<Singer> singerlist = db.Singers.Where(n => n.singer_bin == true).ToList();
            List<SingerJson> list = singerlist.Select(n => new SingerJson
            {
                singer_id = n.singer_id,
                singer_active = n.singer_active,
                singer_bin = n.singer_bin,
                singer_name = n.singer_name,
                singer_note = n.singer_note,
                singer_img = n.singer_img
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MusicAd()
        {
            List<Music> musics = db.Musics.Where(n => n.music_bin == false && n.User.role_id == 2).ToList();
            List<MusicJson> list = musics.Select(n => new MusicJson
            {
                music_id = n.music_id,
                music_active = n.music_active,
                music_bin = n.music_bin,
                music_datecreate = n.music_datecreate.ToString(),
                music_dateedit = n.music_dateedit.ToString(),
                music_dowload = n.music_dowload,
                music_img = n.music_img,
                music_linkdow = n.music_linkdow,
                music_love = n.music_love,
                music_lyric = n.music_lyric,
                music_name = n.music_name,
                music_option = n.music_option,
                music_time = n.music_time,
                music_view = n.music_view,
                user_id = n.user_id,
                listarr = ListMs(n.music_id),
                listarral = ListAl(n.music_id),
                listarrsing = ListSing(n.music_id)
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MusicUsers()
        {
            List<Music> musics = db.Musics.Where(n => n.music_bin == false && n.User.role_id == 1).ToList();
            List<MusicJson> list = musics.Select(n => new MusicJson
            {
                music_id = n.music_id,
                music_active = n.music_active,
                music_bin = n.music_bin,
                music_datecreate = n.music_datecreate.ToString(),
                music_dateedit = n.music_dateedit.ToString(),
                music_dowload = n.music_dowload,
                music_img = n.music_img,
                music_linkdow = n.music_linkdow,
                music_love = n.music_love,
                music_lyric = n.music_lyric,
                music_name = n.music_name,
                music_option = n.music_option,
                music_time = n.music_time,
                music_view = n.music_view,
                user_id = n.user_id,
                nameCreate = n.User.user_name,
                listarr = ListMs(n.music_id),
                listarral = ListAl(n.music_id),
                listarrsing = ListSing(n.music_id)
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public string[] ListMs(int ? id)
        {
            List<string> myList = new List<string>();
            List<Music_Category> music_Category = db.Music_Category.Where(n => n.music_id == id).ToList();
            foreach (var item in music_Category)
            {
                myList.Add(item.Category.category_name);
            }   
            string[] arr = myList.ToArray();
            return arr;
        }

        public string[] ListAl(int? id)
        {
            List<string> myList = new List<string>();
            List<Music_Ablum> music_Ablums = db.Music_Ablum.Where(n => n.music_id == id).ToList();
            foreach (var item in music_Ablums)
            {
                myList.Add(item.Album.album_name);
            }
            string[] arr = myList.ToArray();
            return arr;
        }

        public string[] ListSing(int? id)
        {
            List<string> myList = new List<string>();
            List<Music_Singer> music_Singers = db.Music_Singer.Where(n => n.music_id == id).ToList();
            foreach (var item in music_Singers)
            {
                myList.Add(item.Singer.singer_name);
            }
            string[] arr = myList.ToArray();
            return arr;
        }


        public JsonResult MusicDel()
        {
            List<Music> musics = db.Musics.Where(n => n.music_bin == true).ToList();
            List<MusicJson> list = musics.Select(n => new MusicJson
            {
                music_id = n.music_id,
                music_active = n.music_active,
                music_bin = n.music_bin,
                music_datecreate = n.music_datecreate.ToString(),
                music_dateedit = n.music_dateedit.ToString(),
                music_dowload = n.music_dowload,
                music_img = n.music_img,
                music_linkdow = n.music_linkdow,
                music_love = n.music_love,
                music_lyric = n.music_lyric,
                music_name = n.music_name,
                music_option = n.music_option,
                music_time = n.music_time,
                music_view = n.music_view,
                user_id = n.user_id,
                nameCreate = n.User.user_name
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}