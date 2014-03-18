using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public interface IBarInfo
    {
        Image Image { get; }
        string Name { get;  }
    }
}
