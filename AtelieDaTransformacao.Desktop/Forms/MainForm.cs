using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.UserControls;
using AtelieDaTransformacao.Desktop.Themes;
using Guna.UI2.WinForms;

namespace AtelieDaTransformacao.Desktop.Forms;

public partial class MainForm : Form
{
    private UserControl? _current;
    public bool RequestLoginAgain { get; private set; }

    public MainForm()
    {
        InitializeComponent();
        ConfigureRuntime();
        FormClosed += MainForm_FormClosed;
    }

    private void ConfigureRuntime()
    {
        _userEmailLabel.Text = SessionManager.Email ?? "Usuário";
        _roleLabel.Text = SessionManager.IsAdmin ? "Administrador" : "Usuário";
        _roleBadge.Text = SessionManager.IsAdmin ? "ADMIN" : "USUÁRIO";
        _usersButton.Visible = SessionManager.IsAdmin;
        // Todos os usuários autenticados podem consultar o Status de Pedidos.
        _ordersStatusButton.Visible = true;
        WireNavigationButtons();
        Shown += (_, _) => ShowPage("dashboard");
    }

    private void WireNavigationButtons()
    {
        // Mantém o visual configurado no Designer e apenas garante a ligação
        // da navegação. Assim, alterações visuais feitas no Designer não
        // removem acidentalmente o Click dos botões.
        WireNavigationButton(_dashboardButton, "dashboard");
        WireNavigationButton(_productsButton, "products");
        WireNavigationButton(_categoriesButton, "categories");
        WireNavigationButton(_usersButton, "users");
        WireNavigationButton(_ordersStatusButton, "orders-status");
        WireNavigationButton(_profileButton, "profile");
    }

    private static void WireNavigationButton(Guna2Button button, string key)
    {
        button.Tag = key;
        button.Click -= NavClickStatic;
        button.Click += NavClickStatic;
    }

    private static void NavClickStatic(object? sender, EventArgs e)
    {
        if (sender is not Guna2Button button || button.FindForm() is not MainForm form)
            return;

        if (button.Tag is string key)
            form.ShowPage(key);
    }

    private void NavClick(object? sender, EventArgs e)
    {
        if (sender is Guna2Button button && button.Tag is string key)
            ShowPage(key);
    }

    private void ShowPage(string key)
    {
        foreach (Control control in _navPanel.Controls)
        {
            if (control is Guna2Button button)
            {
                var selected = string.Equals(button.Tag?.ToString(), key, StringComparison.OrdinalIgnoreCase);
                button.FillColor = selected ? LibraryTheme.Accent : Color.Transparent;
                button.ForeColor = selected ? Color.White : Color.FromArgb(190, 195, 206);
            }
        }

        _current?.Dispose();
        _current = key switch
        {
            "products" => new ProductsUserControl(),
            "categories" => new CategoriesUserControl(),
            "users" => new UsersUserControl(),
            "orders-status" => new OrdersStatusUserControl(),
            "profile" => new ProfileUserControl(),
            _ => new DashboardUserControl()
        };

        _pageTitle.Text = key switch
        {
            "products" => "Produtos",
            "categories" => "Categorias",
            "users" => "Usuários",
            "orders-status" => "Status de Pedidos",
            "profile" => "Meu perfil",
            _ => "Dashboard"
        };
        _pageSubtitle.Text = key switch
        {
            "products" => "Cadastre, edite e acompanhe o estoque dos seus produtos.",
            "categories" => "Organize o catálogo por categorias.",
            "users" => "Gerencie os acessos administrativos ao sistema.",
            "orders-status" => "Acompanhe o andamento dos pedidos em todas as etapas.",
            "profile" => "Confira os dados da sua sessão atual.",
            _ => "Visão geral do Ateliê da Transformação."
        };

        _contentPanel.Controls.Clear();
        _contentPanel.Controls.Add(_current);
        _current.Dock = DockStyle.Fill;
    }

    private void LogoutButton_Click(object? sender, EventArgs e)
    {
        if (MessageBox.Show(this, "Deseja realmente sair da sua conta?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        RequestLoginAgain = true;
        SessionManager.Clear();
        Close();
    }

    private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        _current?.Dispose();
    }

    private void WireLogoutButton()
    {
        if (_logoutButton == null)
            return;

        // Evita registrar o mesmo evento duas vezes
        _logoutButton.Click -= LogoutButton_Click;
        _logoutButton.Click += LogoutButton_Click;
    }

    private void _logoutButton_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show(
        "Deseja realmente sair da sua conta?",
        "Sair",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
            return;

        // Limpa o usuário/token da sessão
        SessionManager.Clear();

        // Informa ao Program.cs que deve voltar para o login
        RequestLoginAgain = true;

        // Fecha a janela principal
        Close();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.Application.Exit();
    }

    private void btnMinimize_Click(object sender, EventArgs e)
    {

    }
}
