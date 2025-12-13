using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiroMonitor.Domain.Entities
{
    public class HistoricoAlerta
    {
        public int Id { get; private set; }
        public int AlertaId { get; private set; }
        public Alerta Alerta { get; private set; }
        public decimal ValorNoMomento { get; private set; }
        public DateTime DataDisparo { get; private set; }
        public bool EmailEnviado { get; private set; }
        public DateTime? DataEnvioEmail { get; private set; }

        private HistoricoAlerta() { }

        public HistoricoAlerta(int alertaId, decimal valorNoMomento)
        {
            AlertaId = alertaId;
            ValorNoMomento = valorNoMomento;
            DataDisparo = DateTime.Now;
            EmailEnviado = false;
        }

        public void RegistrarEnvioEmail()
        {
            EmailEnviado = true;
            DataEnvioEmail = DateTime.Now;
        }
    }
}
