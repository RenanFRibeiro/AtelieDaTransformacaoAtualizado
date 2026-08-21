using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

partial class ProductsUserControl
{
    private System.ComponentModel.IContainer? components = null;
    private Guna.UI2.WinForms.Guna2Panel _toolbarCard = null!;
    private Guna.UI2.WinForms.Guna2TextBox _searchTextBox = null!;
    private Guna.UI2.WinForms.Guna2ComboBox _categoryComboBox = null!;
    private Guna.UI2.WinForms.Guna2Button _refreshButton = null!;
    private Guna.UI2.WinForms.Guna2Button _newButton = null!;
    private Guna.UI2.WinForms.Guna2Button _editButton = null!;
    private Guna.UI2.WinForms.Guna2Button _deleteButton = null!;
    private Guna.UI2.WinForms.Guna2Panel _tableCard = null!;
    private Guna.UI2.WinForms.Guna2DataGridView _grid = null!;
    private DataGridViewTextBoxColumn _idColumn = null!;
    private DataGridViewTextBoxColumn _productColumn = null!;
    private DataGridViewTextBoxColumn _categoryColumn = null!;
    private DataGridViewTextBoxColumn _priceColumn = null!;
    private DataGridViewTextBoxColumn _stockColumn = null!;
    private DataGridViewTextBoxColumn _statusColumn = null!;
    private DataGridViewTextBoxColumn _featuredColumn = null!;
    private DataGridViewButtonColumn _actionsColumn = null!;
    private Label _countLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges e1 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e2 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e3 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e4 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e5 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e6 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e7 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e8 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e9 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e10 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e11 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e12 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e13 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e14 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e15 = new();
        Guna.UI2.WinForms.Suite.CustomizableEdges e16 = new();
        DataGridViewCellStyle headerStyle = new();
        DataGridViewCellStyle bodyStyle = new();
        DataGridViewCellStyle actionStyle = new();

        _toolbarCard = new Guna.UI2.WinForms.Guna2Panel();
        _deleteButton = new Guna.UI2.WinForms.Guna2Button();
        _editButton = new Guna.UI2.WinForms.Guna2Button();
        _newButton = new Guna.UI2.WinForms.Guna2Button();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _categoryComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
        _searchTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _tableCard = new Guna.UI2.WinForms.Guna2Panel();
        _grid = new Guna.UI2.WinForms.Guna2DataGridView();
        _idColumn = new DataGridViewTextBoxColumn();
        _productColumn = new DataGridViewTextBoxColumn();
        _categoryColumn = new DataGridViewTextBoxColumn();
        _priceColumn = new DataGridViewTextBoxColumn();
        _stockColumn = new DataGridViewTextBoxColumn();
        _statusColumn = new DataGridViewTextBoxColumn();
        _featuredColumn = new DataGridViewTextBoxColumn();
        _actionsColumn = new DataGridViewButtonColumn();
        _countLabel = new Label();

        _toolbarCard.SuspendLayout();
        _tableCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        SuspendLayout();

        _toolbarCard.BackColor = Color.White;
        _toolbarCard.BorderColor = Color.White;
        _toolbarCard.BorderRadius = 15;
        _toolbarCard.BorderThickness = 1;
        _toolbarCard.Controls.Add(_deleteButton);
        _toolbarCard.Controls.Add(_editButton);
        _toolbarCard.Controls.Add(_newButton);
        _toolbarCard.Controls.Add(_refreshButton);
        _toolbarCard.Controls.Add(_categoryComboBox);
        _toolbarCard.Controls.Add(_searchTextBox);
        e1.TopLeft = false; e1.TopRight = false;
        _toolbarCard.CustomizableEdges = e1;
        _toolbarCard.Dock = DockStyle.Top;
        _toolbarCard.FillColor = Color.LightGray;
        _toolbarCard.Location = new Point(0, 0);
        _toolbarCard.Name = "_toolbarCard";
        _toolbarCard.Padding = new Padding(14);
        _toolbarCard.ShadowDecoration.CustomizableEdges = e2;
        _toolbarCard.Size = new Size(795, 92);
        _toolbarCard.TabIndex = 1;

