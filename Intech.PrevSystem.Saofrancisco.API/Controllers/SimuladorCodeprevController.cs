using Intech.Lib.Util.Date;
using Intech.PrevSystem.API;
using Intech.PrevSystem.Negocio.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
                var percentualPatronal = Math.Min(dados.PercentualContrib, 8);
                var contribMensal = dados.SalarioContribuicao / 100 * dados.PercentualContrib;
                var contribPatronal = dados.SalarioContribuicao / 100 * percentualPatronal;
                var contribBrutaTotal = contribMensal + contribPatronal;

                var indiceTaxaAdm = new IndiceProxy().BuscarUltimoPorCodigo("TXADMCD");
                var indiceTaxaRisco = new IndiceProxy().BuscarUltimoPorCodigo("TXRISCOCD");

                var taxaAdm = contribBrutaTotal * indiceTaxaAdm.VALORES.First().VALOR_IND / 100;
                var taxaRisco = contribBrutaTotal * indiceTaxaRisco.VALORES.First().VALOR_IND / 100;

                var dadosPessoais = new DadosPessoaisProxy().BuscarPorCodEntid(CodEntid);
                var dataAposentadoria = dadosPessoais.DT_NASCIMENTO.AddYears(dados.IdadeAposentadoria);

                var saldoProjetado = dados.SaldoAcumulado;

                for(var data = DateTime.Today; data <= dataAposentadoria; data = data.AddMonths(1))
                {
                    var valor = contribBrutaTotal - taxaAdm;
                    var idadeNaData = new Intervalo(data, dadosPessoais.DT_NASCIMENTO).Anos;

                    if (idadeNaData < 58)
                        valor -= taxaRisco;

                    saldoProjetado += valor;
                }

                var saque = saldoProjetado / (dados.PercentualSaque * 100);

                return Ok(new
                {
                    SaldoProjetado = saldoProjetado,
                    Saque = saque
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class DadosSimulacao
    {
        public int IdadeAposentadoria { get; set; }
        public decimal PercentualContrib { get; set; }
        public decimal PercentualSaque { get; set; }
        public decimal Aporte { get; set; }
        public decimal SaldoAcumulado { get; set; }
        public decimal SalarioContribuicao { get; set; }
    }
}