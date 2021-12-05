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
                UserRols = RepositoryUserRol.Get(),
                Accounts = RepositoryAccount.Get(),
                AttentionAreas = RepositoryAttentionArea.Get(),
                RequesterAreas = RepositoryRequesterArea.Get(),
                Workplaces = RepositoryWorkplace.Get(),
                Sessions = RepositorySession.Get(),
                LaboralTasks = RepositoryLaboralTask.Get(),
                DocumentFiles = RepositoryDocumentFile.Get(),
                LaboralTaskHistoryLogs = RepositoryLaboralTaskHistoryLog.Get(),
                HistoryLogDocumentFiles = RepositoryHistoryLogDocumentFile.Get()
            };

            return backup;
        }

        public static int RecoverData(Backup backup)
        {
            int entitiesAdded = 0, entitiesDeleted = 0;

            // Elimina los datos de los repositorios correspondientes empezando por las entidades relacionadas y terminando con las entidades principales
            // entitiesDeleted += RepositoryHistoryLogDocumentFile.PurgeAll();
            // entitiesDeleted += RepositoryLaboralTaskHistoryLog.PurgeAll();
            // entitiesDeleted += RepositoryDocumentFile.PurgeAll();
            // entitiesDeleted += RepositoryLaboralTask.PurgeAll();

            entitiesDeleted += RepositoryLaboralTask.PurgeAll();
            entitiesDeleted += RepositoryHistoryLogDocumentFile.PurgeAll();
            entitiesDeleted += RepositoryDocumentFile.PurgeAll();
            entitiesDeleted += RepositoryLaboralTaskHistoryLog.PurgeAll();

            // entitiesDeleted += RepositoryDocumentFile.PurgeAll();

            entitiesDeleted += RepositorySession.PurgeAll();
            entitiesDeleted += RepositoryWorkplace.PurgeAll();
            entitiesDeleted += RepositoryRequesterArea.PurgeAll();
            entitiesDeleted += RepositoryAttentionArea.PurgeAll();
            entitiesDeleted += RepositoryAccount.PurgeAll();
            entitiesDeleted += RepositoryUserRol.PurgeAll();

            // Agrega datos a los repositorios correspondientes empezando por las entidades principales hasta las entidades relacionadas

            // Se elimina la propiedad relacional anidada dentro de cada entidad en Accounts
            for (int i = 0; i < backup.Accounts.Count; i++)
                backup.Accounts[i].UserRol = null;

            entitiesAdded += RepositoryUserRol.AddRange(backup.UserRols);
            entitiesAdded += RepositoryAccount.AddRange(backup.Accounts);
            entitiesAdded += RepositoryAttentionArea.AddRange(backup.AttentionAreas);
            entitiesAdded += RepositoryRequesterArea.AddRange(backup.RequesterAreas);
            entitiesAdded += RepositoryWorkplace.AddRange(backup.Workplaces);
            entitiesAdded += RepositorySession.AddRange(backup.Sessions);

            entitiesAdded += RepositoryDocumentFile.AddRange(backup.DocumentFiles);
            entitiesAdded += RepositoryHistoryLogDocumentFile.AddRange(backup.HistoryLogDocumentFiles);
            entitiesAdded += RepositoryLaboralTask.AddRange(backup.LaboralTasks);
            entitiesAdded += RepositoryLaboralTaskHistoryLog.AddRange(backup.LaboralTaskHistoryLogs);

            return entitiesAdded;
        }
    }
}