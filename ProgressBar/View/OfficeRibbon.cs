﻿using System;
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
using Microsoft.Office.Interop.PowerPoint;
using ProgressBar.Bar;
using System.Drawing;
using ProgressBar.CustomExceptions;

namespace ProgressBar
{
    public partial class BarRibbon1 : IBarView
    {
        private IBarModel model;
        public IBarController Controller { get; set; }
        private IPowerPointAdapter powerpointAdapter;
        ShapeNameHelper sn;

        #region MVCLogic
        internal void Setup(
                            IBarController controller,
                            IBarModel model,
                            IPowerPointAdapter powerpointAdapter,
                            ShapeNameHelper sn
                            )
        {
            this.model = new BarModel();
            this.Controller = new BarController(this.model);
            this.powerpointAdapter = powerpointAdapter;
            this.sn = sn;
        }

        public void Register(IBarModel model)
        {
            model.BarCreatedEvent += model_BarCreatedEvent;
            model.BarRemovedEvent += model_BarRemovedEvent;
        }


        public void Release(IBarModel model)
        {
            model.BarCreatedEvent -= model_BarCreatedEvent;
            model.BarRemovedEvent -= model_BarRemovedEvent;
        }
        #endregion

        private void model_BarCreatedEvent(Bar.IBar createdBar)
        {
            int slideCounter = 1;
            List<Slide> visibleSlides = this.powerpointAdapter.VisibleSlides();

            PresentationInfo presentationInfo = new PresentationInfo();

            presentationInfo.Height = this.powerpointAdapter.PresentationHeight();
            presentationInfo.Width = this.powerpointAdapter.PresentationWidth();
            presentationInfo.SlidesCount = visibleSlides.Count();
            presentationInfo.UserSize = this.BarSize();

            foreach (Slide slide in visibleSlides)
            {
                foreach (IBasicShape shape in createdBar.Render(slideCounter, presentationInfo))
                {
                    Shape addedShape = slide.Shapes.AddShape(
                                                            shape.Type,
                                                            shape.Left,
                                                            shape.Top,
                                                            shape.Width,
                                                            shape.Height
                                                            );

                    switch (shape.ColorType)
                    {
                        case ProgressBar.DataStructs.ShapeType.BACKGROUND:
                            addedShape.Fill.ForeColor.RGB = GetSelectedBackgroundColor();
                            addedShape.Name = this.sn.GetBackgroundShapeName();

                            break;
                        case ProgressBar.DataStructs.ShapeType.PROGRESS_BAR:
                            addedShape.Fill.ForeColor.RGB = GetSelectedForegroundColor();
                            addedShape.Name = this.sn.GetForegroundShapeName();
                            break;
                        default:
                            throw new InvalidStateException();
                    }


                    addedShape.Line.Weight = 0;
                    addedShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

                }

                slideCounter++;
            }
        }

        void model_BarRemovedEvent()
        {
            List<Shape> s = this.powerpointAdapter.AddInShapes();
            s.ForEach(e => e.Delete());
        }


        private int BarSize()
        {
            return int.Parse(this.dropDown_BarHeight.SelectedItem.Label);
        }

        private int GetSelectedForegroundColor()
        {
            return ColorTranslator.ToOle(this.colorDialog_Foreground.Color);
        }

        private int GetSelectedBackgroundColor()
        {
            return ColorTranslator.ToOle(this.colorDialog_Background.Color);
        }

        private void BarRibbon1_Load(object sender, RibbonUIEventArgs e)
        {
            Register(model);

            Globals.ThisAddIn.Application.AfterNewPresentation += AfterNewPresentationHandle;
            Globals.ThisAddIn.Application.AfterPresentationOpen += AfterPresentationOpenHandle;

            this.FillDropDown();
            this.SetDropDownDefaultSize();
            this.SetDefaultColors(); ;
        }

        private void SetDefaultColors()
        {
            this.colorDialog_Background.Color = this.Controller.BackgroundDefaultColor();
            this.colorDialog_Foreground.Color = this.Controller.ForegroundDefaultColor();
        }

        private void SetDropDownDefaultSize()
        {
            this.dropDown_BarHeight.SelectedItemIndex = this.Controller.GetDefaultSize();
        }


        private void FillDropDown()
        {
            foreach (int size in this.Controller.GetSizes())
            {
                RibbonDropDownItem itemToAdd = Factory.CreateRibbonDropDownItem();
                itemToAdd.Label = size.ToString();
                dropDown_BarHeight.Items.Add(itemToAdd);
            }

        }

        /// <summary>
        /// Occurs after a presentation is created.
        /// </summary>
        /// <param name="Pres"></param>
        private void AfterNewPresentationHandle(Presentation Pres)
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Occurs after an existing presentation is opened.
        /// </summary>
        /// <param name="Pres"></param>
        private void AfterPresentationOpenHandle(Presentation Pres)
        {
            throw new NotImplementedException();
        }

        private void BarRibbon1_Close(object sender, EventArgs e)
        {
            Release(model);
        }

        public BarRibbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, RibbonControlEventArgs e)
        {
            this.Controller.AddBarClicked();
        }

        private void btn_Remove_Click(object sender, RibbonControlEventArgs e)
        {
            this.Controller.RemoveBarClicked();
        }

        private void btn_ChangeForeground_Click(object sender, RibbonControlEventArgs e)
        {

            if (DialogResult.OK == colorDialog_Foreground.ShowDialog())
            {
                colorDialog_Foreground.Color = colorDialog_Foreground.Color;

                // TODO recolor
            }
        }

        private void btn_ChangeBackground_Click(object sender, RibbonControlEventArgs e)
        {
            if (DialogResult.OK == colorDialog_Background.ShowDialog())
            {
                colorDialog_Background.Color = colorDialog_Background.Color;

                // TODO recolor
            }
        }
    }
}