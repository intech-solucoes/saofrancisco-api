using Intech.PrevSystem.Entidades;
using System.Collections.Generic;

namespace Intech.PrevSystem.Negocio.Saofrancisco.Relatorios
{
    public class RelatorioExtratoContribuicoes
    {
        public FuncionarioDados Funcionario { get; set; }
        public FundacaoEntidade Fundacao { get; set; }
        public PlanoVinculadoEntidade Plano { get; set; }
        public List<FichaFechamentoEntidade> Ficha { get; set; }
        public string Periodo { get; set; }

        public RelatorioExtratoContribuicoes(FuncionarioDados funcionario, FundacaoEntidade fundacao, PlanoVinculadoEntidade plano, List<FichaFechamentoEntidade> ficha, string periodo)
        {
            Funcionario = funcionario;
            Fundacao = fundacao;
            Plano = plano;
            Ficha = ficha;
            Periodo = periodo;
        }
    }
}