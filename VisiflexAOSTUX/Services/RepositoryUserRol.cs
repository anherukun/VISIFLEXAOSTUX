using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryUserRol
    {
        public static int AddOrUpdate(UserRol userRol)
        {
            using (var db = new VisiflexContext())
            {
                db.UserRols.AddOrUpdate(userRol);
                return db.SaveChanges();
            }
        }

        public static List<UserRol> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.UserRols.OrderBy(x => x.UserLevel).ToList();
            }
        }
        public static UserRol Get(string idUserrol)
        {
            using (var db = new VisiflexContext())
            {
                return db.UserRols.Where(x => x.IDUserRol == idUserrol).FirstOrDefault();
            }
        }

        public static UserRol Get(Expression<Func<UserRol, bool>> predicate)
        {
            using (var db = new VisiflexContext())
            {
                return db.UserRols.Where(predicate).FirstOrDefault();
            }
        }

        public static int Delete(string idUserrol)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(idUserrol)).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static int PurgeAll()
        {
            var table = Get();
            int count = 0;
            foreach (var item in table)
            {
                count += Delete(item.IDUserRol);
            }

            return count;
        }
    }
}