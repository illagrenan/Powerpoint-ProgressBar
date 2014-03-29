using ProgressBar.Bar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ProgressBar._CustomExceptions;

namespace ProgressBar.BuiltInPresentation
{
    class DottedBar : IBar
    {

        private IBarInfo info;
        private PositionOptions p;

        public DottedBar()
        {
            Image im = global::ProgressBar.Properties.Resources.theme_dotted;
            string lab = "Dotted Bar";

            this.info = new BarInfo(im, lab);

            this.p = new PositionOptions();

            // (enabled, checked)
            this.p.Top = new Location(true, true);
            this.p.Right = new Location(true, false);
            this.p.Bottom = new Location(true, false);
            this.p.Left = new Location(true, true);
        }


        public PositionOptions GetPositionOptions()
        {
            throw new ObsoleteException();
        }

        public List<Microsoft.Office.Interop.PowerPoint.Shape> Render(int currentPosition, PresentationInfo ppp)
        {
            throw new NotImplementedException();
        }


        List<IBasicShape> IBar.Render(int currentPosition, PresentationInfo ppp)
        {
            List<IBasicShape> shapes = new List<IBasicShape>();

            if (ppp.DisableOnFirstSlide && currentPosition == 1)
            {
                return shapes;
            }

            for (int i = 0; i < ppp.SlidesCount; i++)
            {
                IBasicShape x = new BasicShape();
                x.Height = x.Width = ppp.UserSize;
                x.Top = 10;
                x.Left = 10 + (i * (ppp.UserSize + 10));
                x.Type = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;

                // Slides are NOT indexed from 0!
                if ((i + 1) == currentPosition)
                {
                    x.ColorType = DataStructs.ShapeType.PROGRESS_BAR;
                }
                else
                {
                    x.ColorType = DataStructs.ShapeType.BACKGROUND;
                }

                shapes.Add(x);
            }

            return shapes;
        }


        public IBarInfo GetInfo()
        {
            return this.info;
        }

        Model.IPositionOptions IBar.GetPositionOptions()
        {
            return this.p;
        }

        public Model.IPositionOptions GetPositionOptions(Model.IPositionOptions positionOptions)
        {

            var po = (PositionOptions) positionOptions;




            return po;

        }
    }
}
