using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ProgressBar.CustomExceptions;
using ProgressBar.Illagrenan;
using ProgressBar.DataStructs;

namespace ProgressBar.ProgressShape
{
    internal abstract class PresentationBar
    {
        internal const string SHAPE_NAME = "PG_634722689";
        internal const string BAR_SUFFIX = "789_BAR";
        internal const string BG_SUFFIX = "789_BACK";
        public const int DEFAULT_HEIGHT = 5;
        public const int MAX_HEIGHT = 100;
        internal float StepWidth { get; set; }
        internal int CountOfSteps { get; set; }

        private static Color _foreground = Color.CornflowerBlue;
        private static Color _background = Color.LightGray;

        internal DataStructs.BarAlign Alignment { get; set; }
        private float _height = DEFAULT_HEIGHT;

        protected static volatile PresentationBar instance;
        protected static object syncRoot = new Object();
        public static bool DisableOnFirstSlide;

        internal static Color Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                _foreground = value;
            }
        }
        internal static Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }


        public float Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        internal void SetStepWidth(float slideWidth)
        {
            if (slideWidth <= 0)
            {
                throw new InvalidArgumentException("Width must be greater than zero.");
            }

            if (CountOfSteps <= 0)
            {
                throw new InvalidStateException("Cannot calculate with zero count of slides");
            }

            this.StepWidth = slideWidth / CountOfSteps;
        }

        internal static float GetTopMargin()
        {
            if (BarAlign.TOP == SolidBar.Instance.Alignment)
            {

                if (SlideProperties.Instance.Theme == Theme.DOTTED)
                {
                    return DottedBar.DEFAULT_TOP_MARGIN;
                }

                return 0;
            }
            else
            {
                var tempMargin = SlideProperties.Instance.Height - SolidBar.Instance.Height;

                if (SlideProperties.Instance.Theme == Theme.DOTTED)
                {
                    tempMargin -= DottedBar.DEFAULT_TOP_MARGIN;
                }

                return tempMargin;
            }
        }

        public static int BackgroundOLE
        {
            get
            {
                return ColorTranslator.ToOle(Background);

            }
        }

        public static int ForegroundOLE
        {
            get
            {
                return ColorTranslator.ToOle(Foreground);
            }
        }
    }
}
