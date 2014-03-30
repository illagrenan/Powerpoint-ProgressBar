#region

using Microsoft.Office.Core;
using ProgressBar.DataStructs;

#endregion

namespace ProgressBar.Bar
{
    public interface IBasicShape
    {
        ShapeType ColorType { get; set; }

        float Height { get; set; }

        float Left { get; set; }

        float Top { get; set; }

        MsoAutoShapeType Type { get; set; }
        float Width { get; set; }
    }
}