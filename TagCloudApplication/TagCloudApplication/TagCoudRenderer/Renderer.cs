using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TagCloud;

namespace TagCoudRenderer
{
    public class Renderer
    {
        private Color TagColor { get; }
        private Font TagFont { get; }
        private Size ImgSize { get; }
        private Visualizer TagCloudVisualizer { get; }

        public Renderer(Point center, Color color, Font font, Size imgSize)
        {
            TagColor = color;
            TagFont = font;
            ImgSize = imgSize;
            TagCloudVisualizer = new Visualizer(center, imgSize);
        }

        public void Draw(string path, Dictionary<string, Rectangle> tagList)
        {
            if(!Directory.Exists(path))
                throw new ArgumentException("Неверный путь для изображения.");           
        }

        public int GetFontSize(Rectangle rectangle)
        {
            return rectangle.Height;
        }
        
    }
}
