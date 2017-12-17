using System.Drawing;

namespace TagCloudApplication.Layouter
{
    public interface ILayouter
    {
        void SetFrame(Size frame);
        void SetCenter(Point center);
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
