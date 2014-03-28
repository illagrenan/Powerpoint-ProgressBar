using ProgressBar.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    class Location : ILocation
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

            this.Enabled = enabled;
            this.Checked = cheked;
        }

        public Location(bool cheked)
        {
            this.Checked = cheked;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public bool Checked
        {
            get;
            set;
        }
    }
}
