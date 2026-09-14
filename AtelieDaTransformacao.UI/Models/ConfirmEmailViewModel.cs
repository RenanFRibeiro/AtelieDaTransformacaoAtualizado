namespace AtelieDaTransformacao.UI.Models;

/// <summary>
/// Dados necessários para concluir a confirmação do endereço de e-mail.
/// Mantido em Models para que o Razor não dependa de tipos declarados no namespace Controllers.
/// </summary>
public sealed class ConfirmEmailViewModel
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
