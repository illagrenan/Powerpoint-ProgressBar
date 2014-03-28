using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools.Ribbon;
using ProgressBar._CustomExceptions;
using ProgressBar.Adapter;
using ProgressBar.Bar;
using ProgressBar.Controller;
using ProgressBar.CustomExceptions;
using ProgressBar.Model;
using ProgressBar.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProgressBar
{
    public partial class BarRibbon1 : IBarView
    {
        private IBarModel model;
        public IBarController Controller { get; set; }
        private IPowerPointAdapter powerpointAdapter;
        ShapeNameHelper nameHelper;

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
            this.nameHelper = sn;
        }

        public void Register(IBarModel model)
        {
            model.BarCreatedEvent += model_BarCreatedEvent;
            model.BarRemovedEvent += model_BarRemovedEvent;
            model.RegisteredBarsEvent += model_RegisteredBarEvents;
            model.BarThemeChangedEvent += model_themeChanged;
            model.AlignmentOptionsChanged += model_AlignmentOptionsChanged;
        }

        private void model_AlignmentOptionsChanged(IPositionOptions obj)
        {
            this.btn_AlignTop.Enabled = obj.Top.Enabled;
            this.btn_AlignTop.Checked = obj.Top.Checked;

            this.btn_AlignRight.Enabled = obj.Right.Enabled;
            this.btn_AlignRight.Checked = obj.Right.Checked;

            this.btn_AlinBottom.Enabled = obj.Bottom.Enabled;
            this.btn_AlinBottom.Checked = obj.Bottom.Checked;

            this.btn_AlignLeft.Enabled = obj.Left.Enabled;
            this.btn_AlignLeft.Checked = obj.Left.Checked;
        }


        private void model_themeChanged(IBar obj)
        {
            SetPositionOptions(obj);
            this.model_BarCreatedEvent(obj);
        }

        private void SetPositionOptions(IBar obj)
        {

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

            PresentationInfo presentationInfo = CreateInfo(visibleSlides);
            SetPositionOptions(createdBar);

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
                            addedShape.Name = this.nameHelper.GetBackgroundShapeName();
                            break;

                        case ProgressBar.DataStructs.ShapeType.PROGRESS_BAR:
                            addedShape.Fill.ForeColor.RGB = GetSelectedForegroundColor();
                            addedShape.Name = this.nameHelper.GetForegroundShapeName();
                            break;

                        default:

                            // TODO Explain what happened
                            throw new InvalidStateException();
                    }


                    addedShape.Line.Weight = 0;
                    addedShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

                }

                slideCounter++;
            }
        }

        private PresentationInfo CreateInfo(List<Slide> visibleSlides)
        {
            PresentationInfo presentationInfo = new PresentationInfo();

            presentationInfo.Height = this.powerpointAdapter.PresentationHeight();
            presentationInfo.Width = this.powerpointAdapter.PresentationWidth();
            presentationInfo.SlidesCount = visibleSlides.Count();
            presentationInfo.UserSize = this.BarSize();
            presentationInfo.DisableOnFirstSlide = this.checkBox1.Checked;
            return presentationInfo;
        }

        void model_BarRemovedEvent()
        {
            List<Shape> shape = this.powerpointAdapter.AddInShapes();
            shape.ForEach(e => e.Delete());
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
            Globals.ThisAddIn.Application.SlideSelectionChanged += OnSlidesChanged;

            this.FillDropDown();
            this.SetDropDownDefaultSize();
            this.SetDefaultColors();

            this.Controller.GetRegistered();

        }

        private void OnSlidesChanged(SlideRange SldRange)
        {
            if (this.powerpointAdapter.HasSlides == false && this.Controller.HasBar())
            {
                // When a user deletes all slides,
                // we can simulate Remove Event with button
                this.btn_Remove_Click(null, null);
            }

            Debug.WriteLine(String.Format(
                "OnSlidesChanged={0}",
                this.powerpointAdapter.VisibleSlides().Count()
                ));
            // http://social.msdn.microsoft.com/Forums/en-US/22a64e2b-32eb-4eab-930f-f3ca526d9d3b/powerpoint-events-for-adding-a-shape-deleting-a-shape-and-deleting-a-slide?forum=vsto
        }

        private void model_RegisteredBarEvents(List<IBar> bars)
        {
            foreach (IBar item in bars)
            {

                Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItem = this.Factory.CreateRibbonDropDownItem();
                ribbonDropDownItem.Image = item.GetInfo().Image;
                ribbonDropDownItem.Label = item.GetInfo().FriendlyName;

                this.themeGallery.Items.Add(ribbonDropDownItem);
            }
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
            if (this.powerpointAdapter.HasSlides == false)
            {
                MessageBox.Show(
                                "This presentation has no slides.",
                                "Unable to add bar",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }


            string selectedTheme = GetSelectedTheme();
            this.Controller.AddBarClicked(selectedTheme);

            // Enable items only when adding new bar
            // If users is refresing bar, all items remain enabled
            if (this.btn_Add.Label == "Add")
            {
                SwapStateBarRelatedItems();
            }

            SwapAddRefreshButton();
        }

        private void SwapStateBarRelatedItems()
        {

            bool newState = this.Controller.HasBar();

            SwapItemsStateInGroup(this.styleGroup.Items, newState);
            SwapItemsStateInGroup(this.positionGroup.Items, newState);
            SwapItemsStateInGroup(this.themeGroup.Items, newState);
        }

        private static void SwapItemsStateInGroup(IList<RibbonControl> groupItems, bool newState)
        {
            foreach (var anItem in groupItems)
            {
                anItem.Enabled = newState;
            }
        }


        private void SwapAddRefreshButton()
        {
            this.btn_Remove.Enabled = this.Controller.HasBar();

            if (this.Controller.HasBar())
            {
                this.btn_Add.Label = "Refresh";
                this.btn_Add.Image = null;
                this.btn_Add.OfficeImageId = "Refresh";
            }
            else
            {
                this.btn_Add.Image = global::ProgressBar.Properties.Resources.progressbar;
                this.btn_Add.Label = "Add";
            }
        }

        private void btn_Remove_Click(object sender, RibbonControlEventArgs e)
        {
            this.Controller.RemoveBarClicked();
            this.SwapAddRefreshButton();
            this.SwapStateBarRelatedItems();
        }

        private void btn_ChangeForeground_Click(object sender, RibbonControlEventArgs e)
        {

            // Microsoft.Office.Tools.Ribbon.RibbonButton b = (Microsoft.Office.Tools.Ribbon.RibbonButton)sender;
            // title.Contains("string", StringComparison.OrdinalIgnoreCase);

            if (DialogResult.OK == colorDialog_Foreground.ShowDialog())
            {
                colorDialog_Foreground.Color = colorDialog_Foreground.Color;

                this.powerpointAdapter.AddInShapes().ForEach(
                    shape =>
                    {
                        if (this.nameHelper.IsShapeForegroundShape(shape.Name))
                        {
                            shape.Fill.ForeColor.RGB = GetSelectedForegroundColor();
                        }
                    });
            }
        }

        private void btn_ChangeBackground_Click(object sender, RibbonControlEventArgs e)
        {

            if (DialogResult.OK == colorDialog_Background.ShowDialog())
            {
                colorDialog_Background.Color = colorDialog_Background.Color;

                this.powerpointAdapter.AddInShapes().ForEach(
                    shape =>
                    {
                        if (this.nameHelper.IsShapeBackgroundShape(shape.Name))
                        {
                            shape.Fill.ForeColor.RGB = GetSelectedBackgroundColor();
                        }
                    });

            }
        }

        private void galleryTheme_Click(object sender, RibbonControlEventArgs e)
        {
            string selectedTheme = this.GetSelectedTheme();
            this.Controller.ChangeThemeClicked(selectedTheme);
        }

        private string GetSelectedTheme()
        {
            string selectedTheme = themeGallery.SelectedItem.ToString();
            return selectedTheme;
        }

        private void buttonAbout_Click(object sender, RibbonControlEventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        private void checkBox1_Click(object sender, RibbonControlEventArgs e)
        {
            string selectedTheme = GetSelectedTheme();
            this.Controller.AddBarClicked(selectedTheme);
        }

        private void gallery1_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void dropDown_BarHeight_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            this.Controller.ChangeSizeClicked(this.BarSize());
        }

        private void btn_AlignTop_Click(object sender, RibbonControlEventArgs e)
        {
            this.Controller.PositionOptionsChanged(
                                                    this.btn_AlignTop.Checked,
                                                    this.btn_AlignRight.Enabled,
                                                    this.btn_AlinBottom.Enabled,
                                                    this.btn_AlignLeft.Enabled
                                                   );
        }
    }
}
