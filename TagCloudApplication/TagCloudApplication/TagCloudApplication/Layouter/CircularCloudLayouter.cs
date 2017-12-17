using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace TagCloudApplication.Layouter
{
    public class CircularCloudLayouter : ILayouter
    {
        private List<Rectangle> AllRectangles { get; } = new List<Rectangle>();
        private IEnumerator<Point> CloudSpiral { get; set; }
        private Point Center { get; set; }
        private Size Frame { get; set; }
        private int MaxRadius { get; set; }

        public void SetFrame(Size frame)
        {
            if (frame.Width <= 0 || frame.Height <= 0)
                throw new ArgumentException("Ширина и высота рамки должны быть положительными либо равны нулю.");
            Center = new Point(frame.Width / 2, frame.Height / 2);
            Frame = frame;
            if (frame.Width > frame.Height)
            {
                MaxRadius = DistanceBetweenPoints(Center, new Point(frame.Width, frame.Height / 2));
            }
            else
            {
                MaxRadius = DistanceBetweenPoints(Center, new Point(frame.Width / 2, frame.Height));
            }
        }

        public void SetCenter(Point center)
        {
            if (center.X <= 0 || center.Y <= 0)
                throw new ArgumentException("Координаты центра должны быть положительными либо равны нулю.");
            Center = center;
            Frame = new Size(center.X * 2, center.Y * 2);
            if (Frame.Width > Frame.Height)
            {
                MaxRadius = DistanceBetweenPoints(Center, new Point(Frame.Width, Frame.Height / 2));
            }
            else
            {
                MaxRadius = DistanceBetweenPoints(Center, new Point(Frame.Width / 2, Frame.Height));
            }
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Ширина и высота прямоугольника должны быть положительными либо равны нулю.");

            Rectangle currRect;
            CloudSpiral = new Spiral(0.5, Center).GetEnumerator();
            for (;;)
            {
                currRect = new Rectangle(GetPossiblePointForRectCenter(rectangleSize), rectangleSize);
                if (!AllRectangles.Any(rect => rect.IntersectsWith(currRect)))
                    break;
            }
            if (GetMaxDistanceFromPointToRectangle(Center, currRect) > MaxRadius)
                throw new ArgumentException("Облако выходит за рамку.");

            AllRectangles.Add(currRect);
            return currRect;
        }

        private Point GetPossiblePointForRectCenter(Size rectangleSize)
        {
            Point currCenter;
            if (AllRectangles.Count == 0)
            {
                currCenter = Center;
            }
            else
            {
                CloudSpiral.MoveNext();
                currCenter = CloudSpiral.Current;
            }
            return GetRectangleCenterOffset(currCenter, rectangleSize);
        }

        private Point GetRectangleCenterOffset(Point currPoint, Size rectangleSize)
        {
            var newPoint = new Point
            {
                X = currPoint.X - rectangleSize.Width / 2,
                Y = currPoint.Y - rectangleSize.Height / 2
            };

            return newPoint;
        }

        private int DistanceBetweenPoints(Point p1, Point p2)
        {
            return (int) Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X))
                                   + ((p1.Y - p2.Y) * (p1.Y - p2.Y)));
        }

        private int GetMaxDistanceFromPointToRectangle(Point point, Rectangle rectangle)
        {
            var corners = new List<Point>
            {
                rectangle.Location,
                new Point(rectangle.Location.X + rectangle.Width, rectangle.Location.Y),
                new Point(rectangle.Location.X, rectangle.Location.Y - rectangle.Height),
                new Point(rectangle.Location.X + rectangle.Width,
                    rectangle.Location.Y - rectangle.Height)
            };

            return corners.Max(p => DistanceBetweenPoints(p, point));
        }
    }
}