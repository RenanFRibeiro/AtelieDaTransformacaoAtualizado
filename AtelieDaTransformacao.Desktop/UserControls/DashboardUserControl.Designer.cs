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
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _headerPanel = new Guna.UI2.WinForms.Guna2Panel();
        _statusLabel = new Label();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _descriptionLabel = new Label();
        _welcomeLabel = new Label();
        _cardsPanel = new Guna.UI2.WinForms.Guna2Panel();
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
        _tableCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        SuspendLayout();
        // 
        // _headerPanel
        // 
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
        _descriptionLabel.ForeColor = Color.FromArgb(113, 120, 135);
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
        _welcomeLabel.ForeColor = Color.FromArgb(30, 34, 43);
        _welcomeLabel.Location = new Point(0, 0);
        _welcomeLabel.Name = "_welcomeLabel";
        _welcomeLabel.Size = new Size(132, 31);
        _welcomeLabel.TabIndex = 3;
        _welcomeLabel.Text = "Visão geral";
        // 
        // _cardsPanel
        // 
        _cardsPanel.CustomizableEdges = customizableEdges5;
        _cardsPanel.Dock = DockStyle.Top;
        _cardsPanel.FillColor = Color.Transparent;
        _cardsPanel.Location = new Point(0, 82);
        _cardsPanel.Name = "_cardsPanel";
        _cardsPanel.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _cardsPanel.Size = new Size(795, 122);
        _cardsPanel.TabIndex = 1;
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
        _tableCard.BorderColor = Color.FromArgb(226, 229, 236);
        _tableCard.BorderRadius = 12;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_tableTitle);
        _tableCard.CustomizableEdges = customizableEdges7;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 204);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(16);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _tableCard.Size = new Size(795, 354);
        _tableCard.TabIndex = 0;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.ColumnHeadersHeight = 42;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.FromArgb(226, 229, 236);
        _grid.Location = new Point(16, 50);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 40;
        _grid.Size = new Size(763, 288);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.FromArgb(226, 229, 236);
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Font = null;
        _grid.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
        _grid.ThemeStyle.RowsStyle.Height = 40;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = SystemColors.Highlight;
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = SystemColors.HighlightText;
        // 
        // _tableTitle
        // 
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
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }

    private static Guna.UI2.WinForms.Guna2Panel MakeCard() => new() { Width = 205, Height = 106, BorderRadius = 12, BorderThickness = 1, BorderColor = LibraryTheme.Border, FillColor = Color.White };
    private void AddCard(Guna.UI2.WinForms.Guna2Panel card, Label caption, Label value, string title, string initial, int x)
    {
        card.Location = new Point(x, 5); card.Anchor = AnchorStyles.Top | AnchorStyles.Left; caption.AutoSize = true; caption.Text = title; caption.Font = new Font("Segoe UI", 8F, FontStyle.Bold); caption.ForeColor = LibraryTheme.Muted; caption.Location = new Point(18, 15); value.AutoSize = true; value.Text = initial; value.Font = new Font("Segoe UI", 22F, FontStyle.Bold); value.ForeColor = LibraryTheme.Text; value.Location = new Point(18, 37); card.Controls.Add(value); card.Controls.Add(caption); _cardsPanel.Controls.Add(card);
    }
}
