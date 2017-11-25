using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class TagCloudCreator
    {
        public List<WordRectanglePair> Create(Dictionary<string, int> tagsCollection, ILayouter lauouter)
        {
            return tagsCollection.Select(tag =>
                    new WordRectanglePair(tag.Key, lauouter
                        .PutNextRectangle(GetRectangleSizeForWord(tag.Key.Length, tag.Value))))
                        .ToList();
        }

        private Size GetRectangleSizeForWord(int wordLength, int fontSize)
        {
            return new Size(fontSize + fontSize / 2, wordLength * fontSize);
        }
    }
}
