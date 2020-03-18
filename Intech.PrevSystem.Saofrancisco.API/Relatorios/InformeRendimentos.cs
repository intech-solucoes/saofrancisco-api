using DevExpress.XtraReports.UI;
using Intech.PrevSystem.Entidades;
using Intech.PrevSystem.Negocio.Proxy;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Intech.PrevSystem.Saofrancisco.API.Relatorios
{
    /// <summary>
    /// Summary description for InformeRendimentos
    /// </summary>
    public class InformeRendimentos : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRPictureBox xrPictureBox1;
        private XRLabel xrLabel1;
        private XRTableCell xrTableCell2;
        private XRLabel xrLabelAnoExercicio;
        private XRLabel xrLabel3;
        private XRLabel xrLabel2;
        private XRLabel xrLabelAnoCalendario;
        private XRLabel xrLabel4;
        private XRLabel xrLabel5;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell3;
        private XRLabel xrLabel6;
        private XRTableCell xrTableCell4;
        private XRLabel xrLabel7;
        private XRLabel xrLabel9;
        private XRLabel xrLabelEmpresaCNPJ;
        private XRLabel xrLabelEmpresaNome;
        private XRLabel xrLabel8;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell5;
        private XRLabel xrLabelCPF;
        private XRLabel xrLabel14;
        private XRTableCell xrTableCell6;
        private XRLabel xrLabelMatricula;
        private XRLabel xrLabelNome;
        private XRLabel xrLabel17;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell7;
        private XRLabel xrLabel10;
        private XRLabel xrLabelNatureza;
        private XRLabel xrLabel12;
        private XRLabel xrLabel11;
        private XRTable xrTable4;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell301;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell302;
        private XRTableRow xrTableRow7;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell303;
        private XRTableRow xrTableRow8;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell304;
        private XRTableRow xrTableRow9;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell305;
        private XRLabel xrLabel13;
        private XRTable xrTable5;
        private XRTableRow xrTableRow10;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell401;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCell402;
        private XRTableRow xrTableRow12;
        private XRTableCell xrTableCell17;
        private XRTableCell xrTableCell403;
        private XRTableRow xrTableRow13;
        private XRTableCell xrTableCell19;
        private XRTableCell xrTableCell404;
        private XRTableRow xrTableRow14;
        private XRTableCell xrTableCell21;
        private XRTableCell xrTableCell405;
        private XRTableRow xrTableRow15;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell406;
        private XRTableRow xrTableRow16;
        private XRTableCell xrTableCell25;
        private XRTableCell xrTableCell407;
        private XRLabel xrLabel18;
        private XRLabel xrLabel16;
        private XRLabel xrLabel15;
        private XRTable xrTable6;
        private XRTableRow xrTableRow17;
        private XRTableCell xrTableCell27;
        private XRTableCell xrTableCell501;
        private XRTableRow xrTableRow18;
        private XRTableCell xrTableCell29;
        private XRTableCell xrTableCell502;
        private XRTableRow xrTableRow19;
        private XRTableCell xrTableCell31;
        private XRTableCell xrTableCell503;
        private XRLabel xrLabel20;
        private XRLabel xrLabel19;
        private XRTable xrTable7;
        private XRTableRow xrTableRow20;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell601;
        private XRTableRow xrTableRow21;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell602;
        private XRTableRow xrTableRow22;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell603;
        private XRTableRow xrTableRow23;
        private XRTableCell xrTableCell26;
        private XRTableCell xrTableCell604;
        private XRTableRow xrTableRow24;
        private XRTableCell xrTableCell30;
        private XRTableCell xrTableCell605;
        private XRTableRow xrTableRow25;
        private XRTableCell xrTableCell33;
        private XRTableCell xrTableCell606;
        private XRTable xrTable8;
        private XRTableRow xrTableRow26;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCell36;
        private XRTableCell xrTableCell37;
        private XRTableRow xrTableRow27;
        private XRTableCell xrTableCell38;
        private XRLabel xrLabel21;
        private XRPanel xrPanel1;
        private XRLabel xrLabel701Descricao;
        private XRLabel xrLabel701Valor;
        private XRLabel xrLabel24;
        private XRTable xrTable10;
        private XRTableRow xrTableRow29;
        private XRTableCell xrTableCell42;
        private XRLabel xrLabelResponsavelNome;
        private XRLabel xrLabel26;
        private XRTableCell xrTableCell43;
        private XRLabel xrLabelData;
        private XRLabel xrLabel28;
        private XRTableCell xrTableCell39;
        private XRLabel xrLabel29;
        private XRLabel xrLabel30;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public InformeRendimentos()
        {
            InitializeComponent();
        }

        private HeaderInfoRendEntidade Informe;

        public void GerarRelatorio(HeaderInfoRendEntidade informe)
        {
            Informe = informe;

            xrLabelAnoExercicio.Text = $"Exercício de {informe.ANO_EXERCICIO}";
            xrLabelAnoCalendario.Text = $"Ano-calendário de {informe.ANO_CALENDARIO}";

            xrLabelEmpresaCNPJ.Text = informe.CNPJ_EMPRESA.AplicarMascara(Mascaras.CNPJ);
            xrLabelEmpresaNome.Text = informe.NOM_EMPRESA;

            xrLabelCPF.Text = informe.CPF;
            xrLabelNome.Text = informe.NOME;
            xrLabelMatricula.Text = informe.NUM_MATRICULA;
            xrLabelNatureza.Text = informe.DESC_NATUREZA;

            xrTableCell301.Text = BuscarValor("301");
            xrTableCell302.Text = BuscarValor("302");
            xrTableCell303.Text = BuscarValor("303");
            xrTableCell304.Text = BuscarValor("304");
            xrTableCell305.Text = BuscarValor("305");

            xrTableCell401.Text = BuscarValor("401");
            xrTableCell402.Text = BuscarValor("402");
            xrTableCell403.Text = BuscarValor("403");
            xrTableCell404.Text = BuscarValor("404");
            xrTableCell405.Text = BuscarValor("405");
            xrTableCell406.Text = BuscarValor("406");
            xrTableCell407.Text = BuscarValor("407");

            xrTableCell501.Text = BuscarValor("501");
            xrTableCell502.Text = BuscarValor("502");
            xrTableCell503.Text = BuscarValor("503");

            xrTableCell601.Text = BuscarValor("601");
            xrTableCell602.Text = BuscarValor("602");
            xrTableCell603.Text = BuscarValor("603");
            xrTableCell604.Text = BuscarValor("604");
            xrTableCell605.Text = BuscarValor("605");
            xrTableCell606.Text = BuscarValor("606");

            var grupo7 = Informe.Grupos.Where(x => x.COD_GRUPO == "7").SingleOrDefault();

            if (grupo7 != null)
            {
                xrLabel701Descricao.Text =
                    grupo7.Itens.Where(x => x.COD_LINHA == "701").SingleOrDefault()?.TXT_QUADRO + "\r\n\r\n" +
                    grupo7.Itens.Where(x => x.COD_LINHA == "702").SingleOrDefault()?.TXT_QUADRO + "\r\n\r\n" +
                    grupo7.Itens.Where(x => x.COD_LINHA == "703").SingleOrDefault()?.TXT_QUADRO;

                xrLabel701Valor.Text = BuscarValor("701");
            }

            var dirf = new DirfProxy().Buscar().First();

            xrLabelResponsavelNome.Text = dirf.NOME_RESPONSAVEL;
            xrLabelData.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private string BuscarValor(string codigo)
        {
            var codGrupo = codigo.Substring(0, 1);
            var grupo = Informe.Grupos.SingleOrDefault(x => x.COD_GRUPO == codGrupo);

            if (grupo != null)
            {
                var item = grupo.Itens.SingleOrDefault(x => x.COD_LINHA == codigo);

                if (item != null && item.VAL_LINHA.HasValue)
                    return item.VAL_LINHA.Value.ToString("N2");
            }

            return "0,00";
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformeRendimentos));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelResponsavelNome = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelData = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell601 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell602 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell603 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell604 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell605 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell606 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell501 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell502 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell503 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell401 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell402 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell403 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell404 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell405 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell406 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell407 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelCPF = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelMatricula = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelNome = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelNatureza = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelAnoExercicio = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelAnoCalendario = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelEmpresaCNPJ = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabelEmpresaNome = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell301 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell302 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell303 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell304 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell305 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel701Descricao = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel701Valor = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10,
            this.xrLabel24,
            this.xrLabel21,
            this.xrLabel20,
            this.xrLabel19,
            this.xrTable7,
            this.xrLabel18,
            this.xrLabel16,
            this.xrLabel15,
            this.xrTable6,
            this.xrTable5,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrTable3,
            this.xrLabel8,
            this.xrLabel9,
            this.xrLabel5,
            this.xrTable1,
            this.xrTable2,
            this.xrTable4,
            this.xrTable8,
            this.xrPanel1});
            this.Detail.HeightF = 1247.307F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable10
            // 
            this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0.0008265178F, 1029.577F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow29});
            this.xrTable10.SizeF = new System.Drawing.SizeF(769.9985F, 37.31061F);
            this.xrTable10.StylePriority.UseBorders = false;
            // 
            // xrTableRow29
            // 
            this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell39});
            this.xrTableRow29.Name = "xrTableRow29";
            this.xrTableRow29.Weight = 1D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelResponsavelNome,
            this.xrLabel26});
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.Text = "xrTableCell3";
            this.xrTableCell42.Weight = 1.3193595651759755D;
            // 
            // xrLabelResponsavelNome
            // 
            this.xrLabelResponsavelNome.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelResponsavelNome.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabelResponsavelNome.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15.00003F);
            this.xrLabelResponsavelNome.Name = "xrLabelResponsavelNome";
            this.xrLabelResponsavelNome.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelResponsavelNome.SizeF = new System.Drawing.SizeF(338.6356F, 22.99995F);
            this.xrLabelResponsavelNome.StylePriority.UseBorders = false;
            this.xrLabelResponsavelNome.StylePriority.UseFont = false;
            this.xrLabelResponsavelNome.StylePriority.UseTextAlignment = false;
            this.xrLabelResponsavelNome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel26.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(100F, 15F);
            this.xrLabel26.StylePriority.UseBorders = false;
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "NOME";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelData,
            this.xrLabel28});
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.Text = "xrTableCell4";
            this.xrTableCell43.Weight = 0.51475717675464516D;
            // 
            // xrLabelData
            // 
            this.xrLabelData.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelData.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabelData.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15.00003F);
            this.xrLabelData.Name = "xrLabelData";
            this.xrLabelData.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelData.SizeF = new System.Drawing.SizeF(131.6301F, 22.99995F);
            this.xrLabelData.StylePriority.UseBorders = false;
            this.xrLabelData.StylePriority.UseFont = false;
            this.xrLabelData.StylePriority.UseTextAlignment = false;
            this.xrLabelData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel28
            // 
            this.xrLabel28.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel28.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(131.6301F, 14.99996F);
            this.xrLabel28.StylePriority.UseBorders = false;
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "DATA";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel29,
            this.xrLabel30});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Text = "xrTableCell39";
            this.xrTableCell39.Weight = 1.1658775376844002D;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel29.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0.0005772908F, 0F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(131.6301F, 14.99996F);
            this.xrLabel29.StylePriority.UseBorders = false;
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "ASSINATURA";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel30.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0.0005722046F, 14.65543F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(299.2413F, 22.99995F);
            this.xrLabel30.StylePriority.UseBorders = false;
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            this.xrLabel30.Text = "Isento de Assinatura conforme IN/SRF nº 288/2003";
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel24.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0.0008123893F, 1012.723F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(769.9992F, 16.85413F);
            this.xrLabel24.StylePriority.UseBorders = false;
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "8. RESPONSÁVEIS PELAS INFORMAÇÕES";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0.000777068F, 861.2093F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(769.9992F, 16.85413F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "7. INFORMAÇÕES COMPLEMENTARES";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel20.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(570.1898F, 695.3022F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(199.8102F, 16.85419F);
            this.xrLabel20.StylePriority.UseBorders = false;
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "VALORES EM REAIS";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel19.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0.0001766064F, 673.5219F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(769.9992F, 16.85413F);
            this.xrLabel19.StylePriority.UseBorders = false;
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "6. RENDIMENTOS RECEBIDOS ACUMULADAMENTE - Art. 12-A da Lei nº 7.713, de 1998 (suj" +
    "eitos a tributação exclusiva)";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable7
            // 
            this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0.0002472489F, 733.9366F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20,
            this.xrTableRow21,
            this.xrTableRow22,
            this.xrTableRow23,
            this.xrTableRow24,
            this.xrTableRow25});
            this.xrTable7.SizeF = new System.Drawing.SizeF(769.9998F, 127.2726F);
            this.xrTable7.StylePriority.UseBorders = false;
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell601});
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "1. Total dos rendimentos tributáveis (inclusive férias e décimo terceiro salário";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell12.Weight = 5.7018927001953124D;
            // 
            // xrTableCell601
            // 
            this.xrTableCell601.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell601.Name = "xrTableCell601";
            this.xrTableCell601.StylePriority.UseFont = false;
            this.xrTableCell601.StylePriority.UseTextAlignment = false;
            this.xrTableCell601.Text = "0,00";
            this.xrTableCell601.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell601.Weight = 1.9981048583984373D;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell602});
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "2. Exclusão: Despesas com a ação judicial";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell18.Weight = 5.7018927001953124D;
            // 
            // xrTableCell602
            // 
            this.xrTableCell602.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell602.Name = "xrTableCell602";
            this.xrTableCell602.StylePriority.UseFont = false;
            this.xrTableCell602.StylePriority.UseTextAlignment = false;
            this.xrTableCell602.Text = "0,00";
            this.xrTableCell602.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell602.Weight = 1.9981048583984373D;
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22,
            this.xrTableCell603});
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 1D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = "3. Dedução: Contribuição previdenciária oficial";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell22.Weight = 5.7018927001953124D;
            // 
            // xrTableCell603
            // 
            this.xrTableCell603.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell603.Name = "xrTableCell603";
            this.xrTableCell603.StylePriority.UseFont = false;
            this.xrTableCell603.StylePriority.UseTextAlignment = false;
            this.xrTableCell603.Text = "0,00";
            this.xrTableCell603.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell603.Weight = 1.9981048583984373D;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell604});
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 1D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.Text = "4. Dedução: Pensão Alimentícia (preencher também o quadro 7)";
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell26.Weight = 5.7018927001953124D;
            // 
            // xrTableCell604
            // 
            this.xrTableCell604.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell604.Name = "xrTableCell604";
            this.xrTableCell604.StylePriority.UseFont = false;
            this.xrTableCell604.StylePriority.UseTextAlignment = false;
            this.xrTableCell604.Text = "0,00";
            this.xrTableCell604.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell604.Weight = 1.9981048583984373D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell30,
            this.xrTableCell605});
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "5. Imposto sobre a renda retido na fonte";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell30.Weight = 5.7018927001953124D;
            // 
            // xrTableCell605
            // 
            this.xrTableCell605.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell605.Name = "xrTableCell605";
            this.xrTableCell605.StylePriority.UseFont = false;
            this.xrTableCell605.StylePriority.UseTextAlignment = false;
            this.xrTableCell605.Text = "0,00";
            this.xrTableCell605.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell605.Weight = 1.9981048583984373D;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell606});
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.Weight = 1D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            this.xrTableCell33.Text = "6. Rendimentos isentos de pensão, proventos de aposentadoria ou reforma por molés" +
    "tia grave ou aposentadoria ou reforma por acidente de serviço";
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell33.Weight = 5.7018927001953124D;
            // 
            // xrTableCell606
            // 
            this.xrTableCell606.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell606.Name = "xrTableCell606";
            this.xrTableCell606.StylePriority.UseFont = false;
            this.xrTableCell606.StylePriority.UseTextAlignment = false;
            this.xrTableCell606.Text = "0,00";
            this.xrTableCell606.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell606.Weight = 1.9981048583984373D;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel18.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(570.1892F, 593.0314F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(199.8102F, 16.85419F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "VALORES EM REAIS";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel16.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(570.1891F, 427.6925F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(199.8102F, 16.85419F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "VALORES EM REAIS";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 593.0313F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(570.1893F, 16.85419F);
            this.xrLabel15.StylePriority.UseBorders = false;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "5. RENDIMENTOS SUJEITOS À TRIBUTAÇÃO EXCLUSIVA (RENDIMENTO LÍQUIDO)";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 609.8856F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17,
            this.xrTableRow18,
            this.xrTableRow19});
            this.xrTable6.SizeF = new System.Drawing.SizeF(769.9998F, 63.63629F);
            this.xrTable6.StylePriority.UseBorders = false;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell501});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.Text = "1. Décimo Terceiro Salário";
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell27.Weight = 5.7018927001953124D;
            // 
            // xrTableCell501
            // 
            this.xrTableCell501.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell501.Name = "xrTableCell501";
            this.xrTableCell501.StylePriority.UseFont = false;
            this.xrTableCell501.StylePriority.UseTextAlignment = false;
            this.xrTableCell501.Text = "0,00";
            this.xrTableCell501.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell501.Weight = 1.9981048583984373D;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell502});
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.Weight = 1D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "2. Imposto sobre a renda retida na fonte sobre o 13º salário";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell29.Weight = 5.7018927001953124D;
            // 
            // xrTableCell502
            // 
            this.xrTableCell502.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell502.Name = "xrTableCell502";
            this.xrTableCell502.StylePriority.UseFont = false;
            this.xrTableCell502.StylePriority.UseTextAlignment = false;
            this.xrTableCell502.Text = "0,00";
            this.xrTableCell502.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell502.Weight = 1.9981048583984373D;
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31,
            this.xrTableCell503});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 1D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "3. Outros";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell31.Weight = 5.7018927001953124D;
            // 
            // xrTableCell503
            // 
            this.xrTableCell503.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell503.Name = "xrTableCell503";
            this.xrTableCell503.StylePriority.UseFont = false;
            this.xrTableCell503.StylePriority.UseTextAlignment = false;
            this.xrTableCell503.Text = "0,00";
            this.xrTableCell503.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell503.Weight = 1.9981048583984373D;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 444.5467F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow15,
            this.xrTableRow16});
            this.xrTable5.SizeF = new System.Drawing.SizeF(769.9998F, 148.4847F);
            this.xrTable5.StylePriority.UseBorders = false;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell401});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "1. Parcela isenta dos proventos de aposentadoria, reserva remunerada, reforma e p" +
    "ensão (65 anos ou mais)";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell11.Weight = 5.7018927001953124D;
            // 
            // xrTableCell401
            // 
            this.xrTableCell401.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell401.Name = "xrTableCell401";
            this.xrTableCell401.StylePriority.UseFont = false;
            this.xrTableCell401.StylePriority.UseTextAlignment = false;
            this.xrTableCell401.Text = "0,00";
            this.xrTableCell401.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell401.Weight = 1.9981048583984373D;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell402});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "2. Diárias e Ajudas de Custo";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell14.Weight = 5.7018927001953124D;
            // 
            // xrTableCell402
            // 
            this.xrTableCell402.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell402.Name = "xrTableCell402";
            this.xrTableCell402.StylePriority.UseFont = false;
            this.xrTableCell402.StylePriority.UseTextAlignment = false;
            this.xrTableCell402.Text = "0,00";
            this.xrTableCell402.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell402.Weight = 1.9981048583984373D;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell403});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "3. Pensão e proventos de aposentadoria ou reforma por moléstia grave, proventos d" +
    "e aposentadoria ou reforma por acidente em serviço";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell17.Weight = 5.7018927001953124D;
            // 
            // xrTableCell403
            // 
            this.xrTableCell403.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell403.Name = "xrTableCell403";
            this.xrTableCell403.StylePriority.UseFont = false;
            this.xrTableCell403.StylePriority.UseTextAlignment = false;
            this.xrTableCell403.Text = "0,00";
            this.xrTableCell403.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell403.Weight = 1.9981048583984373D;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell404});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = "4. Lucro e Dividendo Apurado a partir de 1996 pago por PJ (Lucro Real, Presumido " +
    "ou Arbitrado)";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell19.Weight = 5.7018927001953124D;
            // 
            // xrTableCell404
            // 
            this.xrTableCell404.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell404.Name = "xrTableCell404";
            this.xrTableCell404.StylePriority.UseFont = false;
            this.xrTableCell404.StylePriority.UseTextAlignment = false;
            this.xrTableCell404.Text = "0,00";
            this.xrTableCell404.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell404.Weight = 1.9981048583984373D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell405});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "5. Valores Pagos ao Titular ou Sócio da Microempresa ou Empresa de Pequeno Porte," +
    " exceto Pro-labore, Aluguéis ou Serviços Prestados";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell21.Weight = 5.7018927001953124D;
            // 
            // xrTableCell405
            // 
            this.xrTableCell405.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell405.Name = "xrTableCell405";
            this.xrTableCell405.StylePriority.UseFont = false;
            this.xrTableCell405.StylePriority.UseTextAlignment = false;
            this.xrTableCell405.Text = "0,00";
            this.xrTableCell405.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell405.Weight = 1.9981048583984373D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell406});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "6. Indenizações por rescisão de contrato de trabalho, inclusive a título de PDV, " +
    "e acidente de trabalho";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell23.Weight = 5.7018927001953124D;
            // 
            // xrTableCell406
            // 
            this.xrTableCell406.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell406.Name = "xrTableCell406";
            this.xrTableCell406.StylePriority.UseFont = false;
            this.xrTableCell406.StylePriority.UseTextAlignment = false;
            this.xrTableCell406.Text = "0,00";
            this.xrTableCell406.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell406.Weight = 1.9981048583984373D;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell407});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "7. Outros";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell25.Weight = 5.7018927001953124D;
            // 
            // xrTableCell407
            // 
            this.xrTableCell407.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell407.Name = "xrTableCell407";
            this.xrTableCell407.StylePriority.UseFont = false;
            this.xrTableCell407.StylePriority.UseTextAlignment = false;
            this.xrTableCell407.Text = "0,00";
            this.xrTableCell407.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell407.Weight = 1.9981048583984373D;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 427.6925F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(570.1891F, 16.85413F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "4. RENDIMENTOS ISENTOS E NÃO TRIBUTÁVEIS";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel12.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(570.1893F, 304.7779F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(199.8102F, 16.85419F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "VALORES EM REAIS";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 304.7779F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(570.1893F, 16.85419F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "3. RENDIMENTOS TRIBUTÁVEIS, DEDUÇÕES E IMPOSTO RETIDO NA FONTE";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 218.4905F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4});
            this.xrTable3.SizeF = new System.Drawing.SizeF(769.9999F, 80.30302F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelCPF,
            this.xrLabel14});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell3";
            this.xrTableCell5.Weight = 0.71590922692310011D;
            // 
            // xrLabelCPF
            // 
            this.xrLabelCPF.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelCPF.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabelCPF.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15F);
            this.xrLabelCPF.Name = "xrLabelCPF";
            this.xrLabelCPF.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCPF.SizeF = new System.Drawing.SizeF(183.75F, 23.00001F);
            this.xrLabelCPF.StylePriority.UseBorders = false;
            this.xrLabelCPF.StylePriority.UseFont = false;
            this.xrLabelCPF.StylePriority.UseTextAlignment = false;
            this.xrLabelCPF.Text = "445.472.696-53";
            this.xrLabelCPF.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel14.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(100F, 15F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "CPF";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelMatricula,
            this.xrLabelNome,
            this.xrLabel17});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "xrTableCell4";
            this.xrTableCell6.Weight = 2.2840905352775804D;
            // 
            // xrLabelMatricula
            // 
            this.xrLabelMatricula.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelMatricula.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabelMatricula.LocationFloat = new DevExpress.Utils.PointFloat(405.341F, 15F);
            this.xrLabelMatricula.Name = "xrLabelMatricula";
            this.xrLabelMatricula.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelMatricula.SizeF = new System.Drawing.SizeF(180.9089F, 23.00001F);
            this.xrLabelMatricula.StylePriority.UseBorders = false;
            this.xrLabelMatricula.StylePriority.UseFont = false;
            this.xrLabelMatricula.StylePriority.UseTextAlignment = false;
            this.xrLabelMatricula.Text = "000030874";
            this.xrLabelMatricula.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabelNome
            // 
            this.xrLabelNome.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelNome.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabelNome.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15F);
            this.xrLabelNome.Name = "xrLabelNome";
            this.xrLabelNome.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelNome.SizeF = new System.Drawing.SizeF(405.341F, 23.00001F);
            this.xrLabelNome.StylePriority.UseBorders = false;
            this.xrLabelNome.StylePriority.UseFont = false;
            this.xrLabelNome.StylePriority.UseTextAlignment = false;
            this.xrLabelNome.Text = "SEMIRAMIS REZENDE E S M CEZAR";
            this.xrLabelNome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel17.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(200.38F, 15F);
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "NOME COMPLETO";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabelNatureza});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.Weight = 2.9999997622006807D;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(223.106F, 15F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "NATUREZA DO RENDIMENTO";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabelNatureza
            // 
            this.xrLabelNatureza.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelNatureza.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabelNatureza.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15F);
            this.xrLabelNatureza.Name = "xrLabelNatureza";
            this.xrLabelNatureza.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelNatureza.SizeF = new System.Drawing.SizeF(770.0001F, 22.99999F);
            this.xrLabelNatureza.StylePriority.UseBorders = false;
            this.xrLabelNatureza.StylePriority.UseFont = false;
            this.xrLabelNatureza.StylePriority.UseTextAlignment = false;
            this.xrLabelNatureza.Text = "BENEFÍCIO PREVIDÊNCIA COMPLEMENTAR/NÃO OPTANTE TRIBUTAÇÃO EXCLUSIVA";
            this.xrLabelNatureza.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0.0001155969F, 201.6363F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(769.9999F, 16.85417F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "2. PESSOA FÍSICA BENEFICIÁRIA DOS RENDIMENTOS";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 146.7822F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(769.9999F, 16.85417F);
            this.xrLabel9.StylePriority.UseBorders = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "1. FONTE PAGADORA PESSOA JURÍDICA OU PESSOA FÍSICA";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(10.00002F, 103.125F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(750F, 33.52082F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = resources.GetString("xrLabel5.Text");
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(770.0001F, 92.70834F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelAnoExercicio,
            this.xrLabel3,
            this.xrLabel2,
            this.xrPictureBox1,
            this.xrLabel1});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 1.5600648906705867D;
            // 
            // xrLabelAnoExercicio
            // 
            this.xrLabelAnoExercicio.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelAnoExercicio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelAnoExercicio.LocationFloat = new DevExpress.Utils.PointFloat(97.91666F, 69.70834F);
            this.xrLabelAnoExercicio.Name = "xrLabelAnoExercicio";
            this.xrLabelAnoExercicio.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelAnoExercicio.SizeF = new System.Drawing.SizeF(302.5F, 23F);
            this.xrLabelAnoExercicio.StylePriority.UseBorders = false;
            this.xrLabelAnoExercicio.StylePriority.UseFont = false;
            this.xrLabelAnoExercicio.StylePriority.UseTextAlignment = false;
            this.xrLabelAnoExercicio.Text = "Exercício de 2017";
            this.xrLabelAnoExercicio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(97.91666F, 46F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(302.5F, 23F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Imposto sobre a Renda da Pessoa Física";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(97.91666F, 23F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(302.5F, 23F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Secretaria Especial da Receita Federal do Brasil";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("brasil")));
            this.xrPictureBox1.Image = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "brasil.gif"));
            this.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1.589457E-05F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(87.5F, 92.70834F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(97.91666F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(302.5F, 23F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "MINISTÉRIO DA ECONOMIA";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelAnoCalendario,
            this.xrLabel4});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 1.4399351093294133D;
            // 
            // xrLabelAnoCalendario
            // 
            this.xrLabelAnoCalendario.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelAnoCalendario.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabelAnoCalendario.LocationFloat = new DevExpress.Utils.PointFloat(0F, 69.70834F);
            this.xrLabelAnoCalendario.Name = "xrLabelAnoCalendario";
            this.xrLabelAnoCalendario.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelAnoCalendario.SizeF = new System.Drawing.SizeF(369.5833F, 16.85417F);
            this.xrLabelAnoCalendario.StylePriority.UseBorders = false;
            this.xrLabelAnoCalendario.StylePriority.UseFont = false;
            this.xrLabelAnoCalendario.StylePriority.UseTextAlignment = false;
            this.xrLabelAnoCalendario.Text = "Ano-calendário de 2016";
            this.xrLabelAnoCalendario.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 11F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1.589457E-05F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(369.5833F, 58.99998F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "COMPROVANTE DE RENDIMENTOS PAGOS E DE IMPOSTO SOBRE A RENDA RETINO NA FONTE";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(5.779844E-05F, 163.6363F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(769.9999F, 37.31061F);
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelEmpresaCNPJ,
            this.xrLabel6});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 0.71590922692310011D;
            // 
            // xrLabelEmpresaCNPJ
            // 
            this.xrLabelEmpresaCNPJ.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelEmpresaCNPJ.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabelEmpresaCNPJ.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15F);
            this.xrLabelEmpresaCNPJ.Name = "xrLabelEmpresaCNPJ";
            this.xrLabelEmpresaCNPJ.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelEmpresaCNPJ.SizeF = new System.Drawing.SizeF(183.75F, 23.00001F);
            this.xrLabelEmpresaCNPJ.StylePriority.UseBorders = false;
            this.xrLabelEmpresaCNPJ.StylePriority.UseFont = false;
            this.xrLabelEmpresaCNPJ.StylePriority.UseTextAlignment = false;
            this.xrLabelEmpresaCNPJ.Text = "01.225.861/001-30";
            this.xrLabelEmpresaCNPJ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 15F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "CNPJ/CPF";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelEmpresaNome,
            this.xrLabel7});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 2.2840905352775804D;
            // 
            // xrLabelEmpresaNome
            // 
            this.xrLabelEmpresaNome.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelEmpresaNome.Font = new System.Drawing.Font("Arial", 10F);
            this.xrLabelEmpresaNome.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15F);
            this.xrLabelEmpresaNome.Name = "xrLabelEmpresaNome";
            this.xrLabelEmpresaNome.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelEmpresaNome.SizeF = new System.Drawing.SizeF(586.2498F, 23.00001F);
            this.xrLabelEmpresaNome.StylePriority.UseBorders = false;
            this.xrLabelEmpresaNome.StylePriority.UseFont = false;
            this.xrLabelEmpresaNome.StylePriority.UseTextAlignment = false;
            this.xrLabelEmpresaNome.Text = "REGIUS - SOCIEDADE CIVIL DE PREVIDENCIA PRIVADA";
            this.xrLabelEmpresaNome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(200.38F, 15F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "NOME EMPRESARIAL / NOME";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 321.632F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9});
            this.xrTable4.SizeF = new System.Drawing.SizeF(769.9998F, 106.0605F);
            this.xrTable4.StylePriority.UseBorders = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell301});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "1. Total dos Rendimentos (inclusive férias)";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell8.Weight = 5.7018927001953124D;
            // 
            // xrTableCell301
            // 
            this.xrTableCell301.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell301.Name = "xrTableCell301";
            this.xrTableCell301.StylePriority.UseFont = false;
            this.xrTableCell301.StylePriority.UseTextAlignment = false;
            this.xrTableCell301.Text = "0,00";
            this.xrTableCell301.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell301.Weight = 1.9981048583984373D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell302});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "2. Contribuição Previdência Oficial";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell10.Weight = 5.7018927001953124D;
            // 
            // xrTableCell302
            // 
            this.xrTableCell302.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell302.Name = "xrTableCell302";
            this.xrTableCell302.StylePriority.UseFont = false;
            this.xrTableCell302.StylePriority.UseTextAlignment = false;
            this.xrTableCell302.Text = "0,00";
            this.xrTableCell302.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell302.Weight = 1.9981048583984373D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell303});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "3. Contribuição a entidade de previdência complementar, pública ou privada, e a f" +
    "undos de aposentadoria programada individual (Fapi) (preencher também o quadro 7" +
    ")";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell9.Weight = 5.7018927001953124D;
            // 
            // xrTableCell303
            // 
            this.xrTableCell303.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell303.Name = "xrTableCell303";
            this.xrTableCell303.StylePriority.UseFont = false;
            this.xrTableCell303.StylePriority.UseTextAlignment = false;
            this.xrTableCell303.Text = "0,00";
            this.xrTableCell303.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell303.Weight = 1.9981048583984373D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell304});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "4. Pensão Alimentícia";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell13.Weight = 5.7018927001953124D;
            // 
            // xrTableCell304
            // 
            this.xrTableCell304.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell304.Name = "xrTableCell304";
            this.xrTableCell304.StylePriority.UseFont = false;
            this.xrTableCell304.StylePriority.UseTextAlignment = false;
            this.xrTableCell304.Text = "0,00";
            this.xrTableCell304.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell304.Weight = 1.9981048583984373D;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell305});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "5. Imposto sobre a renda retido na fonte";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell15.Weight = 5.7018927001953124D;
            // 
            // xrTableCell305
            // 
            this.xrTableCell305.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell305.Name = "xrTableCell305";
            this.xrTableCell305.StylePriority.UseFont = false;
            this.xrTableCell305.StylePriority.UseTextAlignment = false;
            this.xrTableCell305.Text = "0,00";
            this.xrTableCell305.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell305.Weight = 1.9981048583984373D;
            // 
            // xrTable8
            // 
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 690.376F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26,
            this.xrTableRow27});
            this.xrTable8.SizeF = new System.Drawing.SizeF(570.1892F, 43.56067F);
            this.xrTable8.StylePriority.UseBorders = false;
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37});
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.Weight = 1D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.Text = "6.1 Número do Processo:";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell35.Weight = 3.3863637028438403D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.Text = "Quantidade de meses";
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell36.Weight = 1.3212108577501105D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell37.Weight = 0.99431727414854221D;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38});
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 1D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell38.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "Natureza do Rendimento:";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell38.Weight = 5.7018918347424927D;
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel701Descricao,
            this.xrLabel701Valor});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0.000777068F, 878.0634F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(769.9986F, 134.6591F);
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // xrLabel701Descricao
            // 
            this.xrLabel701Descricao.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel701Descricao.Font = new System.Drawing.Font("Arial", 8F);
            this.xrLabel701Descricao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel701Descricao.Multiline = true;
            this.xrLabel701Descricao.Name = "xrLabel701Descricao";
            this.xrLabel701Descricao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel701Descricao.SizeF = new System.Drawing.SizeF(676.7045F, 134.659F);
            this.xrLabel701Descricao.StylePriority.UseBorders = false;
            this.xrLabel701Descricao.StylePriority.UseFont = false;
            // 
            // xrLabel701Valor
            // 
            this.xrLabel701Valor.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel701Valor.Font = new System.Drawing.Font("Arial", 8F);
            this.xrLabel701Valor.LocationFloat = new DevExpress.Utils.PointFloat(676.7045F, 0F);
            this.xrLabel701Valor.Name = "xrLabel701Valor";
            this.xrLabel701Valor.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel701Valor.SizeF = new System.Drawing.SizeF(93.29407F, 23.00002F);
            this.xrLabel701Valor.StylePriority.UseBorders = false;
            this.xrLabel701Valor.StylePriority.UseFont = false;
            this.xrLabel701Valor.StylePriority.UseTextAlignment = false;
            this.xrLabel701Valor.Text = "0,00";
            this.xrLabel701Valor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 21F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 23F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // InformeRendimentos
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(28, 29, 21, 23);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}