using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public static bool Exist(string historyLogDocumentFileID)
        {
            using (var db = new VisiflexContext())
            {
                return db.HistoryLogDocumentFiles.Any(x => x.IDHistoryLogDocumentFile == historyLogDocumentFileID);
            }
        }

        public static int Delete(string idHistorylogDocfile)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(idHistorylogDocfile)).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static int PurgeAll()
        {
            var table = Get();
            int count = 0;
            foreach (var item in table)
            {
                count += Delete(item.IDHistoryLogDocumentFile);
            }

            return count;
        }
    }
}