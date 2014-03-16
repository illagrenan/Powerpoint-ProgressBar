using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public sealed class BasicShape : IBasicShape
    {

        public Microsoft.Office.Core.MsoAutoShapeType Type { get; set; }
        public DataStructs.ShapeType ColorType { get; set; }

        public float Left { get; set; }

        public float Top { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }



    }
}
