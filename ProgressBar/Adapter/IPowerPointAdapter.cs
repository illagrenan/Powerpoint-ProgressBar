#region

using System.Collections.Generic;
using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.Tag;

#endregion

namespace ProgressBar.Adapter
{
    internal interface IPowerPointAdapter
    {
        bool HasSlides { get; set; }
        List<Shape> AddInShapes();

        IBarTag GetBarFromTag();

        bool HasBarInTags();

        void InsertShape(Shape s);

        float PresentationHeight();

        float PresentationWidth();
        void SavePresentationToTag(IBarTag bt);

        List<Slide> VisibleSlides();
    }
}