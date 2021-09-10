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

        public static int GetNextCount()
        {
            using (var db = new VisiflexContext())
            {
                if (db.LaboralTasks.Count() == 0)
                    return 1;
                else
                    return db.LaboralTasks.Last().Count + 1;
            }
        }

        public static Dictionary<string, List<LaboralTask>> GetByRequesterArea()
        {
            List<string> RequesterAreas = new List<string>();
            foreach (var item in RepositoryRequesterArea.Get())
                RequesterAreas.Add(item.RequesterAreaID);

            Dictionary<string, List<LaboralTask>> result = new Dictionary<string, List<LaboralTask>>();

            using (var db = new VisiflexContext())
            {
                foreach (var item in RequesterAreas)
                    result.Add(item, db.LaboralTasks.Where(x => x.IDRequesterArea == item).ToList());

                return result;
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