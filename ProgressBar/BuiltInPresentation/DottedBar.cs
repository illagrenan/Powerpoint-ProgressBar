using ProgressBar.Bar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
            return this.p;
        }

        public List<Microsoft.Office.Interop.PowerPoint.Shape> Render(int currentPosition, PresentationInfo ppp)
        {
            throw new NotImplementedException();
        }


        List<IBasicShape> IBar.Render(int currentPosition, PresentationInfo ppp)
        {
            List<IBasicShape> shapes = new List<IBasicShape>();

            IBasicShape i = new BasicShape();
            i.Height = i.Width = 42;
            i.Top = 0;
            i.Left = 0;
            i.Type = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;
            i.ColorType = DataStructs.ShapeType.BACKGROUND;

            shapes.Add(i);

            return shapes;
        }


        public IBarInfo GetInfo()
        {
            return this.info;
        }
    }
}
