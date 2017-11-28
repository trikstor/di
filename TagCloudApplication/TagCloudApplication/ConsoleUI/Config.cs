using System.Collections.Generic;
using System.Drawing;
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

            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>().WithParameter("center", new Point(250, 250));
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
            builder.RegisterType<Converter>().As<IConverter>();

            return builder.Build();
        }
    }
}
