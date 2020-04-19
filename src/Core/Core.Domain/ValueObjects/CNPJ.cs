using Core.Domain.Models;
using Core.Domain.Validations;

namespace Core.Domain.ValueObjects
{
    public class CNPJ : ValueObject<CNPJ>
    {
        public string Numero { get; private set; }

        protected CNPJ() { }

        public CNPJ(string numero)
        {
            Numero = numero;
        }

        public override string ToString()
        {
            return Numero;
        }

        public bool IsValid()
        {
            return CNPJValidation.Validar(Numero);
        }
    }
}
