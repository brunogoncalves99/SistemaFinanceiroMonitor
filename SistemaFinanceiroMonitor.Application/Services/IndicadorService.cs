using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;

namespace SistemaFinanceiroMonitor.Application.Services
{
    public class IndicadorService : IIndicadorService
    {
        private readonly IIndicadorRepository _indicadorRepository;

        public IndicadorService(IIndicadorRepository indicadorRepository)
        {
            _indicadorRepository = indicadorRepository;
        }

        public async Task<IndicadorDTO> ObterUltimoIndicadorAsync(TipoIndicador tipoIndicador)
        {
            var indicador = await _indicadorRepository.ObterUltimoIndicadorAsync(tipoIndicador);

            if (indicador == null)
                return null;

            return ConverterParaDTO(indicador);
        }

        public async Task<IEnumerable<IndicadorDTO>> ObterIndicadoresPorPeriodoAsync(TipoIndicador tipoIndicador, DateTime dataInicio, DateTime dataFim)
        {
            var indicadores = await _indicadorRepository.ObterIndicadoresPorPeriodoAsync(tipoIndicador, dataInicio, dataFim);

            return indicadores.Select(i => ConverterParaDTO(i)).ToList();
        }

        public async Task<IEnumerable<IndicadorDTO>> ObterHistoricoUltimos12MesesAsync(TipoIndicador tipoIndicador)
        {
            var dataFim = DateTime.Now;
            var dataInicio = dataFim.AddMonths(-12);

            var indicadores = await _indicadorRepository.ObterIndicadoresPorPeriodoAsync(tipoIndicador, dataInicio, dataFim);
            var listaIndicadores = indicadores.OrderBy(i => i.DataReferencia).ToList();

            var resultado = new List<IndicadorDTO>();

            for (int i = 0; i < listaIndicadores.Count; i++)
            {
                var indicadorDTO = ConverterParaDTO(listaIndicadores[i]);

                // Calcular variação em relação ao mês anterior
                if (i > 0)
                {
                    var variacao = listaIndicadores[i].CalcularVariacaoPercentual(listaIndicadores[i - 1]);
                    indicadorDTO.VariacaoPercentual = variacao;
                }

                resultado.Add(indicadorDTO);
            }

            return resultado;
        }

        public async Task<bool> AtualizarIndicadoresAsync()
        {
            // Será implementado na Infrastructure com consumo da API BCB

            await Task.CompletedTask;
            return true;
        }

        private IndicadorDTO ConverterParaDTO(IndicadorEconomico indicador)
        {
            return new IndicadorDTO
            {
                Id = indicador.Id,
                TipoIndicador = indicador.TipoIndicador,
                NomeIndicador = ObterNomeIndicador(indicador.TipoIndicador),
                Valor = indicador.Valor,
                ValorFormatado = FormatarValorIndicador(indicador.TipoIndicador, indicador.Valor),
                DataReferencia = indicador.DataReferencia
            };
        }

        private string ObterNomeIndicador(TipoIndicador tipoIndicador)
        {
            return tipoIndicador switch
            {
                TipoIndicador.Selic => "Taxa SELIC",
                TipoIndicador.IPCA => "IPCA (Inflação)",
                TipoIndicador.IGPM => "IGP-M",
                TipoIndicador.CDI => "CDI",
                TipoIndicador.Poupanca => "Poupança",
                _ => tipoIndicador.ToString()
            };
        }

        private string FormatarValorIndicador(TipoIndicador tipo, decimal valor)
        {
            return tipo switch
            {
                TipoIndicador.Selic => $"{valor:F2}% a.a.",
                TipoIndicador.IPCA => $"{valor:F2}%",
                TipoIndicador.IGPM => $"{valor:F2}%",
                TipoIndicador.CDI => $"{valor:F2}% a.a.",
                TipoIndicador.Poupanca => $"{valor:F2}% a.a.",
                _ => $"{valor:F2}"
            };
        }

    }
}
