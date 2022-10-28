using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ColorMC.UI.Views;

public partial class InfoControl : UserControl
{
    private Action<bool>? call;

    private CrossFade transition = new(TimeSpan.FromMilliseconds(300));

    public InfoControl()
    {
        InitializeComponent();

        Confirm.Click += Confirm_Click;
        Cancel.Click += Cancel_Click;
    }

    private void Cancel_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Cancel.IsEnabled = false;
        transition.Start(this, null, CancellationToken.None);

        call?.Invoke(false);
    }

    private void Confirm_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Confirm.IsEnabled = false;
        Cancel.IsEnabled = false;
        transition.Start(this, null, CancellationToken.None);

        call?.Invoke(true);
    }

    public void Show(string title, Action<bool> res)
    {
        Confirm.IsEnabled = true;
        Cancel.IsEnabled = true;
        Text.Text = title;
        call = res;

        transition.Start(null, this, cancellationToken: CancellationToken.None);
    }

    public void Show(string title)
    {
        Confirm.IsEnabled = true;
        Cancel.IsVisible = false;
        Text.Text = title;

        transition.Start(null, this, cancellationToken: CancellationToken.None);
    }
}
