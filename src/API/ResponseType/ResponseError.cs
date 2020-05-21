using Core.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace Rumox.API.ResponseType
{
    public class ResponseError : ProblemDetails
    {
        private ResponseError(string title, string detail, int status, string path)
        {
            Title = title;
            Detail = detail;
            Status = status;
            Instance = path;
        }
        public ResponseError(string title, string detail, int status, string path, string error)
            : this(title, detail, status, path)
        {
            Errors.Add("error", new[] { error });
        }
        public ResponseError(string title, string detail, int status, string path, IDictionary<string, IEnumerable<string>> errors)
            : this(title, detail, status, path)
        {
            Errors = errors.ToDictionary(p => p.Key.ToFirstLetterLower(), p => p.Value.ToArray());
        }
        public ResponseError(string title, string detail, int status, string path, IEnumerable<DomainNotification> notificationErrors)
            : this(title, detail, status, path)
        {
            var errors = new Dictionary<string, Collection<string>>();

            foreach (var notificacao in notificationErrors)
            {
                if (errors.ContainsKey(notificacao.Key))
                    errors[notificacao.Key].Add(notificacao.Value);
                else
                    errors.Add(notificacao.Key, new Collection<string> { notificacao.Value });
            }

            Errors = errors.ToDictionary(p => p.Key.ToFirstLetterLower(), v => v.Value.ToArray());
        }
        public ResponseError(string title, string detail, int status, string path, ModelStateDictionary modelState)
             : this(title, detail, status, path)
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        Errors.Add(key.ToFirstLetterLower(), new[] { errorMessage });
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }

                        Errors.Add(key.ToFirstLetterLower(), errorMessages);
                    }
                }
            }

            static string GetErrorMessage(ModelError error)
            {
                return error.Exception?.Message ?? error.ErrorMessage;
            }
        }

        [JsonPropertyName("errors")]
        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>(StringComparer.Ordinal);
    }

    internal static class StringExtensions
    {
        public static string ToFirstLetterLower(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value.Length == 1) return value.ToLower();

            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }
    }
}
