using ProgressBar.Bar;
using ProgressBar.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Model
{
    public interface IBarModel : IModel
    {
        event Action<IBar> BarCreatedEvent;
        event Action BarRemovedEvent;
        int Add(int a, int b);

        void CreateStrippedPresentation();

        void RemoveBar();


        int[] GetSizes();

        int GetDefaultSize();

        System.Drawing.Color ForegroundDefaultColor();

        System.Drawing.Color BackgroundDefaultColor();
    }
}
