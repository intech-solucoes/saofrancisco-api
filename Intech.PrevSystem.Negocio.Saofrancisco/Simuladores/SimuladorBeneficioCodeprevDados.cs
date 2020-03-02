namespace Intech.PrevSystem.Negocio.Saofrancisco.Simuladores
{
    public class SimuladorBeneficioCodeprevDados
    {
        public int IdadeAposentadoria { get; set; }
        public decimal PercentualContrib { get; set; }
        public decimal PercentualSaque { get; set; }
        public decimal Aporte { get; set; }
        public decimal SaldoAcumulado { get; set; }
        public decimal SaldoAcumulado8 { get; set; }
        public decimal SalarioContribuicao { get; set; }
    }

    public class RendaMensalItem
    {
        public decimal Percentual { get; set; }
        public decimal Renda { get; set; }
        public decimal Renda8 { get; set; }
        public string StringTempoRecebimento { get; set; }
    }
}