        _deleteButton.BackColor = Color.Transparent;
        _deleteButton.BorderRadius = 10;
        _deleteButton.CustomizableEdges = e3;
        _deleteButton.FillColor = Color.FromArgb(192, 0, 0);
        _deleteButton.Font = new Font("Segoe UI", 9F);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.Location = new Point(370, 30);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.ShadowDecoration.CustomizableEdges = e4;
        _deleteButton.Size = new Size(80, 36);
        _deleteButton.TabIndex = 0;
        _deleteButton.Text = "🗑️ Excluir";

        _editButton.BackColor = Color.Transparent;
        _editButton.BorderRadius = 10;
        _editButton.CustomizableEdges = e5;
        _editButton.FillColor = Color.MidnightBlue;
        _editButton.Font = new Font("Segoe UI", 9F);
        _editButton.ForeColor = Color.White;
        _editButton.Location = new Point(285, 30);
        _editButton.Name = "_editButton";
        _editButton.ShadowDecoration.CustomizableEdges = e6;
        _editButton.Size = new Size(79, 36);
        _editButton.TabIndex = 1;
        _editButton.Text = "✏️ Editar";

        _newButton.BackColor = Color.Transparent;
        _newButton.BorderRadius = 10;
        _newButton.CustomizableEdges = e7;
        _newButton.FillColor = Color.Green;
        _newButton.Font = new Font("Segoe UI", 9F);
        _newButton.ForeColor = Color.White;
        _newButton.Location = new Point(183, 30);
        _newButton.Name = "_newButton";
        _newButton.ShadowDecoration.CustomizableEdges = e8;
        _newButton.Size = new Size(96, 36);
        _newButton.TabIndex = 2;
        _newButton.Text = "+ Novo Produto";

        _refreshButton.BackColor = Color.Transparent;
        _refreshButton.BorderRadius = 10;
        _refreshButton.CustomizableEdges = e9;
        _refreshButton.FillColor = Color.Gray;
        _refreshButton.Font = new Font("Segoe UI", 9F);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(456, 30);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = e10;
        _refreshButton.Size = new Size(88, 36);
        _refreshButton.TabIndex = 3;
        _refreshButton.Text = "🔄️ Atualizar";

        _categoryComboBox.BackColor = Color.Transparent;
        _categoryComboBox.BorderColor = Color.FromArgb(226, 229, 236);
        _categoryComboBox.BorderRadius = 10;
        _categoryComboBox.CustomizableEdges = e11;
        _categoryComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        _categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _categoryComboBox.Font = new Font("Segoe UI", 9.5F);
        _categoryComboBox.ForeColor = Color.FromArgb(68, 88, 112);
        _categoryComboBox.ItemHeight = 30;
        _categoryComboBox.Location = new Point(550, 30);
        _categoryComboBox.Name = "_categoryComboBox";
        _categoryComboBox.ShadowDecoration.CustomizableEdges = e12;
        _categoryComboBox.Size = new Size(177, 36);
        _categoryComboBox.TabIndex = 4;

        _searchTextBox.BackColor = Color.Transparent;
        _searchTextBox.BorderColor = Color.FromArgb(226, 229, 236);
        _searchTextBox.BorderRadius = 10;
        _searchTextBox.CustomizableEdges = e13;
        _searchTextBox.DefaultText = "";
        _searchTextBox.FocusedState.BorderColor = Color.FromArgb(74, 108, 247);
        _searchTextBox.Font = new Font("Segoe UI", 9.5F);
        _searchTextBox.Location = new Point(11, 30);
        _searchTextBox.Name = "_searchTextBox";
        _searchTextBox.PlaceholderText = "🔎 Pesquisar produto...";
        _searchTextBox.SelectedText = "";
        _searchTextBox.ShadowDecoration.CustomizableEdges = e14;
        _searchTextBox.Size = new Size(166, 36);
        _searchTextBox.TabIndex = 5;

