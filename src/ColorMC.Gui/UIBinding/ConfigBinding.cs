﻿using ColorMC.Core.Net;
using ColorMC.Core.Objs;
using ColorMC.Core.Utils;
using ColorMC.Gui.Objs;
using ColorMC.Gui.Utils.LaunchSetting;
using System.Threading.Tasks;

namespace ColorMC.Gui.UIBinding;

public static class ConfigBinding
{
    public static bool LoadAuthDatabase(string dir)
    {
        return AuthDatabase.LoadData(dir);
    }

    public static bool LoadConfig(string dir)
    {
        return ConfigUtils.Load(dir, true);
    }

    public static bool LoadGuiConfig(string dir)
    {
        return GuiConfigUtils.Load(dir, true);
    }

    public static (ConfigObj, GuiConfigObj) GetAllConfig()
    {
        return (ConfigUtils.Config, GuiConfigUtils.Config);
    }

    public static void SetColor(string main, string back, string back1) 
    {
        GuiConfigUtils.Config.ColorMain = main;
        GuiConfigUtils.Config.ColorBack = back;
        GuiConfigUtils.Config.ColorTranBack = back1;
        GuiConfigUtils.Save();
        Colors.Load();
    }

    public static void DeleteGuiImageConfig()
    {
        App.RemoveImage();
        GuiConfigUtils.Config.BackImage = null;
        GuiConfigUtils.Save();
        App.OnPicUpdate();
    }

    public static async Task SetBackPic(string dir, int data) 
    {
        GuiConfigUtils.Config.BackEffect = data;
        GuiConfigUtils.Config.BackImage = dir;
        GuiConfigUtils.Save();

        if (!await App.LoadImage(dir, data))
        {
            App.RemoveImage();
            return;
        }

        App.OnPicUpdate();
    }

    public static void SetBackTran(int data)
    {
        GuiConfigUtils.Config.BackTran = data;
        GuiConfigUtils.Save();

        App.OnPicUpdate();
    }

    public static void SetBl(bool open, int type)
    {
        GuiConfigUtils.Config.WindowTranType = type;
        GuiConfigUtils.Config.WindowTran = open;
        GuiConfigUtils.Save();

        App.OnPicUpdate();
    }

    public static void SetHttpConfig(HttpObj obj)
    {
        ConfigUtils.Config.Http = obj;
        ConfigUtils.Save();

        BaseClient.Init();
    }

    public static void SetJvmArgConfig(JvmArgObj obj)
    {
        ConfigUtils.Config.DefaultJvmArg = obj;
        ConfigUtils.Save();

        BaseClient.Init();
    }

    public static void SetWindowSettingConfig(WindowSettingObj obj)
    {
        ConfigUtils.Config.Window = obj;
        ConfigUtils.Save();

        BaseClient.Init();
    }
}