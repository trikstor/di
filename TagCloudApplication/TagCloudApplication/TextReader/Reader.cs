using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextReader.Filrters;

namespace TextReader
{
    public class Reader : IReader
    {
        public Dictionary<string, int> Read(string path, IFilter filters)
        {
            var result = new Dictionary<string, int>();
            using (var textReader = new StreamReader(path))
            {
                string currStr;
                while ((currStr = textReader.ReadLine()) != null)
                {

                }
            }
            return result;
        }

        private Dictionary<string, int> AddNewTagOrChangeQuantity(Dictionary<string, int> tags, string currString)
        {
            return tags;
        }
    }
}
