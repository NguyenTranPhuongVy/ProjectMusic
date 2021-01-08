using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMusicSound.Models
{
    public class CategoryJson
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public Nullable<bool> category_active { get; set; }
        public Nullable<bool> category_bin { get; set; }
        public string category_note { get; set; }
        public Nullable<int> category_view { get; set; }
    }
}