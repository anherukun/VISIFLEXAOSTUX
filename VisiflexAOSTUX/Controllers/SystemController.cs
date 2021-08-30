using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisiflexAOSTUX.Models;
using VisiflexAOSTUX.Services;

namespace VisiflexAOSTUX.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            return Redirect(Url.Action("Settings", "System"));
        }

        public ActionResult Settings(ResponseMessage response)
        {
            ViewData["AttentionAreas"] = RepositoryAttentionArea.Get();
            ViewData["Workplaces"] = RepositoryWorkplace.Get();
            ViewData["Agents"] = RepositoryAgent.Get();
            ViewData["RequesterAreas"] = RepositoryRequesterArea.Get();

            ViewData["Response"] = response;

            return View();
        }
    }
}