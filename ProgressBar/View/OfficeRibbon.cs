using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using ProgressBar.View;
using ProgressBar.Model;
using ProgressBar.Controller;
using System.Windows.Forms;
using System.Diagnostics;
using ProgressBar.Adapter;

namespace ProgressBar
{
    public partial class BarRibbon1 : IBarView
    {
        private IBarModel model;
        private IBarController controller;
        private IPowerPointAdapter powerpointAdapter;


        public void Register(IBarModel model)
        {
            model.AddResultEvent += model_AddResultEvent;
            model.BarCreatedEvent += model_BarCreatedEvent;
        }

        private void model_BarCreatedEvent(Bar.IBar obj)
        {
            throw new NotImplementedException();
        }

        public void Release(IBarModel model)
        {
            model.AddResultEvent -= model_AddResultEvent;
            model.BarCreatedEvent -= model_BarCreatedEvent;
        }

        private void model_AddResultEvent(int result)
        {

            MessageBox.Show(result.ToString());
        }

        private void BarRibbon1_Load(object sender, RibbonUIEventArgs e)
        {
            Register(model);
        }

        private void BarRibbon1_Close(object sender, EventArgs e)
        {
            Release(model);
        }

        private void Hello_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                int a = 40;
                int b = 2;
                this.Controller.PlusButtonClicked(a, b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public IBarController Controller
        {
            get
            {
                return this.controller;
            }
            set
            {
                this.controller = value;
            }
        }

        public BarRibbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        internal void Setup
                            (
                            IBarController controller,
                            IBarModel model,
                            IPowerPointAdapter powerpointAdapter
                            )
        {
            this.model = new BarModel();
            this.controller = new BarController(this.model);
            this.powerpointAdapter = powerpointAdapter;
        }

        private void btn_Add_Click(object sender, RibbonControlEventArgs e)
        {
            this.Controller.AddBarClicked();
        }

        private void btn_Remove_Click(object sender, RibbonControlEventArgs e)
        {
            this.Controller.RemoveBarClicked();
        }
    }
}
