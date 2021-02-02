using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMusicSound.Models
{
    public class AlbumJson
    {
        public int album_id { get; set; }
        public string album_name { get; set; }
        public string album_datecreate { get; set; }
        public string album_dateedit { get; set; }
        public Nullable<int> album_view { get; set; }
        public Nullable<int> album_love { get; set; }
        public Nullable<bool> album_active { get; set; }
        public Nullable<bool> album_bin { get; set; }
        public string album_note { get; set; }
        public string album_img { get; set; }
        public Nullable<int> user_id { get; set; }
    }
}