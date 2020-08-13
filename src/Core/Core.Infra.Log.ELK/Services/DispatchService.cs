using Core.Infra.Log.ELK.Models;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Core.Infra.Log.ELK.Services
{
    internal sealed class DispatchService : IDisposable
    {
        private readonly IModel _model;

        public DispatchService(IModel model)
        {
            _model = model;
        }

        public void Dispatch(LogEntry log, string queueToSend)
        {
            var serializedContent = System.Text.Json.JsonSerializer.Serialize(log);

            var basicProperties = _model.CreateBasicProperties();
            basicProperties.DeliveryMode = 2;

            byte[] payload = Encoding.UTF8.GetBytes(serializedContent);

            _model.BasicPublish("", queueToSend, basicProperties, payload);
        }

        public void Dispose()
        {
            _model.Dispose(); 
        }
    }
}
