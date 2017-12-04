using System.Collections.Generic;
using System.Drawing;
using TagCloudApplication.BrushProvider;
using TagCloudApplication.Layouter;

namespace TagCloudApplication.Renderer
{
    public class Renderer : IRenderer
    {
        private ILayouter Layouter { get; }
        private IBrushProvider Colorer { get; }

        public Renderer(IBrushProvider colorer, ILayouter layouter)
        {
            Layouter = layouter;
            Colorer = colorer;
        }
        
        public Bitmap Draw(IEnumerable<Tag> tagList, Size imgSize, List<Brush> brushes)
        {
            var bitmap = new Bitmap(imgSize.Width, imgSize.Height);
            using (var gr = Graphics.FromImage(bitmap))
            {
                foreach (var tag in tagList)
                    gr.DrawString(tag.Word, 
                        tag.Font, Colorer.GetColor(tag.Word, brushes), tag.Rectangle);
            }
            return bitmap;
        }      
    }
}
