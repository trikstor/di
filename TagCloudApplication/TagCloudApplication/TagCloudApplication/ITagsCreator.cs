using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication
{
    public interface ITagsCreator
    {
        IEnumerable<Tag> Create(Dictionary<string, int> tagsCollection, int minFontSize, int maxFontSize, string fontName);
    }
}
