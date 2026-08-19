using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Services;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class ProfileUserControl : UserControl
{
    private readonly AuthApiService _auth = new();

    public ProfileUserControl()
    {
        InitializeComponent();

        // Atualiza valores visíveis com os dados do SessionManager
        _emailValueLabel.Text = SessionManager.Email ?? "-";
        _roleValueLabel.Text = SessionManager.IsAdmin ? "Administrador" : "Usuário";
        _permissionsValueLabel.Text = SessionManager.IsAdmin
            ? "Produtos • Categorias • Usuários"
            : "Consulta de produtos e categorias";
        _sessionValueLabel.Text = !string.IsNullOrWhiteSpace(SessionManager.Token) ? "JWT autenticado" : "Sem sessão";

        // Garante que a inicial seja atualizada ao carregar o controle
        Load += ProfileUserControl_Load;
    }

    private void ProfileUserControl_Load(object? sender, EventArgs e)
    {
        if (DesignMode) return;
        AtualizarAvatarEInformacoes();
    }

    private void lblAvatar_Click(object sender, EventArgs e)
    {
        if (DesignMode) return;

        // Ao clicar mantém comportamento de atualizar dados (se necessário)
        AtualizarAvatarEInformacoes();
    }

    private void AtualizarAvatarEInformacoes()
    {
        var email = SessionManager.Email ?? "-";
        var displayName = email.Contains('@') ? email.Split('@')[0] : email;
        var initial = !string.IsNullOrWhiteSpace(displayName) ? displayName.Substring(0, 1).ToUpper() : "U";

        // Avatar (inicial)
        lblAvatar.Text = initial;

        // Campos do perfil existentes no designer
        _emailValueLabel.Text = email;
        _roleValueLabel.Text = SessionManager.IsAdmin ? "Administrador" : "Usuário";
        _sessionValueLabel.Text = !string.IsNullOrWhiteSpace(SessionManager.Token) ? "JWT autenticado" : "Sem sessão";

        // Permissões + roles
        var roles = SessionManager.Roles ?? Array.Empty<string>();
        _permissionsValueLabel.Text = SessionManager.IsAdmin
            ? "Produtos • Categorias • Usuários"
            : "Consulta de produtos e categorias";

        if (roles.Count > 0)
            _permissionsValueLabel.Text += " • " + string.Join(", ", roles);
    }
}