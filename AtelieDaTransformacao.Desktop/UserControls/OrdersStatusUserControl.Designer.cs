using Guna.UI2.WinForms;

namespace AtelieDaTransformacao.Desktop.UserControls;

partial class OrdersStatusUserControl
{
    private System.ComponentModel.IContainer? components = null;

    private Guna2Panel _rootPanel = null!;
    private Panel _headingPanel = null!;
    private Label _titleLabel = null!;
    private Label _subtitleLabel = null!;
    private Guna2Button _refreshButton = null!;

    private Panel _stepsHost = null!;
    private FlowLayoutPanel _stepsFlowPanel = null!;

    private Panel _stepCreatedPanel = null!;
    private Panel _stepPendingPanel = null!;
    private Panel _stepApprovedPanel = null!;
    private Panel _stepSeparationPanel = null!;
    private Panel _stepInvoicedPanel = null!;
    private Panel _stepShippedPanel = null!;
    private Panel _stepDeliveredPanel = null!;

    private Label _stepCreatedIcon = null!;
    private Label _stepPendingIcon = null!;
    private Label _stepApprovedIcon = null!;
    private Label _stepSeparationIcon = null!;
    private Label _stepInvoicedIcon = null!;
    private Label _stepShippedIcon = null!;
    private Label _stepDeliveredIcon = null!;

    private Label _stepCreatedName = null!;
    private Label _stepPendingName = null!;
    private Label _stepApprovedName = null!;
    private Label _stepSeparationName = null!;
    private Label _stepInvoicedName = null!;
    private Label _stepShippedName = null!;
    private Label _stepDeliveredName = null!;

    private Label _stepCreatedDescription = null!;
    private Label _stepPendingDescription = null!;
    private Label _stepApprovedDescription = null!;
    private Label _stepSeparationDescription = null!;
    private Label _stepInvoicedDescription = null!;
    private Label _stepShippedDescription = null!;
    private Label _stepDeliveredDescription = null!;

    private Label _connector1 = null!;
    private Label _connector2 = null!;
    private Label _connector3 = null!;
    private Label _connector4 = null!;
    private Label _connector5 = null!;
    private Label _connector6 = null!;

    private Guna2Panel _filterCard = null!;
    private FlowLayoutPanel _filterFlowPanel = null!;

    private Panel _startFilterPanel = null!;
    private Panel _endFilterPanel = null!;
    private Panel _statusFilterPanel = null!;
    private Panel _searchFilterPanel = null!;

    private Label _startCaption = null!;
    private Label _endCaption = null!;
    private Label _statusCaption = null!;
    private Label _searchCaption = null!;

    private Guna2DateTimePicker _startDatePicker = null!;
    private Guna2DateTimePicker _endDatePicker = null!;
    private Guna2ComboBox _statusComboBox = null!;
    private Guna2TextBox _searchTextBox = null!;

    private Guna2Button _clearButton = null!;
    private Guna2Button _exportButton = null!;

    private Guna2Panel _gridCard = null!;
    private Guna2DataGridView _grid = null!;

    private Panel _bottomPanel = null!;
    private Label _countLabel = null!;
    private Label _pageLabel = null!;
    private FlowLayoutPanel _paginationPanel = null!;

    private Guna2Button _previousPageButton = null!;
    private Guna2Button _page1Button = null!;
    private Guna2Button _page2Button = null!;
    private Guna2Button _page3Button = null!;
    private Guna2Button _nextPageButton = null!;

