using System.Text.Json;

namespace AtelieDaTransformacao.Desktop.Helpers;

public static class AppConfig
{
    public static string ApiBaseUrl { get; } = Load("ApiBaseUrl", "http://localhost:5112/api/");
    public static string ImageBaseUrl { get; } = Load("ImageBaseUrl", "http://localhost:5199/");

    private static string Load(string key, string fallback)
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (File.Exists(path))
            {
                using var doc = JsonDocument.Parse(File.ReadAllText(path));
                if (doc.RootElement.TryGetProperty(key, out var value))
                {
                    var url = value.GetString();
                    if (!string.IsNullOrWhiteSpace(url)) return url.EndsWith('/') ? url : url + "/";
                }
            }
        }
        catch { }
        return fallback;
    }
}
