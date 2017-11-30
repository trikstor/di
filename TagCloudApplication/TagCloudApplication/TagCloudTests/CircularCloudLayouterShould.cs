using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using System.IO;
using TagCloud;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private CircularCloudLayouter Layout {get; set;}
        private Point Center { get; set; }

        [SetUp]
        public void SetUp()
        {
            Center = new Point(500, 500);
            Layout = new CircularCloudLayouter(Center);
        }

        [TestCase(-10, 5, "Coordinates must be positive or zero", TestName = "Create a new layout with negative cordinate(s)")]
        public void ThrowException_UncorrectParams(int x, int y, string exMessage)
        {
            Action res = () => { new CircularCloudLayouter(new Point(x, y)); };
            res.ShouldThrow<ArgumentException>().WithMessage(exMessage);
        }

        [TestCase(0, 5, "Size must be positive", 
            TestName = "Create a new rectangle with negative or zero size")]
        public void PutNextRectangle_ThrowException(int width, int height, string exMessage)
        {
            Action res = () => { Layout.PutNextRectangle(new Size(width, height));};
            res.ShouldThrow<ArgumentException>().WithMessage(exMessage);
        }

        [Test]
        public void PutNextRectangle_QuantityOfRectangles_EqualsQuantity()
        {
            var allRect = new List<Rectangle>();
            const int expectedQuantity = 5;
            var sizeOfRectangles = new Size[expectedQuantity];

            for (var i = 0; i < expectedQuantity; i++)
                sizeOfRectangles[i] = new Size(i + 1, i + 2);

            foreach (var size in sizeOfRectangles)
            {
                allRect.Add(Layout.PutNextRectangle(size));
            }

            allRect.Count.Should().Be(expectedQuantity);
        }

        public static bool RectanglesNotOverlap(List<Rectangle> rectangles)
        {
            return rectangles.All(currRect => !rectangles
            .Any(rect => rect.IntersectsWith(currRect) && rect.Size != currRect.Size));
        }

        [TestCase(5, TestName = "Few rectangles")]
        public void PutNextRectangle_NotOverlapOfRectangles(int expectedQuantity)
        {
            var allRect = FillCloudWithRandRect(5);
            RectanglesNotOverlap(allRect).Should().BeTrue();
        }

        private List<Rectangle> FillCloudWithRandRect(int expectedQuantity)
        {
            var allRect = new List<Rectangle>();
            var sizeOfRectangles = new Size[expectedQuantity];
            var rnd = new Random();

            for (var i = 0; i < expectedQuantity; i++)
                sizeOfRectangles[i] = new Size(
                    i + rnd.Next(10, 300), i + rnd.Next(10, 300));

                foreach (var size in sizeOfRectangles)
                {
                    allRect.Add(Layout.PutNextRectangle(size));
                }
            return allRect;
        }

        [Test]
        public void PutNextRectangle_OneRectangle_CenterOfRectСalibration()
        {
            Layout.PutNextRectangle(new Size(200, 100)).Location.Should().Be(new Point(400, 450));
        }

        private int DistanceBetweenPoints(Point p1, Point p2)
        {
            return (int)Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X))
                                  + ((p1.Y - p2.Y) * (p1.Y - p2.Y)));
        }

        private int MaxCenterEnvirons(List<Rectangle> rectangles)
        {
            return rectangles
                .Select(rect => DistanceBetweenPoints(rect.Location, Center))
                .Concat(new[] { 0 }).Max();
        }

        private int MaxRectDiagonal(List<Rectangle> rectangles)
        {
            return rectangles
                .Select(rect => DistanceBetweenPoints(
                    rect.Location, new Point(rect.X + rect.Width, rect.Y + rect.Height)))
                .Concat(new[] { 0 }).Max();
        }

        [Test]
        public void PutNextRectangle_ManyRectangles_CorrectLocation()
        {
            var allRect = new List<Rectangle>();
            for (var i = 0; i < 7; i++)
            {
                allRect.Add(Layout.PutNextRectangle(new Size(100, 100)));
            }
            (MaxCenterEnvirons(allRect) + MaxRectDiagonal(allRect))
                .Should().BeLessThan(352);
        }
    }
}