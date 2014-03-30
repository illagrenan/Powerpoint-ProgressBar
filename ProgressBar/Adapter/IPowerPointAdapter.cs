#region

using System.Collections.Generic;
using Microsoft.Office.Interop.PowerPoint;

#endregion

namespace ProgressBar.Adapter
{
    internal interface IPowerPointAdapter
    {
        bool HasSlides { get; set; }
        List<Shape> AddInShapes();
        float PresentationHeight();

        float PresentationWidth();

        List<Slide> VisibleSlides();
    }
}