namespace ProgressBar.Bar
{
    internal sealed class ShapeNameHelper
    {
        private const string BackgroundSuffix = "789_BACK";
        private const string ForegroundSuffix = "789_BAR";
        private const string ShapePrefix = "PG_634722689";


        public bool IsShapeAddInShape(string nameToCheck)
        {
            return nameToCheck.StartsWith(ShapePrefix);
        }

        public bool IsShapeForegroundShape(string nameToCheck)
        {
            return nameToCheck.EndsWith(ForegroundSuffix);
        }

        public bool IsShapeBackgroundShape(string nameToCheck)
        {
            return nameToCheck.EndsWith(BackgroundSuffix);
        }


        internal string GetBackgroundShapeName()
        {
            return string.Format("{0}{1}", ShapePrefix, BackgroundSuffix);
        }

        internal string GetForegroundShapeName()
        {
            return string.Format("{0}{1}", ShapePrefix, ForegroundSuffix);
        }
    }
}