using System.Drawing;
using Autofac;
using CommandLine;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            args = new[]
            {
                "-i", "test.txt",
                "-o", "test.png",
                "-f", "Arial",
                "-w", "500",
                "-h", "500",
            };

            var options = new Options();
            //if(!Parser.Default.ParseArguments(args, options))
            //    return;

            var engine = Config.ConfigureContainer(Color.Black, options.Font, new Size(options.ImgWidth, options.ImgHeight)).Resolve<IConverter>();
            engine.FromTextToImg(options.InputPath, options.ImgPath);
        }
    }
}
