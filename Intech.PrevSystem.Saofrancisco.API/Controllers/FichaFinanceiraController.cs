﻿#region Usings
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
                var cdTipoContrib = "31";

                var plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(CdFundacao, CdEmpresa, Matricula, cdPlano);

                if (plano.CD_CATEGORIA == "3")
                    cdTipoContrib = "60";

                var contribsBasicas = new FichaFechamentoProxy().BuscarUltimaPorFundacaoEmpresaPlanoInscricao(CdFundacao, CdEmpresa, cdPlano, Inscricao);
                var contribs = new FichaFinanceiraProxy().BuscarUltimoFechamentoPorFundacaoPlanoInscricao(CdFundacao, cdPlano, Inscricao).ToList();

                var contribsIndividuais = new ContribuicaoIndividualProxy().BuscarPorFundacaoPlanoInscricaoTipo(CdFundacao, cdPlano, Inscricao, cdTipoContrib);

                if (contribsBasicas == null || contribs.Count == 0 || contribsIndividuais == null)
                    return BadRequest("Não foi possível buscar sua ultima contribuição.");

                var listaContribs = new List<Tuple<string, decimal>>
                {
                    new Tuple<string, decimal>("Contribuição Participante", contribsBasicas.VL_GRUPO1),
                    new Tuple<string, decimal>("Contribuição Patrocinadora", contribsBasicas.VL_GRUPO2)
                };
                listaContribs.Add(new Tuple<string, decimal>("Total", listaContribs.Sum(x => x.Item2)));

                // Buscar custeios
                var listaDescontos = new FichaFinanceiraProxy().BuscarResumoCusteio(CdFundacao, Inscricao, cdPlano).ToList();
                listaDescontos.Add(new FichaFinanceiraEntidade
                {
                    CONTRIB_PARTICIPANTE = listaDescontos.Sum(x => x.CONTRIB_PARTICIPANTE),
                    DS_AGRUPADOR_WEB = "Total"
                });

                return Json(new
                {
                    DataReferencia = $"01/{contribsBasicas.MES_REF}/{contribsBasicas.ANO_REF}",
                    contribs.First().SRC,
                    Percentual = contribsIndividuais.VL_PERC_PAR,
                    Contribuicoes = listaContribs,
                    Descontos = listaDescontos,
                    Liquido = listaContribs.Last().Item2 - listaDescontos.Last().CONTRIB_PARTICIPANTE
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}