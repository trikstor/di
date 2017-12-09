using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication.BrushProvider
{
    public interface IBrushProvider
    {
        Brush GetColor(string words, List<Brush> cloudBrushes);
    }
}
