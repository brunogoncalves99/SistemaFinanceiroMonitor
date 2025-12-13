using System.Text.Json.Serialization;

namespace SistemaFinanceiroMonitor.Infrastructure.ExternalServices.BancoCentralApi.DTOs
{
    public class IndicadorResponseDTO
    {
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("valor")]
        public string Valor { get; set; }
    }
}
