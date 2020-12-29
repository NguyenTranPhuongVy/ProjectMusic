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
    }
}