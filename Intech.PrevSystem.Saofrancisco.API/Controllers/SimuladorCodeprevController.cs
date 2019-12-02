using Intech.Lib.Util.Date;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intech.PrevSystem.Saofrancisco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimuladorCodeprevController : BaseController
    {
        [HttpPost("[action]")]
        [Authorize("Bearer")]
        public IActionResult Simular(DadosSimulacao dados)
        {
            try
            {
                decimal saldoProjetado, saque, saldoBeneficio,
                    saldoProjetado8 = 0M, saque8 = 0M, saldoBeneficio8 = 0M;
                Calcular(dados, dados.PercentualContrib, out saldoProjetado, out saque, out saldoBeneficio);

                if (dados.PercentualContrib < 8)
                {
                    Calcular(dados, 8, out saldoProjetado8, out saque8, out saldoBeneficio8);
                }

                var listaRendaMensal = new List<RendaMensalItem>();

                for (var i = 0.001M; i <= 0.015M; i = i + 0.001M)
                {
                    var rendaMensal = saldoBeneficio * i;
                    var tempoRecebimento = saldoBeneficio / rendaMensal / 13;
                    var anosCompletos = Math.Floor(tempoRecebimento);
                    var meses = Math.Floor((tempoRecebimento - anosCompletos) * 12);

                    var rendaMensalItem = new RendaMensalItem
                    {
                        Percentual = i * 100,
                        Renda = rendaMensal,
                        StringTempoRecebimento = $"{anosCompletos} anos {meses} meses"
                    };

                    if(dados.PercentualContrib < 8)
                    {
                        var rendaMensal8 = saldoBeneficio8 * i;
                        rendaMensalItem.Renda8 = rendaMensal8;
                    }

                    listaRendaMensal.Add(rendaMensalItem);
                }

                var retorno = new
                {
                    SaldoProjetado = saldoProjetado,
                    SaldoProjetado8 = dados.PercentualContrib < 8 ? saldoProjetado8 : 0M,
                    Saque = saque,
                    Saque8 = dados.PercentualContrib < 8 ? saque8 : 0M,
                    RendaMensal = listaRendaMensal
                };

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void Calcular(DadosSimulacao dados, decimal percentualContrib, out decimal saldoProjetado, out decimal saque, out decimal saldoBeneficio)
        {
            var percentualPatronal = Math.Min(percentualContrib, 8);
            var contribMensal = dados.SalarioContribuicao / 100 * percentualContrib;
            var contribPatronal = dados.SalarioContribuicao / 100 * percentualPatronal;
            var contribBrutaTotal = contribMensal + contribPatronal;

            var indiceTaxaAdm = new IndiceProxy().BuscarUltimoPorCodigo("TXADMCD");
            var indiceTaxaRisco = new IndiceProxy().BuscarUltimoPorCodigo("TXRISCOCD");

            var taxaAdm = contribBrutaTotal * indiceTaxaAdm.VALORES.First().VALOR_IND / 100;
            var taxaRisco = contribBrutaTotal * indiceTaxaRisco.VALORES.First().VALOR_IND / 100;

            var dadosPessoais = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
            var dataAposentadoria = dadosPessoais.DT_NASCIMENTO.AddYears(dados.IdadeAposentadoria);

            saldoProjetado = dados.SaldoAcumulado;
            for (var data = DateTime.Today; data <= dataAposentadoria; data = data.AddMonths(1))
            {
                var valor = contribBrutaTotal - taxaAdm;
                var idadeNaData = new Intervalo(data, dadosPessoais.DT_NASCIMENTO).Anos;

                if (idadeNaData < 58)
                    valor -= taxaRisco;

                saldoProjetado += valor;
            }

            saque = 0M;
            if (dados.PercentualSaque > 0)
                saque = saldoProjetado * (dados.PercentualSaque / 100);

            saldoBeneficio = saldoProjetado - saque;
        }
    }

    public class DadosSimulacao
    {
        public int IdadeAposentadoria { get; set; }
        public decimal PercentualContrib { get; set; }
        public decimal PercentualSaque { get; set; }
        public decimal Aporte { get; set; }
        public decimal SaldoAcumulado { get; set; }
        public decimal SaldoAcumulado8 { get; set; }
        public decimal SalarioContribuicao { get; set; }
    }

    public class RendaMensalItem
    {
        public decimal Percentual { get; set; }
        public decimal Renda { get; set; }
        public decimal Renda8 { get; set; }
        public string StringTempoRecebimento {get;set;}
    }
}