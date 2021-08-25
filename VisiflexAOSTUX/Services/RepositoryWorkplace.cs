using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryWorkplace
    {
        public static int Add(Workplace w)
        {
            using (var db = new VisiflexContext())
            {
                db.Workplaces.AddOrUpdate(w);
                return db.SaveChanges();
            }
        }

        public static List<Workplace> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.Workplaces.OrderBy(x => x.WorkplaceCode).ToList();
            }
        }
        public static Workplace Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.Workplaces.Where(x => x.WorkplaceID == id).FirstOrDefault();
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

        public static bool Exist(Workplace w)
        {
            using (var db = new VisiflexContext())
            {
                return db.Workplaces.Any(x => x.WorkplaceCode == w.WorkplaceCode);
            }
        }
        public static bool Exist(string workplaceID)
        {
            using (var db = new VisiflexContext())
            {
                return db.Workplaces.Any(x => x.WorkplaceID == workplaceID);
            }
        }
    }
}