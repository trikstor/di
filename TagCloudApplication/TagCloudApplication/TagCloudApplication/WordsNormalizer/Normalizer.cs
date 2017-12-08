using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHunspell;

namespace TagCloudApplication.WordsNormalizer
{
    public class Normalizer : INormalizer
    {
        private Hunspell Hunspell { get; }
        public Normalizer(string affPath = "dict\\ru_RU.aff", string dicPath = "dict\\ru_RU.dic")
        {
            Hunspell = new Hunspell(affPath, dicPath);
        }

        public string Normalize(string word)
        {
            return Hunspell.Stem(word).FirstOrDefault() ?? word;
        }
    }
}
