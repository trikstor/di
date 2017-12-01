using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication.Renderer
{
    public interface IRenderer
    {
        Bitmap Draw(Dictionary<string, Rectangle> tagList);
    }
}
