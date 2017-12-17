using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication
{
    public interface ITagsCreator
    {
        Result<IEnumerable<Tag>> Create(Dictionary<string, int> tagsCollection, Size frame, int minFontSize, int maxFontSize, string fontName);
    }
}
