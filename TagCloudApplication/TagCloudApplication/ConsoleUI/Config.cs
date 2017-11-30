using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Core;
using TagCloud;
using TagCoudRenderer;
using TextReader;
using TextReader.Filrters;
using TextReader.Parsers;

namespace ConsoleUI
{
    public class Config
    {
        public static IContainer ConfigureContainer(Color color, string fontName, Size imgSize)
        {
            var builder = new ContainerBuilder();
            var testColor = new List<Brush> {Brushes.Blue};
            var center = new Point(imgSize.Width / 2, imgSize.Height / 2);

            builder.RegisterType<CircularCloudLayouter>().AsSelf().WithParameter("center", center);
            builder.RegisterType<TagCloudCreator>().As<ITagCloudCreator>().WithParameters(new List<Parameter>
            {
                new NamedParameter("imgSize", imgSize)
            });
            builder.RegisterType<NormalFormConverter>().AsSelf().WithParameter("mystemPath",
                Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName
                    , "mystem.exe"));
            builder.RegisterType<Reader>().As<IReader>().WithParameters(new List<Parameter>
            {
                new NamedParameter("textParsers", new List<IParser>{new SimpleTextParser()}),
                new NamedParameter("textFilters", null)
            });
            builder.RegisterType<Renderer>().As<IRenderer>().WithParameters(new List<Parameter>
            {
               new NamedParameter("color", testColor[0]),
               new NamedParameter("fontName", fontName),
               new NamedParameter("imgSize", imgSize)
            });
            builder.RegisterType<Converter>().AsSelf();

            return builder.Build();
        }
    }
}
