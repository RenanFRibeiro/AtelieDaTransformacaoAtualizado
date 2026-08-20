using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Services;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;

public partial class LoginForm : Form
{
    private readonly AuthApiService _auth = new();

    public LoginForm()
    {
        InitializeComponent();
        _loginButton.Click += async (_, _) => await LoginAsync();
        Load += async (_, _) => await CheckApiAsync();
        AcceptButton = _loginButton;
        _passwordToggleButton.Click += (_, _) => TogglePassword();
    }

    private void TogglePassword()
    {
        try
        {
            if (_passwordTextBox.PasswordChar == '•')
            {
                _passwordTextBox.PasswordChar = '\0';
                _passwordToggleButton.Text = "🙈"; // closed eye icon
            }
            else
            {
                _passwordTextBox.PasswordChar = '•';
                _passwordToggleButton.Text = "👁️"; // eye icon
            }
        }
        catch { }
    }

    private async Task CheckApiAsync()
    {
        try
        {
            await HttpClientHelper.GetAsync<List<ProductDto>>("products");
            _apiStatusLabel.Text = "API online";
            _apiStatusLabel.ForeColor = LibraryTheme.Success;
        }
        catch
        {
            _apiStatusLabel.Text = $"API offline • {AppConfig.ApiBaseUrl}";
            _apiStatusLabel.ForeColor = LibraryTheme.Danger;
        }
    }

    private async Task LoginAsync()
    {
        if (!ValidateFields()) return;
        try
        {
            ToggleInputs(false);
            _statusLabel.Text = "Autenticando...";
            _statusLabel.ForeColor = LibraryTheme.Muted;
            var response = await _auth.LoginAsync(_emailTextBox.Text.Trim(), _passwordTextBox.Text);
            if (response is null || string.IsNullOrWhiteSpace(response.Token))
                throw new InvalidOperationException("A API não retornou um token válido.");

            SessionManager.Start(response.Token, response.User.Email, response.User.Roles);
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            _statusLabel.Text = ex.Message;
            _statusLabel.ForeColor = LibraryTheme.Danger;
            ToggleInputs(true);
        }
    }

    private bool ValidateFields()
    {
        if (!_emailTextBox.Text.Contains('@'))
        {
            _statusLabel.Text = "Informe um e-mail válido.";
            _statusLabel.ForeColor = LibraryTheme.Danger;
            _emailTextBox.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(_passwordTextBox.Text))
        {
            _statusLabel.Text = "Informe sua senha.";
            _statusLabel.ForeColor = LibraryTheme.Danger;
            _passwordTextBox.Focus();
            return false;
        }
        return true;
    }

    private void ToggleInputs(bool enabled)
    {
        _emailTextBox.Enabled = enabled;
        _passwordTextBox.Enabled = enabled;
        _loginButton.Enabled = enabled;
    }

    private void LoginForm_Load(object sender, EventArgs e)
    {
        _emailTextBox.Text = "Admin@atelie.com";
        _passwordTextBox.Text = "Admin@Atelie123";
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.Application.Exit();
    }

    private void btnMinimize_Click(object sender, EventArgs e)
    {

    }
}
