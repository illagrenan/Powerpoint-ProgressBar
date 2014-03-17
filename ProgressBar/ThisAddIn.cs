using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;
using System.Diagnostics;
using ProgressBar.Model;
using ProgressBar.Controller;
using ProgressBar.Adapter;
using ProgressBar.Bar;

namespace ProgressBar
{

    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // http://stackoverflow.com/a/12030801/752142

            IBarModel barModel = new BarModel();
            IBarController barController = new BarController(barModel);

            ShapeNameHelper nameHelper = new ShapeNameHelper();
            IPowerPointAdapter powerpointAdapter = new PowerPointAdapter(Globals.ThisAddIn.Application, nameHelper);

            Globals.Ribbons.Ribbon1.Setup(barController, barModel, powerpointAdapter, nameHelper);

            // Application.PresentationOpen += new PowerPoint.EApplication_PresentationOpenEventHandler(UpdateStatusBarMessage.ShowStatusMessage);
        }


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
