﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMC.Core.Config;

public record JvmConfigObj
{ 
    public string Name { get; set; }
    public string Local { get; set; }
}

public record GameConfigObj
{ 
    public string UUID { get; set; }
    public string Name { get; set; }
    public string Jvm { get; set; }
    public string Args { get; set; }
    public int MaxMem { get; set; }
    public int MinMem { get; set; }
}

public record ConfigObj
{
    public string Version { get; set; }
    public string MCPath { get; set; }
    
    public List<JvmConfigObj> JavaList { get; set; }
    public List<GameConfigObj> GameList { get; set; }

}
