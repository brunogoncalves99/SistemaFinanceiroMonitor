using SistemaFinanceiroMonitor.Application.DTOs;

namespace SistemaFinanceiroMonitor.Web.Models
{
    public class AlertaViewModel
    {
        public List<AlertaDTO> Alertas { get; set; }
        public int UsuarioId { get; set; }

        public AlertaViewModel()
        {
            Alertas = new List<AlertaDTO>();
        }
    }
}
