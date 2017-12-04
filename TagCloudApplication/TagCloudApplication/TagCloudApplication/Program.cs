using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Core;
using CommandLine;
using NUnit.Framework;
using TagCloudApplication.Filrters;
using TagCloudApplication.Layouter;
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
                "-i", "...text.txt",
                "-o", "...test.gif",
                "-f", "Arial",
                "-w", "1000",
                "-h", "1000",
                "-q", "50",
                "--minf", "8",
                "--maxf", "50"
            };

            var options = new Options();
            if(!Parser.Default.ParseArguments(args, options))
                return;

            var parsers = new List<IParser>
            {
                new SimpleTextParser()
            };
            var filters = new List<IFilter>
            {
                new BoringWordsFilter()
            };
            var builder = new ContainerBuilder();
            var assembly = typeof(Program).Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<Reader>().As<IReader>().WithParameters(new List<Parameter>
            {
                new NamedParameter("parsers", parsers),
                new NamedParameter("filters", filters)

            });
            builder.Build().Resolve<ITagCloudCreator>().Create(options);
        }
    }
}
