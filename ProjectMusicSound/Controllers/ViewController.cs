using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMusicSound.Controllers
{
    public class ViewController : Controller
    {
        // GET: View
        public PartialViewResult Validation()
        {
            return PartialView();
        }

        public PartialViewResult Menu()
        {
            return PartialView();
        }

        public PartialViewResult MenuUser()
        {
            return PartialView();
        }
    }
}