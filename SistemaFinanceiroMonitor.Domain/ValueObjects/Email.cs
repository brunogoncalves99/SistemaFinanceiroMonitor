using System.Text.RegularExpressions;

namespace SistemaFinanceiroMonitor.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; private set; }

        public Email(string endereco) 
        {
            if (string.IsNullOrEmpty(endereco))
                throw new ArgumentNullException("E-mail não pode ser vazio");

            if (!ValidarEmail(endereco))
                throw new ArgumentException("E-mail invalido");

            this.Endereco = endereco.Trim();
        }

        private bool ValidarEmail(string mail)
        {
            var regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(mail, regex);
        }

        public override string ToString() => Endereco;

        public override bool Equals(object obj)
        {
            if (obj is Email other)
                return Endereco == other.Endereco;
            return false;
        }

        public override int GetHashCode() => Endereco.GetHashCode();

    }
}
