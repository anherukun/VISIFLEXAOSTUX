using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryAgent
    {
        public static int Add(Agent a)
        {
            using (var db = new VisiflexContext())
            {
                db.Agents.AddOrUpdate(a);
                return db.SaveChanges();
            }
        }

        public static List<Agent> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.Agents.OrderBy(x => x.AgentCode).ToList();
            }
        }
        public static Agent Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.Agents.Where(x => x.AgentID == id).FirstOrDefault();
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

        public static bool Exist(Agent a)
        {
            using (var db = new VisiflexContext())
            {
                return db.Agents.Any(x => x.AgentCode == a.AgentCode);
            }
        }
        public static bool Exist(string agentID)
        {
            using (var db = new VisiflexContext())
            {
                return db.Agents.Any(x => x.AgentID == agentID);
            }
        }
    }
}