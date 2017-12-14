using System.Collections.Generic;

namespace TagCloudApplication
{
    public interface ITagCloudCreator
    {
        Result<IEnumerable<Tag>> CreateTags(string text, Options options);
        void CreateAndSave(string text, Options options);
    }
}
