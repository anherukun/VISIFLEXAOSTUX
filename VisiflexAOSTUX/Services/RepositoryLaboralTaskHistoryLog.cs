using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryLaboralTaskHistoryLog
    {
        public static int AddOrUpdate(LaboralTaskHistoryLog l)
        {
            using (var db = new VisiflexContext())
            {
                db.LaboralTaskHistoryLogs.AddOrUpdate(l);
                return db.SaveChanges();
            }
        }
        internal static int AddRange(List<LaboralTaskHistoryLog> laboralTaskHistoryLogs)
        {
            using (var db = new VisiflexContext())
            {
                db.LaboralTaskHistoryLogs.AddRange(laboralTaskHistoryLogs);
                return db.SaveChanges();
            }
        }

        public static List<LaboralTaskHistoryLog> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTaskHistoryLogs.OrderBy(x => x.UploadTicks).ToList();
            }
        }
        public static List<LaboralTaskHistoryLog> Get(string laboralTaskID)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTaskHistoryLogs.Where(x => x.IDLaboralTask == laboralTaskID)
                    .OrderByDescending(x => x.UploadTicks).ToList();
            }
        }
        public static LaboralTaskHistoryLog GetSingle(string laboralTaskHistoryLogID)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTaskHistoryLogs.Where(x => x.IDLaboralTaskHistoryLog == laboralTaskHistoryLogID).FirstOrDefault();
            }
        }
        public static LaboralTaskHistoryLog GetLast(string laboralTaskID)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTaskHistoryLogs.Where(x => x.IDLaboralTask == laboralTaskID)
                    .OrderByDescending(x => x.Date).FirstOrDefault();
            }
        }


        public static bool Exist(string laboralTaskHistoryLogID)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTaskHistoryLogs.Any(x => x.IDLaboralTaskHistoryLog == laboralTaskHistoryLogID);
            }
        }

        public static int Delete(string idLaboraltaskHistlog)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(GetSingle(idLaboraltaskHistlog)).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static int PurgeAll()
        {
            var table = Get();
            int count = 0;
            foreach (var item in table)
            {
                count += Delete(item.IDLaboralTaskHistoryLog);
            }

            return count;
        }
    }
}