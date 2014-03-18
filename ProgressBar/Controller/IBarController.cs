using ProgressBar.Model;
using ProgressBar.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Controller
{
    public interface IBarController : IController<IBarModel>
    {


        void RemoveBarClicked();
        void ResizeBarClicked();
        void GetRegistered();

        int[] GetSizes();

        int GetDefaultSize();

        System.Drawing.Color ForegroundDefaultColor();

        System.Drawing.Color BackgroundDefaultColor();

        void ChangeThemeClicked(string selectedTheme);

        void AddBarClicked(string selectedTheme);
    }
}
