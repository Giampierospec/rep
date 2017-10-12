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
        public Object DownloadDoc(int? id)
        {
            var model = _repo.GetAllDocumentos().Where(x => x.Id == id).FirstOrDefault();

            return File(Path.Combine(Server.MapPath("~/Uploads"),model.Archivo),"application/pdf");
        }

        public ActionResult Docus()
        {
            var model = _repo.GetAllDocu();
            return View(model);
        }
        public ActionResult CreateDocu()
        {
            return View();
        }
        public object DownloadDocBytes(int? id)
        {
            var model = _repo.GetAllDocu().Where(x => x.Id == id).FirstOrDefault();
            Response.Clear();
            MemoryStream ms = new MemoryStream(model.data);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=labtest.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
            return File(Response.OutputStream, Response.ContentType, "Some.pdf");
        }
        [HttpPost]
        public ActionResult CreateDocu(docs2 doc, HttpPostedFileBase archive)
        {
            string path = Path.Combine(Server.MapPath("~/Uploads"), archive.FileName);
            if (!Directory.Exists(Server.MapPath("~/Uploads")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Uploads"));
            }
            archive.SaveAs(path);
            var fs = archive.InputStream;
            var br = new BinaryReader(fs);
            doc.data = br.ReadBytes((Int32)fs.Length);
            _repo.InsertDocu(doc);
            return RedirectToAction("Index");
        }

    }
}