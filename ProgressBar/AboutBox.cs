#region

using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace ProgressBar
{
    partial class AboutBox : Form
    {
        private const string ProgressbarLogFileLog = @"ProgressBar\log_file.log";

        public AboutBox()
        {
            InitializeComponent();

            assemblyVersion.Text = String.Format("Assembly version: {0}", AssemblyVersion);
            clickOnceVersion.Text = String.Format("ClickOnce version: {0}", GetClickOnceApplicationVersion());
        }

        private string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void AboutBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                var tempUserPath = Path.GetTempPath();
                var fullLogPath = Path.Combine(tempUserPath, ProgressbarLogFileLog);

                Process.Start("notepad.exe", fullLogPath);
            }
        }

        private string GetClickOnceApplicationVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return myVersion.ToString();
            }

            return "unknown";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.presentation-progressbar.com/");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}