namespace ControleDeContas.Models
{
    public class Movimentacao
    {
        public long Id { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime DataPagamento { get; set; }

        public decimal ValorDevido { get; set; }

        public decimal ValorPago { get; set; }

        public int TotalParcelas { get; set; }

        public int NumeroParcela { get; set; }

       
    }
}
