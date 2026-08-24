using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

partial class UsersUserControl
{
    private System.ComponentModel.IContainer? components = null;

    private Guna.UI2.WinForms.Guna2Panel _toolbarCard = null!;
    private Guna.UI2.WinForms.Guna2Panel _activeUsersCard = null!;
    private Guna.UI2.WinForms.Guna2Panel _inactiveUsersCard = null!;
    private Guna.UI2.WinForms.Guna2Panel _activeUsersBar = null!;
    private Guna.UI2.WinForms.Guna2Panel _inactiveUsersBar = null!;
    private Label _activeUsersTitleLabel = null!;
    private Label _activeUsersCountLabel = null!;
    private Label _activeUsersDescLabel = null!;
    private Label _inactiveUsersTitleLabel = null!;
    private Label _inactiveUsersCountLabel = null!;
    private Label _inactiveUsersDescLabel = null!;
    private Guna.UI2.WinForms.Guna2Button _newButton = null!;
    private Guna.UI2.WinForms.Guna2Button _refreshButton = null!;
    private Guna.UI2.WinForms.Guna2Button _deleteButton = null!;
    private Guna.UI2.WinForms.Guna2Button _activationButton = null!;
    private Guna.UI2.WinForms.Guna2Panel _tableCard = null!;
    private Guna.UI2.WinForms.Guna2DataGridView _grid = null!;
    private Label _countLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _toolbarCard = new Guna.UI2.WinForms.Guna2Panel();
        _activeUsersCard = new Guna.UI2.WinForms.Guna2Panel();
        _activeUsersDescLabel = new Label();
        _activeUsersCountLabel = new Label();
        _activeUsersTitleLabel = new Label();
        _inactiveUsersCard = new Guna.UI2.WinForms.Guna2Panel();
        _inactiveUsersDescLabel = new Label();
        _inactiveUsersCountLabel = new Label();
        _inactiveUsersTitleLabel = new Label();
        _activeUsersBar = new Guna.UI2.WinForms.Guna2Panel();
        _inactiveUsersBar = new Guna.UI2.WinForms.Guna2Panel();
        _newButton = new Guna.UI2.WinForms.Guna2Button();
        _refreshButton = new Guna.UI2.WinForms.Guna2Button();
        _activationButton = new Guna.UI2.WinForms.Guna2Button();
        _deleteButton = new Guna.UI2.WinForms.Guna2Button();
        _tableCard = new Guna.UI2.WinForms.Guna2Panel();
        _grid = new Guna.UI2.WinForms.Guna2DataGridView();
        _countLabel = new Label();
        _toolbarCard.SuspendLayout();
        _activeUsersCard.SuspendLayout();
        _inactiveUsersCard.SuspendLayout();
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
        _toolbarCard.Controls.Add(_activeUsersCard);
        _toolbarCard.Controls.Add(_inactiveUsersCard);
        _toolbarCard.Controls.Add(_activeUsersBar);
        _toolbarCard.Controls.Add(_inactiveUsersBar);
        _toolbarCard.Controls.Add(_newButton);
        _toolbarCard.Controls.Add(_refreshButton);
        _toolbarCard.Controls.Add(_activationButton);
        _toolbarCard.Controls.Add(_deleteButton);
        _toolbarCard.CustomizableEdges = customizableEdges17;
        _toolbarCard.Dock = DockStyle.Top;
        _toolbarCard.FillColor = Color.LightGray;
        _toolbarCard.Location = new Point(0, 0);
        _toolbarCard.Name = "_toolbarCard";
        _toolbarCard.ShadowDecoration.CustomizableEdges = customizableEdges18;
        _toolbarCard.Size = new Size(715, 205);
        _toolbarCard.TabIndex = 1;
        // 
        // _activeUsersCard
        // 
        _activeUsersCard.BackColor = Color.Transparent;
        _activeUsersCard.BorderRadius = 10;
        _activeUsersCard.Controls.Add(_activeUsersDescLabel);
        _activeUsersCard.Controls.Add(_activeUsersCountLabel);
        _activeUsersCard.Controls.Add(_activeUsersTitleLabel);
        _activeUsersCard.CustomizableEdges = customizableEdges1;
        _activeUsersCard.FillColor = Color.White;
        _activeUsersCard.Location = new Point(331, 81);
        _activeUsersCard.Name = "_activeUsersCard";
        _activeUsersCard.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _activeUsersCard.Size = new Size(180, 118);
        _activeUsersCard.TabIndex = 2;
        // 
        // _activeUsersDescLabel
        // 
        _activeUsersDescLabel.AutoSize = true;
        _activeUsersDescLabel.BackColor = Color.Transparent;
        _activeUsersDescLabel.Font = new Font("Century Gothic", 8.25F);
        _activeUsersDescLabel.ForeColor = Color.Black;
        _activeUsersDescLabel.Location = new Point(12, 83);
        _activeUsersDescLabel.Name = "_activeUsersDescLabel";
        _activeUsersDescLabel.Size = new Size(149, 16);
        _activeUsersDescLabel.TabIndex = 0;
        _activeUsersDescLabel.Text = "Usuários com acesso ativo";
        // 
        // _activeUsersCountLabel
        // 
        _activeUsersCountLabel.AutoSize = true;
        _activeUsersCountLabel.BackColor = Color.Transparent;
        _activeUsersCountLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        _activeUsersCountLabel.Location = new Point(12, 38);
        _activeUsersCountLabel.Name = "_activeUsersCountLabel";
        _activeUsersCountLabel.Size = new Size(38, 45);
        _activeUsersCountLabel.TabIndex = 1;
        _activeUsersCountLabel.Text = "0";
        // 
        // _activeUsersTitleLabel
        // 
        _activeUsersTitleLabel.AutoSize = true;
        _activeUsersTitleLabel.BackColor = Color.Transparent;
        _activeUsersTitleLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
        _activeUsersTitleLabel.ForeColor = Color.FromArgb(34, 139, 34);
        _activeUsersTitleLabel.Location = new Point(12, 19);
        _activeUsersTitleLabel.Name = "_activeUsersTitleLabel";
        _activeUsersTitleLabel.Size = new Size(144, 19);
        _activeUsersTitleLabel.TabIndex = 2;
        _activeUsersTitleLabel.Text = "\U0001f7e2 Usuários Ativos";
        // 
        // _inactiveUsersCard
        // 
        _inactiveUsersCard.BackColor = Color.Transparent;
        _inactiveUsersCard.BorderRadius = 10;
        _inactiveUsersCard.Controls.Add(_inactiveUsersDescLabel);
        _inactiveUsersCard.Controls.Add(_inactiveUsersCountLabel);
        _inactiveUsersCard.Controls.Add(_inactiveUsersTitleLabel);
        _inactiveUsersCard.CustomizableEdges = customizableEdges3;
        _inactiveUsersCard.FillColor = Color.White;
        _inactiveUsersCard.Location = new Point(521, 81);
        _inactiveUsersCard.Name = "_inactiveUsersCard";
        _inactiveUsersCard.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _inactiveUsersCard.Size = new Size(180, 118);
        _inactiveUsersCard.TabIndex = 4;
        // 
        // _inactiveUsersDescLabel
        // 
        _inactiveUsersDescLabel.AutoSize = true;
        _inactiveUsersDescLabel.BackColor = Color.Transparent;
        _inactiveUsersDescLabel.Font = new Font("Century Gothic", 8.25F);
        _inactiveUsersDescLabel.ForeColor = Color.Black;
        _inactiveUsersDescLabel.Location = new Point(12, 83);
        _inactiveUsersDescLabel.Name = "_inactiveUsersDescLabel";
        _inactiveUsersDescLabel.Size = new Size(114, 16);
        _inactiveUsersDescLabel.TabIndex = 0;
        _inactiveUsersDescLabel.Text = "Usuários sem acesso";
        // 
        // _inactiveUsersCountLabel
        // 
        _inactiveUsersCountLabel.AutoSize = true;
        _inactiveUsersCountLabel.BackColor = Color.Transparent;
        _inactiveUsersCountLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        _inactiveUsersCountLabel.Location = new Point(12, 38);
        _inactiveUsersCountLabel.Name = "_inactiveUsersCountLabel";
        _inactiveUsersCountLabel.Size = new Size(38, 45);
        _inactiveUsersCountLabel.TabIndex = 1;
        _inactiveUsersCountLabel.Text = "0";
        // 
        // _inactiveUsersTitleLabel
        // 
        _inactiveUsersTitleLabel.AutoSize = true;
        _inactiveUsersTitleLabel.BackColor = Color.Transparent;
        _inactiveUsersTitleLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
        _inactiveUsersTitleLabel.ForeColor = Color.FromArgb(192, 0, 0);
        _inactiveUsersTitleLabel.Location = new Point(12, 19);
        _inactiveUsersTitleLabel.Name = "_inactiveUsersTitleLabel";
        _inactiveUsersTitleLabel.Size = new Size(157, 19);
        _inactiveUsersTitleLabel.TabIndex = 2;
        _inactiveUsersTitleLabel.Text = "🔴 Usuários Inativos";
        // 
        // _activeUsersBar
        // 
        _activeUsersBar.CustomizableEdges = customizableEdges5;
        _activeUsersBar.FillColor = Color.FromArgb(34, 139, 34);
        _activeUsersBar.Location = new Point(330, 71);
        _activeUsersBar.Name = "_activeUsersBar";
        _activeUsersBar.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _activeUsersBar.Size = new Size(180, 10);
        _activeUsersBar.TabIndex = 3;
        // 
        // _inactiveUsersBar
        // 
        _inactiveUsersBar.CustomizableEdges = customizableEdges7;
        _inactiveUsersBar.FillColor = Color.FromArgb(192, 0, 0);
        _inactiveUsersBar.Location = new Point(520, 71);
        _inactiveUsersBar.Name = "_inactiveUsersBar";
        _inactiveUsersBar.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _inactiveUsersBar.Size = new Size(180, 10);
        _inactiveUsersBar.TabIndex = 5;
        // 
        // _newButton
        // 
        _newButton.CustomizableEdges = customizableEdges9;
        _newButton.Font = new Font("Segoe UI", 9F);
        _newButton.ForeColor = Color.White;
        _newButton.Location = new Point(0, 0);
        _newButton.Name = "_newButton";
        _newButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _newButton.Size = new Size(180, 45);
        _newButton.TabIndex = 6;
        // 
        // _refreshButton
        // 
        _refreshButton.CustomizableEdges = customizableEdges11;
        _refreshButton.Font = new Font("Segoe UI", 9F);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(0, 0);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _refreshButton.Size = new Size(180, 45);
        _refreshButton.TabIndex = 7;
        // 
        // _activationButton
        // 
        _activationButton.CustomizableEdges = customizableEdges13;
        _activationButton.Font = new Font("Segoe UI", 9F);
        _activationButton.ForeColor = Color.White;
        _activationButton.Location = new Point(0, 0);
        _activationButton.Name = "_activationButton";
        _activationButton.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _activationButton.Size = new Size(180, 45);
        _activationButton.TabIndex = 8;
        // 
        // _deleteButton
        // 
        _deleteButton.CustomizableEdges = customizableEdges15;
        _deleteButton.Font = new Font("Segoe UI", 9F);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.Location = new Point(0, 0);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.ShadowDecoration.CustomizableEdges = customizableEdges16;
        _deleteButton.Size = new Size(180, 45);
        _deleteButton.TabIndex = 9;
        // 
        // _tableCard
        // 
        _tableCard.BackColor = Color.Transparent;
        _tableCard.BorderColor = Color.White;
        _tableCard.BorderRadius = 15;
        _tableCard.BorderThickness = 1;
        _tableCard.Controls.Add(_grid);
        _tableCard.Controls.Add(_countLabel);
        _tableCard.CustomizableEdges = customizableEdges19;
        _tableCard.Dock = DockStyle.Fill;
        _tableCard.FillColor = Color.White;
        _tableCard.Location = new Point(0, 205);
        _tableCard.Name = "_tableCard";
        _tableCard.Padding = new Padding(14, 14, 14, 40);
        _tableCard.ShadowDecoration.CustomizableEdges = customizableEdges20;
        _tableCard.Size = new Size(715, 231);
        _tableCard.TabIndex = 0;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.BorderStyle = BorderStyle.FixedSingle;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(0, 77, 147);
        dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F);
        dataGridViewCellStyle1.ForeColor = Color.White;
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(0, 77, 147);
        dataGridViewCellStyle1.SelectionForeColor = Color.White;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.ColumnHeadersHeight = 42;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.White;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(45, 45, 45);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(225, 225, 225);
        dataGridViewCellStyle2.SelectionForeColor = Color.Black;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.FromArgb(220, 220, 220);
        _grid.Location = new Point(14, 14);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 42;
        _grid.Size = new Size(687, 149);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.GridColor = Color.FromArgb(220, 220, 220);
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(0, 77, 147);
        _grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 9F);
        _grid.ThemeStyle.HeaderStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        _grid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(45, 45, 45);
        _grid.ThemeStyle.RowsStyle.Height = 42;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(225, 225, 225);
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
        // 
        // _countLabel
        // 
        _countLabel.Dock = DockStyle.Bottom;
        _countLabel.Font = new Font("Segoe UI", 8.5F);
        _countLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _countLabel.Location = new Point(14, 163);
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
        _activeUsersCard.ResumeLayout(false);
        _activeUsersCard.PerformLayout();
        _inactiveUsersCard.ResumeLayout(false);
        _inactiveUsersCard.PerformLayout();
        _tableCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        ResumeLayout(false);
    }

    private static void ConfigureButton(Guna.UI2.WinForms.Guna2Button button, string text, int x, int width, Color fill, Color foreColor, bool bold)
    {
        button.BackColor = Color.Transparent;
        button.BorderRadius = 7;
        button.FillColor = fill;
        button.Font = new Font("Segoe UI", 9F, bold ? FontStyle.Bold : FontStyle.Regular);
        button.ForeColor = foreColor;
        button.Location = new Point(x, 144);
        button.Name = text;
        button.Size = new Size(width, 42);
        button.TabIndex = 0;
        button.Text = text;
    }
}