    private Panel panel1 = null!;
    private Panel panel2 = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _rootPanel = new Guna2Panel();
        _gridCard = new Guna2Panel();
        _grid = new Guna2DataGridView();
        _numberColumn = new DataGridViewTextBoxColumn();
        _dateColumn = new DataGridViewTextBoxColumn();
        _customerColumn = new DataGridViewTextBoxColumn();
        _totalColumn = new DataGridViewTextBoxColumn();
        _statusColumn = new DataGridViewComboBoxColumn();
        _actionsColumn = new DataGridViewButtonColumn();
        _bottomPanel = new Panel();
        _paginationPanel = new FlowLayoutPanel();
        _previousPageButton = new Guna2Button();
        _page1Button = new Guna2Button();
        _page2Button = new Guna2Button();
        _page3Button = new Guna2Button();
        _nextPageButton = new Guna2Button();
        _pageLabel = new Label();
        _countLabel = new Label();
        _filterCard = new Guna2Panel();
        _filterFlowPanel = new FlowLayoutPanel();
        _startFilterPanel = new Panel();
        _startDatePicker = new Guna2DateTimePicker();
        _startCaption = new Label();
        _endFilterPanel = new Panel();
        _endDatePicker = new Guna2DateTimePicker();
        _endCaption = new Label();
        _statusFilterPanel = new Panel();
        _statusComboBox = new Guna2ComboBox();
        _statusCaption = new Label();
        _searchFilterPanel = new Panel();
        _searchCaption = new Label();
        _searchTextBox = new Guna2TextBox();
        panel1 = new Panel();
        _exportButton = new Guna2Button();
        panel2 = new Panel();
        _clearButton = new Guna2Button();
        _stepsHost = new Panel();
        _stepsFlowPanel = new FlowLayoutPanel();
        _stepCreatedPanel = new Panel();
        _stepCreatedDescription = new Label();
        _stepCreatedName = new Label();
        _stepCreatedIcon = new Label();
        _connector1 = new Label();
        _stepPendingPanel = new Panel();
        _stepPendingDescription = new Label();
        _stepPendingName = new Label();
        _stepPendingIcon = new Label();
        _connector2 = new Label();
        _stepApprovedPanel = new Panel();
        _stepApprovedDescription = new Label();
        _stepApprovedName = new Label();
        _stepApprovedIcon = new Label();
        _connector3 = new Label();
        _stepSeparationPanel = new Panel();
        _stepSeparationDescription = new Label();
        _stepSeparationName = new Label();
        _stepSeparationIcon = new Label();
        _connector4 = new Label();
        _stepInvoicedPanel = new Panel();
        _stepInvoicedDescription = new Label();
        _stepInvoicedName = new Label();
        _stepInvoicedIcon = new Label();
        _connector5 = new Label();
        _stepShippedPanel = new Panel();
        _stepShippedDescription = new Label();
        _stepShippedName = new Label();
        _stepShippedIcon = new Label();
        _connector6 = new Label();
        _stepDeliveredPanel = new Panel();
        _stepDeliveredDescription = new Label();
        _stepDeliveredName = new Label();
        _stepDeliveredIcon = new Label();
        _headingPanel = new Panel();
        _refreshButton = new Guna2Button();
        _subtitleLabel = new Label();
        _titleLabel = new Label();
        _lastUpdateColumn = new DataGridViewTextBoxColumn();
        _rootPanel.SuspendLayout();
        _gridCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _bottomPanel.SuspendLayout();
        _paginationPanel.SuspendLayout();
        _filterCard.SuspendLayout();
        _filterFlowPanel.SuspendLayout();
        _startFilterPanel.SuspendLayout();
        _endFilterPanel.SuspendLayout();
        _statusFilterPanel.SuspendLayout();
        _searchFilterPanel.SuspendLayout();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        _stepsHost.SuspendLayout();
        _stepsFlowPanel.SuspendLayout();
        _stepCreatedPanel.SuspendLayout();
        _stepPendingPanel.SuspendLayout();
        _stepApprovedPanel.SuspendLayout();
        _stepSeparationPanel.SuspendLayout();
        _stepInvoicedPanel.SuspendLayout();
        _stepShippedPanel.SuspendLayout();
        _stepDeliveredPanel.SuspendLayout();
        _headingPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _rootPanel
        // 
        _rootPanel.BackColor = Color.FromArgb(43, 26, 18);
        _rootPanel.Controls.Add(_gridCard);
        _rootPanel.Controls.Add(_filterCard);
        _rootPanel.Controls.Add(_stepsHost);
        _rootPanel.Controls.Add(_headingPanel);
        _rootPanel.CustomizableEdges = customizableEdges29;
        _rootPanel.Dock = DockStyle.Fill;
        _rootPanel.FillColor = Color.FromArgb(43, 26, 18);
        _rootPanel.Location = new Point(0, 0);
        _rootPanel.Name = "_rootPanel";
        _rootPanel.Padding = new Padding(18, 14, 18, 14);
        _rootPanel.ShadowDecoration.CustomizableEdges = customizableEdges30;
        _rootPanel.Size = new Size(1465, 438);
        _rootPanel.TabIndex = 0;
        // 
        // _gridCard
        // 
        _gridCard.BorderColor = Color.FromArgb(120, 79, 43);
        _gridCard.BorderRadius = 10;
        _gridCard.BorderThickness = 1;
        _gridCard.Controls.Add(_grid);
        _gridCard.Controls.Add(_bottomPanel);
        _gridCard.CustomizableEdges = customizableEdges11;
        _gridCard.Dock = DockStyle.Fill;
        _gridCard.Location = new Point(18, 269);
        _gridCard.Name = "_gridCard";
        _gridCard.Padding = new Padding(10, 10, 10, 8);
        _gridCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _gridCard.Size = new Size(1429, 155);
        _gridCard.TabIndex = 0;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeColumns = false;
        _grid.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.White;
        _grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(74, 46, 29);
        dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 8.5F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.Padding = new Padding(6, 0, 6, 0);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(74, 46, 29);
        dataGridViewCellStyle2.SelectionForeColor = Color.White;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _grid.ColumnHeadersHeight = 38;
        _grid.Columns.AddRange(new DataGridViewColumn[] { _numberColumn, _dateColumn, _customerColumn, _totalColumn, _statusColumn, _actionsColumn });
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.White;
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 8.5F);
        dataGridViewCellStyle4.ForeColor = Color.Black;
        dataGridViewCellStyle4.Padding = new Padding(6, 0, 6, 0);
        dataGridViewCellStyle4.SelectionBackColor = Color.LightGray;
        dataGridViewCellStyle4.SelectionForeColor = Color.Black;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle4;
        _grid.Dock = DockStyle.Fill;
        _grid.GridColor = Color.FromArgb(100, 65, 39);
        _grid.Location = new Point(10, 10);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowTemplate.Height = 36;
        _grid.ScrollBars = ScrollBars.Vertical;
        _grid.Size = new Size(1409, 93);
        _grid.TabIndex = 0;
        _grid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
        _grid.ThemeStyle.GridColor = Color.FromArgb(100, 65, 39);
        _grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(74, 46, 29);
        _grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 8.5F);
        _grid.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.ThemeStyle.HeaderStyle.Height = 38;
        _grid.ThemeStyle.ReadOnly = true;
        _grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 8.5F);
        _grid.ThemeStyle.RowsStyle.ForeColor = Color.Black;
        _grid.ThemeStyle.RowsStyle.Height = 36;
        _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.LightGray;
        _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
        // 
        // _numberColumn
        // 
        _numberColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        _numberColumn.FillWeight = 10F;
        _numberColumn.Frozen = true;
        _numberColumn.HeaderText = "Nº Pedido";
        _numberColumn.MinimumWidth = 70;
        _numberColumn.Name = "_numberColumn";
        _numberColumn.ReadOnly = true;
        _numberColumn.Resizable = DataGridViewTriState.True;
        _numberColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        _numberColumn.Width = 152;
        // 
        // _dateColumn
        // 
        _dateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        _dateColumn.FillWeight = 13F;
        _dateColumn.Frozen = true;
        _dateColumn.HeaderText = "Data";
        _dateColumn.MinimumWidth = 90;
        _dateColumn.Name = "_dateColumn";
        _dateColumn.ReadOnly = true;
        _dateColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        _dateColumn.Width = 196;
        // 
        // _customerColumn
        // 
        _customerColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        _customerColumn.FillWeight = 20F;
        _customerColumn.Frozen = true;
        _customerColumn.HeaderText = "Cliente";
        _customerColumn.MinimumWidth = 105;
        _customerColumn.Name = "_customerColumn";
        _customerColumn.ReadOnly = true;
        _customerColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        _customerColumn.Width = 303;
        // 
        // _totalColumn
        // 
        _totalColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        _totalColumn.FillWeight = 12F;
        _totalColumn.Frozen = true;
        _totalColumn.HeaderText = "Valor Total";
        _totalColumn.MinimumWidth = 82;
        _totalColumn.Name = "_totalColumn";
        _totalColumn.ReadOnly = true;
        _totalColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        _totalColumn.Width = 182;
        // 
        // _statusColumn
        // 
        _statusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        _statusColumn.DisplayStyleForCurrentCellOnly = true;
        _statusColumn.FillWeight = 12F;
        _statusColumn.FlatStyle = FlatStyle.Flat;
        _statusColumn.Frozen = true;
        _statusColumn.HeaderText = "Status";
        _statusColumn.Items.AddRange(new object[] { "Criado", "Pendente", "Aprovado", "Separação", "Faturado", "Enviado", "Entregue" });
        _statusColumn.MinimumWidth = 96;
        _statusColumn.Name = "_statusColumn";
        _statusColumn.ReadOnly = true;
        _statusColumn.Width = 182;
        // 
        // _actionsColumn
        // 
        _actionsColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.BackColor = Color.White;
        dataGridViewCellStyle3.ForeColor = Color.Black;
        dataGridViewCellStyle3.Padding = new Padding(8, 4, 8, 4);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(103, 70, 46);
        dataGridViewCellStyle3.SelectionForeColor = Color.White;
        _actionsColumn.DefaultCellStyle = dataGridViewCellStyle3;
        _actionsColumn.FillWeight = 10F;
        _actionsColumn.FlatStyle = FlatStyle.Flat;
        _actionsColumn.Frozen = true;
        _actionsColumn.HeaderText = "Ações";
        _actionsColumn.MinimumWidth = 74;
        _actionsColumn.Name = "_actionsColumn";
        _actionsColumn.ReadOnly = true;
        _actionsColumn.Resizable = DataGridViewTriState.False;
        _actionsColumn.Text = "Detalhes";
        _actionsColumn.UseColumnTextForButtonValue = true;
        _actionsColumn.Width = 120;
        // 
        // _bottomPanel
        // 
        _bottomPanel.BackColor = Color.Transparent;
        _bottomPanel.Controls.Add(_paginationPanel);
        _bottomPanel.Controls.Add(_pageLabel);
        _bottomPanel.Controls.Add(_countLabel);
        _bottomPanel.Dock = DockStyle.Bottom;
        _bottomPanel.Location = new Point(10, 103);
        _bottomPanel.Name = "_bottomPanel";
        _bottomPanel.Size = new Size(1409, 44);
        _bottomPanel.TabIndex = 1;
        // 
        // _paginationPanel
        // 
        _paginationPanel.AutoSize = true;
        _paginationPanel.BackColor = Color.Transparent;
        _paginationPanel.Controls.Add(_previousPageButton);
        _paginationPanel.Controls.Add(_page1Button);
        _paginationPanel.Controls.Add(_page2Button);
        _paginationPanel.Controls.Add(_page3Button);
        _paginationPanel.Controls.Add(_nextPageButton);
        _paginationPanel.Dock = DockStyle.Right;
        _paginationPanel.Location = new Point(1239, 0);
        _paginationPanel.Margin = new Padding(0);
        _paginationPanel.Name = "_paginationPanel";
        _paginationPanel.Padding = new Padding(0, 3, 0, 0);
        _paginationPanel.Size = new Size(170, 44);
        _paginationPanel.TabIndex = 0;
        _paginationPanel.WrapContents = false;
        // 
        // _previousPageButton
        // 
        _previousPageButton.BorderRadius = 6;
        _previousPageButton.Cursor = Cursors.Hand;
        _previousPageButton.CustomizableEdges = customizableEdges1;
        _previousPageButton.FillColor = Color.FromArgb(82, 55, 38);
        _previousPageButton.Font = new Font("Segoe UI Semibold", 8F);
        _previousPageButton.ForeColor = Color.White;
        _previousPageButton.Location = new Point(2, 3);
        _previousPageButton.Margin = new Padding(2, 0, 2, 0);
        _previousPageButton.Name = "_previousPageButton";
        _previousPageButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _previousPageButton.Size = new Size(30, 28);
        _previousPageButton.TabIndex = 0;
        _previousPageButton.Text = "‹";
        // 
        // _page1Button
        // 
        _page1Button.BorderRadius = 6;
        _page1Button.Cursor = Cursors.Hand;
        _page1Button.CustomizableEdges = customizableEdges3;
        _page1Button.FillColor = Color.Gray;
        _page1Button.Font = new Font("Segoe UI Semibold", 8.5F);
        _page1Button.ForeColor = Color.White;
        _page1Button.Location = new Point(36, 3);
        _page1Button.Margin = new Padding(2, 0, 2, 0);
        _page1Button.Name = "_page1Button";
        _page1Button.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _page1Button.Size = new Size(30, 28);
        _page1Button.TabIndex = 1;
        _page1Button.Text = "1";
        // 
        // _page2Button
        // 
        _page2Button.BorderRadius = 6;
        _page2Button.Cursor = Cursors.Hand;
        _page2Button.CustomizableEdges = customizableEdges5;
        _page2Button.FillColor = Color.FromArgb(82, 55, 38);
        _page2Button.Font = new Font("Segoe UI Semibold", 8.5F);
        _page2Button.ForeColor = Color.White;
        _page2Button.Location = new Point(70, 3);
        _page2Button.Margin = new Padding(2, 0, 2, 0);
        _page2Button.Name = "_page2Button";
        _page2Button.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _page2Button.Size = new Size(30, 28);
        _page2Button.TabIndex = 2;
        _page2Button.Text = "2";
        // 
        // _page3Button
        // 
        _page3Button.BorderRadius = 6;
        _page3Button.Cursor = Cursors.Hand;
        _page3Button.CustomizableEdges = customizableEdges7;
        _page3Button.FillColor = Color.FromArgb(82, 55, 38);
        _page3Button.Font = new Font("Segoe UI Semibold", 8.5F);
        _page3Button.ForeColor = Color.White;
        _page3Button.Location = new Point(104, 3);
        _page3Button.Margin = new Padding(2, 0, 2, 0);
        _page3Button.Name = "_page3Button";
        _page3Button.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _page3Button.Size = new Size(30, 28);
        _page3Button.TabIndex = 3;
        _page3Button.Text = "3";
        // 
        // _nextPageButton
        // 
        _nextPageButton.BorderRadius = 6;
        _nextPageButton.Cursor = Cursors.Hand;
        _nextPageButton.CustomizableEdges = customizableEdges9;
        _nextPageButton.FillColor = Color.FromArgb(82, 55, 38);
        _nextPageButton.Font = new Font("Segoe UI Semibold", 8.5F);
        _nextPageButton.ForeColor = Color.White;
        _nextPageButton.Location = new Point(138, 3);
        _nextPageButton.Margin = new Padding(2, 0, 2, 0);
        _nextPageButton.Name = "_nextPageButton";
        _nextPageButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _nextPageButton.Size = new Size(30, 28);
        _nextPageButton.TabIndex = 4;
        _nextPageButton.Text = "›";
        // 
        // _pageLabel
        // 
        _pageLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _pageLabel.AutoSize = true;
        _pageLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _pageLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _pageLabel.Location = new Point(1819, 13);
        _pageLabel.Name = "_pageLabel";
        _pageLabel.Size = new Size(77, 13);
        _pageLabel.TabIndex = 1;
        _pageLabel.Text = "Página 1 de 1";
        // 
        // _countLabel
        // 
        _countLabel.AutoSize = true;
        _countLabel.Font = new Font("Segoe UI", 8F);
        _countLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _countLabel.Location = new Point(2, 13);
        _countLabel.Name = "_countLabel";
        _countLabel.Size = new Size(152, 13);
        _countLabel.TabIndex = 2;
        _countLabel.Text = "Exibindo 0 a 0 de 0 registros";
        // 
        // _filterCard
        // 
        _filterCard.BorderColor = Color.FromArgb(120, 79, 43);
        _filterCard.BorderRadius = 10;
        _filterCard.BorderThickness = 1;
        _filterCard.Controls.Add(_filterFlowPanel);
        _filterCard.CustomizableEdges = customizableEdges25;
        _filterCard.Dock = DockStyle.Top;
        _filterCard.Location = new Point(18, 189);
        _filterCard.Name = "_filterCard";
        _filterCard.Padding = new Padding(10, 7, 10, 7);
        _filterCard.ShadowDecoration.CustomizableEdges = customizableEdges26;
        _filterCard.Size = new Size(1429, 80);
        _filterCard.TabIndex = 1;
        // 
        // _filterFlowPanel
        // 
        _filterFlowPanel.BackColor = Color.Transparent;
        _filterFlowPanel.Controls.Add(_startFilterPanel);
        _filterFlowPanel.Controls.Add(_endFilterPanel);
        _filterFlowPanel.Controls.Add(_statusFilterPanel);
        _filterFlowPanel.Controls.Add(_searchFilterPanel);
        _filterFlowPanel.Controls.Add(panel1);
        _filterFlowPanel.Controls.Add(panel2);
        _filterFlowPanel.Dock = DockStyle.Fill;
        _filterFlowPanel.Location = new Point(10, 7);
        _filterFlowPanel.Margin = new Padding(0);
        _filterFlowPanel.Name = "_filterFlowPanel";
        _filterFlowPanel.Size = new Size(1409, 66);
        _filterFlowPanel.TabIndex = 0;
        _filterFlowPanel.WrapContents = false;
        // 
        // _startFilterPanel
        // 
        _startFilterPanel.BackColor = Color.Transparent;
        _startFilterPanel.Controls.Add(_startDatePicker);
        _startFilterPanel.Controls.Add(_startCaption);
        _startFilterPanel.Location = new Point(0, 0);
        _startFilterPanel.Margin = new Padding(0, 0, 4, 0);
        _startFilterPanel.Name = "_startFilterPanel";
        _startFilterPanel.Size = new Size(119, 62);
        _startFilterPanel.TabIndex = 0;
        // 
        // _startDatePicker
        // 
        _startDatePicker.BorderColor = Color.FromArgb(120, 79, 43);
        _startDatePicker.BorderRadius = 8;
        _startDatePicker.Checked = true;
        _startDatePicker.CustomizableEdges = customizableEdges13;
        _startDatePicker.FillColor = Color.FromArgb(55, 33, 23);
        _startDatePicker.Font = new Font("Segoe UI", 8F);
        _startDatePicker.ForeColor = Color.White;
        _startDatePicker.Format = DateTimePickerFormat.Short;
        _startDatePicker.Location = new Point(0, 18);
        _startDatePicker.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
        _startDatePicker.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
        _startDatePicker.Name = "_startDatePicker";
        _startDatePicker.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _startDatePicker.Size = new Size(116, 34);
        _startDatePicker.TabIndex = 0;
        _startDatePicker.Value = new DateTime(2026, 8, 24, 22, 18, 43, 886);
        // 
        // _startCaption
        // 
        _startCaption.AutoSize = true;
        _startCaption.Font = new Font("Segoe UI Semibold", 7.5F);
        _startCaption.ForeColor = Color.FromArgb(210, 195, 182);
        _startCaption.Location = new Point(4, 0);
        _startCaption.Name = "_startCaption";
        _startCaption.Size = new Size(41, 12);
        _startCaption.TabIndex = 1;
        _startCaption.Text = "Período";
        // 
        // _endFilterPanel
        // 
        _endFilterPanel.BackColor = Color.Transparent;
        _endFilterPanel.Controls.Add(_endDatePicker);
        _endFilterPanel.Controls.Add(_endCaption);
        _endFilterPanel.Location = new Point(123, 0);
        _endFilterPanel.Margin = new Padding(0, 0, 4, 0);
        _endFilterPanel.Name = "_endFilterPanel";
        _endFilterPanel.Size = new Size(117, 62);
        _endFilterPanel.TabIndex = 1;
        // 
        // _endDatePicker
        // 
        _endDatePicker.BorderColor = Color.FromArgb(120, 79, 43);
        _endDatePicker.BorderRadius = 8;
        _endDatePicker.Checked = true;
        _endDatePicker.CustomizableEdges = customizableEdges15;
        _endDatePicker.FillColor = Color.FromArgb(55, 33, 23);
        _endDatePicker.Font = new Font("Segoe UI", 8F);
        _endDatePicker.ForeColor = Color.White;
        _endDatePicker.Format = DateTimePickerFormat.Short;
        _endDatePicker.Location = new Point(0, 18);
        _endDatePicker.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
        _endDatePicker.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
        _endDatePicker.Name = "_endDatePicker";
        _endDatePicker.ShadowDecoration.CustomizableEdges = customizableEdges16;
        _endDatePicker.Size = new Size(114, 36);
        _endDatePicker.TabIndex = 0;
        _endDatePicker.Value = new DateTime(2026, 8, 24, 22, 18, 43, 922);
        // 
        // _endCaption
        // 
        _endCaption.AutoSize = true;
        _endCaption.Font = new Font("Segoe UI Semibold", 7.5F);
        _endCaption.ForeColor = Color.FromArgb(210, 195, 182);
        _endCaption.Location = new Point(4, 0);
        _endCaption.Name = "_endCaption";
        _endCaption.Size = new Size(21, 12);
        _endCaption.TabIndex = 1;
        _endCaption.Text = "Até";
        // 
        // _statusFilterPanel
        // 
        _statusFilterPanel.BackColor = Color.Transparent;
        _statusFilterPanel.Controls.Add(_statusComboBox);
        _statusFilterPanel.Controls.Add(_statusCaption);
        _statusFilterPanel.Location = new Point(244, 0);
        _statusFilterPanel.Margin = new Padding(0, 0, 4, 0);
        _statusFilterPanel.Name = "_statusFilterPanel";
        _statusFilterPanel.Size = new Size(119, 62);
        _statusFilterPanel.TabIndex = 2;
        // 
        // _statusComboBox
        // 
        _statusComboBox.BackColor = Color.Transparent;
        _statusComboBox.BorderColor = Color.FromArgb(120, 79, 43);
        _statusComboBox.BorderRadius = 8;
        _statusComboBox.CustomizableEdges = customizableEdges17;
        _statusComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        _statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _statusComboBox.FillColor = Color.FromArgb(55, 33, 23);
        _statusComboBox.FocusedColor = Color.Empty;
        _statusComboBox.Font = new Font("Segoe UI", 8.5F);
        _statusComboBox.ForeColor = Color.White;
        _statusComboBox.ItemHeight = 30;
        _statusComboBox.Items.AddRange(new object[] { "Todos", "Criado", "Pendente", "Aprovado", "Separação", "Faturado", "Enviado", "Entregue" });
        _statusComboBox.Location = new Point(0, 18);
        _statusComboBox.Name = "_statusComboBox";
        _statusComboBox.ShadowDecoration.CustomizableEdges = customizableEdges18;
        _statusComboBox.Size = new Size(116, 36);
        _statusComboBox.TabIndex = 0;
        // 
        // _statusCaption
        // 
        _statusCaption.AutoSize = true;
        _statusCaption.Font = new Font("Segoe UI Semibold", 7.5F);
        _statusCaption.ForeColor = Color.FromArgb(210, 195, 182);
        _statusCaption.Location = new Point(4, 0);
        _statusCaption.Name = "_statusCaption";
        _statusCaption.Size = new Size(33, 12);
        _statusCaption.TabIndex = 1;
        _statusCaption.Text = "Status";
        // 
        // _searchFilterPanel
        // 
        _searchFilterPanel.BackColor = Color.Transparent;
        _searchFilterPanel.Controls.Add(_searchCaption);
        _searchFilterPanel.Controls.Add(_searchTextBox);
        _searchFilterPanel.Location = new Point(367, 0);
        _searchFilterPanel.Margin = new Padding(0, 0, 4, 0);
        _searchFilterPanel.Name = "_searchFilterPanel";
        _searchFilterPanel.Size = new Size(170, 62);
        _searchFilterPanel.TabIndex = 3;
        // 
        // _searchCaption
        // 
        _searchCaption.AutoSize = true;
        _searchCaption.Font = new Font("Segoe UI Semibold", 7.5F);
        _searchCaption.ForeColor = Color.FromArgb(210, 195, 182);
        _searchCaption.Location = new Point(4, 0);
        _searchCaption.Name = "_searchCaption";
        _searchCaption.Size = new Size(35, 12);
        _searchCaption.TabIndex = 1;
        _searchCaption.Text = "Buscar";
        // 
        // _searchTextBox
        // 
        _searchTextBox.BorderColor = Color.FromArgb(120, 79, 43);
        _searchTextBox.BorderRadius = 8;
        _searchTextBox.CustomizableEdges = customizableEdges19;
        _searchTextBox.DefaultText = "";
        _searchTextBox.FillColor = Color.FromArgb(55, 33, 23);
        _searchTextBox.Font = new Font("Segoe UI", 8.5F);
        _searchTextBox.ForeColor = Color.White;
        _searchTextBox.Location = new Point(-1, 20);
        _searchTextBox.Name = "_searchTextBox";
        _searchTextBox.PlaceholderForeColor = Color.FromArgb(170, 155, 143);
        _searchTextBox.PlaceholderText = "Digite nº pedido, cliente...";
        _searchTextBox.SelectedText = "";
        _searchTextBox.ShadowDecoration.CustomizableEdges = customizableEdges20;
        _searchTextBox.Size = new Size(168, 34);
        _searchTextBox.TabIndex = 0;
        // 
        // panel1
        // 
        panel1.BackColor = Color.Transparent;
        panel1.Controls.Add(_exportButton);
        panel1.Location = new Point(541, 0);
        panel1.Margin = new Padding(0, 0, 4, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(101, 62);
        panel1.TabIndex = 6;
        // 
        // _exportButton
        // 
        _exportButton.BorderRadius = 9;
        _exportButton.Cursor = Cursors.Hand;
        _exportButton.CustomizableEdges = customizableEdges21;
        _exportButton.FillColor = Color.FromArgb(82, 55, 38);
        _exportButton.Font = new Font("Segoe UI Semibold", 8.5F);
        _exportButton.ForeColor = Color.White;
        _exportButton.Location = new Point(8, 15);
        _exportButton.Name = "_exportButton";
        _exportButton.ShadowDecoration.CustomizableEdges = customizableEdges22;
        _exportButton.Size = new Size(82, 36);
        _exportButton.TabIndex = 5;
        _exportButton.Text = "⇩ Exportar";
        // 
        // panel2
        // 
        panel2.BackColor = Color.Transparent;
        panel2.Controls.Add(_clearButton);
        panel2.Location = new Point(646, 0);
        panel2.Margin = new Padding(0, 0, 4, 0);
        panel2.Name = "panel2";
        panel2.Size = new Size(101, 62);
        panel2.TabIndex = 7;
        // 
        // _clearButton
        // 
        _clearButton.BorderRadius = 9;
        _clearButton.Cursor = Cursors.Hand;
        _clearButton.CustomizableEdges = customizableEdges23;
        _clearButton.FillColor = Color.FromArgb(82, 55, 38);
        _clearButton.Font = new Font("Segoe UI Semibold", 8.5F);
        _clearButton.ForeColor = Color.White;
        _clearButton.Location = new Point(15, 15);
        _clearButton.Name = "_clearButton";
        _clearButton.ShadowDecoration.CustomizableEdges = customizableEdges24;
        _clearButton.Size = new Size(72, 36);
        _clearButton.TabIndex = 4;
        _clearButton.Text = "Limpar";
        // 
        // _stepsHost
        // 
        _stepsHost.BackColor = Color.Transparent;
        _stepsHost.Controls.Add(_stepsFlowPanel);
        _stepsHost.Dock = DockStyle.Top;
        _stepsHost.Location = new Point(18, 75);
        _stepsHost.Name = "_stepsHost";
        _stepsHost.Size = new Size(1429, 114);
        _stepsHost.TabIndex = 2;
        // 
        // _stepsFlowPanel
        // 
        _stepsFlowPanel.BackColor = Color.Transparent;
        _stepsFlowPanel.Controls.Add(_stepCreatedPanel);
        _stepsFlowPanel.Controls.Add(_connector1);
        _stepsFlowPanel.Controls.Add(_stepPendingPanel);
        _stepsFlowPanel.Controls.Add(_connector2);
        _stepsFlowPanel.Controls.Add(_stepApprovedPanel);
        _stepsFlowPanel.Controls.Add(_connector3);
        _stepsFlowPanel.Controls.Add(_stepSeparationPanel);
        _stepsFlowPanel.Controls.Add(_connector4);
        _stepsFlowPanel.Controls.Add(_stepInvoicedPanel);
        _stepsFlowPanel.Controls.Add(_connector5);
        _stepsFlowPanel.Controls.Add(_stepShippedPanel);
        _stepsFlowPanel.Controls.Add(_connector6);
        _stepsFlowPanel.Controls.Add(_stepDeliveredPanel);
        _stepsFlowPanel.Dock = DockStyle.Top;
        _stepsFlowPanel.Location = new Point(0, 0);
        _stepsFlowPanel.Margin = new Padding(0);
        _stepsFlowPanel.Name = "_stepsFlowPanel";
        _stepsFlowPanel.Padding = new Padding(4, 4, 4, 0);
        _stepsFlowPanel.Size = new Size(1429, 111);
        _stepsFlowPanel.TabIndex = 0;
        _stepsFlowPanel.WrapContents = false;
        // 
        // _stepCreatedPanel
        // 
        _stepCreatedPanel.BackColor = Color.Transparent;
        _stepCreatedPanel.Controls.Add(_stepCreatedDescription);
        _stepCreatedPanel.Controls.Add(_stepCreatedName);
        _stepCreatedPanel.Controls.Add(_stepCreatedIcon);
        _stepCreatedPanel.Location = new Point(4, 4);
        _stepCreatedPanel.Margin = new Padding(0);
        _stepCreatedPanel.Name = "_stepCreatedPanel";
        _stepCreatedPanel.Size = new Size(82, 112);
        _stepCreatedPanel.TabIndex = 0;
        // 
        // _stepCreatedDescription
        // 
        _stepCreatedDescription.Font = new Font("Segoe UI", 6.5F);
        _stepCreatedDescription.ForeColor = Color.FromArgb(204, 190, 177);
        _stepCreatedDescription.Location = new Point(0, 68);
        _stepCreatedDescription.Name = "_stepCreatedDescription";
        _stepCreatedDescription.Size = new Size(82, 44);
        _stepCreatedDescription.TabIndex = 0;
        _stepCreatedDescription.Text = "Compra registrada\nno sistema.";
        _stepCreatedDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepCreatedName
        // 
        _stepCreatedName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _stepCreatedName.ForeColor = Color.White;
        _stepCreatedName.Location = new Point(0, 45);
        _stepCreatedName.Name = "_stepCreatedName";
        _stepCreatedName.Size = new Size(82, 20);
        _stepCreatedName.TabIndex = 0;
        _stepCreatedName.Text = "Criado";
        _stepCreatedName.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepCreatedIcon
        // 
        _stepCreatedIcon.BackColor = Color.FromArgb(70, 43, 27);
        _stepCreatedIcon.BorderStyle = BorderStyle.FixedSingle;
        _stepCreatedIcon.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
        _stepCreatedIcon.ForeColor = Color.White;
        _stepCreatedIcon.Location = new Point(21, 0);
        _stepCreatedIcon.Name = "_stepCreatedIcon";
        _stepCreatedIcon.Size = new Size(40, 40);
        _stepCreatedIcon.TabIndex = 0;
        _stepCreatedIcon.Text = "▣";
        _stepCreatedIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _connector1
        // 
        _connector1.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _connector1.ForeColor = Color.FromArgb(151, 101, 52);
        _connector1.Location = new Point(86, 4);
        _connector1.Margin = new Padding(0);
        _connector1.Name = "_connector1";
        _connector1.Size = new Size(8, 40);
        _connector1.TabIndex = 1;
        _connector1.Text = "────";
        _connector1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _stepPendingPanel
        // 
        _stepPendingPanel.BackColor = Color.Transparent;
        _stepPendingPanel.Controls.Add(_stepPendingDescription);
        _stepPendingPanel.Controls.Add(_stepPendingName);
        _stepPendingPanel.Controls.Add(_stepPendingIcon);
        _stepPendingPanel.Location = new Point(94, 4);
        _stepPendingPanel.Margin = new Padding(0);
        _stepPendingPanel.Name = "_stepPendingPanel";
        _stepPendingPanel.Size = new Size(82, 112);
        _stepPendingPanel.TabIndex = 2;
        // 
        // _stepPendingDescription
        // 
        _stepPendingDescription.Font = new Font("Segoe UI", 6.5F);
        _stepPendingDescription.ForeColor = Color.FromArgb(204, 190, 177);
        _stepPendingDescription.Location = new Point(0, 68);
        _stepPendingDescription.Name = "_stepPendingDescription";
        _stepPendingDescription.Size = new Size(82, 44);
        _stepPendingDescription.TabIndex = 0;
        _stepPendingDescription.Text = "Aguardando a\naprovação do pagamento.";
        _stepPendingDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepPendingName
        // 
        _stepPendingName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _stepPendingName.ForeColor = Color.White;
        _stepPendingName.Location = new Point(0, 45);
        _stepPendingName.Name = "_stepPendingName";
        _stepPendingName.Size = new Size(82, 20);
        _stepPendingName.TabIndex = 0;
        _stepPendingName.Text = "Pendente";
        _stepPendingName.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepPendingIcon
        // 
        _stepPendingIcon.BackColor = Color.FromArgb(70, 43, 27);
        _stepPendingIcon.BorderStyle = BorderStyle.FixedSingle;
        _stepPendingIcon.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
        _stepPendingIcon.ForeColor = Color.White;
        _stepPendingIcon.Location = new Point(21, 0);
        _stepPendingIcon.Name = "_stepPendingIcon";
        _stepPendingIcon.Size = new Size(40, 40);
        _stepPendingIcon.TabIndex = 0;
        _stepPendingIcon.Text = "⌛";
        _stepPendingIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _connector2
        // 
        _connector2.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _connector2.ForeColor = Color.FromArgb(151, 101, 52);
        _connector2.Location = new Point(176, 4);
        _connector2.Margin = new Padding(0);
        _connector2.Name = "_connector2";
        _connector2.Size = new Size(8, 40);
        _connector2.TabIndex = 3;
        _connector2.Text = "────";
        _connector2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _stepApprovedPanel
        // 
        _stepApprovedPanel.BackColor = Color.Transparent;
        _stepApprovedPanel.Controls.Add(_stepApprovedDescription);
        _stepApprovedPanel.Controls.Add(_stepApprovedName);
        _stepApprovedPanel.Controls.Add(_stepApprovedIcon);
        _stepApprovedPanel.Location = new Point(184, 4);
        _stepApprovedPanel.Margin = new Padding(0);
        _stepApprovedPanel.Name = "_stepApprovedPanel";
        _stepApprovedPanel.Size = new Size(82, 112);
        _stepApprovedPanel.TabIndex = 4;
        // 
        // _stepApprovedDescription
        // 
        _stepApprovedDescription.Font = new Font("Segoe UI", 6.5F);
        _stepApprovedDescription.ForeColor = Color.FromArgb(204, 190, 177);
        _stepApprovedDescription.Location = new Point(0, 68);
        _stepApprovedDescription.Name = "_stepApprovedDescription";
        _stepApprovedDescription.Size = new Size(82, 44);
        _stepApprovedDescription.TabIndex = 0;
        _stepApprovedDescription.Text = "Pagamento confirmado\ne pedido liberado.";
        _stepApprovedDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepApprovedName
        // 
        _stepApprovedName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _stepApprovedName.ForeColor = Color.White;
        _stepApprovedName.Location = new Point(0, 45);
        _stepApprovedName.Name = "_stepApprovedName";
        _stepApprovedName.Size = new Size(82, 20);
        _stepApprovedName.TabIndex = 0;
        _stepApprovedName.Text = "Aprovado";
        _stepApprovedName.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepApprovedIcon
        // 
        _stepApprovedIcon.BackColor = Color.FromArgb(70, 43, 27);
        _stepApprovedIcon.BorderStyle = BorderStyle.FixedSingle;
        _stepApprovedIcon.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
        _stepApprovedIcon.ForeColor = Color.White;
        _stepApprovedIcon.Location = new Point(21, 0);
        _stepApprovedIcon.Name = "_stepApprovedIcon";
        _stepApprovedIcon.Size = new Size(40, 40);
        _stepApprovedIcon.TabIndex = 0;
        _stepApprovedIcon.Text = "✓";
        _stepApprovedIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _connector3
        // 
        _connector3.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _connector3.ForeColor = Color.FromArgb(151, 101, 52);
        _connector3.Location = new Point(266, 4);
        _connector3.Margin = new Padding(0);
        _connector3.Name = "_connector3";
        _connector3.Size = new Size(8, 40);
        _connector3.TabIndex = 5;
        _connector3.Text = "────";
        _connector3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _stepSeparationPanel
        // 
        _stepSeparationPanel.BackColor = Color.Transparent;
        _stepSeparationPanel.Controls.Add(_stepSeparationDescription);
        _stepSeparationPanel.Controls.Add(_stepSeparationName);
        _stepSeparationPanel.Controls.Add(_stepSeparationIcon);
        _stepSeparationPanel.Location = new Point(274, 4);
        _stepSeparationPanel.Margin = new Padding(0);
        _stepSeparationPanel.Name = "_stepSeparationPanel";
        _stepSeparationPanel.Size = new Size(82, 112);
        _stepSeparationPanel.TabIndex = 6;
        // 
        // _stepSeparationDescription
        // 
        _stepSeparationDescription.Font = new Font("Segoe UI", 6.5F);
        _stepSeparationDescription.ForeColor = Color.FromArgb(204, 190, 177);
        _stepSeparationDescription.Location = new Point(0, 68);
        _stepSeparationDescription.Name = "_stepSeparationDescription";
        _stepSeparationDescription.Size = new Size(82, 44);
        _stepSeparationDescription.TabIndex = 0;
        _stepSeparationDescription.Text = "Produto localizado\ne embalado.";
        _stepSeparationDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepSeparationName
        // 
        _stepSeparationName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _stepSeparationName.ForeColor = Color.White;
        _stepSeparationName.Location = new Point(0, 45);
        _stepSeparationName.Name = "_stepSeparationName";
        _stepSeparationName.Size = new Size(82, 20);
        _stepSeparationName.TabIndex = 0;
        _stepSeparationName.Text = "Separação";
        _stepSeparationName.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepSeparationIcon
        // 
        _stepSeparationIcon.BackColor = Color.FromArgb(70, 43, 27);
        _stepSeparationIcon.BorderStyle = BorderStyle.FixedSingle;
        _stepSeparationIcon.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
        _stepSeparationIcon.ForeColor = Color.White;
        _stepSeparationIcon.Location = new Point(21, 0);
        _stepSeparationIcon.Name = "_stepSeparationIcon";
        _stepSeparationIcon.Size = new Size(40, 40);
        _stepSeparationIcon.TabIndex = 0;
        _stepSeparationIcon.Text = "□";
        _stepSeparationIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _connector4
        // 
        _connector4.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _connector4.ForeColor = Color.FromArgb(151, 101, 52);
        _connector4.Location = new Point(356, 4);
        _connector4.Margin = new Padding(0);
        _connector4.Name = "_connector4";
        _connector4.Size = new Size(8, 40);
        _connector4.TabIndex = 7;
        _connector4.Text = "────";
        _connector4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _stepInvoicedPanel
        // 
        _stepInvoicedPanel.BackColor = Color.Transparent;
        _stepInvoicedPanel.Controls.Add(_stepInvoicedDescription);
        _stepInvoicedPanel.Controls.Add(_stepInvoicedName);
        _stepInvoicedPanel.Controls.Add(_stepInvoicedIcon);
        _stepInvoicedPanel.Location = new Point(364, 4);
        _stepInvoicedPanel.Margin = new Padding(0);
        _stepInvoicedPanel.Name = "_stepInvoicedPanel";
        _stepInvoicedPanel.Size = new Size(82, 112);
        _stepInvoicedPanel.TabIndex = 8;
        // 
        // _stepInvoicedDescription
        // 
        _stepInvoicedDescription.Font = new Font("Segoe UI", 6.5F);
        _stepInvoicedDescription.ForeColor = Color.FromArgb(204, 190, 177);
        _stepInvoicedDescription.Location = new Point(0, 68);
        _stepInvoicedDescription.Name = "_stepInvoicedDescription";
        _stepInvoicedDescription.Size = new Size(82, 44);
        _stepInvoicedDescription.TabIndex = 0;
        _stepInvoicedDescription.Text = "Nota Fiscal\nemitida.";
        _stepInvoicedDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepInvoicedName
        // 
        _stepInvoicedName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _stepInvoicedName.ForeColor = Color.White;
        _stepInvoicedName.Location = new Point(0, 45);
        _stepInvoicedName.Name = "_stepInvoicedName";
        _stepInvoicedName.Size = new Size(82, 20);
        _stepInvoicedName.TabIndex = 0;
        _stepInvoicedName.Text = "Faturado";
        _stepInvoicedName.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepInvoicedIcon
        // 
        _stepInvoicedIcon.BackColor = Color.FromArgb(70, 43, 27);
        _stepInvoicedIcon.BorderStyle = BorderStyle.FixedSingle;
        _stepInvoicedIcon.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
        _stepInvoicedIcon.ForeColor = Color.White;
        _stepInvoicedIcon.Location = new Point(21, 0);
        _stepInvoicedIcon.Name = "_stepInvoicedIcon";
        _stepInvoicedIcon.Size = new Size(40, 40);
        _stepInvoicedIcon.TabIndex = 0;
        _stepInvoicedIcon.Text = "▤";
        _stepInvoicedIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _connector5
        // 
        _connector5.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _connector5.ForeColor = Color.FromArgb(151, 101, 52);
        _connector5.Location = new Point(446, 4);
        _connector5.Margin = new Padding(0);
        _connector5.Name = "_connector5";
        _connector5.Size = new Size(8, 40);
        _connector5.TabIndex = 9;
        _connector5.Text = "────";
        _connector5.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _stepShippedPanel
        // 
        _stepShippedPanel.BackColor = Color.Transparent;
        _stepShippedPanel.Controls.Add(_stepShippedDescription);
        _stepShippedPanel.Controls.Add(_stepShippedName);
        _stepShippedPanel.Controls.Add(_stepShippedIcon);
        _stepShippedPanel.Location = new Point(454, 4);
        _stepShippedPanel.Margin = new Padding(0);
        _stepShippedPanel.Name = "_stepShippedPanel";
        _stepShippedPanel.Size = new Size(82, 112);
        _stepShippedPanel.TabIndex = 10;
        // 
        // _stepShippedDescription
        // 
        _stepShippedDescription.Font = new Font("Segoe UI", 6.5F);
        _stepShippedDescription.ForeColor = Color.FromArgb(204, 190, 177);
        _stepShippedDescription.Location = new Point(0, 68);
        _stepShippedDescription.Name = "_stepShippedDescription";
        _stepShippedDescription.Size = new Size(82, 44);
        _stepShippedDescription.TabIndex = 0;
        _stepShippedDescription.Text = "Pacote coletado\npela transportadora.";
        _stepShippedDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepShippedName
        // 
        _stepShippedName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _stepShippedName.ForeColor = Color.White;
        _stepShippedName.Location = new Point(0, 45);
        _stepShippedName.Name = "_stepShippedName";
        _stepShippedName.Size = new Size(82, 20);
        _stepShippedName.TabIndex = 0;
        _stepShippedName.Text = "Enviado";
        _stepShippedName.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepShippedIcon
        // 
        _stepShippedIcon.BackColor = Color.FromArgb(70, 43, 27);
        _stepShippedIcon.BorderStyle = BorderStyle.FixedSingle;
        _stepShippedIcon.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
        _stepShippedIcon.ForeColor = Color.White;
        _stepShippedIcon.Location = new Point(21, 0);
        _stepShippedIcon.Name = "_stepShippedIcon";
        _stepShippedIcon.Size = new Size(40, 40);
        _stepShippedIcon.TabIndex = 0;
        _stepShippedIcon.Text = "▰";
        _stepShippedIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _connector6
        // 
        _connector6.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _connector6.ForeColor = Color.FromArgb(151, 101, 52);
        _connector6.Location = new Point(536, 4);
        _connector6.Margin = new Padding(0);
        _connector6.Name = "_connector6";
        _connector6.Size = new Size(8, 40);
        _connector6.TabIndex = 11;
        _connector6.Text = "────";
        _connector6.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _stepDeliveredPanel
        // 
        _stepDeliveredPanel.BackColor = Color.Transparent;
        _stepDeliveredPanel.Controls.Add(_stepDeliveredDescription);
        _stepDeliveredPanel.Controls.Add(_stepDeliveredName);
        _stepDeliveredPanel.Controls.Add(_stepDeliveredIcon);
        _stepDeliveredPanel.Location = new Point(544, 4);
        _stepDeliveredPanel.Margin = new Padding(0);
        _stepDeliveredPanel.Name = "_stepDeliveredPanel";
        _stepDeliveredPanel.Size = new Size(82, 112);
        _stepDeliveredPanel.TabIndex = 12;
        // 
        // _stepDeliveredDescription
        // 
        _stepDeliveredDescription.Font = new Font("Segoe UI", 6.5F);
        _stepDeliveredDescription.ForeColor = Color.FromArgb(204, 190, 177);
        _stepDeliveredDescription.Location = new Point(0, 68);
        _stepDeliveredDescription.Name = "_stepDeliveredDescription";
        _stepDeliveredDescription.Size = new Size(82, 44);
        _stepDeliveredDescription.TabIndex = 0;
        _stepDeliveredDescription.Text = "Produto entregue\nao destinatário.";
        _stepDeliveredDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepDeliveredName
        // 
        _stepDeliveredName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        _stepDeliveredName.ForeColor = Color.White;
        _stepDeliveredName.Location = new Point(0, 45);
        _stepDeliveredName.Name = "_stepDeliveredName";
        _stepDeliveredName.Size = new Size(82, 20);
        _stepDeliveredName.TabIndex = 0;
        _stepDeliveredName.Text = "Entregue";
        _stepDeliveredName.TextAlign = ContentAlignment.TopCenter;
        // 
        // _stepDeliveredIcon
        // 
        _stepDeliveredIcon.BackColor = Color.FromArgb(70, 43, 27);
        _stepDeliveredIcon.BorderStyle = BorderStyle.FixedSingle;
        _stepDeliveredIcon.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
        _stepDeliveredIcon.ForeColor = Color.White;
        _stepDeliveredIcon.Location = new Point(21, 0);
        _stepDeliveredIcon.Name = "_stepDeliveredIcon";
        _stepDeliveredIcon.Size = new Size(40, 40);
        _stepDeliveredIcon.TabIndex = 0;
        _stepDeliveredIcon.Text = "✓";
        _stepDeliveredIcon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _headingPanel
        // 
        _headingPanel.BackColor = Color.Transparent;
        _headingPanel.Controls.Add(_refreshButton);
        _headingPanel.Controls.Add(_subtitleLabel);
        _headingPanel.Controls.Add(_titleLabel);
        _headingPanel.Dock = DockStyle.Top;
        _headingPanel.Location = new Point(18, 14);
        _headingPanel.Name = "_headingPanel";
        _headingPanel.Size = new Size(1429, 61);
        _headingPanel.TabIndex = 3;
        // 
        // _refreshButton
        // 
        _refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _refreshButton.BorderRadius = 9;
        _refreshButton.Cursor = Cursors.Hand;
        _refreshButton.CustomizableEdges = customizableEdges27;
        _refreshButton.FillColor = Color.Silver;
        _refreshButton.Font = new Font("Segoe UI Semibold", 9F);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(1229, 4);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.ShadowDecoration.CustomizableEdges = customizableEdges28;
        _refreshButton.Size = new Size(108, 38);
        _refreshButton.TabIndex = 0;
        _refreshButton.Text = "↻ Atualizar";
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.AutoSize = true;
        _subtitleLabel.Font = new Font("Segoe UI", 9F);
        _subtitleLabel.ForeColor = Color.FromArgb(226, 213, 201);
        _subtitleLabel.Location = new Point(2, 35);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(319, 15);
        _subtitleLabel.TabIndex = 1;
        _subtitleLabel.Text = "Acompanhe o andamento dos pedidos em todas as etapas.";
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.White;
        _titleLabel.Location = new Point(0, 0);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(215, 32);
        _titleLabel.TabIndex = 2;
        _titleLabel.Text = "Status de Pedidos";
        // 
        // _lastUpdateColumn
        // 
        _lastUpdateColumn.Name = "_lastUpdateColumn";
        // 
        // OrdersStatusUserControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_rootPanel);
        Name = "OrdersStatusUserControl";
        Size = new Size(1465, 438);
        _rootPanel.ResumeLayout(false);
        _gridCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _bottomPanel.ResumeLayout(false);
        _bottomPanel.PerformLayout();
        _paginationPanel.ResumeLayout(false);
        _filterCard.ResumeLayout(false);
        _filterFlowPanel.ResumeLayout(false);
        _startFilterPanel.ResumeLayout(false);
        _startFilterPanel.PerformLayout();
        _endFilterPanel.ResumeLayout(false);
        _endFilterPanel.PerformLayout();
        _statusFilterPanel.ResumeLayout(false);
        _statusFilterPanel.PerformLayout();
        _searchFilterPanel.ResumeLayout(false);
        _searchFilterPanel.PerformLayout();
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        _stepsHost.ResumeLayout(false);
        _stepsFlowPanel.ResumeLayout(false);
        _stepCreatedPanel.ResumeLayout(false);
        _stepPendingPanel.ResumeLayout(false);
        _stepApprovedPanel.ResumeLayout(false);
        _stepSeparationPanel.ResumeLayout(false);
        _stepInvoicedPanel.ResumeLayout(false);
        _stepShippedPanel.ResumeLayout(false);
        _stepDeliveredPanel.ResumeLayout(false);
        _headingPanel.ResumeLayout(false);
        _headingPanel.PerformLayout();
        ResumeLayout(false);
    }

    private DataGridViewTextBoxColumn _numberColumn;
    private DataGridViewTextBoxColumn _dateColumn;
    private DataGridViewTextBoxColumn _customerColumn;
    private DataGridViewTextBoxColumn _totalColumn;
    private DataGridViewComboBoxColumn _statusColumn;
    private DataGridViewTextBoxColumn _lastUpdateColumn;
    private DataGridViewButtonColumn _actionsColumn;
}