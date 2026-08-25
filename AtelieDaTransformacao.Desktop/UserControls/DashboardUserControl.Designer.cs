using Guna.UI2.WinForms.Suite;

namespace AtelieDaTransformacao.Desktop.UserControls;

partial class DashboardUserControl
{
    private System.ComponentModel.IContainer? components = null;

    private Guna.UI2.WinForms.Guna2Panel _headerPanel = null!;
    private Label _welcomeLabel = null!;
    private Label _descriptionLabel = null!;
    private Guna.UI2.WinForms.Guna2Button _refreshButton = null!;
    private Label _statusLabel = null!;
    private Guna.UI2.WinForms.Guna2Panel _cardsPanel = null!;

    private Guna.UI2.WinForms.Guna2Panel cardGames = null!;
    private Guna.UI2.WinForms.Guna2Panel pnlCorGames = null!;
    private Label cardGamesLblDesc = null!;
    private Label cardGamesLblNumero = null!;
    private Label cardGamesLblTitulo = null!;

    private Guna.UI2.WinForms.Guna2Panel guna2Panel1 = null!;
    private Guna.UI2.WinForms.Guna2Panel guna2Panel2 = null!;
    private Label label1 = null!;
    private Label label2 = null!;
    private Label label3 = null!;

    private Guna.UI2.WinForms.Guna2Panel cardCategorias = null!;
    private Guna.UI2.WinForms.Guna2Panel pnlCorCategorias = null!;
    private Label cardCategoriasLblNumero = null!;
    private Label cardCategoriasLblTitulo = null!;
    private Label cardCategoriasLblDesc = null!;

    private Guna.UI2.WinForms.Guna2Panel cardUsuarios = null!;
    private Guna.UI2.WinForms.Guna2Panel pnlCorUsuarios = null!;
    private Label cardUsuariosLblNumero = null!;
    private Label cardUsuariosLblTitulo = null!;
    private Label cardUsuariosLblDesc = null!;

