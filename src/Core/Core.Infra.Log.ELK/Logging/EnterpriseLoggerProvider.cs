using Core.Infra.Log.ELK.Models;
using Core.Infra.Log.ELK.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Core.Infra.Log.ELK.Web
{
    [ProviderAlias("EnterpriseLog")]
    internal class EnterpriseLoggerProvider : ILoggerProvider
    {
        private readonly DispatchService _dispatchService;
        private readonly EnterpriseLogOptions _options;
        private readonly ConcurrentDictionary<string, EnterpriseLogger> _loggers = new ConcurrentDictionary<string, EnterpriseLogger>();

        public EnterpriseLoggerProvider(
            DispatchService dispatchService,
            IOptions<EnterpriseLogOptions> options)
        {
            _dispatchService = dispatchService;
            _options = options.Value;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new EnterpriseLogger(name, _dispatchService, _options));
        }

        public void Dispose()
        { }
    }
}
