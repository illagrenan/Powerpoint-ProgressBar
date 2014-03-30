#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using ProgressBar.Bar;
using ProgressBar.BuiltInPresentation;
using ProgressBar.CustomExceptions;
using ProgressBar.DataStructs;

#endregion

namespace ProgressBar.Model
{
    public class BarModel : IBarModel
    {
        private readonly Dictionary<ShapeType, Color> _colors =
            new Dictionary<ShapeType, Color>();

        private readonly List<IBar> _registeredBars = new List<IBar>();
        private IBar _currentBar;
        private bool _hasBar;

        public BarModel()
        {
            _colors.Add(ShapeType.Inactive, Color.LightGray);
            _colors.Add(ShapeType.Active, Color.SlateBlue);
        }

        public event Action<IBar> BarCreatedEvent;
        public event Action<IBar> BarThemeChangedEvent;
        public event Action BarRemovedEvent;
        public event Action<IPositionOptions> AlignmentOptionsChanged;
        public event Action<IBar> BarResizedEvent;
        public event Action<Dictionary<ShapeType, Color>> ColorsSetuped;
        public event Action<int[]> SizesSetuped;
        public event Action<int> DefaultSizeSetuped;
        public event Action<List<IBar>> RegisteredBarsEvent;


        public void CreateStrippedPresentation()
        {
        }

        public void RemoveBar()
        {
            _hasBar = false;
            _currentBar = null;

            if (BarRemovedEvent != null)
            {
                BarRemovedEvent();
            }
        }


        public void RegisterBars()
        {
            _registeredBars.Add(new DottedBar());
            _registeredBars.Add(new StrippedBar());

            if (RegisteredBarsEvent != null)
            {
                RegisteredBarsEvent(_registeredBars);
            }
        }

        public List<IBar> GetRegisteredBars()
        {
            if (!_registeredBars.Any())
            {
                throw new NoRegisteredBarException(
                    "All bars you want to user must be registered in method \"RegisterBars\". Currently no bars are registered.");
            }

            return _registeredBars;
        }

        public void Add(IBar barToAdd)
        {
            // Remove old bar before adding new one
            if (_hasBar)
            {
                RemoveBar();
                Debug.WriteLine("AddBarClicked: Removing old theme");
            }

            _hasBar = true;
            _currentBar = barToAdd;

            AlignmentOptionsChanged(GetCurrentBar().GetPositionOptions());
            BarCreatedEvent(barToAdd);
        }

        public void ChangeTheme(IBar newBar)
        {
            if (_hasBar && (newBar.GetInfo().FriendlyName != GetCurrentBar().GetInfo().FriendlyName))
            {
                Debug.WriteLine("Changing theme FROM=\"{0}\" TO=\"{1}\"", GetCurrentBar().GetInfo().FriendlyName,
                    newBar.GetInfo().FriendlyName);

                RemoveBar();

                _currentBar = newBar;
                _hasBar = true;

                BarThemeChangedEvent(newBar);
            }
            else
            {
                Debug.WriteLine("Ignoring change theme event. Nothing changed");
            }
        }

        public IBar GetCurrentBar()
        {
            if (_hasBar == false)
            {
                throw new NoActiveBarException();
            }

            return _currentBar;
        }

        public void Reposition(PositionOptions positionOptions)
        {
            GetCurrentBar().GetPositionOptions().Top.Selected = positionOptions.Top.Selected;
            GetCurrentBar().GetPositionOptions().Right.Selected = positionOptions.Right.Selected;
            GetCurrentBar().GetPositionOptions().Bottom.Selected = positionOptions.Bottom.Selected;
            GetCurrentBar().GetPositionOptions().Left.Selected = positionOptions.Left.Selected;

            Add(GetCurrentBar());
            AlignmentOptionsChanged(GetCurrentBar().GetPositionOptions());
        }

        public void Resize(int newSize)
        {
            if (BarResizedEvent != null)
            {
                BarResizedEvent(GetCurrentBar());
            }
        }


        public void SetupColors()
        {
            if (ColorsSetuped != null)
            {
                ColorsSetuped(_colors);
            }
        }

        public void SetupSizes()
        {
            var x = GetSizes();

            if (SizesSetuped != null)
            {
                SizesSetuped(x);
            }
        }


        public void SetupDefaultSize()
        {
            int f = GetSizes()[GetSizes().Count()/4];

            if (DefaultSizeSetuped != null)
            {
                DefaultSizeSetuped(f);
            }
        }


        public Color ForegroundDefaultColor()
        {
            throw new NotImplementedException();
        }

        public Color BackgroundDefaultColor()
        {
            throw new NotImplementedException();
        }

        private int[] GetSizes()
        {
            var x = Enumerable.Range(1, 30).ToArray();
            return x;
        }
    }
}