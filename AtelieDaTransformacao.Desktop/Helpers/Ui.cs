using Guna.UI2.WinForms;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Helpers;

public static class Ui
{
    public static Guna2Button Button(string text, EventHandler click, int width = 120, bool primary = false)
    {
        var b = new Guna2Button
        {
            Text = text,
            Width = width,
            Height = 40,
            BorderRadius = 9,
            BorderThickness = 0,
            FillColor = primary ? LibraryTheme.Accent : Color.FromArgb(241, 243, 248),
            ForeColor = primary ? Color.White : LibraryTheme.Text,
            Font = new Font("Segoe UI Semibold", 9F),
            Cursor = Cursors.Hand,
            Margin = new Padding(4)
        };
        b.HoverState.FillColor = primary ? LibraryTheme.AccentDark : Color.FromArgb(225, 229, 238);
        b.Click += click;
        return b;
    }

    public static Guna2TextBox SearchBox(string placeholder)
    {
        var t = new Guna2TextBox
        {
            Height = 40,
            BorderRadius = 9,
            BorderThickness = 1,
            BorderColor = LibraryTheme.Border,
            FocusedState = { BorderColor = LibraryTheme.Accent },
            Font = new Font("Segoe UI", 9.5F),
            PlaceholderText = placeholder,
            FillColor = Color.White,
            Dock = DockStyle.Fill,
            Margin = new Padding(0)
        };
        return t;
    }

    public static Label Label(string text, float size = 9F, bool bold = false)
        => new() { Text = text, AutoSize = true, Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular), ForeColor = LibraryTheme.Muted, Margin = new Padding(0, 0, 0, 6) };

    public static Guna2Panel Card()
        => new() { BackColor = LibraryTheme.Surface, BorderColor = LibraryTheme.Border, BorderThickness = 1, BorderRadius = 12, Padding = new Padding(18), Margin = new Padding(0, 0, 14, 14) };

    public static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = LibraryTheme.Border;
        grid.RowHeadersVisible = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.RowTemplate.Height = 42;
        grid.ColumnHeadersHeight = 42;
        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(248, 249, 252), ForeColor = Color.FromArgb(70, 75, 85), Font = new Font("Segoe UI Semibold", 9F), SelectionBackColor = Color.FromArgb(248, 249, 252), SelectionForeColor = Color.FromArgb(70, 75, 85), Padding = new Padding(8, 0, 8, 0) };
        grid.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(45, 48, 55), SelectionBackColor = Color.FromArgb(232, 237, 255), SelectionForeColor = Color.FromArgb(35, 55, 100), Padding = new Padding(8, 0, 8, 0) };
        grid.AutoGenerateColumns = true;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }
}
