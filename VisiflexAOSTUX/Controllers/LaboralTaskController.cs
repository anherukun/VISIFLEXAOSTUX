using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VisiflexAOSTUX.Application;
using VisiflexAOSTUX.Models;
using VisiflexAOSTUX.Services;

namespace VisiflexAOSTUX.Controllers
{
    public class LaboralTaskController : Controller
    {
        // GET: LaboralTask
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LaboralTask(string laboralTaskID, ResponseMessage response)
        {
            ViewData["Response"] = response;

            ViewData["LaboralTask"] = RepositoryLaboralTask.Get(laboralTaskID);
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

                    return View();
                }
            }
            return View();
        }

        public ActionResult ViewAll(string status, string idrequesterarea, string idattentionarea, string idworkplace, ResponseMessage response)
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

                    if (account.UserRol.UserLevel == 4)
                    {
                        // SI LAS CREDENCIALES SON LAS CORRECTAS
                        if (status != null && idattentionarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status == status && x.IDAttentionArea == idattentionarea && x.IDWorkplace == idworkplace && x.IDAccount == account.IDAccount);
                        }
                        else if (status != null && idrequesterarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status == status && x.IDRequesterArea == idrequesterarea && x.IDWorkplace == idworkplace && x.IDAccount == account.IDAccount);
                        }
                        else if (idattentionarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.IDAttentionArea == idattentionarea && x.IDWorkplace == idworkplace && x.IDAccount == account.IDAccount);
                        }
                        else if (idrequesterarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.IDRequesterArea == idrequesterarea && x.IDWorkplace == idworkplace && x.IDAccount == account.IDAccount);
                        }
                        else
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status != "NO PROCEDE" && x.Status != "ATENDIDO" && x.IDAccount == account.IDAccount);
                        }
                        return View();
                    }
                    else if (account.UserRol.UserLevel == 5 || account.UserRol.UserLevel == 10)
                    {
                        if (status != null && idattentionarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status == status && x.IDAttentionArea == idattentionarea && x.IDWorkplace == idworkplace);
                            ViewData["queryStatus"] = status;
                            ViewData["queryIdattentionArea"] = idattentionarea;
                            ViewData["queryIdworkplace"] = idworkplace;
                        }
                        else if (status != null && idrequesterarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status == status && x.IDRequesterArea == idrequesterarea && x.IDWorkplace == idworkplace);
                            ViewData["queryStatus"] = status;
                            ViewData["queryIdrequesterArea"] = idrequesterarea;
                            ViewData["queryIdworkplace"] = idworkplace;
                        }
                        else if (idattentionarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.IDAttentionArea == idattentionarea && x.IDWorkplace == idworkplace);
                            ViewData["queryIdattentionArea"] = idattentionarea;
                            ViewData["queryIdworkplace"] = idworkplace;
                        }
                        else if (idrequesterarea != null && idworkplace != null)
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.IDRequesterArea == idrequesterarea && x.IDWorkplace == idworkplace);
                            ViewData["queryIdrequesterArea"] = idrequesterarea;
                            ViewData["queryIdworkplace"] = idworkplace;
                        }
                        else
                        {
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status != "NO PROCEDE" && x.Status != "ATENDIDO");
                        }

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
        public ActionResult ViewByRequesterArea(ResponseMessage response)
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

                    switch (account.UserRol.UserLevel)
                    {
                        case 4:
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByRequesterArea(account.IDAccount);
                            break;

                        case 5:
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByRequesterArea();
                            break;

                        case 10:
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByRequesterArea();
                            break;

                        default:
                            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Cuenta con permisos insuficientes", Type = ResponseType.ERROR }));
                    }

                    return View();
                }
            }
            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Inicia sesion para acceder a este sitio.", Type = ResponseType.ERROR }));
        }
        public ActionResult ViewByAttentionArea(ResponseMessage response)
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

                    switch (account.UserRol.UserLevel)
                    {
                        case 4:
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByAttentionArea(account.IDAccount);
                            break;

                        case 5:
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByAttentionArea();
                            break;

                        case 10:
                            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByAttentionArea();
                            break;

                        default:
                            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Cuenta con permisos insuficientes", Type = ResponseType.ERROR }));
                    }

                    return View();
                }
            }
            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "Inicia sesion para acceder a este sitio.", Type = ResponseType.ERROR }));
        }

        public ActionResult NewLaboralTask(ResponseMessage response)
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

                    if (account.UserRol.UserLevel == 1 || account.UserRol.UserLevel == 5 || account.UserRol.UserLevel == 10)
                    {
                        // SI LAS CREDENCIALES SON LAS CORRECTAS

                        ViewData["Workplaces"] = RepositoryWorkplace.Get();
                        ViewData["Accounts"] = RepositoryAccount.Get();
                        ViewData["AttentionAreas"] = RepositoryAttentionArea.Get();
                        ViewData["RequesterAreas"] = RepositoryRequesterArea.Get();
                        ViewData["Response"] = response;

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

        [HttpPost]
        public ActionResult AddLaboralTask(LaboralTask laboralTask, HttpPostedFileBase File)
        {
            if (laboralTask != null)
            {
                if (RepositoryLaboralTask.Exist(x => x.DocumentID == laboralTask.DocumentID))
                    return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "La referencia del documento ya fue registrada con otro asunto distinto.", Type = ResponseType.ERROR}));

                long submittionticks = DateTime.Now.Ticks;
                LaboralTask l = new LaboralTask()
                {
                    IDLaboralTask = ApplicationManager.GenerateGUID,
                    IDAttentionArea = laboralTask.IDAttentionArea,
                    IDRequesterArea = laboralTask.IDRequesterArea,
                    IDWorkplace = laboralTask.IDWorkplace,
                    IDAccount = laboralTask.IDAccount,
                    DocumentID = laboralTask.DocumentID.Trim().ToUpper(),
                    AttentionInstructions = laboralTask.AttentionInstructions.Trim().ToUpper(),
                    DocumentDate = laboralTask.DocumentDate,
                    ReceptionDate = laboralTask.ReceptionDate,
                    CommitmentDate = laboralTask.CommitmentDate,
                    LongDescription = laboralTask.LongDescription.Trim().ToUpper(),
                    Observations = laboralTask.Observations.Trim().ToUpper(),
                    Subjet = laboralTask.Subjet.Trim().ToUpper(),
                    Status = "INICIADO",
                    UploadTicks = submittionticks,
                    Progress = 0,
                    Count = RepositoryLaboralTask.GetNextCount()
                };

                if (File != null)
                {
                    DocumentFile d = new DocumentFile()
                    {
                        DocumentFileID = ApplicationManager.GenerateGUID,
                        LaboralTaskID = l.IDLaboralTask,
                        Size = File.ContentLength,
                        Mime = File.ContentType,
                        Name = File.FileName,
                        UploadTicks = submittionticks,
                        Data = ApplicationManager.Compress(new BinaryReader(File.InputStream).ReadBytes(File.ContentLength))
                    };

                    if (RepositoryDocumentFile.AddOrUpdate(d) > 0)
                    {
                        l.IDDocumentFile = d.DocumentFileID;

                        if (RepositoryLaboralTask.AddOrUpdate(l) > 0)
                        {
                            LaboralTaskHistoryLog log = new LaboralTaskHistoryLog()
                            {
                                IDLaboralTaskHistoryLog = ApplicationManager.GenerateGUID,
                                IDLaboralTask = l.IDLaboralTask,
                                Description = "DOCUMENTACION RECIBIDA, EN ESPERA DE SU ATENCION POR SU ESPECIALISTA",
                                Date = DateTime.Now,
                                UploadTicks = submittionticks
                            };
                            if (RepositoryLaboralTaskHistoryLog.AddOrUpdate(log) > 0)
                                return Redirect(Url.Action("LaboralTask", "LaboralTask", new { laboralTaskID = l.IDLaboralTask, Message = "Asunto laboral registrado correctamente: Actualizacion de asunto registrada", Type = ResponseType.SUCCESS }));
                            //return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente: Actualizacion de asunto registrada", Type = ResponseType.SUCCESS }));
                            else
                                return Redirect(Url.Action("LaboralTask", "LaboralTask", new { laboralTaskID = l.IDLaboralTask, Message = "Asunto laboral registrado correctamente", Type = ResponseType.SUCCESS }));
                            //return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente", Type = ResponseType.SUCCESS }));
                        }
                        else
                        {
                            if (RepositoryDocumentFile.Delete(d.DocumentFileID) > 1)
                                return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "No se pudo procesar solicitud, Archivo eliminado", Type = ResponseType.ERROR }));
                            else
                                return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "No se pudo procesar solicitud, Archivo no eliminado", Type = ResponseType.ERROR }));
                        }
                    }
                }
                else if (RepositoryLaboralTask.AddOrUpdate(l) > 0)
                {
                    LaboralTaskHistoryLog log = new LaboralTaskHistoryLog()
                    {
                        IDLaboralTaskHistoryLog = ApplicationManager.GenerateGUID,
                        IDLaboralTask = l.IDLaboralTask,
                        Description = "DOCUMENTACION RECIBIDA, EN ESPERA DE SU ATENCION POR SU ESPECIALISTA",
                        Date = DateTime.Now,
                        UploadTicks = submittionticks
                    };

                    if (RepositoryLaboralTaskHistoryLog.AddOrUpdate(log) > 0)
                        return Redirect(Url.Action("LaboralTask", "LaboralTask", new { laboralTaskID = l.IDLaboralTask, Message = "Asunto laboral registrado correctamente: Actualizacion de asunto registrada", Type = ResponseType.SUCCESS }));
                    //return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente: Actualizacion de asunto registrada", Type = ResponseType.SUCCESS }));
                    else
                        return Redirect(Url.Action("LaboralTask", "LaboralTask", new { laboralTaskID = l.IDLaboralTask, Message = "Asunto laboral registrado correctamente", Type = ResponseType.SUCCESS }));
                    //return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente", Type = ResponseType.SUCCESS }));
                }
            }
            return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "No se pudo procesar solicitud, falta informacion o archivo digital", Type = ResponseType.ERROR }));
        }

        [HttpPost]
        public ActionResult DirectSearch(string query)
        {
            int c = -1;
            Int32.TryParse(query, out c);
            if (c != -1)
                if (RepositoryLaboralTask.Exist(x => x.DocumentID == query || x.Count == c || x.IDLaboralTask == query))
                {
                    return Redirect(Url.Action("LaboralTask", "LaboralTask", new { laboralTaskID = RepositoryLaboralTask.GetByPredicate(x => x.DocumentID == query || x.Count == c || x.IDLaboralTask == query).IDLaboralTask }));
                }
            else if (RepositoryLaboralTask.Exist(x => x.DocumentID == query || x.IDLaboralTask == query))
                {
                    return Redirect(Url.Action("LaboralTask", "LaboralTask", new { RepositoryLaboralTask.GetByPredicate(x => x.DocumentID == query || x.IDLaboralTask == query).IDLaboralTask }));
                }

            return Redirect(Url.Action("Index", "Home", new ResponseMessage() { Message = "No se contro ningun resultado con el criterio de busqueda", Type = ResponseType.ERROR }));
        }

        public FileResult DownloadReport(string qstatus, string qidrequesterarea, string qidworkplace, string qidattentionarea)
        {
            string stringdata = "";
            if (qstatus != null && qidworkplace != null && qidattentionarea != null)
            {
                stringdata = ExportService.GetCSVStringData(ExportService.GetLaboralTaskMatrix(this, x => x.Status == qstatus && x.IDWorkplace == qidworkplace && x.IDAttentionArea == qidattentionarea), ',');
            }
            else if (qstatus != null && qidworkplace != null && qidrequesterarea != null)
            {
                stringdata = ExportService.GetCSVStringData(ExportService.GetLaboralTaskMatrix(this, x => x.Status == qstatus && x.IDWorkplace == qidworkplace && x.IDRequesterArea == qidrequesterarea), ',');
            }
            else if (qidworkplace != null && qidattentionarea != null)
            {
                stringdata = ExportService.GetCSVStringData(ExportService.GetLaboralTaskMatrix(this, x => x.IDWorkplace == qidworkplace && x.IDAttentionArea == qidattentionarea), ',');
            }
            else if (qidworkplace != null && qidrequesterarea != null)
            {
                stringdata = ExportService.GetCSVStringData(ExportService.GetLaboralTaskMatrix(this, x => x.IDWorkplace == qidworkplace && x.IDRequesterArea == qidrequesterarea), ',');
            }
            else 
                stringdata = ExportService.GetCSVStringData(ExportService.GetLaboralTaskMatrix(this), ',');

            return File(Encoding.UTF8.GetBytes(stringdata), "txt/csv", $"Visiflex_Reporte_{DateTime.Now.Ticks}.csv");

            //return Redirect(Url.Action("ViewAll", "LaboralTask"));
        }
    }
}