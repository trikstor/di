﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud;

namespace TagCoudRenderer
{
    public interface IRenderer
    {
        Bitmap Draw(Dictionary<string, Rectangle> tagList);
    }
}
