#region

using System;
using System.Diagnostics;
using ProgressBar.Bar;
using ProgressBar.CustomExceptions;
using ProgressBar.Model;

#endregion

namespace ProgressBar.Controller
{
    internal class BarController : IBarController
    {
        private readonly IBarModel _model;

        public BarController(IBarModel model)
        {
            _model = model;
        }


        public IBarModel Model
        {
            get { return _model; }
        }

        public void AddBarClicked(string selectedTheme)
        {
            IBar newTheme = GetThemeByString(selectedTheme);

            Debug.WriteLine(string.Format("AddBarClicked theme=\"{0}\"", newTheme.GetInfo().FriendlyName));

            Model.Add(newTheme);
        }

        public void BarDetected(IBar bar, IPositionOptions positionOptions)
        {
            Model.AddExternalBar(bar, positionOptions);
        }

        public void ChangeSizeClicked(int newSize)
        {
            Model.Resize(newSize);
        }

        public void ChangeThemeClicked(string selectedTheme)
        {
            IBar newTheme = GetThemeByString(selectedTheme);

            Debug.WriteLine(string.Format("ChangeThemeClicked param=\"{0}\"", selectedTheme));


            Model.ChangeTheme(newTheme);
        }

        void IBarController.SetupSizes()
        {
            Model.SetupSizes();
            Model.SetupDefaultSize();
        }

        public void PositionOptionsChanged(bool top, bool right, bool bottom, bool left)
        {
            var positionOptions = new PositionOptions
            {
                Top = new Location(top),
                Right = new Location(right),
                Bottom = new Location(bottom),
                Left = new Location(left)
            };

            Model.Reposition(positionOptions);
        }

        public void RemoveBarClicked()
        {
            Debug.WriteLine("RemoveBarClicked");
            Model.RemoveBar();
        }

        public void SaveBarToMetadata()
        {
            Model.SaveBarTo();
        }

        public void SetupColors()
        {
            Model.SetupColors();
        }

        public void SetupRegisteredBars()
        {
            Model.RegisterBars();
        }


        public void DisableOnFirstSlideClicked()
        {
            Model.DisableOnFirst();
        }

        private IBar GetThemeByString(string selectedTheme)
        {
            IBar newTheme = null;

            foreach (var item in Model.GetRegisteredBars())
            {
                if (selectedTheme == item.GetInfo().FriendlyName)
                {
                    newTheme = item;
                }
            }

            if (newTheme == null)
            {
                string message = String.Format("Given \"{0}\" is not valid registered theme", selectedTheme);
                throw new InvalidStateException(message);
            }

            return newTheme;
        }
    }
}