using ArchiveUpload.Models;
using ArchiveUpload.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchiveUpload.Controllers
{
    public class DocumentController : Controller
    {
        private ArchiveRepository _repo;

        public DocumentController()
        {
            _repo = new ArchiveRepository();
        }
        // GET: Document
        public ActionResult Index()
        {
            var model = _repo.GetAllDocumentos();
            return View(model);
        }
        public ActionResult CreateDoc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoc(Documento doc, HttpPostedFileBase archive)
        {
            string path = Path.Combine(Server.MapPath("~/Uploads"),archive.FileName);
            if (!Directory.Exists(Server.MapPath("~/Uploads"))){
                Directory.CreateDirectory(Server.MapPath("~/Uploads"));
            }
            archive.SaveAs(path);
            doc.Archivo = archive.FileName;
            _repo.InsertDocumento(doc);
            return RedirectToAction("Index");
        }
        public FileResult DownloadDoc(int? id)
        {
            var model = _repo.GetAllDocumentos().Where(x => x.Id == id).FirstOrDefault();

            return File(Path.Combine(Server.MapPath("~/Uploads"),model.Archivo),"application/pdf");
        }
    }
}