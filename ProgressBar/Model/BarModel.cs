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
            _colors.Add(ShapeType.Active, Color.Crimson);
        }

        public event Action<IPositionOptions> AlignmentOptionsChanged;
        public event Action<IBar> BarCreated;
        public event Action<IBar> BarInfoRetrieved;

        public event Action BarRemoved;

        public event Action<IBar> BarSizeChanged;

        public event Action<List<IBar>> BarsRegistered;

        public event Action<Dictionary<ShapeType, Color>> ColorsSet;

        public event Action<int> DefaultSizeSet;

        public event Action ExternalBarAdded;
        public event Action<int[]> SizesSet;

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

            BarCreated(barToAdd);
            AlignmentOptionsChanged(_currentBar.PositionOptions);
        }

        public void AddExternalBar(IBar ibb, IPositionOptions po)
        {
            _currentBar = ibb;
            _hasBar = true;
            _currentBar.PositionOptions = po;

            /*
             * Událost ExternalBarAdded kromě aktivace Add tlačítka
             * iteruje přes všechny ovládací prvky Ribbonu a povolí je.
             * Problém nastává u StrippedBaru, který mí Right a Left zakázané.
             * 
             * Nejdříve tedy aktivujeme všechny prvky na liště a teprve poté případně
             * zakážeme některá tlačítka pro zarovnání.
             */

            ExternalBarAdded();
            AlignmentOptionsChanged(GetCurrentBar().PositionOptions);
        }

        public IEnumerable<IBar> GetRegisteredBars()
        {
            if (!_registeredBars.Any())
            {
                throw new NoRegisteredBarException(
                    "All bars you want to user must be registered in method \"RegisterBars\". Currently no bars are registered.");
            }

            return _registeredBars;
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

                Add(newBar);
            }
            else
            {
                Debug.WriteLine("Ignoring change theme event. Nothing changed");
            }
        }

        public void RegisterBars()
        {
            _registeredBars.Add(new DottedBar());
            _registeredBars.Add(new StrippedBar());

            if (BarsRegistered != null)
            {
                BarsRegistered(_registeredBars);
            }
        }

        public void RemoveBar()
        {
            _hasBar = false;

            if (BarRemoved != null)
            {
                BarRemoved();
            }
        }

        public void Reposition(PositionOptions positionOptions)
        {
            GetCurrentBar().PositionOptions.Top.Selected = positionOptions.Top.Selected;
            GetCurrentBar().PositionOptions.Right.Selected = positionOptions.Right.Selected;
            GetCurrentBar().PositionOptions.Bottom.Selected = positionOptions.Bottom.Selected;
            GetCurrentBar().PositionOptions.Left.Selected = positionOptions.Left.Selected;

            Add(GetCurrentBar());
            AlignmentOptionsChanged(GetCurrentBar().PositionOptions);
        }

        public void Resize(int newSize)
        {
            Add(_currentBar);
        }


        public void SaveBarTo()
        {
            if (_hasBar == false)
            {
                throw new InvalidOperationException("Presentation has no bar.");
            }

            BarInfoRetrieved(GetCurrentBar());
        }

        public void SetupColors()
        {
            if (ColorsSet != null)
            {
                ColorsSet(_colors);
            }
        }

        public void SetupDefaultSize()
        {
            int defaultSize = GetSizes()[GetSizes().Count()/3];

            if (DefaultSizeSet != null)
            {
                DefaultSizeSet(defaultSize);
            }
        }

        public void SetupSizes()
        {
            var availableSizes = GetSizes();

            if (SizesSet != null)
            {
                SizesSet(availableSizes);
            }
        }

        public void DisableOnFirst()
        {
            Add(_currentBar);
        }

        private IBar GetCurrentBar()
        {
            if (_hasBar == false)
            {
                throw new NoActiveBarException();
            }

            return _currentBar;
        }

        private int[] GetSizes()
        {
            var x = Enumerable.Range(1, 30).ToArray();
            return x;
        }


        public void RefreshBar()
        {
            this.Add(_currentBar);
        }
    }
}