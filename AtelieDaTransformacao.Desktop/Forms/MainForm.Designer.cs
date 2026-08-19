using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;
    private Guna.UI2.WinForms.Guna2BorderlessForm _borderlessForm = null!;
    private Guna.UI2.WinForms.Guna2DragControl _dragControl = null!;
    private Guna.UI2.WinForms.Guna2Panel _sidebar = null!;
    private Guna.UI2.WinForms.Guna2Panel _brandPanel = null!;
    private Guna.UI2.WinForms.Guna2Panel _navPanel = null!;
    private Guna.UI2.WinForms.Guna2Button _dashboardButton = null!;
    private Guna.UI2.WinForms.Guna2Button _productsButton = null!;
    private Guna.UI2.WinForms.Guna2Button _categoriesButton = null!;
    private Guna.UI2.WinForms.Guna2Button _usersButton = null!;
    private Guna.UI2.WinForms.Guna2Button _profileButton = null!;
    private Guna.UI2.WinForms.Guna2Button _logoutButton = null!;
    private Guna.UI2.WinForms.Guna2Panel _topBar = null!;
    private Guna.UI2.WinForms.Guna2Panel _contentPanel = null!;
    private System.Windows.Forms.Label _brandLabel = null!;
    private System.Windows.Forms.Label _brandSubtitle = null!;
    private System.Windows.Forms.Label _pageTitle = null!;
    private System.Windows.Forms.Label _pageSubtitle = null!;
    private System.Windows.Forms.Label _userEmailLabel = null!;
    private System.Windows.Forms.Label _roleLabel = null!;
    private System.Windows.Forms.Label _roleBadge = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
        _dragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
        _topBar = new Guna.UI2.WinForms.Guna2Panel();
        _roleBadge = new Label();
        _pageSubtitle = new Label();
        _pageTitle = new Label();
        _sidebar = new Guna.UI2.WinForms.Guna2Panel();
        _navPanel = new Guna.UI2.WinForms.Guna2Panel();
        _profileButton = new Guna.UI2.WinForms.Guna2Button();
        _usersButton = new Guna.UI2.WinForms.Guna2Button();
        _categoriesButton = new Guna.UI2.WinForms.Guna2Button();
        _productsButton = new Guna.UI2.WinForms.Guna2Button();
        _dashboardButton = new Guna.UI2.WinForms.Guna2Button();
        _logoutButton = new Guna.UI2.WinForms.Guna2Button();
        _roleLabel = new Label();
        _userEmailLabel = new Label();
        _brandPanel = new Guna.UI2.WinForms.Guna2Panel();
        _brandSubtitle = new Label();
        _brandLabel = new Label();
        _contentPanel = new Guna.UI2.WinForms.Guna2Panel();
        _topBar.SuspendLayout();
        _sidebar.SuspendLayout();
        _navPanel.SuspendLayout();
        _brandPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _borderlessForm
        // 
        _borderlessForm.BorderRadius = 14;
        _borderlessForm.ContainerControl = this;
        _borderlessForm.DockIndicatorTransparencyValue = 0.6D;
        _borderlessForm.TransparentWhileDrag = true;
        // 
        // _dragControl
        // 
        _dragControl.DockIndicatorTransparencyValue = 0.6D;
        _dragControl.TargetControl = _topBar;
        _dragControl.UseTransparentDrag = true;
        // 
        // _topBar
        // 
        _topBar.BackColor = Color.FromArgb(107, 69, 37);
        _topBar.Controls.Add(_roleBadge);
        _topBar.Controls.Add(_pageSubtitle);
        _topBar.Controls.Add(_pageTitle);
        _topBar.CustomizableEdges = customizableEdges3;
        _topBar.Dock = DockStyle.Top;
        _topBar.FillColor = Color.Transparent;
        _topBar.Location = new Point(255, 0);
        _topBar.Name = "_topBar";
        _topBar.Padding = new Padding(28, 15, 28, 10);
        _topBar.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _topBar.Size = new Size(795, 92);
        _topBar.TabIndex = 1;
        // 
        // _roleBadge
        // 
        _roleBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _roleBadge.AutoSize = true;
        _roleBadge.BackColor = Color.FromArgb(235, 239, 255);
        _roleBadge.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _roleBadge.Location = new Point(1415, 25);
        _roleBadge.Name = "_roleBadge";
        _roleBadge.Padding = new Padding(10, 6, 10, 6);
        _roleBadge.Size = new Size(66, 25);
        _roleBadge.TabIndex = 0;
        _roleBadge.Text = "ADMIN";
        // 
        // _pageSubtitle
        // 
        _pageSubtitle.AutoSize = true;
        _pageSubtitle.BackColor = Color.Transparent;
        _pageSubtitle.Font = new Font("Segoe UI", 9.5F);
        _pageSubtitle.ForeColor = Color.Transparent;
        _pageSubtitle.Location = new Point(30, 53);
        _pageSubtitle.Name = "_pageSubtitle";
        _pageSubtitle.Size = new Size(243, 17);
        _pageSubtitle.TabIndex = 1;
        _pageSubtitle.Text = "Visão geral do Ateliê da Transformação.";
        // 
        // _pageTitle
        // 
        _pageTitle.AutoSize = true;
        _pageTitle.BackColor = Color.Transparent;
        _pageTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        _pageTitle.ForeColor = Color.Transparent;
        _pageTitle.Location = new Point(28, 14);
        _pageTitle.Name = "_pageTitle";
        _pageTitle.Size = new Size(157, 37);
        _pageTitle.TabIndex = 2;
        _pageTitle.Text = "Dashboard";
        // 
        // _sidebar
        // 
        _sidebar.BackColor = Color.FromArgb(107, 69, 37);
        _sidebar.Controls.Add(_navPanel);
        _sidebar.Controls.Add(_logoutButton);
        _sidebar.Controls.Add(_roleLabel);
        _sidebar.Controls.Add(_userEmailLabel);
        _sidebar.Controls.Add(_brandPanel);
        _sidebar.CustomizableEdges = customizableEdges21;
        _sidebar.Dock = DockStyle.Left;
        _sidebar.Location = new Point(0, 0);
        _sidebar.Name = "_sidebar";
        _sidebar.Padding = new Padding(14);
        _sidebar.ShadowDecoration.CustomizableEdges = customizableEdges22;
        _sidebar.Size = new Size(255, 650);
        _sidebar.TabIndex = 2;
        // 
        // _navPanel
        // 
        _navPanel.BackColor = Color.FromArgb(107, 69, 37);
        _navPanel.Controls.Add(_profileButton);
        _navPanel.Controls.Add(_usersButton);
        _navPanel.Controls.Add(_categoriesButton);
        _navPanel.Controls.Add(_productsButton);
        _navPanel.Controls.Add(_dashboardButton);
        _navPanel.CustomizableEdges = customizableEdges15;
        _navPanel.Dock = DockStyle.Top;
        _navPanel.FillColor = Color.Transparent;
        _navPanel.Location = new Point(14, 114);
        _navPanel.Name = "_navPanel";
        _navPanel.Padding = new Padding(0, 8, 0, 0);
        _navPanel.ShadowDecoration.CustomizableEdges = customizableEdges16;
        _navPanel.Size = new Size(227, 417);
        _navPanel.TabIndex = 0;
        // 
        // _profileButton
        // 
        _profileButton.BorderRadius = 10;
        _profileButton.CustomizableEdges = customizableEdges5;
        _profileButton.FillColor = Color.FromArgb(164, 104, 45);
        _profileButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _profileButton.ForeColor = Color.DimGray;
        _profileButton.Location = new Point(0, 292);
        _profileButton.Name = "_profileButton";
        _profileButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _profileButton.Size = new Size(227, 56);
        _profileButton.TabIndex = 0;
        _profileButton.Text = "Perfil";
        // 
        // _usersButton
        // 
        _usersButton.BorderRadius = 10;
        _usersButton.CustomizableEdges = customizableEdges7;
        _usersButton.FillColor = Color.FromArgb(164, 104, 45);
        _usersButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _usersButton.ForeColor = Color.DimGray;
        _usersButton.Location = new Point(0, 230);
        _usersButton.Name = "_usersButton";
        _usersButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _usersButton.Size = new Size(227, 56);
        _usersButton.TabIndex = 1;
        _usersButton.Text = "Usuários";
        // 
        // _categoriesButton
        // 
        _categoriesButton.BorderRadius = 10;
        _categoriesButton.CustomizableEdges = customizableEdges9;
        _categoriesButton.FillColor = Color.FromArgb(164, 104, 45);
        _categoriesButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _categoriesButton.ForeColor = Color.DimGray;
        _categoriesButton.Location = new Point(0, 168);
        _categoriesButton.Name = "_categoriesButton";
        _categoriesButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _categoriesButton.Size = new Size(227, 56);
        _categoriesButton.TabIndex = 2;
        _categoriesButton.Text = "Categorias";
        // 
        // _productsButton
        // 
        _productsButton.BorderRadius = 10;
        _productsButton.CustomizableEdges = customizableEdges11;
        _productsButton.FillColor = Color.FromArgb(164, 104, 45);
        _productsButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _productsButton.ForeColor = Color.DimGray;
        _productsButton.Location = new Point(0, 106);
        _productsButton.Name = "_productsButton";
        _productsButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _productsButton.Size = new Size(227, 56);
        _productsButton.TabIndex = 3;
        _productsButton.Text = "Produtos";
        // 
        // _dashboardButton
        // 
        _dashboardButton.BorderRadius = 10;
        _dashboardButton.CustomizableEdges = customizableEdges13;
        _dashboardButton.FillColor = Color.FromArgb(164, 104, 45);
        _dashboardButton.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _dashboardButton.ForeColor = Color.DimGray;
        _dashboardButton.Location = new Point(0, 44);
        _dashboardButton.Name = "_dashboardButton";
        _dashboardButton.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _dashboardButton.Size = new Size(227, 56);
        _dashboardButton.TabIndex = 4;
        _dashboardButton.Text = "Dashboard";
        // 
        // _logoutButton
        // 
        _logoutButton.BorderRadius = 9;
        _logoutButton.CustomizableEdges = customizableEdges17;
        _logoutButton.Dock = DockStyle.Bottom;
        _logoutButton.FillColor = Color.FromArgb(192, 0, 0);
        _logoutButton.Font = new Font("Segoe UI", 9F);
        _logoutButton.ForeColor = Color.White;
        _logoutButton.HoverState.FillColor = Color.FromArgb(55, 60, 72);
        _logoutButton.Location = new Point(14, 568);
        _logoutButton.Margin = new Padding(5, 10, 5, 8);
        _logoutButton.Name = "_logoutButton";
        _logoutButton.ShadowDecoration.CustomizableEdges = customizableEdges18;
        _logoutButton.Size = new Size(227, 42);
        _logoutButton.TabIndex = 1;
        _logoutButton.Text = "↪ Sair da conta";
        _logoutButton.Click += _logoutButton_Click;
        // 
        // _roleLabel
        // 
        _roleLabel.BackColor = Color.Transparent;
        _roleLabel.Dock = DockStyle.Bottom;
        _roleLabel.Font = new Font("Segoe UI", 8.5F);
        _roleLabel.ForeColor = Color.Transparent;
        _roleLabel.Location = new Point(14, 610);
        _roleLabel.Name = "_roleLabel";
        _roleLabel.Padding = new Padding(8, 0, 8, 0);
        _roleLabel.Size = new Size(227, 16);
        _roleLabel.TabIndex = 2;
        // 
        // _userEmailLabel
        // 
        _userEmailLabel.AutoEllipsis = true;
        _userEmailLabel.BackColor = Color.Transparent;
        _userEmailLabel.Dock = DockStyle.Bottom;
        _userEmailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _userEmailLabel.ForeColor = Color.Transparent;
        _userEmailLabel.Location = new Point(14, 626);
        _userEmailLabel.Name = "_userEmailLabel";
        _userEmailLabel.Padding = new Padding(8, 0, 8, 0);
        _userEmailLabel.Size = new Size(227, 10);
        _userEmailLabel.TabIndex = 3;
        // 
        // _brandPanel
        // 
        _brandPanel.BackColor = Color.FromArgb(88, 52, 27);
        _brandPanel.BorderRadius = 7;
        _brandPanel.Controls.Add(_brandSubtitle);
        _brandPanel.Controls.Add(_brandLabel);
        _brandPanel.CustomizableEdges = customizableEdges19;
        _brandPanel.Dock = DockStyle.Top;
        _brandPanel.FillColor = Color.Transparent;
        _brandPanel.Location = new Point(14, 14);
        _brandPanel.Name = "_brandPanel";
        _brandPanel.ShadowDecoration.CustomizableEdges = customizableEdges20;
        _brandPanel.Size = new Size(227, 100);
        _brandPanel.TabIndex = 4;
        // 
        // _brandSubtitle
        // 
        _brandSubtitle.AutoSize = true;
        _brandSubtitle.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        _brandSubtitle.ForeColor = Color.FromArgb(137, 145, 160);
        _brandSubtitle.Location = new Point(15, 66);
        _brandSubtitle.Name = "_brandSubtitle";
        _brandSubtitle.Size = new Size(128, 12);
        _brandSubtitle.TabIndex = 0;
        _brandSubtitle.Text = "PAINEL ADMINISTRATIVO";
        // 
        // _brandLabel
        // 
        _brandLabel.AutoSize = true;
        _brandLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
        _brandLabel.ForeColor = Color.Transparent;
        _brandLabel.Location = new Point(15, 14);
        _brandLabel.Name = "_brandLabel";
        _brandLabel.Size = new Size(170, 40);
        _brandLabel.TabIndex = 1;
        _brandLabel.Text = "ATELIÊ\nDA TRANSFORMAÇÃO";
        // 
        // _contentPanel
        // 
        _contentPanel.BackColor = Color.Transparent;
        _contentPanel.CustomizableEdges = customizableEdges1;
        _contentPanel.Dock = DockStyle.Fill;
        _contentPanel.Location = new Point(255, 92);
        _contentPanel.Name = "_contentPanel";
        _contentPanel.Padding = new Padding(28, 24, 28, 24);
        _contentPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _contentPanel.Size = new Size(795, 558);
        _contentPanel.TabIndex = 0;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1050, 650);
        Controls.Add(_contentPanel);
        Controls.Add(_topBar);
        Controls.Add(_sidebar);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.None;
        MinimumSize = new Size(1050, 650);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Ateliê da Transformação | Gestão";
        _topBar.ResumeLayout(false);
        _topBar.PerformLayout();
        _sidebar.ResumeLayout(false);
        _navPanel.ResumeLayout(false);
        _brandPanel.ResumeLayout(false);
        _brandPanel.PerformLayout();
        ResumeLayout(false);
    }

}
