using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;
partial class ProductDialog
{
    private System.ComponentModel.IContainer? components=null;private Guna.UI2.WinForms.Guna2BorderlessForm _borderlessForm=null!;private Guna.UI2.WinForms.Guna2DragControl _dragControl=null!;private Guna.UI2.WinForms.Guna2Panel _headerPanel=null!;private Label _titleLabel=null!;private Guna.UI2.WinForms.Guna2Panel _bodyPanel=null!;private Guna.UI2.WinForms.Guna2TextBox _titleTextBox=null!;private Guna.UI2.WinForms.Guna2TextBox _descriptionTextBox=null!;private Guna.UI2.WinForms.Guna2NumericUpDown _priceNumeric=null!;private Guna.UI2.WinForms.Guna2NumericUpDown _stockNumeric=null!;private Guna.UI2.WinForms.Guna2ComboBox _categoryComboBox=null!;private Guna.UI2.WinForms.Guna2CheckBox _featuredCheckBox=null!;private Guna.UI2.WinForms.Guna2Button _cancelButton=null!;private Guna.UI2.WinForms.Guna2Button _saveButton=null!;private Label _nameCaption=null!;private Label _descriptionCaption=null!;private Label _priceCaption=null!;private Label _stockCaption=null!;private Label _categoryCaption=null!;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
        _borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
        _dragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
        _headerPanel = new Guna.UI2.WinForms.Guna2Panel();
        _titleLabel = new Label();
        _bodyPanel = new Guna.UI2.WinForms.Guna2Panel();
        _saveButton = new Guna.UI2.WinForms.Guna2Button();
        _cancelButton = new Guna.UI2.WinForms.Guna2Button();
        _featuredCheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
        _categoryComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
        _stockNumeric = new Guna.UI2.WinForms.Guna2NumericUpDown();
        _priceNumeric = new Guna.UI2.WinForms.Guna2NumericUpDown();
        _descriptionTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _titleTextBox = new Guna.UI2.WinForms.Guna2TextBox();
        _categoryCaption = new Label();
        _stockCaption = new Label();
        _priceCaption = new Label();
        _descriptionCaption = new Label();
        _nameCaption = new Label();
        _headerPanel.SuspendLayout();
        _bodyPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_stockNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_priceNumeric).BeginInit();
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
        _headerPanel.CustomizableEdges = customizableEdges17;
        _headerPanel.Dock = DockStyle.Top;
        _headerPanel.FillColor = Color.White;
        _headerPanel.Location = new Point(0, 0);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges18;
        _headerPanel.Size = new Size(620, 78);
        _headerPanel.TabIndex = 1;
        // 
        // _titleLabel
        // 
        _titleLabel.AutoSize = true;
        _titleLabel.BackColor = Color.Transparent;
        _titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.FromArgb(30, 34, 43);
        _titleLabel.Location = new Point(24, 22);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(102, 31);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Produto";
        // 
        // _bodyPanel
        // 
        _bodyPanel.Controls.Add(_saveButton);
        _bodyPanel.Controls.Add(_cancelButton);
        _bodyPanel.Controls.Add(_featuredCheckBox);
        _bodyPanel.Controls.Add(_categoryComboBox);
        _bodyPanel.Controls.Add(_stockNumeric);
        _bodyPanel.Controls.Add(_priceNumeric);
        _bodyPanel.Controls.Add(_descriptionTextBox);
        _bodyPanel.Controls.Add(_titleTextBox);
        _bodyPanel.Controls.Add(_categoryCaption);
        _bodyPanel.Controls.Add(_stockCaption);
        _bodyPanel.Controls.Add(_priceCaption);
        _bodyPanel.Controls.Add(_descriptionCaption);
        _bodyPanel.Controls.Add(_nameCaption);
        _bodyPanel.CustomizableEdges = customizableEdges15;
        _bodyPanel.Dock = DockStyle.Fill;
        _bodyPanel.FillColor = Color.White;
        _bodyPanel.Location = new Point(0, 78);
        _bodyPanel.Name = "_bodyPanel";
        _bodyPanel.Padding = new Padding(24);
        _bodyPanel.ShadowDecoration.CustomizableEdges = customizableEdges16;
        _bodyPanel.Size = new Size(620, 572);
        _bodyPanel.TabIndex = 0;
        // 
        // _saveButton
        // 
        _saveButton.BackColor = Color.Transparent;
        _saveButton.BorderRadius = 10;
        _saveButton.CustomizableEdges = customizableEdges1;
        _saveButton.FillColor = Color.FromArgb(145, 98, 57);
        _saveButton.Font = new Font("Segoe UI", 9F);
        _saveButton.ForeColor = Color.White;
        _saveButton.Location = new Point(476, 507);
        _saveButton.Name = "_saveButton";
        _saveButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
        _saveButton.Size = new Size(117, 38);
        _saveButton.TabIndex = 0;
        _saveButton.Text = "💾 Salvar";
        // 
        // _cancelButton
        // 
        _cancelButton.BackColor = Color.Transparent;
        _cancelButton.BorderRadius = 10;
        _cancelButton.CustomizableEdges = customizableEdges3;
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.FillColor = Color.Gray;
        _cancelButton.Font = new Font("Segoe UI", 9F);
        _cancelButton.ForeColor = Color.White;
        _cancelButton.Location = new Point(336, 507);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
        _cancelButton.Size = new Size(117, 38);
        _cancelButton.TabIndex = 1;
        _cancelButton.Text = "❌ Cancelar";
        // 
        // _featuredCheckBox
        // 
        _featuredCheckBox.AutoSize = true;
        _featuredCheckBox.BackColor = Color.Transparent;
        _featuredCheckBox.CheckedState.BorderRadius = 0;
        _featuredCheckBox.CheckedState.BorderThickness = 0;
        _featuredCheckBox.CheckedState.FillColor = Color.FromArgb(88, 52, 27);
        _featuredCheckBox.Font = new Font("Segoe UI", 9F);
        _featuredCheckBox.Location = new Point(24, 352);
        _featuredCheckBox.Name = "_featuredCheckBox";
        _featuredCheckBox.Size = new Size(140, 19);
        _featuredCheckBox.TabIndex = 2;
        _featuredCheckBox.Text = "Produto em destaque";
        _featuredCheckBox.UncheckedState.BorderRadius = 0;
        _featuredCheckBox.UncheckedState.BorderThickness = 0;
        _featuredCheckBox.UseVisualStyleBackColor = false;
        // 
        // _categoryComboBox
        // 
        _categoryComboBox.BackColor = Color.Transparent;
        _categoryComboBox.BorderColor = Color.FromArgb(226, 229, 236);
        _categoryComboBox.BorderRadius = 9;
        _categoryComboBox.CustomizableEdges = customizableEdges5;
        _categoryComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        _categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _categoryComboBox.FocusedColor = Color.Empty;
        _categoryComboBox.Font = new Font("Segoe UI", 9.5F);
        _categoryComboBox.ForeColor = Color.FromArgb(68, 88, 112);
        _categoryComboBox.ItemHeight = 30;
        _categoryComboBox.Location = new Point(24, 296);
        _categoryComboBox.Name = "_categoryComboBox";
        _categoryComboBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
        _categoryComboBox.Size = new Size(540, 36);
        _categoryComboBox.TabIndex = 3;
        // 
        // _stockNumeric
        // 
        _stockNumeric.BackColor = Color.Transparent;
        _stockNumeric.BorderColor = Color.FromArgb(226, 229, 236);
        _stockNumeric.BorderRadius = 9;
        _stockNumeric.CustomizableEdges = customizableEdges7;
        _stockNumeric.Font = new Font("Segoe UI", 9F);
        _stockNumeric.Location = new Point(306, 226);
        _stockNumeric.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        _stockNumeric.Name = "_stockNumeric";
        _stockNumeric.ShadowDecoration.CustomizableEdges = customizableEdges8;
        _stockNumeric.Size = new Size(258, 42);
        _stockNumeric.TabIndex = 4;
        _stockNumeric.UpDownButtonFillColor = Color.FromArgb(145, 98, 57);
        // 
        // _priceNumeric
        // 
        _priceNumeric.BackColor = Color.Transparent;
        _priceNumeric.BorderColor = Color.FromArgb(226, 229, 236);
        _priceNumeric.BorderRadius = 9;
        _priceNumeric.CustomizableEdges = customizableEdges9;
        _priceNumeric.DecimalPlaces = 2;
        _priceNumeric.Font = new Font("Segoe UI", 9F);
        _priceNumeric.Increment = new decimal(new int[] { 50, 0, 0, 131072 });
        _priceNumeric.Location = new Point(24, 226);
        _priceNumeric.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        _priceNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        _priceNumeric.Name = "_priceNumeric";
        _priceNumeric.ShadowDecoration.CustomizableEdges = customizableEdges10;
        _priceNumeric.Size = new Size(255, 42);
        _priceNumeric.TabIndex = 5;
        _priceNumeric.UpDownButtonFillColor = Color.FromArgb(145, 98, 57);
        _priceNumeric.Value = new decimal(new int[] { 1, 0, 0, 131072 });
        // 
        // _descriptionTextBox
        // 
        _descriptionTextBox.BackColor = Color.Transparent;
        _descriptionTextBox.BorderColor = Color.FromArgb(226, 229, 236);
        _descriptionTextBox.BorderRadius = 9;
        _descriptionTextBox.CustomizableEdges = customizableEdges11;
        _descriptionTextBox.DefaultText = "";
        _descriptionTextBox.FocusedState.BorderColor = Color.FromArgb(88, 52, 27);
        _descriptionTextBox.Font = new Font("Segoe UI", 9F);
        _descriptionTextBox.Location = new Point(24, 110);
        _descriptionTextBox.Multiline = true;
        _descriptionTextBox.Name = "_descriptionTextBox";
        _descriptionTextBox.PlaceholderText = "Descrição do produto";
        _descriptionTextBox.SelectedText = "";
        _descriptionTextBox.ShadowDecoration.CustomizableEdges = customizableEdges12;
        _descriptionTextBox.Size = new Size(540, 78);
        _descriptionTextBox.TabIndex = 6;
        // 
        // _titleTextBox
        // 
        _titleTextBox.BackColor = Color.Transparent;
        _titleTextBox.BorderRadius = 9;
        _titleTextBox.CustomizableEdges = customizableEdges13;
        _titleTextBox.DefaultText = "";
        _titleTextBox.Font = new Font("Segoe UI", 9F);
        _titleTextBox.Location = new Point(27, 38);
        _titleTextBox.Name = "_titleTextBox";
        _titleTextBox.PlaceholderText = "Nome do Produto";
        _titleTextBox.SelectedText = "";
        _titleTextBox.ShadowDecoration.CustomizableEdges = customizableEdges14;
        _titleTextBox.Size = new Size(537, 43);
        _titleTextBox.TabIndex = 7;
        // 
        // _categoryCaption
        // 
        _categoryCaption.BackColor = Color.Transparent;
        _categoryCaption.Location = new Point(38, 49);
        _categoryCaption.Name = "_categoryCaption";
        _categoryCaption.Size = new Size(100, 23);
        _categoryCaption.TabIndex = 8;
        // 
        // _stockCaption
        // 
        _stockCaption.BackColor = Color.Transparent;
        _stockCaption.Location = new Point(308, 200);
        _stockCaption.Name = "_stockCaption";
        _stockCaption.Size = new Size(100, 23);
        _stockCaption.TabIndex = 9;
        _stockCaption.Text = "Estoque";
        // 
        // _priceCaption
        // 
        _priceCaption.BackColor = Color.Transparent;
        _priceCaption.Location = new Point(26, 204);
        _priceCaption.Name = "_priceCaption";
        _priceCaption.Size = new Size(60, 23);
        _priceCaption.TabIndex = 10;
        _priceCaption.Text = "Preço";
        // 
        // _descriptionCaption
        // 
        _descriptionCaption.BackColor = Color.Transparent;
        _descriptionCaption.Location = new Point(29, 88);
        _descriptionCaption.Name = "_descriptionCaption";
        _descriptionCaption.Size = new Size(100, 14);
        _descriptionCaption.TabIndex = 11;
        _descriptionCaption.Text = "Descrição";
        // 
        // _nameCaption
        // 
        _nameCaption.BackColor = Color.Transparent;
        _nameCaption.Location = new Point(29, 13);
        _nameCaption.Name = "_nameCaption";
        _nameCaption.Size = new Size(55, 23);
        _nameCaption.TabIndex = 12;
        _nameCaption.Text = "Nome";
        // 
        // ProductDialog
        // 
        AcceptButton = _saveButton;
        BackColor = Color.FromArgb(245, 247, 251);
        CancelButton = _cancelButton;
        ClientSize = new Size(620, 650);
        Controls.Add(_bodyPanel);
        Controls.Add(_headerPanel);
        FormBorderStyle = FormBorderStyle.None;
        Name = "ProductDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Produto";
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        _bodyPanel.ResumeLayout(false);
        _bodyPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_stockNumeric).EndInit();
        ((System.ComponentModel.ISupportInitialize)_priceNumeric).EndInit();
        ResumeLayout(false);
    }
    private static void ConfigureCaption(Label l,string t,int x,int y){l.AutoSize=true;l.Text=t;l.Font=new Font("Segoe UI Semibold",8.5F);l.ForeColor=LibraryTheme.Text;l.Location=new Point(x,y);}private static void ConfigureText(Guna.UI2.WinForms.Guna2TextBox t,int x,int y,int w,int h,string ph){t.Location=new Point(x,y);t.Size=new Size(w,h);t.BorderRadius=9;t.BorderColor=LibraryTheme.Border;t.FocusedState.BorderColor=LibraryTheme.Accent;t.PlaceholderText=ph;t.Font=new Font("Segoe UI",9.5F);}
    private static void ConfigureButton(Guna.UI2.WinForms.Guna2Button b,string text,int x,int w,bool primary){b.Location=new Point(x,420);b.Size=new Size(w,42);b.Text=text;b.BorderRadius=9;b.FillColor=primary?LibraryTheme.Accent:Color.FromArgb(241,243,248);b.ForeColor=primary?Color.White:LibraryTheme.Text;b.Font=new Font("Segoe UI Semibold",9F);b.HoverState.FillColor=primary?LibraryTheme.AccentDark:Color.FromArgb(225,229,238);}
}
