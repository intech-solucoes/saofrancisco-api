using Intech.PrevSystem.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intech.PrevSystem.Negocio.Saofrancisco.Relatorios
{
    public class RelatorioExtratoSaldado
    {
        public FuncionarioDados Funcionario { get; set; }
        public FundacaoEntidade Fundacao { get; set; }
        public PlanoVinculadoEntidade Plano { get; set; }
        public List<FichaFinanceiraEntidade> Ficha { get; set; }
        public string Periodo { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal SaldoAtualizado { get; set; }
        public decimal QuantidadeCotas { get; set; }
        public decimal ValorBruto { get; set; }
        public DateTime DataConversao { get; set; }


        public RelatorioExtratoSaldado(FuncionarioDados funcionario, FundacaoEntidade fundacao, PlanoVinculadoEntidade plano, List<FichaFinanceiraEntidade> ficha, string periodo,
                                       DateTime dataEmissao, decimal saldoAtualizado, decimal quantidadeCotas, decimal valorBruto, DateTime dataConversao)
        {
            Funcionario = funcionario;
            Fundacao = fundacao;
            Plano = plano;
            Ficha = ficha;
            Periodo = periodo;
            DataEmissao = dataEmissao;
            SaldoAtualizado = saldoAtualizado;
            QuantidadeCotas = quantidadeCotas;
            ValorBruto = valorBruto;
            DataConversao = dataConversao;
        }
    }
}