namespace ProgressBar
{
    partial class ProgressBar_Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ProgressBar_Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.components = new System.ComponentModel.Container();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btn_Add = this.Factory.CreateRibbonButton();
            this.btn_Remove = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btn_ChangeForeground = this.Factory.CreateRibbonButton();
            this.btn_ChangeBackground = this.Factory.CreateRibbonButton();
            this.dropDown_BarHeight = this.Factory.CreateRibbonDropDown();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.btn_AlignTop = this.Factory.CreateRibbonToggleButton();
            this.btn_AlinBottom = this.Factory.CreateRibbonToggleButton();
            this.checkBox1 = this.Factory.CreateRibbonCheckBox();
            this.btn_AlignLeft = this.Factory.CreateRibbonToggleButton();
            this.btn_AlignRight = this.Factory.CreateRibbonToggleButton();
            this.Theme = this.Factory.CreateRibbonGroup();
            this.gallery1 = this.Factory.CreateRibbonGallery();
            this.button4 = this.Factory.CreateRibbonButton();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.menu1 = this.Factory.CreateRibbonMenu();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.button3 = this.Factory.CreateRibbonButton();
            this.colorDialog_Background = new System.Windows.Forms.ColorDialog();
            this.colorDialog_Foreground = new System.Windows.Forms.ColorDialog();
            this.detectBar = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group3.SuspendLayout();
            this.Theme.SuspendLayout();
            this.group4.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.group3);
            this.tab1.Groups.Add(this.Theme);
            this.tab1.Groups.Add(this.group4);
            this.tab1.Label = "Progress Bar";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btn_Add);
            this.group1.Items.Add(this.btn_Remove);
            this.group1.Label = "Progress Bar";
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
            this.btn_Remove.Image = global::ProgressBar.Properties.Resources.shape_square_delete;
            this.btn_Remove.Label = "Remove";
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.ShowImage = true;
            this.btn_Remove.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Remove_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.btn_ChangeForeground);
            this.group2.Items.Add(this.btn_ChangeBackground);
            this.group2.Items.Add(this.dropDown_BarHeight);
            this.group2.Label = "Style";
            this.group2.Name = "group2";
            // 
            // btn_ChangeForeground
            // 
            this.btn_ChangeForeground.Image = global::ProgressBar.Properties.Resources.color_wheel;
            this.btn_ChangeForeground.Label = "Foreground";
            this.btn_ChangeForeground.Name = "btn_ChangeForeground";
            this.btn_ChangeForeground.ShowImage = true;
            this.btn_ChangeForeground.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ChangeForeground_Click);
            // 
            // btn_ChangeBackground
            // 
            this.btn_ChangeBackground.Image = global::ProgressBar.Properties.Resources.color_wheel;
            this.btn_ChangeBackground.Label = "Background";
            this.btn_ChangeBackground.Name = "btn_ChangeBackground";
            this.btn_ChangeBackground.ShowImage = true;
            this.btn_ChangeBackground.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ChangeBackground_Click);
            // 
            // dropDown_BarHeight
            // 
            this.dropDown_BarHeight.Label = "Height";
            this.dropDown_BarHeight.Name = "dropDown_BarHeight";
            this.dropDown_BarHeight.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDown1_SelectionChanged);
            // 
            // group3
            // 
            this.group3.Items.Add(this.btn_AlignTop);
            this.group3.Items.Add(this.btn_AlinBottom);
            this.group3.Items.Add(this.checkBox1);
            this.group3.Items.Add(this.btn_AlignLeft);
            this.group3.Items.Add(this.btn_AlignRight);
            this.group3.Label = "Bar Position";
            this.group3.Name = "group3";
            // 
            // btn_AlignTop
            // 
            this.btn_AlignTop.Checked = true;
            this.btn_AlignTop.Image = global::ProgressBar.Properties.Resources.border_2_top;
            this.btn_AlignTop.Label = "Top";
            this.btn_AlignTop.Name = "btn_AlignTop";
            this.btn_AlignTop.ShowImage = true;
            this.btn_AlignTop.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignTop_Click);
            // 
            // btn_AlinBottom
            // 
            this.btn_AlinBottom.Image = global::ProgressBar.Properties.Resources.border_2_bottom;
            this.btn_AlinBottom.Label = "Bottom";
            this.btn_AlinBottom.Name = "btn_AlinBottom";
            this.btn_AlinBottom.ShowImage = true;
            this.btn_AlinBottom.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignBottom_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Label = "Disable on First slide";
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkBox1_Click);
            // 
            // btn_AlignLeft
            // 
            this.btn_AlignLeft.Image = global::ProgressBar.Properties.Resources.border_2_left;
            this.btn_AlignLeft.Label = "Left";
            this.btn_AlignLeft.Name = "btn_AlignLeft";
            this.btn_AlignLeft.ShowImage = true;
            this.btn_AlignLeft.Visible = false;
            this.btn_AlignLeft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignLeft_Click);
            // 
            // btn_AlignRight
            // 
            this.btn_AlignRight.Image = global::ProgressBar.Properties.Resources.border_2_right;
            this.btn_AlignRight.Label = "Right";
            this.btn_AlignRight.Name = "btn_AlignRight";
            this.btn_AlignRight.ShowImage = true;
            this.btn_AlignRight.Visible = false;
            this.btn_AlignRight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AlignRight_Click);
            // 
            // Theme
            // 
            this.Theme.Items.Add(this.gallery1);
            this.Theme.Items.Add(this.button4);
            this.Theme.Label = "Theme";
            this.Theme.Name = "Theme";
            // 
            // gallery1
            // 
            this.gallery1.Image = global::ProgressBar.Properties.Resources.select_by_color;
            ribbonDropDownItemImpl1.Image = global::ProgressBar.Properties.Resources.theme_solid;
            ribbonDropDownItemImpl1.Label = "Solid";
            ribbonDropDownItemImpl2.Image = global::ProgressBar.Properties.Resources.theme_dotted;
            ribbonDropDownItemImpl2.Label = "Dotted";
            this.gallery1.Items.Add(ribbonDropDownItemImpl1);
            this.gallery1.Items.Add(ribbonDropDownItemImpl2);
            this.gallery1.Label = "Bar theme";
            this.gallery1.Name = "gallery1";
            this.gallery1.ShowImage = true;
            this.gallery1.ShowItemSelection = true;
            this.gallery1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.gallery1_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Image = global::ProgressBar.Properties.Resources.error;
            this.button4.Label = "Selected theme is beta.";
            this.button4.Name = "button4";
            this.button4.ShowImage = true;
            this.button4.Visible = false;
            // 
            // group4
            // 
            this.group4.Items.Add(this.menu1);
            this.group4.Label = "Help and Support";
            this.group4.Name = "group4";
            // 
            // menu1
            // 
            this.menu1.Items.Add(this.button1);
            this.menu1.Items.Add(this.button2);
            this.menu1.Items.Add(this.button3);
            this.menu1.Label = "Help and Support";
            this.menu1.Name = "menu1";
            // 
            // button1
            // 
            this.button1.Image = global::ProgressBar.Properties.Resources.globe_model;
            this.button1.Label = "Visit AddIn Website";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::ProgressBar.Properties.Resources.bug;
            this.button2.Label = "Report Bug";
            this.button2.Name = "button2";
            this.button2.ShowImage = true;
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Image = global::ProgressBar.Properties.Resources.information;
            this.button3.Label = "About";
            this.button3.Name = "button3";
            this.button3.ShowImage = true;
            this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
            // 
            // colorDialog_Background
            // 
            this.colorDialog_Background.AnyColor = true;
            // 
            // colorDialog_Foreground
            // 
            this.colorDialog_Foreground.AnyColor = true;
            // 
            // detectBar
            // 
            this.detectBar.Tick += new System.EventHandler(this.detectBar_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ProgressBar_Ribbon
            // 
            this.Name = "ProgressBar_Ribbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ProgressBar_Ribbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.Theme.ResumeLayout(false);
            this.Theme.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Add;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Remove;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ChangeBackground;
        private System.Windows.Forms.ColorDialog colorDialog_Background;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ChangeForeground;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        private System.Windows.Forms.ColorDialog colorDialog_Foreground;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlignTop;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlinBottom;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown_BarHeight;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        private System.Windows.Forms.Timer detectBar;
        private System.Windows.Forms.Timer timer1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Theme;
        internal Microsoft.Office.Tools.Ribbon.RibbonGallery gallery1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlignLeft;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_AlignRight;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
    }

    partial class ThisRibbonCollection
    {
        internal ProgressBar_Ribbon Ribbon1
        {
            get { return this.GetRibbon<ProgressBar_Ribbon>(); }
        }
    }
}
