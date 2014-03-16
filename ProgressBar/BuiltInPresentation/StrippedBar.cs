using ProgressBar.Bar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.BuiltInPresentation
{
    class StrippedBar : IBar
    {
        public PositionOptions GetPositionOptions()
        {
            throw new NotImplementedException();
        }

        public List<Microsoft.Office.Interop.PowerPoint.Shape> Render(int currentPosition, PresentationInfo ppp)
        {
            throw new NotImplementedException();
        }
    }
}
