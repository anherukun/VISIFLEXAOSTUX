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
    public class LaboralTaskController : Controller
    {
        // GET: LaboralTask
        public ActionResult Index()
        {
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
            if (File != null && laboralTask != null)
            {
                long submittionticks = DateTime.Now.Ticks;
                LaboralTask l = new LaboralTask()
                {
                    IDLaboralTask = ApplicationManager.GenerateGUID,
                    IDAttentionArea = laboralTask.IDAttentionArea,
                    IDRequesterArea = laboralTask.IDRequesterArea,
                    IDWorkplace = laboralTask.IDWorkplace,
                    DocumentID = laboralTask.DocumentID.Trim().ToUpper(),
                    AttentionInstructions = laboralTask.AttentionInstructions.Trim().ToUpper(),
                    DocumentDate = laboralTask.DocumentDate,
                    ReceptionDate = laboralTask.ReceptionDate,
                    LongDescription = laboralTask.LongDescription.Trim().ToUpper(),
                    Observations = laboralTask.Observations.Trim().ToUpper(),
                    Subjet = laboralTask.Subjet.Trim().ToUpper(),
                    Status = "INICIADO",
                    UploadTicks = submittionticks,
                    Progress = 0
                };

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
                        return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "Asunto laboral registrado correctamente", Type = ResponseType.SUCCESS }));
                    else
                    {
                        if (RepositoryDocumentFile.Delete(d.DocumentFileID) > 1)
                            return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "No se pudo procesar solicitud, Archivo eliminado", Type = ResponseType.ERROR }));
                        else
                            return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "No se pudo procesar solicitud, Archivo no eliminado", Type = ResponseType.ERROR }));
                    }
                }
            }
            return Redirect(Url.Action("NewLaboralTask", "LaboralTask", new ResponseMessage() { Message = "No se pudo procesar solicitud, falta informacion o archivo digital", Type = ResponseType.ERROR }));
        }
    }
}