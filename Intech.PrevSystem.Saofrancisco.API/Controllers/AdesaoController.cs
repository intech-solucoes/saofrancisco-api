using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Entidades.Outros;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.Adesao)]
    [ApiController]
    public class AdesaoController : BaseController
    {
        [HttpGet("[action]")]
        [Authorize("Bearer")]
        public IActionResult Download()
        {
            try
            {
                var nomeArquivo = "FormularioAdesao.pdf";
                var caminhoArquivo = Path.Combine("Formularios", nomeArquivo);

                if (!System.IO.File.Exists(caminhoArquivo))
                    throw new Exception("Arquivo não encontrado!");

                var arquivo = new System.IO.FileInfo(caminhoArquivo);
                var file = System.IO.File.OpenRead(caminhoArquivo);
                var mimeType = MimeTypes.GetMimeType(arquivo.Name);

                return File(file, mimeType, nomeArquivo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize("Bearer")]
        public IActionResult Upload([FromServices] IConfiguration config, [FromForm] FileUploadEntidade model)
        {
            try
            {
                var urlCloudDocs = config.GetSection("UrlCloudDocs").Value;

                var file = model.File;

                if (!Directory.Exists(BaseUploadController.DiretorioUpload))
                    Directory.CreateDirectory(BaseUploadController.DiretorioUpload);

                long oidArquivoUpload = 0;

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = $"{Guid.NewGuid()}_{fileName}";
                    string fullPath = Path.Combine(BaseUploadController.DiretorioUpload, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        var base64 = stream.ConvertToBase64();

                        var dados = new FuncionarioNPProxy().BuscarDadosNaoParticipantePorMatriculaEmpresa(Matricula, CdEmpresa);
                        var resultado = EnviarJson(dados, base64, urlCloudDocs);

                        if (resultado.Count > 0 && resultado[0].ERRO != null)
                            throw new Exception(resultado[0].ERRO.ToString());
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

        public static dynamic EnviarJson(FuncionarioNPDados dadosNP, string base64, string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/json; charset=utf-8";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var dados = new List<CloudDocsItem>
                    {
                        new CloudDocsItem
                        {
                            Acao = "CRIARFOLDER",
                            Cliente = "427944587a774f2b7763303d",
                            TipoFolder = "10018",
                            Origem = "10172",
                            Indice01 = dadosNP.Funcionario.NOME_ENTID,
                            Indice02 = dadosNP.CPF,
                            Indice03 = dadosNP.Funcionario.DT_NASCIMENTO.Value.ToString("dd/MM/yyyy"),
                            Observacao = "",
                            Documento = base64,
                            Extensao = ".PDF",
                            Classificacao = "12051",
                            Complemento = "",
                            Evento = ""
                        }
                    };
                    ;

                    var json = JsonConvert.SerializeObject(dados, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

                    streamWriter.Write(json);
                }

                var response = (HttpWebResponse)request.GetResponse();

                string resultado = null;

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                    resultado = streamReader.ReadToEnd();

                response.Close();

                return JsonConvert.DeserializeObject<dynamic>(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao acessar API da CloudDocs. url: {url}", ex);
            }
        }
    }

    public class CloudDocsItem
    {
        public string Acao { get; set; }
        public string Cliente { get; set; }
        public string TipoFolder { get; set; }
        public string Origem { get; set; }
        public string Indice01 { get; set; }
        public string Indice02 { get; set; }
        public string Indice03 { get; set; }
        public string Indice04 { get; set; }
        public string Indice05 { get; set; }
        public string Indice06 { get; set; }
        public string Indice07 { get; set; }
        public string Indice08 { get; set; }
        public string Indice09 { get; set; }
        public string Indice10 { get; set; }
        public string Observacao { get; set; }
        public string Documento { get; set; }
        public string Extensao { get; set; }
        public string Classificacao { get; set; }
        public string Complemento { get; set; }
        public string Evento { get; set; }
    }
}
