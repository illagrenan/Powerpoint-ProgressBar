namespace ProgressBar.MVC
{
    public interface IController
    {
    }

    public interface IController<out TModel> : IController where TModel : IModel
    {
        TModel Model { get; }
    }
}