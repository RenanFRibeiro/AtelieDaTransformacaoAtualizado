using AtelieDaTransformacao.Desktop.Themes;
namespace AtelieDaTransformacao.Desktop.Forms;
public partial class SimpleUserDialog : Form
{
    public string Email => _emailTextBox.Text.Trim(); public string Password => _passwordTextBox.Text;
    public SimpleUserDialog(){InitializeComponent();_createButton.Click+=(_,_)=>Create();_passwordToggleButton.Click+=(_,_)=>TogglePassword();}
    private void TogglePassword(){try{if(_passwordTextBox.PasswordChar=='•'){_passwordTextBox.PasswordChar='\0';_passwordToggleButton.Text="🙈";}else{_passwordTextBox.PasswordChar='•';_passwordToggleButton.Text="👁️";}}catch{}}
    private void Create(){if(!Email.Contains('@')){MessageBox.Show(this,"Informe um e-mail válido.","Validação",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}if(Password.Length<6){MessageBox.Show(this,"A senha precisa ter pelo menos 6 caracteres.","Validação",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}DialogResult=DialogResult.OK;}
}
