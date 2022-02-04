using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryAttentionArea
    {
        public static int AddOrUpdate(AttentionArea a)
        {
            using (var db = new VisiflexContext())
            {
                db.AttentionAreas.AddOrUpdate(a);
                return db.SaveChanges();
            }
        }
        internal static int AddRange(List<AttentionArea> attentionAreas)
        {
            using (var db = new VisiflexContext())
            {
                db.AttentionAreas.AddRange(attentionAreas);
                return db.SaveChanges();
            }
        }

        public static List<AttentionArea> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.AttentionAreas.OrderBy(x => x.AreaCode).ToList();
            }
        }
        public static AttentionArea Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.AttentionAreas.Where(x => x.AttentionAreaID == id).FirstOrDefault();
            } 
        }

        public static bool Exist(AttentionArea a)
        {
            using (var db = new VisiflexContext())
            {
                return db.AttentionAreas.Any(x => x.AreaCode == a.AreaCode );
            }
        }

        public static bool Exist(string attentionAreaID)
        {
            using (var db = new VisiflexContext())
            {
                return db.AttentionAreas.Any(x => x.AttentionAreaID == attentionAreaID);
            }
        }

        public static int Delete(string idAttentionarea)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(idAttentionarea)).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static int PurgeAll()
        {
            var table = Get();
            int count = 0;
            foreach (var item in table)
            {
                count += Delete(item.AttentionAreaID);
            }

            return count;
        }
    }
}