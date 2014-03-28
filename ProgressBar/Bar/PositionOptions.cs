using ProgressBar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public sealed class PositionOptions : IPositionOptions
    {
        public ILocation Bottom
        {
            get;
            set;
        }

        public ILocation Top
        {
            get;
            set;
        }

        public ILocation Right
        {
            get;
            set;
        }

        public ILocation Left
        {
            get;
            set;
        }
    }
}
