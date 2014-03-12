using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.CustomExceptions;
using ProgressBar.DataStructs;
using ProgressBar.Illagrenan;
using ProgressBar.ProgressShape;


namespace ProgressBar.RunningPresentation
{
    internal class Presentation
    {
        private Microsoft.Office.Interop.PowerPoint.Application _powerPointApp;
        internal bool HasProgressBar = false;
        internal ProgressBar.DataStructs.Theme currentTheme = ProgressBar.DataStructs.Theme.SOLID;

        internal Presentation(Microsoft.Office.Interop.PowerPoint.Application powerPointApp)
        {
            this._powerPointApp = powerPointApp;
        }

        internal Slides PresentationSlides
        {
            get
            {
                return _powerPointApp.ActivePresentation.Slides;
            }
        }

        internal List<Shape> AddedShapes
        {
            get
            {
                List<Shape> shapesToD = new List<Shape>();

                foreach (Slide item in PresentationSlides)
                {
                    foreach (Shape sh in item.Shapes)
                    {
                        if (sh.Name.StartsWith(SolidBar.SHAPE_NAME))
                        {
                            shapesToD.Add(sh);
                        }
                    }
                }

                return shapesToD;
            }
        }

        public int HiddenSlidesCount
        {
            get
            {
                int count = 0;

                foreach (Slide item in PresentationSlides)
                {
                    if (item.SlideShowTransition.Hidden == Microsoft.Office.Core.MsoTriState.msoTrue)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        internal void ResizeAllShapes()
        {
            if (HasProgressBar == false)
            {
                return;
            }

            foreach (Shape item in AddedShapes)
            {
                item.Height = SolidBar.Instance.Height;

                if (currentTheme == ProgressBar.DataStructs.Theme.DOTTED)
                {
                    item.Width = SolidBar.Instance.Height;
                }
            }
        }

        private void TreatNoSlidesFoundError()
        {
            MessageBox.Show("No slides found",
                            "Sorry, no slides found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        internal void GenerateProgressBar()
        {
            if (PresentationSlides.Count == 0 || HiddenSlidesCount >= PresentationSlides.Count)
            {
                TreatNoSlidesFoundError();
                return;
            }

            Microsoft.Office.Core.MsoAutoShapeType shapeBackground = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;
            Microsoft.Office.Core.MsoAutoShapeType shapeForeground = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;

            int stepCounter = 1;
            SolidBar.Instance.CountOfSteps = PresentationSlides.Count - HiddenSlidesCount;
            SolidBar.Instance.SetStepWidth(SlideWidth);
            SlideProperties.Instance.Height = SlideHeight;
            SlideProperties.Instance.Width = SlideWidth;
            float stepWidth = SolidBar.Instance.StepWidth;
            float topMargin = SolidBar.GetTopMargin();

            if (currentTheme == ProgressBar.DataStructs.Theme.DOTTED)
            {
                if (SolidBar.Instance.Alignment == BarAlign.TOP)
                {
                    topMargin = topMargin + 5;
                }
                else
                {
                    topMargin = topMargin - 5;
                }
            }

            foreach (Slide oneSlide in PresentationSlides)
            {
                Shape addedShapeBackground, addedShapeForeground;

                if (SolidBar.DisableOnFirstSlide &&
                        stepCounter == 1 &&
                        oneSlide.SlideShowTransition.Hidden == Microsoft.Office.Core.MsoTriState.msoFalse)
                {
                    stepCounter++;
                    continue;
                }

                if (oneSlide.SlideShowTransition.Hidden == Microsoft.Office.Core.MsoTriState.msoTrue)
                {
                    continue;
                }

                if (currentTheme == ProgressBar.DataStructs.Theme.DOTTED)
                {
                    int margin = ((int)SlideWidth - (int)(SolidBar.Instance.CountOfSteps * SolidBar.Instance.Height)) / SolidBar.Instance.CountOfSteps;

                    for (int i = 0; i < SolidBar.Instance.CountOfSteps; i++)
                    {
                        if ((stepCounter - 1) == i)
                        {
                            addedShapeBackground = oneSlide.Shapes.AddShape(shapeForeground,
                                                5 + (5 * i) + (i * SolidBar.Instance.Height), topMargin,
                                                SolidBar.Instance.Height,
                                                SolidBar.Instance.Height);


                            addedShapeBackground.Fill.ForeColor.RGB = SolidBar.BackgroundOLE;
                            addedShapeBackground.Line.Weight = 0;
                            addedShapeBackground.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                            addedShapeBackground.Name = SolidBar.SHAPE_NAME + SolidBar.BG_SUFFIX;
                        }
                        else
                        {
                            addedShapeForeground = oneSlide.Shapes.AddShape(shapeForeground,
                                                5 + (5 * i) + (i * SolidBar.Instance.Height), topMargin,
                                                SolidBar.Instance.Height,
                                                SolidBar.Instance.Height);

                            addedShapeForeground.Fill.ForeColor.RGB = SolidBar.ForegroundOLE;
                            addedShapeForeground.Line.Weight = 0;
                            addedShapeForeground.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                            addedShapeForeground.Name = SolidBar.SHAPE_NAME + SolidBar.BAR_SUFFIX;
                        }
                    }
                }
                else
                {
                    addedShapeBackground = oneSlide.Shapes.AddShape(shapeBackground,
                                                                 0,
                                                                 topMargin,
                                                                 SlideWidth,
                                                                 SolidBar.Instance.Height);

                    addedShapeForeground = oneSlide.Shapes.AddShape(shapeForeground,
                                                                    0,
                                                                    topMargin,
                                                                    stepWidth * stepCounter,
                                                                    SolidBar.Instance.Height);

                    addedShapeBackground.Name = SolidBar.SHAPE_NAME + SolidBar.BG_SUFFIX;
                    addedShapeForeground.Name = SolidBar.SHAPE_NAME + SolidBar.BAR_SUFFIX;

                    addedShapeBackground.Line.Weight = addedShapeForeground.Line.Weight = 0;
                    addedShapeBackground.Line.Visible = addedShapeForeground.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

                    addedShapeBackground.Fill.ForeColor.RGB = SolidBar.BackgroundOLE;
                    addedShapeForeground.Fill.ForeColor.RGB = SolidBar.ForegroundOLE;

                }

                stepCounter++;
            }

            HasProgressBar = true;
        }

        internal void RemoveAllAddInShapes()
        {
            foreach (Shape item in AddedShapes)
            {
                item.Delete();
            }

            HasProgressBar = false;
        }

        internal void ApplyTopMargin()
        {

            SlideProperties.Instance.Height = SlideHeight;
            SlideProperties.Instance.Width = SlideWidth;
            SlideProperties.Instance.Theme = currentTheme;

            foreach (Shape item in AddedShapes)
            {
                item.Top = SolidBar.GetTopMargin();
            }
        }

        public float SlideHeight
        {
            get
            {
                if (PresentationSlides.Count <= 0)
                {
                    throw new InvalidStateException("Height of slides is unknown. Presentation has no slides.");
                }

                return PresentationSlides[1].Master.Height;
            }
        }

        public float SlideWidth
        {
            get
            {
                if (PresentationSlides.Count <= 0)
                {
                    throw new InvalidStateException("Width of slides is unknown. Presentation has no slides.");
                }

                return PresentationSlides[1].Master.Width;
            }
        }

        internal void ColorForeground()
        {
            IEnumerable<Shape> query =
                from sc in AddedShapes
                where sc.Name.EndsWith(SolidBar.BAR_SUFFIX)
                select sc;

            foreach (Shape item in query)
            {
                item.Fill.ForeColor.RGB = SolidBar.ForegroundOLE;
            }
        }

        internal void ColorBacground()
        {
            IEnumerable<Shape> query =
                from sc in AddedShapes
                where sc.Name.EndsWith(SolidBar.BG_SUFFIX)
                select sc;

            foreach (Shape item in query)
            {
                item.Fill.ForeColor.RGB = SolidBar.BackgroundOLE;
            }
        }

        internal void ApplyLeftMargin()
        {

            int i = 0;
            float constantLeftMarginForRightAlign = 0;

            if (SolidBar.Instance.Alignment == BarAlign.RIGHT)
            {
                float widthOfDots =
                    (PresentationSlides.Count - HiddenSlidesCount) * SolidBar.Instance.Height;
                widthOfDots +=
                    (PresentationSlides.Count - HiddenSlidesCount) * DottedBar.DEFAULT_TOP_MARGIN;

                constantLeftMarginForRightAlign =
                        SlideWidth - widthOfDots - DottedBar.DEFAULT_TOP_MARGIN;
            }

            foreach (Shape item in AddedShapes)
            {
                var left = 5 + (5 * i) + (i * SolidBar.Instance.Height);
                item.Left = left + constantLeftMarginForRightAlign;

                if ((i + 1) == (PresentationSlides.Count - HiddenSlidesCount))
                {
                    i = 0;
                    continue;
                }

                i++;
            }
        }
    }
}
