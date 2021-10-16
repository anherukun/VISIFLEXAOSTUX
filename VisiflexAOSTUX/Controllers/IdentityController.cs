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
    public class IdentityController : Controller
    {
        // GET: Identity
        public ActionResult Index()
        {
            return Redirect(Url.Action("Login", "Identity"));
        }

        public ActionResult Login(ResponseMessage response)
        {
            ViewData["Response"] = response;

            return View();
        }

        [HttpPost]
        public ActionResult SubmitLogin(string Username, string Password)
        {
            if (RepositoryAccount.IsPasswordCorrect(Username, Password))
            {
                Account account = RepositoryAccount.Get(x => x.Username == Username || x.Email == Username);
                if (RepositorySession.Exist(account.IDAccount))
                {
                    Session session = RepositorySession.Get(x => x.IDAccount == account.IDAccount);
                    session.LastLogin = DateTime.Now;
                    session.SessionToken = Security.SHA256Hash(ApplicationManager.GenerateGUID);
                    RepositorySession.AddOrUpdate(session);

                    HttpContext.Response.Cookies.Add(new HttpCookie("sessionToken", session.SessionToken));
                    HttpContext.Response.Cookies.Add(new HttpCookie("idAccount", account.IDAccount));
                }
                else
                {
                    Session session = new Session()
                    {
                        IDSession = ApplicationManager.GenerateGUID,
                        IDAccount = account.IDAccount,
                        SessionToken = Security.SHA256Hash(ApplicationManager.GenerateGUID),
                        LastLogin = DateTime.Now
                    };
                    RepositorySession.AddOrUpdate(session);

                    HttpContext.Response.Cookies.Add(new HttpCookie("sessionToken", session.SessionToken));
                    HttpContext.Response.Cookies.Add(new HttpCookie("idAccount", account.IDAccount));
                }
                return Redirect(Url.Action("Index", "Home"));
            }
            return Redirect(Url.Action("Login", "Identity"));
        }
    }
}