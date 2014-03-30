#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools.Ribbon;
using ProgressBar.Adapter;
using ProgressBar.Bar;
using ProgressBar.Controller;
using ProgressBar.CustomExceptions;
using ProgressBar.DataStructs;
using ProgressBar.Model;
using ProgressBar.Properties;
using ProgressBar.View;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

#endregion

namespace ProgressBar
{
    public partial class BarRibbon : IBarView
    {
        private bool _hasBar;
        private IBarModel _model;
        private ShapeNameHelper _nameHelper;
        private IPowerPointAdapter _powerpointAdapter;

        #region MVCLogic

        public void Register(IBarModel model)
        {
            // Right now: Resizing == Creating
            model.BarCreatedEvent += model_BarCreatedEvent;
            model.BarResizedEvent += model_BarResizedEvent;
            model.BarRemovedEvent += model_BarRemovedEvent;
            model.RegisteredBarsEvent += model_RegisteredBarEvents;
            model.BarThemeChangedEvent += model_themeChanged;
            model.AlignmentOptionsChanged += model_AlignmentOptionsChanged;
            model.ColorsSetuped += model_ColorsSetuped;

            model.SizesSetuped += model_SizesSetuped;
            model.DefaultSizeSetuped += model_DefaultSizeSetuped;
        }

        public void Release(IBarModel model)
        {
            model.BarCreatedEvent -= model_BarCreatedEvent;
            model.BarRemovedEvent -= model_BarRemovedEvent;
        }

        internal void Setup(
            IBarController controller,
            IBarModel model,
            IPowerPointAdapter powerpointAdapter,
            ShapeNameHelper sn
            )
        {
            _model = new BarModel();
            Controller = new BarController(_model);
            _powerpointAdapter = powerpointAdapter;
            _nameHelper = sn;
        }

        #endregion

        public BarRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        public IBarController Controller { get; set; }

        private void model_DefaultSizeSetuped(int defaultSize)
        {
            dropDown_BarHeight.SelectedItemIndex = defaultSize;
        }

        private void model_SizesSetuped(int[] obj)
        {
            foreach (int size in obj)
            {
                RibbonDropDownItem itemToAdd = Factory.CreateRibbonDropDownItem();
                itemToAdd.Label = size.ToString();
                dropDown_BarHeight.Items.Add(itemToAdd);
            }
        }

        private void model_ColorsSetuped(Dictionary<ShapeType, Color> obj)
        {
            colorDialog_Background.Color = obj[ShapeType.Inactive];
            colorDialog_Foreground.Color = obj[ShapeType.Active];
        }

        private void model_BarResizedEvent(IBar obj)
        {
            Controller.RemoveBarClicked();
            Controller.AddBarClicked(GetSelectedTheme());
        }

        private void model_AlignmentOptionsChanged(IPositionOptions newAlignmentOptions)
        {
            SetPositionOptions(newAlignmentOptions);
        }


        private void model_themeChanged(IBar obj)
        {
            SetPositionOptions(obj.GetPositionOptions());
            model_BarCreatedEvent(obj);
        }

        private void SetPositionOptions(IPositionOptions newAlignmentOptions)
        {
            btn_AlignTop.Enabled = newAlignmentOptions.Top.Available;
            btn_AlignTop.Checked = newAlignmentOptions.Top.Selected;

            btn_AlignRight.Enabled = newAlignmentOptions.Right.Available;
            btn_AlignRight.Checked = newAlignmentOptions.Right.Selected;

            btn_AlignBottom.Enabled = newAlignmentOptions.Bottom.Available;
            btn_AlignBottom.Checked = newAlignmentOptions.Bottom.Selected;

            btn_AlignLeft.Enabled = newAlignmentOptions.Left.Available;
            btn_AlignLeft.Checked = newAlignmentOptions.Left.Selected;
        }

        private void model_BarCreatedEvent(IBar createdBar)
        {
            int slideCounter = 1;
            List<Slide> visibleSlides = _powerpointAdapter.VisibleSlides();

            PresentationInfo presentationInfo = CreateInfo(visibleSlides);

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
                        case ShapeType.Inactive:
                            addedShape.Fill.ForeColor.RGB = GetSelectedBackgroundColor();
                            addedShape.Name = _nameHelper.GetBackgroundShapeName();
                            break;

                        case ShapeType.Active:
                            addedShape.Fill.ForeColor.RGB = GetSelectedForegroundColor();
                            addedShape.Name = _nameHelper.GetForegroundShapeName();
                            break;

                        default:

                            string message = String.Format("Unknown shape type \"{0}\".", shape.ColorType);
                            throw new InvalidStateException(message);
                    }


