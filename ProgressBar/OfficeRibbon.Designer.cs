namespace ProgressBar
{
    partial class BarRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog_Foreground = new System.Windows.Forms.ColorDialog();
            this.colorDialog_Background = new System.Windows.Forms.ColorDialog();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btn_Add = this.Factory.CreateRibbonButton();
            this.btn_Remove = this.Factory.CreateRibbonButton();
            this.styleGroup = this.Factory.CreateRibbonGroup();
            this.btn_ChangeForeground = this.Factory.CreateRibbonButton();
            this.btn_ChangeBackground = this.Factory.CreateRibbonButton();
            this.dropDown_BarHeight = this.Factory.CreateRibbonDropDown();
            this.positionGroup = this.Factory.CreateRibbonGroup();
            this.btn_AlignTop = this.Factory.CreateRibbonToggleButton();
            this.btn_AlignBottom = this.Factory.CreateRibbonToggleButton();
            this.checkBox1 = this.Factory.CreateRibbonCheckBox();
            this.btn_AlignRight = this.Factory.CreateRibbonToggleButton();
            this.btn_AlignLeft = this.Factory.CreateRibbonToggleButton();
            this.themeGroup = this.Factory.CreateRibbonGroup();
            this.themeGallery = this.Factory.CreateRibbonGallery();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.menu1 = this.Factory.CreateRibbonMenu();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.button3 = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.styleGroup.SuspendLayout();
            this.positionGroup.SuspendLayout();
            this.themeGroup.SuspendLayout();
            this.group4.SuspendLayout();
            // 
            // colorDialog_Foreground
            // 
            this.colorDialog_Foreground.AnyColor = true;
            // 
            // colorDialog_Background
            // 
            this.colorDialog_Background.AnyColor = true;
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.styleGroup);
            this.tab1.Groups.Add(this.positionGroup);
            this.tab1.Groups.Add(this.themeGroup);
            this.tab1.Groups.Add(this.group4);
            this.tab1.Label = "Progress Bar";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btn_Add);
            this.group1.Items.Add(this.btn_Remove);
            this.group1.Label = "Progress bar";
            this.group1.Name = "group1";
            // 
            // btn_Add
            // 
            this.btn_Add.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_Add.Image = global::ProgressBar.Properties.Resources.progressbar;
            this.btn_Add.Label = "Add";
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.ShowImage = true;
            this.btn_Add.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Add_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_Remove.Enabled = false;
            this.btn_Remove.Image = global::ProgressBar.Properties.Resources.shape_square_delete;
            this.btn_Remove.Label = "Remove";
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.ShowImage = true;
            this.btn_Remove.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Remove_Click);
            // 
            // styleGroup
            // 
            this.styleGroup.Items.Add(this.btn_ChangeForeground);
            this.styleGroup.Items.Add(this.btn_ChangeBackground);
            this.styleGroup.Items.Add(this.dropDown_BarHeight);
            this.styleGroup.Label = "Color and size";
            this.styleGroup.Name = "styleGroup";
            // 
            // btn_ChangeForeground
            // 
            this.btn_ChangeForeground.Enabled = false;
            this.btn_ChangeForeground.Image = global::ProgressBar.Properties.Resources.color_wheel;
            this.btn_ChangeForeground.Label = "Active";
            this.btn_ChangeForeground.Name = "btn_ChangeForeground";
            this.btn_ChangeForeground.ShowImage = true;
            this.btn_ChangeForeground.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ChangeForeground_Click);
            // 
            // btn_ChangeBackground
            // 
            this.btn_ChangeBackground.Enabled = false;
            this.btn_ChangeBackground.Image = global::ProgressBar.Properties.Resources.color_wheel;
            this.btn_ChangeBackground.Label = "Inactive";
            this.btn_ChangeBackground.Name = "btn_ChangeBackground";
            this.btn_ChangeBackground.ShowImage = true;
            this.btn_ChangeBackground.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ChangeBackground_Click);
            // 
            // dropDown_BarHeight
            // 
            this.dropDown_BarHeight.Enabled = false;
            this.dropDown_BarHeight.Label = "Size";
            this.dropDown_BarHeight.Name = "dropDown_BarHeight";
            this.dropDown_BarHeight.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDown_BarHeight_SelectionChanged);
            // 
            // positionGroup
            // 
            this.positionGroup.Items.Add(this.btn_AlignTop);
            this.positionGroup.Items.Add(this.btn_AlignBottom);
            this.positionGroup.Items.Add(this.checkBox1);
            this.positionGroup.Items.Add(this.btn_AlignRight);
            this.positionGroup.Items.Add(this.btn_AlignLeft);
            this.positionGroup.Label = "Bar position";
            this.positionGroup.Name = "positionGroup";
            // 
            // btn_AlignTop
            // 
            this.btn_AlignTop.Checked = true;
            this.btn_AlignTop.Enabled = false;
            this.btn_AlignTop.Label = "Top";
            this.btn_AlignTop.Name = "btn_AlignTop";
            this.btn_AlignTop.OfficeImageId = "ObjectsAlignTop";
            this.btn_AlignTop.ShowImage = true;
            this.btn_AlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignTop_Click_1);
            // 
            // btn_AlignBottom
            // 
            this.btn_AlignBottom.Enabled = false;
            this.btn_AlignBottom.Label = "Bottom";
            this.btn_AlignBottom.Name = "btn_AlignBottom";
            this.btn_AlignBottom.OfficeImageId = "ObjectsAlignBottom";
            this.btn_AlignBottom.ShowImage = true;
            this.btn_AlignBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignBottom_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Enabled = false;
            this.checkBox1.Label = "Disable on First slide";
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBox1_Click);
            // 
            // btn_AlignRight
            // 
            this.btn_AlignRight.Enabled = false;
            this.btn_AlignRight.Label = "Right";
            this.btn_AlignRight.Name = "btn_AlignRight";
            this.btn_AlignRight.OfficeImageId = "ObjectsAlignRight";
            this.btn_AlignRight.ShowImage = true;
            this.btn_AlignRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignRight_Click);
            // 
            // btn_AlignLeft
            // 
            this.btn_AlignLeft.Enabled = false;
            this.btn_AlignLeft.Label = "Left";
            this.btn_AlignLeft.Name = "btn_AlignLeft";
            this.btn_AlignLeft.OfficeImageId = "ObjectsAlignLeft";
            this.btn_AlignLeft.ShowImage = true;
            this.btn_AlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignLeft_Click);
            // 
            // themeGroup
            // 
            this.themeGroup.Items.Add(this.themeGallery);
            this.themeGroup.Label = "Theme";
            this.themeGroup.Name = "themeGroup";
            // 
            // themeGallery
            // 
            this.themeGallery.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.themeGallery.Enabled = false;
            this.themeGallery.Image = global::ProgressBar.Properties.Resources.select_by_color;
            this.themeGallery.Label = "Bar theme";
            this.themeGallery.Name = "themeGallery";
            this.themeGallery.ShowImage = true;
            this.themeGallery.ShowItemSelection = true;
            this.themeGallery.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.galleryTheme_Click);
            // 
            // group4
            // 
            this.group4.Items.Add(this.menu1);
            this.group4.Label = "Help";
            this.group4.Name = "group4";
            // 
            // menu1
            // 
            this.menu1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.menu1.Items.Add(this.button1);
            this.menu1.Items.Add(this.button2);
            this.menu1.Items.Add(this.button3);
            this.menu1.Label = "Help and support";
            this.menu1.Name = "menu1";
            this.menu1.OfficeImageId = "Help";
            this.menu1.ShowImage = true;
            // 
            // button1
            // 
            this.button1.Label = "Visit add-in homepage";
            this.button1.Name = "button1";
            this.button1.OfficeImageId = "ViewOnlineConnection";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::ProgressBar.Properties.Resources.bug;
            this.button2.Label = "Report problem";
            this.button2.Name = "button2";
            this.button2.ShowImage = true;
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Label = "About this add-in";
            this.button3.Name = "button3";
            this.button3.OfficeImageId = "Info";
            this.button3.ShowImage = true;
            this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAbout_Click);
            // 
            // BarRibbon
            // 
            this.Name = "BarRibbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Close += new System.EventHandler(this.BarRibbon1_Close);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BarRibbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.styleGroup.ResumeLayout(false);
            this.styleGroup.PerformLayout();
            this.positionGroup.ResumeLayout(false);
            this.positionGroup.PerformLayout();
            this.themeGroup.ResumeLayout(false);
            this.themeGroup.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Add;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Remove;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup styleGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ChangeForeground;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ChangeBackground;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown_BarHeight;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup positionGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup themeGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonGallery themeGallery;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        private System.Windows.Forms.ColorDialog colorDialog_Foreground;
        private System.Windows.Forms.ColorDialog colorDialog_Background;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlignTop;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlignBottom;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlignLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlignRight;
    }

    partial class ThisRibbonCollection
    {
        internal BarRibbon Ribbon
        {
            get { return this.GetRibbon<BarRibbon>(); }
        }
    }
}
