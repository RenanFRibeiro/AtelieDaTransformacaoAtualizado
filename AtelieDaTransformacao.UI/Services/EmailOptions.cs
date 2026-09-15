namespace AtelieDaTransformacao.UI.Services;

public sealed class EmailOptions
{
    /// <summary>
    /// Brevo transactional email API key. Keep this value in User Secrets or an environment variable.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Verified sender email configured in Brevo.
    /// </summary>
    public string From { get; set; } = string.Empty;

    public string FromName { get; set; } = "Ateliê da Transformação";

    /// <summary>
    /// Public base URL used in order notification links.
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;
}
