using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public interface IBar
    {
        IPositionOptions GetPositionOptions();
        IPositionOptions GetPositionOptions(IPositionOptions positionOptions);

        List<IBasicShape> Render(int currentPosition, PresentationInfo ppp);

        IBarInfo GetInfo();


    }
}
