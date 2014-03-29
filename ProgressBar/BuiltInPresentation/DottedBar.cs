#region

using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Office.Core;
using ProgressBar.Bar;
using ProgressBar.DataStructs;
using ProgressBar.Model;
using ProgressBar.Properties;
using ProgressBar._CustomExceptions;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

#endregion

namespace ProgressBar.BuiltInPresentation
{
    internal class DottedBar : IBar
    {
        private readonly IBarInfo _barInfo;
        private readonly PositionOptions _positionOptions;

        public DottedBar()
        {
            Image thumbnailImage = Resources.theme_dotted;
            const string friendlyName = "Dotted Bar";

            _barInfo = new BarInfo(thumbnailImage, friendlyName);

            _positionOptions = new PositionOptions
            {
                // (enabled, checked)
                Top = new Location(true, true),
                Right = new Location(true, false),
                Bottom = new Location(true, false),
                Left = new Location(true, true)
            };
        }


        List<IBasicShape> IBar.Render(int currentPosition, PresentationInfo presentationInfo)
        {
            List<IBasicShape> shapes = new List<IBasicShape>();

            if (presentationInfo.DisableOnFirstSlide && currentPosition == 1)
            {
                return shapes;
            }

            for (int i = 0; i < presentationInfo.SlidesCount; i++)
            {
                IBasicShape shape = new BasicShape();
                shape.Height = shape.Width = presentationInfo.UserSize;

                if (_positionOptions.Top.Checked)
                {
                    shape.Top = 10;
                }
                else
                {
                    shape.Top = presentationInfo.Height - presentationInfo.UserSize - 10;
                }

                shape.Left = 10 + (i*(presentationInfo.UserSize + 10));


                if (_positionOptions.Right.Checked)
                {
                    float leftConstant = presentationInfo.SlidesCount*presentationInfo.UserSize;
                    leftConstant = presentationInfo.Width - leftConstant - (presentationInfo.SlidesCount*10) - 10;
                    
                    // Add constant to left margin
                    shape.Left = leftConstant + shape.Left;
                }

                shape.Type = MsoAutoShapeType.msoShapeRectangle;

                // Slides are NOT indexed from 0!
                if ((i + 1) == currentPosition)
                {
                    shape.ColorType = ShapeType.PROGRESS_BAR;
                }
                else
                {
                    shape.ColorType = ShapeType.BACKGROUND;
                }

                shapes.Add(shape);
            }

            return shapes;
        }


        public IBarInfo GetInfo()
        {
            return _barInfo;
        }

        IPositionOptions IBar.GetPositionOptions()
        {
            return _positionOptions;
        }

        public IPositionOptions GetPositionOptions(IPositionOptions positionOptions)
        {
            throw new ObsoleteException();
        }

        public PositionOptions GetPositionOptions()
        {
            throw new ObsoleteException();
        }

        public List<Shape> Render(int currentPosition, PresentationInfo presentationInfo)
        {
            throw new NotImplementedException();
        }
    }
}