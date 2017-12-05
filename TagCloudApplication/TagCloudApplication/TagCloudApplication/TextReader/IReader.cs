﻿using System.Collections.Generic;

namespace TagCloudApplication.TextReader
{
    public interface IReader
    {
        Dictionary<string, int> Read(string path, int maxWordQuant, string affPath, string dicPath);
    }
}
