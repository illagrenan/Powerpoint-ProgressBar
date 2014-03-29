#region

using ProgressBar.Bar;

#endregion

namespace ProgressBar.Model
{
    public interface IPositionOptions
    {
        ILocation Bottom { get; set; }
        ILocation Top { get; set; }

        ILocation Right { get; set; }

        ILocation Left { get; set; }
    }
}