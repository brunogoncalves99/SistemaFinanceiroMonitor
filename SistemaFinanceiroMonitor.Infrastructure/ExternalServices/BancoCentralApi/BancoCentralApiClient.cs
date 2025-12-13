using SistemaFinanceiroMonitor.Domain.Entities;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.ValueObjects;
using SistemaFinanceiroMonitor.Infrastructure.ExternalServices.BancoCentralApi.DTOs;
using System.Globalization;
using System.Text.Json;

namespace SistemaFinanceiroMonitor.Infrastructure.ExternalServices.BancoCentralApi
{
    public class BancoCentralApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly BancoCentralApiSettings _settings;

        public BancoCentralApiClient(HttpClient httpClient, BancoCentralApiSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _httpClient.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);
        }

        public async Task<List<Cotacao>> ObterCotacaoDolarAsync(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                var dataIni = dataInicial.ToString("MM-dd-yyyy");
                var dataFim = dataFinal.ToString("MM-dd-yyyy");

                var url = $"{_settings.PtaxBaseUrl}/CotacaoDolarPeriodo(dataInicial=@dataInicial,dataFinalCotacao=@dataFinalCotacao)?@dataInicial='{dataIni}'&@dataFinalCotacao='{dataFim}'&$format=json";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<BancoCentralCotacaoResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var cotacoes = new List<Cotacao>();

                foreach (var item in resultado.Value)
                {
                    var dataCotacao = DateTime.Parse(item.DataHoraCotacao);
                    var valorCompra = new ValorMonetario(item.CotacaoCompra, "BRL");
                    var valorVenda = new ValorMonetario(item.CotacaoVenda, "BRL");

                    var cotacao = new Cotacao(TipoMoeda.Dolar, valorCompra, valorVenda, dataCotacao);
                    cotacoes.Add(cotacao);
                }

                return cotacoes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter cotação do dólar: {ex.Message}");
                throw;
            }
        }

        public async Task<List<IndicadorEconomico>> ObterSelicAsync(int quantidadeMeses = 12)
        {
            try
            {
                // Série 11 = SELIC
                var url = $"{_settings.BaseUrl}/dados/serie/bcdata.sgs.11/dados/ultimos/{quantidadeMeses}?formato=json";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<List<IndicadorResponseDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var indicadores = new List<IndicadorEconomico>();

                foreach (var item in resultado)
                {
                    var dataReferencia = DateTime.ParseExact(item.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var valor = decimal.Parse(item.Valor, CultureInfo.InvariantCulture);

                    var indicador = new IndicadorEconomico(TipoIndicador.Selic, valor, dataReferencia);
                    indicadores.Add(indicador);
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter SELIC: {ex.Message}");
                throw;
            }
        }

        public async Task<List<IndicadorEconomico>> ObterIPCAAsync(int quantidadeMeses = 12)
        {
            try
            {
                // Série 433 = IPCA
                var url = $"{_settings.BaseUrl}/dados/serie/bcdata.sgs.433/dados/ultimos/{quantidadeMeses}?formato=json";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<List<IndicadorResponseDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var indicadores = new List<IndicadorEconomico>();

                foreach (var item in resultado)
                {
                    var dataReferencia = DateTime.ParseExact(item.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var valor = decimal.Parse(item.Valor, CultureInfo.InvariantCulture);

                    var indicador = new IndicadorEconomico(TipoIndicador.IPCA, valor, dataReferencia);
                    indicadores.Add(indicador);
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter IPCA: {ex.Message}");
                throw;
            }
        }
    }
}
