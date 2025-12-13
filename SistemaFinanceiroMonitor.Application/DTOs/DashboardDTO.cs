using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiroMonitor.Application.DTOs
{
    public class DashboardDTO
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

        public DashboardDTO()
        {
            HistoricoDolar30Dias = new List<CotacaoDTO>();
            HistoricoSelic12Meses = new List<IndicadorDTO>();
        }
    }
}
