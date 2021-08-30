using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryLaboralTask
    {
        public static int Add(LaboralTask l)
        {
            using (var db = new VisiflexContext())
            {
                db.LaboralTasks.AddOrUpdate(l);
                return db.SaveChanges();
            }
        }

        public static List<LaboralTask> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTasks.OrderBy(x => x.UploadTicks).ToList();
            }
        }
        public static LaboralTask Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTasks.Where(x => x.IDLaboralTask == id).FirstOrDefault();
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

        public static bool Exist(string laboralTaskID)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTasks.Any(x => x.IDLaboralTask == laboralTaskID);
            }
        }
    }
}