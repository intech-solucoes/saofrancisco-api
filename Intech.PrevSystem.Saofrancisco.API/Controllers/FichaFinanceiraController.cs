#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route(RotasApi.FichaFinanceira)]
    public class FichaFinanceiraController : BaseFichaFinanceiraController
    {
        [HttpGet("saoFranciscoSaldoPorPlano/{cdPlano}")]
        [Authorize("Bearer")]
        public IActionResult BuscarSaoFranciscoSaldoPorFundacaoEmpresaPlanoFundo(string cdPlano)
        {
            try
            {
                if (cdPlano == "0002") // Se for plano de benefícios II
                {
                    var saldo1 = new FichaFinanceiraProxy().BuscarSaldoPorFundacaoEmpresaPlanoInscricaoFundo(CdFundacao, CdEmpresa, cdPlano, Inscricao, "3");
                    var saldo2 = new FichaFinanceiraProxy().BuscarSaldoPorFundacaoEmpresaPlanoInscricaoFundo(CdFundacao, CdEmpresa, cdPlano, Inscricao, "4");
                    var saldo3 = new FichaFinanceiraProxy().BuscarSaldoPorFundacaoEmpresaPlanoInscricaoFundo(CdFundacao, CdEmpresa, cdPlano, Inscricao, "7");

                    return Json(new SaldoContribuicoesEntidade
                    {
                        QuantidadeCotasParticipante = saldo1.QuantidadeCotasParticipante + saldo2.QuantidadeCotasParticipante + saldo3.QuantidadeCotasParticipante,
                        QuantidadeCotasPatrocinadora = saldo1.QuantidadeCotasPatrocinadora + saldo2.QuantidadeCotasPatrocinadora + saldo3.QuantidadeCotasPatrocinadora,
                        ValorParticipante = saldo1.ValorParticipante + saldo2.ValorParticipante + saldo3.ValorParticipante,
                        ValorPatrocinadora = saldo1.ValorPatrocinadora + saldo2.ValorPatrocinadora + saldo3.ValorPatrocinadora,
                        DataReferencia = saldo1.DataReferencia,
                        DataCota = saldo1.DataCota,
                        ValorCota = saldo1.ValorCota
                    });
                }
                else
                {
                    var saldo = new FichaFinanceiraProxy().BuscarSaldoPorFundacaoEmpresaPlanoInscricaoFundo(CdFundacao, CdEmpresa, cdPlano, Inscricao, "6");
                    return Json(saldo);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ultimaExibicaoPorPlano/{cdPlano}")]
        [Authorize("Bearer")]
        public IActionResult GetUltimaExibicaoPorPlano(string cdPlano)
        {
            try
            {
                var contribs = new FichaFinanceiraProxy().BuscarUltimaPorFundacaoPlanoInscricao(CdFundacao, cdPlano, Inscricao);
                var contribsIndividuais = new ContribuicaoIndividualProxy().BuscarPorFundacaoPlanoInscricaoTipo(CdFundacao, cdPlano, Inscricao, "31");

                var listaContribs = new List<Tuple<string, decimal>>
                {
                    new Tuple<string, decimal>("Contribuição Participante", contribs.Single(x => x.CD_TIPO_CONTRIBUICAO == "31").CONTRIB_PARTICIPANTE.Value),
                    new Tuple<string, decimal>("Contribuição Patrocinadora", contribs.Single(x => x.CD_TIPO_CONTRIBUICAO == "35").CONTRIB_EMPRESA.Value),
                    new Tuple<string, decimal>("Taxa Administrativa", contribs.Single(x => x.CD_TIPO_CONTRIBUICAO == "32").CONTRIB_PARTICIPANTE.Value * -1M)
                };

                listaContribs.Add(new Tuple<string, decimal>("Total", listaContribs.Sum(x => x.Item2)));

                return Json(new
                {
                    DataReferencia = contribs.First().DT_MOVIMENTO,
                    contribs.First().SRC,
                    Percentual = contribsIndividuais.VL_PERC_PAR,
                    Itens = listaContribs
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}