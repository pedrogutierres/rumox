using Core.Domain.Models;
using Core.Identity;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Domain.Clientes.ValuesObjects
{
    public class ClienteSenha : ValueObject<ClienteSenha>, IUserPassword
    {
        public byte[] Salt { get; private set; }
        public byte[] Hash { get; private set; }

        private ClienteSenha()
        { }
        public ClienteSenha(byte[] salt, byte[] hash)
        {
            Salt = salt;
            Hash = hash;
        }

        public bool EhValido()
        {
            return Salt?.Length > 0 && Hash?.Length > 0;
        }

        public override bool Equals(object obj)
        {
            return (Hash == (obj as ClienteSenha)?.Hash);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Salt, Hash);
        }

        public static bool VerificarSenha(string senha, byte[] salt, byte[] hash) => PasswordHelper.VerifyHash(senha, salt, hash);
        public static bool VerificarSenha(string senha, ClienteSenha clienteSenha) => VerificarSenha(senha, clienteSenha.Salt, clienteSenha.Hash);

        public class Factory
        {
            public static ClienteSenha NovaSenha(string senha)
            {
                 if (string.IsNullOrEmpty(senha))
                    return new ClienteSenha(null, null);

                var salt = PasswordHelper.CreateSalt();
                var hash = PasswordHelper.HashPassword(senha, salt);

                return new ClienteSenha(salt, hash);
            }
        }
    }
}
