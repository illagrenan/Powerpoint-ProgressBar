#region

using System.Drawing;

#endregion

namespace ProgressBar.Bar
{
    public interface IBarInfo
    {
        string FriendlyName { get; }

        Image Image { get; }
    }
}