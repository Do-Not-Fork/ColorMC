using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ColorMC.Core.Utils;
using ColorMC.Gui.UI.Windows;
using ColorMC.Gui.UIBinding;
using ColorMC.Gui.Utils.LaunchSetting;
using System.Collections.Generic;
using System.Linq;

namespace ColorMC.Gui.UI.Controls.Setting;

public partial class Tab2Control : UserControl
{
    private SettingWindow Window;
    public Tab2Control()
    {
        InitializeComponent();

        Button_Delete.Click += Button_Delete_Click;
        Button_SelectFile.Click += Button_SelectFile_Click;
        Button_Set.Click += Button_Set_Click;
        Button_Set1.Click += Button_Set1_Click;
        Button_Set2.Click += Button_Set2_Click;
        Button_Set3.Click += Button_Set3_Click;
        Button_Set4.Click += Button_Set4_Click;
        Button_Change.Click += Button_Change_Click;

        CheckBox1.Click += CheckBox1_Click;
        CheckBox2.Click += CheckBox2_Click;

        ComboBox1.Items = OtherBinding.GetWindowTranTypes();
        ComboBox2.Items = OtherBinding.GetLanguages();
    }

    private void CheckBox2_Click(object? sender, RoutedEventArgs e)
    {
        GuiConfigUtils.Config.RGB = CheckBox2.IsChecked == true;
        GuiConfigUtils.Save();
        Colors.Load();
    }

    private void Button_Set4_Click(object? sender, RoutedEventArgs e)
    {
        ConfigBinding.SetColor("#FF5ABED6", "#FFF4F4F5", "#88FFFFFF");
        ColorPicker1.Color = Colors.MainColor.ToColor();
        ColorPicker2.Color = Colors.BackColor.ToColor();
        ColorPicker3.Color = Colors.Back1Color.ToColor();
        Window.Info2.Show("������");
    }

    private void Button_Set3_Click(object? sender, RoutedEventArgs e)
    {
        ConfigBinding.SetColor(ColorPicker1.Color.ToString(), 
            ColorPicker2.Color.ToString(), ColorPicker3.Color.ToString());
        Window.Info2.Show(Localizer.Instance["SettingWindow.Tab2.Info4"]);
    }

    private async void Button_Set2_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TextBox1.Text))
        {
            Window.Info.Show("û��ѡ��ͼƬ");
            return;
        }
        Window.Info1.Show(Localizer.Instance["SettingWindow.Tab2.Info2"]);
        await ConfigBinding.SetBackPic(TextBox1.Text, (int)Slider1.Value);
        Window.Info1.Close();

        Window.Info2.Show(Localizer.Instance["SettingWindow.Tab2.Info4"]);
    }

    private void Button_Set1_Click(object? sender, RoutedEventArgs e)
    {
        Window.Info1.Show("��������͸��Ч��");
        ConfigBinding.SetBl(CheckBox1.IsChecked == true, ComboBox1.SelectedIndex);
        Window.Info1.Close();

        Window.Info2.Show("������");
    }

    private void Button_Change_Click(object? sender, RoutedEventArgs e)
    {
        var type = (LanguageType)ComboBox2.SelectedIndex;
        Window.Info1.Show(Localizer.Instance["SettingWindow.Tab2.Info1"]);
        LanguageHelper.Change(type);
        Window.Info1.Close();
    }

    private void CheckBox1_Click(object? sender, RoutedEventArgs e)
    {
        ComboBox1.IsEnabled = CheckBox1.IsChecked == true;
    }

    public void SetWindow(SettingWindow window)
    {
        Window = window;
    }

    public void Load()
    {
        var config = ConfigBinding.GetAllConfig();
        if (config.Item2 != null)
        {
            TextBox1.Text = config.Item2.BackImage;
            Slider1.Value = config.Item2.BackEffect;
            Slider2.Value = config.Item2.BackTran;
            CheckBox1.IsChecked = config.Item2.WindowTran;
            ComboBox1.SelectedIndex = config.Item2.WindowTranType;
            ComboBox2.SelectedIndex = (int)config.Item1.Language;
            ColorPicker1.Color = Colors.MainColor.ToColor();
            ColorPicker2.Color = Colors.BackColor.ToColor();
            ColorPicker3.Color = Colors.Back1Color.ToColor();
            CheckBox2.IsChecked = config.Item2.RGB;
            if (config.Item2.WindowTran)
            {
                ComboBox1.IsEnabled = true;
            }
            else
            {
                ComboBox1.IsEnabled = false;
            }
        }
    }

    private void Button_Set_Click(object? sender, RoutedEventArgs e)
    {
        ConfigBinding.SetBackTran((int)Slider2.Value);
        Window.Info2.Show(Localizer.Instance["SettingWindow.Tab2.Info4"]);
    }

    private async void Button_SelectFile_Click(object? sender, RoutedEventArgs e)
    {
        var file =  await Window.StorageProvider.OpenFilePickerAsync(new()
        {
            Title = Localizer.Instance["SettingWindow.Tab2.Info5"],
            AllowMultiple = false,
            FileTypeFilter = new List<FilePickerFileType>()
            {
                new FilePickerFileType("ͼƬ")
                {
                    Patterns = new List<string>()
                    {
                        "*.png",
                        "*.jpg",
                        "*.bmp"
                    }
                }
            }
        });

        if (file?.Any() == true)
        {
            var item = file[0];
            item.TryGetUri(out var uri);
            TextBox1.Text = uri!.OriginalString;

            Button_Set2_Click(sender, e);
        }
    }

    private void Button_Delete_Click(object? sender, RoutedEventArgs e)
    {
        TextBox1.Text = "";

        ConfigBinding.DeleteGuiImageConfig();
    }
}