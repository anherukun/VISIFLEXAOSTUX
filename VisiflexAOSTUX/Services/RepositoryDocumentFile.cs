using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryDocumentFile
    {
        public static int AddOrUpdate(DocumentFile d)
        {
            using (var db = new VisiflexContext())
            {
                db.DocumentFiles.AddOrUpdate(d);
                return db.SaveChanges();
            }
        }
        internal static int AddRange(List<DocumentFile> documentFiles)
        {
            using (var db = new VisiflexContext())
            {
                db.DocumentFiles.AddRange(documentFiles);
                return db.SaveChanges();
            }
        }

        public static List<DocumentFile> Get()
        {
            using (var db = new VisiflexContext())
            {
                return db.DocumentFiles.OrderBy(x => x.UploadTicks).ToList();
            }
        }
        public static DocumentFile Get(string id)
        {
            using (var db = new VisiflexContext())
            {
                return db.DocumentFiles.Where(x => x.DocumentFileID == id).FirstOrDefault();
            }
        }

        public static bool Exist(string documentID)
        {
            using (var db = new VisiflexContext())
            {
                return db.DocumentFiles.Any(x => x.DocumentFileID == documentID);
            }
        }
        public static int Delete(string idDocumentfile)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(idDocumentfile)).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static int PurgeAll()
        {
            var table = Get();
            int count = 0;
            foreach (var item in table)
            {
                count += Delete(item.DocumentFileID);
            }

            return count;
        }
    }
}