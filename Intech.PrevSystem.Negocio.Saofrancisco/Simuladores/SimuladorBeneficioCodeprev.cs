using Intech.Lib.Util.Date;
using Intech.PrevSystem.Negocio.Proxy;
using System;
using System.Linq;

namespace Intech.PrevSystem.Negocio.Saofrancisco.Simuladores
{
    public static class SimuladorBeneficioCodeprev : BaseSimulador
    {
        public static List<KeyValuePair<string, string>> Calcular(SimuladorBeneficioCodeprevDados dados, string codEntid, decimal percentualContrib, out decimal saldoProjetado, out decimal saque, out decimal saldoBeneficio)
        {
            MemoriaCalculo = new List<KeyValuePair<string, string>>();

            MemoriaCalculo.Add("Percentual Patronal", dados.SalarioContribuicao.ToString("C"));

            var percentualPatronal = Math.Min(percentualContrib, 8);
            MemoriaCalculo.Add("Percentual Patronal", percentualPatronal.ToString());
            MemoriaCalculo.Add("Percentual Participante", percentualContrib.ToString());

            var contribMensal = dados.SalarioContribuicao / 100 * percentualContrib;
            MemoriaCalculo.Add("Contribuição Mensal", $"{dados.SalarioContribuicao} / 100 * {percentualContrib} = {contribMensal}");

            var contribPatronal = dados.SalarioContribuicao / 100 * percentualPatronal;
            MemoriaCalculo.Add("Contribuição Patronal", $"{dados.SalarioContribuicao} / 100 * {percentualContrib} = {contribPatronal}");

            var contribBrutaTotal = contribMensal + contribPatronal;
            MemoriaCalculo.Add("Contribuição Total", $"{contribMensal} + {contribPatronal} = {contribBrutaTotal}");

            var indiceTaxaAdm = new IndiceProxy().BuscarUltimoPorCodigo("TXADMCD");
            var taxaAdm = contribBrutaTotal * indiceTaxaAdm.VALORES.First().VALOR_IND / 100;
            MemoriaCalculo.Add("Taxa Adm", $"{taxaAdm}");

            var indiceTaxaRisco = new IndiceProxy().BuscarUltimoPorCodigo("TXRISCOCD");
            var taxaRisco = contribBrutaTotal * indiceTaxaRisco.VALORES.First().VALOR_IND / 100;
            MemoriaCalculo.Add("Taxa Risco", $"{taxaRisco}");

            var dadosPessoais = new DadosPessoaisProxy().BuscarPorCodEntid(codEntid);
            var dataAposentadoria = dadosPessoais.DT_NASCIMENTO.AddYears(dados.IdadeAposentadoria);

            MemoriaCalculo.Add("Data de Aposentadoria", $"{dataAposentadoria.ToString("dd/MM/yyyy")}");
            MemoriaCalculo.Add("Idade na Aposentadoria", $"{dados.IdadeAposentadoria}");

            MemoriaCalculo.Add("Saldo Acumulado", $"{dados.SaldoAcumulado.ToString("C")}");

            saldoProjetado = dados.SaldoAcumulado;
            for (var data = DateTime.Today; data <= dataAposentadoria; data = data.AddMonths(1))
            {
                var valor = contribBrutaTotal - taxaAdm;
                var idadeNaData = new Intervalo(data, dadosPessoais.DT_NASCIMENTO).Anos;

                if (idadeNaData < 58)
                    valor -= taxaRisco;
                var saldoProjetadoAnterior = saldoProjetado;
                saldoProjetado += valor;
                MemoriaCalculo.Add($"Saldo em {data.ToString("dd/MM/yyyy")}", $"{valor} - {taxaRisco} = {valor.ToString("C")} + {saldoProjetadoAnterior.ToString("C")} = {saldoProjetado.ToString("C")}");
            }
            MemoriaCalculo.Add("Saldo Projetado", $"{saldoProjetado.ToString("C")}");

            saque = 0M;
            if (dados.PercentualSaque > 0)
                saque = saldoProjetado * (dados.PercentualSaque / 100);

            MemoriaCalculo.Add("Percentual de Saque a Vista", $"{dados.PercentualSaque}");
            MemoriaCalculo.Add("Valor Saque a Vista", $"{saldoProjetado} * {dados.PercentualSaque / 100} = {saque.ToString("C")}");

            saldoBeneficio = saldoProjetado - saque;
            MemoriaCalculo.Add("Saldo do Benefício", $"{saldoProjetado} - {saque} = {saldoBeneficio.ToString("C")}");

            return MemoriaCalculo;
        }
    }
}
