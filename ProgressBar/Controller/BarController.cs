using ProgressBar.Bar;
using ProgressBar.CustomExceptions;
using ProgressBar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Controller
{
    class BarController : IBarController
    {
        private readonly IBarModel model;

        public BarController(IBarModel model)
        {
            this.model = model;
        }


        public IBarModel Model
        {
            get
            {
                return this.model;
            }
        }

        public void AddBarClicked(string selectedTheme)
        {
            IBar newTheme = GetBarByString(selectedTheme);

            if (this.Model.HasProgressBar())
            {
                this.Model.RemoveBar();
            }

            this.Model.Add(newTheme);
        }

        public void RemoveBarClicked()
        {
            this.Model.RemoveBar();
        }

        public void ResizeBarClicked()
        {
            throw new NotImplementedException();
        }

        public int[] GetSizes()
        {
            return this.Model.GetSizes();
        }


        public int GetDefaultSize()
        {
            return this.Model.GetDefaultSize();
        }


        public System.Drawing.Color ForegroundDefaultColor()
        {
            return this.Model.ForegroundDefaultColor();
        }

        public System.Drawing.Color BackgroundDefaultColor()
        {
            return this.Model.BackgroundDefaultColor();
        }


        public void ChangeTheme(string selectedTheme)
        {
            IBar newTheme = GetBarByString(selectedTheme);

            if (this.Model.HasProgressBar() && (newTheme.GetInfo().Name != this.Model.GetCurrentBar().GetInfo().Name))
            {
                this.Model.ChangeTheme(newTheme);
            }

        }

        private IBar GetBarByString(string selectedTheme)
        {
            IBar newTheme = null;

            foreach (var item in this.Model.GetRegisteredBars())
            {
                if (selectedTheme == item.GetInfo().Name)
                {
                    newTheme = item;
                }
            }

            if (newTheme == null)
            {
                // TODO Explain why
                throw new InvalidStateException();
            }
            return newTheme;
        }

        public void GetRegistered()
        {
            this.Model.RegisterBars();
        }
    }
}
