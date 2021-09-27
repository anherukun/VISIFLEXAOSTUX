﻿using System;
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
    public class LaboralTaskController : Controller
    {
        // GET: LaboralTask
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LaboralTask(string laboralTaskID)
        {
            return View();
        }

        public ActionResult ViewAll(string status, string idrequesterarea, string idattentionarea, string idworkplace, ResponseMessage response)
        {
            if (status != null && idattentionarea != null && idworkplace != null)
            {
                ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status == status && x.IDAttentionArea == idattentionarea && x.IDWorkplace == idworkplace);
            }
            else if (status != null && idrequesterarea != null && idworkplace != null)
            {
                ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.Status == status && x.IDRequesterArea == idrequesterarea && x.IDWorkplace == idworkplace);
            }
            else if (idattentionarea != null && idworkplace != null)
            {
                ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.IDAttentionArea == idattentionarea && x.IDWorkplace == idworkplace);
            }
            else if (idrequesterarea != null && idworkplace != null)
            {
                ViewData["LaboralTasks"] = RepositoryLaboralTask.Get(x => x.IDRequesterArea == idrequesterarea && x.IDWorkplace == idworkplace);
            }
            else
            {
                ViewData["LaboralTasks"] = RepositoryLaboralTask.Get();
            }
            ViewData["Response"] = response;
            return View();
        }
        public ActionResult ViewByRequesterArea(ResponseMessage response)
        {
            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByRequesterArea();

            ViewData["Response"] = response;
            return View();
        }
        public ActionResult ViewByAttentionArea(ResponseMessage response)
        {
            ViewData["LaboralTasks"] = RepositoryLaboralTask.GetByAttentionArea();

            ViewData["Response"] = response;
            return View();
        }

        public ActionResult NewLaboralTask(ResponseMessage response)
        {
            ViewData["Workplaces"] = RepositoryWorkplace.Get();
            ViewData["Agents"] = RepositoryAgent.Get();
            ViewData["AttentionAreas"] = RepositoryAttentionArea.Get();
            ViewData["RequesterAreas"] = RepositoryRequesterArea.Get();
            ViewData["Response"] = response;
            return View();
        }

        public ActionResult AddLaboralTask(LaboralTask laboralTask, HttpPostedFileBase File)
        {
            if (laboralTask != null)
            {
                long submittionticks = DateTime.Now.Ticks;
                LaboralTask l = new LaboralTask()
                {
                    IDLaboralTask = ApplicationManager.GenerateGUID,
                    IDAttentionArea = laboralTask.IDAttentionArea,
                    IDRequesterArea = laboralTask.IDRequesterArea,
                    IDWorkplace = laboralTask.IDWorkplace,
                    IDAgent = laboralTask.IDAgent,
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

                    if (RepositoryDocumentFile.Add(d) > 0)
                    {
                        l.IDDocumentFile = d.DocumentFileID;

                        if (RepositoryLaboralTask.Add(l) > 0)
                        {
                            LaboralTaskHistoryLog log = new LaboralTaskHistoryLog()
                            {
                                IDLaboralTaskHistoryLog = ApplicationManager.GenerateGUID,
                                IDLaboralTask = l.IDLaboralTask,
                                Description = "DOCUMENTACION RECIBIDA, EN ESPERA DE SU ATENCION POR SU ESPECIALISTA",
                                Date = DateTime.Now,
                                UploadTicks = submittionticks
                            };
                            if (RepositoryLaboralTaskHistoryLog.Add(log) > 0)
                                return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente: Actualizacion de asunto registrada", Type = ResponseType.SUCCESS }));
                            else
                                return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente", Type = ResponseType.SUCCESS }));
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
                else if (RepositoryLaboralTask.Add(l) > 0)
                {
                    LaboralTaskHistoryLog log = new LaboralTaskHistoryLog()
                    {
                        IDLaboralTaskHistoryLog = ApplicationManager.GenerateGUID,
                        IDLaboralTask = l.IDLaboralTask,
                        Description = "DOCUMENTACION RECIBIDA, EN ESPERA DE SU ATENCION POR SU ESPECIALISTA",
                        Date = DateTime.Now,
                        UploadTicks = submittionticks
                    };

                    if (RepositoryLaboralTaskHistoryLog.Add(log) > 0)
                        return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente: Actualizacion de asunto registrada", Type = ResponseType.SUCCESS }));
                    else
                        return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente", Type = ResponseType.SUCCESS }));
                }
            }
            return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "No se pudo procesar solicitud, falta informacion o archivo digital", Type = ResponseType.ERROR }));
        }
    }
}