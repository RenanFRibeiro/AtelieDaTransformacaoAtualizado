using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Forms;
using AtelieDaTransformacao.Desktop.Services;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class UsersUserControl : UserControl
{
    private readonly UsersApiService _service = new();
    private readonly AuthApiService _auth = new();
    private List<UserSummaryDto> _items = new();

    public UsersUserControl()
    {
        InitializeComponent();

        _newButton.Click += async (_, _) => await CreateAsync();
        _refreshButton.Click += async (_, _) => await LoadAsync();
        _deleteButton.Click += async (_, _) => await DeactivateSelectedAsync();
        _activationButton.Click += async (_, _) => await ActivateSelectedAsync();

        Load += async (_, _) => await LoadAsync();
    }

    private async Task LoadAsync()
    {
        try
        {
            _items = await _service.GetAllAsync() ?? new();

            _grid.DataSource = _items
                .Select(x => new
                {
                    x.Id,
                    Usuário = x.Email,
                    Perfil = x.Roles.Count == 0 ? "Usuário" : string.Join(", ", x.Roles),
                    Ativo = x.IsActive ? "Sim" : "Não"
                })
                .ToList();

            if (_grid.Columns["Id"] is not null)
                _grid.Columns["Id"].Visible = false;

            var ativos = _items.Count(x => x.IsActive);
            var inativos = _items.Count - ativos;

            _activeCountLabel.Text = ativos.ToString();
            _inactiveCountLabel.Text = inativos.ToString();
            _countLabel.Text = $"{_items.Count} usuário(s)";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task CreateAsync()
    {
        using var dialog = new SimpleUserDialog();

        if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
            return;

        try
        {
            await _auth.RegisterAsync(new RegisterRequestDto
            {
                Email = dialog.Email,
                Password = dialog.Password,
                ConfirmPassword = dialog.Password
            });

            await LoadAsync();

            MessageBox.Show(
                this,
                "Usuário criado com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                "Não foi possível criar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private async Task DeactivateSelectedAsync()
    {
        if (_grid.CurrentRow is null)
            return;

        var id = _grid.CurrentRow.Cells["Id"].Value?.ToString();
        var email = _grid.CurrentRow.Cells["Usuário"].Value?.ToString();

        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(email))
            return;

        if (string.Equals(email, SessionManager.Email, StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(this, "Você não pode desativar o próprio usuário.");
            return;
        }

        if (MessageBox.Show(
                this,
                $"Desativar {email}?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        try
        {
            await _service.DeactivateAsync(id);
            await LoadAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task ActivateSelectedAsync()
    {
        if (_grid.CurrentRow is null)
            return;

        var id = _grid.CurrentRow.Cells["Id"].Value?.ToString();
        var email = _grid.CurrentRow.Cells["Usuário"].Value?.ToString();

        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(email))
            return;

        if (MessageBox.Show(
                this,
                $"Ativar {email}?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            await _service.ActivateAsync(id);
            await LoadAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}