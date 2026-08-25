namespace AtelieDaTransformacao.Desktop.Forms;

partial class ChangePasswordDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Label _titleLabel = null!;
    private Label _subtitleLabel = null!;
    private Label _currentPasswordCaptionLabel = null!;
    private Label _newPasswordCaptionLabel = null!;
    private Label _confirmPasswordCaptionLabel = null!;
    private Guna.UI2.WinForms.Guna2TextBox _currentPasswordTextBox = null!;
    private Guna.UI2.WinForms.Guna2TextBox _newPasswordTextBox = null!;
    private Guna.UI2.WinForms.Guna2TextBox _confirmPasswordTextBox = null!;
    private Guna.UI2.WinForms.Guna2Button _changeButton = null!;
    private Guna.UI2.WinForms.Guna2Button _cancelButton = null!;
    private Guna.UI2.WinForms.Guna2Button _currentPasswordToggleButton = null!;
    private Guna.UI2.WinForms.Guna2Button _newPasswordToggleButton = null!;
    private Guna.UI2.WinForms.Guna2Button _confirmPasswordToggleButton = null!;
    private Guna.UI2.WinForms.Guna2ControlBox btnClose = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        var e1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        var e18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();

        _titleLabel = new Label(); _subtitleLabel = new Label();
        _currentPasswordCaptionLabel = new Label(); _newPasswordCaptionLabel = new Label(); _confirmPasswordCaptionLabel = new Label();
        _currentPasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _newPasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _confirmPasswordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _changeButton = new Guna.UI2.WinForms.Guna2Button(); _cancelButton = new Guna.UI2.WinForms.Guna2Button();
        _currentPasswordToggleButton = new Guna.UI2.WinForms.Guna2Button();
        _newPasswordToggleButton = new Guna.UI2.WinForms.Guna2Button();
        _confirmPasswordToggleButton = new Guna.UI2.WinForms.Guna2Button();
        btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
        SuspendLayout();

        _titleLabel.AutoSize = true; _titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold); _titleLabel.ForeColor = Color.FromArgb(43,26,18); _titleLabel.Location = new Point(28,22); _titleLabel.Text = "Alterar Senha";
        _subtitleLabel.AutoSize = true; _subtitleLabel.Font = new Font("Segoe UI",9F); _subtitleLabel.ForeColor = Color.FromArgb(113,120,135); _subtitleLabel.Location = new Point(30,59); _subtitleLabel.Text = "Informe sua senha atual e defina uma nova senha.";

        ConfigureCaption(_currentPasswordCaptionLabel, "SENHA ATUAL", 82);
        ConfigureCaption(_newPasswordCaptionLabel, "NOVA SENHA", 142);
        ConfigureCaption(_confirmPasswordCaptionLabel, "CONFIRMAR NOVA SENHA", 202);

        ConfigurePasswordBox(_currentPasswordTextBox, "Senha atual", 98, e1, e2);
        ConfigurePasswordBox(_newPasswordTextBox, "Nova senha", 158, e3, e4);
        ConfigurePasswordBox(_confirmPasswordTextBox, "Confirme a nova senha", 218, e5, e6);

        ConfigureToggle(_currentPasswordToggleButton, 101, e7, e8);
        ConfigureToggle(_newPasswordToggleButton, 161, e9, e10);
        ConfigureToggle(_confirmPasswordToggleButton, 221, e11, e12);

        _changeButton.BorderRadius=9; _changeButton.CustomizableEdges=e13; _changeButton.FillColor=Color.FromArgb(88,52,27); _changeButton.Font=new Font("Segoe UI Semibold",9F); _changeButton.ForeColor=Color.White; _changeButton.Location=new Point(343,293); _changeButton.Size=new Size(96,40); _changeButton.Text="🔑 Alterar"; _changeButton.ShadowDecoration.CustomizableEdges=e14; _changeButton.Click += ChangeButton_Click;
        _cancelButton.BorderRadius=9; _cancelButton.CustomizableEdges=e15; _cancelButton.FillColor=Color.Gray; _cancelButton.Font=new Font("Segoe UI Semibold",9F); _cancelButton.ForeColor=Color.White; _cancelButton.Location=new Point(242,293); _cancelButton.Size=new Size(95,40); _cancelButton.Text="❌ Cancelar"; _cancelButton.ShadowDecoration.CustomizableEdges=e16; _cancelButton.Click += CancelButton_Click;

        btnClose.Anchor=AnchorStyles.Top|AnchorStyles.Right; btnClose.CustomizableEdges=e17; btnClose.FillColor=Color.Transparent; btnClose.IconColor=Color.Gray; btnClose.Location=new Point(413,12); btnClose.Size=new Size(35,30); btnClose.ShadowDecoration.CustomizableEdges=e18; btnClose.Click += btnClose_Click;

        Controls.Add(btnClose); Controls.Add(_cancelButton); Controls.Add(_changeButton);
        Controls.Add(_confirmPasswordToggleButton); Controls.Add(_newPasswordToggleButton); Controls.Add(_currentPasswordToggleButton);
        Controls.Add(_confirmPasswordTextBox); Controls.Add(_newPasswordTextBox); Controls.Add(_currentPasswordTextBox);
        Controls.Add(_confirmPasswordCaptionLabel); Controls.Add(_newPasswordCaptionLabel); Controls.Add(_currentPasswordCaptionLabel); Controls.Add(_subtitleLabel); Controls.Add(_titleLabel);
        AutoScaleDimensions=new SizeF(7F,15F); AutoScaleMode=AutoScaleMode.Font; BackColor=Color.White; ClientSize=new Size(460,345); FormBorderStyle=FormBorderStyle.None; MaximizeBox=false; MinimizeBox=false; Name="ChangePasswordDialog"; ShowInTaskbar=false; StartPosition=FormStartPosition.CenterParent; Text="Alterar Senha";
        ResumeLayout(false); PerformLayout();
    }

    private static void ConfigureCaption(Label label, string text, int y)
    {
        label.AutoSize=true; label.Font=new Font("Segoe UI",8.5F,FontStyle.Bold); label.ForeColor=Color.FromArgb(113,120,135); label.Location=new Point(30,y); label.Text=text;
    }

    private static void ConfigurePasswordBox(Guna.UI2.WinForms.Guna2TextBox box, string placeholder, int y, Guna.UI2.WinForms.Suite.CustomizableEdges borderEdges, Guna.UI2.WinForms.Suite.CustomizableEdges shadowEdges)
    {
        box.BorderColor=Color.FromArgb(215,218,225); box.BorderRadius=8; box.CustomizableEdges=borderEdges; box.DefaultText=""; box.Font=new Font("Segoe UI",9.5F); box.Location=new Point(30,y); box.Name=placeholder.Replace(" ",""); box.PasswordChar='●'; box.PlaceholderText=placeholder; box.SelectedText=""; box.ShadowDecoration.CustomizableEdges=shadowEdges; box.Size=new Size(400,38);
    }

    private static void ConfigureToggle(Guna.UI2.WinForms.Guna2Button button, int y, Guna.UI2.WinForms.Suite.CustomizableEdges edges, Guna.UI2.WinForms.Suite.CustomizableEdges shadowEdges)
    {
        button.BackColor=Color.Transparent; button.BorderColor=Color.Transparent; button.BorderRadius=7; button.Cursor=Cursors.Hand; button.CustomizableEdges=edges; button.FillColor=Color.Transparent; button.Font=new Font("Segoe UI",10F); button.ForeColor=Color.Gray; button.Location=new Point(385,y); button.ShadowDecoration.CustomizableEdges=shadowEdges; button.Size=new Size(40,32); button.Text="👁️";
    }
}
