using ProgressBar.Bar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Model
{
    public interface IPositionOptions
    {
        ILocation Bottom { get; set; }
        ILocation Top { get; set; }

        ILocation Right { get; set; }

        ILocation Left { get; set; }
    }
}
