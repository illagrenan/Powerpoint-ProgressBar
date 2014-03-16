using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.ProgressShape
{
    internal sealed class DottedBar : PresentationBar
    {

        public static int DEFAULT_TOP_MARGIN = 5;

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
                            instance = new DottedBar();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
