using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryAttentionArea
    {
        public static int Add(AttentionArea a)
        {
            using (var db = new VisiflexContext())
            {
                db.AttentionAreas.AddOrUpdate(a);
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

        public static int Delete(string id)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(id)).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
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
    }
}