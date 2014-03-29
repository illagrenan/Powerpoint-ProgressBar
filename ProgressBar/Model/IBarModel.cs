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
        event Action<Bar.IBar> BarResizedEvent;
        event Action<IPositionOptions> AlignmentOptionsChanged;
        event Action<Dictionary<DataStructs.ShapeType, System.Drawing.Color>> ColorsSetuped;
        event Action<int[]> SizesSetuped;
        event Action<int> DefaultSizeSetuped;

        void Add(IBar barToAdd);

        void CreateStrippedPresentation();

        void RemoveBar();



        List<IBar> GetRegisteredBars();
        void RegisterBars();




        System.Drawing.Color ForegroundDefaultColor();

        System.Drawing.Color BackgroundDefaultColor();

        void ChangeTheme(IBar newBar);

        IBar GetCurrentBar();




        void Reposition(PositionOptions positionOptions);

        void Resize(int newSize);

        void SetupColors();

        void SetupSizes();

        void SetupDefaultSize();
    }
}
