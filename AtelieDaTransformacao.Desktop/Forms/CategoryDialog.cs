using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Themes;
namespace AtelieDaTransformacao.Desktop.Forms;
public partial class CategoryDialog : Form
{
    public CategoryWriteDto? Result { get; private set; }
    private readonly CategoryDto? _item;
    public CategoryDialog(CategoryDto? item){_item=item;InitializeComponent();_nameTextBox.Text=item?.Name??"";_descriptionTextBox.Text=item?.Description??"";_saveButton.Click+=(_,_)=>Save();}
    private void Save(){if(string.IsNullOrWhiteSpace(_nameTextBox.Text)){MessageBox.Show(this,"Informe o nome.","Validação",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}Result=new CategoryWriteDto{Name=_nameTextBox.Text.Trim(),Description=_descriptionTextBox.Text.Trim()};DialogResult=DialogResult.OK;}
}
