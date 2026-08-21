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
        _borderlessForm = new Guna2BorderlessForm(components);
        _dragControl = new Guna2DragControl(components);
        _headerPanel = new Guna2Panel();
        _bodyPanel = new Guna2Panel();
        _titleLabel = new Label();
        _subtitleLabel = new Label();
        _detailsTable = new TableLayoutPanel();
        _numberCaptionLabel = new Label();
        _customerCaptionLabel = new Label();
        _dateCaptionLabel = new Label();
        _totalCaptionLabel = new Label();
        _statusCaptionLabel = new Label();
        _lastUpdateCaptionLabel = new Label();
        _numberValueLabel = new Label();
        _customerValueLabel = new Label();
        _dateValueLabel = new Label();
        _totalValueLabel = new Label();
        _statusValueLabel = new Label();
        _lastUpdateValueLabel = new Label();
        _flowLabel = new Label();
        _closeButton = new Guna2Button();

        SuspendLayout();
        _headerPanel.SuspendLayout();
        _bodyPanel.SuspendLayout();
        _detailsTable.SuspendLayout();

        // Borderless form / drag control
        _borderlessForm.ContainerControl = this;
        _borderlessForm.BorderRadius = 14;
        _borderlessForm.ShadowColor = Color.Black;
        _dragControl.TargetControl = _headerPanel;

        // Form
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(560, 470);
        BackColor = Color.FromArgb(43, 26, 18);
        Text = "Detalhes do Pedido";
        MinimizeBox = false;
        MaximizeBox = false;

        // Header
        _headerPanel.Name = "_headerPanel";
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.Height = 84;
        _headerPanel.FillColor = Color.FromArgb(43, 26, 18);
        _headerPanel.Padding = new Padding(22, 14, 22, 10);

        _titleLabel.Name = "_titleLabel";
        _titleLabel.AutoSize = true;
        _titleLabel.Text = "Pedido";
        _titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.White;
        _titleLabel.Location = new Point(22, 14);

        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.AutoSize = true;
        _subtitleLabel.Text = "Detalhes e situação atual do pedido";
        _subtitleLabel.Font = new Font("Segoe UI", 9F);
        _subtitleLabel.ForeColor = Color.FromArgb(218, 204, 190);
        _subtitleLabel.Location = new Point(24, 48);

        _headerPanel.Controls.Add(_subtitleLabel);
        _headerPanel.Controls.Add(_titleLabel);

        // Body
        _bodyPanel.Name = "_bodyPanel";
        _bodyPanel.Dock = DockStyle.Fill;
        _bodyPanel.FillColor = Color.FromArgb(65, 40, 27);
        _bodyPanel.Padding = new Padding(22, 18, 22, 18);

        // Details table
        _detailsTable.Name = "_detailsTable";
        _detailsTable.Dock = DockStyle.Top;
        _detailsTable.ColumnCount = 2;
        _detailsTable.RowCount = 6;
        _detailsTable.AutoSize = true;
        _detailsTable.BackColor = Color.Transparent;
        _detailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
        _detailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        _detailsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

        // Captions
        _numberCaptionLabel.Name = "_numberCaptionLabel";
        _numberCaptionLabel.Text = "Nº Pedido";
        _numberCaptionLabel.Dock = DockStyle.Fill;
        _numberCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        _numberCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _numberCaptionLabel.ForeColor = Color.FromArgb(218, 204, 190);

        _customerCaptionLabel.Name = "_customerCaptionLabel";
        _customerCaptionLabel.Text = "Cliente";
        _customerCaptionLabel.Dock = DockStyle.Fill;
        _customerCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        _customerCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _customerCaptionLabel.ForeColor = Color.FromArgb(218, 204, 190);

        _dateCaptionLabel.Name = "_dateCaptionLabel";
        _dateCaptionLabel.Text = "Data";
        _dateCaptionLabel.Dock = DockStyle.Fill;
        _dateCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        _dateCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _dateCaptionLabel.ForeColor = Color.FromArgb(218, 204, 190);

        _totalCaptionLabel.Name = "_totalCaptionLabel";
        _totalCaptionLabel.Text = "Valor Total";
        _totalCaptionLabel.Dock = DockStyle.Fill;
        _totalCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        _totalCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _totalCaptionLabel.ForeColor = Color.FromArgb(218, 204, 190);

        _statusCaptionLabel.Name = "_statusCaptionLabel";
        _statusCaptionLabel.Text = "Status";
        _statusCaptionLabel.Dock = DockStyle.Fill;
        _statusCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        _statusCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _statusCaptionLabel.ForeColor = Color.FromArgb(218, 204, 190);

        _lastUpdateCaptionLabel.Name = "_lastUpdateCaptionLabel";
        _lastUpdateCaptionLabel.Text = "Última Atualização";
        _lastUpdateCaptionLabel.Dock = DockStyle.Fill;
        _lastUpdateCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        _lastUpdateCaptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _lastUpdateCaptionLabel.ForeColor = Color.FromArgb(218, 204, 190);

        // Values
        _numberValueLabel.Name = "_numberValueLabel";
        _numberValueLabel.Dock = DockStyle.Fill;
        _numberValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        _numberValueLabel.Font = new Font("Segoe UI", 9.5F);
        _numberValueLabel.ForeColor = Color.White;

        _customerValueLabel.Name = "_customerValueLabel";
        _customerValueLabel.Dock = DockStyle.Fill;
        _customerValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        _customerValueLabel.Font = new Font("Segoe UI", 9.5F);
        _customerValueLabel.ForeColor = Color.White;

        _dateValueLabel.Name = "_dateValueLabel";
        _dateValueLabel.Dock = DockStyle.Fill;
        _dateValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        _dateValueLabel.Font = new Font("Segoe UI", 9.5F);
        _dateValueLabel.ForeColor = Color.White;

        _totalValueLabel.Name = "_totalValueLabel";
        _totalValueLabel.Dock = DockStyle.Fill;
        _totalValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        _totalValueLabel.Font = new Font("Segoe UI", 9.5F);
        _totalValueLabel.ForeColor = Color.White;

        _statusValueLabel.Name = "_statusValueLabel";
        _statusValueLabel.Dock = DockStyle.Fill;
        _statusValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        _statusValueLabel.Font = new Font("Segoe UI", 9.5F);
        _statusValueLabel.ForeColor = Color.White;

        _lastUpdateValueLabel.Name = "_lastUpdateValueLabel";
        _lastUpdateValueLabel.Dock = DockStyle.Fill;
        _lastUpdateValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        _lastUpdateValueLabel.Font = new Font("Segoe UI", 9.5F);
        _lastUpdateValueLabel.ForeColor = Color.White;

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

        // Flow information
        _flowLabel.Name = "_flowLabel";
        _flowLabel.AutoSize = false;
        _flowLabel.Dock = DockStyle.Top;
        _flowLabel.Height = 62;
        _flowLabel.Font = new Font("Segoe UI", 8.5F);
        _flowLabel.ForeColor = Color.FromArgb(218, 204, 190);
        _flowLabel.Padding = new Padding(0, 14, 0, 0);
        _flowLabel.Text = "Fluxo do pedido";

        // Close button
        _closeButton.Name = "_closeButton";
        _closeButton.Text = "Fechar";
        _closeButton.Size = new Size(105, 38);
        _closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _closeButton.Location = new Point(411, 354);
        _closeButton.BorderRadius = 9;
        _closeButton.BorderThickness = 0;
        _closeButton.FillColor = LibraryTheme.Accent;
        _closeButton.ForeColor = Color.White;
        _closeButton.Font = new Font("Segoe UI Semibold", 9F);
        _closeButton.Cursor = Cursors.Hand;
        _closeButton.Click += CloseButton_Click;

        _bodyPanel.Controls.Add(_closeButton);
        _bodyPanel.Controls.Add(_flowLabel);
        _bodyPanel.Controls.Add(_detailsTable);

        Controls.Add(_bodyPanel);
        Controls.Add(_headerPanel);

        _detailsTable.ResumeLayout(false);
        _detailsTable.PerformLayout();
        _bodyPanel.ResumeLayout(false);
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        ResumeLayout(false);
    }
}
