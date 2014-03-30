using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using ProgressBar.Model;

namespace ProgressBar.Tag
{
    interface IBarTag
    {
        IPositionOptions PositionOptions { get; set; }
        Color ActiveColor { get; set; }
        Color InactiveColor { get; set; }

        int SizeSelectedItemIndex { get; set; }
        int ThemeSelectedItemIndex { get; set; }




        bool DisableFirstSlideChecked { get; set; }

        Bar.IBar IBar { get; set; }
    }
}
