namespace AtelieDaTransformacao.Desktop.Forms;

partial class EditProfileDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Label _titleLabel = null!;
    private Label _subtitleLabel = null!;
    private Label _emailCaptionLabel = null!;
    private Label _currentPasswordCaptionLabel = null!;
    private Label _newPasswordCaptionLabel = null!;
    private Guna.UI2.WinForms.Guna2TextBox _emailTextBox = null!;
    private Guna.UI2.WinForms.Guna2TextBox _currentPasswordTextBox = null!;
    private Guna.UI2.WinForms.Guna2TextBox _newPasswordTextBox = null!;
    private Guna.UI2.WinForms.Guna2Button _saveButton = null!;
    private Guna.UI2.WinForms.Guna2Button _cancelButton = null!;
    private Guna.UI2.WinForms.Guna2Button _currentPasswordToggleButton = null!;
    private Guna.UI2.WinForms.Guna2Button _newPasswordToggleButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _titleLabel = new Label();
        _subtitleLabel = new Label();
        _emailCaptionLabel = new Label();
        _currentPasswordCaptionLabel = new Label();
        _newPasswordCaptionLabel = new Label();
        _emailTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _currentPasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _newPasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _saveButton = new Guna.UI2.WinForms.Guna2Button();
        _cancelButton = new Guna.UI2.WinForms.Guna2Button();
        _currentPasswordToggleButton = new Guna.UI2.WinForms.Guna2Button();
        _newPasswordToggleButton = new Guna.UI2.WinForms.Guna2Button();
        btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
        SuspendLayout();
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.FromArgb(43, 26, 18);
        _titleLabel.Location = new Point(28, 22);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(149, 32);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Editar Perfil";
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.AutoSize = true;
        _subtitleLabel.Font = new Font("Segoe UI", 9F);
        _subtitleLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _subtitleLabel.Location = new Point(30, 59);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(242, 15);
        _subtitleLabel.TabIndex = 1;
        _subtitleLabel.Text = "Altere seu e-mail ou defina uma nova senha.";
        // 
        // _emailCaptionLabel
        // 
        _emailCaptionLabel.AutoSize = true;
        _emailCaptionLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        _emailCaptionLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _emailCaptionLabel.Location = new Point(30, 82);
        _emailCaptionLabel.Name = "_emailCaptionLabel";
        _emailCaptionLabel.Size = new Size(47, 15);
        _emailCaptionLabel.TabIndex = 2;
        _emailCaptionLabel.Text = "E-MAIL";
        // 
        // _currentPasswordCaptionLabel
        // 
        _currentPasswordCaptionLabel.AutoSize = true;
        _currentPasswordCaptionLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        _currentPasswordCaptionLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _currentPasswordCaptionLabel.Location = new Point(30, 142);
        _currentPasswordCaptionLabel.Name = "_currentPasswordCaptionLabel";
        _currentPasswordCaptionLabel.Size = new Size(86, 15);
        _currentPasswordCaptionLabel.TabIndex = 3;
        _currentPasswordCaptionLabel.Text = "SENHA ATUAL";
        // 
        // _newPasswordCaptionLabel
        // 
        _newPasswordCaptionLabel.AutoSize = true;
        _newPasswordCaptionLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        _newPasswordCaptionLabel.ForeColor = Color.FromArgb(113, 120, 135);
        _newPasswordCaptionLabel.Location = new Point(30, 202);
        _newPasswordCaptionLabel.Name = "_newPasswordCaptionLabel";
        _newPasswordCaptionLabel.Size = new Size(82, 15);
        _newPasswordCaptionLabel.TabIndex = 4;
        _newPasswordCaptionLabel.Text = "NOVA SENHA";
        // 
        // _emailTextBox
        // 
        _emailTextBox.BorderColor = Color.FromArgb(215, 218, 225);
        _emailTextBox.BorderRadius = 8;
        _emailTextBox.CustomizableEdges = customizableEdges1;
        _emailTextBox.DefaultText = "";
        _emailTextBox.Font = new Font("Segoe UI", 9.5F);
        _emailTextBox.Location = new Point(30, 98);
        _emailTextBox.Name = "_emailTextBox";
        _emailTextBox.PlaceholderText = "E-mail";
        _emailTextBox.SelectedText = "";
        _emailTextBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _emailTextBox.Size = new Size(400, 38);
        _emailTextBox.TabIndex = 5;
        // 
        // _currentPasswordTextBox
        // 
        _currentPasswordTextBox.BorderColor = Color.FromArgb(215, 218, 225);
        _currentPasswordTextBox.BorderRadius = 8;
        _currentPasswordTextBox.CustomizableEdges = customizableEdges3;
        _currentPasswordTextBox.DefaultText = "";
        _currentPasswordTextBox.Font = new Font("Segoe UI", 9.5F);
        _currentPasswordTextBox.Location = new Point(30, 158);
        _currentPasswordTextBox.Name = "_currentPasswordTextBox";
        _currentPasswordTextBox.PasswordChar = '●';
        _currentPasswordTextBox.PlaceholderText = "Senha atual (necessária para trocar a senha)";
        _currentPasswordTextBox.SelectedText = "";
        _currentPasswordTextBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _currentPasswordTextBox.Size = new Size(400, 38);
        _currentPasswordTextBox.TabIndex = 6;
        // 
        // _newPasswordTextBox
        // 
        _newPasswordTextBox.BorderColor = Color.FromArgb(215, 218, 225);
        _newPasswordTextBox.BorderRadius = 8;
        _newPasswordTextBox.CustomizableEdges = customizableEdges5;
        _newPasswordTextBox.DefaultText = "";
        _newPasswordTextBox.Font = new Font("Segoe UI", 9.5F);
        _newPasswordTextBox.Location = new Point(30, 218);
        _newPasswordTextBox.Name = "_newPasswordTextBox";
        _newPasswordTextBox.PasswordChar = '●';
        _newPasswordTextBox.PlaceholderText = "Nova senha (deixe vazio para não alterar)";
        _newPasswordTextBox.SelectedText = "";
        _newPasswordTextBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _newPasswordTextBox.Size = new Size(400, 38);
        _newPasswordTextBox.TabIndex = 7;
        // 
        // _saveButton
        // 
        _saveButton.BorderRadius = 9;
        _saveButton.CustomizableEdges = customizableEdges7;
        _saveButton.FillColor = Color.FromArgb(88, 52, 27);
        _saveButton.Font = new Font("Segoe UI Semibold", 9F);
        _saveButton.ForeColor = Color.White;
        _saveButton.Location = new Point(343, 293);
        _saveButton.Name = "_saveButton";
        _saveButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _saveButton.Size = new Size(96, 40);
        _saveButton.TabIndex = 8;
        _saveButton.Text = "💾 Salvar ";
        _saveButton.Click += SaveButton_Click;
        // 
        // _cancelButton
        // 
        _cancelButton.BorderRadius = 9;
        _cancelButton.CustomizableEdges = customizableEdges9;
        _cancelButton.FillColor = Color.Gray;
        _cancelButton.Font = new Font("Segoe UI Semibold", 9F);
        _cancelButton.ForeColor = Color.White;
        _cancelButton.Location = new Point(242, 293);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.PressedColor = Color.White;
        _cancelButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _cancelButton.Size = new Size(95, 40);
        _cancelButton.TabIndex = 9;
        _cancelButton.Text = "❌ Cancelar";
        _cancelButton.Click += CancelButton_Click;
        // 
        // _currentPasswordToggleButton
        // 
        _currentPasswordToggleButton.BackColor = Color.Transparent;
        _currentPasswordToggleButton.BorderColor = Color.Transparent;
        _currentPasswordToggleButton.BorderRadius = 7;
        _currentPasswordToggleButton.Cursor = Cursors.Hand;
        _currentPasswordToggleButton.CustomizableEdges = customizableEdges11;
        _currentPasswordToggleButton.FillColor = Color.Transparent;
        _currentPasswordToggleButton.Font = new Font("Segoe UI", 10F);
        _currentPasswordToggleButton.ForeColor = Color.Gray;
        _currentPasswordToggleButton.Location = new Point(385, 161);
        _currentPasswordToggleButton.Name = "_currentPasswordToggleButton";
        _currentPasswordToggleButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _currentPasswordToggleButton.Size = new Size(40, 32);
        _currentPasswordToggleButton.TabIndex = 11;
        _currentPasswordToggleButton.Text = "👁️";
        // 
        // _newPasswordToggleButton
        // 
        _newPasswordToggleButton.BackColor = Color.Transparent;
        _newPasswordToggleButton.BorderColor = Color.Transparent;
        _newPasswordToggleButton.BorderRadius = 7;
        _newPasswordToggleButton.Cursor = Cursors.Hand;
        _newPasswordToggleButton.CustomizableEdges = customizableEdges13;
        _newPasswordToggleButton.FillColor = Color.Transparent;
        _newPasswordToggleButton.Font = new Font("Segoe UI", 10F);
        _newPasswordToggleButton.ForeColor = Color.Gray;
        _newPasswordToggleButton.Location = new Point(385, 221);
        _newPasswordToggleButton.Name = "_newPasswordToggleButton";
        _newPasswordToggleButton.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _newPasswordToggleButton.Size = new Size(40, 32);
        _newPasswordToggleButton.TabIndex = 12;
        _newPasswordToggleButton.Text = "👁️";
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClose.CustomizableEdges = customizableEdges15;
        btnClose.FillColor = Color.Transparent;
        btnClose.IconColor = Color.Gray;
        btnClose.Location = new Point(413, 12);
        btnClose.Name = "btnClose";
        btnClose.ShadowDecoration.CustomizableEdges = customizableEdges16;
        btnClose.Size = new Size(35, 30);
        btnClose.TabIndex = 10;
        btnClose.Click += btnClose_Click;
        // 
        // EditProfileDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(460, 345);
        Controls.Add(btnClose);
        Controls.Add(_cancelButton);
        Controls.Add(_saveButton);
        Controls.Add(_newPasswordToggleButton);
        Controls.Add(_currentPasswordToggleButton);
        Controls.Add(_newPasswordTextBox);
        Controls.Add(_currentPasswordTextBox);
        Controls.Add(_emailTextBox);
        Controls.Add(_newPasswordCaptionLabel);
        Controls.Add(_currentPasswordCaptionLabel);
        Controls.Add(_emailCaptionLabel);
        Controls.Add(_subtitleLabel);
        Controls.Add(_titleLabel);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "EditProfileDialog";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Editar Perfil";
        ResumeLayout(false);
        PerformLayout();
    }

    private Guna.UI2.WinForms.Guna2ControlBox btnClose;
}
