using ProgressBar.Bar;
using ProgressBar.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Model
{
    public interface IBarModel : IModel
    {
        event Action<int> AddResultEvent;
        event Action<IBar> BarCreatedEvent;
        int Add(int a, int b);

        void CreateStrippedPresentation();
    }
}
