using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Adapter
{
    interface IPowerPointAdapter
    {
        float PresentationWidth();

        float PresentationHeight();

        void InsertShape(Shape s);

        List<Slide> VisibleSlides();

        int HiddenSlidesCount();

        List<Shape> AddinShapes();
    }
}
