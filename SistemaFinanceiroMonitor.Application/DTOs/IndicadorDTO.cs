using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Application.DTOs
{
    public class IndicadorDTO
    {
        public int Id { get; set; }
        public TipoIndicador TipoIndicador { get; set; }
        public string NomeIndicador { get; set; }
        public decimal Valor { get; set; }
        public string ValorFormatado { get; set; }
        public DateTime DataReferencia { get; set; }
        public decimal? VariacaoPercentual { get; set; }
    }
}
