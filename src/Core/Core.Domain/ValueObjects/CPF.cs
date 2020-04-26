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

        public override bool Equals(object obj)
        {
            var compareTo = obj as CPF;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            if (Numero == null) return false;
            if (compareTo.Numero == null) return false;

            return Numero.Equals(compareTo.Numero);
        }

        public static bool operator ==(CPF a, CPF b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(CPF a, CPF b)
        {
            return !(a == b);
        }

        public static implicit operator CPF(string cpfNumero) => new CPF(cpfNumero);

        public bool EhValido()
        {
            return CPFValidation.Validar(Numero);
        }
    }
}
