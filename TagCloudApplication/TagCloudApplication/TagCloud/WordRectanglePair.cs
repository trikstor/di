using System.Drawing;

namespace TagCloud
{
    public class WordRectanglePair
    {
        public Rectangle Rectangle { get; }
        public string Word { get; }

        public WordRectanglePair(string word, Rectangle rectangle)
        {
            Rectangle = rectangle;
            Word = word;
        }
    }
}
