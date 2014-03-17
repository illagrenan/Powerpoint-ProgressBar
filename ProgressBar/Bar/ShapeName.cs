using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    internal sealed class ShapeNameHelper
    {
        private string SHAPE_PREFIX = "PG_634722689";
        private string FOREGROUND_SUFFIX = "789_BAR";
        private string BACKGROUND_SUFFIX = "789_BACK";


        public bool IsShapeAddInShape(string nameToCheck)
        {
            return nameToCheck.StartsWith(this.SHAPE_PREFIX);
        }

        public bool IsShapeForegroundShape(string nameToCheck)
        {
            return nameToCheck.EndsWith(this.FOREGROUND_SUFFIX);
        }

        public bool IsShapeBackgroundShape(string nameToCheck)
        {
            return nameToCheck.EndsWith(this.BACKGROUND_SUFFIX);
        }


        internal string GetBackgroundShapeName()
        {
            return string.Format("{0}{1}", SHAPE_PREFIX, BACKGROUND_SUFFIX);
        }

        internal string GetForegroundShapeName()
        {
            return string.Format("{0}{1}", SHAPE_PREFIX, FOREGROUND_SUFFIX);
        }
    }
}
