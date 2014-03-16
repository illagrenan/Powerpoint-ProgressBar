using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProgressBar.Adapter
{
    class PowerPointAdapter : IPowerPointAdapter
    {
        private Microsoft.Office.Interop.PowerPoint.Application powerPointApp;
        private Bar.ShapeName sn;

        public PowerPointAdapter(Microsoft.Office.Interop.PowerPoint.Application powerPointApp, Bar.ShapeName sn)
        {
            this.powerPointApp = powerPointApp;
             this.sn = sn;
        }


        public float PresentationWidth()
        {
            if (this.VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Width;
        }

        public void InsertShape(Microsoft.Office.Interop.PowerPoint.Shape s)
        {
            throw new NotImplementedException();
        }

        public List<Slide> VisibleSlides()
        {
            List<Slide> s = new List<Slide>();

            foreach (Slide item in powerPointApp.ActivePresentation.Slides)
            {
                if (item.SlideShowTransition.Hidden == Microsoft.Office.Core.MsoTriState.msoTrue)
                {
                    continue;
                }

                s.Add(item);
            }

            return s;
        }

        public int HiddenSlidesCount()
        {
            throw new NotImplementedException();
        }


        public float PresentationHeight()
        {
            if (this.VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Height;
        }


        public List<Shape> AddinShapes()
        {
            List<Shape> s = new List<Shape>();

            foreach (Slide item in this.VisibleSlides())
            {

                foreach (Shape sh in item.Shapes)
                {

                    //if (this.)

                    if (this.sn.IsAddInShape(sh.Name))
                    {
                        s.Add(sh);
                    }

                }
            }

            return s;
        }
    }
}
