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
                "-i", "C:\\Users\\Антон\\Desktop\\di\\TagCloudApplication\\TagCloudApplication\\ConsoleUI\\bin\\Debug\\text.txt",
                "-o", "C:\\Users\\Антон\\Desktop\\di\\TagCloudApplication\\TagCloudApplication\\ConsoleUI\\bin\\Debug\\test.png",
                "-f", "Arial",
                "-w", "3000",
                "-h", "3000"
            };

            var options = new Options();
            if(!Parser.Default.ParseArguments(args, options))
                return;

            var engine = Config.ConfigureContainer(Color.Black, options.Font, new Size(options.ImgWidth, options.ImgHeight)).Resolve<Converter>();
            engine.FromTextToImg(options.InputPath, options.ImgPath);
        }
    }
}
