using System.Text.Json;

namespace AtelieDaTransformacao.Desktop.Helpers;

public static class AppConfig
{
    public static string ApiBaseUrl { get; } = Load();

    private static string Load()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (File.Exists(path))
            {
                using var doc = JsonDocument.Parse(File.ReadAllText(path));
                if (doc.RootElement.TryGetProperty("ApiBaseUrl", out var value))
                {
                    var url = value.GetString();
                    if (!string.IsNullOrWhiteSpace(url)) return url.EndsWith('/') ? url : url + "/";
                }
            }
        }
        catch { }
        return "http://localhost:5112/api/";
    }
}
