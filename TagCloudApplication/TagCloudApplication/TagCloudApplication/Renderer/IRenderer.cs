using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication.Renderer
{
    public interface IRenderer
    {
        Bitmap Draw(IEnumerable<Tag> tagList, Size imgSize, List<Brush> brushes);
    }
}
