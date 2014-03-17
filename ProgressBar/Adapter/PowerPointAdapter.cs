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
        private Bar.ShapeNameHelper nameHelper;

        public PowerPointAdapter(Microsoft.Office.Interop.PowerPoint.Application powerPointApp, Bar.ShapeNameHelper nameHelper)
        {
            this.powerPointApp = powerPointApp;
            this.nameHelper = nameHelper;
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
            List<Slide> visibleSlides = new List<Slide>();

            foreach (Slide slide in powerPointApp.ActivePresentation.Slides)
            {
                if (slide.SlideShowTransition.Hidden == Microsoft.Office.Core.MsoTriState.msoTrue)
                {
                    continue;
                }

                visibleSlides.Add(slide);
            }

            return visibleSlides;
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


        public List<Shape> AddInShapes()
        {
            List<Shape> addInShapes = new List<Shape>();

            foreach (Slide slide in this.VisibleSlides())
            {
                foreach (Shape shape in slide.Shapes)
                {

                    if (this.nameHelper.IsShapeAddInShape(shape.Name))
                    {
                        addInShapes.Add(shape);
                    }
                }
            }

            return addInShapes;
        }
    }
}
