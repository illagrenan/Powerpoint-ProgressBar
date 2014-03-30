#region

using ProgressBar.CustomExceptions;

#endregion

namespace ProgressBar.Bar
{
    internal class Location : ILocation
    {
        public Location()
        {
        }

        public Location(bool available, bool selected)
        {
            if (!available && selected)
            {
                throw new InvalidArgumentException("Location cannot be disabled and checked.");
            }

            Available = available;
            Selected = selected;
        }

        public Location(bool selected)
        {
            Selected = selected;
        }

        public bool Available { get; set; }

        public bool Selected { get; set; }
    }
}