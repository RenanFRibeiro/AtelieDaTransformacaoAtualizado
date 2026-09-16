namespace AtelieDaTransformacao.UI.Services;

public interface IEmailAddressVerifier
{
    Task<EmailAddressVerificationResult> VerifyAsync(string email, CancellationToken cancellationToken = default);
}

public sealed record EmailAddressVerificationResult(bool IsValid, string Message);
