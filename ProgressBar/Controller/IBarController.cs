using ProgressBar.Model;
using ProgressBar.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Controller
{
    public interface IBarController : IController<IBarModel>
    {
        void PlusButtonClicked(int a, int b);
        void AddBarClicked();
        void RemoveBarClicked();
        void ResizeBarClicked();
    }
}
