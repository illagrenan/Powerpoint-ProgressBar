#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.CustomExceptions;
using ProgressBar.Helper;
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

        public bool HasSlides
        {
            get { return (_powerPointApp.ActivePresentation.Slides.Count > 0); }
            set { throw new NotImplementedException(); }
        }

        public List<Shape> AddInShapes()
        {
            var addInShapes = new List<Shape>();

            foreach (Slide slide in VisibleSlides())
            {
                addInShapes.AddRange(
                    slide.Shapes.Cast<Shape>().Where(
                        shape => _nameHelper.IsShapeAddInShape(shape.Name)
                        )
                    );
            }

            return addInShapes;
        }

        public float PresentationHeight()
        {
            if (VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Height;
        }

        public float PresentationWidth()
        {
            if (VisibleSlides().Count <= 0)
            {
                throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
            }

            return VisibleSlides()[0].Master.Width;
        }


        public List<Slide> VisibleSlides()
        {
            return
                _powerPointApp.ActivePresentation.Slides.Cast<Slide>()
                    .Where(slide => slide.SlideShowTransition.Hidden != MsoTriState.msoTrue)
                    .ToList();
        }
    }
}