namespace AtelieDaTransformacao.Desktop.Forms;

partial class CategoryDialog
{
    private System.ComponentModel.IContainer? components = null;

    private Guna.UI2.WinForms.Guna2BorderlessForm _borderlessForm = null!;
    private Guna.UI2.WinForms.Guna2DragControl _dragControl = null!;
    private Guna.UI2.WinForms.Guna2Panel _headerPanel = null!;
    private Guna.UI2.WinForms.Guna2Panel _bodyPanel = null!;
    private Label _titleLabel = null!;
    private Label _nameCaption = null!;
    private Label _descriptionCaption = null!;
    private Guna.UI2.WinForms.Guna2TextBox _nameTextBox = null!;
    private Guna.UI2.WinForms.Guna2TextBox _descriptionTextBox = null!;
    private Guna.UI2.WinForms.Guna2Button _cancelButton = null!;
    private Guna.UI2.WinForms.Guna2Button _saveButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

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
        _saveButton = new Guna.UI2.WinForms.Guna2Button();
        _cancelButton = new Guna.UI2.WinForms.Guna2Button();
        _descriptionTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _descriptionCaption = new Label();
        _nameTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _nameCaption = new Label();
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
        _headerPanel.BackColor = Color.White;
        _headerPanel.Controls.Add(_titleLabel);
        _headerPanel.CustomizableEdges = customizableEdges11;
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.FillColor = Color.White;
        _headerPanel.Location = new Point(0, 0);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _headerPanel.Size = new Size(560, 76);
        _headerPanel.TabIndex = 0;
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.FromArgb(43, 26, 18);
        _titleLabel.Location = new Point(24, 20);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(117, 31);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Categoria";
        // 
        // _bodyPanel
        // 
        _bodyPanel.BackColor = Color.White;
        _bodyPanel.Controls.Add(_saveButton);
        _bodyPanel.Controls.Add(_cancelButton);
        _bodyPanel.Controls.Add(_descriptionTextBox);
        _bodyPanel.Controls.Add(_descriptionCaption);
        _bodyPanel.Controls.Add(_nameTextBox);
        _bodyPanel.Controls.Add(_nameCaption);
        _bodyPanel.CustomizableEdges = customizableEdges9;
        _bodyPanel.Dock = DockStyle.Fill;
        _bodyPanel.FillColor = Color.White;
        _bodyPanel.Location = new Point(0, 76);
        _bodyPanel.Name = "_bodyPanel";
        _bodyPanel.Padding = new Padding(24, 0, 24, 20);
        _bodyPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _bodyPanel.Size = new Size(560, 314);
        _bodyPanel.TabIndex = 1;
        // 
        // _saveButton
        // 
        _saveButton.BorderRadius = 9;
        _saveButton.CustomizableEdges = customizableEdges1;
        _saveButton.FillColor = Color.FromArgb(88, 52, 27);
        _saveButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _saveButton.ForeColor = Color.White;
        _saveButton.HoverState.FillColor = Color.FromArgb(88, 52, 27);
        _saveButton.Location = new Point(413, 249);
        _saveButton.Name = "_saveButton";
        _saveButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _saveButton.Size = new Size(120, 42);
        _saveButton.TabIndex = 5;
        _saveButton.Text = "💾 Salvar";
        // 
        // _cancelButton
        // 
        _cancelButton.BorderRadius = 9;
        _cancelButton.CustomizableEdges = customizableEdges3;
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.FillColor = Color.Gray;
        _cancelButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _cancelButton.ForeColor = Color.White;
        _cancelButton.HoverState.FillColor = Color.FromArgb(225, 229, 238);
        _cancelButton.Location = new Point(293, 249);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _cancelButton.Size = new Size(112, 42);
        _cancelButton.TabIndex = 4;
        _cancelButton.Text = "❌ Cancelar";
        // 
        // _descriptionTextBox
        // 
        _descriptionTextBox.BorderColor = Color.FromArgb(220, 220, 225);
        _descriptionTextBox.BorderRadius = 9;
        _descriptionTextBox.Cursor = Cursors.IBeam;
        _descriptionTextBox.CustomizableEdges = customizableEdges5;
        _descriptionTextBox.DefaultText = "";
        _descriptionTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
        _descriptionTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
        _descriptionTextBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
        _descriptionTextBox.FocusedState.BorderColor = Color.FromArgb(88, 52, 27);
        _descriptionTextBox.Font = new Font("Segoe UI", 9.5F);
        _descriptionTextBox.ForeColor = Color.FromArgb(43, 26, 18);
        _descriptionTextBox.Location = new Point(24, 114);
        _descriptionTextBox.Multiline = true;
        _descriptionTextBox.Name = "_descriptionTextBox";
        _descriptionTextBox.PlaceholderText = "Descrição da categoria";
        _descriptionTextBox.ScrollBars = ScrollBars.Vertical;
        _descriptionTextBox.SelectedText = "";
        _descriptionTextBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _descriptionTextBox.Size = new Size(512, 78);
        _descriptionTextBox.TabIndex = 3;
        // 
        // _descriptionCaption
        // 
        _descriptionCaption.AutoSize = true;
        _descriptionCaption.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
        _descriptionCaption.ForeColor = Color.FromArgb(43, 26, 18);
        _descriptionCaption.Location = new Point(24, 92);
        _descriptionCaption.Name = "_descriptionCaption";
        _descriptionCaption.Size = new Size(59, 15);
        _descriptionCaption.TabIndex = 2;
        _descriptionCaption.Text = "Descrição";
        // 
        // _nameTextBox
        // 
        _nameTextBox.BorderColor = Color.FromArgb(220, 220, 225);
        _nameTextBox.BorderRadius = 9;
        _nameTextBox.Cursor = Cursors.IBeam;
        _nameTextBox.CustomizableEdges = customizableEdges7;
        _nameTextBox.DefaultText = "";
        _nameTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
        _nameTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
        _nameTextBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
        _nameTextBox.FocusedState.BorderColor = Color.FromArgb(88, 52, 27);
        _nameTextBox.Font = new Font("Segoe UI", 9.5F);
        _nameTextBox.ForeColor = Color.FromArgb(43, 26, 18);
        _nameTextBox.Location = new Point(24, 40);
        _nameTextBox.Name = "_nameTextBox";
        _nameTextBox.PlaceholderText = "Nome da categoria";
        _nameTextBox.SelectedText = "";
        _nameTextBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _nameTextBox.Size = new Size(512, 42);
        _nameTextBox.TabIndex = 1;
        // 
        // _nameCaption
        // 
        _nameCaption.AutoSize = true;
        _nameCaption.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
        _nameCaption.ForeColor = Color.FromArgb(43, 26, 18);
        _nameCaption.Location = new Point(24, 18);
        _nameCaption.Name = "_nameCaption";
        _nameCaption.Size = new Size(40, 15);
        _nameCaption.TabIndex = 0;
        _nameCaption.Text = "Nome";
        // 
        // CategoryDialog
        // 
        AcceptButton = _saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        CancelButton = _cancelButton;
        ClientSize = new Size(560, 390);
        ControlBox = false;
        Controls.Add(_bodyPanel);
        Controls.Add(_headerPanel);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "CategoryDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Categoria";
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        _bodyPanel.ResumeLayout(false);
        _bodyPanel.PerformLayout();
        ResumeLayout(false);
    }
}
