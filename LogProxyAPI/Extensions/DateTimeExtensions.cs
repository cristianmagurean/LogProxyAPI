using System;

namespace LogProxyAPI.Extensions
{
    public static class DateTimeExtensions
    {
        public static string AirTableFormat(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
    }
}
