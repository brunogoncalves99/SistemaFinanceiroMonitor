using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Domain.Entities
{
    public class IndicadorEconomico
    {
        public int Id { get; private set; }
        public TipoIndicador TipoIndicador { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataReferencia { get; private set; }
        public DateTime DataRegistro { get; private set; }

        private IndicadorEconomico() { }

        public IndicadorEconomico(TipoIndicador tipoIndicador, decimal valor, DateTime dataReferencia)
        {
            if (valor < 0)
                throw new ArgumentException("Valor do indicador não pode ser negativo");

            TipoIndicador = tipoIndicador;
            Valor = Math.Round(valor, 2);
            DataReferencia = dataReferencia;
            DataRegistro = DateTime.Now;
        }

        public decimal CalcularVariacaoPercentual(IndicadorEconomico indicadorAnterior)
        {
            if (indicadorAnterior == null || indicadorAnterior.Valor == 0)
                return 0;

            return ((Valor - indicadorAnterior.Valor) / indicadorAnterior.Valor) * 100;
        }
    }
}