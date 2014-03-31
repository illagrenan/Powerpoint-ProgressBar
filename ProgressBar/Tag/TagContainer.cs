#region

using System.Drawing;
using ProgressBar.Bar;
using ProgressBar.Model;

#endregion

namespace ProgressBar.Tag
{
    public class TagContainer : ITagContainer
    {
        public Color ActiveColor { get; set; }

        public bool DisableFirstSlideChecked { get; set; }

        public IBar Bar { get; set; }

        public Color InactiveColor { get; set; }

        public IPositionOptions PositionOptions { get; set; }
        public int SizeSelectedItemIndex { get; set; }

        public int ThemeSelectedItemIndex { get; set; }
    }
}