using Core.Domain.Interfaces;
using Core.Infra.Log.ELK.Models;
using Core.Infra.Log.ELK.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infra.Log.ELK.Web
{
    internal class EnterpriseLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DispatchService _dispatchService;
        private readonly EnterpriseLogOptions _options;

        public EnterpriseLogMiddleware(
            RequestDelegate next,
            DispatchService dispatchService,
            EnterpriseLogOptions options)
        {
            _next = next;
            _dispatchService = dispatchService;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context, IUser user)
        {
            // TODO
            // aqui irá logar somente entradas (requests) e saídas (responses) da API
            // está pegando o length do body tanto do request, quanto do response
            // o user iniciará nulo e apenas estará preenchido na volta após executar todos os outros middlewares
            // comunicacao com o rabbit foi abstraida de forma simples no dispatch service
            // caso de excessão irá logar o body da requisição
            // os logs de negócio do dominio serão logados pelo log provider

            var stopwatch = Stopwatch.StartNew();

            var log = new LogEntry();
            log.ProjectKey = _options.ProjectKey;
            log.Date = DateTime.UtcNow;
            log.Context = "Request";

            PreExecutionLog(context, log);

            if (context.Request.Method != "GET") context.Request.EnableBuffering();

            try
            {
                await _next(context);
                log.LogLevel = LogLevel.Debug;
            }
            catch (Exception ex)
            {
                log.LogLevel = LogLevel.Error;
                AddOrUpdateTag(log, $"Exception", ex.ToString());
                await RegisterContent(context, log);
                throw;
            }
            finally
            {
                PostExecutionLog(context, log, user, stopwatch);
                _dispatchService.Dispatch(log, _options.RabbitMQ.Queue);
            }
        }

        private static void PreExecutionLog(HttpContext context, LogEntry log)
        {
            log.Tags = new Dictionary<string, object>()
            {
                { "Scheme", context.Request.Scheme },
                { "Host", context.Request.Host.ToString() },
                { "Path", context.Request.Path },
                { "ClientIP", context.Connection.RemoteIpAddress?.ToString() },
                { "RequestLength", context.Request.ContentLength ?? 0}
            };

            if (context.Request.Headers != null)
            {
                foreach (var header in context.Request.Headers)
                {
                    if (EnterpriseLogOptions.HeadersIgnore != null && EnterpriseLogOptions.HeadersIgnore.Contains(header.Key))
                        continue;

                    AddOrUpdateTag(log, $"header.{header.Key}", header.Value.ToString());
                }
            }

            if (context.Request.Query != null)
            {
                foreach (var query in context.Request.Query)
                    AddOrUpdateTag(log, $"query.{query.Key}", query.Value);
            }
        }
        private static void PostExecutionLog(HttpContext context, LogEntry log, IUser user, Stopwatch stopwatch)
        {
            try
            {
                if (user?.Autenticado() ?? false)
                {
                    AddOrUpdateTag(log, "UsuarioId", user.UsuarioId().ToString());
                    AddOrUpdateTag(log, "UsuarioNome", user.Nome);
                }

                if (context.Items?.Any() ?? false)
                {
                    foreach (var item in context.Items)
                    {
                        if (item.Key is string key)
                            AddOrUpdateTag(log, key, item.Value);
                    }
                }

                AddOrUpdateTag(log, "StatusCode", context.Response?.StatusCode ?? 0);
                AddOrUpdateTag(log, "ResponseLength", context.Response?.ContentLength ?? 0);
            }
            catch (Exception ex)
            {
                AddOrUpdateTag(log, "ExceptionPostExecutionLog", ex.ToString());
            }

            stopwatch.Stop();

            log.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        }
        private static void AddOrUpdateTag(LogEntry log, string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                return;

            if (!log.Tags.ContainsKey(key))
                log.Tags.Add(key, value);
            else
                log.Tags[key] = value;
        }

        private static async Task RegisterContent(HttpContext context, LogEntry log)
        {
            if (context.Request.Method != "GET")
            {
                // Caso queira deixar em aberto o stream reader para outro middleware ler, habilitar o leaveOpen
                //using var reader = new StreamReader(
                //       context.Request.Body,
                //       encoding: Encoding.UTF8,
                //       detectEncodingFromByteOrderMarks: false,
                //       bufferSize: -1,
                //       leaveOpen: true);

                using var reader = new StreamReader(context.Request.Body);
                reader.BaseStream.Position = 0;
                log.Content = await reader.ReadToEndAsync();
            }
        }
    }
}
