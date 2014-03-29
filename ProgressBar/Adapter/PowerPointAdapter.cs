#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.Bar;
using ProgressBar.CustomExceptions;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

#endregion

namespace ProgressBar.Adapter
{
    internal class PowerPointAdapter : IPowerPointAdapter
    {
        private readonly ShapeNameHelper _nameHelper;
        private readonly Application _powerPointApp;

        public PowerPointAdapter(Application powerPointApp, ShapeNameHelper nameHelper)
        {
            _powerPointApp = powerPointApp;
            _nameHelper = nameHelper;
        }

        public float PresentationWidth()
        {
            if (VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Width;
        }

        public void InsertShape(Shape s)
        {
            throw new NotImplementedException();
        }

        public List<Slide> VisibleSlides()
        {
            return
                _powerPointApp.ActivePresentation.Slides.Cast<Slide>()
                    .Where(slide => slide.SlideShowTransition.Hidden != MsoTriState.msoTrue)
                    .ToList();
        }

        public int HiddenSlidesCount()
        {
            throw new NotImplementedException();
        }


        public float PresentationHeight()
        {
            if (VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Height;
        }


        public List<Shape> AddInShapes()
        {
            List<Shape> addInShapes = new List<Shape>();

            foreach (Slide slide in VisibleSlides())
            {
                foreach (Shape shape in slide.Shapes)
                {
                    if (_nameHelper.IsShapeAddInShape(shape.Name))
                    {
                        addInShapes.Add(shape);
                    }
                }
            }

            return addInShapes;
        }


        public bool HasSlides
        {
            get { return (_powerPointApp.ActivePresentation.Slides.Count > 0); }
            set { throw new NotImplementedException(); }
        }
    }
}