using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudApplication.Filrters;

namespace TagCloudApplication.StatProvider
{
    public interface IStatisticsProvider
    {
        Dictionary<string, int> GetStatistic(string text, int maxWordQuant);
    }
}
