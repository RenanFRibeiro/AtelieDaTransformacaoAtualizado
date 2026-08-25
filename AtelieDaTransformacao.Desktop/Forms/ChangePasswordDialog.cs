using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Services;

namespace AtelieDaTransformacao.Desktop.Forms;

public sealed partial class ChangePasswordDialog : Form
{
    private readonly AuthApiService _auth = new();

    public ChangePasswordDialog()
    {
        InitializeComponent();
        AcceptButton = _changeButton;
        CancelButton = _cancelButton;

        _currentPasswordToggleButton.Click += (_, _) => TogglePasswordVisibility(_currentPasswordTextBox, _currentPasswordToggleButton);
        _newPasswordToggleButton.Click += (_, _) => TogglePasswordVisibility(_newPasswordTextBox, _newPasswordToggleButton);
        _confirmPasswordToggleButton.Click += (_, _) => TogglePasswordVisibility(_confirmPasswordTextBox, _confirmPasswordToggleButton);
    }

    private async void ChangeButton_Click(object? sender, EventArgs e)
    {
        var currentPassword = _currentPasswordTextBox.Text;
        var newPassword = _newPasswordTextBox.Text;
        var confirmPassword = _confirmPasswordTextBox.Text;

        if (string.IsNullOrWhiteSpace(currentPassword))
        {
            MessageBox.Show(this, "Informe a senha atual.", "Alterar Senha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _currentPasswordTextBox.Focus();
            return;
        }

        if (newPassword.Length < 6)
        {
            MessageBox.Show(this, "A nova senha deve possuir pelo menos 6 caracteres.", "Alterar Senha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _newPasswordTextBox.Focus();
            return;
        }

        if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
        {
            MessageBox.Show(this, "A confirmação da nova senha não coincide.", "Alterar Senha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _confirmPasswordTextBox.Focus();
            return;
        }

        _changeButton.Enabled = false;
        _cancelButton.Enabled = false;

        try
        {
            // Mantém o e-mail atual e usa o endpoint existente apenas para a troca de senha.
            var result = await _auth.UpdateProfileAsync(new UpdateProfileRequestDto
            {
                Email = SessionManager.Email ?? string.Empty,
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            });

            if (result is null)
                throw new InvalidOperationException("A API não retornou os dados atualizados.");

            SessionManager.UpdateProfile(result.Email, result.Roles);

            MessageBox.Show(this, "Senha alterada com sucesso.", "Alterar Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Não foi possível alterar a senha", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _changeButton.Enabled = true;
            _cancelButton.Enabled = true;
        }
    }

    private static void TogglePasswordVisibility(Guna.UI2.WinForms.Guna2TextBox textBox, Guna.UI2.WinForms.Guna2Button toggleButton)
    {
        if (textBox.PasswordChar == '●')
        {
            textBox.PasswordChar = '\0';
            toggleButton.Text = "🙈";
        }
        else
        {
            textBox.PasswordChar = '●';
            toggleButton.Text = "👁️";
        }
    }

    private void CancelButton_Click(object? sender, EventArgs e) => DialogResult = DialogResult.Cancel;

    private void btnClose_Click(object? sender, EventArgs e) => Close();
}
