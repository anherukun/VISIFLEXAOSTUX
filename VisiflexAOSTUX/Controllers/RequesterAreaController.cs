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
    public class RequesterAreaController : Controller
    {
        // GET: RequesterArea
        public ActionResult Index()
        {
            return Redirect(Url.Action("", ""));
        }

        [HttpPost]
        public ActionResult AddRequesterArea(RequesterArea requesterArea, string controller, string action)
        {
            if (requesterArea != null && controller != null && action != null)
            {
                RequesterArea r = new RequesterArea()
                {
                    RequesterAreaID = ApplicationManager.GenerateGUID,
                    Name = requesterArea.Name.Trim().ToUpper()
                };

                if (RepositoryRequesterArea.AddOrUpdate(r) > 0)
                    return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "Area solicitante agregada correctamente.", Type = ResponseType.SUCCESS }));
                else
                    return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "No se pudo completar la operacion, verifica los datos.", Type = ResponseType.ERROR }));
            }

            return Redirect(Url.Action("", ""));
        }

        public ActionResult DeleteRequesterArea(string id, string c, string a)
        {
            if (id != null && c != null && a != null)
            {
                if (RepositoryRequesterArea.Exist(id))
                    if (RepositoryRequesterArea.Delete(id) > 0)
                        return Redirect(Url.Action(a, c, new ResponseMessage()
                        {
                            Message = "Objeto eliminado correctamente.",
                            Type = ResponseType.SUCCESS
                        }));
            }


            return Redirect(Url.Action("", "", new ResponseMessage() { Message = "No se pudo completar la operacion, objeto no existente", Type = ResponseType.ERROR }));
        }
    }
}