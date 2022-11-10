﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMC.Core.Path;

public static class MCPath
{
    private const string Name = "minecraft";
    public static string BaseDir { get; set; }

    public static void InitPath(string dir)
    {
        BaseDir = dir + "/" + Name;

        Directory.CreateDirectory(BaseDir);

        AssetsPath.InitPath(BaseDir);
        LibrariesPath.InitPath(BaseDir);
        InstancesPath.InitPath(BaseDir);
    }
}