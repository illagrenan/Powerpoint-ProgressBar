#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools.Ribbon;
using ProgressBar.Adapter;
using ProgressBar.Bar;
using ProgressBar.Controller;
using ProgressBar.CustomExceptions;
using ProgressBar.DataStructs;
using ProgressBar.Helper;
using ProgressBar.Model;
using ProgressBar.Properties;
using ProgressBar.Tag;
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
        private ITagAdapter _tagAdapter;
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region MVCLogic

        public BarRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();

            
        }

        public IBarController Controller { get; set; }

        public void Register(IBarModel model)
        {

            this.log.Debug("Debug message");
            this.log.Info("haha");

            model.BarCreated += model_BarCreated;
            model.BarSizeChanged += model_BarSizeChanged;
            model.BarRemoved += model_BarRemoved;
            model.BarsRegistered += model_BarsRegistered;
            model.AlignmentOptionsChanged += model_AlignmentOptionsChanged;
            model.ColorsSet += model_ColorsSet;
            model.ExternalBarAdded += model_ExternalBarAdded;
            model.BarInfoRetrieved += model_BarInfoRetrieved;

            model.SizesSet += ModelSizesSet;
            model.DefaultSizeSet += ModelDefaultSizeSet;
        }

        public void Release(IBarModel model)
        {
            model.BarCreated -= model_BarCreated;
            model.BarRemoved -= model_BarRemoved;
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

        private static void SwapItemsStateInGroup(IEnumerable<RibbonControl> groupItems, bool newState)
        {
            foreach (var anItem in groupItems)
            {
                anItem.Enabled = newState;
            }
        }

        /// <summary>
        ///     Occurs after a presentation is created.
        ///     In MS 2010 when powerpoint is opened or when File -> New.
        /// </summary>
        /// <param name="pres"></param>
        private void AfterNewPresentationHandle(Presentation pres)
        {
            SetupTagWriter();
        }

        /// <summary>
        ///     Occurs after an existing presentation is opened.
        ///     Double click on file or File -> Open...
        /// </summary>
        /// <param name="pres"></param>
        private void AfterPresentationOpenHandle(Presentation pres)
        {
            SetupTagWriter();
            Debug.WriteLine("Detecting bar...");

            if (_tagAdapter.HasPersistedBar())
            {
                Debug.WriteLine("Bar detected.");
                var barFromTag = _tagAdapter.GetPersistedBar();
                Controller.BarDetected(barFromTag.Bar, barFromTag.PositionOptions);
            }
        }

        private void BarRibbon1_Close(object sender, EventArgs e)
        {
            Release(_model);
        }

        private void BarRibbon1_Load(object sender, RibbonUIEventArgs e)
        {
            Register(_model);

            _hasBar = false;

            Globals.ThisAddIn.Application.AfterNewPresentation += AfterNewPresentationHandle;
            Globals.ThisAddIn.Application.AfterPresentationOpen += AfterPresentationOpenHandle;

            Globals.ThisAddIn.Application.SlideSelectionChanged += OnSlidesChanged;
            Globals.ThisAddIn.Application.PresentationBeforeClose += BeforePresentationClose;

            Controller.SetupColors();
            Controller.SetupSizes();
            Controller.SetupRegisteredBars();
        }

        private int BarSize()
        {
            return int.Parse(dropDown_BarSize.SelectedItem.Label);
        }

        /// <summary>
        ///     Represents a Presentation object before it closes.
        /// </summary>
        /// <param name="pres"></param>
        /// <param name="cancel"></param>
        private void BeforePresentationClose(Presentation pres, ref bool cancel)
        {
            if (_hasBar && pres.Saved == MsoTriState.msoTrue)
            {
                Controller.SaveBarToMetadata();
                pres.Save();
            }
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


            // Enable items only when adding new bar
            // If users is refresing bar, all items remain enabled
            if (btn_Add.Label == "Add")
            {
                string selectedTheme = GetSelectedTheme();
                Controller.AddBarClicked(selectedTheme);
                SwapStateBarRelatedItems();
            }
            else
            {
                Controller.RefreshBarClicked();
            }

            SwapAddRefreshButton();
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

        private void btn_AlignTop_Click_1(object sender, RibbonControlEventArgs e)
        {
            btn_AlignBottom.Checked = false;
            btn_AlignTop.Checked = true;

            PositionOptionsChanged();
        }

        private void btn_ChangeBackground_Click(object sender, RibbonControlEventArgs e)
        {
            if (DialogResult.OK == colorDialog_Inactive.ShowDialog())
            {
                if (ProceedWithSameColors() == false)
                {
                    return;
                }

                colorDialog_Inactive.Color = colorDialog_Inactive.Color;

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

        private void btn_ChangeForeground_Click(object sender, RibbonControlEventArgs e)
        {
            // Microsoft.Office.Tools.Ribbon.RibbonButton b = (Microsoft.Office.Tools.Ribbon.RibbonButton)sender;
            // title.Contains("string", StringComparison.OrdinalIgnoreCase);

            if (DialogResult.OK == colorDialog_Active.ShowDialog())
            {
                if (ProceedWithSameColors() == false)
                {
                    return;
                }

                colorDialog_Active.Color = colorDialog_Active.Color;

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

        private void btn_Remove_Click(object sender, RibbonControlEventArgs e)
        {
            Controller.RemoveBarClicked();
            SwapAddRefreshButton();
            SwapStateBarRelatedItems();
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start("https://www.presentation-progressbar.com/");
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            Process.Start("https://www.presentation-progressbar.com/report-bug");
        }

        private void buttonAbout_Click(object sender, RibbonControlEventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private PresentationInfo CreateInfo(IEnumerable<Slide> visibleSlides)
        {
            var presentationInfo = new PresentationInfo
            {
                Height = _powerpointAdapter.PresentationHeight(),
                Width = _powerpointAdapter.PresentationWidth(),
                SlidesCount = visibleSlides.Count(),
                UserSize = BarSize(),
                DisableOnFirstSlide = checkBox1.Checked
            };

            return presentationInfo;
        }

        private void dropDown_BarHeight_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            Controller.ChangeSizeClicked(BarSize());
        }

        private void galleryTheme_Click(object sender, RibbonControlEventArgs e)
        {
            string selectedTheme = GetSelectedTheme();
            Controller.ChangeThemeClicked(selectedTheme);
        }

        private int GetSelectedBackgroundColor()
        {
            return ColorTranslator.ToOle(colorDialog_Inactive.Color);
        }

        private int GetSelectedForegroundColor()
        {
            return ColorTranslator.ToOle(colorDialog_Active.Color);
        }

        private string GetSelectedTheme()
        {
            string selectedTheme = themeGallery.SelectedItem.ToString();
            return selectedTheme;
        }

        private void checkBox1_Click(object sender, RibbonControlEventArgs e)
        {
            Controller.DisableOnFirstSlideClicked();
        }

        private void model_AlignmentOptionsChanged(IPositionOptions newAlignmentOptions)
        {
            SetPositionOptions(newAlignmentOptions);
        }

        private void model_BarCreated(IBar createdBar)
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

        private void model_BarInfoRetrieved(IBar obj)
        {
            var bt = new TagContainer
            {
                ActiveColor = colorDialog_Active.Color,
                InactiveColor = colorDialog_Inactive.Color,
                SizeSelectedItemIndex = dropDown_BarSize.SelectedItemIndex,
                ThemeSelectedItemIndex = themeGallery.SelectedItemIndex,
                DisableFirstSlideChecked = checkBox1.Checked,
                Bar = obj,
                PositionOptions = obj.PositionOptions
            };

            _tagAdapter.PersistContainer(bt);
        }

        private void model_BarRemoved()
        {
            List<Shape> shape = _powerpointAdapter.AddInShapes();
            shape.ForEach(s => s.Delete());

            _hasBar = false;
            _tagAdapter.RemoveTagContainer();
        }

        private void model_BarSizeChanged(IBar obj)
        {
            Controller.RemoveBarClicked();
            Controller.AddBarClicked(GetSelectedTheme());
        }

        private void model_BarsRegistered(List<IBar> bars)
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

        private void model_ColorsSet(Dictionary<ShapeType, Color> obj)
        {
            colorDialog_Inactive.Color = obj[ShapeType.Inactive];
            colorDialog_Active.Color = obj[ShapeType.Active];
        }

        private void model_ExternalBarAdded()
        {
            var barFromTag = _tagAdapter.GetPersistedBar();

            _hasBar = true;

            colorDialog_Inactive.Color = barFromTag.InactiveColor;
            colorDialog_Active.Color = barFromTag.ActiveColor;
            checkBox1.Checked = barFromTag.DisableFirstSlideChecked;
            dropDown_BarSize.SelectedItemIndex = barFromTag.SizeSelectedItemIndex;
            themeGallery.SelectedItemIndex = barFromTag.ThemeSelectedItemIndex;

            SwapStateBarRelatedItems();
            SwapAddRefreshButton();
        }

        private void ModelDefaultSizeSet(int defaultSize)
        {
            dropDown_BarSize.SelectedItemIndex = defaultSize;
        }

        private void ModelSizesSet(int[] obj)
        {
            foreach (int size in obj)
            {
                RibbonDropDownItem itemToAdd = Factory.CreateRibbonDropDownItem();
                itemToAdd.Label = size.ToString(CultureInfo.InvariantCulture);
                dropDown_BarSize.Items.Add(itemToAdd);
            }
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

        private void PositionOptionsChanged()
        {
            Controller.PositionOptionsChanged(
                btn_AlignTop.Checked,
                btn_AlignRight.Checked,
                btn_AlignBottom.Checked,
                btn_AlignLeft.Checked
                );
        }

        private bool ProceedWithSameColors()
        {
            if (colorDialog_Inactive.Color == colorDialog_Active.Color)
            {
                var userResult = MessageBox.Show(
                    Resources.BarRibbon_ProceedWithSameColors_Do_you_want_to_cancel_this_change_,
                    Resources.BarRibbon_ProceedWithSameColors_Active_and_inactive_colors_are_the_same,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );

                if (userResult == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
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

        private void SetupTagWriter()
        {
            Debug.WriteLine("Tag Writer set");

            var ww = new TagWriter(Globals.ThisAddIn.Application.ActivePresentation.Tags);
            ITagAdapter adap = new TagAdapter(ww);
            _tagAdapter = adap;
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

        private void SwapStateBarRelatedItems()
        {
            bool newState = _hasBar;

            SwapItemsStateInGroup(styleGroup.Items, newState);
            SwapItemsStateInGroup(positionGroup.Items, newState);
            SwapItemsStateInGroup(themeGroup.Items, newState);
        }
    }
}