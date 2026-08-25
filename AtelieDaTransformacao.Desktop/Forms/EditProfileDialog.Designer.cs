namespace AtelieDaTransformacao.Desktop.Forms;

partial class EditProfileDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Label _titleLabel = null!;
    private Label _subtitleLabel = null!;
    private Label _emailCaptionLabel = null!;
    private Guna.UI2.WinForms.Guna2TextBox _emailTextBox = null!;
    private Guna.UI2.WinForms.Guna2Button _saveButton = null!;
    private Guna.UI2.WinForms.Guna2Button _cancelButton = null!;
    private Guna.UI2.WinForms.Guna2ControlBox btnClose = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        var e1=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e2=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e3=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e4=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e5=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e6=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e7=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e8=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e9=new Guna.UI2.WinForms.Suite.CustomizableEdges(); var e10=new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _titleLabel=new Label(); _subtitleLabel=new Label(); _emailCaptionLabel=new Label(); _emailTextBox=new Guna.UI2.WinForms.Guna2TextBox(); _saveButton=new Guna.UI2.WinForms.Guna2Button(); _cancelButton=new Guna.UI2.WinForms.Guna2Button(); btnClose=new Guna.UI2.WinForms.Guna2ControlBox();
        SuspendLayout();
        _titleLabel.AutoSize=true; _titleLabel.Font=new Font("Segoe UI",18F,FontStyle.Bold); _titleLabel.ForeColor=Color.FromArgb(43,26,18); _titleLabel.Location=new Point(28,22); _titleLabel.Text="Editar Perfil";
        _subtitleLabel.AutoSize=true; _subtitleLabel.Font=new Font("Segoe UI",9F); _subtitleLabel.ForeColor=Color.FromArgb(113,120,135); _subtitleLabel.Location=new Point(30,59); _subtitleLabel.Text="Atualize somente o e-mail da sua conta.";
        _emailCaptionLabel.AutoSize=true; _emailCaptionLabel.Font=new Font("Segoe UI",8.5F,FontStyle.Bold); _emailCaptionLabel.ForeColor=Color.FromArgb(113,120,135); _emailCaptionLabel.Location=new Point(30,91); _emailCaptionLabel.Text="E-MAIL";
        _emailTextBox.BorderColor=Color.FromArgb(215,218,225); _emailTextBox.BorderRadius=8; _emailTextBox.CustomizableEdges=e1; _emailTextBox.DefaultText=""; _emailTextBox.Font=new Font("Segoe UI",9.5F); _emailTextBox.Location=new Point(30,108); _emailTextBox.Name="_emailTextBox"; _emailTextBox.PlaceholderText="E-mail"; _emailTextBox.SelectedText=""; _emailTextBox.ShadowDecoration.CustomizableEdges=e2; _emailTextBox.Size=new Size(400,38);
        _cancelButton.BorderRadius=9; _cancelButton.CustomizableEdges=e5; _cancelButton.FillColor=Color.Gray; _cancelButton.Font=new Font("Segoe UI Semibold",9F); _cancelButton.ForeColor=Color.White; _cancelButton.Location=new Point(242,184); _cancelButton.Name="_cancelButton"; _cancelButton.ShadowDecoration.CustomizableEdges=e6; _cancelButton.Size=new Size(95,40); _cancelButton.Text="❌ Cancelar"; _cancelButton.Click+=CancelButton_Click;
        _saveButton.BorderRadius=9; _saveButton.CustomizableEdges=e7; _saveButton.FillColor=Color.FromArgb(88,52,27); _saveButton.Font=new Font("Segoe UI Semibold",9F); _saveButton.ForeColor=Color.White; _saveButton.Location=new Point(343,184); _saveButton.Name="_saveButton"; _saveButton.ShadowDecoration.CustomizableEdges=e8; _saveButton.Size=new Size(96,40); _saveButton.Text="💾 Salvar"; _saveButton.Click+=SaveButton_Click;
        btnClose.Anchor=AnchorStyles.Top|AnchorStyles.Right; btnClose.CustomizableEdges=e9; btnClose.FillColor=Color.Transparent; btnClose.IconColor=Color.Gray; btnClose.Location=new Point(413,12); btnClose.Name="btnClose"; btnClose.ShadowDecoration.CustomizableEdges=e10; btnClose.Size=new Size(35,30); btnClose.Click+=btnClose_Click;
        Controls.Add(btnClose); Controls.Add(_saveButton); Controls.Add(_cancelButton); Controls.Add(_emailTextBox); Controls.Add(_emailCaptionLabel); Controls.Add(_subtitleLabel); Controls.Add(_titleLabel);
        AutoScaleDimensions=new SizeF(7F,15F); AutoScaleMode=AutoScaleMode.Font; BackColor=Color.White; ClientSize=new Size(460,245); FormBorderStyle=FormBorderStyle.None; MaximizeBox=false; MinimizeBox=false; Name="EditProfileDialog"; ShowInTaskbar=false; StartPosition=FormStartPosition.CenterParent; Text="Editar Perfil";
        ResumeLayout(false); PerformLayout();
    }
}
