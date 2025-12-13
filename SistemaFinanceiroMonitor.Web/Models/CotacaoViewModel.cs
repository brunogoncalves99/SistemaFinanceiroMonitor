using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Web.Models
{
    public class CotacaoViewModel
    {
        public TipoMoeda TipoMoedaSelecionada { get; set; }
        public List<CotacaoDTO> Cotacoes { get; set; }
        public CotacaoDTO UltimaCotacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public CotacaoViewModel()
        {
            Cotacoes = new List<CotacaoDTO>();
        }
    }
}
