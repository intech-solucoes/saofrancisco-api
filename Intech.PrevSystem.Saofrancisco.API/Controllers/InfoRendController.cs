#region Usings
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
        [HttpGet("relatorio/{referencia}")]
        [Authorize("Bearer")]
        public IActionResult GetCertificado(decimal referencia)
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

                    return File(pdfStream, "application/pdf", filename);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}