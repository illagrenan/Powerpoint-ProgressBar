#region

using Microsoft.Office.Core;
using ProgressBar.DataStructs;

#endregion

namespace ProgressBar.Bar
{
    public interface IBasicShape
    {
        MsoAutoShapeType Type { get; set; }
        ShapeType ColorType { get; set; }
        float Left { get; set; }
        float Top { get; set; }
        float Width { get; set; }
        float Height { get; set; }
    }
}