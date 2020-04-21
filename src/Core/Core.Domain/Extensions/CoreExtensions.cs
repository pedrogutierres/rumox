using System;
using System.Text;

namespace Core.Domain.Extensions
{
    public static class CoreExtensions
    {
        public static DateTime ToFirstHourOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime ToLastHourOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        public static string AgruparTodasAsMensagens(this Exception exception)
        {
            var msg = new StringBuilder(exception.Message);

            var tmp = exception;
            while (tmp.InnerException != null)
            {
                tmp = tmp.InnerException;
                
                msg.AppendLine();
                msg.Append(tmp.Message);
            }

            return msg.ToString();
        }
    }
}
