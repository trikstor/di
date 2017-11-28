using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ILayouter
    {
        Dictionary<string, Rectangle> Create(Dictionary<string, int> tagsCollection);
    }
}
