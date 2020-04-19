using Polly;
using Polly.CircuitBreaker;
using System;

namespace Core.Cache
{
    public static class CacheCircuitBreakerPolicy<T>
    {
        public static CircuitBreakerPolicy<T> CachePolicy = 
            Policy<T>
                .Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromMinutes(10));

        public static AsyncCircuitBreakerPolicy<T> CachePolicyAsync =
            Policy<T>
                .Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(10));
    }
}
