using System;

namespace Core.Domain.ValueObjects
{
    public sealed class Periodo
    {
        public DateTime DataInicial { get; private set; }
        public DateTime DataFinal { get; private set; }

        public Periodo(DateTime dataInicial, int diasAcrescer)
        {
            DataInicial = dataInicial;
            DataFinal = DataInicial.AddDays(diasAcrescer);
        }
        public Periodo(DateTime dataInicial, DateTime dataFinal)
        {
            DataInicial = dataInicial;
            DataFinal = dataFinal;
        }

        public override string ToString()
        {
            return $"Data de {DataInicial:dd/MM/yyyy} até {DataFinal:dd/MM/yyyy}";
        }
    }
}
