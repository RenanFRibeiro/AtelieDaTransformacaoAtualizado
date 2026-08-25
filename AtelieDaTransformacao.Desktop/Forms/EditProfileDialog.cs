using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Services;

namespace AtelieDaTransformacao.Desktop.Forms;

public sealed partial class EditProfileDialog : Form
{
    private readonly AuthApiService _auth = new();

    public EditProfileDialog()
    {
        InitializeComponent();
        _emailTextBox.Text = SessionManager.Email ?? string.Empty;
        AcceptButton = _saveButton;
        CancelButton = _cancelButton;
    }

    private async void SaveButton_Click(object? sender, EventArgs e)
    {
        var email = _emailTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show(this, "Informe o e-mail.", "Editar Perfil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _emailTextBox.Focus();
            return;
        }

        _saveButton.Enabled = false;
        _cancelButton.Enabled = false;

        try
        {
            var result = await _auth.UpdateEmailAsync(new UpdateEmailRequestDto { Email = email });
            if (result is null) throw new InvalidOperationException("A API não retornou os dados atualizados.");

            SessionManager.UpdateProfile(result.Email, result.Roles);
            MessageBox.Show(this, "E-mail atualizado com sucesso.", "Editar Perfil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Não foi possível atualizar o perfil", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _saveButton.Enabled = true;
            _cancelButton.Enabled = true;
        }
    }

    private void CancelButton_Click(object? sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    private void btnClose_Click(object? sender, EventArgs e) => Close();
}
