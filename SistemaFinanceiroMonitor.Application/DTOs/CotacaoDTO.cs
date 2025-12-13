using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Application.DTOs
{
    public class CotacaoDTO
    {
        public int Id { get; set; }
        public TipoMoeda TipoMoeda { get; set; }
        public string NomeMoeda { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorMedio { get; set; }
        public DateTime DataCotacao { get; set; }
        public decimal? VariacaoPercentual { get; set; }
        public string VariacaoFormatada { get; set; }
    }
}
