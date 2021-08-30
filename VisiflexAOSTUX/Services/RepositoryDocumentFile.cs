using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VisiflexAOSTUX.Models;

namespace VisiflexAOSTUX.Services
{
    public class RepositoryDocumentFile
    {
        public static int Add(DocumentFile d)
        {
            using (var db = new VisiflexContext())
            {
                db.DocumentFiles.AddOrUpdate(d);
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

        public static int Delete(string id)
        {
            using (var db = new VisiflexContext())
            {
                db.Entry(Get(id)).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public static bool Exist(string documentID)
        {
            using (var db = new VisiflexContext())
            {
                return db.DocumentFiles.Any(x => x.DocumentFileID == documentID);
            }
        }
    }
}