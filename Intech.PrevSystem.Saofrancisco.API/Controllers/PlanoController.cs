#region Usings
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using Intech.Lib.Email;
using Intech.Lib.Web;
using Intech.Lib.Web.API;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
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
    /// <service nome="Plano" />
    [Route(RotasApi.Plano)]
    public class PlanoController : BasePlanoController
    {
        private IHostingEnvironment HostingEnvironment;

        public PlanoController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        /// <rota caminho="[action]" tipo="GET" />
        /// <retorno tipo="PlanoVinculadoEntidade" lista="true" />
        [HttpGet("[action]")]
        [Retorno(nameof(PlanoVinculadoEntidade), true)]
        [Authorize("Bearer")]
        public IActionResult Buscar()
        {
            try
            {
                return Ok(new PlanoVinculadoProxy().BuscarPorFundacaoInscricao(CdFundacao, Inscricao));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <rota caminho="[action]" tipo="GET" />
        /// <retorno tipo="PlanoEntidade" lista="true" />
        [HttpGet("[action]")]
        [Retorno(nameof(PlanoEntidade), true)]
        [Authorize("Bearer")]
        public IActionResult BuscarTodos()
        {
            try
            {
                return Ok(new PlanoProxy().BuscarTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("saldado")]
        [Authorize("Bearer")]
        public IActionResult GetSaldado()
        {
            try
            {
                return Json(new PlanoVinculadoProxy().FSFBuscarSaldado(CdFundacao, "0003", Inscricao).First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        [Authorize("Bearer")]
        public IActionResult ExtratoCodeprev()
        {
            try
            {
                var cdPlano = "0002";

                var dataInicial = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano).DT_INSC_PLANO;
                var dataFinal = new FichaFechamentoProxy().BuscarDataUltimaContrib(CdFundacao, CdEmpresa, cdPlano, Inscricao);

                string anoRefMesRefInicio = dataInicial.ToString("yyyyMM");
                string anoRefMesRefFim = dataFinal.ToString("yyyyMM");

                var ficha = new FichaFechamentoProxy().BuscarRelatorioPorFundacaoEmpresaPlanoInscricaoReferencia(CdFundacao, CdEmpresa, cdPlano, Inscricao, anoRefMesRefInicio, anoRefMesRefFim);

                var resultado = new
                {
                    Ultimofechamento = dataFinal.ToString("MM/yyyy"),
                    QuantidadeCotas = ficha.Sum(x => x.QTE_COTA).ToString("N6"),
                    ValorAcumulado = ficha.Last().VL_ACUMULADO,
                    CotaConversao = ficha.Last().VL_COTA.ToString("N6")
                };

                return Json(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        [Authorize("Bearer")]
        public IActionResult ExtratoSaldado()
        {
            try
            {
                var cdPlano = "0003";

                var funcionario = new FuncionarioProxy().BuscarDadosPorCodEntid(CodEntid);
                var fundacao = new FundacaoProxy().BuscarPorCodigo(CdFundacao);
                var plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano);
                
                var listaFicha = new FichaFinanceiraProxy().BuscarPorFundacaoPlanoInscricao(CdFundacao, cdPlano, Inscricao).ToList();

                var ultimaFicha = listaFicha.Last();
                var dataInicial = new DateTime(Convert.ToInt32(ultimaFicha.ANO_REF), Convert.ToInt32(ultimaFicha.MES_REF), 1);

                var primeiraFicha = listaFicha.First();
                var dataFinal = new DateTime(Convert.ToInt32(primeiraFicha.ANO_REF), Convert.ToInt32(primeiraFicha.MES_REF), 1);

                string anoRefMesRefInicio = dataInicial.ToString("yyyyMM");
                string anoRefMesRefFim = dataFinal.ToString("yyyyMM");

                var ficha = new FichaFinanceiraProxy().BuscarExtratoTipoResgate(CdFundacao, cdPlano, Inscricao, anoRefMesRefInicio, anoRefMesRefFim, "01");
                var listaCompetenciasAPular = new string[] { "201710", "201711", "201712", "201713" };
                ficha = ficha.Where(x => !listaCompetenciasAPular.Contains(x.ANO_COMP + x.MES_COMP)).ToList();

                var listaFichaSaldo = new FichaFinanceiraProxy().BuscarPorFundacaoPlanoInscricaoTipoResgate(CdFundacao, cdPlano, Inscricao, "01");

                var qntCotaFD = 0M;
                var qntCotaRP = 0M;
                foreach (var fichaSaldo in listaFichaSaldo)
                {
                    if (fichaSaldo.CD_OPERACAO == "D")
                    {
                        qntCotaFD += fichaSaldo.QTD_COTA_FD_PARTICIPANTE.Value * -1;
                        qntCotaRP += fichaSaldo.QTD_COTA_RP_PARTICIPANTE.Value * -1;
                    }
                    else
                    {
                        qntCotaFD += fichaSaldo.QTD_COTA_FD_PARTICIPANTE.Value;
                        qntCotaRP += fichaSaldo.QTD_COTA_RP_PARTICIPANTE.Value;
                    }
                }

                var empresaPlano = new EmpresaPlanosProxy().BuscarPorFundacaoEmpresaPlano(CdFundacao, CdEmpresa, cdPlano);

                var indicesFD = new IndiceValoresProxy().BuscarPorCodigo(empresaPlano.IND_FUNDO).ToList();
                var indicesRP = new IndiceValoresProxy().BuscarPorCodigo(empresaPlano.IND_RESERVA_POUP).ToList();

                var ultimoIndiceFD = indicesFD[0];
                var ultimoIndiceRP = indicesRP[0];
                var indiceAtrasadoFD = indicesFD[1];
                var indiceAtrasadoRP = indicesRP[1];

                var valorFD = qntCotaFD * indiceAtrasadoFD.VALOR_IND;
                var valorRP = qntCotaRP * indiceAtrasadoRP.VALOR_IND;

                var calculoRP = true;
                var saldoAtualizado = 0M;

                if (valorRP > valorFD)
                {
                    saldoAtualizado = valorRP;
                }
                else
                {
                    saldoAtualizado = valorFD;
                    calculoRP = false;
                }

                var qntCotasTotal = 0M;
                var valorBruto = 0M;
                DateTime dataConversao;

                if (calculoRP)
                {
                    qntCotasTotal = new FichaFinanceiraProxy().FSF_BuscarCotasSaldado(CdFundacao, cdPlano, Inscricao);
                    valorBruto = qntCotasTotal * indiceAtrasadoRP.VALOR_IND;
                    dataConversao = ultimoIndiceRP.DT_IND;
                }
                else
                {
                    if (plano.DT_INSC_PLANO < new DateTime(1998, 12, 3))
                    {
                        qntCotasTotal = new FichaFinanceiraProxy().FSF_BuscarCotasSaldadoFDApos98(CdFundacao, cdPlano, Inscricao);
                        valorBruto = qntCotasTotal * indiceAtrasadoFD.VALOR_IND;
                    }
                    else
                    {
                        var percentual = new ValoresPercIdadeProxy().BuscarPercentual(CdFundacao, cdPlano, Inscricao);
                        qntCotasTotal = new FichaFinanceiraProxy().FSF_BuscarCotasSaldadoFDAntes98(CdFundacao, cdPlano, Inscricao, percentual);
                        valorBruto = qntCotasTotal * indiceAtrasadoFD.VALOR_IND;
                    }

                    dataConversao = ultimoIndiceFD.DT_IND;
                }

                return Json(new
                {
                    Ficha = ficha,
                    ValorBruto = valorBruto,
                    DataConversao = dataConversao,
                    SaldoAtualizado = saldoAtualizado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("relatorioExtratoPorPlanoReferencia/{cdPlano}/{dtInicio}/{dtFim}/{enviarPorEmail}")]
        [Authorize("Bearer")]
        public IActionResult GetRelatorioExtratoPorPlanoReferencia(string cdPlano, string dtInicio, string dtFim, bool enviarPorEmail)
        {
            try
            {
                var nomeArquivoRepx = "ExtratoContribuicoes";
                if (cdPlano == "0003")
                    nomeArquivoRepx = "ExtratoSaldado";

                var relatorio = XtraReport.FromFile($"Relatorios/{nomeArquivoRepx}.repx");

                DateTime dataInicio;
                DateTime dataFim;

                try
                {
                    dataInicio = DateTime.ParseExact(dtInicio, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                } catch
                {
                    throw new Exception("Data Início inválida!");
                }

                try
                {
                    dataFim = DateTime.ParseExact(dtFim, "dd.MM.yyyy", new CultureInfo("pt-BR"));
                }
                catch
                {
                    throw new Exception("Data Fim inválida!");
                }

                string AnoRefMesRefInicio = dataInicio.ToString("yyyyMM");
                string AnoRefMesRefFim = dataFim.ToString("yyyyMM");

                var funcionario = new FuncionarioProxy().BuscarDadosPorCodEntid(CodEntid);
                var fundacao = new FundacaoProxy().BuscarPorCodigo(CdFundacao);
                var plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano);

                fundacao.CEP_ENTID = fundacao.CEP_ENTID.AplicarMascara(Mascaras.CEP);
                fundacao.CPF_CGC = fundacao.CPF_CGC.AplicarMascara(Mascaras.CNPJ);

                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "funcionario").Value = funcionario;
                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "fundacao").Value = fundacao;
                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "plano").Value = plano;
                ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "periodo").Value = $"{dataInicio.ToString("MM/yyyy")} a {dataFim.ToString("MM/yyyy")}";
                
                if (cdPlano == "0003")
                {
                    var ficha = new FichaFinanceiraProxy().BuscarExtratoTipoResgate(CdFundacao, cdPlano, Inscricao, dataInicio.ToString("yyyyMM"), dataFim.ToString("yyyyMM"), "01");
                    var listaCompetenciasAPular = new string[] { "201710", "201711", "201712", "201713" };
                    ficha = ficha.Where(x => !listaCompetenciasAPular.Contains(x.ANO_COMP + x.MES_COMP)).ToList();
                    ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "ficha").Value = ficha;

                    var listaFichaSaldo = new FichaFinanceiraProxy().BuscarPorFundacaoPlanoInscricaoTipoResgate(CdFundacao, cdPlano, Inscricao, "01");

                    var qntCotaFD = 0M;
                    var qntCotaRP = 0M;
                    foreach(var fichaSaldo in listaFichaSaldo)
                    {
                        if (fichaSaldo.CD_OPERACAO == "D")
                        {
                            qntCotaFD += fichaSaldo.QTD_COTA_FD_PARTICIPANTE.Value * -1;
                            qntCotaRP += fichaSaldo.QTD_COTA_RP_PARTICIPANTE.Value * -1;
                        }
                        else
                        {
                            qntCotaFD += fichaSaldo.QTD_COTA_FD_PARTICIPANTE.Value;
                            qntCotaRP += fichaSaldo.QTD_COTA_RP_PARTICIPANTE.Value;
                        }
                    }

                    var empresaPlano = new EmpresaPlanosProxy().BuscarPorFundacaoEmpresaPlano(CdFundacao, CdEmpresa, cdPlano);

                    var indicesFD = new IndiceValoresProxy().BuscarPorCodigo(empresaPlano.IND_FUNDO).ToList();
                    var indicesRP = new IndiceValoresProxy().BuscarPorCodigo(empresaPlano.IND_RESERVA_POUP).ToList();

                    var ultimoIndiceFD = indicesFD[0];
                    var ultimoIndiceRP = indicesRP[0];
                    var indiceAtrasadoFD = indicesFD[1];
                    var indiceAtrasadoRP = indicesRP[1];

                    var valorFD = qntCotaFD * indiceAtrasadoFD.VALOR_IND;
                    var valorRP = qntCotaRP * indiceAtrasadoRP.VALOR_IND;

                    var calculoRP = true;
                    var saldoAtualizado = 0M;

                    if (valorRP > valorFD) {
                        saldoAtualizado = valorRP;
                    } else
                    {
                        saldoAtualizado = valorFD;
                        calculoRP = false;
                    }

                    ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "saldoAtualizado").Value = saldoAtualizado;
                    ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "dataEmissao").Value = DateTime.Now;

                    var qntCotasTotal = 0M;
                    var valorBruto = 0M;
                    DateTime dataConversao;

                    if (calculoRP)
                    {
                        qntCotasTotal = new FichaFinanceiraProxy().FSF_BuscarCotasSaldado(CdFundacao, cdPlano, Inscricao);
                        valorBruto = qntCotasTotal * indiceAtrasadoRP.VALOR_IND;
                        dataConversao = ultimoIndiceRP.DT_IND;
                    }
                    else
                    {
                        if (plano.DT_INSC_PLANO < new DateTime(1998, 12, 3))
                        {
                            qntCotasTotal = new FichaFinanceiraProxy().FSF_BuscarCotasSaldadoFDApos98(CdFundacao, cdPlano, Inscricao);
                            valorBruto = qntCotasTotal * indiceAtrasadoFD.VALOR_IND;
                        }
                        else
                        {
                            var percentual = new ValoresPercIdadeProxy().BuscarPercentual(CdFundacao, cdPlano, Inscricao);
                            qntCotasTotal = new FichaFinanceiraProxy().FSF_BuscarCotasSaldadoFDAntes98(CdFundacao, cdPlano, Inscricao, percentual);
                            valorBruto = qntCotasTotal * indiceAtrasadoFD.VALOR_IND;
                        }

                        dataConversao = ultimoIndiceFD.DT_IND;
                    }

                    ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "valorBruto").Value = valorBruto;
                    ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "quantidadeCotas").Value = qntCotasTotal;
                    ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "dataConversao").Value = dataConversao;
                }
                else
                {
                    var ficha = new FichaFechamentoProxy().BuscarRelatorioPorFundacaoEmpresaPlanoInscricaoReferencia(CdFundacao, CdEmpresa, cdPlano, Inscricao, AnoRefMesRefInicio, AnoRefMesRefFim);
                    ((ObjectDataSource)relatorio.DataSource).Constructor.Parameters.First(x => x.Name == "ficha").Value = ficha;
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    relatorio.FillDataSource();
                    relatorio.ExportToPdf(ms);

                    // Clona stream pois o método ExportToPdf fecha a atual
                    var pdfStream = new MemoryStream();
                    pdfStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    pdfStream.Position = 0;

                    var filename = $"ExtratoContribuicoes_{Guid.NewGuid().ToString()}.pdf";

                    if (enviarPorEmail)
                    {
                        var dados = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
                        var emailConfig = AppSettings.Get().Email;
                        EnvioEmail.Enviar(emailConfig, dados.EMAIL_AUX, "Extrato de Contribuições", "", pdfStream, filename);

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