                    addedShape.Line.Weight = 0;
                    addedShape.Line.Visible = MsoTriState.msoFalse;
                }

                slideCounter++;
            }

            _hasBar = true;
        }

        private PresentationInfo CreateInfo(IEnumerable<Slide> visibleSlides)
        {
            PresentationInfo presentationInfo = new PresentationInfo();

            presentationInfo.Height = _powerpointAdapter.PresentationHeight();
            presentationInfo.Width = _powerpointAdapter.PresentationWidth();
            presentationInfo.SlidesCount = visibleSlides.Count();
            presentationInfo.UserSize = BarSize();
            presentationInfo.DisableOnFirstSlide = checkBox1.Checked;

            return presentationInfo;
        }

        private void model_BarRemovedEvent()
        {
            List<Shape> shape = _powerpointAdapter.AddInShapes();
            shape.ForEach(s => s.Delete());

            _hasBar = false;
        }


        private int BarSize()
        {
            return int.Parse(dropDown_BarHeight.SelectedItem.Label);
        }

        private int GetSelectedForegroundColor()
        {
            return ColorTranslator.ToOle(colorDialog_Foreground.Color);
        }

        private int GetSelectedBackgroundColor()
        {
            return ColorTranslator.ToOle(colorDialog_Background.Color);
        }

        private void BarRibbon1_Load(object sender, RibbonUIEventArgs e)
        {
            Register(_model);

            Globals.ThisAddIn.Application.AfterNewPresentation += AfterNewPresentationHandle;
            Globals.ThisAddIn.Application.AfterPresentationOpen += AfterPresentationOpenHandle;
            Globals.ThisAddIn.Application.SlideSelectionChanged += OnSlidesChanged;

            _hasBar = false;

            Controller.SetupColors();
            Controller.SetupSizes();
            Controller.GetRegistered();
        }

        private void OnSlidesChanged(SlideRange sldRange)
        {
            if (_powerpointAdapter.HasSlides == false && _hasBar)
            {
                // When a user deletes all slides,
                // we can simulate Remove Event with button
                btn_Remove_Click(null, null);
            }

            Debug.WriteLine("OnSlidesChanged={0}", _powerpointAdapter.VisibleSlides().Count());
            // http://social.msdn.microsoft.com/Forums/en-US/22a64e2b-32eb-4eab-930f-f3ca526d9d3b/powerpoint-events-for-adding-a-shape-deleting-a-shape-and-deleting-a-slide?forum=vsto
        }

        private void model_RegisteredBarEvents(List<IBar> bars)
        {
            foreach (IBar item in bars)
            {
                RibbonDropDownItem ribbonDropDownItem =
                    Factory.CreateRibbonDropDownItem();
                ribbonDropDownItem.Image = item.GetInfo().Image;
                ribbonDropDownItem.Label = item.GetInfo().FriendlyName;

                themeGallery.Items.Add(ribbonDropDownItem);
            }
        }


        /// <summary>
        ///     Occurs after a presentation is created.
        /// </summary>
        /// <param name="pres"></param>
        private void AfterNewPresentationHandle(Presentation pres)
        {
            // throw new NotImplementedException();
            Debug.WriteLine("Presentation was created");
        }

        /// <summary>
        ///     Occurs after an existing presentation is opened.
        /// </summary>
        /// <param name="pres"></param>
        private void AfterPresentationOpenHandle(Presentation pres)
        {
            Debug.WriteLine("Existing presentation is opened");
            throw new NotImplementedException();
        }

        private void BarRibbon1_Close(object sender, EventArgs e)
        {
            Release(_model);
        }

        private void btn_Add_Click(object sender, RibbonControlEventArgs e)
        {
            if (_powerpointAdapter.HasSlides == false)
            {
                MessageBox.Show(
                    Resources.BarRibbon1_btn_Add_Click_This_presentation_has_no_slides_,
                    Resources.BarRibbon1_btn_Add_Click_Unable_to_add_bar,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }


            string selectedTheme = GetSelectedTheme();
            Controller.AddBarClicked(selectedTheme);

            // Enable items only when adding new bar
            // If users is refresing bar, all items remain enabled
            if (btn_Add.Label == "Add")
            {
                SwapStateBarRelatedItems();
            }

            SwapAddRefreshButton();
        }

        private void SwapStateBarRelatedItems()
        {
            bool newState = _hasBar;

            SwapItemsStateInGroup(styleGroup.Items, newState);
            SwapItemsStateInGroup(positionGroup.Items, newState);
            SwapItemsStateInGroup(themeGroup.Items, newState);
        }

        private static void SwapItemsStateInGroup(IEnumerable<RibbonControl> groupItems, bool newState)
        {
            foreach (var anItem in groupItems)
            {
                anItem.Enabled = newState;
            }
        }


        private void SwapAddRefreshButton()
        {
            btn_Remove.Enabled = _hasBar;

            if (_hasBar)
            {
                btn_Add.Label = "Refresh";
                btn_Add.Image = null;
                btn_Add.OfficeImageId = "Refresh";
            }
            else
            {
                btn_Add.Image = Resources.progressbar;
                btn_Add.Label = "Add";
            }
        }

        private void btn_Remove_Click(object sender, RibbonControlEventArgs e)
        {
            Controller.RemoveBarClicked();
            SwapAddRefreshButton();
            SwapStateBarRelatedItems();
        }

        private void btn_ChangeForeground_Click(object sender, RibbonControlEventArgs e)
        {
            // Microsoft.Office.Tools.Ribbon.RibbonButton b = (Microsoft.Office.Tools.Ribbon.RibbonButton)sender;
            // title.Contains("string", StringComparison.OrdinalIgnoreCase);

            if (DialogResult.OK == colorDialog_Foreground.ShowDialog())
            {
                colorDialog_Foreground.Color = colorDialog_Foreground.Color;

                _powerpointAdapter.AddInShapes().ForEach(
                    shape =>
                    {
                        if (_nameHelper.IsShapeForegroundShape(shape.Name))
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

                _powerpointAdapter.AddInShapes().ForEach(
                    shape =>
                    {
                        if (_nameHelper.IsShapeBackgroundShape(shape.Name))
                        {
                            shape.Fill.ForeColor.RGB = GetSelectedBackgroundColor();
                        }
                    });
            }
        }

        private void galleryTheme_Click(object sender, RibbonControlEventArgs e)
        {
            string selectedTheme = GetSelectedTheme();
            Controller.ChangeThemeClicked(selectedTheme);
        }

        private string GetSelectedTheme()
        {
            string selectedTheme = themeGallery.SelectedItem.ToString();
            return selectedTheme;
        }

        private void buttonAbout_Click(object sender, RibbonControlEventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void checkBox1_Click(object sender, RibbonControlEventArgs e)
        {
            string selectedTheme = GetSelectedTheme();
            Controller.AddBarClicked(selectedTheme);
        }

        private void dropDown_BarHeight_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            Controller.ChangeSizeClicked(BarSize());
        }

        private void btn_AlignTop_Click_1(object sender, RibbonControlEventArgs e)
        {
            btn_AlignBottom.Checked = false;
            btn_AlignTop.Checked = true;

            PositionOptionsChanged();
        }

        private void btn_AlignBottom_Click(object sender, RibbonControlEventArgs e)
        {
            btn_AlignTop.Checked = false;
            btn_AlignBottom.Checked = true;

            PositionOptionsChanged();
        }

        private void btn_AlignLeft_Click(object sender, RibbonControlEventArgs e)
        {
            btn_AlignLeft.Checked = true;
            btn_AlignRight.Checked = false;

            PositionOptionsChanged();
        }

        private void btn_AlignRight_Click(object sender, RibbonControlEventArgs e)
        {
            btn_AlignRight.Checked = true;
            btn_AlignLeft.Checked = false;

            PositionOptionsChanged();
        }

        private void PositionOptionsChanged()
        {
            Controller.PositionOptionsChanged(
                btn_AlignTop.Checked,
                btn_AlignRight.Checked,
                btn_AlignBottom.Checked,
                btn_AlignLeft.Checked
                );
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start("https://www.presentation-progressbar.com/");
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start("https://www.presentation-progressbar.com/report-bug");
        }
    }
}