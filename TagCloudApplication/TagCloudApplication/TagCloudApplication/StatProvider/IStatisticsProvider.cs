using System.Collections.Generic;

namespace TagCloudApplication.StatProvider
{
    public interface IStatisticsProvider
    {
        Result<Dictionary<string, int>> GetStatistic(string text, int maxWordQuant);
    }
}
