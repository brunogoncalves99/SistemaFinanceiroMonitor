using SistemaFinanceiroMonitor.Domain.Enums;

namespace SistemaFinanceiroMonitor.Domain.Entities
{
    public class Alerta
    {

        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }

        public TipoAlerta TipoAlerta { get; private set; }
        public TipoMoeda? TipoMoeda { get; private set; } 
        public TipoIndicador? TipoIndicador { get; private set; } 

        public decimal? ValorGatilho { get; private set; }  
        public decimal? PercentualGatilho { get; private set; } 

        public StatusAlerta Status { get; private set; }
        public string Descricao { get; private set; }

        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUltimoDisparo { get; private set; }

        public ICollection<HistoricoAlerta> Historico { get; private set; }

        private Alerta()
        {
            Historico = new List<HistoricoAlerta>();
        }

        public Alerta(int usuarioId, TipoAlerta tipoAlerta, TipoMoeda tipoMoeda, decimal valorGatilho, string descricao)
        {
            UsuarioId = usuarioId;
            TipoAlerta = tipoAlerta;
            TipoMoeda = tipoMoeda;
            ValorGatilho = valorGatilho;
            Descricao = descricao;
            Status = StatusAlerta.Ativo;
            DataCriacao = DateTime.Now;
            Historico = new List<HistoricoAlerta>();
        }

        public Alerta(int usuarioId, TipoAlerta tipoAlerta, TipoIndicador tipoIndicador, decimal valorGatilho, string descricao)
        {
            UsuarioId = usuarioId;
            TipoAlerta = tipoAlerta;
            TipoIndicador = tipoIndicador;
            ValorGatilho = valorGatilho;
            Descricao = descricao;
            Status = StatusAlerta.Ativo;
            DataCriacao = DateTime.Now;
            Historico = new List<HistoricoAlerta>();
        }

        public bool VerificarSeDeveDisparar(decimal valorAtual)
        {
            if (Status != StatusAlerta.Ativo)
                return false;

            switch (TipoAlerta)
            {
                case TipoAlerta.CotacaoAcimaDe:
                    return ValorGatilho.HasValue && valorAtual > ValorGatilho.Value;

                case TipoAlerta.CotacaoAbaixoDe:
                    return ValorGatilho.HasValue && valorAtual < ValorGatilho.Value;

                default:
                    return false;
            }
        }

        public void Disparar()
        {
            Status = StatusAlerta.Disparado;
            DataUltimoDisparo = DateTime.Now;
        }

        public void Pausar() => Status = StatusAlerta.Pausado;
        public void Reativar() => Status = StatusAlerta.Ativo;
        public void Inativar() => Status = StatusAlerta.Inativo;
    }
}
