using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication
{
    public interface ITagCloudCreator
    {
        IEnumerable<Tag> CreateTags(Options options);
        void CreateAndSave(Options options);
    }
}
