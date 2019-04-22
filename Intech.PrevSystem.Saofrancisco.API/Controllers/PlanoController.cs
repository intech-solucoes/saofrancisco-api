#region Usings
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.IO;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.Plano)]
    public class PlanoController : BasePlanoController
    {
        [HttpGet("relatorioExtratoPorPlanoReferencia/{cdPlano}/{dtInicio}/{dtFim}")]
        [Authorize("Bearer")]
        public IActionResult GetRelatorioExtratoPorPlanoReferencia(string cdPlano, string dtInicio, string dtFim)
        {
            try
            {
                var funcionario = new FuncionarioProxy().BuscarPorCodEntid(CodEntid);

                var dataInicio = DateTime.ParseExact(dtInicio, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                var dataFim = DateTime.ParseExact(dtFim, "dd.MM.yyyy", new CultureInfo("pt-BR"));

                var relatorio = new Relatorios.RelatorioExtratoContribuicao();
                relatorio.GerarRelatorio(funcionario.CD_FUNDACAO, funcionario.CD_EMPRESA, cdPlano, Matricula, dataInicio, dataFim);

                using (MemoryStream ms = new MemoryStream())
                {
                    relatorio.ExportToPdf(ms);

                    // Clona stream pois o método ExportToPdf fecha a atual
                    var pdfStream = new MemoryStream();
                    pdfStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    pdfStream.Position = 0;

                    var filename = $"ExtratoContribuicoes_{Guid.NewGuid().ToString()}.pdf";

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