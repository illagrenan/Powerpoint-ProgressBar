using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgressBar.CustomExceptions;

namespace ProgressBar.Illagrenan
{
    public sealed class SlideProperties
    {
        private static volatile SlideProperties instance;
        private static object syncRoot = new Object();

        private float _width;

        public float Width
        {
            get
            {
                if (_width <= 0)
                {
                    throw new InvalidStateException("Current width is 0, maybe is not set.");
                }

                return _width;
            }

            set
            {
                _width = value;
            }
        }

        private float _height;

        public float Height
        {
            get
            {
                if (_height <= 0)
                {
                    throw new InvalidStateException("Current height is 0, maybe is not set.");
                }

                return _height;
            }

            set
            {
                _height = value;
            }
        }

        private SlideProperties() { }

        public static SlideProperties Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SlideProperties();
                    }
                }

                return instance;
            }
        }


        internal DataStructs.Theme Theme { get; set; }
    }
}
