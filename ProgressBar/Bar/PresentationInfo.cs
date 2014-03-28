using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public sealed class PresentationInfo
    {
        public float Height { get; set; }

        public float Width { get; set; }

        public int SlidesCount { get; set; }

        public float UserSize { get; set; }

        public bool DisableOnFirstSlide { get; set; }
    }
}
