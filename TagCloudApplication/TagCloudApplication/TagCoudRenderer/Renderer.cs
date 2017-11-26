using System.Collections.Generic;
using System.Drawing;
using TagCloud;

namespace TagCoudRenderer
{
    public class Renderer
    {
        private Color TagColor { get; }
        private Font TagFont { get; }
        private Size ImgSize { get; }

        public Renderer(Color color, Font font, Size imgSize)
        {
            TagColor = color;
            TagFont = font;
            ImgSize = imgSize;
        }

        public void Draw(string path, Dictionary<string, Rectangle> tagList)
        {
            
        }
    }
}
