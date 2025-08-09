using System.Text.Json;

namespace ChicoKoodo.AndroidApp.Utilities
{
    public static class JsonSerializerHelper
    {
        public static JsonSerializerOptions NihongoSerializerOption()
        {
            return new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
