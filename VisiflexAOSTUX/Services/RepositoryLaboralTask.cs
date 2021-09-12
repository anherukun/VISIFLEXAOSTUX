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
                return db.LaboralTasks.OrderBy(x => x.Count).ToList();
            }
        }
        public static List<LaboralTask> Get(System.Linq.Expressions.Expression<Func<LaboralTask, bool>> predicate)
        {
            using (var db = new VisiflexContext())
            {
                return db.LaboralTasks.Where(predicate).OrderBy(x => x.Count).ToList();
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
                {
                    List<LaboralTask> docs = db.LaboralTasks.OrderBy(x => x.Count).ToList();

                    return docs.Last().Count + 1;
                }
            }
        }

        public static Dictionary<string, Dictionary<string, List<LaboralTask>>> GetByRequesterArea()
        {
            List<string> RequesterAreas = new List<string>();
            List<string> Workplaces = new List<string>();

            Dictionary<string, Dictionary<string, List<LaboralTask>>> result = new Dictionary<string, Dictionary<string, List<LaboralTask>>>();
            using (var db = new VisiflexContext())
            {
                RequesterAreas = db.LaboralTasks.Select(x => x.IDRequesterArea).Distinct().ToList();
                Workplaces = db.LaboralTasks.Select(x => x.IDWorkplace).Distinct().ToList();

                for (int i = 0; i < Workplaces.Count; i++)
                {
                    Dictionary<string, List<LaboralTask>> node = new Dictionary<string, List<LaboralTask>>();
                    for (int j = 0; j < RequesterAreas.Count; j++)
                    {
                        string wp = Workplaces[i], ra = RequesterAreas[j];
                        List<LaboralTask> laboralTasks = db.LaboralTasks.Where(x => x.IDWorkplace == wp).ToList();

                        node.Add(RequesterAreas[j], laboralTasks.Where(x => x.IDRequesterArea == ra).ToList());
                    }
                    result.Add(Workplaces[i], node);
                }
                return result;
            }
        }

        public static Dictionary<string, Dictionary<string, List<LaboralTask>>> GetByAttentionArea()
        {
            List<string> AttentionAreas = new List<string>();
            List<string> Workplaces = new List<string>();

            Dictionary<string, Dictionary<string, List<LaboralTask>>> result = new Dictionary<string, Dictionary<string, List<LaboralTask>>>();
            using (var db = new VisiflexContext())
            {
                AttentionAreas = db.LaboralTasks.Select(x => x.IDAttentionArea).Distinct().ToList();
                Workplaces = db.LaboralTasks.Select(x => x.IDWorkplace).Distinct().ToList();

                for (int i = 0; i < Workplaces.Count; i++)
                {
                    Dictionary<string, List<LaboralTask>> node = new Dictionary<string, List<LaboralTask>>();
                    for (int j = 0; j < AttentionAreas.Count; j++)
                    {
                        string wp = Workplaces[i], aa = AttentionAreas[j];
                        List<LaboralTask> laboralTasks = db.LaboralTasks.Where(x => x.IDWorkplace == wp).ToList();
                        
                        node.Add(AttentionAreas[j], laboralTasks.Where(x => x.IDAttentionArea == aa).ToList());
                    }
                    result.Add(Workplaces[i], node);
                }
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