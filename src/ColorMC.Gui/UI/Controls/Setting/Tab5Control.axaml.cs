using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using ColorMC.Core.Utils;
using ColorMC.Gui.Objs;
using ColorMC.Gui.UI.Windows;
using ColorMC.Gui.UIBinding;
using ColorMC.Gui.Utils.LaunchSetting;
using DynamicData;
using System;
using System.Collections.ObjectModel;

namespace ColorMC.Gui.UI.Controls.Setting;

public partial class Tab5Control : UserControl
{
    private SettingWindow Window;
    private readonly ObservableCollection<JavaDisplayObj> List = new();
    private readonly CrossFade CrossFade1 = new(TimeSpan.FromMilliseconds(100));
    public Tab5Control()
    {
        InitializeComponent();

        Button_SelectFile.Click += Button_SelectFile_Click;
        Button_Add.Click += Button_Add_Click;

        Button_R1.PointerExited += Button_R1_PointerLeave;
        Button_R.PointerEntered += Button_R_PointerEnter;

        Button_R1.Click += Button_R1_Click;

        Expander_R.ContentTransition = CrossFade1;

        DataGrid1.Items = List;
        DataGrid1.CellPointerPressed += DataGrid1_CellPointerPressed;

        LayoutUpdated += Tab5Control_LayoutUpdated;
    }

    private void DataGrid1_CellPointerPressed(object? sender,
        DataGridCellPointerPressedEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var java = DataGrid1.SelectedItem as JavaDisplayObj;
            if (java == null)
                return;

            if (e.PointerPressedEventArgs.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                new SettingFlyout(Window, java).ShowAt(this, true);
            }
        });
    }

    private void Tab5Control_LayoutUpdated(object? sender, EventArgs e)
    {
        DataGrid1.MakeTran();
        Expander_R.MakePadingNull();
    }

    private void Button_D1_Click(object? sender, RoutedEventArgs e)
    {
        var item = DataGrid1.SelectedItem as JavaDisplayObj;
        if (item == null)
        {
            Window.Info.Show(Localizer.Instance["SettingWindow.Tab5.Error1"]);
            return;
        }

        JavaBinding.RemoveJava(item.Name);
        Load();
    }

    private void Button_R1_Click(object? sender, RoutedEventArgs e)
    {
        Load();
    }

    private void Button_R1_PointerLeave(object? sender, PointerEventArgs e)
    {
        Expander_R.IsExpanded = false;
    }

    private void Button_R_PointerEnter(object? sender, PointerEventArgs e)
    {
        Expander_R.IsExpanded = true;
    }

    private void Button_Add_Click(object? sender, RoutedEventArgs e)
    {
        string name = TextBox1.Text;
        string local = TextBox2.Text;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(local))
        {
            Window.Info.Show(Localizer.Instance["SettingWindow.Tab5.Error2"]);
            return;
        }

        try
        {
            Window.Info1.Show(Localizer.Instance["SettingWindow.Tab5.Info1"]);

            var res = JavaBinding.AddJava(name, local);
            if (res.Item1 == null)
            {
                Window.Info1.Close();
                Window.Info.Show(res.Item2!);
                return;
            }

            TextBox1.Text = "";
            TextBox2.Text = "";
            Window.Info1.Close();

            Load();
        }
        finally
        {

        }
    }

    private async void Button_SelectFile_Click(object? sender, RoutedEventArgs e)
    {
        OpenFileDialog openFile = new()
        {
            Title = Localizer.Instance["SettingWindow.Tab5.Info2"],
            AllowMultiple = false,
            Filters = SystemInfo.Os == OsType.Windows ? new()
            {
                new FileDialogFilter()
                {
                    Name =  "javaw" ,
                    Extensions =new()
                    {
                        "exe"
                    }
                }
            } : new()
        };

        var file = await openFile.ShowAsync(Window);
        if (file?.Length > 0)
        {
            var item = file[0];
            TextBox2.Text = item;
        }
    }

    public void SetWindow(SettingWindow window)
    {
        Window = window;
    }

    public void Load()
    {
        List.Clear();
        List.AddRange(JavaBinding.GetJavas());
    }
}