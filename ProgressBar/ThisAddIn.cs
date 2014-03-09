using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;
using System.Diagnostics;

namespace ProgressBar
{   

    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            ProgressBar_Ribbon rb = new ProgressBar_Ribbon();
            rb.btn_Add.Visible = false;

           /* UserControl1 uc = new UserControl1();
            CustomTaskPane ct = CustomTaskPanes.Add(uc, "Progress Bar");
            ct.Visible = true;            */
            //Application.PresentationOpen += new PowerPoint.EApplication_PresentationOpenEventHandler(ProgressBar_Ribbon.);

            Application.PresentationOpen += new PowerPoint.EApplication_PresentationOpenEventHandler(UpdateStatusBarMessage.ShowStatusMessage);
        }

        void Application_PresentationOpen(PowerPoint.Presentation Pres)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Yeah!");
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
