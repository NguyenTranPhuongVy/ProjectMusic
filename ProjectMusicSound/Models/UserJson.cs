using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMusicSound.Models
{
    public class UserJson
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_img { get; set; }
        public string user_email { get; set; }
        public string user_pass { get; set; }
        public string user_token { get; set; }
        public Nullable<System.DateTime> user_datecreate { get; set; }
        public Nullable<System.DateTime> user_datelogin { get; set; }
        public Nullable<bool> user_active { get; set; }
        public Nullable<bool> user_option { get; set; }
        public Nullable<bool> user_bin { get; set; }
        public Nullable<int> role_id { get; set; }
        public string user_code { get; set; }

        public virtual Role Role { get; set; }
    }
}