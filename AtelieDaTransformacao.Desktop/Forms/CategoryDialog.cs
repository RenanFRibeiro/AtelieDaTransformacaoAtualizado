using AtelieDaTransformacao.Desktop.DTOs;

namespace AtelieDaTransformacao.Desktop.Forms;

public partial class CategoryDialog : Form
{
    public CategoryWriteDto? Result { get; private set; }
    private readonly CategoryDto? _item;

    public CategoryDialog() : this(null)
    {
    }

    public CategoryDialog(CategoryDto? item)
    {
        _item = item;
        InitializeComponent();

        _nameTextBox.Text = item?.Name ?? string.Empty;
        _descriptionTextBox.Text = item?.Description ?? string.Empty;

        _saveButton.Click += SaveButton_Click;
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        Save();
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(_nameTextBox.Text))
        {
            MessageBox.Show(
                this,
                "Informe o nome.",
                "Validação",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        Result = new CategoryWriteDto
        {
            Name = _nameTextBox.Text.Trim(),
            Description = _descriptionTextBox.Text.Trim()
        };

        DialogResult = DialogResult.OK;
    }
}
