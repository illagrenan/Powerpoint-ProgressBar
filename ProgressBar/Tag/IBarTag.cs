#region

using System.Drawing;
using ProgressBar.Bar;
using ProgressBar.Model;

#endregion

namespace ProgressBar.Tag
{
    public interface IBarTag
    {
        Color ActiveColor { get; set; }

        bool DisableFirstSlideChecked { get; set; }

        IBar Bar { get; set; }

        Color InactiveColor { get; set; }

        IPositionOptions PositionOptions { get; set; }
        int SizeSelectedItemIndex { get; set; }
        int ThemeSelectedItemIndex { get; set; }
    }
}