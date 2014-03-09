using System;
using System.Reflection;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Diagnostics;

namespace ProgressBar
{
    public partial class AboutInformations : Form
    {
        public AboutInformations()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AboutInformations_Load(object sender, EventArgs e)
        {
            this.label3.Text = GetRunningVersion().ToString();
        }

        private Version GetExecutingAssemblyVersion()
        {
            Assembly thisAssem = Assembly.GetExecutingAssembly();
            AssemblyName thisAssemName = thisAssem.GetName();
            return thisAssemName.Version;
        }

        private Version GetRunningVersion()
        {
            try
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (InvalidDeploymentException)
            {
                return GetExecutingAssemblyVersion();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.vaclavdohnal.cz");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.presentation-progressbar.com");
        }
    }
}
