#region

using ProgressBar.Controller;
using ProgressBar.Model;
using ProgressBar.MVC;

#endregion

namespace ProgressBar.View
{
    public interface IBarView : IView<IBarController, IBarModel>
    {
    }
}