using System.Drawing;
using Autofac;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            var engine = Config.ConfigureContainer(Color.Black, new Font(options.Font, 0), new Size(options.ImgWidth, options.ImgHeight)).Resolve<Converter>();
            engine.FromTextToImg(options.InputPath, options.ImgPath);
        }
    }
}
