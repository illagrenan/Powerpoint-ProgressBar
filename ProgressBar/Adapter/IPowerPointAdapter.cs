using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Adapter
{
    interface IPowerPointAdapter
    {
        int PresentationWidth();
        void InsertShape(Shape s);
    }
}
