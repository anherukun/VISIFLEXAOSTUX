using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryLaboralTaskHistoryLog
    {
        public static int Add(LaboralTaskHistoryLog l)
        {
            using (var db = new VisiflexContext())
            {
                db.LaboralTaskHistoryLogs.AddOrUpdate(l);
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
        public static LaboralTaskHistoryLog GetLast(string laboralTaskID)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTaskHistoryLogs.Where(x => x.IDLaboralTask == laboralTaskID)
                    .OrderByDescending(x => x.Date).FirstOrDefault();
            }
        }

        public static int Delete(string id)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(id)).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static bool Exist(string laboralTaskHistoryLogID)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTaskHistoryLogs.Any(x => x.IDLaboralTaskHistoryLog == laboralTaskHistoryLogID);
            }
        }
    }
}