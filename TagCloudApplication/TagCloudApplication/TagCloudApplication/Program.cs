using System.Drawing;
using System.IO;
using Autofac;
using CommandLine;
using TagCloudApplication.StatProvider;

namespace TagCloudApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            args = new[]
            {
                "-i", "C:\\Users\\Антон\\Desktop\\di\\TagCloudApplication\\TagCloudApplication\\TagCloudApplication\\bin\\Debug\\text.txt",
                "-o", "C:\\Users\\Антон\\Desktop\\di\\TagCloudApplication\\TagCloudApplication\\TagCloudApplication\\bin\\Debug\\test.gif",
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

            var text = File.ReadAllText(options.InputPath);
            builder.Build().Resolve<ITagCloudCreator>().CreateAndSave(text, options);
        }
    }
}
