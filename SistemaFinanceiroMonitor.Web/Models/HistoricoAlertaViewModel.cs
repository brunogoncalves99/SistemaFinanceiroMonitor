using SistemaFinanceiroMonitor.Application.DTOs;

namespace SistemaFinanceiroMonitor.Web.Models
{
    public class HistoricoAlertaViewModel
    {
        public AlertaDTO Alerta { get; set; }
        public List<HistoricoAlertaDTO> Historico { get; set; }

        public HistoricoAlertaViewModel()
        {
            Historico = new List<HistoricoAlertaDTO>();
        }
    }
}
