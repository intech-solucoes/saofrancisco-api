#region Usings
using Intech.Lib.Email;
using Intech.Lib.Web;
using Intech.Lib.Web.API;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Entidades.Outros;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    /// <service nome="Documento" />
    [Route(RotasApi.Documento)]
    public class DocumentoController : BaseController
    {
        [HttpPost("[action]")]
        [Authorize("Bearer")]
        [Retorno("string")]
        public IActionResult AtualizarPasta([FromBody] DocumentoPastaEntidade pasta)
        {
            try
            {
                var pasta_antiga = new DocumentoPastaProxy().BuscarPorChave(pasta.OID_DOCUMENTO_PASTA);
                pasta.DTA_INCLUSAO = pasta_antiga.DTA_INCLUSAO;
                new DocumentoPastaProxy().Atualizar(pasta);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize("Bearer")]
        [Retorno("string")]
        public IActionResult AtualizarDocumento([FromBody] DocumentoEntidade doc)
        {
            try
            {
                var doc_antigo = new DocumentoProxy().BuscarPorChave(doc.OID_DOCUMENTO);
                doc.DTA_INCLUSAO = doc_antigo.DTA_INCLUSAO;
                new DocumentoProxy().Atualizar(doc);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porPasta/{oidPasta}")]
        [Authorize("Bearer")]
        [Retorno("any")]
        public IActionResult Buscar(decimal? oidPasta)
        {
            try
            {
                var listaDocumentos = new
                {
                    pastas = new DocumentoPastaProxy().BuscarPorPastaPai(oidPasta),
                    documentos = new DocumentoProxy().BuscarPorPasta(oidPasta),
                    pastaAtual = new DocumentoPastaProxy().BuscarPorChave(oidPasta)
                };

                return Ok(listaDocumentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{cdPlano}/{oidPasta}/{criteria}")]
        [Authorize("Bearer")]
        [Retorno("any")]
        public IActionResult BuscarPorPlanoPasta(string cdPlano, decimal oidPasta, string criteria)
        {
            try
            {
                //string criteria = "data";
                var oid = (string.IsNullOrEmpty(oidPasta.ToString()) || oidPasta == 0) ? (decimal?)null : oidPasta;
                /*var documentos = new DocumentoProxy().BuscarPorPasta(oidPasta);

                var planosVinculados = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatricula(CdFundacao, CdEmpresa, Matricula);
                var listaDocumentos = new List<DocumentoEntidade>();

                var documentosPlanos = new DocumentoPlanoProxy().Listar();

                foreach (var documento in documentos)
                {
                    if (documentosPlanos.Any(x => x.OID_DOCUMENTO == documento.OID_DOCUMENTO))
                    {
                        if (documentosPlanos.Any(x => planosVinculados.Any(x2 => x2.CD_PLANO == x.CD_PLANO)))
                            listaDocumentos.Add(documento);
                    }
                    else
                    {
                        listaDocumentos.Add(documento);
                    }
                }*/

                var documentos = new DocumentoProxy().BuscarPorPastaPlano(oid, cdPlano, criteria);

                var pastaAtual = new DocumentoPastaProxy().BuscarPorChave(oid);

                return Ok(new
                {
                    pastas = new DocumentoPastaProxy().BuscarPorWebUsuario(Cpf, oid, criteria),
                    documentos,
                    pastaAtual,
                    pastaPai = pastaAtual != null ? new DocumentoPastaProxy().BuscarPorChave(pastaAtual.OID_DOCUMENTO_PASTA_PAI) : null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{oidPasta}/{criteria}")]
        [Authorize("Bearer")]
        [Retorno("any")]
        public IActionResult BuscarTodosPorPasta(decimal oidPasta, string criteria = "data")
        {
            try
            {
                var oid = (string.IsNullOrEmpty(oidPasta.ToString()) || oidPasta == 0) ? (decimal?)null : oidPasta;

                var documentos = new DocumentoProxy().BuscarPorPastaJoinTbPlano(oid, criteria);
                var pastaAtual = new DocumentoPastaProxy().BuscarPorChave(oid);

                return Ok(new
                {
                    pastas = new DocumentoPastaProxy().BuscarPorOidPastaPaiJoinWebGrupoUsuario(oid, criteria),
                    documentos,
                    pastaAtual,
                    pastaPai = pastaAtual != null ? new DocumentoPastaProxy().BuscarPorChave(pastaAtual.OID_DOCUMENTO_PASTA_PAI) : null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{oidDocumento}")]
        [Authorize("Bearer")]
        [Retorno(nameof(ArquivoUploadEntidade))]
        public IActionResult BuscarPorOidDocumento(decimal oidDocumento)
        {
            try
            {
                var documento = new DocumentoProxy().BuscarPorChave(oidDocumento);
                return Ok(new ArquivoUploadProxy().BuscarPorChave(documento.OID_ARQUIVO_UPLOAD));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize("Bearer")]
        [Retorno("string")]
        public IActionResult Criar([FromBody] DocumentoEntidade documento)
        {
            try
            {
                documento.DTA_INCLUSAO = DateTime.Now;
                var oidDocumento = new DocumentoProxy().Inserir(documento);

                if (!string.IsNullOrEmpty(documento.CD_PLANO))
                {
                    new DocumentoPlanoProxy().Inserir(new DocumentoPlanoEntidade
                    {
                        OID_DOCUMENTO = oidDocumento,
                        CD_FUNDACAO = "01",
                        CD_PLANO = documento.CD_PLANO
                    });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize("Bearer")]
        [Retorno("string")]
        public IActionResult Deletar([FromBody] DocumentoEntidade doc)
        {
            try
            {
                var arquivoUploadProxy = new ArquivoUploadProxy();
                var documentoProxy = new DocumentoProxy();
                var documentoPlanoProxy = new DocumentoPlanoProxy();
                var documento = documentoProxy.BuscarPorChave(doc.OID_DOCUMENTO);

                documentoPlanoProxy.DeletarPorOidDocumento(documento.OID_DOCUMENTO);

                documentoProxy.Deletar(documento);

                var arquivoUpload = arquivoUploadProxy.BuscarPorChave(documento.OID_ARQUIVO_UPLOAD);
                arquivoUploadProxy.Deletar(arquivoUpload);

                //var webRootPath = HostingEnvironment.WebRootPath;
                var arquivo = Path.Combine(BaseUploadController.DiretorioUpload, arquivoUpload.NOM_ARQUIVO_LOCAL);

                System.IO.File.Delete(arquivo);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize("Bearer")]
        [Retorno("string")]
        public IActionResult CriarPasta([FromBody] DocumentoPastaEntidade pasta)
        {
            try
            {
                pasta.DTA_INCLUSAO = DateTime.Now;
                new DocumentoPastaProxy().Inserir(pasta);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{OID_DOCUMENTO_PASTA}")]
        [Authorize("Bearer")]
        [Retorno("string")]
        public IActionResult DeletarPasta(decimal OID_DOCUMENTO_PASTA)
        {
            try
            {
                DeletarPastaRecursivo(OID_DOCUMENTO_PASTA);

                var documentoProxy = new DocumentoProxy();
                var documentoPastaProxy = new DocumentoPastaProxy();

                var pasta = documentoPastaProxy.BuscarPorChave(OID_DOCUMENTO_PASTA);

                documentoPastaProxy.Deletar(pasta);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{OID_DOCUMENTO}")]
        [Authorize("Bearer")]
        [Retorno("any", tipoResposta: TipoResposta.Blob)]
        public IActionResult Download(decimal OID_DOCUMENTO)
        {
            try
            {
                var documento = new DocumentoProxy().BuscarPorChave(OID_DOCUMENTO);
                var arquivoUpload = new ArquivoUploadProxy().BuscarPorChave(documento.OID_ARQUIVO_UPLOAD);

                var caminhoArquivo = Path.Combine(arquivoUpload.NOM_DIRETORIO_LOCAL, arquivoUpload.NOM_ARQUIVO_LOCAL);

                if (!System.IO.File.Exists(caminhoArquivo))
                    throw new Exception("Arquivo não encontrado!");

                var arquivo = new FileInfo(caminhoArquivo);
                var file = System.IO.File.OpenRead(caminhoArquivo);
                var mimeType = MimeTypes.GetMimeType(arquivo.Name);

                return File(file, mimeType, arquivoUpload.NOM_ARQUIVO_LOCAL);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("enviarDocumento/{OID_DOCUMENTO}")]
        [Authorize("Bearer")]
        public IActionResult EnviarDocumento(decimal OID_DOCUMENTO)
        {
            try
            {
                var dados = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
                var documento = new DocumentoProxy().BuscarPorChave(OID_DOCUMENTO);
                var arquivoUpload = new ArquivoUploadProxy().BuscarPorChave(documento.OID_ARQUIVO_UPLOAD);

                var caminhoArquivo = Path.Combine(arquivoUpload.NOM_DIRETORIO_LOCAL, arquivoUpload.NOM_ARQUIVO_LOCAL);

                //var arquivo = new System.IO.FileInfo(caminhoArquivo);
                string fileName = caminhoArquivo.ToString();
                string[] nomeArquivo = fileName.Split('\\');
                fileName = nomeArquivo[nomeArquivo.Length - 1];

                //var file = System.IO.File.ReadAllBytes(caminhoArquivo);

                var arquivoStream = System.IO.File.Open(caminhoArquivo, FileMode.Open);
                var ms = new MemoryStream();
                int arquivoBytes;
                var buffer = new byte[4096];
                do
                {
                    arquivoBytes = arquivoStream.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, arquivoBytes);
                } while (arquivoBytes > 0);

                ms.Position = 0;
                arquivoStream.Close();

                var emailConfig = AppSettings.Get().Email;

                var Anexo = new KeyValuePair<string, Stream>(fileName, ms);

                EnvioEmail.Enviar(emailConfig, dados.EMAIL_AUX, "Preves", "", Anexo);

                return Ok($"Documento enviado com sucesso para o e-mail {dados.EMAIL_AUX}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize("Bearer")]
        public IActionResult UploadFile([FromForm] FileUploadEntidade model)
        {
            try
            {
                var file = model.File;

                if (!Directory.Exists(BaseUploadController.DiretorioUpload))
                    Directory.CreateDirectory(BaseUploadController.DiretorioUpload);

                long oidArquivoUpload = 0;

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(BaseUploadController.DiretorioUpload, fileName);
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

                return Ok(oidArquivoUpload);
            }
            catch (Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }

        private void DeletarPastaRecursivo(decimal OID_DOCUMENTO_PASTA)
        {
            var documentoProxy = new DocumentoProxy();
            var documentoPastaProxy = new DocumentoPastaProxy();
            var arquivoUploadProxy = new ArquivoUploadProxy();

            // Deleta documentos dentro da pasta
            var documentos = documentoProxy.BuscarPorPasta(OID_DOCUMENTO_PASTA);

            foreach (var documento in documentos)
            {
                documentoProxy.Deletar(documento);

                var arquivoUpload = arquivoUploadProxy.BuscarPorChave(documento.OID_ARQUIVO_UPLOAD);
                arquivoUploadProxy.Deletar(arquivoUpload);

                var arquivo = System.IO.Path.Combine(BaseUploadController.DiretorioUpload, arquivoUpload.NOM_ARQUIVO_LOCAL);

                System.IO.File.Delete(arquivo);
            }

            // Deleta pastas dentro da pasta
            var pastas = documentoPastaProxy.BuscarPorPastaPai(OID_DOCUMENTO_PASTA);

            foreach (var pastaItem in pastas)
            {
                DeletarPastaRecursivo(pastaItem.OID_DOCUMENTO_PASTA);
                documentoPastaProxy.Deletar(pastaItem);
            }
        }
    }
}