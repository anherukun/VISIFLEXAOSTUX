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
            if (attentionArea != null && controller != null && action != null)
            {
                if (!RepositoryAttentionArea.Exist(attentionArea))
                {
                    AttentionArea a = new AttentionArea()
                    {
                        AttentionAreaID = ApplicationManager.GenerateGUID,
                        AreaCode = attentionArea.AreaCode,
                        Name = attentionArea.Name.ToUpper()
                    };

                    if (RepositoryAttentionArea.Add(a) > 0)
                        return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "Area de atencion agregada correctamente.", Type = ResponseType.SUCCESS }));
                    else
                        return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "No se pudo completar la operacion, verifica los datos.", Type = ResponseType.ERROR }));
                }
                else
                    return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "Ya fue registrada esta area de atencion.", Type = ResponseType.ERROR }));
            }

            return Redirect(Url.Action("", ""));
        }

        public ActionResult DeleteAttentionArea(string id, string c, string a)
        {
            if (id != null && c != null && a != null)
            {
                if (RepositoryAttentionArea.Exist(id))
                    if (RepositoryAttentionArea.Delete(id) > 0)
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