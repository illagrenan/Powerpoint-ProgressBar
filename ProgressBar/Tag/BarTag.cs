using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Tag
{
    class BarTag : IBarTag
    {

        public Model.IPositionOptions PositionOptions { get; set; }

        public System.Drawing.Color ActiveColor { get; set; }

        public System.Drawing.Color InactiveColor { get; set; }

        public int SizeSelectedItemIndex { get; set; }

        public int ThemeSelectedItemIndex { get; set; }


        public bool DisableFirstSlideChecked { get; set; }

        public Bar.IBar IBar { get; set; }
    }
}
