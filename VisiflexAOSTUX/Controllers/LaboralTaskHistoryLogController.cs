using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisiflexAOSTUX.Application;
using VisiflexAOSTUX.Models;
using VisiflexAOSTUX.Services;

namespace VisiflexAOSTUX.Controllers
{
    public class LaboralTaskHistoryLogController : Controller
    {
        // GET: LaboralTaskHistoryLog
        public ActionResult Index()
        {
            return Redirect(Url.Action("", ""));
        }

        public ActionResult HistoryLog(string laboralTaskID, ResponseMessage response)
        {
            string laboraltaskstatus = RepositoryLaboralTask.Get(laboralTaskID).Status;
            if (laboraltaskstatus != "ATENDIDO" && laboraltaskstatus != "NO PROCEDE")
            {
                ViewData["laboralTask"] = RepositoryLaboralTask.Get(laboralTaskID);
                ViewData["laboralTaskHystoryLog"] = RepositoryLaboralTaskHistoryLog.Get(laboralTaskID);
                ViewData["Response"] = response;

                return View();
            }

            return Redirect(Url.Action("ViewAll", "LaboralTask", new ResponseMessage() { Message = "Este asunto ya fue concluido", Type = ResponseType.ERROR }));
        }

        [HttpPost] public ActionResult AddHistoryLog(LaboralTaskHistoryLog historyLog, HttpPostedFileBase File, string Status)
        {
            if (historyLog != null && Status != null)
            {
                long ticks = DateTime.Now.Ticks;
                LaboralTaskHistoryLog log = new LaboralTaskHistoryLog()
                {
                    IDLaboralTaskHistoryLog = ApplicationManager.GenerateGUID,
                    IDLaboralTask = historyLog.IDLaboralTask,
                    Date = historyLog.Date,
                    Description = historyLog.Description.Trim().ToUpper(),
                    UploadTicks = ticks
                };

                LaboralTaskHistoryLog statuslog = new LaboralTaskHistoryLog()
                {
                    IDLaboralTaskHistoryLog = ApplicationManager.GenerateGUID,
                    IDLaboralTask = historyLog.IDLaboralTask,
                    Date = historyLog.Date,
                    Description = $"[SYSTEM-LOG] EL ESTADO DEL ASUNTO CAMBIO A: {Status}",
                    UploadTicks = DateTime.Now.Ticks
                };

                if (File != null)
                {
                    HistoryLogDocumentFile logfile = new HistoryLogDocumentFile()
                    {
                        IDHistoryLogDocumentFile = ApplicationManager.GenerateGUID,
                        LaboralTaskHistoryLogID = log.IDLaboralTaskHistoryLog,
                        Size = File.ContentLength,
                        Mime = File.ContentType,
                        Name = File.FileName,
                        UploadTicks = ticks,
                        Data = ApplicationManager.Compress(new BinaryReader(File.InputStream).ReadBytes(File.ContentLength))
                    };

                    if (RepositoryHistoryLogDocumentFile.Add(logfile) > 0)
                    {
                        log.IDHistoryLogDocumentFile = logfile.IDHistoryLogDocumentFile;

                        if (RepositoryLaboralTaskHistoryLog.Add(log) > 0)
                        {
                            LaboralTask laboralTask = RepositoryLaboralTask.Get(log.IDLaboralTask);
                            if (laboralTask.Status != Status)
                                RepositoryLaboralTaskHistoryLog.Add(statuslog);

                            laboralTask.Status = Status;

                            RepositoryLaboralTask.Add(laboralTask);

                            // ResponseMessage r = new ResponseMessage() { Message = "Actualizacion guardada correctamente", Type = ResponseType.SUCCESS };
                            return Redirect(Url.Action("HistoryLog", "LaboralTaskHistoryLog", new { laboralTaskID = log.IDLaboralTask, 
                                response = new ResponseMessage() { Message = "Actualizacion guardada correctamente", Type = ResponseType.SUCCESS } }));
                        } 
                        else
                        {
                            RepositoryHistoryLogDocumentFile.Delete(logfile.IDHistoryLogDocumentFile);

                            // ResponseMessage r = new ResponseMessage() { Message = "Ocurrio un error al procesar la solicitud: Archivo eliminado", Type = ResponseType.ERROR };
                            return Redirect(Url.Action("HistoryLog", "LaboralTaskHistoryLog", new { laboralTaskID = log.IDLaboralTask,
                                response = new ResponseMessage() { Message = "Ocurrio un error al procesar la solicitud: Archivo eliminado", Type = ResponseType.ERROR } }));
                        }
                    }
                    else
                    {
                        // ResponseMessage r = new ResponseMessage() { Message = "Ocurrio un error al cargar el archivo", Type = ResponseType.ERROR };
                        return Redirect(Url.Action("HistoryLog", "LaboralTaskHistoryLog", new { laboralTaskID = log.IDLaboralTask,
                            response = new ResponseMessage() { Message = "Ocurrio un error al cargar el archivo", Type = ResponseType.ERROR } }));
                    }
                }
                else
                {
                    if (RepositoryLaboralTaskHistoryLog.Add(log) > 0)
                    {
                        LaboralTask laboralTask = RepositoryLaboralTask.Get(log.IDLaboralTask);
                        if (laboralTask.Status != Status)
                            RepositoryLaboralTaskHistoryLog.Add(statuslog);

                        laboralTask.Status = Status;

                        RepositoryLaboralTask.Add(laboralTask);

                        // ResponseMessage r = new ResponseMessage() { Message = "Actualizacion guardada correctamente", Type = ResponseType.SUCCESS };
                        return Redirect(Url.Action("HistoryLog", "LaboralTaskHistoryLog", new { laboralTaskID = log.IDLaboralTask,
                            response = new ResponseMessage() { Message = "Actualizacion guardada correctamente", Type = ResponseType.SUCCESS } }));
                    }
                    else
                    {
                        // ResponseMessage r = new ResponseMessage() { Message = "Ocurrio un error al procesar a solicitud", Type = ResponseType.ERROR };
                        return Redirect(Url.Action("HistoryLog", "LaboralTaskHistoryLog", new { laboralTaskID = log.IDLaboralTask,
                            response = new ResponseMessage() { Message = "Ocurrio un error al procesar a solicitud", Type = ResponseType.ERROR } }));
                    }
                }
            }

            return Redirect(Url.Action("ViewAll", "LaboralTask", new ResponseMessage() { Message = "Ocurrio un error al procesar a solicitud", Type = ResponseType.ERROR }));
        }
    }
}