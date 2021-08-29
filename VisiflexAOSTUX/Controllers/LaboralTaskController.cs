using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisiflexAOSTUX.Models;
using VisiflexAOSTUX.Services;

namespace VisiflexAOSTUX.Controllers
{
    public class LaboralTaskController : Controller
    {
        // GET: LaboralTask
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewLaboralTask(ResponseMessage response)
        {
            ViewData["Workplaces"] = RepositoryWorkplace.Get();
            ViewData["Agents"] = RepositoryAgent.Get();
            ViewData["AttentionAreas"] = RepositoryAttentionArea.Get();
            ViewData["Response"] = response;
            return View();
        }

        public ActionResult AddLaboralTask(LaboralTask laboralTask)
        {
            return View();
        }
    }
}