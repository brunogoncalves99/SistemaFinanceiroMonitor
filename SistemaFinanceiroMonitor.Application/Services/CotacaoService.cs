using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;

namespace SistemaFinanceiroMonitor.Application.Services
{
    public class CotacaoService : ICotacaoService
    {
        private readonly ICotacaoRepository _cotacaoRepository;

        public CotacaoService(ICotacaoRepository cotacaoRepository)
        {
            _cotacaoRepository = cotacaoRepository;
        }

        public async Task<CotacaoDTO> ObterUltimaCotacaoAsync(TipoMoeda tipoMoeda)
        {
            var cotacao = await _cotacaoRepository.ObterUltimaCotacaoAsync(tipoMoeda);

            if (cotacao == null)
                return null;

            return ConverterParaDTO(cotacao);
        }

        public async Task<IEnumerable<CotacaoDTO>> ObterCotacoesPorPeriodoAsync(TipoMoeda tipoMoeda, DateTime dataInicio, DateTime dataFim)
        {
            var cotacoes = await _cotacaoRepository.ObterCotacoesPorPeriodoAsync(tipoMoeda, dataInicio, dataFim);

            return cotacoes.Select(c => ConverterParaDTO(c)).ToList();
        }

        public async Task<IEnumerable<CotacaoDTO>> ObterHistoricoUltimos30DiasAsync(TipoMoeda tipoMoeda)
        {
            var dataFim = DateTime.Now;
            var dataInicio = dataFim.AddDays(-30);

            var cotacoes = await _cotacaoRepository.ObterCotacoesPorPeriodoAsync(tipoMoeda, dataInicio, dataFim);
            var listaCotacoes = cotacoes.OrderBy(c => c.DataCotacao).ToList();

            var resultado = new List<CotacaoDTO>();

            for (int i = 0; i < listaCotacoes.Count; i++)
            {
                var cotacaoDTO = ConverterParaDTO(listaCotacoes[i]);

                if (i > 0)
                {
                    var variacao = listaCotacoes[i].CalcularVariacaoPercentual(listaCotacoes[i - 1]);
                    cotacaoDTO.VariacaoPercentual = variacao;
                    cotacaoDTO.VariacaoFormatada = FormatarVariacao(variacao);
                }

                resultado.Add(cotacaoDTO);
            }

            return resultado;
        }

        public async Task<bool> AtualizarCotacoesAsync()
        {
            // Este método será implementado na camada Infrastructure
            // que consumirá a API do Banco Central
            // Por agora, apenas retorna true

            await Task.CompletedTask;
            return true;
        }

        private CotacaoDTO ConverterParaDTO(Cotacao cotacao)
        {
            return new CotacaoDTO
            {
                Id = cotacao.Id,
                TipoMoeda = cotacao.TipoMoeda,
                NomeMoeda = ObterNomeMoeda(cotacao.TipoMoeda),
                ValorCompra = cotacao.ValorCompra.Valor,
                ValorVenda = cotacao.ValorVenda.Valor,
                ValorMedio = cotacao.ObterValorMedio(),
                DataCotacao = cotacao.DataCotacao
            };
        }

        private string ObterNomeMoeda(TipoMoeda tipoMoeda)
        {
            return tipoMoeda switch
            {
                TipoMoeda.Dolar => "Dólar Americano (USD)",
                TipoMoeda.Euro => "Euro (EUR)",
                TipoMoeda.Libra => "Libra Esterlina (GBP)",
                TipoMoeda.Bitcoin => "Bitcoin (BTC)",
                _ => tipoMoeda.ToString()
            };
        }

        private string FormatarVariacao(decimal variacao)
        {
            var sinal = variacao >= 0 ? "+" : "";
            return $"{sinal}{variacao:F2}%";
        }
    }
}
