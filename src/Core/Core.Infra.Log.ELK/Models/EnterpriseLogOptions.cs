namespace Core.Infra.Log.ELK.Models
{
    public class EnterpriseLogOptions
    {
        public string ProjectKey { get; set; }
        public bool Disabled { get; set; }

        public EnterpriseLogRabbitMQConfig RabbitMQ { get; set; }

        /// <summary>
        /// Informar uma lista dos headers a ser ignorado pelo Middleware de log
        /// </summary>
        public static string[] HeadersIgnore;
    }

    public class EnterpriseLogRabbitMQConfig
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Queue { get; set; }
    }
}
