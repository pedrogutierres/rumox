using Core.Infra.MySQL;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;

namespace Rumox.API.Tests.Catalogo.Setup
{
    // TODO: Refatorar
    public class MySQLSetup : IDisposable
    {
        private DbConnection Db;

        public MySQLSetup(IConfiguration configuration)
        {
            var connectionString = configuration?.GetMySQLDbConnectionString();

            Db = new MySqlConnection(connectionString);

            LimparBaseDeDados();
        }

        private void LimparBaseDeDados()
        {
            var sql = @"
                DELETE FROM Produtos;
                DELETE FROM Categorias;";

            Db.Execute(sql);
        }

        public void Dispose()
        {
            if (Db.State == ConnectionState.Open)
                Db.Close();

            Db.Dispose();
        }
    }
}
