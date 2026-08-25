namespace AtelieDaTransformacao.Desktop.Forms;

partial class ProductDetailsDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Guna.UI2.WinForms.Guna2Panel _rootPanel = null!;
    private PictureBox _pictureBox = null!;
    private Label _titleCaption = null!;
    private Label _titleLabel = null!;
    private Label _categoryCaption = null!;
    private Label _categoryValue = null!;
    private Label _priceCaption = null!;
    private Label _priceValue = null!;
    private Label _stockCaption = null!;
    private Label _stockValue = null!;
    private Label _statusCaption = null!;
    private Label _statusValue = null!;
    private Label _featuredCaption = null!;
    private Label _featuredValue = null!;
    private Label _descriptionCaption = null!;
    private Label _descriptionValue = null!;
    private Label _imageCaption = null!;
    private Label _imageValue = null!;
    private Guna.UI2.WinForms.Guna2Button _closeButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _rootPanel = new Guna.UI2.WinForms.Guna2Panel();
        _pictureBox = new PictureBox();
        _titleCaption = new Label();
        _titleLabel = new Label();
        _categoryCaption = new Label();
        _categoryValue = new Label();
        _priceCaption = new Label();
        _priceValue = new Label();
        _stockCaption = new Label();
        _stockValue = new Label();
        _statusCaption = new Label();
        _statusValue = new Label();
        _featuredCaption = new Label();
        _featuredValue = new Label();
        _descriptionCaption = new Label();
        _descriptionValue = new Label();
        _imageCaption = new Label();
        _imageValue = new Label();
        _closeButton = new Guna.UI2.WinForms.Guna2Button();
        _rootPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_pictureBox).BeginInit();
        SuspendLayout();
        // 
        // _rootPanel
        // 
        _rootPanel.Controls.Add(_pictureBox);
        _rootPanel.Controls.Add(_titleCaption);
        _rootPanel.Controls.Add(_titleLabel);
        _rootPanel.Controls.Add(_categoryCaption);
        _rootPanel.Controls.Add(_categoryValue);
        _rootPanel.Controls.Add(_priceCaption);
        _rootPanel.Controls.Add(_priceValue);
        _rootPanel.Controls.Add(_stockCaption);
        _rootPanel.Controls.Add(_stockValue);
        _rootPanel.Controls.Add(_statusCaption);
        _rootPanel.Controls.Add(_statusValue);
        _rootPanel.Controls.Add(_featuredCaption);
        _rootPanel.Controls.Add(_featuredValue);
        _rootPanel.Controls.Add(_descriptionCaption);
        _rootPanel.Controls.Add(_descriptionValue);
        _rootPanel.Controls.Add(_imageCaption);
        _rootPanel.Controls.Add(_imageValue);
        _rootPanel.Controls.Add(_closeButton);
        _rootPanel.CustomizableEdges = customizableEdges3;
        _rootPanel.Dock = DockStyle.Fill;
        _rootPanel.FillColor = Color.FromArgb(43, 26, 18);
        _rootPanel.Location = new Point(0, 0);
        _rootPanel.Name = "_rootPanel";
        _rootPanel.Padding = new Padding(18);
        _rootPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _rootPanel.Size = new Size(640, 470);
        _rootPanel.TabIndex = 0;
        // 
        // _pictureBox
        // 
        _pictureBox.BackColor = Color.FromArgb(65, 40, 27);
        _pictureBox.BorderStyle = BorderStyle.FixedSingle;
        _pictureBox.Location = new Point(18, 18);
        _pictureBox.Name = "_pictureBox";
        _pictureBox.Size = new Size(190, 190);
        _pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        _pictureBox.TabIndex = 0;
        _pictureBox.TabStop = false;
        // 
        // _titleCaption
        // 
        _titleCaption.AutoSize = true;
        _titleCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _titleCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _titleCaption.Location = new Point(228, 18);
        _titleCaption.Name = "_titleCaption";
        _titleCaption.Size = new Size(50, 15);
        _titleCaption.TabIndex = 1;
        _titleCaption.Text = "Produto";
        // 
        // _titleLabel
        // 
        _titleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.White;
        _titleLabel.Location = new Point(228, 40);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(390, 35);
        _titleLabel.TabIndex = 2;
        // 
        // _categoryCaption
        // 
        _categoryCaption.AutoSize = true;
        _categoryCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _categoryCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _categoryCaption.Location = new Point(228, 82);
        _categoryCaption.Name = "_categoryCaption";
        _categoryCaption.Size = new Size(57, 15);
        _categoryCaption.TabIndex = 3;
        _categoryCaption.Text = "Categoria";
        // 
        // _categoryValue
        // 
        _categoryValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _categoryValue.ForeColor = Color.White;
        _categoryValue.Location = new Point(228, 104);
        _categoryValue.Name = "_categoryValue";
        _categoryValue.Size = new Size(170, 25);
        _categoryValue.TabIndex = 4;
        _categoryValue.Text = "-";
        // 
        // _priceCaption
        // 
        _priceCaption.AutoSize = true;
        _priceCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _priceCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _priceCaption.Location = new Point(410, 82);
        _priceCaption.Name = "_priceCaption";
        _priceCaption.Size = new Size(37, 15);
        _priceCaption.TabIndex = 5;
        _priceCaption.Text = "Preço";
        // 
        // _priceValue
        // 
        _priceValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _priceValue.ForeColor = Color.White;
        _priceValue.Location = new Point(410, 104);
        _priceValue.Name = "_priceValue";
        _priceValue.Size = new Size(140, 25);
        _priceValue.TabIndex = 6;
        _priceValue.Text = "-";
        // 
        // _stockCaption
        // 
        _stockCaption.AutoSize = true;
        _stockCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _stockCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _stockCaption.Location = new Point(228, 145);
        _stockCaption.Name = "_stockCaption";
        _stockCaption.Size = new Size(49, 15);
        _stockCaption.TabIndex = 7;
        _stockCaption.Text = "Estoque";
        // 
        // _stockValue
        // 
        _stockValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _stockValue.ForeColor = Color.White;
        _stockValue.Location = new Point(228, 167);
        _stockValue.Name = "_stockValue";
        _stockValue.Size = new Size(80, 25);
        _stockValue.TabIndex = 8;
        _stockValue.Text = "-";
        // 
        // _statusCaption
        // 
        _statusCaption.AutoSize = true;
        _statusCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _statusCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _statusCaption.Location = new Point(320, 145);
        _statusCaption.Name = "_statusCaption";
        _statusCaption.Size = new Size(40, 15);
        _statusCaption.TabIndex = 9;
        _statusCaption.Text = "Status";
        // 
        // _statusValue
        // 
        _statusValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _statusValue.ForeColor = Color.White;
        _statusValue.Location = new Point(320, 167);
        _statusValue.Name = "_statusValue";
        _statusValue.Size = new Size(150, 25);
        _statusValue.TabIndex = 10;
        _statusValue.Text = "-";
        // 
        // _featuredCaption
        // 
        _featuredCaption.AutoSize = true;
        _featuredCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _featuredCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _featuredCaption.Location = new Point(485, 145);
        _featuredCaption.Name = "_featuredCaption";
        _featuredCaption.Size = new Size(57, 15);
        _featuredCaption.TabIndex = 11;
        _featuredCaption.Text = "Destaque";
        // 
        // _featuredValue
        // 
        _featuredValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _featuredValue.ForeColor = Color.White;
        _featuredValue.Location = new Point(485, 167);
        _featuredValue.Name = "_featuredValue";
        _featuredValue.Size = new Size(100, 25);
        _featuredValue.TabIndex = 12;
        _featuredValue.Text = "-";
        // 
        // _descriptionCaption
        // 
        _descriptionCaption.AutoSize = true;
        _descriptionCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _descriptionCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _descriptionCaption.Location = new Point(18, 228);
        _descriptionCaption.Name = "_descriptionCaption";
        _descriptionCaption.Size = new Size(59, 15);
        _descriptionCaption.TabIndex = 13;
        _descriptionCaption.Text = "Descrição";
        // 
        // _descriptionValue
        // 
        _descriptionValue.AutoEllipsis = true;
        _descriptionValue.Font = new Font("Segoe UI", 9F);
        _descriptionValue.ForeColor = Color.FromArgb(235, 222, 211);
        _descriptionValue.Location = new Point(18, 250);
        _descriptionValue.Name = "_descriptionValue";
        _descriptionValue.Size = new Size(600, 70);
        _descriptionValue.TabIndex = 14;
        // 
        // _imageCaption
        // 
        _imageCaption.AutoSize = true;
        _imageCaption.Font = new Font("Segoe UI Semibold", 8.5F);
        _imageCaption.ForeColor = Color.FromArgb(214, 191, 172);
        _imageCaption.Location = new Point(18, 332);
        _imageCaption.Name = "_imageCaption";
        _imageCaption.Size = new Size(52, 15);
        _imageCaption.TabIndex = 15;
        _imageCaption.Text = "Imagem";
        // 
        // _imageValue
        // 
        _imageValue.AutoEllipsis = true;
        _imageValue.Font = new Font("Segoe UI", 8F);
        _imageValue.ForeColor = Color.FromArgb(190, 175, 160);
        _imageValue.Location = new Point(18, 354);
        _imageValue.Name = "_imageValue";
        _imageValue.Size = new Size(600, 40);
        _imageValue.TabIndex = 16;
        // 
        // _closeButton
        // 
        _closeButton.BackColor = Color.Transparent;
        _closeButton.BorderRadius = 8;
        _closeButton.CustomizableEdges = customizableEdges1;
        _closeButton.FillColor = Color.FromArgb(120, 75, 43);
        _closeButton.Font = new Font("Segoe UI Semibold", 9F);
        _closeButton.ForeColor = Color.White;
        _closeButton.Location = new Point(510, 410);
        _closeButton.Name = "_closeButton";
        _closeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _closeButton.Size = new Size(108, 38);
        _closeButton.TabIndex = 17;
        _closeButton.Text = "Fechar";
        // 
        // ProductDetailsDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(43, 26, 18);
        ClientSize = new Size(640, 470);
        Controls.Add(_rootPanel);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ProductDetailsDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Visualizar Produto";
        _rootPanel.ResumeLayout(false);
        _rootPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_pictureBox).EndInit();
        ResumeLayout(false);
    }

}
