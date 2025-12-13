using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Web.Models
{
    public class IndicadorViewModel
    {
        public TipoIndicador TipoIndicadorSelecionado { get; set; }
        public List<IndicadorDTO> Indicadores { get; set; }
        public IndicadorDTO UltimoIndicador { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public IndicadorViewModel()
        {
            Indicadores = new List<IndicadorDTO>();
        }
    }
}
