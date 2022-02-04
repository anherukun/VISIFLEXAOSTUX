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
    public class AgentController : Controller
    {
        // GET: Agent
        public ActionResult Index()
        {
            return Redirect(Url.Action("", ""));
        }

        [HttpPost]
        public ActionResult AddAgent(Agent agent, string controller, string action)
        {
            if (agent != null && controller != null && action != null)
            {
                if (!RepositoryAgent.Exist(agent))
                {
                    Agent a = new Agent()
                    {
                        AgentID = ApplicationManager.GenerateGUID,
                        AgentCode = agent.AgentCode,
                        Name = agent.Name.Trim().ToUpper()
                    };

                    if (RepositoryAgent.Add(a) > 0)
                        return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "Agente agregado correctamente.", Type = ResponseType.SUCCESS }));
                    else
                        return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "No se pudo completar la operacion, verifica los datos.", Type = ResponseType.ERROR }));
                }
                else
                    return Redirect(Url.Action(action, controller, new ResponseMessage() { Message = "Ya fue registrado este agente.", Type = ResponseType.ERROR }));
            }

            return Redirect(Url.Action("", ""));
        }

        public ActionResult DeleteAgent(string id, string c, string a)
        {
            if (id != null && c != null && a != null)
            {
                if (RepositoryAgent.Exist(id))
                    if (RepositoryAgent.Delete(id) > 0)
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