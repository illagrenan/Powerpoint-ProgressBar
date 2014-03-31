#region

using Microsoft.Office.Core;
using ProgressBar.DataStructs;

#endregion

namespace ProgressBar.Bar
{
    public sealed class BasicShape : IBasicShape
    {
        public ShapeType ColorType { get; set; }

        public float Height { get; set; }

        public float Left { get; set; }

        public float Top { get; set; }

        public MsoAutoShapeType Type { get; set; }
        public float Width { get; set; }
    }
}