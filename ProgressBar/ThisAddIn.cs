#region

using System;
using System.Xml;
using log4net.Config;
using ProgressBar.Adapter;
using ProgressBar.Controller;
using ProgressBar.Helper;
using ProgressBar.Model;
using ProgressBar.Properties;
using Office = Microsoft.Office.Core;

#endregion

namespace ProgressBar
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            // http://stackoverflow.com/a/12030801/752142

            // Logger.Logger.Setup();

            // http://stackoverflow.com/a/4317263/752142            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Resources.log4net);
            XmlConfigurator.Configure(doc.DocumentElement);

            IBarModel barModel = new BarModel();
            IBarController barController = new BarController(barModel);

            ShapeNameHelper nameHelper = new ShapeNameHelper();
            IPowerPointAdapter powerpointAdapter = new PowerPointAdapter(Globals.ThisAddIn.Application, nameHelper);


            Globals.Ribbons.Ribbon.Setup(barController, barModel, powerpointAdapter, nameHelper);

            // Application.PresentationOpen += new PowerPoint.EApplication_PresentationOpenEventHandler(UpdateStatusBarMessage.ShowStatusMessage);
        }


        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;
        }

        #endregion
    }
}