namespace SistemaFinanceiroMonitor.Infrastructure.ExternalServices.BancoCentralApi
{
    public class BancoCentralApiSettings
    {
        public string BaseUrl { get; set; }
        public string PtaxBaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}
