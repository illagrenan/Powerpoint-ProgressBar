namespace ProgressBar.MVC
{
    public interface IView
    {
    }

    public interface IView<TController, in TModel> : IView
        where TController : IController
        where TModel : IModel
    {
        TController Controller { get; set; }
        void Register(TModel model);
        void Release(TModel model);
    }
}