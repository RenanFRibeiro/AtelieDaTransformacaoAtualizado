using AtelieDaTransformacao.Desktop.Themes;

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
    private Guna.UI2.WinForms.Guna2Panel _productsCard = null!;
    private Guna.UI2.WinForms.Guna2Panel _stockCard = null!;
    private Guna.UI2.WinForms.Guna2Panel _categoriesCard = null!;
    private Guna.UI2.WinForms.Guna2Panel _lowStockCard = null!;
    private Guna.UI2.WinForms.Guna2Panel _featuredCard = null!;
    private Label _productsCaptionLabel = null!;
    private Label _productsValueLabel = null!;
    private Label _stockCaptionLabel = null!;
    private Label _stockValueLabel = null!;
    private Label _categoriesCaptionLabel = null!;
    private Label _categoriesValueLabel = null!;
    private Label _lowStockCaptionLabel = null!;
    private Label _lowStockValueLabel = null!;
    private Label _featuredCaptionLabel = null!;
    private Label _featuredValueLabel = null!;
    private Guna.UI2.WinForms.Guna2Panel _tableCard = null!;
    private Label _tableTitle = null!;
    private Guna.UI2.WinForms.Guna2DataGridView _grid = null!;

    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _headerPanel = new Guna.UI2.WinForms.Guna2Panel();
        _statusLabel = new Label();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _descriptionLabel = new Label();
        _welcomeLabel = new Label();
        _cardsPanel = new Guna.UI2.WinForms.Guna2Panel();
        guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
        guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        pnlCorGames = new Guna.UI2.WinForms.Guna2Panel();
        cardGames = new Guna.UI2.WinForms.Guna2Panel();
        cardGamesLblDesc = new Label();
        cardGamesLblNumero = new Label();
        cardGamesLblTitulo = new Label();
        pnlCorCategorias = new Guna.UI2.WinForms.Guna2Panel();
        cardCategorias = new Guna.UI2.WinForms.Guna2Panel();
        cardCategoriasLblNumero = new Label();
        cardCategoriasLblTitulo = new Label();
        cardCategoriasLblDesc = new Label();
        _productsCaptionLabel = new Label();
        _productsValueLabel = new Label();
        _stockCaptionLabel = new Label();
        _stockValueLabel = new Label();
        _categoriesCaptionLabel = new Label();
        _categoriesValueLabel = new Label();
        _lowStockCaptionLabel = new Label();
        _lowStockValueLabel = new Label();
        _featuredCaptionLabel = new Label();
        _featuredValueLabel = new Label();
        _tableCard = new Guna.UI2.WinForms.Guna2Panel();
        _grid = new Guna.UI2.WinForms.Guna2DataGridView();
        _tableTitle = new Label();
        _headerPanel.SuspendLayout();
        _cardsPanel.SuspendLayout();
        guna2Panel1.SuspendLayout();
        cardGames.SuspendLayout();
        cardCategorias.SuspendLayout();
        _tableCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        SuspendLayout();
        // 
        // _headerPanel
        // 
        _headerPanel.BackColor = Color.LightGray;
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
        _headerPanel.TabIndex = 2;
        // 
        // _statusLabel
        // 
        _statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _statusLabel.AutoSize = true;
        _statusLabel.Font = new Font("Segoe UI", 8.5F);
        _statusLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _statusLabel.Location = new Point(1430, 20);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(130, 15);
        _statusLabel.TabIndex = 0;
        _statusLabel.Text = "Resumo em tempo real";
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
        _cardsPanel.Controls.Add(guna2Panel2);
        _cardsPanel.Controls.Add(guna2Panel1);
        _cardsPanel.Controls.Add(pnlCorGames);
        _cardsPanel.Controls.Add(cardGames);
        _cardsPanel.Controls.Add(pnlCorCategorias);
        _cardsPanel.Controls.Add(cardCategorias);
        customizableEdges17.TopLeft = false;
        customizableEdges17.TopRight = false;
        _cardsPanel.CustomizableEdges = customizableEdges17;
        _cardsPanel.Dock = DockStyle.Top;
        _cardsPanel.FillColor = Color.LightGray;
        _cardsPanel.Location = new Point(0, 82);
        _cardsPanel.Name = "_cardsPanel";
        _cardsPanel.ShadowDecoration.CustomizableEdges = customizableEdges18;
        _cardsPanel.Size = new Size(795, 122);
        _cardsPanel.TabIndex = 1;
        // 
        // guna2Panel2
        // 
        guna2Panel2.CustomizableEdges = customizableEdges5;
        guna2Panel2.FillColor = Color.FromArgb(0, 77, 147);
        guna2Panel2.Location = new Point(296, 1);
        guna2Panel2.Name = "guna2Panel2";
        guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges6;
        guna2Panel2.Size = new Size(210, 10);
        guna2Panel2.TabIndex = 4;
        // 
        // guna2Panel1
        // 
        guna2Panel1.BackColor = Color.Transparent;
        guna2Panel1.BorderRadius = 10;
        guna2Panel1.Controls.Add(label1);
        guna2Panel1.Controls.Add(label2);
        guna2Panel1.Controls.Add(label3);
        guna2Panel1.CustomizableEdges = customizableEdges7;
        guna2Panel1.FillColor = Color.Transparent;
        guna2Panel1.Location = new Point(296, 9);
        guna2Panel1.Name = "guna2Panel1";
        guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges8;
        guna2Panel1.Size = new Size(210, 112);
        guna2Panel1.TabIndex = 6;
        guna2Panel1.Paint += guna2Panel1_Paint;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.BackColor = Color.Transparent;
        label1.Font = new Font("Century Gothic", 8.25F);
        label1.ForeColor = SystemColors.ActiveCaptionText;
        label1.Location = new Point(12, 83);
        label1.Name = "label1";
        label1.Size = new Size(166, 16);
        label1.TabIndex = 3;
        label1.Text = "Total de produtos no estoque";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.BackColor = Color.Transparent;
        label2.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        label2.Location = new Point(12, 38);
        label2.Name = "label2";
        label2.Size = new Size(38, 45);
        label2.TabIndex = 2;
        label2.Text = "0";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.BackColor = Color.Transparent;
        label3.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
        label3.ForeColor = Color.FromArgb(0, 77, 147);
        label3.Location = new Point(12, 19);
        label3.Name = "label3";
        label3.Size = new Size(92, 19);
        label3.TabIndex = 1;
        label3.Text = "📦 Estoque";
        // 
        // pnlCorGames
        // 
        pnlCorGames.CustomizableEdges = customizableEdges9;
        pnlCorGames.FillColor = Color.FromArgb(0, 77, 147);
        pnlCorGames.Location = new Point(28, 1);
        pnlCorGames.Name = "pnlCorGames";
        pnlCorGames.ShadowDecoration.CustomizableEdges = customizableEdges10;
        pnlCorGames.Size = new Size(210, 10);
        pnlCorGames.TabIndex = 2;
        // 
        // cardGames
        // 
        cardGames.BackColor = Color.Transparent;
        cardGames.BorderRadius = 10;
        cardGames.Controls.Add(cardGamesLblDesc);
        cardGames.Controls.Add(cardGamesLblNumero);
        cardGames.Controls.Add(cardGamesLblTitulo);
        cardGames.CustomizableEdges = customizableEdges11;
        cardGames.FillColor = Color.Transparent;
        cardGames.Location = new Point(28, 2);
        cardGames.Name = "cardGames";
        cardGames.ShadowDecoration.CustomizableEdges = customizableEdges12;
        cardGames.Size = new Size(210, 118);
        cardGames.TabIndex = 4;
        cardGames.Paint += cardGames_Paint;
        // 
        // cardGamesLblDesc
        // 
        cardGamesLblDesc.AutoSize = true;
        cardGamesLblDesc.BackColor = Color.Transparent;
        cardGamesLblDesc.Font = new Font("Century Gothic", 8.25F);
        cardGamesLblDesc.ForeColor = SystemColors.ActiveCaptionText;
        cardGamesLblDesc.Location = new Point(12, 83);
        cardGamesLblDesc.Name = "cardGamesLblDesc";
        cardGamesLblDesc.Size = new Size(174, 16);
        cardGamesLblDesc.TabIndex = 3;
        cardGamesLblDesc.Text = "Total de produtos cadastrados";
        // 
        // cardGamesLblNumero
        // 
        cardGamesLblNumero.AutoSize = true;
        cardGamesLblNumero.BackColor = Color.Transparent;
        cardGamesLblNumero.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        cardGamesLblNumero.Location = new Point(12, 38);
        cardGamesLblNumero.Name = "cardGamesLblNumero";
        cardGamesLblNumero.Size = new Size(38, 45);
        cardGamesLblNumero.TabIndex = 2;
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
        cardGamesLblTitulo.TabIndex = 1;
        cardGamesLblTitulo.Text = "\U0001fab5 Produtos";
        // 
        // pnlCorCategorias
        // 
        pnlCorCategorias.CustomizableEdges = customizableEdges13;
        pnlCorCategorias.FillColor = Color.FromArgb(248, 148, 27);
        pnlCorCategorias.Location = new Point(551, 1);
        pnlCorCategorias.Name = "pnlCorCategorias";
        pnlCorCategorias.ShadowDecoration.CustomizableEdges = customizableEdges14;
        pnlCorCategorias.Size = new Size(210, 10);
        pnlCorCategorias.TabIndex = 5;
        // 
        // cardCategorias
        // 
        cardCategorias.BackColor = Color.Transparent;
        cardCategorias.Controls.Add(cardCategoriasLblNumero);
        cardCategorias.Controls.Add(cardCategoriasLblTitulo);
        cardCategorias.Controls.Add(cardCategoriasLblDesc);
        cardCategorias.CustomizableEdges = customizableEdges15;
        cardCategorias.FillColor = Color.Transparent;
        cardCategorias.Location = new Point(551, 1);
        cardCategorias.Name = "cardCategorias";
        cardCategorias.ShadowDecoration.CustomizableEdges = customizableEdges16;
        cardCategorias.Size = new Size(210, 120);
        cardCategorias.TabIndex = 3;
        cardCategorias.Paint += cardCategorias_Paint;
        // 
        // cardCategoriasLblNumero
        // 
        cardCategoriasLblNumero.AutoSize = true;
        cardCategoriasLblNumero.BackColor = Color.Transparent;
        cardCategoriasLblNumero.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        cardCategoriasLblNumero.Location = new Point(20, 38);
        cardCategoriasLblNumero.Name = "cardCategoriasLblNumero";
        cardCategoriasLblNumero.Size = new Size(38, 45);
        cardCategoriasLblNumero.TabIndex = 2;
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
        cardCategoriasLblDesc.ForeColor = SystemColors.ActiveCaptionText;
        cardCategoriasLblDesc.Location = new Point(20, 83);
        cardCategoriasLblDesc.Name = "cardCategoriasLblDesc";
        cardCategoriasLblDesc.Size = new Size(111, 16);
        cardCategoriasLblDesc.TabIndex = 0;
        cardCategoriasLblDesc.Text = "Total de categorias";
        // 
        // _productsCaptionLabel
        // 
        _productsCaptionLabel.Location = new Point(0, 0);
        _productsCaptionLabel.Name = "_productsCaptionLabel";
        _productsCaptionLabel.Size = new Size(100, 23);
        _productsCaptionLabel.TabIndex = 0;
        // 
        // _productsValueLabel
        // 
        _productsValueLabel.Location = new Point(0, 0);
        _productsValueLabel.Name = "_productsValueLabel";
        _productsValueLabel.Size = new Size(100, 23);
        _productsValueLabel.TabIndex = 0;
        // 
        // _stockCaptionLabel
        // 
        _stockCaptionLabel.Location = new Point(0, 0);
        _stockCaptionLabel.Name = "_stockCaptionLabel";
        _stockCaptionLabel.Size = new Size(100, 23);
        _stockCaptionLabel.TabIndex = 0;
        // 
        // _stockValueLabel
        // 
        _stockValueLabel.Location = new Point(0, 0);
        _stockValueLabel.Name = "_stockValueLabel";
        _stockValueLabel.Size = new Size(100, 23);
        _stockValueLabel.TabIndex = 0;
        // 
        // _categoriesCaptionLabel
        // 
        _categoriesCaptionLabel.Location = new Point(0, 0);
        _categoriesCaptionLabel.Name = "_categoriesCaptionLabel";
        _categoriesCaptionLabel.Size = new Size(100, 23);
        _categoriesCaptionLabel.TabIndex = 0;
        // 
        // _categoriesValueLabel
        // 
        _categoriesValueLabel.Location = new Point(0, 0);
        _categoriesValueLabel.Name = "_categoriesValueLabel";
        _categoriesValueLabel.Size = new Size(100, 23);
        _categoriesValueLabel.TabIndex = 0;
        // 
        // _lowStockCaptionLabel
        // 
        _lowStockCaptionLabel.Location = new Point(0, 0);
        _lowStockCaptionLabel.Name = "_lowStockCaptionLabel";
        _lowStockCaptionLabel.Size = new Size(100, 23);
        _lowStockCaptionLabel.TabIndex = 0;
        // 
        // _lowStockValueLabel
        // 
        _lowStockValueLabel.Location = new Point(0, 0);
        _lowStockValueLabel.Name = "_lowStockValueLabel";
        _lowStockValueLabel.Size = new Size(100, 23);
        _lowStockValueLabel.TabIndex = 0;
        // 
        // _featuredCaptionLabel
        // 
        _featuredCaptionLabel.Location = new Point(0, 0);
        _featuredCaptionLabel.Name = "_featuredCaptionLabel";
        _featuredCaptionLabel.Size = new Size(100, 23);
        _featuredCaptionLabel.TabIndex = 0;
        // 
        // _featuredValueLabel
        // 
        _featuredValueLabel.Location = new Point(0, 0);
        _featuredValueLabel.Name = "_featuredValueLabel";
        _featuredValueLabel.Size = new Size(100, 23);
        _featuredValueLabel.TabIndex = 0;
        // 
        // _tableCard
        // 
        _tableCard.BackColor = Color.Transparent;
        _tableCard.BorderColor = Color.Transparent;
        _tableCard.BorderRadius = 15;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_tableTitle);
        _tableCard.CustomizableEdges = customizableEdges19;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 204);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(16);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges20;
        _tableCard.Size = new Size(795, 354);
        _tableCard.TabIndex = 0;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.BorderStyle = BorderStyle.FixedSingle;
        _grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        _grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.ColumnHeadersHeight = 42;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(225, 225, 225);
        dataGridViewCellStyle2.SelectionForeColor = Color.Black;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.Black;
        _grid.Location = new Point(16, 50);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 40;
        _grid.Size = new Size(763, 288);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.Black;
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
        _grid.ThemeStyle.HeaderStyle.Font = null;
        _grid.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
        _grid.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.Single;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
        _grid.ThemeStyle.RowsStyle.Height = 40;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(225, 225, 225);
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
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
        guna2Panel1.ResumeLayout(false);
        guna2Panel1.PerformLayout();
        cardGames.ResumeLayout(false);
        cardGames.PerformLayout();
        cardCategorias.ResumeLayout(false);
        cardCategorias.PerformLayout();
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }

    private static Guna.UI2.WinForms.Guna2Panel MakeCard() => new() { Width = 205, Height = 106, BorderRadius = 12, BorderThickness = 1, BorderColor = LibraryTheme.Border, FillColor = Color.White };
    private void AddCard(Guna.UI2.WinForms.Guna2Panel card, Label caption, Label value, string title, string initial, int x)
    {
        card.Location = new Point(x, 5); card.Anchor = AnchorStyles.Top | AnchorStyles.Left; caption.AutoSize = true; caption.Text = title; caption.Font = new Font("Segoe UI", 8F, FontStyle.Bold); caption.ForeColor = LibraryTheme.Muted; caption.Location = new Point(18, 15); value.AutoSize = true; value.Text = initial; value.Font = new Font("Segoe UI", 22F, FontStyle.Bold); value.ForeColor = LibraryTheme.Text; value.Location = new Point(18, 37); card.Controls.Add(value); card.Controls.Add(caption); _cardsPanel.Controls.Add(card);
    }

    private Guna.UI2.WinForms.Guna2Panel pnlCorCategorias;
    private Guna.UI2.WinForms.Guna2Panel cardCategorias;
    private Label cardCategoriasLblNumero;
    private Label cardCategoriasLblTitulo;
    private Label cardCategoriasLblDesc;
    private Guna.UI2.WinForms.Guna2Panel cardGames;
    private Guna.UI2.WinForms.Guna2Panel pnlCorGames;
    private Label cardGamesLblDesc;
    private Label cardGamesLblNumero;
    private Label cardGamesLblTitulo;
    private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
    private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    private Label label1;
    private Label label2;
    private Label label3;
}
