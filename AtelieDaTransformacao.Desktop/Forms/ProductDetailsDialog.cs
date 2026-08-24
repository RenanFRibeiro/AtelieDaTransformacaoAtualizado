using System.Drawing;
using System.Globalization;
using AtelieDaTransformacao.Desktop.DTOs;

namespace AtelieDaTransformacao.Desktop.Forms;

public sealed partial class ProductDetailsDialog : Form
{
    private readonly ProductDto _product;

    public ProductDetailsDialog(ProductDto product)
    {
        _product = product ?? throw new ArgumentNullException(nameof(product));
        InitializeComponent();
        LoadProduct();
    }

    private void LoadProduct()
    {
        _titleLabel.Text = _product.Title;
        _categoryValue.Text = string.IsNullOrWhiteSpace(_product.CategoryName) ? "Sem categoria" : _product.CategoryName;
        _priceValue.Text = _product.Price.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
        _stockValue.Text = _product.StockQuantity.ToString();
        _statusValue.Text = _product.StockQuantity == 0 ? "Sem estoque" : _product.StockQuantity <= 5 ? "Baixo" : "Disponível";
        _featuredValue.Text = _product.IsFeatured ? "Sim" : "Não";
        _descriptionValue.Text = string.IsNullOrWhiteSpace(_product.Description) ? "Sem descrição cadastrada." : _product.Description;
        _imageValue.Text = string.IsNullOrWhiteSpace(_product.Image) ? "Sem imagem cadastrada." : _product.Image;

        _statusValue.ForeColor = _product.StockQuantity == 0
            ? Color.FromArgb(192, 0, 0)
            : _product.StockQuantity <= 5
                ? Color.FromArgb(180, 120, 15)
                : Color.FromArgb(35, 145, 55);

        if (!string.IsNullOrWhiteSpace(_product.Image))
        {
            try
            {
                if (Uri.TryCreate(_product.Image, UriKind.Absolute, out var uri) &&
                    (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                    _pictureBox.LoadAsync(_product.Image);
                else if (File.Exists(_product.Image))
                    _pictureBox.Image = Image.FromFile(_product.Image);
            }
            catch
            {
                _pictureBox.Image = null;
            }
        }
    }
}
