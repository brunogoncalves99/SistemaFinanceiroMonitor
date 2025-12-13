using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Infrastructure.ExternalServices.BancoCentralApi;

namespace SistemaFinanceiroMonitor.Infrastructure.BackgroundJobs
{
    public class AtualizarIndicadoresJob
    {
        private readonly BancoCentralApiClient _apiClient;
        private readonly IIndicadorRepository _indicadorRepository;

        public AtualizarIndicadoresJob(
            BancoCentralApiClient apiClient,
            IIndicadorRepository indicadorRepository)
        {
            _apiClient = apiClient;
            _indicadorRepository = indicadorRepository;
        }

        public async Task ExecutarAsync()
        {
            try
            {
                Console.WriteLine($"[{DateTime.Now}] Iniciando atualização de indicadores...");

                // Atualizar SELIC
                var indicadoresSelic = await _apiClient.ObterSelicAsync(3);
                foreach (var indicador in indicadoresSelic)
                {
                    var existe = await _indicadorRepository.ExisteIndicadorAsync(
                        TipoIndicador.Selic,
                        indicador.DataReferencia
                    );

                    if (!existe)
                    {
                        await _indicadorRepository.AdicionarAsync(indicador);
                        Console.WriteLine($"SELIC adicionada: {indicador.DataReferencia:dd/MM/yyyy} - {indicador.Valor}%");
                    }
                }

                // Atualizar IPCA
                var indicadoresIPCA = await _apiClient.ObterIPCAAsync(3);
                foreach (var indicador in indicadoresIPCA)
                {
                    var existe = await _indicadorRepository.ExisteIndicadorAsync(
                        TipoIndicador.IPCA,
                        indicador.DataReferencia
                    );

                    if (!existe)
                    {
                        await _indicadorRepository.AdicionarAsync(indicador);
                        Console.WriteLine($"IPCA adicionado: {indicador.DataReferencia:dd/MM/yyyy} - {indicador.Valor}%");
                    }
                }

                Console.WriteLine($"[{DateTime.Now}] Atualização de indicadores concluída!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar indicadores: {ex.Message}");
            }
        }
    }
}
