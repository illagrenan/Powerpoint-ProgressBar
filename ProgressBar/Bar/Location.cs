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

        public Location(bool enabled, bool cheked)
        {
            if (!enabled && cheked)
            {
                throw new InvalidArgumentException("Location cannot be disabled and checked.");
            }

            Enabled = enabled;
            Checked = cheked;
        }

        public Location(bool cheked)
        {
            Checked = cheked;
        }

        public bool Enabled { get; set; }

        public bool Checked { get; set; }
    }
}