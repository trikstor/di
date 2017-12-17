using System.Collections.Generic;

namespace TagCloudApplication
{
    public interface ITagCloudCreator
    {
        Result<IEnumerable<Tag>> CreateTags(Options options);
        void CreateAndSave(Options options);
    }
}
