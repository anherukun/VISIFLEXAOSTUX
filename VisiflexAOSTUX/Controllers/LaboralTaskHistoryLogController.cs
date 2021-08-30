using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisiflexAOSTUX.Models;
using VisiflexAOSTUX.Services;

namespace VisiflexAOSTUX.Controllers
{
    public class LaboralTaskHistoryLogController : Controller
    {
        // GET: LaboralTaskHistoryLog
        public ActionResult Index()
        {
            return Redirect(Url.Action("", ""));
        }

        public ActionResult HistoryLog(string laboralTaskID, ResponseMessage response)
        {
            ViewData["laboralTask"] = RepositoryLaboralTask.Get(laboralTaskID);
            ViewData["laboralTaskHystoryLog"] = RepositoryLaboralTaskHistoryLog.Get(laboralTaskID);
            ViewData["Response"] = response;

            return View();
        }
    }
}