#region Usings
using Intech.Lib.Email;
using Intech.Lib.Web;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.InfoRend)]
    public class InfoRendController : BaseInfoRendController
    {
        [HttpGet("relatorio/{referencia}/{enviarPorEmail}")]
        [Authorize("Bearer")]
        public IActionResult GetCertificado(decimal referencia, bool enviarPorEmail)
        {
            try
            {
                HeaderInfoRendEntidade informe;
                if(Pensionista)
                    informe = new HeaderInfoRendProxy().BuscarPorEmpresaMatriculaReferencia(CdEmpresa, Matricula, referencia, SeqRecebedor);
                else
                    informe = new HeaderInfoRendProxy().BuscarPorEmpresaMatriculaReferencia(CdEmpresa, Matricula, referencia);

                var relatorio = new Relatorios.InformeRendimentos();
                relatorio.GerarRelatorio(informe);

                using (MemoryStream ms = new MemoryStream())
                {
                    relatorio.ExportToPdf(ms);

                    // Clona stream pois o método ExportToPdf fecha a atual
                    var pdfStream = new MemoryStream();
                    pdfStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    pdfStream.Position = 0;

                    var filename = $"Informe de Rendimentos - {informe.ANO_CALENDARIO}.pdf";

                    if (enviarPorEmail)
                    {
                        var dados = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
                        var emailConfig = AppSettings.Get().Email;
                        
                        var Anexo = new KeyValuePair<string, Stream>(filename, pdfStream);

                        EnvioEmail.Enviar(emailConfig, dados.EMAIL_AUX, "Informe de Rendimentos", "", Anexo);

                        return Json($"Extrato enviado com sucesso para o e-mail {dados.EMAIL_AUX}");
                    }
                    else
                    {
                        return File(pdfStream, "application/pdf", filename);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}