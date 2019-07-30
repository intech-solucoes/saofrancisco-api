﻿#region Usings
using Intech.Lib.Email;
using Intech.Lib.Web;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
                var informe = new HeaderInfoRendProxy().BuscarPorCpfReferencia(Cpf, referencia);

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
                        EnvioEmail.EnviarMailKit(emailConfig, dados.EMAIL_AUX, "Informe de Rendimentos", "", pdfStream, filename);

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