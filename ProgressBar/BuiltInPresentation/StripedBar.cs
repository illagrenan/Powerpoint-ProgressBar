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
    internal class StripedBar : IBar
    {
        private readonly IBarInfo _info;
        private PositionOptions _positionInfo;
        private PresentationInfo _presentationInfo;

        public StripedBar()
        {
            _positionInfo = new PositionOptions
            {
                Top = new Location(available: true, selected: true),
                Right = new Location(available: false, selected: false),
                Bottom = new Location(available: true, selected: false),
                Left = new Location(available: false, selected: false)
            };

            Image thumbnailImage = Resources.theme_solid;
            const string friendlyName = "Striped Bar";

            _info = new BarInfo(thumbnailImage, friendlyName);
        }

        IPositionOptions IBar.PositionOptions
        {
            get { return _positionInfo; }
            set { _positionInfo = (PositionOptions) value; }
        }

        public IBarInfo GetInfo()
        {
            return _info;
        }

        IEnumerable<IBasicShape> IBar.Render(int currentPosition, PresentationInfo presentationInfo)
        {
            _presentationInfo = presentationInfo;

            List<IBasicShape> shapes = new List<IBasicShape>();

            if (presentationInfo.DisableOnFirstSlide && currentPosition == 1)
            {
                return shapes;
            }

            shapes.Add(MakeBackground());
            shapes.Add(MakeProgressBar(currentPosition));

            if (_positionInfo.Bottom.Selected)
            {
                foreach (IBasicShape basicShape in shapes)
                {
                    basicShape.Top = presentationInfo.Height - presentationInfo.UserSize;
                }
            }

            return shapes;
        }

        private float CalculateWidthOfBarOnOneSlide()
        {
            int slidesCount = _presentationInfo.DisableOnFirstSlide
                ? (_presentationInfo.SlidesCount - 1)
                : _presentationInfo.SlidesCount;
            return _presentationInfo.Width/slidesCount;
        }

        private IBasicShape MakeBackground()
        {
            IBasicShape backgroundShape = MakeShapeStub(_presentationInfo);

            backgroundShape.Width = _presentationInfo.Width;
            backgroundShape.ColorType = ShapeType.Inactive;

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
            backgroundShape.ColorType = ShapeType.Active;

            return backgroundShape;
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
    }
}