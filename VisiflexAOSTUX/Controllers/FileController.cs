using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisiflexAOSTUX.Application;
using VisiflexAOSTUX.Models;
using VisiflexAOSTUX.Services;

namespace VisiflexAOSTUX.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public FileResult ViewLaboralTaskDocumentFile(string DocumentFileID)
        {
            byte[] bytes;

            if (RepositoryDocumentFile.Exist(DocumentFileID))
            {
                DocumentFile document = RepositoryDocumentFile.Get(DocumentFileID);

                bytes = ApplicationManager.Decompress(document.Data);
                return File(bytes, document.Mime);

            }
            return null;
        }
    }
}