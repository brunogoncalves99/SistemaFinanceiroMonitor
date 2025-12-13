using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Application.DTOs
{
    public class CriarAlertaDTO
    {
        public int UsuarioId { get; set; }
        public TipoAlerta TipoAlerta { get; set; }
        public TipoMoeda? TipoMoeda { get; set; }
        public TipoIndicador? TipoIndicador { get; set; }
        public decimal? ValorGatilho { get; set; }
        public decimal? PercentualGatilho { get; set; }
        public string Descricao { get; set; }
    }
}
