#region Usings
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : BaseUploadController
    {
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile(FileUploadViewModel model)
        {
            try
            {
                var file = model.File;

                if (!Directory.Exists(DiretorioUpload))
                    Directory.CreateDirectory(DiretorioUpload);

                long oidArquivoUpload = 0;

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(DiretorioUpload, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var arquivo = new ArquivoUploadEntidade
                    {
                        DTA_UPLOAD = DateTime.Now,
                        IND_STATUS = 2,
                        NOM_ARQUIVO_LOCAL = fileName,
                        NOM_ARQUIVO_ORIGINAL = fileName,
                        NOM_DIRETORIO_LOCAL = "Upload"
                    };

                    oidArquivoUpload = new ArquivoUploadProxy().Inserir(arquivo);
                }

                return Json(oidArquivoUpload);
            }
            catch (Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }
    }

    public class FileUploadViewModel
    {
        public IFormFile File { get; set; }
        public string source { get; set; }
        public long Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Extension { get; set; }
    }
}