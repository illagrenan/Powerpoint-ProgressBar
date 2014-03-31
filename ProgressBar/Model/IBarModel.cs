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
        event Action<IPositionOptions> AlignmentOptionsChanged;

        event Action<IBar> BarCreated;
        event Action<IBar> BarInfoRetrieved;
        event Action BarRemoved;
        event Action<IBar> BarSizeChanged;
        event Action<List<IBar>> BarsRegistered;
        event Action<Dictionary<ShapeType, Color>> ColorsSet;
        event Action<int> DefaultSizeSet;
        event Action ExternalBarAdded;
        event Action<int[]> SizesSet;

        void Add(IBar barToAdd);

        void AddExternalBar(IBar ibb, IPositionOptions po);

        IEnumerable<IBar> GetRegisteredBars();

        void ChangeTheme(IBar newBar);

        void RegisterBars();

        void RemoveBar();
        void Reposition(PositionOptions positionOptions);

        void Resize(int newSize);

        void SaveBarTo();

        void SetupColors();

        void SetupDefaultSize();

        void SetupSizes();

        void DisableOnFirst();

        void RefreshBar();
    }
}