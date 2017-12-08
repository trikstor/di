using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication
{
    public interface ITagCloudCreator
    {
        IEnumerable<Tag> CreateTags(string text, Options options);
        void CreateAndSave(string text, Options options);
    }
}
