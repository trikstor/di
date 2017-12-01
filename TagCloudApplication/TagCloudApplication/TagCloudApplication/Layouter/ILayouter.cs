using System.Drawing;

namespace TagCloudApplication.Layouter
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
