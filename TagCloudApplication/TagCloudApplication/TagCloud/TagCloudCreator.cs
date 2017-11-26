using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class TagCloudCreator
    {
        public Dictionary<string, Rectangle> Create(Dictionary<string, int> tagsCollection, ILayouter lauouter)
        {
            return tagsCollection
                .ToDictionary(tag => tag.Key,
                tag => lauouter.PutNextRectangle(GetRectangleSizeForWord(tag.Key.Length, tag.Value)));
        }

        private Size GetRectangleSizeForWord(int wordLength, int fontSize)
        {
            return new Size(fontSize + fontSize / 2, wordLength * fontSize);
        }
    }
}
