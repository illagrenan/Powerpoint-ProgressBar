using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using ProgressBar.DataStructs;
using ProgressBar.RunningPresentation;
using ProgressBar.ProgressShape;
using System.Threading;
using System;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Resources;
using System.Drawing;

namespace ProgressBar
{

    public partial class ProgressBar_Ribbon
    {
        private Presentation currentPresentation;
        const string WEBSIT = "http://www.presentation-progressbar.com";

        private int timerTicks = 0;

        private void ProgressBar_Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            Globals.ThisAddIn.Application.AfterNewPresentation += Application_AfterNewPresentation;
            Globals.ThisAddIn.Application.AfterPresentationOpen += Application_AfterNewPresentation;

            // Globals.ThisAddIn.Application.AfterPresentationOpen += Application_AfterNewPresentation;
        }

        void Application_AfterNewPresentation(Microsoft.Office.Interop.PowerPoint.Presentation Pres)
        {
            currentPresentation = new Presentation(Globals.ThisAddIn.Application);
            UpdateStatusBarMessage.OnNewStatusMessage += UpdateStatusBarMessage_OnNewStatusMessage;

            detectBar.Start();
            DisableAlignButtons();

            FillDropDown();
            SetDropdownDefaultHeight();
            SetDefaultColors();
        }

        void UpdateStatusBarMessage_OnNewStatusMessage(string strMessage)
        {
            timerTicks = 0;
            detectBar.Start();
        }

        private Version GetExecutingAssemblyVersion()
        {
            Assembly thisAssem = Assembly.GetExecutingAssembly();
            AssemblyName thisAssemName = thisAssem.GetName();
            return thisAssemName.Version;
        }

        private void SetDefaultColors()
        {
            this.colorDialog_Background.Color = SolidBar.Background;
            this.colorDialog_Foreground.Color = SolidBar.Foreground;
        }

        private void FillDropDown()
        {
            for (int i = 1; i < SolidBar.MAX_HEIGHT; i++)
            {
                RibbonDropDownItem itemToAdd = Factory.CreateRibbonDropDownItem();
                itemToAdd.Label = (i).ToString();
                dropDown_BarHeight.Items.Add(itemToAdd);
            }
        }

        private void SetDropdownDefaultHeight()
        {
            dropDown_BarHeight.SelectedItemIndex = SolidBar.DEFAULT_HEIGHT;
        }

        private void DisableAlignButtons()
        {
            btn_AlignTop.Enabled = false;
            btn_AlinBottom.Enabled = false;
            btn_AlignRight.Enabled = false;
            btn_AlignLeft.Enabled = false;
        }

        private void EnableAlignButtons()
        {
            btn_AlignTop.Enabled = true;
            btn_AlinBottom.Enabled = true;
            btn_AlignRight.Enabled = true;
            btn_AlignLeft.Enabled = true;
        }

        private void UncheckAllAlignButton()
        {
            btn_AlignTop.Checked = false;
            btn_AlinBottom.Checked = false;
            btn_AlignRight.Checked = false;
            btn_AlignLeft.Checked = false;
        }


        private void btn_Add_Click(object sender, RibbonControlEventArgs e)
        {

            btn_Remove.Enabled = true;
            //Thread listenThread = new Thread(new ThreadStart(currentPresentation.GenerateProgressBar));

            if (currentPresentation.HasProgressBar)
            {
                currentPresentation.RemoveAllAddInShapes();
            }

            currentPresentation.GenerateProgressBar();

            //listenThread.Start();
            // listenThread.Join();
            EnableAlignButtons();
        }

        private void btn_Remove_Click(object sender, RibbonControlEventArgs e)
        {
            btn_Remove.Enabled = false;

            currentPresentation.RemoveAllAddInShapes();
            DisableAlignButtons();
        }

