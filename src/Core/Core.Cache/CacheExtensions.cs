using JsonNet.PrivateSettersContractResolvers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace Core.Cache
{
    public static class CacheExtensions
    {
        public static TItem GetOrCreate<TItem>(this IDistributedCache cache, string key, Func<TItem> factory, int minutesExpiration = 0)
        {
            var circuitBreaker = CacheCircuitBreakerPolicy<string>.CachePolicy;

            var policy = Policy<string>
                .Handle<Exception>()
                .Fallback(string.Empty)
                .Wrap(circuitBreaker);

            var result = policy.Execute(() =>
            {
                return cache.GetString(key);
            });

            if (!string.IsNullOrEmpty(result))
                return JsonConvert.DeserializeObject<TItem>(result, JsonSettingsPrivateSetters);

            var obj = factory();

            if (circuitBreaker.CircuitState != CircuitState.Closed)
                return obj;

            policy.Execute(() =>
            {
                var value = JsonConvert.SerializeObject(obj);

                var opcoesCache = new DistributedCacheEntryOptions();

                if (minutesExpiration > 0)
                    opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutesExpiration));

                cache.SetString(key, value, opcoesCache);

                return value;
            });

            return obj;
        }

        public static async Task<TItem> GetOrCreateAsync<TItem>(this IDistributedCache cache, string key, Func<Task<TItem>> factory, int minutesExpiration = 0)
        {
            var circuitBreaker = CacheCircuitBreakerPolicy<string>.CachePolicyAsync;

            var policy = Policy<string>
                .Handle<Exception>()
                .FallbackAsync(string.Empty)
                .WrapAsync(circuitBreaker);

            var result = await policy.ExecuteAsync(() =>
            {
                return cache.GetStringAsync(key);
            });

            if (!string.IsNullOrEmpty(result))
                return JsonConvert.DeserializeObject<TItem>(result, JsonSettingsPrivateSetters);

            var obj = await factory();

            if (circuitBreaker.CircuitState != CircuitState.Closed)
                return obj;

            await policy.ExecuteAsync(async () =>
            {
                var value = JsonConvert.SerializeObject(obj);

                var opcoesCache = new DistributedCacheEntryOptions();

                if (minutesExpiration > 0)
                    opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutesExpiration));

                await cache.SetStringAsync(key, value, opcoesCache);

                return value;
            });

            return obj;
        }

        public static void CreateOrUpdate<TItem>(this IDistributedCache cache, string key, TItem obj, int minutesExpiration = 0, int secondsExpiration = 0)
        {
            var policy = CacheCircuitBreakerPolicy<string>.CachePolicy;

            policy.ExecuteAndCapture(() =>
            {
                cache.Remove(key);

                var value = JsonConvert.SerializeObject(obj);
                var opcoesCache = new DistributedCacheEntryOptions();

                if (minutesExpiration > 0)
                    opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutesExpiration));
                else if (secondsExpiration > 0)
                    opcoesCache.SetAbsoluteExpiration(TimeSpan.FromSeconds(secondsExpiration));

                cache.SetString(key, value, opcoesCache);

                return value;
            });
        }

        public static async Task CreateOrUpdateAsync<TItem>(this IDistributedCache cache, string key, TItem obj, int minutesExpiration = 0, int secondsExpiration = 0)
        {
            var policy = CacheCircuitBreakerPolicy<string>.CachePolicyAsync;

            await policy.ExecuteAndCaptureAsync(async () =>
            {
                await cache.RemoveAsync(key);

                var value = JsonConvert.SerializeObject(obj);
                var opcoesCache = new DistributedCacheEntryOptions();

                if (minutesExpiration > 0)
                    opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutesExpiration));
                else if (secondsExpiration > 0)
                    opcoesCache.SetAbsoluteExpiration(TimeSpan.FromSeconds(secondsExpiration));

                await cache.SetStringAsync(key, value, opcoesCache);

                return value;
            });
        }

        public static TItem Get<TItem>(this IDistributedCache cache, string key, Func<TItem> factory)
        {
            var circuitBreaker = CacheCircuitBreakerPolicy<string>.CachePolicy;

            var policy = Policy<string>
                .Handle<Exception>()
                .Fallback(string.Empty)
                .Wrap(circuitBreaker);

            var result = policy.Execute(() =>
            {
                return cache.GetString(key);
            });

            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<TItem>(result, JsonSettingsPrivateSetters) : factory();
        }

        public static async Task<TItem> GetAsync<TItem>(this IDistributedCache cache, string key, Func<Task<TItem>> factory)
        {
            var circuitBreaker = CacheCircuitBreakerPolicy<string>.CachePolicyAsync;

            var policy = Policy<string>
                .Handle<Exception>()
                .FallbackAsync(string.Empty)
                .WrapAsync(circuitBreaker);

            var result = await policy.ExecuteAsync(async () =>
            {
                return await cache.GetStringAsync(key);
            });

            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<TItem>(result, JsonSettingsPrivateSetters) : await factory();
        }

        private static readonly JsonSerializerSettings JsonSettingsPrivateSetters = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };
    }
}