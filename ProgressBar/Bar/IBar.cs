#region

using System.Collections.Generic;
using ProgressBar.Model;

#endregion

namespace ProgressBar.Bar
{
    public interface IBar
    {
        IPositionOptions PositionOptions { get; set; }
        IBarInfo GetInfo();

        IEnumerable<IBasicShape> Render(int currentPosition, PresentationInfo ppp);
    }
}