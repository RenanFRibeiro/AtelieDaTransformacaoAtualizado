using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;
partial class ProfileUserControl
{
    private System.ComponentModel.IContainer? components=null;private Guna.UI2.WinForms.Guna2Panel _card=null!;private Label _titleLabel=null!;private Label _emailCaptionLabel=null!;private Label _emailValueLabel=null!;private Label _roleCaptionLabel=null!;private Label _roleValueLabel=null!;private Label _sessionCaptionLabel=null!;private Label _sessionValueLabel=null!;private Label _permissionsCaptionLabel=null!;private Label _permissionsValueLabel=null!;    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _card = new Guna.UI2.WinForms.Guna2Panel();
        _permissionsValueLabel = new Label();
        _permissionsCaptionLabel = new Label();
        _sessionValueLabel = new Label();
        _sessionCaptionLabel = new Label();
        _roleValueLabel = new Label();
        _roleCaptionLabel = new Label();
        _emailValueLabel = new Label();
        _emailCaptionLabel = new Label();
        _titleLabel = new Label();
        _card.SuspendLayout();
        SuspendLayout();
        // 
        // _card
        // 
        _card.BorderColor = Color.FromArgb(226, 229, 236);
        _card.BorderRadius = 12;
        _card.BorderThickness = 1;
        _card.Controls.Add(_permissionsValueLabel);
        _card.Controls.Add(_permissionsCaptionLabel);
        _card.Controls.Add(_sessionValueLabel);
        _card.Controls.Add(_sessionCaptionLabel);
        _card.Controls.Add(_roleValueLabel);
        _card.Controls.Add(_roleCaptionLabel);
        _card.Controls.Add(_emailValueLabel);
        _card.Controls.Add(_emailCaptionLabel);
        _card.CustomizableEdges = customizableEdges1;
        _card.FillColor = Color.White;
        _card.Location = new Point(38, 52);
        _card.Name = "_card";
        _card.Padding = new Padding(24);
        _card.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _card.Size = new Size(516, 382);
        _card.TabIndex = 1;
        // 
        // _permissionsValueLabel
        // 
        _permissionsValueLabel.BackColor = Color.Transparent;
        _permissionsValueLabel.Font = new Font("Yu Gothic", 9F);
        _permissionsValueLabel.Location = new Point(22, 348);
        _permissionsValueLabel.Name = "_permissionsValueLabel";
        _permissionsValueLabel.Size = new Size(100, 23);
        _permissionsValueLabel.TabIndex = 0;
        _permissionsValueLabel.Text = "...";
        // 
        // _permissionsCaptionLabel
        // 
        _permissionsCaptionLabel.BackColor = Color.Transparent;
        _permissionsCaptionLabel.Font = new Font("Yu Gothic", 9.75F, FontStyle.Bold);
        _permissionsCaptionLabel.Location = new Point(22, 322);
        _permissionsCaptionLabel.Name = "_permissionsCaptionLabel";
        _permissionsCaptionLabel.Size = new Size(100, 23);
        _permissionsCaptionLabel.TabIndex = 1;
        _permissionsCaptionLabel.Text = "PERMISSÕES";
        // 
        // _sessionValueLabel
        // 
        _sessionValueLabel.BackColor = Color.Transparent;
        _sessionValueLabel.Font = new Font("Yu Gothic", 9F);
        _sessionValueLabel.Location = new Point(22, 291);
        _sessionValueLabel.Name = "_sessionValueLabel";
        _sessionValueLabel.Size = new Size(100, 23);
        _sessionValueLabel.TabIndex = 2;
        _sessionValueLabel.Text = "JWT autenticado";
        // 
        // _sessionCaptionLabel
        // 
        _sessionCaptionLabel.BackColor = Color.Transparent;
        _sessionCaptionLabel.Font = new Font("Yu Gothic", 9.75F, FontStyle.Bold);
        _sessionCaptionLabel.Location = new Point(22, 267);
        _sessionCaptionLabel.Name = "_sessionCaptionLabel";
        _sessionCaptionLabel.Size = new Size(100, 23);
        _sessionCaptionLabel.TabIndex = 3;
        _sessionCaptionLabel.Text = "SESSÃO";
        // 
        // _roleValueLabel
        // 
        _roleValueLabel.BackColor = Color.FromArgb(141, 103, 56);
        _roleValueLabel.ForeColor = Color.White;
        _roleValueLabel.Location = new Point(172, 152);
        _roleValueLabel.Name = "_roleValueLabel";
        _roleValueLabel.Size = new Size(188, 31);
        _roleValueLabel.TabIndex = 4;
        _roleValueLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _roleCaptionLabel
        // 
        _roleCaptionLabel.BackColor = Color.Transparent;
        _roleCaptionLabel.Font = new Font("Yu Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        _roleCaptionLabel.Location = new Point(228, 124);
        _roleCaptionLabel.Name = "_roleCaptionLabel";
        _roleCaptionLabel.Size = new Size(100, 23);
        _roleCaptionLabel.TabIndex = 5;
        _roleCaptionLabel.Text = "Usuário";
        // 
        // _emailValueLabel
        // 
        _emailValueLabel.BackColor = Color.Transparent;
        _emailValueLabel.Font = new Font("Yu Gothic", 9F);
        _emailValueLabel.ForeColor = Color.Black;
        _emailValueLabel.Location = new Point(22, 240);
        _emailValueLabel.Name = "_emailValueLabel";
        _emailValueLabel.Size = new Size(100, 23);
        _emailValueLabel.TabIndex = 6;
        // 
        // _emailCaptionLabel
        // 
        _emailCaptionLabel.BackColor = Color.Transparent;
        _emailCaptionLabel.Font = new Font("Yu Gothic", 9.75F, FontStyle.Bold);
        _emailCaptionLabel.Location = new Point(22, 220);
        _emailCaptionLabel.Name = "_emailCaptionLabel";
        _emailCaptionLabel.Size = new Size(100, 23);
        _emailCaptionLabel.TabIndex = 7;
        _emailCaptionLabel.Text = "E-MAIL";
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.BackColor = Color.Transparent;
        _titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.FromArgb(30, 34, 43);
        _titleLabel.Location = new Point(38, 24);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(137, 31);
        _titleLabel.TabIndex = 8;
        _titleLabel.Text = "Conta atual";
        // 
        // ProfileUserControl
        // 
        BackColor = Color.FromArgb(245, 247, 251);
        Controls.Add(_card);
        Controls.Add(_titleLabel);
        Name = "ProfileUserControl";
        Size = new Size(593, 463);
        _card.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
    private static void AddPair(Label caption,Label value,string title,int y){caption.AutoSize=true;caption.Text=title;caption.Font=new Font("Segoe UI",8.5F);caption.ForeColor=LibraryTheme.Muted;caption.Location=new Point(24,y);value.AutoSize=true;value.Font=new Font("Segoe UI",10F,FontStyle.Bold);value.ForeColor=LibraryTheme.Text;value.Location=new Point(150,y-2);}
}
