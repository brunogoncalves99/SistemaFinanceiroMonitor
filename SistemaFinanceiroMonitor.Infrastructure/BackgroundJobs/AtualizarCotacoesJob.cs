using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Domain.Interfaces.Repositories;
using SistemaFinanceiroMonitor.Infrastructure.ExternalServices.BancoCentralApi;

namespace SistemaFinanceiroMonitor.Infrastructure.BackgroundJobs
{
    public class AtualizarCotacoesJob
    {
        private readonly BancoCentralApiClient _apiClient;
        private readonly ICotacaoRepository _cotacaoRepository;

        public AtualizarCotacoesJob(
            BancoCentralApiClient apiClient,
            ICotacaoRepository cotacaoRepository)
        {
            _apiClient = apiClient;
            _cotacaoRepository = cotacaoRepository;
        }

        public async Task ExecutarAsync()
        {
            try
            {
                Console.WriteLine($"[{DateTime.Now}] Iniciando atualização de cotações...");

                var dataFim = DateTime.Now;
                var dataInicio = dataFim.AddDays(-30); 

                // Obter cotações do dólar
                var cotacoes = await _apiClient.ObterCotacaoDolarAsync(dataInicio, dataFim);

                foreach (var cotacao in cotacoes)
                {
                    // Verificar se já existe
                    var existe = await _cotacaoRepository.ExisteCotacaoAsync(
                        TipoMoeda.Dolar,
                        cotacao.DataCotacao
                    );

                    if (!existe)
                    {
                        await _cotacaoRepository.AdicionarAsync(cotacao);
                        Console.WriteLine($"Cotação adicionada: {cotacao.DataCotacao:dd/MM/yyyy} - R$ {cotacao.ValorVenda.Valor}");
                    }
                }

                Console.WriteLine($"[{DateTime.Now}] Atualização de cotações concluída!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar cotações: {ex.Message}");
            }
        }
    }
}
