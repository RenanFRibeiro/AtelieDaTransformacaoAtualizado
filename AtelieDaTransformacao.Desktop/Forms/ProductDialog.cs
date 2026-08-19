using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;

public partial class ProductDialog : Form
{
    public ProductWriteDto? Result { get; private set; }
    private readonly ProductDto? _item;
    private readonly IEnumerable<CategoryDto> _categories;

    public ProductDialog(ProductDto? item, IEnumerable<CategoryDto> categories)
    {
        _item = item;
        _categories = categories;
        InitializeComponent();
        Load += (_, _) => LoadValues();
        _saveButton.Click += (_, _) => Save();
    }

    private void LoadValues()
    {
        _categoryComboBox.DataSource = _categories.ToList();
        _categoryComboBox.DisplayMember = "Name";
        _categoryComboBox.ValueMember = "Id";
        _titleTextBox.Text = _item?.Title ?? "";
        _descriptionTextBox.Text = _item?.Description ?? "";
        _priceNumeric.Value = _item is null ? 0.01M : Math.Max(_priceNumeric.Minimum, Math.Min(_priceNumeric.Maximum, _item.Price));
        _stockNumeric.Value = _item?.StockQuantity ?? 0;
        _featuredCheckBox.Checked = _item?.IsFeatured ?? false;
        if (_item is not null) _categoryComboBox.SelectedValue = _item.CategoryId;
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(_titleTextBox.Text)) { MessageBox.Show(this, "O nome é obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        if (_priceNumeric.Value <= 0) { MessageBox.Show(this, "O preço deve ser maior que zero.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        if (_categoryComboBox.SelectedValue is null || Convert.ToInt32(_categoryComboBox.SelectedValue) <= 0) { MessageBox.Show(this, "Selecione uma categoria.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        Result = new ProductWriteDto { Title = _titleTextBox.Text.Trim(), Description = _descriptionTextBox.Text.Trim(), Price = _priceNumeric.Value, StockQuantity = (int)_stockNumeric.Value, CategoryId = Convert.ToInt32(_categoryComboBox.SelectedValue), IsFeatured = _featuredCheckBox.Checked };
        DialogResult = DialogResult.OK;
    }
}
