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

            if (Request.Cookies.AllKeys.Contains("idAccount") && Request.Cookies.AllKeys.Contains("idAccount"))
            {
                string session_idAccount = Request.Cookies.Get("idAccount").Value;
                string session_sessionToken = Request.Cookies.Get("sessionToken").Value;
                if (RepositorySession.OnSession(session_sessionToken, session_idAccount))
                {
                    ViewData["session"] = RepositorySession.Get(x => x.IDAccount == session_idAccount && x.SessionToken == session_sessionToken);
                    Account account = RepositoryAccount.Get(x => x.IDAccount == session_idAccount);
                    account.PasswordHash = null;
                    ViewData["account"] = account;

                    return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Sesion actualmente activa", Type = ResponseType.ERROR }));
                }
            }

            return View();
        }

        public ActionResult SignUp(ResponseMessage response)
        {
            ViewData["Response"] = response;

            if (Request.Cookies.AllKeys.Contains("idAccount") && Request.Cookies.AllKeys.Contains("idAccount"))
            {
                string session_idAccount = Request.Cookies.Get("idAccount").Value;
                string session_sessionToken = Request.Cookies.Get("sessionToken").Value;
                if (RepositorySession.OnSession(session_sessionToken, session_idAccount))
                {
                    ViewData["session"] = RepositorySession.Get(x => x.IDAccount == session_idAccount && x.SessionToken == session_sessionToken);
                    Account account = RepositoryAccount.Get(x => x.IDAccount == session_idAccount);
                    account.PasswordHash = null;
                    ViewData["account"] = account;

                    if (account.UserRol.UserLevel == 10 || account.UserRol.UserLevel == 5)
                    {
                        // SI LAS CREDENCIALES SON LAS CORRECTAS
                        return View();
                    }
                    else
                    {
                        return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Cuenta con permisos insuficientes", Type = ResponseType.ERROR }));
                    }
                }
            }

            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Inicia sesion para acceder a este sitio", Type = ResponseType.ERROR }));
        }

        public ActionResult Logout(string sessionToken)
        {
            Session session = RepositorySession.Get(x => x.SessionToken == sessionToken);
            session.SessionToken = null;
            RepositorySession.AddOrUpdate(session);

            Response.Cookies.Remove("sessionToken");
            Response.Cookies.Remove("idAccount");

            return Redirect(Url.Action("Index", "Home"));
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