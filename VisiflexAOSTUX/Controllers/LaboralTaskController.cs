using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisiflexAOSTUX.Controllers
{
    public class LaboralTaskController : Controller
    {
        // GET: LaboralTask
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewLaboralTask()
        {
            return View();
        }
    }
}