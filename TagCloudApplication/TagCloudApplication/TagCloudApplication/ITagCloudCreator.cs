using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication
{
    public interface ITagCloudCreator
    {
        Dictionary<string, Font> Create(Dictionary<string, int> tagsCollection);
    }
}
