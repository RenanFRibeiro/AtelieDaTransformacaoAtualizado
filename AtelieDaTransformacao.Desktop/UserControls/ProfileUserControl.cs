using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class ProfileUserControl : UserControl
{
    public ProfileUserControl()
    {
        InitializeComponent();
        _emailValueLabel.Text = SessionManager.Email ?? "-";
        _roleValueLabel.Text = SessionManager.IsAdmin ? "Administrador" : "Usuário";
        _permissionsValueLabel.Text = SessionManager.IsAdmin ? "Produtos • Categorias • Usuários" : "Consulta de produtos e categorias";
    }
}
