using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;
partial class UsersUserControl
{
    private System.ComponentModel.IContainer? components=null;private Guna.UI2.WinForms.Guna2Panel _toolbarCard=null!;private Guna.UI2.WinForms.Guna2Button _newButton=null!;private Guna.UI2.WinForms.Guna2Button _refreshButton=null!;private Guna.UI2.WinForms.Guna2Button _deleteButton=null!;private Guna.UI2.WinForms.Guna2Panel _tableCard=null!;private Guna.UI2.WinForms.Guna2DataGridView _grid=null!;private Label _countLabel=null!;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _toolbarCard = new Guna.UI2.WinForms.Guna2Panel();
        _activationButton = new Guna.UI2.WinForms.Guna2Button();
        _deleteButton = new Guna.UI2.WinForms.Guna2Button();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _newButton = new Guna.UI2.WinForms.Guna2Button();
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
        _toolbarCard.BackColor = Color.White;
        _toolbarCard.BorderColor = Color.LightGray;
        _toolbarCard.BorderRadius = 15;
        _toolbarCard.BorderThickness = 1;
        _toolbarCard.Controls.Add(_activationButton);
        _toolbarCard.Controls.Add(_deleteButton);
        _toolbarCard.Controls.Add(_refreshButton);
        _toolbarCard.Controls.Add(_newButton);
        customizableEdges9.TopLeft = false;
        customizableEdges9.TopRight = false;
        _toolbarCard.CustomizableEdges = customizableEdges9;
        _toolbarCard.Dock = DockStyle.Top;
        _toolbarCard.FillColor = Color.LightGray;
        _toolbarCard.Location = new Point(0, 0);
        _toolbarCard.Name = "_toolbarCard";
        _toolbarCard.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _toolbarCard.Size = new Size(715, 78);
        _toolbarCard.TabIndex = 1;
        // 
        // _activationButton
        // 
        _activationButton.BackColor = Color.Transparent;
        _activationButton.BorderRadius = 7;
        _activationButton.CustomizableEdges = customizableEdges1;
        _activationButton.DisabledState.BorderColor = Color.DarkGray;
        _activationButton.DisabledState.CustomBorderColor = Color.DarkGray;
        _activationButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
        _activationButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
        _activationButton.FillColor = Color.MidnightBlue;
        _activationButton.Font = new Font("Segoe UI", 9F);
        _activationButton.ForeColor = Color.White;
        _activationButton.Location = new Point(250, 17);
        _activationButton.Name = "_activationButton";
        _activationButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _activationButton.Size = new Size(117, 42);
        _activationButton.TabIndex = 3;
        _activationButton.Text = "✔️ Ativar Usúario";
        // 
        // _deleteButton
        // 
        _deleteButton.BackColor = Color.Transparent;
        _deleteButton.BorderRadius = 7;
        _deleteButton.CustomizableEdges = customizableEdges3;
        _deleteButton.FillColor = Color.FromArgb(192, 0, 0);
        _deleteButton.Font = new Font("Segoe UI", 9F);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.Location = new Point(373, 17);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _deleteButton.Size = new Size(135, 42);
        _deleteButton.TabIndex = 0;
        _deleteButton.Text = "❌ Desativar Usúario";
        // 
        // _refreshButton
        // 
        _refreshButton.BackColor = Color.Transparent;
        _refreshButton.BorderRadius = 7;
        _refreshButton.CustomizableEdges = customizableEdges5;
        _refreshButton.FillColor = Color.Gray;
        _refreshButton.Font = new Font("Segoe UI", 9F);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(154, 17);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _refreshButton.Size = new Size(90, 42);
        _refreshButton.TabIndex = 1;
        _refreshButton.Text = "🔄️ Atualizar";
        // 
        // _newButton
        // 
        _newButton.BackColor = Color.Transparent;
        _newButton.BorderRadius = 7;
        _newButton.CustomizableEdges = customizableEdges7;
        _newButton.FillColor = Color.Green;
        _newButton.Font = new Font("Segoe UI", 9F);
        _newButton.ForeColor = Color.White;
        _newButton.Location = new Point(37, 17);
        _newButton.Name = "_newButton";
        _newButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _newButton.Size = new Size(111, 42);
        _newButton.TabIndex = 2;
        _newButton.Text = "+ Novo Usuário";
        // 
        // _tableCard
        // 
        _tableCard.BackColor = Color.Transparent;
        _tableCard.BorderColor = Color.White;
        _tableCard.BorderRadius = 15;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_countLabel);
        customizableEdges11.TopLeft = false;
        customizableEdges11.TopRight = false;
        _tableCard.CustomizableEdges = customizableEdges11;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 78);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(14, 14, 14, 40);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _tableCard.Size = new Size(715, 358);
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
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(225, 225, 225);
        dataGridViewCellStyle2.SelectionForeColor = Color.Black;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.Black;
        _grid.BorderStyle = BorderStyle.FixedSingle;
        _grid.Location = new Point(14, 14);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 42;
        _grid.Size = new Size(687, 276);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.Black;
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Font = null;
        _grid.ThemeStyle.HeaderStyle.ForeColor = Color.Empty;
        _grid.ThemeStyle.HeaderStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
        _grid.ThemeStyle.RowsStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(225, 225, 225);
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
        // 
        // _countLabel
        // 
        _countLabel.Dock = DockStyle.Bottom;
        _countLabel.Font = new Font("Segoe UI", 8.5F);
        _countLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _countLabel.Location = new Point(14, 290);
        _countLabel.Name = "_countLabel";
        _countLabel.Size = new Size(687, 28);
        _countLabel.TabIndex = 1;
        _countLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // UsersUserControl
        // 
        BackColor = Color.Transparent;
        Controls.Add(_tableCard);
        Controls.Add(_toolbarCard);
        Name = "UsersUserControl";
        Size = new Size(715, 436);
        _toolbarCard.ResumeLayout(false);
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }
    private static void Configure(Guna.UI2.WinForms.Guna2Button b,string text,int x,int width,bool primary){b.Location=new Point(x,18);b.Size=new Size(width,40);b.Text=text;b.BorderRadius=9;b.FillColor=primary?LibraryTheme.Accent:Color.FromArgb(241,243,248);b.ForeColor=primary?Color.White:LibraryTheme.Text;b.Font=new Font("Segoe UI Semibold",9F);b.HoverState.FillColor=primary?LibraryTheme.AccentDark:Color.FromArgb(225,229,238); }

    private Guna.UI2.WinForms.Guna2Button _activationButton;
}
