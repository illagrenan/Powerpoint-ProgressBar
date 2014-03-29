#region

using ProgressBar.Model;

#endregion

namespace ProgressBar.Bar
{
    public sealed class PositionOptions : IPositionOptions
    {
        public ILocation Bottom { get; set; }

        public ILocation Top { get; set; }

        public ILocation Right { get; set; }

        public ILocation Left { get; set; }
    }
}