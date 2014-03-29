#region

using System;
using System.Collections.Generic;
using System.Drawing;
using ProgressBar.Bar;
using ProgressBar.DataStructs;
using ProgressBar.MVC;

#endregion

namespace ProgressBar.Model
{
    public interface IBarModel : IModel
    {
        event Action<IBar> BarCreatedEvent;
        event Action<List<IBar>> RegisteredBarsEvent;
        event Action BarRemovedEvent;
        event Action<IBar> BarThemeChangedEvent;
        event Action<IBar> BarResizedEvent;
        event Action<IPositionOptions> AlignmentOptionsChanged;
        event Action<Dictionary<ShapeType, Color>> ColorsSetuped;
        event Action<int[]> SizesSetuped;
        event Action<int> DefaultSizeSetuped;

        void Add(IBar barToAdd);

        void CreateStrippedPresentation();

        void RemoveBar();


        List<IBar> GetRegisteredBars();
        void RegisterBars();


        Color ForegroundDefaultColor();

        Color BackgroundDefaultColor();

        void ChangeTheme(IBar newBar);

        IBar GetCurrentBar();

        void Reposition(PositionOptions positionOptions);

        void Resize(int newSize);

        void SetupColors();

        void SetupSizes();

        void SetupDefaultSize();
    }
}