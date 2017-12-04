using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication.BrushProvider
{
    public interface IBrushProvider
    {
        Brush GetColor(string words);
    }
}
