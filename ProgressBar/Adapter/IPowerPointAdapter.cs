#region

using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.Tag;

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

        bool HasBarInTags();

        IBarTag GetBarFromTag();

        void SaveTag(string key, string value);

        void SavePresentationToTag(IBarTag bt);
    }
}