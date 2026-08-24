using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;
partial class CategoriesUserControl
{
    private System.ComponentModel.IContainer? components = null;
    private Guna.UI2.WinForms.Guna2Panel _toolbarCard = null!; private Guna.UI2.WinForms.Guna2Button _newButton = null!; private Guna.UI2.WinForms.Guna2Button _editButton = null!; private Guna.UI2.WinForms.Guna2Button _deleteButton = null!; private Guna.UI2.WinForms.Guna2Button _refreshButton = null!; private Guna.UI2.WinForms.Guna2Panel _categoryCard = null!; private Guna.UI2.WinForms.Guna2Panel _categoryAccent = null!; private Label _categoryCardTitle = null!; private Label _categoryCountValue = null!; private Label _categoryCardDesc = null!; private Guna.UI2.WinForms.Guna2Panel _tableCard = null!; private Guna.UI2.WinForms.Guna2DataGridView _grid = null!; private Label _countLabel = null!;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
        _categoryCard = new Guna.UI2.WinForms.Guna2Panel();
        _categoryCardDesc = new Label();
        _categoryCountValue = new Label();
        _categoryCardTitle = new Label();
        _categoryAccent = new Guna.UI2.WinForms.Guna2Panel();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _deleteButton = new Guna.UI2.WinForms.Guna2Button();
        _editButton = new Guna.UI2.WinForms.Guna2Button();
        _newButton = new Guna.UI2.WinForms.Guna2Button();
        _tableCard = new Guna.UI2.WinForms.Guna2Panel();
        _grid = new Guna.UI2.WinForms.Guna2DataGridView();
        _countLabel = new Label();
        _toolbarCard.SuspendLayout();
        _categoryCard.SuspendLayout();
        _tableCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        SuspendLayout();
        // 
        // _toolbarCard
        // 
        _toolbarCard.BackColor = Color.White;
        _toolbarCard.BorderColor = Color.LightGray;
        _toolbarCard.BorderRadius = 15;
        _toolbarCard.BorderThickness = 1;
        _toolbarCard.Controls.Add(_categoryCard);
        _toolbarCard.Controls.Add(_refreshButton);
        _toolbarCard.Controls.Add(_deleteButton);
        _toolbarCard.Controls.Add(_editButton);
        _toolbarCard.Controls.Add(_newButton);
        customizableEdges13.TopLeft = false;
        customizableEdges13.TopRight = false;
        _toolbarCard.CustomizableEdges = customizableEdges13;
        _toolbarCard.Dock = DockStyle.Top;
        _toolbarCard.FillColor = Color.LightGray;
        _toolbarCard.Location = new Point(0, 0);
        _toolbarCard.Name = "_toolbarCard";
        _toolbarCard.Padding = new Padding(14);
        _toolbarCard.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _toolbarCard.Size = new Size(795, 127);
        _toolbarCard.TabIndex = 1;
        // 
        // _categoryCard
        // 
        _categoryCard.BackColor = Color.Transparent;
        _categoryCard.Controls.Add(_categoryCardDesc);
        _categoryCard.Controls.Add(_categoryCountValue);
        _categoryCard.Controls.Add(_categoryCardTitle);
        _categoryCard.Controls.Add(_categoryAccent);
        _categoryCard.CustomizableEdges = customizableEdges3;
        _categoryCard.FillColor = Color.White;
        _categoryCard.Location = new Point(612, 17);
        _categoryCard.Name = "_categoryCard";
        _categoryCard.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _categoryCard.Size = new Size(166, 99);
        _categoryCard.TabIndex = 0;
        // 
        // _categoryCardDesc
        // 
        _categoryCardDesc.AutoSize = true;
        _categoryCardDesc.BackColor = Color.Transparent;
        _categoryCardDesc.Font = new Font("Century Gothic", 8.25F);
        _categoryCardDesc.ForeColor = Color.Black;
        _categoryCardDesc.Location = new Point(12, 75);
        _categoryCardDesc.Name = "_categoryCardDesc";
        _categoryCardDesc.Size = new Size(111, 16);
        _categoryCardDesc.TabIndex = 3;
        _categoryCardDesc.Text = "Total de categorias";
        // 
        // _categoryCountValue
        // 
        _categoryCountValue.AutoSize = true;
        _categoryCountValue.BackColor = Color.Transparent;
        _categoryCountValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        _categoryCountValue.Location = new Point(12, 34);
        _categoryCountValue.Name = "_categoryCountValue";
        _categoryCountValue.Size = new Size(35, 41);
        _categoryCountValue.TabIndex = 2;
        _categoryCountValue.Text = "0";
        // 
        // _categoryCardTitle
        // 
        _categoryCardTitle.AutoSize = true;
        _categoryCardTitle.BackColor = Color.Transparent;
        _categoryCardTitle.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
        _categoryCardTitle.ForeColor = Color.FromArgb(248, 148, 27);
        _categoryCardTitle.Location = new Point(12, 15);
        _categoryCardTitle.Name = "_categoryCardTitle";
        _categoryCardTitle.Size = new Size(112, 18);
        _categoryCardTitle.TabIndex = 1;
        _categoryCardTitle.Text = "🏷️ Categorias";
        // 
        // _categoryAccent
        // 
        _categoryAccent.CustomizableEdges = customizableEdges1;
        _categoryAccent.FillColor = Color.FromArgb(248, 148, 27);
        _categoryAccent.Location = new Point(0, 0);
        _categoryAccent.Name = "_categoryAccent";
        _categoryAccent.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _categoryAccent.Size = new Size(180, 10);
        _categoryAccent.TabIndex = 0;
        // 
        // _refreshButton
        // 
        _refreshButton.BackColor = Color.Transparent;
        _refreshButton.BorderRadius = 10;
        _refreshButton.CustomizableEdges = customizableEdges5;
        _refreshButton.FillColor = Color.Gray;
        _refreshButton.Font = new Font("Segoe UI", 9F);
        _refreshButton.ForeColor = Color.FromArgb(224, 224, 224);
        _refreshButton.Location = new Point(170, 44);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _refreshButton.Size = new Size(90, 42);
        _refreshButton.TabIndex = 0;
        _refreshButton.Text = "🔄️ Atualizar";
        // 
        // _deleteButton
        // 
        _deleteButton.BackColor = Color.Transparent;
        _deleteButton.BorderRadius = 10;
        _deleteButton.CustomizableEdges = customizableEdges7;
        _deleteButton.FillColor = Color.FromArgb(192, 0, 0);
        _deleteButton.Font = new Font("Segoe UI", 9F);
        _deleteButton.ForeColor = Color.FromArgb(224, 224, 224);
        _deleteButton.Location = new Point(370, 44);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _deleteButton.Size = new Size(90, 42);
        _deleteButton.TabIndex = 1;
        _deleteButton.Text = "🗑️ Excluir";
        _deleteButton.Click += _deleteButton_Click;
        // 
        // _editButton
        // 
        _editButton.BackColor = Color.Transparent;
        _editButton.BorderRadius = 10;
        _editButton.CustomizableEdges = customizableEdges9;
        _editButton.FillColor = Color.MidnightBlue;
        _editButton.Font = new Font("Segoe UI", 9F);
        _editButton.ForeColor = Color.FromArgb(224, 224, 224);
        _editButton.Location = new Point(270, 44);
        _editButton.Name = "_editButton";
        _editButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _editButton.Size = new Size(90, 42);
        _editButton.TabIndex = 2;
        _editButton.Text = "✏️ Editar";
        // 
        // _newButton
        // 
        _newButton.BackColor = Color.Transparent;
        _newButton.BorderRadius = 10;
        _newButton.CustomizableEdges = customizableEdges11;
        _newButton.FillColor = Color.Green;
        _newButton.Font = new Font("Segoe UI", 9F);
        _newButton.ForeColor = Color.FromArgb(224, 224, 224);
        _newButton.Location = new Point(33, 44);
        _newButton.Name = "_newButton";
        _newButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _newButton.Size = new Size(127, 42);
        _newButton.TabIndex = 3;
        _newButton.Text = "+ Nova Categoria";
        // 
        // _tableCard
        // 
        _tableCard.BackColor = Color.Transparent;
        _tableCard.BorderColor = Color.White;
        _tableCard.BorderRadius = 15;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_countLabel);
        customizableEdges15.TopLeft = false;
        customizableEdges15.TopRight = false;
        _tableCard.CustomizableEdges = customizableEdges15;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 127);
        _tableCard.Margin = new Padding(3, 3, 0, 0);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(14, 14, 14, 40);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges16;
        _tableCard.Size = new Size(795, 431);
        _tableCard.TabIndex = 0;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.BorderStyle = BorderStyle.FixedSingle;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.ColumnHeadersHeight = 42;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.White;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(30, 34, 43);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(88, 52, 27);
        dataGridViewCellStyle2.SelectionForeColor = Color.White;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.Black;
        _grid.Location = new Point(14, 14);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 42;
        _grid.Size = new Size(767, 349);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.Black;
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Font = null;
        _grid.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(30, 34, 43);
        _grid.ThemeStyle.RowsStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(88, 52, 27);
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;
        // 
        // _countLabel
        // 
        _countLabel.BackColor = Color.Gainsboro;
        _countLabel.Dock = DockStyle.Bottom;
        _countLabel.Font = new Font("Segoe UI", 8.5F);
        _countLabel.ForeColor = Color.Gray;
        _countLabel.Location = new Point(14, 363);
        _countLabel.Name = "_countLabel";
        _countLabel.Size = new Size(767, 28);
        _countLabel.TabIndex = 1;
        _countLabel.Text = "0 categorias";
        _countLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // CategoriesUserControl
        // 
        BackColor = Color.Transparent;
        Controls.Add(_tableCard);
        Controls.Add(_toolbarCard);
        Name = "CategoriesUserControl";
        Size = new Size(795, 558);
        _toolbarCard.ResumeLayout(false);
        _categoryCard.ResumeLayout(false);
        _categoryCard.PerformLayout();
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }
    private static void Configure(Guna.UI2.WinForms.Guna2Button b,string text,int x,int width,bool primary){b.Location=new Point(x,18);b.Size=new Size(width,40);b.Text=text;b.BorderRadius=9;b.FillColor=primary?LibraryTheme.Accent:Color.FromArgb(241,243,248);b.ForeColor=primary?Color.White:LibraryTheme.Text;b.Font=new Font("Segoe UI Semibold",9F);b.HoverState.FillColor=primary?LibraryTheme.AccentDark:Color.FromArgb(225,229,238);}
}
