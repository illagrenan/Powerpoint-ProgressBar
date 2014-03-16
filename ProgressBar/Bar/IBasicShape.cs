using ProgressBar.DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public interface IBasicShape
    {
        Microsoft.Office.Core.MsoAutoShapeType Type { get; set; }
        ShapeType ColorType { get; set; }
        float Left { get; set; }
        float Top { get; set; }
        float Width { get; set; }
        float Height { get; set; }

        
    }
}
