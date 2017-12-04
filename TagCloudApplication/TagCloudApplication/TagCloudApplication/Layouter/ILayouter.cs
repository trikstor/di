using System.Drawing;

namespace TagCloudApplication.Layouter
{
    public interface ILayouter
    {
        void SetLayouterSettings(Point center);
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
