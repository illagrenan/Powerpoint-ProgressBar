#region

using System.Collections.Generic;
using System.Drawing;
using Microsoft.Office.Core;
using ProgressBar.Bar;
using ProgressBar.DataStructs;
using ProgressBar.Model;
using ProgressBar.Properties;

#endregion

namespace ProgressBar.BuiltInPresentation
{
    internal class DottedBar : IBar
    {
        private readonly IBarInfo _barInfo;
        private readonly PositionOptions _positionOptions;
        private int _gap;

        public DottedBar()
        {
            Image thumbnailImage = Resources.theme_dotted;
            const string friendlyName = "Dotted Bar";

            _barInfo = new BarInfo(thumbnailImage, friendlyName);

            _positionOptions = new PositionOptions
            {
                Top = new Location(available: true, selected: true),
                Right = new Location(available: true, selected: false),
                Bottom = new Location(available: true, selected: false),
                Left = new Location(available: true, selected: true)
            };
        }


        IEnumerable<IBasicShape> IBar.Render(int currentPosition, PresentationInfo presentationInfo)
        {
            List<IBasicShape> shapes = new List<IBasicShape>();
            int slidesCount = presentationInfo.SlidesCount;

            if (presentationInfo.DisableOnFirstSlide && currentPosition == 1)
            {
                return shapes;
            }

            if (presentationInfo.DisableOnFirstSlide)
            {
                currentPosition -= 1;
                slidesCount -=  1;
            }
           
            for (int i = 0; i < slidesCount; i++)
            {
                IBasicShape shape = new BasicShape();
                shape.Height = shape.Width = presentationInfo.UserSize;

                _gap = 10;
                if (_positionOptions.Top.Selected)
                {
                    shape.Top = _gap;
                }
                else
                {
                    shape.Top = presentationInfo.Height - presentationInfo.UserSize - _gap;
                }

                shape.Left = _gap + (i*(presentationInfo.UserSize + _gap));


                if (_positionOptions.Right.Selected)
                {
                    float leftConstant = slidesCount*presentationInfo.UserSize;
                    leftConstant = presentationInfo.Width - leftConstant - (slidesCount*_gap) - _gap;

                    // Add constant to left margin
                    shape.Left = leftConstant + shape.Left;
                }

                shape.Type = MsoAutoShapeType.msoShapeRectangle;

                // Slides are NOT indexed from 0!
                if ((i + 1) == currentPosition)
                {
                    shape.ColorType = ShapeType.Active;
                }
                else
                {
                    shape.ColorType = ShapeType.Inactive;
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
    }
}