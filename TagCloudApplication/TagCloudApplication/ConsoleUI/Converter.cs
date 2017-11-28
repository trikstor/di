using System;
using System.IO;
using TagCloud;
using TagCoudRenderer;
using TextReader;

namespace ConsoleUI
{
    public class Converter : IConverter
    {
        private IReader Reader { get; }
        private ILayouter TagCloudLayouter { get; }
        private IRenderer Renderer { get; }
        
        public Converter(IReader reader, ILayouter tagCloudLayouter, IRenderer renderer)
        {
            Reader = reader;
            TagCloudLayouter = tagCloudLayouter;
            Renderer = renderer;
        }
        
        public void FromTextToImg(string inputPath, string imagePath)
        {
            if(!Directory.Exists(imagePath))
                throw new ArgumentException("Неверный путь для изображения.");
            var tagsCollection = Reader.Read(inputPath);
            var tagRectangles = TagCloudLayouter.Create(tagsCollection);
            var bitmap = Renderer.Draw(tagRectangles);
            bitmap.Save(imagePath);
        }
    }
}