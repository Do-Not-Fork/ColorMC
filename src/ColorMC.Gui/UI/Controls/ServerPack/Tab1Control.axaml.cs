using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ColorMC.Core.Objs;
using ColorMC.Core.Objs.ServerPack;
using ColorMC.Gui.UIBinding;

namespace ColorMC.Gui.UI.Controls.ServerPack;

public partial class Tab1Control : UserControl
{
    public Tab1Control()
    {
        InitializeComponent();

        TextBox1.LostFocus += TextBox1_LostFocus;
    }

    private void TextBox1_LostFocus(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TextBox1.Text))
            return;

        if (TextBox1.Text.EndsWith("/"))
            return;

        TextBox1.Text += "/";
    }
}