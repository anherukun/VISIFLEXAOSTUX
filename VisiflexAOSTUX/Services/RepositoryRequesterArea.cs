using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryRequesterArea
    {
        public static int Add(RequesterArea r)
        {
            using (var db = new VisiflexContext())
            {
                db.RequesterAreas.AddOrUpdate(r);
                return db.SaveChanges();
            }
        }

        public static List<RequesterArea> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.RequesterAreas.OrderBy(x => x.Name).ToList();
            }
        }
        public static RequesterArea Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.RequesterAreas.Where(x => x.RequesterAreaID == id).FirstOrDefault();
            }
        }

        public static bool Exist(RequesterArea r)
        {
            using (var db = new VisiflexContext())
            {
                return db.RequesterAreas.Any(x => x.Name == r.Name);
            }
        }
        public static bool Exist(string requesterAreaID)
        {
            using (var db = new VisiflexContext())
            {
                return db.RequesterAreas.Any(x => x.RequesterAreaID == requesterAreaID);
            }
        }

        public static int Delete(string idRequesterarea)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(idRequesterarea)).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static int PurgeAll()
        {
            var table = Get();
            int count = 0;
            foreach (var item in table)
            {
                count += Delete(item.RequesterAreaID);
            }

            return count;
        }
    }
}