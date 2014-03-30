#region

using System.Drawing;

#endregion

namespace ProgressBar.Bar
{
    public interface IBarInfo
    {
        Image Image { get; }
        string FriendlyName { get; }
    }
}