        private void btn_ChangeBackground_Click(object sender, RibbonControlEventArgs e)
        {
            if (DialogResult.OK == colorDialog_Background.ShowDialog())
            {
                SolidBar.Background = colorDialog_Background.Color;
                currentPresentation.ColorBacground(); ;
            }
        }

        private void btn_ChangeForeground_Click(object sender, RibbonControlEventArgs e)
        {
            if (DialogResult.OK == colorDialog_Foreground.ShowDialog())
            {
                SolidBar.Foreground = colorDialog_Foreground.Color;
                currentPresentation.ColorForeground();
            }
        }

        private void btn_AlignTop_Click(object sender, RibbonControlEventArgs e)
        {
            btn_AlignTop.Checked = true;
            btn_AlinBottom.Checked = false;

            SolidBar.Instance.Alignment = BarAlign.TOP;
            currentPresentation.ApplyTopMargin();
        }

        private void btn_AlignBottom_Click(object sender, RibbonControlEventArgs e)
        {
            btn_AlignTop.Checked = false;
            btn_AlinBottom.Checked = true;

            SolidBar.Instance.Alignment = BarAlign.BOTTOM;
            currentPresentation.ApplyTopMargin();
        }

        private void dropDown1_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            RibbonDropDown ribbonDropDown = (RibbonDropDown)sender;
            int height = ribbonDropDown.SelectedItemIndex + 1;
            float previousHeight = SolidBar.Instance.Height;
            SolidBar.Instance.Height = height;
            // float heightDifference = SolidBar.Instance.Height - previousHeight;

            currentPresentation.ResizeAllShapes();

            if (SolidBar.Instance.Alignment == BarAlign.BOTTOM
                        || currentPresentation.currentTheme == DataStructs.Theme.DOTTED)
            {
                currentPresentation.ApplyTopMargin();
            }

