using ArchiveUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchiveUpload.Repository
{
    public interface IArchive
    {
        List<Documento> GetAllDocumentos();
        void InsertDocumento(Documento doc);
        List<docs2> GetAllDocu();
        void InsertDocu(docs2 doc);
    }
}