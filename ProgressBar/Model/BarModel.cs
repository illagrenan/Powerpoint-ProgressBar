using ProgressBar.BuiltInPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Model
{
    public class BarModel : IBarModel
    {
        public event Action<int> AddResultEvent;
        public event Action<Bar.IBar> BarCreatedEvent;

        public int Add(int a, int b)
        {
            int result = a + b;

            if (AddResultEvent != null)
                AddResultEvent(result);

            return result;
        }


        public void CreateStrippedPresentation()
        {
            StrippedBar sb = new StrippedBar();

            if (this.BarCreatedEvent != null)
            {
                this.BarCreatedEvent(sb);
            }
        }



    }
}
