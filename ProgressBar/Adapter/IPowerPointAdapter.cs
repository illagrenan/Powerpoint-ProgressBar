#region

using System.Collections.Generic;
using Microsoft.Office.Interop.PowerPoint;

#endregion

namespace ProgressBar.Adapter
{
    internal interface IPowerPointAdapter
    {
        bool HasSlides { get; set; }
        float PresentationWidth();

        float PresentationHeight();

        void InsertShape(Shape s);

        List<Slide> VisibleSlides();

        int HiddenSlidesCount();

        List<Shape> AddInShapes();
    }
}