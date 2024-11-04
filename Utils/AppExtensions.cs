using api_client_counter.Models.Enums;

namespace api_client_counter.Utils
{
    public static class AppExtensions
    {
        public static long ParseInn(this long inn) =>
            inn.ToString().Length is 10 or 12 ? inn : throw new ArgumentException($"Incorrect inn: {inn}");

        public static ClientType ToClientType(this string clientType) =>
            clientType.ToUpper() switch
            {
                nameof(ClientType.UL) => ClientType.UL,
                nameof(ClientType.IP) => ClientType.IP,
                _ => throw new ArgumentException($"Incorrect client type {clientType}"),
            };
    }
}