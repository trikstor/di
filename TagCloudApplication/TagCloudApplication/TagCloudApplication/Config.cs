using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TagCloudApplication.Filrters;
using TagCloudApplication.Parsers;

namespace TagCloudApplication
{
    public class Config
    {
        public List<Brush> CloudBrushes { get; }
        public Size ImgSize { get; }
        public Point CloudCenter { get; }
        public string FontName { get; }
        public int MaxWordQuant { get; }
        public int MinFontSize { get; }
        public int MaxFontSize { get; }

        public Config(List<Brush> cloudBrushes, Size imgSize, Point cloudCenter, string fontName, int maxWordQuant,
            int minFontSize, int maxFontSize)
        {
            CloudBrushes = cloudBrushes;
            ImgSize = imgSize;
            CloudCenter = cloudCenter;
            FontName = fontName;
            MaxWordQuant = maxWordQuant;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
        }
    }
}
