using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.UI.Services;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default);
    Task SendPasswordResetAsync(string to, string resetUrl, CancellationToken cancellationToken = default);
    Task SendOrderStatusAsync(Order order, CancellationToken cancellationToken = default);
}
