#region Usings
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.Plano)]
    public class PlanoController : BasePlanoController
    {
        private IHostingEnvironment HostingEnvironment;

        public PlanoController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        [HttpGet("saldado")]
        [Authorize("Bearer")]
        public IActionResult GetSaldado()
        {
            try
            {
                return Json(new PlanoVinculadoProxy().FSFBuscarSaldado(CdFundacao, "0003", Inscricao));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("relatorioExtratoPorPlanoReferencia/{cdPlano}/{dtInicio}/{dtFim}")]
        [Authorize("Bearer")]
        public IActionResult GetRelatorioExtratoPorPlanoReferencia(string cdPlano, string dtInicio, string dtFim)
        {
            try
            {
                var dataInicio = DateTime.ParseExact(dtInicio, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                var dataFim = DateTime.ParseExact(dtFim, "dd.MM.yyyy", new CultureInfo("pt-BR"));

                string AnoRefMesRefInicio = dataInicio.ToString("yyyyMM");
                string AnoRefMesRefFim = dataFim.ToString("yyyyMM");

                var funcionario = new FuncionarioProxy().BuscarDadosPorCodEntid(CodEntid);
                var fundacao = new FundacaoProxy().BuscarPorCodigo(CdFundacao);
                var plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano);
                var ficha = new FichaFechamentoProxy().BuscarRelatorioPorFundacaoEmpresaPlanoInscricaoReferencia(CdFundacao, CdEmpresa, cdPlano, Inscricao, AnoRefMesRefInicio, AnoRefMesRefFim);

                fundacao.CEP_ENTID = fundacao.CEP_ENTID.AplicarMascara(Mascaras.CEP);
                fundacao.CPF_CGC = fundacao.CPF_CGC.AplicarMascara(Mascaras.CNPJ);

                var nomeArquivoRepx = "ExtratoContribuicoes";
                var relatorio = XtraReport.FromFile($"Relatorios/{nomeArquivoRepx}.repx");

                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "funcionario").Value = funcionario;
                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "fundacao").Value = fundacao;
                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "plano").Value = plano;
                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "ficha").Value = ficha;
                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "periodo").Value = $"{dataInicio.ToString("MM/yyyy")} a {dataFim.ToString("MM/yyyy")}";

                using (MemoryStream ms = new MemoryStream())
                {
                    relatorio.FillDataSource();
                    relatorio.ExportToPdf(ms);

                    // Clona stream pois o método ExportToPdf fecha a atual
                    var pdfStream = new MemoryStream();
                    pdfStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    pdfStream.Position = 0;

                    var filename = $"ExtratoContribuicoes_{Guid.NewGuid().ToString()}.pdf";

                    return base.File(pdfStream, "application/pdf", filename);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}