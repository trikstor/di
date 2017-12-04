using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication
{
    public interface ITagCloudCreator
    {
        void Create(Options options);
    }
}
