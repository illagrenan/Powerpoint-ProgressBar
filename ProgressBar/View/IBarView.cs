using ProgressBar.Controller;
using ProgressBar.Model;
using ProgressBar.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.View
{

    public interface IBarView : IView<IBarController, IBarModel>
    {
    }
}
