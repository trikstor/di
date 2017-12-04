using System.Drawing;

namespace TagCloudApplication
{
    public class Tag
    {
        public string Word { get; }
        public Font Font { get; }
        public Rectangle Rectangle { get; }

        public Tag(string word, Font font, Rectangle rectangle)
        {
            Word = word;
            Font = font;
            Rectangle = rectangle;
        }
    }
}
