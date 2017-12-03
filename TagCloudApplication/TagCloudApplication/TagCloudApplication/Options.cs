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
        
        [Option('q', "quantity", HelpText = "Максимальное кол-во слов в облаке тегов.")]
        public int MaxWordQuant { get; set; }
        
        [Option("minf", HelpText = "Минимальный размер шрифта.")]
        public int MinFontSize { get; set; }

        [Option("maxf", HelpText = "mМаксимальный размер шрифта.")]
        public int MaxFontSize { get; set; }
        
        [HelpOption]
        public string GetHelp()
        {
            return HelpText.AutoBuild(this);
        }
    }
}