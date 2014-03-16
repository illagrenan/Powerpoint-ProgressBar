using ProgressBar.BuiltInPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Model
{
    public class BarModel : IBarModel
    {
        public event Action<Bar.IBar> BarCreatedEvent;
        public event Action BarRemovedEvent;

        public void CreateStrippedPresentation()
        {
            StrippedBar strippedBar = new StrippedBar();

            if (this.BarCreatedEvent != null)
            {
                this.BarCreatedEvent(strippedBar);
            }
        }


        public int Add(int a, int b)
        {
            throw new NotImplementedException();
        }


        public void RemoveBar()
        {
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
    }
}
