using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Controllers
{
    public class JsonUsersController : Controller
    {
        MusicDataEntities db = new MusicDataEntities();
        // GET: JsonUsers
        public JsonResult MusicList(int ? id)
        {
            List<Music> music = db.Musics.Where(n => n.music_active == true && n.music_bin == false && n.user_id == id).ToList();
            List<MusicJson> listms = music.Select(n => new MusicJson 
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
                user_id = n.user_id
            }).ToList();
            return Json(listms ,JsonRequestBehavior.AllowGet);
        }
    }
}