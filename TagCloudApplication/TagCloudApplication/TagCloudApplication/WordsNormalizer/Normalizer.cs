using System.IO;
using System.Linq;
using NHunspell;

namespace TagCloudApplication.WordsNormalizer
{
    public class Normalizer : INormalizer
    {
        private Hunspell Hunspell { get; }
        public Normalizer(string affPath = "dict\\ru_RU.aff", string dicPath = "dict\\ru_RU.dic")
        {
            if(!File.Exists(affPath) || !File.Exists(dicPath))
                throw new FileNotFoundException("Путь к .aff или .dic файлу не найден.");
            Hunspell = new Hunspell(affPath, dicPath);
        }

        public string Normalize(string word)
        {
            return Hunspell.Stem(word).FirstOrDefault() ?? word;
        }
    }
}
