using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Bar
{
    public class BarInfo : IBarInfo
    {
        private System.Drawing.Image im;
        private string lab;

        public BarInfo(System.Drawing.Image im, string lab)
        {
            this.im = im;
            this.lab = lab;
        }

        public System.Drawing.Image Image
        {
            get
            {
                return this.im;
            }
        }

        public string Name
        {
            get
            {
                return this.lab;
            }
        }
    }
}
