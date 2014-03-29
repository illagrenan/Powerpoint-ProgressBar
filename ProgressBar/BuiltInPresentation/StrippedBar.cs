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
    internal class StrippedBar : IBar
    {
        private readonly IBarInfo _info;
        private readonly PositionOptions _positionInfo;
        private PresentationInfo _presentationInfo;

        public StrippedBar()
        {
            _positionInfo = new PositionOptions
            {
                // (enabled, checked)
                Top = new Location(true, true),
                Right = new Location(false, false),
                Bottom = new Location(true, false),
                Left = new Location(false, false)
            };

            Image thumbnailImage = Resources.theme_solid;
            const string friendlyName = "Stripped Bar";

            _info = new BarInfo(thumbnailImage, friendlyName);
        }

        List<IBasicShape> IBar.Render(int currentPosition, PresentationInfo presentationInfo)
        {
            _presentationInfo = presentationInfo;

            List<IBasicShape> shapes = new List<IBasicShape>();

            if (presentationInfo.DisableOnFirstSlide && currentPosition == 1)
            {
                return shapes;
            }

            shapes.Add(MakeBackground());
            shapes.Add(MakeProgressBar(currentPosition));

            if (_positionInfo.Bottom.Checked)
            {
                foreach (IBasicShape basicShape in shapes)
                {
                    basicShape.Top = presentationInfo.Height - presentationInfo.UserSize;
                }
            }

            return shapes;
        }


        public IBarInfo GetInfo()
        {
            return _info;
        }

        IPositionOptions IBar.GetPositionOptions()
        {
            return _positionInfo;
        }

        private IBasicShape MakeShapeStub(PresentationInfo presentation)
        {
            IBasicShape shapeStub = new BasicShape();

            shapeStub.Height = presentation.UserSize;

            shapeStub.Top = 0;
            shapeStub.Left = 0;
            shapeStub.Type = MsoAutoShapeType.msoShapeRectangle;

            return shapeStub;
        }

        private IBasicShape MakeBackground()
        {
            IBasicShape backgroundShape = MakeShapeStub(_presentationInfo);

            backgroundShape.Width = _presentationInfo.Width;
            backgroundShape.ColorType = ShapeType.BACKGROUND;

            return backgroundShape;
        }

        private IBasicShape MakeProgressBar(int currentPosition)
        {
            IBasicShape backgroundShape = MakeShapeStub(_presentationInfo);


            if (_presentationInfo.DisableOnFirstSlide)
            {
                currentPosition -= 1;
            }


            backgroundShape.Width = (CalculateWidthOfBarOnOneSlide())*currentPosition;
            backgroundShape.ColorType = ShapeType.PROGRESS_BAR;

            return backgroundShape;
        }

        private float CalculateWidthOfBarOnOneSlide()
        {
            int slidesCount = _presentationInfo.DisableOnFirstSlide
                ? (_presentationInfo.SlidesCount - 1)
                : _presentationInfo.SlidesCount;
            return _presentationInfo.Width/slidesCount;
        }
    }
}