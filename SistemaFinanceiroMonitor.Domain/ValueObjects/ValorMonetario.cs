using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFinanceiroMonitor.Domain.ValueObjects
{
    public class ValorMonetario
    {
        public decimal Valor { get; set; }
        public string Moeda { get; set; }


        public ValorMonetario(decimal valor, string moeda = "BRL")
        {
            if (valor < 0)
                throw new ArgumentException("Valor monetário não pode ser negativo");

            if (string.IsNullOrWhiteSpace(moeda))
                throw new ArgumentException("Moeda deve ser informada");

            Valor = Math.Round(valor, 4); 
            Moeda = moeda.ToUpper();
        }

        public decimal CalcularVariacaoPercentual(ValorMonetario valorAnterior)
        {
            if(valorAnterior.Valor == 0)
                return 0;

            return ((Valor - valorAnterior.Valor) / valorAnterior.Valor) * 100;
        }

        public bool MaiorQue(ValorMonetario outro) => Valor > outro.Valor;
        public bool MenorQue(ValorMonetario outro) => Valor < outro.Valor;

        public override string ToString() => $"{Moeda} {Valor:N4}";

        public override bool Equals(object obj)
        {
            if (obj is ValorMonetario other)
                return Valor == other.Valor && Moeda == other.Moeda;
            return false;
        }
        public override int GetHashCode() => HashCode.Combine(Valor, Moeda);
    }
}
