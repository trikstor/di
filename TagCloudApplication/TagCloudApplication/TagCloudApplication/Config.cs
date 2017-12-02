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
        public string MystemPath { get; }
        public List<IParser> TextParsers { get; }
        public List<IFilter> TextFilters { get; }
        public string FontName { get; }
        public int MaxWordQuant { get; }

        public Config(List<Brush> cloudBrushes, Size imgSize, Point cloudCenter, 
            string mystemPath, List<IParser> textParsers, List<IFilter> textFilters, string fontName, int maxWordQuant)
        {
            CloudBrushes = cloudBrushes;
            ImgSize = imgSize;
            CloudCenter = cloudCenter;
            MystemPath = mystemPath;
            TextParsers = textParsers;
            TextFilters = textFilters;
            FontName = fontName;
            MaxWordQuant = maxWordQuant;
        }
    }
}
