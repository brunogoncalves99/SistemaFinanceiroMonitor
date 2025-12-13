using Hangfire;

namespace SistemaFinanceiroMonitor.Infrastructure.BackgroundJobs
{
    public static class HangfireConfig
    {
        public static void ConfigurarJobs()
        {
            // Atualizar cotações: todos os dias às 19h (após fechamento do mercado)
            RecurringJob.AddOrUpdate<AtualizarCotacoesJob>(
                "atualizar-cotacoes",
                job => job.ExecutarAsync(),
                "0 19 * * 1-5" // Segunda a sexta às 19h
            );

            // Atualizar indicadores: todo dia 1º do mês às 10h
            RecurringJob.AddOrUpdate<AtualizarIndicadoresJob>(
                "atualizar-indicadores",
                job => job.ExecutarAsync(),
                //"0 10 1 * *"
                "*/10 * * * *"// Dia 1 de cada mês às 10h
            );

            // Verificar alertas: a cada 30 minutos
            RecurringJob.AddOrUpdate<VerificarAlertasJob>(
                "verificar-alertas",
                job => job.ExecutarAsync(),
                "*/10 * * * *" // A cada 30 minutos
            );
        }
    }
}
