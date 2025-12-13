namespace SistemaFinanceiroMonitor.Application.DTOs
{
    public class HistoricoAlertaDTO
    {
        public int Id { get; set; }
        public int AlertaId { get; set; }
        public string DescricaoAlerta { get; set; }
        public decimal ValorNoMomento { get; set; }
        public DateTime DataDisparo { get; set; }
        public bool EmailEnviado { get; set; }
        public DateTime? DataEnvioEmail { get; set; }
    }
}
