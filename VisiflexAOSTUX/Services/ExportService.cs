﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class ExportService
    {
        public static List<Dictionary<string, string>> GetLaboralTaskMatrix(Controllers.LaboralTaskController laboralTaskController)
        {
            List<LaboralTask> laboralTasks = RepositoryLaboralTask.Get();
            List<Dictionary<string, string>> matrix = new List<Dictionary<string, string>>();

            foreach (var item in laboralTasks)
            {
                Account account = RepositoryAccount.Get(item.IDAccount);
                AttentionArea attentionArea = RepositoryAttentionArea.Get(item.IDAttentionArea);
                RequesterArea requesterArea = RepositoryRequesterArea.Get(item.IDRequesterArea);
                Workplace workplace = RepositoryWorkplace.Get(item.IDWorkplace);
                LaboralTaskHistoryLog lastHistoryLog = RepositoryLaboralTaskHistoryLog.GetLast(item.IDLaboralTask);

                Dictionary<string, string> entry = new Dictionary<string, string>();
                // COUNT, LABORALTASKID, DOCUMENTID, RECEPTIONDATE, COMITMENTDATE, STATUS, ATTENTIONAREA, REQUESTERAREA, WORKPLACE, USERNAME, USER_NAMELASTNAME, SUBJECT, LONGDESCRIPTION, LASTINTERVENTION, LINKFILE
                entry.Add("N_SEGUIMIENTO", $"{item.Count}");
                entry.Add("ID_ASUNTO", $"{item.IDLaboralTask}");
                entry.Add("ID_DOCUMENTO", $"{item.DocumentID}");
                entry.Add("FECHA_RECEPCION", $"{item.ReceptionDate}");
                entry.Add("FECHA_COMPROMISO", $"{item.CommitmentDate}");
                entry.Add("ESTADO", $"{item.Status}");
                entry.Add("AREA_ATENCION", $"{attentionArea.AreaCode} - {attentionArea.Name}");
                entry.Add("AREA_SOLICITANTE", $"{requesterArea.Name}");
                entry.Add("CENTRO_TRABAJO", $"{workplace.WorkplaceCode} - {workplace.Name}");
                entry.Add("FICHA_AGENTE", $"{account.Username}");
                entry.Add("NOMBRE_AGENTE", $"{account.Name} {account.LastName}");
                entry.Add("ASUNTO", $"{item.Subjet}");
                entry.Add("DESCRIPCION_LARGA", $"{item.LongDescription}");
                entry.Add("FECHA_ULTIMO_SEGUIMIENTO", $"{lastHistoryLog.Date}");
                entry.Add("ULTIMO_SEGUIMIENTO", $"{lastHistoryLog.Description}");

                if (item.IDDocumentFile == null)
                    entry.Add("LINK_ARCHIVO", $"No disponible");
                else
                    entry.Add("LINK_ARCHIVO", $"http://{laboralTaskController.HttpContext.Request.Url.Authority}{laboralTaskController.Url.Action("ViewLaboralTaskDocumentFile", "File", new { DocumentFileID = item.IDDocumentFile })}");

                // item.IDDocumentFile == null ? entry.Add("LINK_ARCHIVO", $"No disponible") : entry.Add("LINK_ARCHIVO", $"{url.Action("ViewLaboralTaskDocumentFile", "File", new { DocumentFileID = item.IDDocumentFile })}");

                matrix.Add(entry);
            }
            return matrix;
        }
    }
}