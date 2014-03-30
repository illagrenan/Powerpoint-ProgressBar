#region

using ProgressBar.Bar;
using ProgressBar.Model;
using ProgressBar.MVC;

#endregion

namespace ProgressBar.Controller
{
    public interface IBarController : IController<IBarModel>
    {
        void RemoveBarClicked();
        void SetupRegisteredBars();

        void SetupSizes();

        void ChangeThemeClicked(string selectedTheme);

        void AddBarClicked(string selectedTheme);

        void PositionOptionsChanged(bool topChecked, bool rightChecked, bool bottomChecked, bool leftChecked);

        void ChangeSizeClicked(int newSize);

        void SetupColors();


        void SaveBarToMetadata();

        void BarDetected(IBar bar, IPositionOptions positionOptions);

        void DisableOnFirstSlideClicked();
    }
}