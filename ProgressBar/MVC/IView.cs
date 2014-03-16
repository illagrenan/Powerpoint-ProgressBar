using ProgressBar.MVC;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.MVC
{
    public interface IView
    {
    }

    public interface IView<TController, TModel> : IView
        where TController : IController
        where TModel : IModel
    {
        TController Controller { get; set; }
        void Register(TModel model);
        void Release(TModel model);
    }
}
