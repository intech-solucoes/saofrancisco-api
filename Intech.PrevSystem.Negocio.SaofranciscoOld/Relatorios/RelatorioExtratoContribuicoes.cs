using Intech.PrevSystem.Entidades;
using System.Collections.Generic;

namespace Intech.PrevSystem.Negocio.Saofrancisco.Relatorios
{
    public class RelatorioExtratoContribuicoes
    {
        public List<FuncionarioDados> Funcionario { get; set; }
        public List<FundacaoEntidade> Fundacao { get; set; }
        public List<PlanoVinculadoEntidade> Plano { get; set; }

        public RelatorioExtratoContribuicoes(FuncionarioDados funcionario, FundacaoEntidade fundacao, PlanoVinculadoEntidade plano)
        {
            Funcionario = new List<FuncionarioDados> {
                funcionario
            };

            Fundacao = new List<FundacaoEntidade>
            {
                fundacao
            };

            Plano = new List<PlanoVinculadoEntidade>
            {
                plano
            };
        }
    }
}