using Core.Infra.Log.ELK.Models;
using Core.Infra.Log.ELK.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Core.Infra.Log.ELK.Web
{
    internal class EnterpriseLogger : ILogger
    {
        private readonly string _name;
        private readonly DispatchService _dispatchService;
        private readonly EnterpriseLogOptions _options;

        public EnterpriseLogger(
            string name,
            DispatchService dispatchService,
            EnterpriseLogOptions options)
        {
            _name = name;
            _dispatchService = dispatchService;
            _options = options;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var log = new LogEntry();
            log.ProjectKey = _options.ProjectKey;
            log.Date = DateTime.UtcNow;
            log.Context = _name;

            log.Tags = new Dictionary<string, object>()
            {
                { "EventId", eventId }
            };

            if (exception == null)
                log.Tags.Add("Message", formatter(state, exception));
            else
                log.Tags.Add("Exception", exception.ToString());

            log.LogLevel = logLevel;

            _dispatchService.Dispatch(log, _options.RabbitMQ.Queue);
        }
    }
}
