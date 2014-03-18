using ProgressBar._CustomExceptions;
using ProgressBar.Bar;
using ProgressBar.BuiltInPresentation;
using ProgressBar.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Model
{
    public class BarModel : IBarModel
    {
        public event Action<Bar.IBar> BarCreatedEvent;
        public event Action<Bar.IBar> BarChangedEvent;
        public event Action BarRemovedEvent;

        private List<Bar.IBar> registeredBars = new List<Bar.IBar>();

        private bool hasBar = false;
        public event Action<List<Bar.IBar>> RegisteredBarsEvent;
        private IBar currentBar;

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

        public int[] GetSizes()
        {
            return Enumerable.Range(1, 30).ToArray();
        }


        public int GetDefaultSize()
        {
            return this.GetSizes()[(int)(this.GetSizes().Count() / 4)];
        }



        System.Drawing.Color IBarModel.ForegroundDefaultColor()
        {
            return System.Drawing.Color.SlateBlue;
        }

        System.Drawing.Color IBarModel.BackgroundDefaultColor()
        {
            return System.Drawing.Color.LightGray;
        }


        public bool HasProgressBar()
        {
            return this.hasBar;
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
                // TODO Explain why
                throw new NoRegisteredBarException();
            }

            return this.registeredBars;
        }

        public void Add(Bar.IBar barToAdd)
        {
            this.hasBar = true;
            this.currentBar = barToAdd;

            if (this.BarCreatedEvent != null)
            {
                this.BarCreatedEvent(barToAdd);
            }
        }

        public void ChangeTheme(Bar.IBar t)
        {
            throw new NotImplementedException();
        }

        public Bar.IBar GetCurrentBar()
        {
            if (this.hasBar == false)
            {
                throw new NoActiveBarException();
            }

            return this.currentBar;
        }



    }
}
