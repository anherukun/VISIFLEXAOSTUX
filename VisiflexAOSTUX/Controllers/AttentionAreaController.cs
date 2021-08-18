using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisiflexAOSTUX.Application;
using VisiflexAOSTUX.Models;
using VisiflexAOSTUX.Services;

namespace VisiflexAOSTUX.Controllers
{
    public class AttentionAreaController : Controller
    {
        // GET: AttentionArea
        public ActionResult Index()
        {
            return Redirect(Url.Action("", ""));
        }

        [HttpPost] public ActionResult AddAttentionArea(AttentionArea attentionArea, string controller, string action)
        {
            AttentionArea a = new AttentionArea()
            {
                AttentionAreaID = ApplicationManager.GenerateGUID,
                AreaCode = attentionArea.AreaCode,
                Name = attentionArea.Name
            };

            RepositoryAttentionArea.Add(a);

            return Redirect(Url.Action(action, controller));
        }
    }
}