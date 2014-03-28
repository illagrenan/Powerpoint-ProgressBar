using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public interface ILocation
    {
        bool Enabled { get; set; }
        bool Checked { get; set; }
    }
}
