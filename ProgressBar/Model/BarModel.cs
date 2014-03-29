using ProgressBar._CustomExceptions;
using ProgressBar.Bar;
using ProgressBar.BuiltInPresentation;
using ProgressBar.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProgressBar.Model
{
    public class BarModel : IBarModel
    {
        public event Action<Bar.IBar> BarCreatedEvent;
        public event Action<Bar.IBar> BarThemeChangedEvent;
        public event Action BarRemovedEvent;
        public event Action<IPositionOptions> AlignmentOptionsChanged;
        public event Action<IBar> BarResizedEvent;
        public event Action<Dictionary<DataStructs.ShapeType, System.Drawing.Color>> ColorsSetuped;
        public event Action<int[]> SizesSetuped;
        public event Action<int> DefaultSizeSetuped;


        private List<Bar.IBar> registeredBars = new List<Bar.IBar>();

        private bool hasBar = false;
        public event Action<List<Bar.IBar>> RegisteredBarsEvent;
        private IBar currentBar;

        private Dictionary<DataStructs.ShapeType, System.Drawing.Color> cols =
            new Dictionary<DataStructs.ShapeType, System.Drawing.Color>();

        public BarModel()
        {
            this.cols.Add(DataStructs.ShapeType.BACKGROUND, System.Drawing.Color.LightGray);
            this.cols.Add(DataStructs.ShapeType.PROGRESS_BAR, System.Drawing.Color.SlateBlue);
        }


        public void CreateStrippedPresentation()
        {
        }

        public void RemoveBar()
        {
            this.hasBar = false;
            this.currentBar = null;

            if (this.BarRemovedEvent != null)
            {
                this.BarRemovedEvent();
            }
        }


        private bool HasProgressBar()
        {
            throw new ObsoleteException();
        }

        public void RegisterBars()
        {
            this.registeredBars.Add(new DottedBar());
            this.registeredBars.Add(new StrippedBar());

            if (this.RegisteredBarsEvent != null)
            {
                RegisteredBarsEvent(this.registeredBars);
            }
        }

        public List<Bar.IBar> GetRegisteredBars()
        {
            if (this.registeredBars.Count() == 0)
            {
                throw new NoRegisteredBarException(
                    "All bars you want to user must be registered in method \"RegisterBars\". Currently no bars are registered.");
            }

            return this.registeredBars;
        }

        public void Add(Bar.IBar barToAdd)
        {
            // Remove old bar before adding new one
            if (this.hasBar)
            {
                this.RemoveBar();
                Debug.WriteLine("AddBarClicked: Removing old theme");
            }

            this.hasBar = true;
            this.currentBar = barToAdd;

            this.AlignmentOptionsChanged(this.GetCurrentBar().GetPositionOptions());
            this.BarCreatedEvent(barToAdd);
        }

        public void ChangeTheme(Bar.IBar newBar)
        {
            if (this.hasBar && (newBar.GetInfo().FriendlyName != this.GetCurrentBar().GetInfo().FriendlyName))
            {
                Debug.WriteLine(String.Format("Changing theme FROM=\"{0}\" TO=\"{1}\"",
                    this.GetCurrentBar().GetInfo().FriendlyName,
                    newBar.GetInfo().FriendlyName));

                this.RemoveBar();

                this.currentBar = newBar;
                this.hasBar = true;

                this.BarThemeChangedEvent(newBar);
            }
            else
            {
                Debug.WriteLine("Ignoring change theme event. Nothing changed");
            }
        }

        public Bar.IBar GetCurrentBar()
        {
            if (this.hasBar == false)
            {
                throw new NoActiveBarException();
            }

            return this.currentBar;
        }

        public void Reposition(PositionOptions positionOptions)
        {
            this.GetCurrentBar().GetPositionOptions().Top.Checked = positionOptions.Top.Checked;
            this.GetCurrentBar().GetPositionOptions().Right.Checked = positionOptions.Right.Checked;
            this.GetCurrentBar().GetPositionOptions().Bottom.Checked = positionOptions.Bottom.Checked;
            this.GetCurrentBar().GetPositionOptions().Left.Checked = positionOptions.Left.Checked;

            this.Add(this.GetCurrentBar());
            this.AlignmentOptionsChanged(this.GetCurrentBar().GetPositionOptions());
        }

        public void Resize(int newSize)
        {
            if (this.BarResizedEvent != null)
            {
                this.BarResizedEvent(this.GetCurrentBar());
            }
        }


        public void SetupColors()
        {
            if (this.ColorsSetuped != null)
            {
                this.ColorsSetuped(this.cols);
            }
        }

        public void SetupSizes()
        {
            var x = GetSizes();

            if (this.SizesSetuped != null)
            {
                this.SizesSetuped(x);
            }
        }

        private int[] GetSizes()
        {
            var x = Enumerable.Range(1, 30).ToArray();
            return x;
        }


        public void SetupDefaultSize()
        {
            int f = this.GetSizes()[(int) (this.GetSizes().Count()/4)];

            if (this.DefaultSizeSetuped != null)
            {
                this.DefaultSizeSetuped(f);
            }
        }


        public System.Drawing.Color ForegroundDefaultColor()
        {
            throw new NotImplementedException();
        }

        public System.Drawing.Color BackgroundDefaultColor()
        {
            throw new NotImplementedException();
        }
    }
}