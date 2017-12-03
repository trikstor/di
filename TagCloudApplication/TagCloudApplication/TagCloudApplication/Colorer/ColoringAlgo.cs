using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication.Coloring
{
    public class ColoringAlgo : IColorer
    {
        private Random Random = new Random();
        private List<Brush> AllBrushes { get; }

        public ColoringAlgo(Config config)
        {
            AllBrushes = config.CloudBrushes;
        }
        
        public Brush GetColor(string words)
        {
            return AllBrushes[Random.Next(0, AllBrushes.Count)];
        }
    }
}
