using Intech.Lib.Util.Date;
using Intech.PrevSystem.Negocio.Proxy;
using System;
using System.Linq;

namespace Intech.PrevSystem.Negocio.Saofrancisco.Simuladores
{
    public static class SimuladorBeneficioCodeprev
    {

        public static void Calcular(SimuladorBeneficioCodeprevDados dados, string codEntid, decimal percentualContrib, out decimal saldoProjetado, out decimal saque, out decimal saldoBeneficio)
        {
            var percentualPatronal = Math.Min(percentualContrib, 8);
            var contribMensal = dados.SalarioContribuicao / 100 * percentualContrib;
            var contribPatronal = dados.SalarioContribuicao / 100 * percentualPatronal;
            var contribBrutaTotal = contribMensal + contribPatronal;

            var indiceTaxaAdm = new IndiceProxy().BuscarUltimoPorCodigo("TXADMCD");
            var indiceTaxaRisco = new IndiceProxy().BuscarUltimoPorCodigo("TXRISCOCD");

            var taxaAdm = contribBrutaTotal * indiceTaxaAdm.VALORES.First().VALOR_IND / 100;
            var taxaRisco = contribBrutaTotal * indiceTaxaRisco.VALORES.First().VALOR_IND / 100;

            var dadosPessoais = new DadosPessoaisProxy().BuscarPorCodEntid(codEntid);
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
}
