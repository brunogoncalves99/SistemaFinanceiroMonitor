using System.Text.Json.Serialization;

namespace SistemaFinanceiroMonitor.Infrastructure.ExternalServices.BancoCentralApi.DTOs
{
    public class CotacaoResponseDTO
    {
        [JsonPropertyName("cotacaoCompra")]
        public decimal CotacaoCompra { get; set; }

        [JsonPropertyName("cotacaoVenda")]
        public decimal CotacaoVenda { get; set; }

        [JsonPropertyName("dataHoraCotacao")]
        public string DataHoraCotacao { get; set; }
    }

    public class BancoCentralCotacaoResponse
    {
        [JsonPropertyName("value")]
        public List<CotacaoResponseDTO> Value { get; set; }
    }
}
