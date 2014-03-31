namespace ProgressBar.Bar
{
    public interface ILocation
    {
        bool Available { get; set; }
        bool Selected { get; set; }
    }
}