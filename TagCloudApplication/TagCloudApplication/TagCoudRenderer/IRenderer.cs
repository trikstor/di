using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud;

namespace TagCoudRenderer
{
    public interface IRenderer
    {
        void Draw(string path, List<WordRectanglePair> tagList);
    }
}
