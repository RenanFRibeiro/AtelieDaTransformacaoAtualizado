using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;
partial class SimpleUserDialog
{
    private System.ComponentModel.IContainer? components=null;private Guna.UI2.WinForms.Guna2BorderlessForm _borderlessForm=null!;private Guna.UI2.WinForms.Guna2DragControl _dragControl=null!;private Guna.UI2.WinForms.Guna2Panel _headerPanel=null!;private Guna.UI2.WinForms.Guna2Panel _bodyPanel=null!;private Label _titleLabel=null!;private Label _emailCaption=null!;private Label _passwordCaption=null!;private Guna.UI2.WinForms.Guna2TextBox _emailTextBox=null!;private Guna.UI2.WinForms.Guna2TextBox _passwordTextBox=null!;private Guna.UI2.WinForms.Guna2Button _passwordToggleButton=null!;private Guna.UI2.WinForms.Guna2Button _cancelButton=null!;private Guna.UI2.WinForms.Guna2Button _createButton=null!;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        _borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
        _dragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
        _headerPanel = new Guna.UI2.WinForms.Guna2Panel();
        _titleLabel = new Label();
        _bodyPanel = new Guna.UI2.WinForms.Guna2Panel();
        _createButton = new Guna.UI2.WinForms.Guna2Button();
        _cancelButton = new Guna.UI2.WinForms.Guna2Button();
        _passwordTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _passwordToggleButton = new Guna.UI2.WinForms.Guna2Button();
        _emailTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _passwordCaption = new Label();
        _emailCaption = new Label();
        _headerPanel.SuspendLayout();
        _bodyPanel.SuspendLayout();
        SuspendLayout();
        // 
        // _borderlessForm
        // 
        _borderlessForm.BorderRadius = 14;
        _borderlessForm.ContainerControl = this;
        _borderlessForm.DockIndicatorTransparencyValue = 0.6D;
        _borderlessForm.TransparentWhileDrag = true;
        // 
        // _dragControl
        // 
        _dragControl.DockIndicatorTransparencyValue = 0.6D;
        _dragControl.TargetControl = _headerPanel;
        _dragControl.UseTransparentDrag = true;
        // 
        // _headerPanel
        // 
        _headerPanel.Controls.Add(_titleLabel);
        _headerPanel.CustomizableEdges = customizableEdges11;
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.FillColor = Color.White;
        _headerPanel.Location = new Point(0, 0);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _headerPanel.Size = new Size(520, 76);
        _headerPanel.TabIndex = 1;
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.FromArgb(30, 34, 43);
        _titleLabel.Location = new Point(24, 20);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(158, 31);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Novo usuário";
        // 
        // _bodyPanel
        // 
        _bodyPanel.Controls.Add(_createButton);
        _bodyPanel.Controls.Add(_cancelButton);
        _bodyPanel.Controls.Add(_passwordTextBox);
        _bodyPanel.Controls.Add(_passwordToggleButton);
        _bodyPanel.Controls.Add(_emailTextBox);
        _bodyPanel.Controls.Add(_passwordCaption);
        _bodyPanel.Controls.Add(_emailCaption);
        _bodyPanel.CustomizableEdges = customizableEdges9;
        _bodyPanel.Dock = DockStyle.Fill;
        _bodyPanel.FillColor = Color.White;
        _bodyPanel.Location = new Point(0, 76);
        _bodyPanel.Name = "_bodyPanel";
        _bodyPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _bodyPanel.Size = new Size(520, 284);
        _bodyPanel.TabIndex = 0;
        // 
        // _createButton
        // 
        _createButton.BorderRadius = 7;
        _createButton.CustomizableEdges = customizableEdges1;
        _createButton.FillColor = Color.FromArgb(145, 98, 57);
        _createButton.Font = new Font("Segoe UI", 9F);
        _createButton.ForeColor = Color.White;
        _createButton.Location = new Point(391, 234);
        _createButton.Name = "_createButton";
        _createButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _createButton.Size = new Size(117, 38);
        _createButton.TabIndex = 0;
        _createButton.Text = "💾 Salvar";
        // 
        // _cancelButton
        // 
        _cancelButton.BorderRadius = 7;
        _cancelButton.CustomizableEdges = customizableEdges3;
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.FillColor = Color.Gray;
        _cancelButton.Font = new Font("Segoe UI", 9F);
        _cancelButton.ForeColor = Color.White;
        _cancelButton.Location = new Point(254, 234);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _cancelButton.Size = new Size(117, 38);
        _cancelButton.TabIndex = 1;
        _cancelButton.Text = "❌ Cancelar";
        // 
        // _passwordTextBox
        // 
        _passwordTextBox.BorderRadius = 9;
        _passwordTextBox.CustomizableEdges = customizableEdges5;
        _passwordTextBox.DefaultText = "";
        _passwordTextBox.Font = new Font("Segoe UI", 9F);
        _passwordTextBox.Location = new Point(24, 132);
        _passwordTextBox.Name = "_passwordTextBox";
        _passwordTextBox.PasswordChar = '•';
        _passwordTextBox.PlaceholderText = "🔒 Mínimo 6 caracteres";
        _passwordTextBox.SelectedText = "";
        _passwordTextBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _passwordTextBox.Size = new Size(200, 36);
        _passwordTextBox.TabIndex = 2;
        // 
        // _passwordToggleButton
        // 
        _passwordToggleButton.BorderRadius = 7;
        _passwordToggleButton.Cursor = Cursors.Hand;
        _passwordToggleButton.CustomizableEdges = customizableEdges1;
        _passwordToggleButton.FillColor = Color.FromArgb(241, 243, 248);
        _passwordToggleButton.Font = new Font("Segoe UI", 9F);
        _passwordToggleButton.ForeColor = Color.FromArgb(30, 34, 43);
        _passwordToggleButton.Location = new Point(234, 132);
        _passwordToggleButton.Name = "_passwordToggleButton";
        _passwordToggleButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _passwordToggleButton.Size = new Size(42, 36);
        _passwordToggleButton.TabIndex = 4;
        _passwordToggleButton.Text = "👁";
        // 
        // _emailTextBox
        // 
        _emailTextBox.BorderRadius = 9;
        _emailTextBox.CustomizableEdges = customizableEdges7;
        _emailTextBox.DefaultText = "";
        _emailTextBox.Font = new Font("Segoe UI", 9F);
        _emailTextBox.Location = new Point(24, 58);
        _emailTextBox.Name = "_emailTextBox";
        _emailTextBox.PlaceholderText = "✉️ emai@email.com";
        _emailTextBox.SelectedText = "";
        _emailTextBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _emailTextBox.Size = new Size(200, 36);
        _emailTextBox.TabIndex = 3;
        // 
        // _passwordCaption
        // 
        _passwordCaption.Location = new Point(25, 112);
        _passwordCaption.Name = "_passwordCaption";
        _passwordCaption.Size = new Size(100, 23);
        _passwordCaption.TabIndex = 4;
        _passwordCaption.Text = "Digite sua senha";
        // 
        // _emailCaption
        // 
        _emailCaption.Location = new Point(25, 39);
        _emailCaption.Name = "_emailCaption";
        _emailCaption.Size = new Size(100, 23);
        _emailCaption.TabIndex = 5;
        _emailCaption.Text = "Digite seu Email";
        // 
        // SimpleUserDialog
        // 
        AcceptButton = _createButton;
        BackColor = Color.White;
        CancelButton = _cancelButton;
        ClientSize = new Size(520, 360);
        Controls.Add(_bodyPanel);
        Controls.Add(_headerPanel);
        FormBorderStyle = FormBorderStyle.None;
        Name = "SimpleUserDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Novo usuário";
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        _bodyPanel.ResumeLayout(false);
        ResumeLayout(false);
    }
    private static void ConfigureCaption(Label l,string t,int x,int y){l.AutoSize=true;l.Text=t;l.Font=new Font("Segoe UI Semibold",8.5F);l.ForeColor=LibraryTheme.Text;l.Location=new Point(x,y);}private static void ConfigureText(Guna.UI2.WinForms.Guna2TextBox t,int x,int y,string ph,bool password){t.Location=new Point(x,y);t.Size=new Size(472,42);t.BorderRadius=9;t.BorderColor=LibraryTheme.Border;t.FocusedState.BorderColor=LibraryTheme.Accent;t.PlaceholderText=ph;t.Font=new Font("Segoe UI",9.5F);if(password)t.PasswordChar='●';}private static void ConfigureButton(Guna.UI2.WinForms.Guna2Button b,string text,int x,int y,bool primary){b.Location=new Point(x,180);b.Size=new Size(y,42);b.Text=text;b.BorderRadius=9;b.FillColor=primary?LibraryTheme.Accent:Color.FromArgb(241,243,248);b.ForeColor=primary?Color.White:LibraryTheme.Text;b.Font=new Font("Segoe UI Semibold",9F);b.HoverState.FillColor=primary?LibraryTheme.AccentDark:Color.FromArgb(225,229,238);}
}
