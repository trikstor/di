using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextReader.Filrters;

namespace TextReader
{
    public interface IReader
    {
        Dictionary<string, int> Read(string path, IFilter filters);
    }
}
