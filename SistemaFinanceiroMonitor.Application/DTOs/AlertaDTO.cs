using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Application.DTOs
{
    public class AlertaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public TipoAlerta TipoAlerta { get; set; }
        public string TipoAlertaDescricao { get; set; }
        public TipoMoeda? TipoMoeda { get; set; }
        public string NomeMoeda { get; set; }
        public TipoIndicador? TipoIndicador { get; set; }
        public string NomeIndicador { get; set; }
        public decimal? ValorGatilho { get; set; }
        public decimal? PercentualGatilho { get; set; }
        public StatusAlerta Status { get; set; }
        public string StatusDescricao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUltimoDisparo { get; set; }
    }
}
