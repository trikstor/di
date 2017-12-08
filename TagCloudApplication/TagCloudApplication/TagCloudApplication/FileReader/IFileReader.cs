using System.Collections.Generic;

namespace TagCloudApplication.StatProvider
{
    public interface IFileReader
    {
        IEnumerable<string> Read(string path);
    }
}
