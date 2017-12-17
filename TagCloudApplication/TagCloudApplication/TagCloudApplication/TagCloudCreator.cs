using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloudApplication.FileReader;
using TagCloudApplication.ImgSaver;
using TagCloudApplication.Renderer;
using TagCloudApplication.StatProvider;

namespace TagCloudApplication
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private ITagsCreator TagsCreator { get; }
        private IRenderer Renderer { get; }
        private IStatisticsProvider StatProvider { get; }
        private IFileReader FileReader { get; }
        private IImgSaver ImgSaver { get; }

        public TagCloudCreator(IStatisticsProvider statProvider, IImgSaver imgSaver, 
            IFileReader fileReader, ITagsCreator tagsCreator, IRenderer renderer)
        {
            TagsCreator = tagsCreator;
            Renderer = renderer;
            StatProvider = statProvider;
            FileReader = fileReader;
            ImgSaver = imgSaver;
        }

        public Result<IEnumerable<Tag>> CreateTags(Options options)
        {
            var imgSize = new Size(options.ImgWidth, options.ImgHeight);
            
            return FileReader.GetTextWords(options.InputPath)
                .Then(text => StatProvider.GetStatistic(text, options.MaxWordQuant))
                .Then(tags => TagsCreator.Create(tags, imgSize, 
                    options.MinFontSize, options.MaxFontSize, options.Font))
                .RefineError("Возникла ошибка");
        }

        public void CreateAndSave(Options options)
        {
            var cloudBrushes = new List<Brush> { Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue };
            var imgSize = new Size(options.ImgWidth, options.ImgHeight);

            var result = FileReader.GetTextWords(options.InputPath)
                .Then(words => StatProvider.GetStatistic(words, options.MaxWordQuant))
                .Then(tags => TagsCreator.Create(tags, imgSize, 
                    options.MinFontSize, options.MaxFontSize, options.Font))
                .Then(trect => Renderer.Draw(trect, imgSize, cloudBrushes))
                .Then(img => ImgSaver.Save(img, options.ImgPath))
                .RefineError("Возникла ошибка");

            if (!result.IsSuccess)
                Console.WriteLine(result.Error);
            Console.ReadLine();

        }
    }
}
