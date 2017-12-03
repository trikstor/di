using TagCloudApplication.Renderer;
using TagCloudApplication.TextReader;

namespace TagCloudApplication
{
    public class Converter : IConverter
    {
        private IReader Reader { get; }
        private ITagCloudCreator TagCloudTagCloudCreator { get; }
        private IRenderer Renderer { get; }
        
        public Converter(IReader reader, ITagCloudCreator tagCloudTagCloudCreator, IRenderer renderer)
        {
            Reader = reader;
            TagCloudTagCloudCreator = tagCloudTagCloudCreator;
            Renderer = renderer;
        }
        
        public void FromTextToImg(string inputPath, string imagePath)
        {
            var tagsCollection = Reader.Read(inputPath);
            var tagRectangles = TagCloudTagCloudCreator.Create(tagsCollection);
            var bitmap = Renderer.Draw(tagRectangles);
            bitmap.Save(imagePath);
        }
    }
}