            if (currentPresentation.currentTheme == DataStructs.Theme.DOTTED)
            {
                currentPresentation.ApplyLeftMargin();
            }
        }

        private void editBox1_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void checkBox1_Click(object sender, RibbonControlEventArgs e)
        {
            RibbonCheckBox ch = (RibbonCheckBox)sender;

            if (ch.Checked)
            {
                SolidBar.DisableOnFirstSlide = true;
            }
            else
            {
                SolidBar.DisableOnFirstSlide = false;
            }

            if (currentPresentation.HasProgressBar)
            {
                currentPresentation.RemoveAllAddInShapes();
                currentPresentation.GenerateProgressBar();
                currentPresentation.ApplyTopMargin();
                currentPresentation.ApplyLeftMargin();
            }
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            System.Diagnostics.Process.Start(WEBSIT);
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            System.Diagnostics.Process.Start(WEBSIT + "/report-bug");
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            AboutInformations ab = new AboutInformations();
            ab.ShowDialog();

            /*System.Diagnostics.Process.Start(WEBSIT + "/check-for-updates/" + GetExecutingAssemblyVersion().ToString());*/
        }

        private void detectBar_Tick(object sender, EventArgs e)
        {
            if (currentPresentation.AddedShapes.Count > 0)
            {
                if (currentPresentation.AddedShapes[0].Top >= 0
                            && currentPresentation.AddedShapes[0].Top <= 5)
                {
                    btn_AlignTop.Checked = true;
                    btn_AlinBottom.Checked = false;
                }
                else
                {
                    btn_AlignTop.Checked = false;
                    btn_AlinBottom.Checked = true;
                }
                currentPresentation.HasProgressBar = true;
                EnableAlignButtons();
                btn_Remove.Enabled = true;
                detectBar.Stop();
            }

            timerTicks++;

            if (timerTicks == 20)
            {
                detectBar.Stop();
                Debug.WriteLine("stopped");
            }
        }

        private int f = 0;
        private Microsoft.Office.Interop.PowerPoint.Shape kk;
        private DateTime now;

        private void timer1_Tick(object sender, EventArgs e)
        {

            /*            private void button4_Click(object sender, RibbonControlEventArgs e)
                    {
                        Microsoft.Office.Core.MsoAutoShapeType shapeBackground = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;

                        var ff = currentPresentation.PresentationSlides[1].Shapes.AddShape(shapeBackground, 0, 0, 600, 300);

                        //ff.BackgroundStyle.co
                        ff.Fill.ForeColor.RGB = ShapeProperties.BackgroundOLE;
                        now = DateTime.Now;
                        ff.TextFrame.TextRange.Text = "0 from " + now.ToString();
                        kk = ff;

            

                        this.timer1.Start();
                    }*/


            kk.TextFrame.TextRange.Text = (DateTime.Now - now).ToString() + " from " + now.ToString();
            f++;
        }

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Core.MsoAutoShapeType shapeBackground = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;

            var ff = currentPresentation.PresentationSlides[1].Shapes.AddShape(shapeBackground, 0, 0, 600, 300);

            //ff.BackgroundStyle.co
            ff.Fill.ForeColor.RGB = SolidBar.BackgroundOLE;
            now = DateTime.Now;
            ff.TextFrame.TextRange.Text = "0 from " + now.ToString();
            kk = ff;



            this.timer1.Start();
        }

        private void gallery1_Click(object sender, RibbonControlEventArgs e)
        {
            var selectedTheme = gallery1.SelectedItem.ToString();
            Theme selectedThemeType;

            switch (selectedTheme)
            {
                case "Solid":
                    selectedThemeType = DataStructs.Theme.SOLID;
                    break;

                case "Dotted":
                    selectedThemeType = DataStructs.Theme.DOTTED;

                    break;

                default:
                    throw new Exception("Unknown theme " + selectedTheme);
            }

            if (currentPresentation.currentTheme != selectedThemeType)
            {
                currentPresentation.currentTheme = selectedThemeType;

                if (selectedThemeType == DataStructs.Theme.DOTTED)
                {
                    SolidBar.Instance.Alignment = BarAlign.BOTTOM;
                    btn_AlignTop.Checked = false;
                    btn_AlinBottom.Checked = true;
                    btn_AlignLeft.Checked = true;

                    btn_AlignLeft.Visible = btn_AlignRight.Visible = true;

                    button4.Visible = true;
                }
                else
                {
                    btn_AlignLeft.Visible = btn_AlignRight.Visible = false;
                    UncheckAllAlignButton();
                    btn_AlignTop.Checked = true;
                    SolidBar.Instance.Alignment = BarAlign.TOP;

                    button4.Visible = false;
                }


                if (currentPresentation.HasProgressBar)
                {
                    currentPresentation.RemoveAllAddInShapes();
                    currentPresentation.GenerateProgressBar();
                    currentPresentation.ApplyTopMargin();
                }
            }

            ChangeControlLabels();
        }

        private void ChangeControlLabels()
        {
            if (currentPresentation.currentTheme == DataStructs.Theme.DOTTED)
            {
                dropDown_BarHeight.Label = "Size";
                btn_ChangeBackground.Label = "Active color";
                btn_ChangeForeground.Label = "Inactive color";
            }
            else
            {
                dropDown_BarHeight.Label = "Height";
                btn_ChangeBackground.Label = "Background";
                btn_ChangeForeground.Label = "Foreground";
            }
        }

        private void btn_AlignLeft_Click(object sender, RibbonControlEventArgs e)
        {
            btn_AlignLeft.Checked = true;
            btn_AlignRight.Checked = false;
            SolidBar.Instance.Alignment = BarAlign.LEFT;
            currentPresentation.ApplyLeftMargin();
        }

        private void btn_AlignRight_Click(object sender, RibbonControlEventArgs e)
        {
            btn_AlignLeft.Checked = false;
            btn_AlignRight.Checked = true;
            SolidBar.Instance.Alignment = BarAlign.RIGHT;
            currentPresentation.ApplyLeftMargin();
        }

    }
}

