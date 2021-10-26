﻿using System;
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

                    if (account.UserRol.UserLevel != 10)
                        return Redirect(Url.Action("Index", "Home"));
                }
            }
            ViewData["AttentionAreas"] = RepositoryAttentionArea.Get();
            ViewData["Workplaces"] = RepositoryWorkplace.Get();
            ViewData["Agents"] = RepositoryAgent.Get();
            ViewData["RequesterAreas"] = RepositoryRequesterArea.Get();

            ViewData["Response"] = response;

            return View();
        }
    }
}