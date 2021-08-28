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
    public class WorkplaceController : Controller
    {
        // GET: Workplace
        public ActionResult Index()
        {
            return Redirect(Url.Action("", ""));
        }

        [HttpPost] public ActionResult AddWorkplace(Workplace workplace, string controller, string action)
        {
            if (workplace != null && controller != null && action != null)
            {
                if (!RepositoryWorkplace.Exist(workplace))
                {
                    Workplace w = new Workplace()
                    {
                        WorkplaceID = ApplicationManager.GenerateGUID,
                        WorkplaceCode = workplace.WorkplaceCode,
                        Name = workplace.Name.Trim().ToUpper()
                    };

                    if (RepositoryWorkplace.Add(w) > 0)
                        return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "Centro de trabajo agregada correctamente.", Type = ResponseType.SUCCESS }));
                    else
                        return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "No se pudo completar la operacion, verifica los datos.", Type = ResponseType.ERROR }));
                }
                else
                    return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "Ya fue registrada centro de trabajo.", Type = ResponseType.ERROR }));
            }
            return Redirect(Url.Action("", ""));
        }

        public ActionResult DeleteWorkplace(string id, string c, string a)
        {
            if (id != null && c != null && a != null)
            {
                if (RepositoryWorkplace.Exist(id))
                    if (RepositoryWorkplace.Delete(id) > 0)
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