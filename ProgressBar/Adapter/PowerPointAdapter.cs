using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.Adapter
{
    class PowerPointAdapter : IPowerPointAdapter
    {
        private Microsoft.Office.Interop.PowerPoint.Application powerPointApp;

        public PowerPointAdapter(Microsoft.Office.Interop.PowerPoint.Application powerPointApp)
        {
            this.powerPointApp = powerPointApp;
        }



        public int PresentationWidth()
        {
            throw new NotImplementedException();
        }

        public void InsertShape(Microsoft.Office.Interop.PowerPoint.Shape s)
        {
            throw new NotImplementedException();
        }
    }
}
