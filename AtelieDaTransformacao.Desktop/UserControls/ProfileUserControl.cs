using AtelieDaTransformacao.Desktop.Forms;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class ProfileUserControl : UserControl
{
    public ProfileUserControl()
    {
        InitializeComponent();

        Load += ProfileUserControl_Load;
        _editProfileButton.Click += EditProfileButton_Click;
        _changePasswordButton.Click += ChangePasswordButton_Click;
    }

    private void ProfileUserControl_Load(object? sender, EventArgs e)
    {
        if (DesignMode) return;
        AtualizarAvatarEInformacoes();
    }

    private void lblAvatar_Click(object? sender, EventArgs e)
    {
        if (DesignMode) return;
        AtualizarAvatarEInformacoes();
    }

    private void EditProfileButton_Click(object? sender, EventArgs e)
    {
        if (DesignMode) return;

        using var dialog = new EditProfileDialog();
        if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
            AtualizarAvatarEInformacoes();
    }

    private void ChangePasswordButton_Click(object? sender, EventArgs e)
    {
        if (DesignMode) return;

        using var dialog = new ChangePasswordDialog();
        dialog.ShowDialog(FindForm());
    }

    private void AtualizarAvatarEInformacoes()
    {
        var email = SessionManager.Email ?? "-";
        var displayName = email.Contains('@') ? email.Split('@')[0] : email;
        var initial = !string.IsNullOrWhiteSpace(displayName)
            ? displayName.Substring(0, 1).ToUpperInvariant()
            : "U";

        lblAvatar.Text = initial;
        _emailValueLabel.Text = email;
        _roleValueLabel.Text = SessionManager.IsAdmin ? "Administrador 🔧" : "Usuário 🤵🏿";
        _sessionValueLabel.Text = !string.IsNullOrWhiteSpace(SessionManager.Token)
            ? "JWT autenticado"
            : "Sem sessão";

        var roles = SessionManager.Roles ?? Array.Empty<string>();
        _permissionsValueLabel.Text = SessionManager.IsAdmin
            ? "Produtos • Categorias • Usuários"
            : "Consulta de produtos e categorias";

        if (roles.Count > 0)
            _permissionsValueLabel.Text += " • " + string.Join(", ", roles);
    }
}
