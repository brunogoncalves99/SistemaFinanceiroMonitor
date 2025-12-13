using SistemaFinanceiroMonitor.Application.DTOs;

namespace SistemaFinanceiroMonitor.Web.Models
{
    public class DashboardViewModel
    {
        public CotacaoDTO UltimaCotacaoDolar { get; set; }
        public CotacaoDTO UltimaCotacaoEuro { get; set; }
        public IndicadorDTO UltimaSelic { get; set; }
        public IndicadorDTO UltimoIPCA { get; set; }

        public List<CotacaoDTO> HistoricoDolar30Dias { get; set; }
        public List<IndicadorDTO> HistoricoSelic12Meses { get; set; }

        public int TotalAlertasAtivos { get; set; }
        public int TotalAlertasDisparados { get; set; }

        public DateTime DataAtualizacao { get; set; }

        // Dados para gráficos (JSON)
        public string GraficoDolarLabels { get; set; }
        public string GraficoDolarValores { get; set; }
        public string GraficoSelicLabels { get; set; }
        public string GraficoSelicValores { get; set; }
    }
}
