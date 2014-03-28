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
        event Action<List<IBar>> RegisteredBarsEvent;
        event Action BarRemovedEvent;
        event Action<Bar.IBar> BarThemeChangedEvent;
        void Add(IBar barToAdd);

        void CreateStrippedPresentation();

        void RemoveBar();

        bool HasProgressBar();

        List<IBar> GetRegisteredBars();
        void RegisterBars();

        int[] GetSizes();

        int GetDefaultSize();

        System.Drawing.Color ForegroundDefaultColor();

        System.Drawing.Color BackgroundDefaultColor();

        void ChangeTheme(IBar t);

        IBar GetCurrentBar();

        event Action<IPositionOptions> AlignmentOptionsChanged;
    }
}
