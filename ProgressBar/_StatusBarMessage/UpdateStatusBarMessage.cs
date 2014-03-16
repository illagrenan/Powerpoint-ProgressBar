using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgressBar
{
    public delegate void AddStatusMessageDelegate(string strMessage);

    public static class UpdateStatusBarMessage
    {

        public static Form mainwin;

        public static event AddStatusMessageDelegate OnNewStatusMessage;

        public static void ShowStatusMessage(Microsoft.Office.Interop.PowerPoint.Presentation Pres)
        {
            ThreadSafeStatusMessage("Foo");
        }

        private static void ThreadSafeStatusMessage(string strMessage)
        {
            if (mainwin != null && mainwin.InvokeRequired)  // we are in a different thread to the main window
                mainwin.Invoke(new AddStatusMessageDelegate(ThreadSafeStatusMessage), new object[] { strMessage });  // call self from main thread
            else
                OnNewStatusMessage(strMessage);
        }

    }
}
