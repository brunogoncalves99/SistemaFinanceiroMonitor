using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.ValueObjects;

namespace SistemaFinanceiroMonitor.Domain.Entities
{
    public class Cotacao
    {
        public int Id { get; private set; }
        public TipoMoeda TipoMoeda { get; private set; }
        public ValorMonetario ValorCompra { get; private set; }
        public ValorMonetario ValorVenda { get; private set; }
        public DateTime DataCotacao { get; private set; }
        public DateTime DataRegistro { get; private set; }

        private Cotacao() { }

        public Cotacao(TipoMoeda tipoMoeda, ValorMonetario valorCompra, ValorMonetario valorVenda, DateTime dataCotacao)
        {
            TipoMoeda = tipoMoeda;
            ValorCompra = valorCompra ?? throw new ArgumentNullException(nameof(valorCompra));
            ValorVenda = valorVenda ?? throw new ArgumentNullException(nameof(valorVenda));
            DataCotacao = dataCotacao;
            DataRegistro = DateTime.Now;
        }

        public decimal CalcularVariacaoPercentual(Cotacao cotacaoAnterior)
        {
            if (cotacaoAnterior == null)
                return 0;

            return ValorVenda.CalcularVariacaoPercentual(cotacaoAnterior.ValorVenda);
        }

        public decimal ObterValorMedio()
        {
            return (ValorCompra.Valor + ValorVenda.Valor) / 2;
        }
    }
}
