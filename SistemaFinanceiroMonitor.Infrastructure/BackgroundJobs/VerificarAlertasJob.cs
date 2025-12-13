using SistemaFinanceiroMonitor.Application.Interface;

namespace SistemaFinanceiroMonitor.Infrastructure.BackgroundJobs
{
    public class VerificarAlertasJob
    {
        private readonly IAlertaService _alertaService;

        public VerificarAlertasJob(IAlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        public async Task ExecutarAsync()
        {
            try
            {
                await _alertaService.VerificarEDispararAlertasAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar alertas: {ex.Message}");
            }
        }
    }
}
