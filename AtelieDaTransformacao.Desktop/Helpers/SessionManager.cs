namespace AtelieDaTransformacao.Desktop.Helpers;

public static class SessionManager
{
    public static string? Token { get; private set; }
    public static string? Email { get; private set; }
    public static bool IsAdmin { get; private set; }

    // Adicionamos as validações para funcionários e usuários
    public static bool IsFuncionarioOuUser { get; private set; }
    public static bool PodeGerenciarProdutos => IsAdmin || IsFuncionarioOuUser;

    public static IReadOnlyList<string> Roles { get; private set; } = Array.Empty<string>();

    public static void Start(string token, string email, IEnumerable<string> roles)
    {
        Token = token;
        Email = email;
        Roles = roles.ToArray();

        IsAdmin = Roles.Any(r => string.Equals(r, "Admin", StringComparison.OrdinalIgnoreCase));
        IsFuncionarioOuUser = Roles.Any(r =>
            string.Equals(r, "Funcionario", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(r, "User", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(r, "Employee", StringComparison.OrdinalIgnoreCase));
    }

    public static void UpdateProfile(string email, IEnumerable<string> roles)
    {
        Email = email;
        Roles = roles.ToArray();

        IsAdmin = Roles.Any(r => string.Equals(r, "Admin", StringComparison.OrdinalIgnoreCase));
        IsFuncionarioOuUser = Roles.Any(r =>
            string.Equals(r, "Funcionario", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(r, "User", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(r, "Employee", StringComparison.OrdinalIgnoreCase));
    }

    public static void Clear()
    {
        Token = null;
        Email = null;
        IsAdmin = false;
        IsFuncionarioOuUser = false;
        Roles = Array.Empty<string>();
    }
}