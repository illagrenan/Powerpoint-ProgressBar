using System.Drawing;
using ProgressBar.CustomExceptions;
using System;
using ProgressBar.DataStructs;
using ProgressBar.Illagrenan;

namespace ProgressBar.ProgressShape
{
    internal sealed class SolidBar : PresentationBar
    {
        public static PresentationBar Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SolidBar();
                        }
                    }
                }

                return instance;
            }
        }

    }
}
