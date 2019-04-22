using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;
using Intech.PrevSystem.Negocio.Proxy;
using Intech.PrevSystem.Entidades;

namespace Intech.PrevSystem.Saofrancisco.API.Relatorios
{
    public partial class RelatorioExtratoContribuicao : DevExpress.XtraReports.UI.XtraReport
    {

        FuncionarioEntidade Funcionario;
        FundacaoEntidade Fundacao;
        EmpresaEntidade Empresa;
        PlanoVinculadoEntidade Plano;

        public RelatorioExtratoContribuicao()
        {
            InitializeComponent();
        }

        public void GerarRelatorio(string cdFundacao, string cdEmpresa, string cdPlano, string numMatricula, DateTime dtaInicio, DateTime dtaFim)
        {
            var prxFichaFechamento = new FichaFechamentoProxy();

            string AnoRefMesRefInicio = dtaInicio.ToString("yyyyMM");
            string AnoRefMesRefFim = dtaFim.ToString("yyyyMM");
            
            Funcionario = new FuncionarioProxy().BuscarPorMatricula(numMatricula);
            Fundacao = new FundacaoProxy().BuscarPorCodigo(cdFundacao);
            Empresa = new EmpresaProxy().BuscarPorCodigo(cdEmpresa);
            Plano = new PlanoVinculadoProxy().BuscarPorFundacaoEmpresaMatriculaPlano(cdFundacao, cdEmpresa, numMatricula, cdPlano);

            var relatorio = new FichaFechamentoProxy()
                .BuscarRelatorioPorFundacaoEmpresaPlanoInscricaoReferencia(cdFundacao, cdEmpresa, cdPlano, Funcionario.NUM_INSCRICAO, AnoRefMesRefInicio, AnoRefMesRefFim);


            foreach (var item in relatorio)
            {
                DataRow dr = TABLE_RELATORIO.NewRow();

                dr["NOME_FUNDACAO"] = Fundacao.NOME_ENTID;
                dr["ENDERECO_FUNDACAO"] = Fundacao.END_ENTID;
                dr["BAIRRO_FUNDACAO"] = Fundacao.BAIRRO_ENTID;
                dr["CEP_FUNDACAO"] = Fundacao.CEP_ENTID.AplicarMascara(Mascaras.CEP);
                dr["ESTADO_FUNDACAO"] = Fundacao.UF_ENTID;
                dr["TELEFONE_FUNDACAO"] = Fundacao.FONE_ENTID;
                dr["FAX_FUNDACAO"] = Fundacao.FAX_ENTID;
                dr["CNPJ_FUNDACAO"] = Fundacao.CPF_CGC.AplicarMascara(Mascaras.CNPJ);
                dr["PERIODO_RELATORIO"] = String.Format("{0} a {1}", dtaInicio.ToString("MM/yyyy"), dtaFim.ToString("MM/yyyy"));
                dr["PATROCINADORA"] = Empresa.NOME_ENTID;
                dr["CNPJ_PATROCINADORA"] = "";
                dr["NOME_PARTICIPANTE"] = Funcionario.NOME_ENTID;                
                dr["MATRICULA"] = Funcionario.NUM_MATRICULA;
                dr["INSCRICAO"] = Funcionario.NUM_INSCRICAO;
                dr["PLANO"] = Plano.DS_PLANO;
                dr["MES_ANO_CONTRIBUICAO"] = String.Format("{0} / {1} ", item.MES_REF, item.ANO_REF);
                dr["CONTRIBUICAO_PARTICIPANTE"] = item.VL_GRUPO1;
                dr["CONTRIBUICAO_PATROCINADORA"] = item.VL_GRUPO2;                
                dr["CUSTEIO"] = item.VL_GRUPO3;
                dr["VALOR_LIQUIDO"] = item.VL_LIQUIDO;
                dr["COTA_CONVERSAO"] = item.VL_COTA;
                dr["QUANTIDADE_COTAS"] = item.QTE_COTA;
                dr["QUANTIDADE_COTAS_ACUMULADAS"] = item.QTE_COTA_ACUM;
                dr["VALOR_ACUMULADO"] = item.VL_ACUMULADO;

                TABLE_RELATORIO.Rows.Add(dr);
            }
        }



    }
}
