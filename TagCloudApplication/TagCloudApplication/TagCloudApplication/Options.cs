using CommandLine;
using CommandLine.Text;

namespace TagCloudApplication
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Ввод.")]
        public string InputPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Вывод.")]
        public string ImgPath { get; set; }

        [Option('f', "Font", HelpText = "Шрифт.")]
        public string Font { get; set; }
        
        [Option('w', "width", HelpText = "Ширина изображения.")]
        public int ImgWidth { get; set; }

        [Option('h', "height", HelpText = "Высота изображения.")]
        public int ImgHeight { get; set; }

        [HelpOption]
        public string GetHelp()
        {
            return HelpText.AutoBuild(this);
        }
    }
}