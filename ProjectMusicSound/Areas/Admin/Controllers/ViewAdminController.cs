using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMusicSound.Areas.Admin.Controllers
{
    public class ViewAdminController : Controller
    {
        // GET: Admin/ViewAdmin
        public PartialViewResult MenuAdmin()
        {
            return PartialView();
        }

        public PartialViewResult Validation()
        {
            return PartialView();
        }
    }
}