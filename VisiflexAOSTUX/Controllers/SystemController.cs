using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
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

        public FileResult DownloadBackup()
        {
            Backup backup = BackupService.CreateBackup();

            if (backup != null)
            {
                byte[] bytes = Application.ApplicationManager.Compress(backup.ToBytes());
                return File(bytes, "application/octet-stream", $"{DateTime.Now.Ticks}_{DateTime.Now.ToShortDateString()}_Visiflex.dat");
            }

            throw new NotImplementedException();
        }

        public ActionResult PurgeTables()
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

                    if (account.UserRol.UserLevel != 5 && account.UserRol.UserLevel != 10)
                        return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Cuenta con permisos insuficientes", Type = ResponseType.ERROR }));
                    else
                    {
                        int deletedEntities = 0;

                        deletedEntities += RepositoryLaboralTask.PurgeAll();
                        deletedEntities += RepositoryHistoryLogDocumentFile.PurgeAll();
                        deletedEntities += RepositoryDocumentFile.PurgeAll();
                        deletedEntities += RepositoryLaboralTaskHistoryLog.PurgeAll();

                        deletedEntities += RepositorySession.PurgeAll();
                        deletedEntities += RepositoryWorkplace.PurgeAll();
                        deletedEntities += RepositoryRequesterArea.PurgeAll();
                        deletedEntities += RepositoryAttentionArea.PurgeAll();


                        return Redirect(Url.Action("Settings", "System", new ResponseMessage() { Message = $"{deletedEntities} Eliminados correctamente", Type = ResponseType.ERROR }));
                    }
                }
            }

            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Inicia sesion para acceder a este sitio", Type = ResponseType.ERROR }));
        }

        public ActionResult Settings(ResponseMessage response)
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

                    if (account.UserRol.UserLevel != 5 && account.UserRol.UserLevel != 10)
                        return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Cuenta con permisos insuficientes", Type = ResponseType.ERROR}));
                    else
                    {
                        // SI LAS CREDENCIALES SON LAS CORRECTAS
                        ViewData["AttentionAreas"] = RepositoryAttentionArea.Get();
                        ViewData["Workplaces"] = RepositoryWorkplace.Get();
                        ViewData["Agents"] = RepositoryAgent.Get();
                        ViewData["RequesterAreas"] = RepositoryRequesterArea.Get();

                        List<Account> accounts = RepositoryAccount.Get();
                        for (int i = 0; i < accounts.Count; i++)
                            accounts[i].PasswordHash = null;
                        ViewData["Accounts"] = accounts;

                        return View();
                    }
                }
            }

            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Inicia sesion para acceder a este sitio", Type = ResponseType.ERROR }));
        }

        [HttpPost] 
        public ActionResult UploadBackup(HttpPostedFileBase filebackup)
        {
            if (filebackup != null)
            {
                DateTime dt1 = DateTime.Now, dt2;
                // Descomprime los bytes del archivo y lo almacena en un arreglo de bytes
                byte[] bytes = Application.ApplicationManager.Decompress(new BinaryReader(filebackup.InputStream).ReadBytes(filebackup.ContentLength));
                // Lee los bytes del archivo .bak y los deserializa a un objeto Backup
                Backup backup = Models.Backup.FromBytes(bytes);
                // Llama al servicio de Backup y empieza la recuperacion de datos
                int entitiesRecovered = BackupService.RecoverData(backup);

                dt2 = DateTime.Now;
                TimeSpan span = dt2.Subtract(dt1);
                return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = $"{entitiesRecovered} datos fueron restaurados correctamente \nT: { span.ToString()}", Type = ResponseType.SUCCESS }));
            }
            return Redirect(Url.Action("Settings", "System", new ResponseMessage() { Message = "No se puede recuperar de un objeto nulo", Type = ResponseType.ERROR }));
        }
    }
}