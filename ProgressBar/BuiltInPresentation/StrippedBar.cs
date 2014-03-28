using ProgressBar.Bar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ProgressBar.BuiltInPresentation
{
    class StrippedBar : IBar
    {
        private PresentationInfo presentationInfo;
        private PositionOptions positionInfo;
        private IBarInfo info;

        public StrippedBar()
        {
            this.positionInfo = new PositionOptions();

            // (enabled, checked)
            this.positionInfo.Top = new Location(true, true);
            this.positionInfo.Right = new Location(false, false);
            this.positionInfo.Bottom = new Location(true, false);
            this.positionInfo.Left = new Location(false, false);

            Image thumbnailImage = global::ProgressBar.Properties.Resources.theme_solid;
            string friendlyName = "Stripped Bar";

            this.info = new BarInfo(thumbnailImage, friendlyName);
        }

        public PositionOptions GetPositionOptions()
        {
            return this.positionInfo;
        }

        public List<Microsoft.Office.Interop.PowerPoint.Shape> Render(int currentPosition, PresentationInfo ppp)
        {
            throw new NotImplementedException();
        }

        private IBasicShape MakeShapeStub(PresentationInfo presentation)
        {
            IBasicShape shapeStub = new BasicShape();

            shapeStub.Height = presentation.UserSize;

            shapeStub.Top = 0;
            shapeStub.Left = 0;
            shapeStub.Type = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;

            return shapeStub;
        }

        private IBasicShape MakeBackground()
        {
            IBasicShape backgroundShape = this.MakeShapeStub(this.presentationInfo);

            backgroundShape.Width = this.presentationInfo.Width;
            backgroundShape.ColorType = DataStructs.ShapeType.BACKGROUND;

            return backgroundShape;
        }

        private IBasicShape MakeProgressBar(int currentPosition)
        {
            IBasicShape backgroundShape = this.MakeShapeStub(this.presentationInfo);


            if (this.presentationInfo.DisableOnFirstSlide)
            {
                currentPosition -= 1;
            }


            backgroundShape.Width = (CalculateWidthOfBarOnOneSlide()) * currentPosition;
            backgroundShape.ColorType = DataStructs.ShapeType.PROGRESS_BAR;

            return backgroundShape;
        }

        private float CalculateWidthOfBarOnOneSlide()
        {
            int slidesCount = this.presentationInfo.DisableOnFirstSlide ? (this.presentationInfo.SlidesCount - 1) : this.presentationInfo.SlidesCount;
            return this.presentationInfo.Width / slidesCount;
        }

        List<IBasicShape> IBar.Render(int currentPosition, PresentationInfo presentationInfo)
        {
            this.presentationInfo = presentationInfo;

            List<IBasicShape> shapes = new List<IBasicShape>();

            if (presentationInfo.DisableOnFirstSlide && currentPosition == 1)
            {
                return shapes;
            }

            shapes.Add(this.MakeBackground());
            shapes.Add(this.MakeProgressBar(currentPosition));

            return shapes;
        }


        public IBarInfo GetInfo()
        {
            return this.info;
        }
    }
}
