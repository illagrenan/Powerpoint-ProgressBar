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

        public void PlusButtonClicked(int a, int b)
        {
            this.model.Add(a, b);
        }

        public IBarModel Model
        {
            get
            {
                return this.model;
            }
        }


        public void AddBarClicked()
        {
            this.Model.CreateStrippedPresentation();
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
    }
}
