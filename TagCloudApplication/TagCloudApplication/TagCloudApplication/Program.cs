using System.Drawing;
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
                "-i", "...text.txt",
                "-o", "...test.gif",
                "-f", "Arial",
                "-w", "500",
                "-h", "500",
                "-q", "50",
                "--minf", "8",
                "--maxf", "50"
            };

            var options = new Options();
            if(!Parser.Default.ParseArguments(args, options))
                return;

            var builder = new ContainerBuilder();
            var assembly = typeof(Program).Assembly;
            builder.Register(_ => new Point(options.ImgWidth / 2, options.ImgHeight / 2));
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            builder.Build().Resolve<ITagCloudCreator>().CreateAndSave(options);
        }
    }
}
