using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rumox.API.HealthChecks
{
    public class MySQLHealthCheck : IHealthCheck
    {
        readonly string _connection;

        public MySQLHealthCheck(string connection)
        {
            _connection = connection;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var connection = new MySqlConnection(_connection))
                {
                    await connection.OpenAsync(cancellationToken);

                    var command = connection.CreateCommand();
                    command.CommandText = "select 1";

                    return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
                }
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
