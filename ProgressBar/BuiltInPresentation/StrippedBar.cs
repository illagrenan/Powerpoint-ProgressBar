#region

using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Office.Core;
using ProgressBar.Bar;
using ProgressBar.DataStructs;
using ProgressBar.Model;
using ProgressBar.Properties;
using ProgressBar.CustomExceptions;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

#endregion

namespace ProgressBar.BuiltInPresentation
{
    internal class StrippedBar : IBar
    {
        private readonly IBarInfo info;
        private readonly PositionOptions positionInfo;
        private PresentationInfo presentationInfo;

        public StrippedBar()
        {
            positionInfo = new PositionOptions();

            // (enabled, checked)
            positionInfo.Top = new Location(true, true);
            positionInfo.Right = new Location(false, false);
            positionInfo.Bottom = new Location(true, false);
            positionInfo.Left = new Location(false, false);

            Image thumbnailImage = Resources.theme_solid;
            string friendlyName = "Stripped Bar";

            info = new BarInfo(thumbnailImage, friendlyName);
        }

        List<IBasicShape> IBar.Render(int currentPosition, PresentationInfo presentationInfo)
        {
            this.presentationInfo = presentationInfo;

            List<IBasicShape> shapes = new List<IBasicShape>();

            if (presentationInfo.DisableOnFirstSlide && currentPosition == 1)
            {
                return shapes;
            }

            shapes.Add(MakeBackground());
            shapes.Add(MakeProgressBar(currentPosition));

            if (positionInfo.Bottom.Checked)
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
            return info;
        }

        IPositionOptions IBar.GetPositionOptions()
        {
            return positionInfo;
        }

        public IPositionOptions GetPositionOptions(IPositionOptions positionOptions)
        {
            throw new NotImplementedException();
        }

        public PositionOptions GetPositionOptions()
        {
            throw new ObsoleteException();
        }

        public List<Shape> Render(int currentPosition, PresentationInfo ppp)
        {
            throw new NotImplementedException();
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
            IBasicShape backgroundShape = MakeShapeStub(presentationInfo);

            backgroundShape.Width = presentationInfo.Width;
            backgroundShape.ColorType = ShapeType.BACKGROUND;

            return backgroundShape;
        }

        private IBasicShape MakeProgressBar(int currentPosition)
        {
            IBasicShape backgroundShape = MakeShapeStub(presentationInfo);


            if (presentationInfo.DisableOnFirstSlide)
            {
                currentPosition -= 1;
            }


            backgroundShape.Width = (CalculateWidthOfBarOnOneSlide())*currentPosition;
            backgroundShape.ColorType = ShapeType.PROGRESS_BAR;

            return backgroundShape;
        }

        private float CalculateWidthOfBarOnOneSlide()
        {
            int slidesCount = presentationInfo.DisableOnFirstSlide
                ? (presentationInfo.SlidesCount - 1)
                : presentationInfo.SlidesCount;
            return presentationInfo.Width/slidesCount;
        }
    }
}