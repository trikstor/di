using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication.Layouter
{
    public interface ITagCloudCreator
    {
        Dictionary<string, Rectangle> Create(Dictionary<string, int> tagsCollection);
    }
}
