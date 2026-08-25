using AtelieDaTransformacao.Desktop.Themes;
using Guna.UI2.WinForms;

namespace AtelieDaTransformacao.Desktop.Forms;

partial class OrderDetailsDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Guna2BorderlessForm _borderlessForm = null!;
    private Guna2DragControl _dragControl = null!;
    private Guna2Panel _headerPanel = null!;
    private Guna2Panel _bodyPanel = null!;
    private Label _titleLabel = null!;
    private Label _subtitleLabel = null!;
    private TableLayoutPanel _detailsTable = null!;
    private Label _numberCaptionLabel = null!;
    private Label _customerCaptionLabel = null!;
    private Label _dateCaptionLabel = null!;
    private Label _totalCaptionLabel = null!;
    private Label _statusCaptionLabel = null!;
    private Label _lastUpdateCaptionLabel = null!;
    private Label _numberValueLabel = null!;
    private Label _customerValueLabel = null!;
    private Label _dateValueLabel = null!;
    private Label _totalValueLabel = null!;
    private Label _statusValueLabel = null!;
    private Label _lastUpdateValueLabel = null!;
    private Label _flowLabel = null!;
    private Guna2Button _closeButton = null!;

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
        components = new System.ComponentModel.Container();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _borderlessForm = new Guna2BorderlessForm(components);
        _dragControl = new Guna2DragControl(components);
        _headerPanel = new Guna2Panel();
        _subtitleLabel = new Label();
        _titleLabel = new Label();
        _bodyPanel = new Guna2Panel();
        _closeButton = new Guna2Button();
        _flowLabel = new Label();
        _detailsTable = new TableLayoutPanel();
        _numberCaptionLabel = new Label();
        _numberValueLabel = new Label();
        _customerCaptionLabel = new Label();
        _customerValueLabel = new Label();
        _dateCaptionLabel = new Label();
        _dateValueLabel = new Label();
        _totalCaptionLabel = new Label();
        _totalValueLabel = new Label();
        _statusCaptionLabel = new Label();
        _statusValueLabel = new Label();
        _lastUpdateCaptionLabel = new Label();
        _lastUpdateValueLabel = new Label();
        btnClose = new Guna2ControlBox();
        _headerPanel.SuspendLayout();
        _bodyPanel.SuspendLayout();
        _detailsTable.SuspendLayout();
        SuspendLayout();
        // 
        // _borderlessForm
        // 
        _borderlessForm.BorderRadius = 14;
        _borderlessForm.ContainerControl = this;
        _borderlessForm.DockIndicatorTransparencyValue = 0.6D;
        _borderlessForm.TransparentWhileDrag = true;
        // 
        // _dragControl
        // 
        _dragControl.DockIndicatorTransparencyValue = 0.6D;
        _dragControl.TargetControl = _headerPanel;
        _dragControl.UseTransparentDrag = true;
        // 
        // _headerPanel
        // 
        _headerPanel.BackColor = Color.FromArgb(43, 26, 18);
        _headerPanel.Controls.Add(btnClose);
        _headerPanel.Controls.Add(_subtitleLabel);
        _headerPanel.Controls.Add(_titleLabel);
        _headerPanel.CustomizableEdges = customizableEdges7;
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.FillColor = Color.FromArgb(43, 26, 18);
        _headerPanel.Location = new Point(0, 0);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.Padding = new Padding(22, 14, 22, 10);
        _headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _headerPanel.Size = new Size(1280, 84);
        _headerPanel.TabIndex = 1;
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.AutoSize = true;
        _subtitleLabel.Font = new Font("Segoe UI", 9F);
        _subtitleLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _subtitleLabel.Location = new Point(24, 48);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(194, 15);
        _subtitleLabel.TabIndex = 0;
        _subtitleLabel.Text = "Detalhes e situação atual do pedido";
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.FromArgb(239, 230, 220);
        _titleLabel.Location = new Point(22, 14);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(88, 31);
        _titleLabel.TabIndex = 1;
        _titleLabel.Text = "Pedido";
        // 
        // _bodyPanel
        // 
        _bodyPanel.Controls.Add(_closeButton);
        _bodyPanel.Controls.Add(_flowLabel);
        _bodyPanel.Controls.Add(_detailsTable);
        _bodyPanel.CustomizableEdges = customizableEdges3;
        _bodyPanel.Dock = DockStyle.Fill;
        _bodyPanel.FillColor = Color.FromArgb(43, 26, 18);
        _bodyPanel.Location = new Point(0, 84);
        _bodyPanel.Name = "_bodyPanel";
        _bodyPanel.Padding = new Padding(22, 18, 22, 18);
        _bodyPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _bodyPanel.Size = new Size(1280, 386);
        _bodyPanel.TabIndex = 0;
        // 
        // _closeButton
        // 
        _closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _closeButton.BorderRadius = 9;
        _closeButton.Cursor = Cursors.Hand;
        _closeButton.CustomizableEdges = customizableEdges1;
        _closeButton.FillColor = Color.FromArgb(88, 52, 27);
        _closeButton.Font = new Font("Segoe UI Semibold", 9F);
        _closeButton.ForeColor = Color.White;
        _closeButton.Location = new Point(1491, 640);
        _closeButton.Name = "_closeButton";
        _closeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _closeButton.Size = new Size(105, 38);
        _closeButton.TabIndex = 0;
        _closeButton.Text = "Fechar";
        _closeButton.Click += CloseButton_Click;
        // 
        // _flowLabel
        // 
        _flowLabel.BackColor = Color.FromArgb(43, 26, 18);
        _flowLabel.Dock = DockStyle.Top;
        _flowLabel.Font = new Font("Segoe UI", 8.5F);
        _flowLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _flowLabel.Location = new Point(22, 288);
        _flowLabel.Name = "_flowLabel";
        _flowLabel.Padding = new Padding(0, 14, 0, 0);
        _flowLabel.Size = new Size(1236, 62);
        _flowLabel.TabIndex = 1;
        _flowLabel.Text = "Fluxo do pedido";
        // 
        // _detailsTable
        // 
        _detailsTable.AutoSize = true;
        _detailsTable.BackColor = Color.Transparent;
        _detailsTable.ColumnCount = 2;
        _detailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
        _detailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _detailsTable.Controls.Add(_numberCaptionLabel, 0, 0);
        _detailsTable.Controls.Add(_numberValueLabel, 1, 0);
        _detailsTable.Controls.Add(_customerCaptionLabel, 0, 1);
        _detailsTable.Controls.Add(_customerValueLabel, 1, 1);
        _detailsTable.Controls.Add(_dateCaptionLabel, 0, 2);
        _detailsTable.Controls.Add(_dateValueLabel, 1, 2);
        _detailsTable.Controls.Add(_totalCaptionLabel, 0, 3);
        _detailsTable.Controls.Add(_totalValueLabel, 1, 3);
        _detailsTable.Controls.Add(_statusCaptionLabel, 0, 4);
        _detailsTable.Controls.Add(_statusValueLabel, 1, 4);
        _detailsTable.Controls.Add(_lastUpdateCaptionLabel, 0, 5);
        _detailsTable.Controls.Add(_lastUpdateValueLabel, 1, 5);
        _detailsTable.Dock = DockStyle.Top;
        _detailsTable.Location = new Point(22, 18);
        _detailsTable.Name = "_detailsTable";
        _detailsTable.RowCount = 6;
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.Size = new Size(1236, 270);
        _detailsTable.TabIndex = 2;
        // 
        // _numberCaptionLabel
        // 
        _numberCaptionLabel.Dock = DockStyle.Fill;
        _numberCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _numberCaptionLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _numberCaptionLabel.Location = new Point(3, 0);
        _numberCaptionLabel.Name = "_numberCaptionLabel";
        _numberCaptionLabel.Size = new Size(149, 45);
        _numberCaptionLabel.TabIndex = 0;
        _numberCaptionLabel.Text = "Nº Pedido";
        _numberCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _numberValueLabel
        // 
        _numberValueLabel.Dock = DockStyle.Fill;
        _numberValueLabel.Font = new Font("Segoe UI", 9.5F);
        _numberValueLabel.ForeColor = Color.FromArgb(239, 230, 220);
        _numberValueLabel.Location = new Point(158, 0);
        _numberValueLabel.Name = "_numberValueLabel";
        _numberValueLabel.Size = new Size(1075, 45);
        _numberValueLabel.TabIndex = 1;
        _numberValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _customerCaptionLabel
        // 
        _customerCaptionLabel.Dock = DockStyle.Fill;
        _customerCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _customerCaptionLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _customerCaptionLabel.Location = new Point(3, 45);
        _customerCaptionLabel.Name = "_customerCaptionLabel";
        _customerCaptionLabel.Size = new Size(149, 45);
        _customerCaptionLabel.TabIndex = 2;
        _customerCaptionLabel.Text = "Cliente";
        _customerCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _customerValueLabel
        // 
        _customerValueLabel.Dock = DockStyle.Fill;
        _customerValueLabel.Font = new Font("Segoe UI", 9.5F);
        _customerValueLabel.ForeColor = Color.FromArgb(239, 230, 220);
        _customerValueLabel.Location = new Point(158, 45);
        _customerValueLabel.Name = "_customerValueLabel";
        _customerValueLabel.Size = new Size(1075, 45);
        _customerValueLabel.TabIndex = 3;
        _customerValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _dateCaptionLabel
        // 
        _dateCaptionLabel.Dock = DockStyle.Fill;
        _dateCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _dateCaptionLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _dateCaptionLabel.Location = new Point(3, 90);
        _dateCaptionLabel.Name = "_dateCaptionLabel";
        _dateCaptionLabel.Size = new Size(149, 45);
        _dateCaptionLabel.TabIndex = 4;
        _dateCaptionLabel.Text = "Data";
        _dateCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _dateValueLabel
        // 
        _dateValueLabel.Dock = DockStyle.Fill;
        _dateValueLabel.Font = new Font("Segoe UI", 9.5F);
        _dateValueLabel.ForeColor = Color.FromArgb(239, 230, 220);
        _dateValueLabel.Location = new Point(158, 90);
        _dateValueLabel.Name = "_dateValueLabel";
        _dateValueLabel.Size = new Size(1075, 45);
        _dateValueLabel.TabIndex = 5;
        _dateValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _totalCaptionLabel
        // 
        _totalCaptionLabel.Dock = DockStyle.Fill;
        _totalCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _totalCaptionLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _totalCaptionLabel.Location = new Point(3, 135);
        _totalCaptionLabel.Name = "_totalCaptionLabel";
        _totalCaptionLabel.Size = new Size(149, 45);
        _totalCaptionLabel.TabIndex = 6;
        _totalCaptionLabel.Text = "Valor Total";
        _totalCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _totalValueLabel
        // 
        _totalValueLabel.Dock = DockStyle.Fill;
        _totalValueLabel.Font = new Font("Segoe UI", 9.5F);
        _totalValueLabel.ForeColor = Color.FromArgb(239, 230, 220);
        _totalValueLabel.Location = new Point(158, 135);
        _totalValueLabel.Name = "_totalValueLabel";
        _totalValueLabel.Size = new Size(1075, 45);
        _totalValueLabel.TabIndex = 7;
        _totalValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _statusCaptionLabel
        // 
        _statusCaptionLabel.Dock = DockStyle.Fill;
        _statusCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _statusCaptionLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _statusCaptionLabel.Location = new Point(3, 180);
        _statusCaptionLabel.Name = "_statusCaptionLabel";
        _statusCaptionLabel.Size = new Size(149, 45);
        _statusCaptionLabel.TabIndex = 8;
        _statusCaptionLabel.Text = "Status";
        _statusCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _statusValueLabel
        // 
        _statusValueLabel.Dock = DockStyle.Fill;
        _statusValueLabel.Font = new Font("Segoe UI", 9.5F);
        _statusValueLabel.ForeColor = Color.FromArgb(239, 230, 220);
        _statusValueLabel.Location = new Point(158, 180);
        _statusValueLabel.Name = "_statusValueLabel";
        _statusValueLabel.Size = new Size(1075, 45);
        _statusValueLabel.TabIndex = 9;
        _statusValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lastUpdateCaptionLabel
        // 
        _lastUpdateCaptionLabel.Dock = DockStyle.Fill;
        _lastUpdateCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _lastUpdateCaptionLabel.ForeColor = Color.FromArgb(220, 205, 192);
        _lastUpdateCaptionLabel.Location = new Point(3, 225);
        _lastUpdateCaptionLabel.Name = "_lastUpdateCaptionLabel";
        _lastUpdateCaptionLabel.Size = new Size(149, 45);
        _lastUpdateCaptionLabel.TabIndex = 10;
        _lastUpdateCaptionLabel.Text = "Última Atualização";
        _lastUpdateCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lastUpdateValueLabel
        // 
        _lastUpdateValueLabel.Dock = DockStyle.Fill;
        _lastUpdateValueLabel.Font = new Font("Segoe UI", 9.5F);
        _lastUpdateValueLabel.ForeColor = Color.FromArgb(239, 230, 220);
        _lastUpdateValueLabel.Location = new Point(158, 225);
        _lastUpdateValueLabel.Name = "_lastUpdateValueLabel";
        _lastUpdateValueLabel.Size = new Size(1075, 45);
        _lastUpdateValueLabel.TabIndex = 11;
        _lastUpdateValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClose.CustomizableEdges = customizableEdges5;
        btnClose.FillColor = Color.Transparent;
        btnClose.IconColor = Color.White;
        btnClose.Location = new Point(1233, 12);
        btnClose.Name = "btnClose";
        btnClose.ShadowDecoration.CustomizableEdges = customizableEdges6;
        btnClose.Size = new Size(35, 30);
        btnClose.TabIndex = 3;
        btnClose.Click += btnClose_Click;
        // 
        // OrderDetailsDialog
        // 
        BackColor = Color.FromArgb(43, 26, 18);
        ClientSize = new Size(1280, 470);
        Controls.Add(_bodyPanel);
        Controls.Add(_headerPanel);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "OrderDetailsDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Detalhes do Pedido";
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        _bodyPanel.ResumeLayout(false);
        _bodyPanel.PerformLayout();
        _detailsTable.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Guna2ControlBox btnClose;
}
