﻿using System.Drawing;
using Autofac;
using CommandLine;

namespace TagCloudApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            args = new[]
            {
                "-i", @"...text.txt",
                "-o", @"...test.jpg",
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

            var builder = new ContainerBuilder();
            var assembly = typeof(Program).Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            builder.Build().Resolve<ITagCloudCreator>().CreateAndSave(options);
        }
    }
}
