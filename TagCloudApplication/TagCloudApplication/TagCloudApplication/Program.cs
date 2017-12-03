using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Core;
using CommandLine;
using TagCloudApplication.Filrters;
using TagCloudApplication.Parsers;

namespace TagCloudApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            args = new[]
            {
                "-i", "D:\\di\\TagCloudApplication\\TagCloudApplication\\TagCloudApplication\\bin\\Debug\\text.txt",
                "-o", "D:\\di\\TagCloudApplication\\TagCloudApplication\\TagCloudApplication\\bin\\Debug\\test.gif",
                "-f", "Arial",
                "-w", "1000",
                "-h", "1000",
                "-q", "100",
                "--minf", "8",
                "--maxf", "45"
            };

            var options = new Options();
            if(!Parser.Default.ParseArguments(args, options))
                return;

            var mystemPath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName
                , "mystem.exe");

            var builder = new ContainerBuilder();
            var assembly = typeof(Program).Assembly;
            builder.RegisterType<Config>().AsSelf().WithParameters(new List<Parameter>
            {
                new NamedParameter("cloudBrushes", new List<Brush> {Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue}),
                new NamedParameter("imgSize", new Size(options.ImgWidth, options.ImgHeight)),
                new NamedParameter("cloudCenter", new Point(options.ImgWidth / 2, options.ImgHeight / 2)),
                new NamedParameter("mystemPath", mystemPath),
                new NamedParameter("textParsers", new List<IParser>{new SimpleTextParser()}),
                new NamedParameter("textFilters", new List<IFilter>{new BoringWordsFilter(new List<string>{"и"})}),
                new NamedParameter("fontName", options.Font),
                new NamedParameter("maxWordQuant", options.MaxWordQuant),
                new NamedParameter("minFontSize", options.MinFontSize),
                new NamedParameter("maxFontSize", options.MaxFontSize)
            });
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().SingleInstance();

            builder.Build().Resolve<IConverter>().FromTextToImg(options.InputPath, options.ImgPath);
        }
    }
}
