using ProgressBar.Bar;
using ProgressBar.CustomExceptions;
using ProgressBar.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Debug.WriteLine(string.Format("AddBarClicked theme=\"{0}\"", newTheme.GetInfo().Name));

            if (this.Model.HasProgressBar())
            {
                this.Model.RemoveBar();
                Debug.WriteLine("AddBarClicked: Removing old theme");
            }

            this.Model.Add(newTheme);
        }

        public void RemoveBarClicked()
        {
            Debug.WriteLine("RemoveBarClicked");
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


        public void ChangeThemeClicked(string selectedTheme)
        {
            IBar newTheme = GetBarByString(selectedTheme);

            Debug.WriteLine(string.Format("ChangeThemeClicked param=\"{0}\"", selectedTheme));

            if (this.Model.HasProgressBar() && (newTheme.GetInfo().Name != this.Model.GetCurrentBar().GetInfo().Name))
            {
                Debug.WriteLine(String.Format("Changing theme FROM=\"{0}\" TO=\"{1}\"",
                                                this.Model.GetCurrentBar().GetInfo().Name,
                                                newTheme.GetInfo().Name));

                this.Model.RemoveBar();
                this.Model.ChangeTheme(newTheme);
            }
            else
            {
                Debug.WriteLine("Ignoring change theme event. Nothing changed");
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
