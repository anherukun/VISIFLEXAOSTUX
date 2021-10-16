using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositorySession
    {
        /// <summary>
        /// Agrega o actualiza una entidad
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>Numero de filas agregadas o modificadas</returns>
        public static int AddOrUpdate(Session session)
        {
            using (var db = new VisiflexContext())
            {
                db.Sessions.AddOrUpdate(session);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// Obtiene una entidad
        /// </summary>
        /// <param name="predicate">Secuencia de condiciones</param>
        /// <returns><see cref="Session"/></returns>
        public static Session Get(Expression<Func<Session, bool>> predicate)
        {
            using (var db = new VisiflexContext())
            {
                return db.Sessions.Where(predicate).FirstOrDefault();
            }
        }

        /// <summary>
        /// Compueba que el token asignado sea el correcto
        /// </summary>
        /// <param name="sessionToken">Token de la sesion</param>
        /// <param name="idAccount">ID de la cuenta</param>
        /// <returns>True: si el token asignado a la cuenta es correcto. False: Si es incorrecto</returns>
        public static bool OnSession(string sessionToken, string idAccount)
        {
            using (var db = new VisiflexContext())
            {
                if (Exist(idAccount))
                {
                    var session = db.Sessions.Where(x => x.IDAccount == idAccount).FirstOrDefault();

                    if (session != null)
                        return session.SessionToken == sessionToken;
                }
                return false;
            }
        }
        /// <summary>
        /// Compueba que exista al menos una entidad
        /// </summary>
        /// <param name="idAccount">ID de la cuenta</param>
        /// <returns>True: si existe una entidad para esa cuenta. False: Si no existe</returns>
        public static bool Exist(string idAccount)
        {
            using (var db = new VisiflexContext())
            {
                return db.Sessions.Any(x => x.IDAccount == idAccount);
            }
        }
        public static bool Exist(Expression<Func<Session, bool>> predicate)
        {
            using (var db = new VisiflexContext())
            {
                return db.Sessions.Any(predicate);
            }
        }
    }
}