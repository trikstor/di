using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication
{
    public interface IConfig
    {
        List<Brush> CloudBrushes { get; }
        Size ImgSize { get; }
        Point CloudCenter { get; }
        string FontName { get; }
        int MaxWordQuant { get; }
        int MinFontSize { get; }
        int MaxFontSize { get; }
    }
}
