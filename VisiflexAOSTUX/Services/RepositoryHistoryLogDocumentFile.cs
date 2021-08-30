using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryHistoryLogDocumentFile
    {
        public static int Add(HistoryLogDocumentFile h)
        {
            using (var db = new VisiflexContext())
            {
                db.HistoryLogDocumentFiles.AddOrUpdate(h);
                return db.SaveChanges();
            }
        }

        public static List<HistoryLogDocumentFile> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.HistoryLogDocumentFiles.OrderBy(x => x.UploadTicks).ToList();
            }
        }
        public static HistoryLogDocumentFile Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.HistoryLogDocumentFiles.Where(x => x.IDHistoryLogDocumentFile == id).FirstOrDefault();
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

        public static bool Exist(string historyLogDocumentFileID)
        {
            using (var db = new VisiflexContext())
            {
                return db.HistoryLogDocumentFiles.Any(x => x.IDHistoryLogDocumentFile == historyLogDocumentFileID);
            }
        }
    }
}