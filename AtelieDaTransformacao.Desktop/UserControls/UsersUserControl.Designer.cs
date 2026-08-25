using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

partial class UsersUserControl
{
    private System.ComponentModel.IContainer? components = null;

    private Guna.UI2.WinForms.Guna2Panel _toolbarCard = null!;
    private Guna.UI2.WinForms.Guna2Button _newButton = null!;
    private Guna.UI2.WinForms.Guna2Button _refreshButton = null!;
    private Guna.UI2.WinForms.Guna2Button _deleteButton = null!;
    private Guna.UI2.WinForms.Guna2Button _activationButton = null!;

    private Guna.UI2.WinForms.Guna2Panel _activeCard = null!;
    private Label _activeTitleLabel = null!;
    private Label _activeCountLabel = null!;
    private Label _activeDescriptionLabel = null!;

    private Guna.UI2.WinForms.Guna2Panel _inactiveCard = null!;
    private Label _inactiveTitleLabel = null!;
    private Label _inactiveCountLabel = null!;
    private Label _inactiveDescriptionLabel = null!;

    private Guna.UI2.WinForms.Guna2Panel _tableCard = null!;
    private Label _countLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _toolbarCard = new Guna.UI2.WinForms.Guna2Panel();
        _deleteButton = new Guna.UI2.WinForms.Guna2Button();
        _inactiveCard = new Guna.UI2.WinForms.Guna2Panel();
        _inactiveDescriptionLabel = new Label();
        _inactiveCountLabel = new Label();
        _inactiveTitleLabel = new Label();
        _activationButton = new Guna.UI2.WinForms.Guna2Button();
        _activeCard = new Guna.UI2.WinForms.Guna2Panel();
        _activeDescriptionLabel = new Label();
        _activeCountLabel = new Label();
        _activeTitleLabel = new Label();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _newButton = new Guna.UI2.WinForms.Guna2Button();
        _tableCard = new Guna.UI2.WinForms.Guna2Panel();
        _countLabel = new Label();
        _grid = new Guna.UI2.WinForms.Guna2DataGridView();
        _toolbarCard.SuspendLayout();
        _inactiveCard.SuspendLayout();
        _activeCard.SuspendLayout();
        _tableCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        SuspendLayout();
        // 
        // _toolbarCard
        // 
        _toolbarCard.BackColor = Color.LightGray;
        _toolbarCard.BorderColor = Color.LightGray;
        _toolbarCard.BorderRadius = 15;
        _toolbarCard.BorderThickness = 1;
        _toolbarCard.Controls.Add(_deleteButton);
        _toolbarCard.Controls.Add(_inactiveCard);
        _toolbarCard.Controls.Add(_activationButton);
        _toolbarCard.Controls.Add(_activeCard);
        _toolbarCard.Controls.Add(_refreshButton);
        _toolbarCard.Controls.Add(_newButton);
        customizableEdges7.TopLeft = false;
        customizableEdges7.TopRight = false;
        _toolbarCard.CustomizableEdges = customizableEdges7;
        _toolbarCard.FillColor = Color.LightGray;
        _toolbarCard.Dock = DockStyle.Top;
        _toolbarCard.Location = new Point(0, 0);
        _toolbarCard.Name = "_toolbarCard";
        _toolbarCard.ShadowDecoration.CustomizableEdges = customizableEdges7;
        _toolbarCard.Size = new Size(795, 120);
        _toolbarCard.TabIndex = 0;
        // 
        // _deleteButton
        // 
        _deleteButton.BackColor = Color.Transparent;
        _deleteButton.BorderRadius = 7;
        _deleteButton.CustomizableEdges = customizableEdges1;
        _deleteButton.FillColor = Color.FromArgb(192, 0, 0);
        _deleteButton.Font = new Font("Segoe UI", 9F);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.Location = new Point(362, 39);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.ShadowDecoration.CustomizableEdges = customizableEdges1;
        _deleteButton.Size = new Size(135, 42);
        _deleteButton.TabIndex = 3;
        _deleteButton.Text = "❌ Desativar Usúario";
        // 
        // _inactiveCard
        // 
        _inactiveCard.BackColor = Color.LightGray;
        _inactiveCard.BorderColor = Color.LightGray;
        _inactiveCard.BorderRadius = 10;
        _inactiveCard.BorderThickness = 1;
        _inactiveCard.Controls.Add(_inactiveDescriptionLabel);
        _inactiveCard.Controls.Add(_inactiveCountLabel);
        _inactiveCard.Controls.Add(_inactiveTitleLabel);
        _inactiveCard.CustomizableEdges = customizableEdges2;
        _inactiveCard.FillColor = Color.White;
        _inactiveCard.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _inactiveCard.Location = new Point(562, 17);
        _inactiveCard.Name = "_inactiveCard";
        _inactiveCard.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _inactiveCard.Size = new Size(218, 84);
        _inactiveCard.TabIndex = 5;
        // 
        // _inactiveDescriptionLabel
        // 
        _inactiveDescriptionLabel.BackColor = Color.Transparent;
        _inactiveDescriptionLabel.Font = new Font("Segoe UI", 8F);
        _inactiveDescriptionLabel.ForeColor = Color.DimGray;
        _inactiveDescriptionLabel.Location = new Point(75, 44);
        _inactiveDescriptionLabel.Name = "_inactiveDescriptionLabel";
        _inactiveDescriptionLabel.Size = new Size(130, 28);
        _inactiveDescriptionLabel.TabIndex = 3;
        _inactiveDescriptionLabel.Text = "Usuários sem acesso";
        _inactiveDescriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _inactiveCountLabel
        // 
        _inactiveCountLabel.BackColor = Color.Transparent;
        _inactiveCountLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
        _inactiveCountLabel.ForeColor = Color.Black;
        _inactiveCountLabel.Location = new Point(14, 34);
        _inactiveCountLabel.Name = "_inactiveCountLabel";
        _inactiveCountLabel.Size = new Size(65, 35);
        _inactiveCountLabel.TabIndex = 2;
        _inactiveCountLabel.Text = "0";
        _inactiveCountLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _inactiveTitleLabel
        // 
        _inactiveTitleLabel.BackColor = Color.Transparent;
        _inactiveTitleLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        _inactiveTitleLabel.ForeColor = Color.Red;
        _inactiveTitleLabel.Location = new Point(14, 13);
        _inactiveTitleLabel.Name = "_inactiveTitleLabel";
        _inactiveTitleLabel.Size = new Size(180, 22);
        _inactiveTitleLabel.TabIndex = 1;
        _inactiveTitleLabel.Text = "● Usuários Inativos";
        _inactiveTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _activationButton
        // 
        _activationButton.BackColor = Color.Transparent;
        _activationButton.BorderRadius = 7;
        _activationButton.CustomizableEdges = customizableEdges3;
        _activationButton.FillColor = Color.MidnightBlue;
        _activationButton.Font = new Font("Segoe UI", 9F);
        _activationButton.ForeColor = Color.White;
        _activationButton.Location = new Point(235, 39);
        _activationButton.Name = "_activationButton";
        _activationButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
        _activationButton.Size = new Size(117, 42);
        _activationButton.TabIndex = 2;
        _activationButton.Text = "✔️ Ativar Usúario";
        // 
        // _activeCard
        // 
        _activeCard.BackColor = Color.LightGray;
        _activeCard.BorderColor = Color.LightGray;
        _activeCard.BorderRadius = 10;
        _activeCard.BorderThickness = 1;
        _activeCard.Controls.Add(_activeDescriptionLabel);
        _activeCard.Controls.Add(_activeCountLabel);
        _activeCard.Controls.Add(_activeTitleLabel);
        _activeCard.CustomizableEdges = customizableEdges4;
        _activeCard.FillColor = Color.White;
        _activeCard.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _activeCard.Location = new Point(333, 17);
        _activeCard.Name = "_activeCard";
        _activeCard.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _activeCard.Size = new Size(218, 84);
        _activeCard.TabIndex = 4;
        // 
        // _activeDescriptionLabel
        // 
        _activeDescriptionLabel.BackColor = Color.Transparent;
        _activeDescriptionLabel.Font = new Font("Segoe UI", 8F);
        _activeDescriptionLabel.ForeColor = Color.DimGray;
        _activeDescriptionLabel.Location = new Point(75, 44);
        _activeDescriptionLabel.Name = "_activeDescriptionLabel";
        _activeDescriptionLabel.Size = new Size(130, 28);
        _activeDescriptionLabel.TabIndex = 3;
        _activeDescriptionLabel.Text = "Usuários com acesso ativo";
        _activeDescriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _activeCountLabel
        // 
        _activeCountLabel.BackColor = Color.Transparent;
        _activeCountLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
        _activeCountLabel.ForeColor = Color.Black;
        _activeCountLabel.Location = new Point(14, 34);
        _activeCountLabel.Name = "_activeCountLabel";
        _activeCountLabel.Size = new Size(65, 35);
        _activeCountLabel.TabIndex = 2;
        _activeCountLabel.Text = "0";
        _activeCountLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _activeTitleLabel
        // 
        _activeTitleLabel.BackColor = Color.Transparent;
        _activeTitleLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        _activeTitleLabel.ForeColor = Color.Green;
        _activeTitleLabel.Location = new Point(14, 13);
        _activeTitleLabel.Name = "_activeTitleLabel";
        _activeTitleLabel.Size = new Size(170, 22);
        _activeTitleLabel.TabIndex = 1;
        _activeTitleLabel.Text = "● Usuários Ativos";
        _activeTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _refreshButton
        // 
        _refreshButton.BackColor = Color.Transparent;
        _refreshButton.BorderRadius = 7;
        _refreshButton.CustomizableEdges = customizableEdges5;
        _refreshButton.FillColor = Color.Gray;
        _refreshButton.Font = new Font("Segoe UI", 9F);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(137, 39);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = customizableEdges5;
        _refreshButton.Size = new Size(90, 42);
        _refreshButton.TabIndex = 1;
        _refreshButton.Text = "🔄️ Atualizar";
        // 
        // _newButton
        // 
        _newButton.BackColor = Color.Transparent;
        _newButton.BorderRadius = 7;
        _newButton.CustomizableEdges = customizableEdges6;
        _newButton.FillColor = Color.Green;
        _newButton.Font = new Font("Segoe UI", 9F);
        _newButton.ForeColor = Color.White;
        _newButton.Location = new Point(18, 39);
        _newButton.Name = "_newButton";
        _newButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _newButton.Size = new Size(111, 42);
        _newButton.TabIndex = 0;
        _newButton.Text = "+ Novo Usuário";
        // 
        // _tableCard
        // 
        _tableCard.BackColor = Color.Transparent;
        _tableCard.BorderColor = Color.White;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_countLabel);
        _tableCard.CustomizableEdges = customizableEdges8;
        _tableCard.FillColor = Color.White;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.Location = new Point(0, 120);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(14, 14, 14, 40);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _tableCard.Size = new Size(795, 438);
        _tableCard.TabIndex = 6;
        // 
        // _countLabel
        // 
        _countLabel.BackColor = Color.Gainsboro;
        _countLabel.Font = new Font("Segoe UI", 8.5F);
        _countLabel.ForeColor = Color.Gray;
        _countLabel.Dock = DockStyle.Bottom;
        _countLabel.Location = new Point(14, 396);
        _countLabel.Name = "_countLabel";
        _countLabel.Size = new Size(767, 28);
        _countLabel.TabIndex = 1;
        _countLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.BorderStyle = BorderStyle.FixedSingle;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(245, 246, 248);
        dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F);
        dataGridViewCellStyle1.ForeColor = Color.Black;
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
        _grid.GridColor = Color.Black;
        _grid.Dock = DockStyle.Fill;
        _grid.Location = new Point(14, 14);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 40;
        _grid.Size = new Size(767, 382);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.Black;
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(245, 246, 248);
        _grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 9F);
        _grid.ThemeStyle.HeaderStyle.ForeColor = Color.Black;
        _grid.ThemeStyle.HeaderStyle.Height = 42;
        _grid.ThemeStyle.ReadOnly = true;
        _grid.ThemeStyle.RowsStyle.BackColor = SystemColors.Window;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlText;
        _grid.ThemeStyle.RowsStyle.Height = 40;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(225, 225, 225);
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
        // 
        // UsersUserControl
        // 
        BackColor = Color.Transparent;
        Controls.Add(_tableCard);
        Controls.Add(_toolbarCard);
        Name = "UsersUserControl";
        Size = new Size(795, 558);
        _toolbarCard.ResumeLayout(false);
        _inactiveCard.ResumeLayout(false);
        _activeCard.ResumeLayout(false);
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }

    private Guna.UI2.WinForms.Guna2DataGridView _grid;
}