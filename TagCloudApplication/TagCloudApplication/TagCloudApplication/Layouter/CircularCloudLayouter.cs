﻿using System;
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

        public CircularCloudLayouter(Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Coordinates must be positive or zero");
            Center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Size must be positive");

            Rectangle currRect;
            CloudSpiral = new Spiral(0.5, Center).GetEnumerator();
            for (;;)
            {
                currRect = new Rectangle(GetPossiblePointForRectCenter(rectangleSize), rectangleSize);
                if (!AllRectangles.Any(rect => rect.IntersectsWith(currRect)))
                    break;
            }
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
    }
}