    private Guna.UI2.WinForms.Guna2Panel _tableCard = null!;
    private Label _tableTitle = null!;
    private Guna.UI2.WinForms.Guna2Button _recentRefreshButton = null!;
    private Guna.UI2.WinForms.Guna2DataGridView _grid = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        CustomizableEdges customizableEdges3 = new CustomizableEdges();
        CustomizableEdges customizableEdges4 = new CustomizableEdges();
        CustomizableEdges customizableEdges1 = new CustomizableEdges();
        CustomizableEdges customizableEdges2 = new CustomizableEdges();
        CustomizableEdges customizableEdges21 = new CustomizableEdges();
        CustomizableEdges customizableEdges22 = new CustomizableEdges();
        CustomizableEdges customizableEdges5 = new CustomizableEdges();
        CustomizableEdges customizableEdges6 = new CustomizableEdges();
        CustomizableEdges customizableEdges7 = new CustomizableEdges();
        CustomizableEdges customizableEdges8 = new CustomizableEdges();
        CustomizableEdges customizableEdges9 = new CustomizableEdges();
        CustomizableEdges customizableEdges10 = new CustomizableEdges();
        CustomizableEdges customizableEdges11 = new CustomizableEdges();
        CustomizableEdges customizableEdges12 = new CustomizableEdges();
        CustomizableEdges customizableEdges13 = new CustomizableEdges();
        CustomizableEdges customizableEdges14 = new CustomizableEdges();
        CustomizableEdges customizableEdges15 = new CustomizableEdges();
        CustomizableEdges customizableEdges16 = new CustomizableEdges();
        CustomizableEdges customizableEdges17 = new CustomizableEdges();
        CustomizableEdges customizableEdges18 = new CustomizableEdges();
        CustomizableEdges customizableEdges19 = new CustomizableEdges();
        CustomizableEdges customizableEdges20 = new CustomizableEdges();
        CustomizableEdges customizableEdges25 = new CustomizableEdges();
        CustomizableEdges customizableEdges26 = new CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        CustomizableEdges customizableEdges23 = new CustomizableEdges();
        CustomizableEdges customizableEdges24 = new CustomizableEdges();
        _headerPanel = new Guna.UI2.WinForms.Guna2Panel();
        _statusLabel = new Label();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _descriptionLabel = new Label();
        _welcomeLabel = new Label();
        _cardsPanel = new Guna.UI2.WinForms.Guna2Panel();
        cardUsuarios = new Guna.UI2.WinForms.Guna2Panel();
        cardUsuariosLblNumero = new Label();
        cardUsuariosLblTitulo = new Label();
        cardUsuariosLblDesc = new Label();
        pnlCorUsuarios = new Guna.UI2.WinForms.Guna2Panel();
        cardCategorias = new Guna.UI2.WinForms.Guna2Panel();
        cardCategoriasLblNumero = new Label();
        cardCategoriasLblTitulo = new Label();
        cardCategoriasLblDesc = new Label();
        pnlCorCategorias = new Guna.UI2.WinForms.Guna2Panel();
        guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
        cardGames = new Guna.UI2.WinForms.Guna2Panel();
        cardGamesLblDesc = new Label();
        cardGamesLblNumero = new Label();
        cardGamesLblTitulo = new Label();
        pnlCorGames = new Guna.UI2.WinForms.Guna2Panel();
        _tableCard = new Guna.UI2.WinForms.Guna2Panel();
        _grid = new Guna.UI2.WinForms.Guna2DataGridView();
        _recentRefreshButton = new Guna.UI2.WinForms.Guna2Button();
        _tableTitle = new Label();
        _headerPanel.SuspendLayout();
        _cardsPanel.SuspendLayout();
        cardUsuarios.SuspendLayout();
        cardCategorias.SuspendLayout();
        guna2Panel1.SuspendLayout();
        cardGames.SuspendLayout();
        _tableCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        SuspendLayout();
        // 
        // _headerPanel
        // 
        _headerPanel.BackColor = Color.LightGray;
        _headerPanel.BorderColor = Color.LightGray;
        _headerPanel.BorderRadius = 10;
        _headerPanel.Controls.Add(_statusLabel);
        _headerPanel.Controls.Add(_refreshButton);
        _headerPanel.Controls.Add(_descriptionLabel);
        _headerPanel.Controls.Add(_welcomeLabel);
        _headerPanel.CustomizableEdges = customizableEdges3;
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.FillColor = Color.Transparent;
        _headerPanel.Location = new Point(0, 0);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _headerPanel.Size = new Size(795, 82);
        _headerPanel.TabIndex = 0;
        // 
        // _statusLabel
        // 
        _statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _statusLabel.AutoSize = true;
        _statusLabel.Font = new Font("Segoe UI", 8.5F);
        _statusLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _statusLabel.Location = new Point(1430, 20);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(78, 15);
        _statusLabel.TabIndex = 0;
        _statusLabel.Text = "Carregando...";
        // 
        // _refreshButton
        // 
        _refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _refreshButton.BorderRadius = 9;
        _refreshButton.CustomizableEdges = customizableEdges1;
        _refreshButton.FillColor = Color.FromArgb(74, 108, 247);
        _refreshButton.Font = new Font("Segoe UI", 9F);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(1635, 8);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _refreshButton.Size = new Size(112, 38);
        _refreshButton.TabIndex = 1;
        _refreshButton.Text = "Atualizar";
        // 
        // _descriptionLabel
        // 
        _descriptionLabel.AutoSize = true;
        _descriptionLabel.Font = new Font("Segoe UI", 9.5F);
        _descriptionLabel.ForeColor = Color.Black;
        _descriptionLabel.Location = new Point(2, 35);
        _descriptionLabel.Name = "_descriptionLabel";
        _descriptionLabel.Size = new Size(358, 17);
        _descriptionLabel.TabIndex = 2;
        _descriptionLabel.Text = "Acompanhe produtos, estoque e categorias em tempo real.";
        // 
        // _welcomeLabel
        // 
        _welcomeLabel.AutoSize = true;
        _welcomeLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _welcomeLabel.ForeColor = Color.Black;
        _welcomeLabel.Location = new Point(0, 0);
        _welcomeLabel.Name = "_welcomeLabel";
        _welcomeLabel.Size = new Size(132, 31);
        _welcomeLabel.TabIndex = 3;
        _welcomeLabel.Text = "Visão geral";
        // 
        // _cardsPanel
        // 
        _cardsPanel.BackColor = Color.White;
        _cardsPanel.BorderRadius = 15;
        _cardsPanel.Controls.Add(cardUsuarios);
        _cardsPanel.Controls.Add(pnlCorUsuarios);
        _cardsPanel.Controls.Add(cardCategorias);
        _cardsPanel.Controls.Add(pnlCorCategorias);
        _cardsPanel.Controls.Add(guna2Panel1);
        _cardsPanel.Controls.Add(guna2Panel2);
        _cardsPanel.Controls.Add(cardGames);
        _cardsPanel.Controls.Add(pnlCorGames);
        customizableEdges21.TopLeft = false;
        customizableEdges21.TopRight = false;
        _cardsPanel.CustomizableEdges = customizableEdges21;
        _cardsPanel.Dock = DockStyle.Top;
        _cardsPanel.FillColor = Color.LightGray;
        _cardsPanel.Location = new Point(0, 82);
        _cardsPanel.Name = "_cardsPanel";
        _cardsPanel.Padding = new Padding(28, 10, 28, 10);
        _cardsPanel.ShadowDecoration.CustomizableEdges = customizableEdges22;
        _cardsPanel.Size = new Size(795, 142);
        _cardsPanel.TabIndex = 1;
        // 
        // cardUsuarios
        // 
        cardUsuarios.BackColor = Color.Transparent;
        cardUsuarios.BorderRadius = 10;
        cardUsuarios.Controls.Add(cardUsuariosLblNumero);
        cardUsuarios.Controls.Add(cardUsuariosLblTitulo);
        cardUsuarios.Controls.Add(cardUsuariosLblDesc);
        cardUsuarios.CustomizableEdges = customizableEdges5;
        cardUsuarios.FillColor = Color.White;
        cardUsuarios.Location = new Point(590, 12);
        cardUsuarios.Name = "cardUsuarios";
        cardUsuarios.ShadowDecoration.CustomizableEdges = customizableEdges6;
        cardUsuarios.Size = new Size(180, 118);
        cardUsuarios.TabIndex = 8;
        // 
        // cardUsuariosLblNumero
        // 
        cardUsuariosLblNumero.AutoSize = true;
        cardUsuariosLblNumero.BackColor = Color.Transparent;
        cardUsuariosLblNumero.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        cardUsuariosLblNumero.Location = new Point(20, 38);
        cardUsuariosLblNumero.Name = "cardUsuariosLblNumero";
        cardUsuariosLblNumero.Size = new Size(38, 45);
        cardUsuariosLblNumero.TabIndex = 0;
        cardUsuariosLblNumero.Text = "0";
        // 
        // cardUsuariosLblTitulo
        // 
        cardUsuariosLblTitulo.AutoSize = true;
        cardUsuariosLblTitulo.BackColor = Color.Transparent;
        cardUsuariosLblTitulo.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
        cardUsuariosLblTitulo.ForeColor = Color.Green;
        cardUsuariosLblTitulo.Location = new Point(20, 19);
        cardUsuariosLblTitulo.Name = "cardUsuariosLblTitulo";
        cardUsuariosLblTitulo.Size = new Size(143, 19);
        cardUsuariosLblTitulo.TabIndex = 1;
        cardUsuariosLblTitulo.Text = "👥 Usuários ativos";
        // 
        // cardUsuariosLblDesc
        // 
        cardUsuariosLblDesc.AutoSize = true;
        cardUsuariosLblDesc.BackColor = Color.Transparent;
        cardUsuariosLblDesc.Font = new Font("Century Gothic", 8.25F);
        cardUsuariosLblDesc.ForeColor = Color.Black;
        cardUsuariosLblDesc.Location = new Point(20, 83);
        cardUsuariosLblDesc.Name = "cardUsuariosLblDesc";
        cardUsuariosLblDesc.Size = new Size(142, 16);
        cardUsuariosLblDesc.TabIndex = 2;
        cardUsuariosLblDesc.Text = "Contas ativas no sistema";
        // 
        // pnlCorUsuarios
        // 
        pnlCorUsuarios.CustomizableEdges = customizableEdges7;
        pnlCorUsuarios.FillColor = Color.FromArgb(108, 76, 52);
        pnlCorUsuarios.Location = new Point(590, 12);
        pnlCorUsuarios.Name = "pnlCorUsuarios";
        pnlCorUsuarios.ShadowDecoration.CustomizableEdges = customizableEdges8;
        pnlCorUsuarios.Size = new Size(180, 10);
        pnlCorUsuarios.TabIndex = 9;
        pnlCorUsuarios.Visible = false;
        // 
        // cardCategorias
        // 
        cardCategorias.BackColor = Color.Transparent;
        cardCategorias.BorderRadius = 10;
        cardCategorias.Controls.Add(cardCategoriasLblNumero);
        cardCategorias.Controls.Add(cardCategoriasLblTitulo);
        cardCategorias.Controls.Add(cardCategoriasLblDesc);
        cardCategorias.CustomizableEdges = customizableEdges9;
        cardCategorias.FillColor = Color.White;
        cardCategorias.Location = new Point(400, 12);
        cardCategorias.Name = "cardCategorias";
        cardCategorias.ShadowDecoration.CustomizableEdges = customizableEdges10;
        cardCategorias.Size = new Size(180, 118);
        cardCategorias.TabIndex = 6;
        // 
        // cardCategoriasLblNumero
        // 
        cardCategoriasLblNumero.AutoSize = true;
        cardCategoriasLblNumero.BackColor = Color.Transparent;
        cardCategoriasLblNumero.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        cardCategoriasLblNumero.Location = new Point(20, 38);
        cardCategoriasLblNumero.Name = "cardCategoriasLblNumero";
        cardCategoriasLblNumero.Size = new Size(38, 45);
        cardCategoriasLblNumero.TabIndex = 0;
        cardCategoriasLblNumero.Text = "0";
        // 
        // cardCategoriasLblTitulo
        // 
        cardCategoriasLblTitulo.AutoSize = true;
        cardCategoriasLblTitulo.BackColor = Color.Transparent;
        cardCategoriasLblTitulo.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
        cardCategoriasLblTitulo.ForeColor = Color.FromArgb(248, 148, 27);
        cardCategoriasLblTitulo.Location = new Point(20, 19);
        cardCategoriasLblTitulo.Name = "cardCategoriasLblTitulo";
        cardCategoriasLblTitulo.Size = new Size(117, 19);
        cardCategoriasLblTitulo.TabIndex = 1;
        cardCategoriasLblTitulo.Text = "🏷️ Categorias";
        // 
        // cardCategoriasLblDesc
        // 
        cardCategoriasLblDesc.AutoSize = true;
        cardCategoriasLblDesc.BackColor = Color.Transparent;
        cardCategoriasLblDesc.Font = new Font("Century Gothic", 8.25F);
        cardCategoriasLblDesc.ForeColor = Color.Black;
        cardCategoriasLblDesc.Location = new Point(20, 83);
        cardCategoriasLblDesc.Name = "cardCategoriasLblDesc";
        cardCategoriasLblDesc.Size = new Size(111, 16);
        cardCategoriasLblDesc.TabIndex = 2;
        cardCategoriasLblDesc.Text = "Total de categorias";
        // 
        // pnlCorCategorias
        // 
        pnlCorCategorias.CustomizableEdges = customizableEdges11;
        pnlCorCategorias.FillColor = Color.FromArgb(248, 148, 27);
        pnlCorCategorias.Location = new Point(400, 12);
        pnlCorCategorias.Name = "pnlCorCategorias";
        pnlCorCategorias.ShadowDecoration.CustomizableEdges = customizableEdges12;
        pnlCorCategorias.Size = new Size(180, 10);
        pnlCorCategorias.TabIndex = 7;
        pnlCorCategorias.Visible = false;
        // 
        // guna2Panel1
        // 
        guna2Panel1.BackColor = Color.Transparent;
        guna2Panel1.BorderRadius = 10;
        guna2Panel1.Controls.Add(label1);
        guna2Panel1.Controls.Add(label2);
        guna2Panel1.Controls.Add(label3);
        guna2Panel1.CustomizableEdges = customizableEdges13;
        guna2Panel1.FillColor = Color.White;
        guna2Panel1.Location = new Point(210, 12);
        guna2Panel1.Name = "guna2Panel1";
        guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges14;
        guna2Panel1.Size = new Size(180, 118);
        guna2Panel1.TabIndex = 4;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.BackColor = Color.Transparent;
        label1.Font = new Font("Century Gothic", 8.25F);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(5, 83);
        label1.Name = "label1";
        label1.Size = new Size(172, 16);
        label1.TabIndex = 0;
        label1.Text = "Total de unidades em estoque";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.BackColor = Color.Transparent;
        label2.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        label2.Location = new Point(12, 38);
        label2.Name = "label2";
        label2.Size = new Size(38, 45);
        label2.TabIndex = 1;
        label2.Text = "0";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.BackColor = Color.Transparent;
        label3.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
        label3.ForeColor = Color.FromArgb(192, 0, 0);
        label3.Location = new Point(12, 19);
        label3.Name = "label3";
        label3.Size = new Size(92, 19);
        label3.TabIndex = 2;
        label3.Text = "📦 Estoque";
        // 
        // guna2Panel2
        // 
        guna2Panel2.CustomizableEdges = customizableEdges15;
        guna2Panel2.FillColor = Color.FromArgb(0, 77, 147);
        guna2Panel2.Location = new Point(210, 12);
        guna2Panel2.Name = "guna2Panel2";
        guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges16;
        guna2Panel2.Size = new Size(180, 10);
        guna2Panel2.TabIndex = 5;
        guna2Panel2.Visible = false;
        // 
        // cardGames
        // 
        cardGames.BackColor = Color.Transparent;
        cardGames.BorderRadius = 10;
        cardGames.Controls.Add(cardGamesLblDesc);
        cardGames.Controls.Add(cardGamesLblNumero);
        cardGames.Controls.Add(cardGamesLblTitulo);
        cardGames.CustomizableEdges = customizableEdges17;
        cardGames.FillColor = Color.White;
        cardGames.Location = new Point(20, 12);
        cardGames.Name = "cardGames";
        cardGames.ShadowDecoration.CustomizableEdges = customizableEdges18;
        cardGames.Size = new Size(180, 118);
        cardGames.TabIndex = 2;
        // 
        // cardGamesLblDesc
        // 
        cardGamesLblDesc.AutoSize = true;
        cardGamesLblDesc.BackColor = Color.Transparent;
        cardGamesLblDesc.Font = new Font("Century Gothic", 8.25F);
        cardGamesLblDesc.ForeColor = Color.Black;
        cardGamesLblDesc.Location = new Point(4, 83);
        cardGamesLblDesc.Name = "cardGamesLblDesc";
        cardGamesLblDesc.Size = new Size(174, 16);
        cardGamesLblDesc.TabIndex = 0;
        cardGamesLblDesc.Text = "Total de produtos cadastrados";
        // 
        // cardGamesLblNumero
        // 
        cardGamesLblNumero.AutoSize = true;
        cardGamesLblNumero.BackColor = Color.Transparent;
        cardGamesLblNumero.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        cardGamesLblNumero.Location = new Point(12, 38);
        cardGamesLblNumero.Name = "cardGamesLblNumero";
        cardGamesLblNumero.Size = new Size(38, 45);
        cardGamesLblNumero.TabIndex = 1;
        cardGamesLblNumero.Text = "0";
        // 
        // cardGamesLblTitulo
        // 
        cardGamesLblTitulo.AutoSize = true;
        cardGamesLblTitulo.BackColor = Color.Transparent;
        cardGamesLblTitulo.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
        cardGamesLblTitulo.ForeColor = Color.FromArgb(0, 77, 147);
        cardGamesLblTitulo.Location = new Point(12, 19);
        cardGamesLblTitulo.Name = "cardGamesLblTitulo";
        cardGamesLblTitulo.Size = new Size(98, 19);
        cardGamesLblTitulo.TabIndex = 2;
        cardGamesLblTitulo.Text = "\U0001f9f5 Produtos";
        // 
        // pnlCorGames
        // 
        pnlCorGames.CustomizableEdges = customizableEdges19;
        pnlCorGames.FillColor = Color.FromArgb(0, 77, 147);
        pnlCorGames.Location = new Point(20, 12);
        pnlCorGames.Name = "pnlCorGames";
        pnlCorGames.ShadowDecoration.CustomizableEdges = customizableEdges20;
        pnlCorGames.Size = new Size(180, 10);
        pnlCorGames.TabIndex = 3;
        pnlCorGames.Visible = false;
        // 
        // _tableCard
        // 
        _tableCard.BackColor = Color.Transparent;
        _tableCard.BorderColor = Color.Transparent;
        _tableCard.BorderRadius = 15;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_recentRefreshButton);
        _tableCard.Controls.Add(_tableTitle);
        _tableCard.CustomizableEdges = customizableEdges25;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 224);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(16);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges26;
        _tableCard.Size = new Size(795, 334);
        _tableCard.TabIndex = 10;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        _grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(245, 247, 251);
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(55, 61, 72);
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(245, 247, 251);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(55, 61, 72);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.ColumnHeadersHeight = 38;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.White;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(45, 49, 56);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(235, 238, 244);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(45, 49, 56);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.FromArgb(230, 233, 238);
        _grid.Location = new Point(16, 50);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 38;
        _grid.Size = new Size(763, 268);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.FromArgb(230, 233, 238);
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(245, 247, 251);
        _grid.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
        _grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _grid.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(55, 61, 72);
        _grid.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.ThemeStyle.HeaderStyle.Height = 38;
        _grid.ThemeStyle.ReadOnly = true;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(45, 49, 56);
        _grid.ThemeStyle.RowsStyle.Height = 38;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(235, 238, 244);
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(45, 49, 56);
        // 
        // _recentRefreshButton
        // 
        _recentRefreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _recentRefreshButton.BorderRadius = 8;
        _recentRefreshButton.CustomizableEdges = customizableEdges23;
        _recentRefreshButton.FillColor = Color.FromArgb(88, 52, 27);
        _recentRefreshButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _recentRefreshButton.ForeColor = Color.White;
        _recentRefreshButton.Location = new Point(650, 17);
        _recentRefreshButton.Name = "_recentRefreshButton";
        _recentRefreshButton.ShadowDecoration.CustomizableEdges = customizableEdges24;
        _recentRefreshButton.Size = new Size(128, 32);
        _recentRefreshButton.TabIndex = 2;
        _recentRefreshButton.Text = "↻ Atualizar";
        // 
        // _tableTitle
        // 
        _tableTitle.BackColor = Color.Transparent;
        _tableTitle.Dock = DockStyle.Top;
        _tableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _tableTitle.ForeColor = Color.FromArgb(30, 34, 43);
        _tableTitle.Location = new Point(16, 16);
        _tableTitle.Name = "_tableTitle";
        _tableTitle.Size = new Size(763, 34);
        _tableTitle.TabIndex = 1;
        _tableTitle.Text = "Produtos recentes";
        // 
        // DashboardUserControl
        // 
        BackColor = Color.FromArgb(245, 247, 251);
        Controls.Add(_tableCard);
        Controls.Add(_cardsPanel);
        Controls.Add(_headerPanel);
        Name = "DashboardUserControl";
        Size = new Size(795, 558);
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        _cardsPanel.ResumeLayout(false);
        cardUsuarios.ResumeLayout(false);
        cardUsuarios.PerformLayout();
        cardCategorias.ResumeLayout(false);
        cardCategorias.PerformLayout();
        guna2Panel1.ResumeLayout(false);
        guna2Panel1.PerformLayout();
        cardGames.ResumeLayout(false);
        cardGames.PerformLayout();
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }
}
