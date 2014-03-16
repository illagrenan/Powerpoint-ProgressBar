using ProgressBar.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.MVC
{
    public interface IController
    {
    }

    public interface IController<TModel> : IController where TModel : IModel
    {
        TModel Model { get; }
    }
}
