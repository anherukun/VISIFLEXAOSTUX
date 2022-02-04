using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class ExportService
    {
        public static string GetCSVStringData(List<Dictionary<string, string>> dataset, char separator)
        {
            string result = "";

            // NOMBRES DE COLUMNAS
            foreach (var item in dataset[0])
            {
                result += $"{item.Key}{separator}";
            }
            result = result.Remove(result.Length - 1);
            result += "\n";

            foreach (var entry in dataset)
            {
                foreach (var item in entry)
                {
                    result += $"{item.Value}{separator}";
                }
                result = result.Remove(result.Length - 1);
                result += "\n";
            }

            return result;
        }

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
                entry.Add("FECHA_RECEPCION", $"{item.ReceptionDate.ToShortDateString()}");
                entry.Add("FECHA_COMPROMISO", $"{item.CommitmentDate.ToShortDateString()}");
                entry.Add("ESTADO", $"{item.Status}");
                entry.Add("AREA_ATENCION", $"{attentionArea.AreaCode} - {attentionArea.Name}");
                entry.Add("AREA_SOLICITANTE", $"{requesterArea.Name}");
                entry.Add("CENTRO_TRABAJO", $"{workplace.WorkplaceCode} - {workplace.Name}");
                entry.Add("FICHA_AGENTE", $"{account.Username}");
                entry.Add("NOMBRE_AGENTE", $"{account.Name} {account.LastName}");
                entry.Add("ASUNTO", $"{item.Subjet.Replace(',' , '.')}");
                entry.Add("DESCRIPCION_LARGA", $"{item.LongDescription.Replace(',', '.')}");
                entry.Add("FECHA_ULTIMO_SEGUIMIENTO", $"{lastHistoryLog.Date.ToShortDateString()}");
                entry.Add("ULTIMO_SEGUIMIENTO", $"{lastHistoryLog.Description.Replace(',', '.')}");

                if (item.IDDocumentFile == null)
                    entry.Add("LINK_ARCHIVO", $"No disponible");
                else
                    entry.Add("LINK_ARCHIVO", $"{laboralTaskController.HttpContext.Request.Url.Authority}{laboralTaskController.Url.Action("ViewLaboralTaskDocumentFile", "File", new { DocumentFileID = item.IDDocumentFile })}");

                // item.IDDocumentFile == null ? entry.Add("LINK_ARCHIVO", $"No disponible") : entry.Add("LINK_ARCHIVO", $"{url.Action("ViewLaboralTaskDocumentFile", "File", new { DocumentFileID = item.IDDocumentFile })}");

                matrix.Add(entry);
            }
            return matrix;
        }

        public static List<Dictionary<string, string>> GetLaboralTaskMatrix(Controllers.LaboralTaskController laboralTaskController, System.Linq.Expressions.Expression<Func<LaboralTask, bool>> predicate)
        {
            List<LaboralTask> laboralTasks = RepositoryLaboralTask.Get(predicate);
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
                entry.Add("FECHA_RECEPCION", $"{item.ReceptionDate.ToShortDateString()}");
                entry.Add("FECHA_COMPROMISO", $"{item.CommitmentDate.ToShortDateString()}");
                entry.Add("ESTADO", $"{item.Status}");
                entry.Add("AREA_ATENCION", $"{attentionArea.AreaCode} - {attentionArea.Name}");
                entry.Add("AREA_SOLICITANTE", $"{requesterArea.Name}");
                entry.Add("CENTRO_TRABAJO", $"{workplace.WorkplaceCode} - {workplace.Name}");
                entry.Add("FICHA_AGENTE", $"{account.Username}");
                entry.Add("NOMBRE_AGENTE", $"{account.Name} {account.LastName}");
                entry.Add("ASUNTO", $"{item.Subjet.Replace(',', '.')}");
                entry.Add("DESCRIPCION_LARGA", $"{item.LongDescription.Replace(',', '.')}");
                entry.Add("FECHA_ULTIMO_SEGUIMIENTO", $"{lastHistoryLog.Date.ToShortDateString()}");
                entry.Add("ULTIMO_SEGUIMIENTO", $"{lastHistoryLog.Description.Replace(',', '.')}");

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