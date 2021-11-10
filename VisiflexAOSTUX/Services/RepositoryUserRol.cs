using System;
using System.Collections.Generic;
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

        public static UserRol Get(Expression<Func<UserRol, bool>> predicate)
        {
            using (var db = new VisiflexContext())
            {
                return db.UserRols.Where(predicate).FirstOrDefault();
            }
        }
    }
}