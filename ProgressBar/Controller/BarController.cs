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
        private readonly IBarModel model;

        public BarController(IBarModel model)
        {
            this.model = model;
        }


        public IBarModel Model
        {
            get { return model; }
        }

        public void AddBarClicked(string selectedTheme)
        {
            IBar newTheme = GetThemeByString(selectedTheme);

            Debug.WriteLine(string.Format("AddBarClicked theme=\"{0}\"", newTheme.GetInfo().FriendlyName));

            Model.Add(newTheme);
        }

        public void RemoveBarClicked()
        {
            Debug.WriteLine("RemoveBarClicked");
            Model.RemoveBar();
        }

        public void ResizeBarClicked()
        {
            throw new NotImplementedException();
        }


        public void ChangeThemeClicked(string selectedTheme)
        {
            IBar newTheme = GetThemeByString(selectedTheme);

            Debug.WriteLine(string.Format("ChangeThemeClicked param=\"{0}\"", selectedTheme));


            Model.ChangeTheme(newTheme);
        }

        public void GetRegistered()
        {
            Model.RegisterBars();
        }


        public void PositionOptionsChanged(bool top, bool right, bool bottom, bool left)
        {
            PositionOptions positionOptions = new PositionOptions();

            positionOptions.Top = new Location(top);
            positionOptions.Right = new Location(right);
            positionOptions.Bottom = new Location(bottom);
            positionOptions.Left = new Location(left);

            Model.Reposition(positionOptions);
        }

        public void ChangeSizeClicked(int newSize)
        {
            Model.Resize(newSize);
        }


        public void SetupColors()
        {
            Model.SetupColors();
        }


        void IBarController.SetupSizes()
        {
            Model.SetupSizes();
            Model.SetupDefaultSize();
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