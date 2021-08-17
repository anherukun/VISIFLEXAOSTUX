using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisiflexAOSTUX.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            return Redirect(Url.Action("Settings", "System"));
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}