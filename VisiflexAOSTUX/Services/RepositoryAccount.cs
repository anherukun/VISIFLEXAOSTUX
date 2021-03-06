using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VisiflexAOSTUX.Application;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryAccount
    {
        /// <summary>
        /// Agrega o actualiza una entidad
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>Numero de filas agregadas o modificadas</returns>
        public static int AddOrUpdate(Account account)
        {
            using (var db = new VisiflexContext())
            {
                db.Accounts.AddOrUpdate(account);
                return db.SaveChanges();
            }
        }
        public static int AddRange(List<Account> accounts)
        {
            using (var db = new VisiflexContext())
            {
                db.Accounts.AddRange(accounts);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// Obtiene una lista de entidades
        /// </summary>
        /// <returns>Lista de <see cref="Account"/></returns>
        public static List<Account> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.Accounts.Include(x => x.UserRol).OrderBy(x => x.Username).ToList();
            }
        }

        /// <summary>
        /// Obtiene una entidad
        /// </summary>
        /// <param name="id">ID de la cuenta</param>
        /// <returns><see cref="Account"/></returns>
        public static Account Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.Accounts.Include(x => x.UserRol).Where(x => x.IDAccount == id).FirstOrDefault();
            }
        }
        /// <summary>
        /// Obtiene una entidad
        /// </summary>
        /// <param name="predicate">Expresion LINQ</param>
        /// <returns><see cref="Account"/></returns>
        public static Account Get(Expression<Func<Account, bool>> predicate)
        {
            using (var db = new VisiflexContext())
            {
                return db.Accounts.Where(predicate).Include(x => x.UserRol).FirstOrDefault();
                //return db.Accounts.Where(predicate).Include(x => x.UserRol).Select(x => new Account
                //{
                //    IDAccount = x.IDAccount,
                //    Email = x.Email,
                //    Username = x.Username,
                //    IDUserRol = x.IDUserRol,
                //    UserRol = x.UserRol,
                //    CreatedAt = x.CreatedAt
                //}).FirstOrDefault();
            }
        }

        /// <summary>
        /// Comparador de contraseñas
        /// </summary>
        /// <param name="username">Usuario de la cuenta</param>
        /// <param name="value">Cadena de comparacion</param>
        /// <returns>True: si es correcta. False: si es incorrecta</returns>
        public static bool IsPasswordCorrect(string username, string value)
        {
            using (var db = new VisiflexContext())
            {
                var account = db.Accounts.Where(x => x.Username == username || x.Email == username).FirstOrDefault();
                if (account != null)
                    return account.PasswordHash == Security.SHA256Hash(value) ? true : false;

                return false;
            }
        }

        public static bool Exist(Expression<Func<Account, bool>> predicate)
        {
            using (var db = new VisiflexContext())
            {
                return db.Accounts.Any(predicate);
            }
        }

        public static int Delete(string idAccount)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(idAccount)).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static int PurgeAll()
        {
            var table = Get();
            int count = 0;
            foreach (var item in table)
            {
                count += Delete(item.IDAccount);
            }

            return count;
        }
    }
}