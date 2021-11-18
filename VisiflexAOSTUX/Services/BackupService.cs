using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class BackupService
    {
        public static Backup CreateBackup()
        {
            Backup backup = new Backup()
            {
                Accounts = RepositoryAccount.Get(),
                AttentionAreas = RepositoryAttentionArea.Get(),
                DocumentFiles = RepositoryDocumentFile.Get(),
                HistoryLogDocumentFiles = RepositoryHistoryLogDocumentFile.Get(),
                LaboralTasks = RepositoryLaboralTask.Get(),
                LaboralTaskHistoryLogs = RepositoryLaboralTaskHistoryLog.Get(),
                RequesterAreas = RepositoryRequesterArea.Get(),
                Sessions = RepositorySession.Get(),
                UserRols = RepositoryUserRol.Get(),
                Workplaces = RepositoryWorkplace.Get()
            };

            return backup;
        }
    }
}