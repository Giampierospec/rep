using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArchiveUpload.Models;

namespace ArchiveUpload.Repository
{
    public class ArchiveRepository : IArchive
    {
        private DocContext _db;

        public ArchiveRepository()
        {
            _db = new DocContext();
        }
        /// <summary>
        /// Consigue todos los documentos
        /// </summary>
        /// <returns></returns>
        public List<Documento> GetAllDocumentos()
        {
            var documentos = _db.Documentos.ToList();
            return documentos;
        }

        public void InsertDocumento(Documento doc)
        {
           var dc = _db.Documentos.Where(x => x.Archivo == doc.Archivo).FirstOrDefault();
            if (dc != null)
            {
                dc = doc;
                _db.SaveChanges();
            }
            else
            {
                _db.Documentos.Add(doc);
                _db.SaveChanges();
            }
        }
        public List<docs2> GetAllDocu()
        {
            var docs = _db.docs2.ToList();
            return docs;
        }

        public void InsertDocu(docs2 doc)
        {
            _db.docs2.Add(doc);
            _db.SaveChanges();
        }
    }
}