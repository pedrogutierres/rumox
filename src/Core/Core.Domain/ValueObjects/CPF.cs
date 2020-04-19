using Core.Domain.Models;
using Core.Domain.Validations;

namespace Core.Domain.ValueObjects
{
    public class CPF : ValueObject<CPF>
    {
        public string Numero { get; private set; }

        protected CPF() { }

        public CPF(string numero)
        {
            Numero = numero;
        }

        public override string ToString()
        {
            return Numero;
        }

        public bool IsValid()
        {
            return CPFValidation.Validar(Numero);
        }
    }
}
