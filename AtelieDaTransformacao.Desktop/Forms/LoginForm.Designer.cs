using System.Drawing;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer? components = null;
    private Guna.UI2.WinForms.Guna2BorderlessForm _borderlessForm = null!;
    private Guna.UI2.WinForms.Guna2DragControl _dragControl = null!;
    private Guna.UI2.WinForms.Guna2Panel _brandPanel = null!;
    private Guna.UI2.WinForms.Guna2Panel _loginCard = null!;
    private Guna.UI2.WinForms.Guna2TextBox _emailTextBox = null!;
    private Guna.UI2.WinForms.Guna2TextBox _passwordTextBox = null!;
    private Guna.UI2.WinForms.Guna2Button _passwordToggleButton = null!;
    private Guna.UI2.WinForms.Guna2Button _loginButton = null!;
    private Label _brandLabel = null!;
    private Label _brandDescription = null!;
    private Label _featureLabel = null!;
    private Label _welcomeLabel = null!;
    private Label _subtitleLabel = null!;
    private Label _emailLabel = null!;
    private Label _passwordLabel = null!;
    private Label _statusLabel = null!;
    private Label _apiStatusLabel = null!;
    private Label _versionLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
        _borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
        _dragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
        _brandPanel = new Guna.UI2.WinForms.Guna2Panel();
        pnSeparador2 = new Panel();
        _brandDescription = new Label();
        _brandLabel = new Label();
        _featureLabel = new Label();
        _loginCard = new Guna.UI2.WinForms.Guna2Panel();
        btnMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
        btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
        _versionLabel = new Label();
        _apiStatusLabel = new Label();
        _statusLabel = new Label();
        _loginButton = new Guna.UI2.WinForms.Guna2Button();
        _passwordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _passwordToggleButton = new Guna.UI2.WinForms.Guna2Button();
        _passwordLabel = new Label();
        _emailTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _emailLabel = new Label();
        _subtitleLabel = new Label();
        _welcomeLabel = new Label();
        _brandPanel.SuspendLayout();
        _loginCard.SuspendLayout();
        SuspendLayout();
        // 
        // _borderlessForm
        // 
        _borderlessForm.BorderRadius = 16;
        _borderlessForm.ContainerControl = this;
        _borderlessForm.DockIndicatorTransparencyValue = 0.6D;
        _borderlessForm.TransparentWhileDrag = true;
        // 
        // _dragControl
        // 
        _dragControl.DockIndicatorTransparencyValue = 0.6D;
        _dragControl.TargetControl = _brandPanel;
        _dragControl.UseTransparentDrag = true;
        // 
        // _brandPanel
        // 
        _brandPanel.Controls.Add(pnSeparador2);
        _brandPanel.Controls.Add(_brandDescription);
        _brandPanel.Controls.Add(_brandLabel);
        _brandPanel.CustomizableEdges = customizableEdges13;
        _brandPanel.Dock = DockStyle.Left;
        _brandPanel.FillColor = Color.FromArgb(43, 26, 18);
        _brandPanel.Location = new Point(0, 0);
        _brandPanel.Name = "_brandPanel";
        _brandPanel.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _brandPanel.Size = new Size(320, 450);
        _brandPanel.TabIndex = 1;
        // 
        // pnSeparador2
        // 
        pnSeparador2.BackColor = Color.FromArgb(164, 104, 45);
        pnSeparador2.BorderStyle = BorderStyle.FixedSingle;
        pnSeparador2.ForeColor = SystemColors.MenuHighlight;
        pnSeparador2.Location = new Point(20, 147);
        pnSeparador2.Name = "pnSeparador2";
        pnSeparador2.Size = new Size(180, 5);
        pnSeparador2.TabIndex = 9;
        // 
        // _brandDescription
        // 
        _brandDescription.AutoSize = true;
        _brandDescription.BackColor = Color.Transparent;
        _brandDescription.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        _brandDescription.ForeColor = SystemColors.ControlDark;
        _brandDescription.Location = new Point(20, 196);
        _brandDescription.Name = "_brandDescription";
        _brandDescription.Size = new Size(275, 102);
        _brandDescription.TabIndex = 1;
        _brandDescription.Text = resources.GetString("_brandDescription.Text");
        // 
        // _brandLabel
        // 
        _brandLabel.AutoSize = true;
        _brandLabel.BackColor = Color.Transparent;
        _brandLabel.Font = new Font("Yu Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        _brandLabel.ForeColor = Color.White;
        _brandLabel.Location = new Point(12, 67);
        _brandLabel.Name = "_brandLabel";
        _brandLabel.Size = new Size(280, 62);
        _brandLabel.TabIndex = 2;
        _brandLabel.Text = "ATELIÊ\nDA TRANSFORMAÇÃO";
        // 
        // _featureLabel
        // 
        _featureLabel.AutoSize = true;
        _featureLabel.BackColor = Color.Transparent;
        _featureLabel.Font = new Font("Segoe UI Semibold", 8F);
        _featureLabel.ForeColor = Color.Black;
        _featureLabel.Location = new Point(46, 428);
        _featureLabel.Name = "_featureLabel";
        _featureLabel.Size = new Size(359, 13);
        _featureLabel.TabIndex = 0;
        _featureLabel.Text = "●  API conectada     ●  Autenticação segura      ●  Controle de estoque ";
        // 
        // _loginCard
        // 
        _loginCard.BorderRadius = 10;
        _loginCard.Controls.Add(btnMinimize);
        _loginCard.Controls.Add(_featureLabel);
        _loginCard.Controls.Add(btnClose);
        _loginCard.Controls.Add(_versionLabel);
        _loginCard.Controls.Add(_apiStatusLabel);
        _loginCard.Controls.Add(_statusLabel);
        _loginCard.Controls.Add(_loginButton);
        _loginCard.Controls.Add(_passwordTextBox);
        _loginCard.Controls.Add(_passwordToggleButton);
        _loginCard.Controls.Add(_passwordLabel);
        _loginCard.Controls.Add(_emailTextBox);
        _loginCard.Controls.Add(_emailLabel);
        _loginCard.Controls.Add(_subtitleLabel);
        _loginCard.Controls.Add(_welcomeLabel);
        _loginCard.CustomizableEdges = customizableEdges11;
        _loginCard.Dock = DockStyle.Fill;
        _loginCard.FillColor = Color.White;
        _loginCard.Location = new Point(320, 0);
        _loginCard.Name = "_loginCard";
        _loginCard.Padding = new Padding(70, 60, 70, 40);
        _loginCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _loginCard.Size = new Size(480, 450);
        _loginCard.TabIndex = 0;
        // 
        // btnMinimize
        // 
        btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
        btnMinimize.CustomizableEdges = customizableEdges1;
        btnMinimize.FillColor = Color.Transparent;
        btnMinimize.IconColor = Color.Gray;
        btnMinimize.Location = new Point(395, 12);
        btnMinimize.Name = "btnMinimize";
        btnMinimize.ShadowDecoration.CustomizableEdges = customizableEdges2;
        btnMinimize.Size = new Size(35, 30);
        btnMinimize.TabIndex = 10;
        btnMinimize.Click += btnMinimize_Click;
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClose.CustomizableEdges = customizableEdges3;
        btnClose.FillColor = Color.Transparent;
        btnClose.IconColor = Color.Gray;
        btnClose.Location = new Point(433, 12);
        btnClose.Name = "btnClose";
        btnClose.ShadowDecoration.CustomizableEdges = customizableEdges4;
        btnClose.Size = new Size(35, 30);
        btnClose.TabIndex = 2;
        btnClose.Click += btnClose_Click;
        // 
        // _versionLabel
        // 
        _versionLabel.AutoSize = true;
        _versionLabel.Font = new Font("Segoe UI", 8F);
        _versionLabel.ForeColor = Color.FromArgb(160, 165, 175);
        _versionLabel.Location = new Point(46, 362);
        _versionLabel.Name = "_versionLabel";
        _versionLabel.Size = new Size(182, 13);
        _versionLabel.TabIndex = 0;
        _versionLabel.Text = "Ateliê da Transformação • Desktop";
        // 
        // _apiStatusLabel
        // 
        _apiStatusLabel.AutoSize = true;
        _apiStatusLabel.Font = new Font("Segoe UI", 8.5F);
        _apiStatusLabel.Location = new Point(46, 342);
        _apiStatusLabel.Name = "_apiStatusLabel";
        _apiStatusLabel.Size = new Size(99, 15);
        _apiStatusLabel.TabIndex = 1;
        _apiStatusLabel.Text = "API: Verificando...";
        // 
        // _statusLabel
        // 
        _statusLabel.Font = new Font("Segoe UI", 9F);
        _statusLabel.Location = new Point(43, 375);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(390, 46);
        _statusLabel.TabIndex = 2;
        // 
        // _loginButton
        // 
        _loginButton.BorderRadius = 9;
        _loginButton.CustomizableEdges = customizableEdges5;
        _loginButton.FillColor = Color.FromArgb(164, 104, 45);
        _loginButton.Font = new Font("Segoe UI Semibold", 10F);
        _loginButton.ForeColor = Color.White;
        _loginButton.Location = new Point(46, 267);
        _loginButton.Name = "_loginButton";
        _loginButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _loginButton.Size = new Size(358, 46);
        _loginButton.TabIndex = 3;
        _loginButton.Text = "ENTRAR";
        // 
        // _passwordTextBox
        // 
        _passwordTextBox.BorderRadius = 9;
        _passwordTextBox.CustomizableEdges = customizableEdges7;
        _passwordTextBox.DefaultText = "";
        _passwordTextBox.Font = new Font("Segoe UI", 10F);
        _passwordTextBox.Location = new Point(49, 186);
        _passwordTextBox.Name = "_passwordTextBox";
        _passwordTextBox.PasswordChar = '•';
        _passwordTextBox.PlaceholderText = "🔒 Digite sua senha";
        _passwordTextBox.SelectedText = "";
        _passwordTextBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _passwordTextBox.Size = new Size(358, 42);
        _passwordTextBox.TabIndex = 4;
        // 
        // _passwordToggleButton
        // 
        _passwordToggleButton.BorderRadius = 7;
        _passwordToggleButton.Cursor = Cursors.Hand;
        _passwordToggleButton.CustomizableEdges = customizableEdges1;
        _passwordToggleButton.FillColor = Color.FromArgb(241, 243, 248);
        _passwordToggleButton.Font = new Font("Segoe UI", 9F);
        _passwordToggleButton.ForeColor = Color.FromArgb(30, 34, 43);
        _passwordToggleButton.Location = new Point(413, 186);
        _passwordToggleButton.Name = "_passwordToggleButton";
        _passwordToggleButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _passwordToggleButton.Size = new Size(46, 42);
        _passwordToggleButton.TabIndex = 11;
        _passwordToggleButton.Text = "👁";
        // 
        // _passwordLabel
        // 
        _passwordLabel.Location = new Point(0, 0);
        _passwordLabel.Name = "_passwordLabel";
        _passwordLabel.Size = new Size(100, 23);
        _passwordLabel.TabIndex = 5;
        // 
        // _emailTextBox
        // 
        _emailTextBox.BorderRadius = 9;
        _emailTextBox.CustomizableEdges = customizableEdges9;
        _emailTextBox.DefaultText = "";
        _emailTextBox.Font = new Font("Segoe UI", 10F);
        _emailTextBox.Location = new Point(49, 138);
        _emailTextBox.Name = "_emailTextBox";
        _emailTextBox.PlaceholderText = "✉️ seu@email.com";
        _emailTextBox.SelectedText = "";
        _emailTextBox.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _emailTextBox.Size = new Size(358, 42);
        _emailTextBox.TabIndex = 6;
        // 
        // _emailLabel
        // 
        _emailLabel.Location = new Point(0, 0);
        _emailLabel.Name = "_emailLabel";
        _emailLabel.Size = new Size(100, 23);
        _emailLabel.TabIndex = 7;
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.AutoSize = true;
        _subtitleLabel.Font = new Font("Segoe UI", 10F);
        _subtitleLabel.ForeColor = SystemColors.ControlDark;
        _subtitleLabel.Location = new Point(52, 103);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(230, 19);
        _subtitleLabel.TabIndex = 8;
        _subtitleLabel.Text = "Entre com sua conta para continuar.";
        // 
        // _welcomeLabel
        // 
        _welcomeLabel.AutoSize = true;
        _welcomeLabel.Font = new Font("Yu Gothic", 24.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        _welcomeLabel.Location = new Point(46, 58);
        _welcomeLabel.Name = "_welcomeLabel";
        _welcomeLabel.Size = new Size(207, 43);
        _welcomeLabel.TabIndex = 9;
        _welcomeLabel.Text = "Bem-Vindo!";
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(800, 450);
        Controls.Add(_loginCard);
        Controls.Add(_brandPanel);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Ateliê da Transformação | Acesso";
        Load += LoginForm_Load;
        _brandPanel.ResumeLayout(false);
        _brandPanel.PerformLayout();
        _loginCard.ResumeLayout(false);
        _loginCard.PerformLayout();
        ResumeLayout(false);
    }

    private static void ConfigureLabel(Label label, string text, int x, int y)
    {
        label.AutoSize = true;
        label.Text = text;
        label.ForeColor = LibraryTheme.Text;
        label.Font = new Font("Segoe UI Semibold", 8.5F);
        label.Location = new Point(x, y);
    }

    private Guna.UI2.WinForms.Guna2ControlBox btnMinimize;
    private Guna.UI2.WinForms.Guna2ControlBox btnClose;
    private Panel pnSeparador2;
}
