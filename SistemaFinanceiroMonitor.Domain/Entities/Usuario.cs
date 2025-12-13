using SistemaFinanceiroMonitor.Domain.ValueObjects;

namespace SistemaFinanceiroMonitor.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataAlteracao { get; private set; } 
        public bool Ativo { get; private set; }

        public ICollection<Alerta> Alertas { get; private set; }

        private Usuario()
        {
            Alertas = new List<Alerta>();
        }
        public Usuario(string nome, Email email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório");

            Nome = nome;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            DataCadastro = DateTime.Now;
            DataAlteracao = null;
            Ativo = true;
            Alertas = new List<Alerta>();
        }

        public void Desativar()
        {
            Ativo = false;
            DataAlteracao = DateTime.Now;
        }
        public void Ativar()
        {
            Ativo = true;
            DataAlteracao = DateTime.Now;
        }
        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("Nome não pode ser vazio");

            Nome = novoNome;
            DataAlteracao = DateTime.Now;
        }

    }
}
