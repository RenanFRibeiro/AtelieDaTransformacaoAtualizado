using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;

partial class ProductDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Guna.UI2.WinForms.Guna2BorderlessForm _borderlessForm = null!;
    private Guna.UI2.WinForms.Guna2DragControl _dragControl = null!;
    private Guna.UI2.WinForms.Guna2Panel _headerPanel = null!;
    private Label _formTitleLabel = null!;
    private Guna.UI2.WinForms.Guna2Panel _bodyPanel = null!;
    private Guna.UI2.WinForms.Guna2TextBox _titleTextBox = null!;
    private Guna.UI2.WinForms.Guna2TextBox _descriptionTextBox = null!;
    private Guna.UI2.WinForms.Guna2NumericUpDown _priceNumeric = null!;
    private Guna.UI2.WinForms.Guna2NumericUpDown _stockNumeric = null!;
    private Guna.UI2.WinForms.Guna2ComboBox _categoryComboBox = null!;
    private Guna.UI2.WinForms.Guna2CheckBox _featuredCheckBox = null!;
    private Guna.UI2.WinForms.Guna2Button _cancelButton = null!;
    private Guna.UI2.WinForms.Guna2Button _saveButton = null!;
    private Guna.UI2.WinForms.Guna2Button _chooseImageButton = null!;
    private Guna.UI2.WinForms.Guna2Button _clearImageButton = null!;
    private Guna.UI2.WinForms.Guna2TextBox _imageUrlTextBox = null!;
    private PictureBox _imagePreviewPictureBox = null!;
    private Label _imageFileLabel = null!;
    private Label _nameCaption = null!;
    private Label _descriptionCaption = null!;
    private Label _priceCaption = null!;
    private Label _stockCaption = null!;
    private Label _categoryCaption = null!;
    private Label _imageCaption = null!;
    private Label _urlCaption = null!;
    private Label _previewCaption = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _previewImage?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var brown = Color.FromArgb(145, 98, 57);
        var darkBrown = Color.FromArgb(88, 52, 27);
        var border = Color.FromArgb(226, 229, 236);
        var text = Color.FromArgb(30, 34, 43);

        _borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
        _dragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
        _headerPanel = new Guna.UI2.WinForms.Guna2Panel();
        _formTitleLabel = new Label();
        _bodyPanel = new Guna.UI2.WinForms.Guna2Panel();
        _titleTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _descriptionTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _priceNumeric = new Guna.UI2.WinForms.Guna2NumericUpDown();
        _stockNumeric = new Guna.UI2.WinForms.Guna2NumericUpDown();
        _categoryComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
        _featuredCheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
        _chooseImageButton = new Guna.UI2.WinForms.Guna2Button();
        _clearImageButton = new Guna.UI2.WinForms.Guna2Button();
        _imageUrlTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _imagePreviewPictureBox = new PictureBox();
        _imageFileLabel = new Label();
        _cancelButton = new Guna.UI2.WinForms.Guna2Button();
        _saveButton = new Guna.UI2.WinForms.Guna2Button();
        _nameCaption = new Label();
        _descriptionCaption = new Label();
        _priceCaption = new Label();
        _stockCaption = new Label();
        _categoryCaption = new Label();
        _imageCaption = new Label();
        _urlCaption = new Label();
        _previewCaption = new Label();

        _headerPanel.SuspendLayout();
        _bodyPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_priceNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_stockNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_imagePreviewPictureBox).BeginInit();
        SuspendLayout();

        _borderlessForm.BorderRadius = 14;
        _borderlessForm.ContainerControl = this;
        _borderlessForm.TransparentWhileDrag = true;
        _dragControl.TargetControl = _headerPanel;
        _dragControl.UseTransparentDrag = true;

        _headerPanel.Controls.Add(_formTitleLabel);
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.FillColor = Color.White;
        _headerPanel.Size = new Size(820, 70);
        _formTitleLabel.AutoSize = true;
        _formTitleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _formTitleLabel.ForeColor = text;
        _formTitleLabel.Location = new Point(24, 19);
        _formTitleLabel.Text = "Produto";

        _bodyPanel.Dock = DockStyle.Fill;
        _bodyPanel.FillColor = Color.White;
        _bodyPanel.Padding = new Padding(24);

        ConfigureCaption(_nameCaption, "Nome", 24, 14);
        ConfigureText(_titleTextBox, 24, 34, 500, 42, "Nome do produto");

        ConfigureCaption(_descriptionCaption, "Descrição", 24, 82);
        _descriptionTextBox.Location = new Point(24, 102);
        _descriptionTextBox.Size = new Size(500, 72);
        _descriptionTextBox.BorderRadius = 9;
        _descriptionTextBox.BorderColor = border;
        _descriptionTextBox.FocusedState.BorderColor = darkBrown;
        _descriptionTextBox.Multiline = true;
        _descriptionTextBox.Font = new Font("Segoe UI", 9.5F);
        _descriptionTextBox.PlaceholderText = "Descrição do produto";

        ConfigureCaption(_priceCaption, "Preço", 24, 182);
        ConfigureNumeric(_priceNumeric, 24, 202, 240);
        ConfigureCaption(_stockCaption, "Estoque", 284, 182);
        ConfigureNumeric(_stockNumeric, 284, 202, 240);
        _stockNumeric.DecimalPlaces = 0;
        _stockNumeric.Increment = 1;
        _stockNumeric.Maximum = 100000;
        _stockNumeric.Minimum = 0;
        _stockNumeric.Value = 0;

        ConfigureCaption(_categoryCaption, "Categoria", 24, 252);
        _categoryComboBox.Location = new Point(24, 272);
        _categoryComboBox.Size = new Size(500, 38);
        _categoryComboBox.BorderRadius = 9;
        _categoryComboBox.BorderColor = border;
        _categoryComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        _categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _categoryComboBox.Font = new Font("Segoe UI", 9.5F);
        _categoryComboBox.ItemHeight = 30;

        _featuredCheckBox.AutoSize = true;
        _featuredCheckBox.CheckedState.FillColor = darkBrown;
        _featuredCheckBox.Font = new Font("Segoe UI", 9F);
        _featuredCheckBox.Location = new Point(24, 323);
        _featuredCheckBox.Text = "Produto em destaque";

        ConfigureCaption(_imageCaption, "Imagem do produto", 555, 14);
        _imagePreviewPictureBox.Location = new Point(555, 34);
        _imagePreviewPictureBox.Size = new Size(220, 150);
        _imagePreviewPictureBox.BorderStyle = BorderStyle.FixedSingle;
        _imagePreviewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        _imagePreviewPictureBox.BackColor = Color.FromArgb(248, 248, 248);

        _chooseImageButton.Location = new Point(555, 193);
        _chooseImageButton.Size = new Size(140, 36);
        _chooseImageButton.BorderRadius = 8;
        _chooseImageButton.FillColor = brown;
        _chooseImageButton.ForeColor = Color.White;
        _chooseImageButton.Font = new Font("Segoe UI Semibold", 8.5F);
        _chooseImageButton.Text = "📁 Escolher imagem";

        _clearImageButton.Location = new Point(705, 193);
        _clearImageButton.Size = new Size(70, 36);
        _clearImageButton.BorderRadius = 8;
        _clearImageButton.FillColor = Color.FromArgb(241, 243, 248);
        _clearImageButton.ForeColor = text;
        _clearImageButton.Font = new Font("Segoe UI Semibold", 8.5F);
        _clearImageButton.Text = "Limpar";

        _imageFileLabel.AutoEllipsis = true;
        _imageFileLabel.Font = new Font("Segoe UI", 8F);
        _imageFileLabel.ForeColor = Color.Gray;
        _imageFileLabel.Location = new Point(555, 234);
        _imageFileLabel.Size = new Size(220, 20);
        _imageFileLabel.Text = "Nenhuma imagem selecionada";

        ConfigureCaption(_urlCaption, "Ou coloque o link da imagem", 555, 258);
        _imageUrlTextBox.Location = new Point(555, 278);
        _imageUrlTextBox.Size = new Size(220, 42);
        _imageUrlTextBox.BorderRadius = 9;
        _imageUrlTextBox.BorderColor = border;
        _imageUrlTextBox.FocusedState.BorderColor = darkBrown;
        _imageUrlTextBox.Font = new Font("Segoe UI", 8.5F);
        _imageUrlTextBox.PlaceholderText = "https://...";

        _previewCaption.AutoSize = true;
        _previewCaption.Font = new Font("Segoe UI", 7.5F);
        _previewCaption.ForeColor = Color.Gray;
        _previewCaption.Location = new Point(555, 326);
        _previewCaption.Text = "Upload e URL são alternativas. Upload tem prioridade.";

        _cancelButton.Location = new Point(570, 345);
        _cancelButton.Size = new Size(98, 40);
        _cancelButton.BorderRadius = 9;
        _cancelButton.FillColor = Color.FromArgb(241, 243, 248);
        _cancelButton.ForeColor = text;
        _cancelButton.Font = new Font("Segoe UI Semibold", 9F);
        _cancelButton.Text = "Cancelar";
        _cancelButton.DialogResult = DialogResult.Cancel;

        _saveButton.Location = new Point(675, 345);
        _saveButton.Size = new Size(100, 40);
        _saveButton.BorderRadius = 9;
        _saveButton.FillColor = brown;
        _saveButton.ForeColor = Color.White;
        _saveButton.Font = new Font("Segoe UI Semibold", 9F);
        _saveButton.Text = "Salvar";

        _bodyPanel.Controls.AddRange(new Control[]
        {
            _nameCaption, _titleTextBox,
            _descriptionCaption, _descriptionTextBox,
            _priceCaption, _priceNumeric,
            _stockCaption, _stockNumeric,
            _categoryCaption, _categoryComboBox,
            _featuredCheckBox,
            _imageCaption, _imagePreviewPictureBox,
            _chooseImageButton, _clearImageButton,
            _imageFileLabel, _urlCaption, _imageUrlTextBox,
            _previewCaption, _cancelButton, _saveButton
        });

        AcceptButton = _saveButton;
        CancelButton = _cancelButton;
        BackColor = Color.FromArgb(245, 247, 251);
        ClientSize = new Size(820, 470);
        Controls.Add(_bodyPanel);
        Controls.Add(_headerPanel);
        FormBorderStyle = FormBorderStyle.None;
        Name = "ProductDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Produto";

        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        _bodyPanel.ResumeLayout(false);
        _bodyPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_priceNumeric).EndInit();
        ((System.ComponentModel.ISupportInitialize)_stockNumeric).EndInit();
        ((System.ComponentModel.ISupportInitialize)_imagePreviewPictureBox).EndInit();
        ResumeLayout(false);
    }

    private static void ConfigureCaption(Label label, string value, int x, int y)
    {
        label.AutoSize = true;
        label.Font = new Font("Segoe UI Semibold", 8.5F);
        label.ForeColor = LibraryTheme.Text;
        label.Location = new Point(x, y);
        label.Text = value;
    }

    private static void ConfigureText(Guna.UI2.WinForms.Guna2TextBox box, int x, int y, int width, int height, string placeholder)
    {
        box.Location = new Point(x, y);
        box.Size = new Size(width, height);
        box.BorderRadius = 9;
        box.BorderColor = LibraryTheme.Border;
        box.FocusedState.BorderColor = LibraryTheme.Accent;
        box.PlaceholderText = placeholder;
        box.Font = new Font("Segoe UI", 9.5F);
    }

    private static void ConfigureNumeric(Guna.UI2.WinForms.Guna2NumericUpDown box, int x, int y, int width)
    {
        box.Location = new Point(x, y);
        box.Size = new Size(width, 42);
        box.BorderRadius = 9;
        box.BorderColor = LibraryTheme.Border;
        box.Font = new Font("Segoe UI", 9.5F);
        box.Maximum = 1000000;
        box.Minimum = 0.01M;
        box.DecimalPlaces = 2;
        box.Increment = 0.50M;
        box.Value = 0.01M;
        box.UpDownButtonFillColor = LibraryTheme.Accent;
    }
}
