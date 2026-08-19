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
    private Label _countLabel = null!;

    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _toolbarCard = new Guna.UI2.WinForms.Guna2Panel();
        _deleteButton = new Guna.UI2.WinForms.Guna2Button();
        _editButton = new Guna.UI2.WinForms.Guna2Button();
        _newButton = new Guna.UI2.WinForms.Guna2Button();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _categoryComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
        _searchTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _tableCard = new Guna.UI2.WinForms.Guna2Panel();
        _grid = new Guna.UI2.WinForms.Guna2DataGridView();
        _countLabel = new Label();
        _toolbarCard.SuspendLayout();
        _tableCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        SuspendLayout();
        // 
        // _toolbarCard
        // 
        _toolbarCard.BorderColor = Color.FromArgb(226, 229, 236);
        _toolbarCard.BorderRadius = 12;
        _toolbarCard.BorderThickness = 1;
        _toolbarCard.Controls.Add(_deleteButton);
        _toolbarCard.Controls.Add(_editButton);
        _toolbarCard.Controls.Add(_newButton);
        _toolbarCard.Controls.Add(_refreshButton);
        _toolbarCard.Controls.Add(_categoryComboBox);
        _toolbarCard.Controls.Add(_searchTextBox);
        _toolbarCard.CustomizableEdges = customizableEdges13;
        _toolbarCard.Dock = DockStyle.Top;
        _toolbarCard.FillColor = Color.White;
        _toolbarCard.Location = new Point(0, 0);
        _toolbarCard.Name = "_toolbarCard";
        _toolbarCard.Padding = new Padding(14);
        _toolbarCard.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _toolbarCard.Size = new Size(795, 92);
        _toolbarCard.TabIndex = 1;
        // 
        // _deleteButton
        // 
        _deleteButton.BorderRadius = 10;
        _deleteButton.CustomizableEdges = customizableEdges1;
        _deleteButton.FillColor = Color.FromArgb(192, 0, 0);
        _deleteButton.Font = new Font("Segoe UI", 9F);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.HoverState.FillColor = Color.FromArgb(248, 220, 220);
        _deleteButton.Location = new Point(407, 29);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _deleteButton.Size = new Size(80, 33);
        _deleteButton.TabIndex = 0;
        _deleteButton.Text = "🗑️ Excluir";
        // 
        // _editButton
        // 
        _editButton.BorderRadius = 10;
        _editButton.CustomizableEdges = customizableEdges3;
        _editButton.FillColor = Color.MidnightBlue;
        _editButton.Font = new Font("Segoe UI", 9F);
        _editButton.ForeColor = Color.White;
        _editButton.Location = new Point(322, 28);
        _editButton.Name = "_editButton";
        _editButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _editButton.Size = new Size(79, 33);
        _editButton.TabIndex = 1;
        _editButton.Text = "✏️ Editar";
        // 
        // _newButton
        // 
        _newButton.BorderRadius = 10;
        _newButton.CustomizableEdges = customizableEdges5;
        _newButton.FillColor = Color.Green;
        _newButton.Font = new Font("Segoe UI", 9F);
        _newButton.ForeColor = Color.White;
        _newButton.Location = new Point(220, 28);
        _newButton.Name = "_newButton";
        _newButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _newButton.Size = new Size(96, 34);
        _newButton.TabIndex = 2;
        _newButton.Text = "+ Novo Produto";
        // 
        // _refreshButton
        // 
        _refreshButton.BorderRadius = 10;
        _refreshButton.CustomizableEdges = customizableEdges7;
        _refreshButton.FillColor = Color.Gray;
        _refreshButton.Font = new Font("Segoe UI", 9F);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(493, 29);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _refreshButton.Size = new Size(88, 33);
        _refreshButton.TabIndex = 3;
        _refreshButton.Text = "🔄️ Atualizar";
        // 
        // _categoryComboBox
        // 
        _categoryComboBox.BackColor = Color.Transparent;
        _categoryComboBox.BorderColor = Color.FromArgb(226, 229, 236);
        _categoryComboBox.BorderRadius = 10;
        _categoryComboBox.CustomizableEdges = customizableEdges9;
        _categoryComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        _categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _categoryComboBox.FocusedColor = Color.Empty;
        _categoryComboBox.Font = new Font("Segoe UI", 9.5F);
        _categoryComboBox.ForeColor = Color.FromArgb(68, 88, 112);
        _categoryComboBox.ItemHeight = 30;
        _categoryComboBox.Location = new Point(587, 26);
        _categoryComboBox.Name = "_categoryComboBox";
        _categoryComboBox.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _categoryComboBox.Size = new Size(157, 36);
        _categoryComboBox.TabIndex = 4;
        // 
        // _searchTextBox
        // 
        _searchTextBox.BorderColor = Color.FromArgb(226, 229, 236);
        _searchTextBox.BorderRadius = 10;
        _searchTextBox.CustomizableEdges = customizableEdges11;
        _searchTextBox.DefaultText = "";
        _searchTextBox.FocusedState.BorderColor = Color.FromArgb(74, 108, 247);
        _searchTextBox.Font = new Font("Segoe UI", 9.5F);
        _searchTextBox.Location = new Point(11, 26);
        _searchTextBox.Name = "_searchTextBox";
        _searchTextBox.PlaceholderText = "🔎 Pesquisar produto...";
        _searchTextBox.SelectedText = "";
        _searchTextBox.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _searchTextBox.Size = new Size(203, 36);
        _searchTextBox.TabIndex = 5;
        // 
        // _tableCard
        // 
        _tableCard.BorderColor = Color.FromArgb(226, 229, 236);
        _tableCard.BorderRadius = 12;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_countLabel);
        _tableCard.CustomizableEdges = customizableEdges15;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 92);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(14, 14, 14, 40);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges16;
        _tableCard.Size = new Size(795, 466);
        _tableCard.TabIndex = 0;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
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
        _grid.Location = new Point(14, 14);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 42;
        _grid.Size = new Size(767, 384);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.FromArgb(226, 229, 236);
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Font = null;
        _grid.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
        _grid.ThemeStyle.RowsStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = SystemColors.Highlight;
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = SystemColors.HighlightText;
        // 
        // _countLabel
        // 
        _countLabel.Dock = DockStyle.Bottom;
        _countLabel.Font = new Font("Segoe UI", 8.5F);
        _countLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _countLabel.Location = new Point(14, 398);
        _countLabel.Name = "_countLabel";
        _countLabel.Size = new Size(767, 28);
        _countLabel.TabIndex = 1;
        _countLabel.Text = "0 produtos";
        _countLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ProductsUserControl
        // 
        BackColor = Color.FromArgb(245, 247, 251);
        Controls.Add(_tableCard);
        Controls.Add(_toolbarCard);
        Name = "ProductsUserControl";
        Size = new Size(795, 558);
        _toolbarCard.ResumeLayout(false);
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }
    private static void ConfigureButton(Guna.UI2.WinForms.Guna2Button b, string text, int x, int width, bool primary) { b.Location = new Point(x,24); b.Size = new Size(width,42); b.Text = text; b.BorderRadius = 9; b.FillColor = primary ? LibraryTheme.Accent : Color.FromArgb(241,243,248); b.ForeColor = primary ? Color.White : LibraryTheme.Text; b.Font = new Font("Segoe UI Semibold", 9F); b.HoverState.FillColor = primary ? LibraryTheme.AccentDark : Color.FromArgb(225,229,238); }
}
