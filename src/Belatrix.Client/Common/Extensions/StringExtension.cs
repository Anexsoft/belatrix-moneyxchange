using System.Text.Json;

namespace Belatrix.Client.Common.Extensions
{
    public static class StringExtension
    {
        public static string Serialize(this object data, bool lowerCase = false)
        {
            if (!lowerCase)
            {
                return JsonSerializer.Serialize(data);
            }

            return JsonSerializer.Serialize(
                data,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        public static T Deserialize<T>(this string data)
        {
            if (string.IsNullOrEmpty(data))
                return default(T);

            return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
