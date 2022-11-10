﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMC.Core.Http.Download;

public enum DownloadState
{ 
    Wait, Download, Init, Action, Done,
    Error,
}

public record DownloadItem
{
    public string Url { get; set; }
    public string Local { get; set; }
    public string SHA1 { get; set; }
    public long AllSize { get; set; }
    public long NowSize { get; set; }
    public DownloadState State { get; set; } = DownloadState.Init;
    public Action Later { get; set; }

    public Action Update { get; set; }
}