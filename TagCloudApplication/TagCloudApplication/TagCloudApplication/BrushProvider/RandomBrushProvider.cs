using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication.BrushProvider
{
    public class RandomBrushProvider : IBrushProvider
    {
        private Random Random = new Random();
        
        public Brush GetColor(string words, List<Brush> cloudBrushes)
        {
            return cloudBrushes[Random.Next(0, cloudBrushes.Count)];
        }
    }
}
