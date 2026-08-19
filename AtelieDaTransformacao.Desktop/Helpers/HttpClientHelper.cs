using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AtelieDaTransformacao.Desktop.Helpers;

public static class HttpClientHelper
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri(AppConfig.ApiBaseUrl),
        Timeout = TimeSpan.FromSeconds(30)
    };

    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    public static Task<T?> GetAsync<T>(string url, CancellationToken ct = default) => SendAsync<T>(Create(HttpMethod.Get, url), ct);
    public static Task<T?> PostAsync<T>(string url, object body, CancellationToken ct = default) => SendAsync<T>(CreateWithBody(HttpMethod.Post, url, body), ct);
    public static Task<T?> PutAsync<T>(string url, object body, CancellationToken ct = default) => SendAsync<T>(CreateWithBody(HttpMethod.Put, url, body), ct);

    public static async Task DeleteAsync(string url, CancellationToken ct = default)
    {
        using var response = await Client.SendAsync(Create(HttpMethod.Delete, url), ct);
        if (!response.IsSuccessStatusCode) throw await CreateException(response);
    }

    private static HttpRequestMessage Create(HttpMethod method, string url)
    {
        var request = new HttpRequestMessage(method, url);
        if (!string.IsNullOrWhiteSpace(SessionManager.Token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.Token);
        return request;
    }

    private static HttpRequestMessage CreateWithBody(HttpMethod method, string url, object body)
    {
        var request = Create(method, url);
        request.Content = new StringContent(JsonSerializer.Serialize(body, Options), Encoding.UTF8, "application/json");
        return request;
    }

    private static async Task<T?> SendAsync<T>(HttpRequestMessage request, CancellationToken ct)
    {
        using var response = await Client.SendAsync(request, ct);
        if (!response.IsSuccessStatusCode) throw await CreateException(response);
        var text = await response.Content.ReadAsStringAsync(ct);
        return string.IsNullOrWhiteSpace(text) ? default : JsonSerializer.Deserialize<T>(text, Options);
    }

    private static async Task<Exception> CreateException(HttpResponseMessage response)
    {
        var text = await response.Content.ReadAsStringAsync();
        try
        {
            using var doc = JsonDocument.Parse(text);
            if (doc.RootElement.TryGetProperty("message", out var msg))
                return new InvalidOperationException(msg.GetString() ?? response.ReasonPhrase ?? "Erro na API.");
            if (doc.RootElement.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Array)
                return new InvalidOperationException(string.Join(Environment.NewLine, errors.EnumerateArray().Select(x => x.GetString()).Where(x => !string.IsNullOrWhiteSpace(x))));
        }
        catch { }
        return new InvalidOperationException($"API retornou {(int)response.StatusCode}: {response.ReasonPhrase}");
    }
}
