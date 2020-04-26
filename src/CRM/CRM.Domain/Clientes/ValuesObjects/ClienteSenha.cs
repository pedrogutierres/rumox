using Core.Domain.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Domain.Clientes.ValuesObjects
{
    public class ClienteSenha : ValueObject<ClienteSenha>
    {
        public string Senha { get; private set; }

        public ClienteSenha()
        {
        }

        public ClienteSenha(string senha)
        {
            Senha = senha;
        }

        public bool EhValido()
        {
            return !string.IsNullOrEmpty(Senha) && Senha.Length == 32 && !Senha.Contains(" ");
        }

        public override bool Equals(object obj)
        {
            return (Senha == (obj as ClienteSenha)?.Senha);
        }

        public class Factory
        {
            public static ClienteSenha NovaSenha(string senha, DateTime dataHora)
            {
                if (string.IsNullOrEmpty(senha.Trim()))
                    return new ClienteSenha("");

                var senhaMD5 = CriptografarMD5($"{senha}-rumox-{dataHora:ddMMyyyy}");

                return new ClienteSenha(senhaMD5);
            }
        }

        private static string CriptografarMD5(string texto)
        {
            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
                var sBuilder = new StringBuilder();
                foreach (var t in data)
                    sBuilder.Append(t.ToString("x2"));
                return sBuilder.ToString();
            }
        }
    }
}
