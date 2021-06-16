using Intech.Lib.Util.Date;
using Intech.PrevSystem.Negocio.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intech.PrevSystem.Negocio.Saofrancisco.Simuladores
{
    public class SimuladorBeneficioCodeprev : BaseSimulador
    {
        public List<KeyValuePair<string, string>> Calcular(SimuladorBeneficioCodeprevDados dados, decimal percentualContrib, DateTime DataNascimento, string CdEmpresa, out decimal saldoProjetado, out decimal saque, out decimal saldoBeneficio)
        {
            MemoriaCalculo = new List<KeyValuePair<string, string>>();

            Add("Salário de Contribuição", dados.SalarioContribuicao.ToString("C"));

            var percentualPatronal = Math.Min(percentualContrib, 8);
            Add("Percentual Patronal", percentualPatronal.ToString());
            Add("Percentual Participante", percentualContrib.ToString());

            var contribMensal = dados.SalarioContribuicao / 100 * percentualContrib;
            Add("Contribuição Mensal", $"{dados.SalarioContribuicao} / 100 * {percentualContrib} = {contribMensal}");

            var contribPatronal = dados.SalarioContribuicao / 100 * percentualPatronal;
            Add("Contribuição Patronal", $"{dados.SalarioContribuicao} / 100 * {percentualContrib} = {contribPatronal}");

            var contribBrutaTotal = contribMensal + contribPatronal;
            Add("Contribuição Total", $"{contribMensal} + {contribPatronal} = {contribBrutaTotal}");

            // Calcula reajuste mensal
            var indiceReajuste = new IndiceProxy().BuscarUltimoPorCodigo("REAJSIMCD");
            var reajusteAnual = indiceReajuste.VALORES.First().VALOR_IND;

            decimal mensal = 1M / 12M;
            decimal valorReajuste = reajusteAnual / 100 + 1;
            var taxaMensal = ((decimal)(Math.Pow((double)valorReajuste, (double)mensal) - 1) * 100).Arredonda(6);
            Add("Taxa Mensal", $"{taxaMensal}");

            var indiceTaxaAdm = new IndiceProxy().BuscarUltimoPorCodigo("TXADMCD");
            var taxaAdm = contribBrutaTotal * indiceTaxaAdm.VALORES.First().VALOR_IND / 100;
            Add("Taxa Adm", $"{taxaAdm}");

            var indiceTaxaRisco = new IndiceProxy().BuscarUltimoPorCodigo("TXRISCOCD");
            var taxaRisco = contribBrutaTotal * indiceTaxaRisco.VALORES.First().VALOR_IND / 100;
            Add("Taxa Risco", $"{taxaRisco}");

           
            var dataAposentadoria = DataNascimento.AddYears(dados.IdadeAposentadoria);
            var data58Anos = DataNascimento.AddYears(58);

            Add("Data de Aposentadoria", $"{dataAposentadoria.ToString("dd/MM/yyyy")}");
            Add("Idade na Aposentadoria", $"{dados.IdadeAposentadoria}");
            
            Add("Saldo Acumulado", $"{dados.SaldoAcumulado.ToString("C")}");

            saldoProjetado = dados.SaldoAcumulado;

            for (var data = DateTime.Today; data <= dataAposentadoria; data = data.AddMonths(1))
            {
                var decimoTerceiro =
                    (CdEmpresa == "0001" && data.Month == 11) ||
                    (CdEmpresa == "0002" && data.Month == 12);

                var idadeNaData = new Intervalo(data, DataNascimento, new CalculoAnosMesesDiasAlgoritmo2()).Anos;

                var valor = contribBrutaTotal - taxaAdm;

                string memoria = "";

                if (decimoTerceiro)
                    memoria += "(";

                memoria = $"{contribBrutaTotal} - {taxaAdm}";

                // Verifica se o mês da evolução atual é o mês de aniversário
                var mesAniversario58Anos = data58Anos.Month == data.Month && data58Anos.Year == data.Year;

                if (idadeNaData < 58 && !mesAniversario58Anos)
                {
                    valor -= taxaRisco;
                    memoria += $" - {taxaRisco}";
                }

                if (decimoTerceiro)
                {
                    valor *= 2;
                    memoria += " * 2)";
                }

                var saldoProjetadoAnterior = saldoProjetado.Arredonda(2);
                var rendimento = valor + (saldoProjetadoAnterior * (taxaMensal / 100));
                saldoProjetado = saldoProjetadoAnterior + rendimento;

                if(data == DateTime.Today)
                {
                    saldoProjetado += dados.Aporte;
                }

                if (!decimoTerceiro)
                {
                    if (idadeNaData < 58 && !mesAniversario58Anos)
                    {
                        Add($"Saldo em {data.ToString("MM/yyyy")} ({idadeNaData} anos)",
                            $"({contribBrutaTotal.ToString("N2")} - {taxaAdm} - {taxaRisco})" +
                            $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")}" +
                                $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")} * ({taxaMensal} / 100))" +
                                $"{(data == DateTime.Today ? $" + {dados.Aporte.ToString("N2")}" : "")}" +
                            $" = {saldoProjetado.ToString("N2")}");
                    }
                    else
                    {
                        Add($"Saldo em {data.ToString("MM/yyyy")} ({idadeNaData} anos {(mesAniversario58Anos ? "e irá completar 58 este mês" : "")})",
                            $"({contribBrutaTotal.ToString("N2")} - {taxaAdm})" +
                            $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")}" +
                                $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")} * ({taxaMensal} / 100))" +
                                $"{(data == DateTime.Today ? $" + {dados.Aporte.ToString("N2")}" : "")}" +
                            $" = {saldoProjetado.ToString("N2")}");
                    }
                }
                else
                {
                    if (idadeNaData < 58 && !mesAniversario58Anos)
                    {
                        Add($"Saldo em {data.ToString("MM/yyyy")} (13º)",
                            $"({contribBrutaTotal.ToString("N2")} - {taxaAdm} - {taxaRisco}) * 2)" +
                            $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")}" +
                                $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")} * ({taxaMensal} / 100))" +
                                $"{(data == DateTime.Today ? $" + {dados.Aporte.ToString("N2")}" : "")}" +
                            $" = {saldoProjetado.ToString("N2")}");
                    }
                    else
                    {
                        Add($"Saldo em {data.ToString("MM/yyyy")} (13º)",
                            $"({contribBrutaTotal.ToString("N2")} - {taxaAdm}) * 2)" +
                            $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")}" +
                                $" + ({saldoProjetadoAnterior.Arredonda(2).ToString("N2")} * ({taxaMensal} / 100))" +
                                $"{(data == DateTime.Today ? $" + {dados.Aporte.ToString("N2")}" : "")}" +
                            $" = {saldoProjetado.ToString("N2")}");
                    }
                }
            }
            Add("Saldo Projetado", $"{saldoProjetado.ToString("C")}");

            saque = 0M;
            if (dados.PercentualSaque > 0)
                saque = saldoProjetado * (dados.PercentualSaque / 100);

            Add("Percentual de Saque a Vista", $"{dados.PercentualSaque}");
            Add("Valor Saque a Vista", $"{saldoProjetado} * {dados.PercentualSaque / 100} = {saque.ToString("N2")}");

            saldoBeneficio = saldoProjetado - saque;
            Add("Saldo do Benefício", $"{saldoProjetado} - {saque} = {saldoBeneficio.ToString("N2")}");

            return MemoriaCalculo;
        }
    }
}
