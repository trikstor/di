using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using TagCloudApplication.Layouter;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private CircularCloudLayouter Layouter {get; set;}
        private Point Center { get; set; }

        [SetUp]
        public void SetUp()
        {
            Center = new Point(500, 500);
            Layouter = new CircularCloudLayouter();
            Layouter.SetCenter(Center);
        }

        [TestCase(-10, 5, "Координаты центра должны быть положительными либо равны нулю.", 
            TestName = "Попытка создать облако с отрицательными координатами центральной точки.")]
        public void ThrowException_UncorrectParams(int x, int y, string exMessage)
        {
            Action res = () => { Layouter.SetCenter(new Point(x, y)); };
            res.ShouldThrow<ArgumentException>().WithMessage(exMessage);
        }

        [TestCase(0, 5, "Ширина и высота прямоугольника должны быть положительными либо равны нулю.", 
            TestName = "Попытка создать прямоугольник с равной нулю шириной и высотой.")]
        [TestCase(10, -5, "Ширина и высота прямоугольника должны быть положительными либо равны нулю.",
            TestName = "Попытка создать прямоугольник с равной отрицательной шириной и высотой.")]
        public void PutNextRectangle_ThrowException(int width, int height, string exMessage)
        {
            Action res = () => { Layouter.PutNextRectangle(new Size(width, height));};
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
                allRect.Add(Layouter.PutNextRectangle(size));
            }

            allRect.Count.Should().Be(expectedQuantity);
        }
        
        [Test]
        public void PutNextRectangle_OversizeCloud()
        {
            Layouter.SetFrame(new Size(500, 500));
            Action res = () => { Layouter.PutNextRectangle(new Size(600, 700));};
            res.ShouldThrow<ArgumentException>().WithMessage("Облако выходит за рамку.");
        }

        private bool RectanglesNotOverlap(List<Rectangle> rectangles)
        {
            return rectangles.All(currRect => !rectangles
            .Any(rect => rect.IntersectsWith(currRect) && rect.Size != currRect.Size));
        }

        [TestCase(5, TestName = "Добавление небольшого количества прямоугольников.")]
        public void PutNextRectangle_NotOverlapOfRectangles(int expectedQuantity)
        {
            Layouter.SetCenter(Center);
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
                    i + rnd.Next(10, 100), i + rnd.Next(10, 100));

                foreach (var size in sizeOfRectangles)
                {
                    allRect.Add(Layouter.PutNextRectangle(size));
                }
            return allRect;
        }

        [Test]
        public void PutNextRectangle_OneRectangle_CenterOfRectСalibration()
        {
            Layouter.PutNextRectangle(new Size(200, 100)).Location.Should().Be(new Point(400, 450));
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
                allRect.Add(Layouter.PutNextRectangle(new Size(100, 100)));
            }
            (MaxCenterEnvirons(allRect) + MaxRectDiagonal(allRect))
                .Should().BeLessThan(352);
        }
    }
}