namespace AtelieDaTransformacao.UI.Services;

public sealed class OrderAutomationOptions
{
    public int IntervalSeconds { get; set; } = 30;
    public int MinimumStatusAgeMinutes { get; set; } = 1;
}
