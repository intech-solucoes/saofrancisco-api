#region Usings
using Intech.PrevSystem.Entidades;
using System;
using System.Linq; 
#endregion

namespace Intech.PrevSystem.Saofrancisco.API.Relatorios
{
    public partial class ContraCheque : DevExpress.XtraReports.UI.XtraReport
    {
        public ContraCheque()
        {
            InitializeComponent();
        }

        public void GerarRelatorio(dynamic demonstrativo, EntidadeEntidade entidade, FuncionarioEntidade funcionario, EmpresaEntidade empresa, PlanoVinculadoEntidade plano)
        {
            var dataReferencia = (DateTime)demonstrativo.Resumo.Referencia;

            xrLabelDataReferencia.Text = $"{dataReferencia.MesPorExtenso()}/{dataReferencia.Year}";
            xrLabelNome.Text = entidade.NOME_ENTID;
            xrLabelEndereco.Text = entidade.END_ENTID;
            xrLabelBairro.Text = entidade.BAIRRO_ENTID;
            xrLabelCidade.Text = entidade.CID_ENTID;
            xrLabelCEP.Text = entidade.CEP_ENTID;
            xrLabelCPF.Text = entidade.CPF_CGC;
            xrLabelRG.Text = $"{entidade.IDENTIDADE} - {entidade.ORGAO_EXP}";
            xrLabelDataNacimento.Text = entidade.DT_NASCIMENTO?.ToString("dd/MM/yyyy");
            xrLabelDataInicioBeneficio.Text = plano.ProcessoBeneficio.DT_INICIO_PREV?.ToString("dd/MM/yyyy");
            xrLabelNomePatrocinadora.Text = empresa.NOME_ENTID;
            xrLabelPlano.Text = plano.DS_PLANO;
            xrLabelTipoAposentadoria.Text = plano.ProcessoBeneficio.DS_ESPECIE;
            xrLabelMatricula.Text = funcionario.NUM_MATRICULA;
            xrLabelProventos.Text = demonstrativo.Resumo.Bruto?.ToString("N2");
            xrLabelDescontos.Text = demonstrativo.Resumo.Descontos?.ToString("N2");
            xrLabelLiquido.Text = demonstrativo.Resumo.Liquido?.ToString("N2");

            xrTableCellDataReferencia.Text = xrLabelDataReferencia.Text;
            xrLabelDataPagamento.Text = dataReferencia.ToString("dd/MM/yyyy");

            xrLabelBanco.Text = entidade.NUM_BANCO;
            xrLabelAgencia.Text = entidade.NUM_AGENCIA;
            xrLabelConta.Text = entidade.NUM_CONTA;

            //xrLabelMensagens.Text = demonstrativo.;

            foreach (var item in demonstrativo.Proventos)
                PreencherDataTable(item);

            foreach (var item in demonstrativo.Descontos)
                PreencherDataTable(item);
        }

        private void PreencherDataTable(FichaFinanceiraAssistidoEntidade item)
        {
            var row = dataTable1.NewRow();

            row["DESCRICAO"] = item.DS_RUBRICA;
            row["PD"] = item.RUBRICA_PROV_DESC;
            row["VALOR"] = item.VALOR_MC;

            dataTable1.Rows.Add(row);
        }
    }
}
