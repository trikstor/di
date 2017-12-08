using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication.WordsNormalizer
{
    public interface INormalizer
    {
        string Normalize(string word);
    }
}
