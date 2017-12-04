using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Core;
using CommandLine;
using TagCloudApplication.Filrters;
using TagCloudApplication.Parsers;
using TagCloudApplication.TextReader;

namespace TagCloudApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            args = new[]
            {
                "-i", "C:\\Users\\Acer\\Saved Games\\Desktop\\di\\TagCloudApplication\\TagCloudApplication\\TagCloudApplication\\bin\\Debug\\text.txt",
                "-o", "C:\\Users\\Acer\\Saved Games\\Desktop\\di\\TagCloudApplication\\TagCloudApplication\\TagCloudApplication\\bin\\Debug\\test.gif",
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

            var builder = new ContainerBuilder();
            var assembly = typeof(Program).Assembly;
            builder.Register(_ => new Config(new List<Brush> {Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue},
                new Size(options.ImgWidth, options.ImgHeight),
                new Point(options.ImgWidth / 2, options.ImgHeight / 2),
                options.Font,
                options.MaxWordQuant,
                options.MinFontSize,
                options.MaxFontSize)).SingleInstance();
            
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            builder.Build().Resolve<IConverter>().FromTextToImg(options.InputPath, options.ImgPath);
        }
    }
}
