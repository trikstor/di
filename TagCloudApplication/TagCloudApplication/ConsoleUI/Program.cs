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
                "-i", "...text.txt",
                "-o", "...test.png",
                "-f", "Arial",
                "-w", "1000",
                "-h", "1000"
            };

            var options = new Options();
            if(!Parser.Default.ParseArguments(args, options))
                return;

            var engine = Config.ConfigureContainer(Color.Black, options.Font, new Size(options.ImgWidth, options.ImgHeight)).Resolve<Converter>();
            engine.FromTextToImg(options.InputPath, options.ImgPath);
        }
    }
}