        _tableCard.BackColor = Color.Transparent;
        _tableCard.BorderColor = Color.White;
        _tableCard.BorderRadius = 15;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_countLabel);
        e15.TopLeft = false; e15.TopRight = false;
        _tableCard.CustomizableEdges = e15;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 92);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(14, 14, 14, 40);
        _tableCard.ShadowDecoration.CustomizableEdges = e16;
        _tableCard.Size = new Size(795, 466);
        _tableCard.TabIndex = 0;

        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeColumns = false;
        _grid.AllowUserToResizeRows = false;
        _grid.AutoGenerateColumns = false;
        _grid.BorderStyle = BorderStyle.None;
        _grid.ColumnHeadersHeight = 42;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.FromArgb(220, 220, 220);
        _grid.MultiSelect = false;
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 40;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.ScrollBars = ScrollBars.Vertical;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _grid.Name = "_grid";
        _grid.TabIndex = 0;

        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        headerStyle.BackColor = LibraryTheme.AccentDark;
        headerStyle.Font = new Font("Segoe UI Semibold", 9F);
        headerStyle.ForeColor = Color.White;
        headerStyle.SelectionBackColor = LibraryTheme.AccentDark;
        headerStyle.SelectionForeColor = Color.White;
        _grid.ColumnHeadersDefaultCellStyle = headerStyle;

        bodyStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        bodyStyle.BackColor = Color.White;
        bodyStyle.Font = new Font("Segoe UI", 9F);
        bodyStyle.ForeColor = LibraryTheme.Text;
        bodyStyle.SelectionBackColor = Color.FromArgb(238, 232, 224);
        bodyStyle.SelectionForeColor = LibraryTheme.Text;
        bodyStyle.Padding = new Padding(6, 0, 6, 0);
        _grid.DefaultCellStyle = bodyStyle;

        _idColumn.Name = "Id"; _idColumn.DataPropertyName = "Id"; _idColumn.Visible = false;
        _productColumn.Name = "Produto"; _productColumn.HeaderText = "Produto"; _productColumn.DataPropertyName = "Produto"; _productColumn.FillWeight = 22F; _productColumn.ReadOnly = true;
        _categoryColumn.Name = "Categoria"; _categoryColumn.HeaderText = "Categoria"; _categoryColumn.DataPropertyName = "Categoria"; _categoryColumn.FillWeight = 18F; _categoryColumn.ReadOnly = true;
        _priceColumn.Name = "Preço"; _priceColumn.HeaderText = "Preço"; _priceColumn.DataPropertyName = "Preço"; _priceColumn.FillWeight = 14F; _priceColumn.ReadOnly = true;
        _stockColumn.Name = "Estoque"; _stockColumn.HeaderText = "Estoque"; _stockColumn.DataPropertyName = "Estoque"; _stockColumn.FillWeight = 11F; _stockColumn.ReadOnly = true;
        _statusColumn.Name = "Status"; _statusColumn.HeaderText = "Status"; _statusColumn.DataPropertyName = "Status"; _statusColumn.FillWeight = 15F; _statusColumn.ReadOnly = true;
        _featuredColumn.Name = "Destaque"; _featuredColumn.HeaderText = "Destaque"; _featuredColumn.DataPropertyName = "Destaque"; _featuredColumn.FillWeight = 11F; _featuredColumn.ReadOnly = true;
        _actionsColumn.Name = "Acoes"; _actionsColumn.HeaderText = "Ações"; _actionsColumn.DataPropertyName = "Acoes"; _actionsColumn.Text = "Visualizar"; _actionsColumn.UseColumnTextForButtonValue = true; _actionsColumn.FillWeight = 14F; _actionsColumn.FlatStyle = FlatStyle.Flat;
        actionStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        actionStyle.BackColor = LibraryTheme.Accent;
        actionStyle.ForeColor = Color.White;
        actionStyle.SelectionBackColor = LibraryTheme.AccentDark;
        actionStyle.SelectionForeColor = Color.White;
        _actionsColumn.DefaultCellStyle = actionStyle;

        _grid.Columns.AddRange(new DataGridViewColumn[]
        { _idColumn, _productColumn, _categoryColumn, _priceColumn, _stockColumn, _statusColumn, _featuredColumn, _actionsColumn });

        _countLabel.BackColor = Color.WhiteSmoke;
        _countLabel.Dock = DockStyle.Bottom;
        _countLabel.Font = new Font("Segoe UI", 8.5F);
        _countLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _countLabel.Location = new Point(14, 398);
        _countLabel.Name = "_countLabel";
        _countLabel.Size = new Size(767, 28);
        _countLabel.TabIndex = 1;
        _countLabel.Text = "0 produtos";
        _countLabel.TextAlign = ContentAlignment.MiddleLeft;

        BackColor = Color.Transparent;
        Controls.Add(_tableCard);
        Controls.Add(_toolbarCard);
        Name = "ProductsUserControl";
        Size = new Size(795, 558);

        _tableCard.ResumeLayout(false);
        _toolbarCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }
}
