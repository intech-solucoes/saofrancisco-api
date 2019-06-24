using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;

namespace Intech.PrevSystem.Negocio.Saofrancisco
{
    public class Dashboard
    {
        public DashboardDados Buscar(string cdFundacao, string cdPlano, string numInscricao)
        {
            DashboardDados dashboardDados = new DashboardDados();
            var fichafinanceira = new FichaFinanceiraProxy();
            var dados = fichafinanceira.BuscarUltimaPorFundacaoPlanoInscricao(cdFundacao, cdPlano, numInscricao);

            foreach (FichaFinanceiraEntidade dado in dados)
            {

                dashboardDados.Salario = (decimal) dado.SRC;

                if (dado.CONTRIB_PARTICIPANTE > 0)
                {
                    if(dado.CD_OPERACAO == "C")
                        dashboardDados.ContribParticipante += (decimal) dado.CONTRIB_PARTICIPANTE;
                    else
                        dashboardDados.ContribParticipante -= (decimal)dado.CONTRIB_PARTICIPANTE;
                }

                if (dado.CONTRIB_EMPRESA > 0)
                {
                    if (dado.CD_OPERACAO == "C")
                        dashboardDados.ContribEmpresa += (decimal)dado.CONTRIB_EMPRESA;
                    else
                        dashboardDados.ContribEmpresa -= (decimal)dado.CONTRIB_EMPRESA;
                }

            }

            return dashboardDados;

        }

    }

    public class DashboardDados
    {

        public decimal Salario { get; set; }
        public decimal ContribParticipante {get; set;}
        public decimal ContribEmpresa {get; set;}

    }

}
