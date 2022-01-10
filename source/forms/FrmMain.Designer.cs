using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{
    [DesignerGenerated()]
    public partial class FrmMain : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            PicBox = new PictureBox();
            PicBox.MouseMove += new MouseEventHandler(PicBox_MouseMove);
            PicBox.MouseWheel += new MouseEventHandler(PicBox_MouseWheel);
            TmrAnimation = new Timer(components);
            TmrAnimation.Tick += new EventHandler(TmrAnimation_Tick);
            DlgColor = new ColorDialog();
            TsZT1Graphic = new ToolStrip();
            TslZT1Graphic = new ToolStripLabel();
            TsbZT1New = new ToolStripButton();
            TsbZT1New.Click += new EventHandler(TsbZT1New_Click);
            TsbZT1Open = new ToolStripButton();
            TsbZT1Open.Click += new EventHandler(TsbZT1Open_Click);
            TsbZT1Write = new ToolStripButton();
            TsbZT1Write.MouseDown += new MouseEventHandler(TsbZT1Write_MouseDown);
            TsbZT1_OpenPal = new ToolStripButton();
            TsbZT1_OpenPal.Click += new EventHandler(TsbZT1_OpenPal_Click);
            Tss_Graphic_1 = new ToolStripSeparator();
            TsbGraphic_ExtraFrame = new ToolStripButton();
            TsbGraphic_ExtraFrame.Click += new EventHandler(TsbGraphic_ExtraFrame_Click);
            TslZT1_AnimSpeed = new ToolStripLabel();
            TstZT1_AnimSpeed = new ToolStripTextBox();
            TstZT1_AnimSpeed.Leave += new EventHandler(TstZT1_AnimSpeed_Leave);
            TstZT1_AnimSpeed.KeyDown += new KeyEventHandler(TstZT1_AnimSpeed_KeyDown);
            DlgOpen = new OpenFileDialog();
            Panel1 = new Panel();
            GBAnimation = new GroupBox();
            LblFrames = new Label();
            LblAnimTime = new Label();
            ChkPlayAnimation = new CheckBox();
            ChkPlayAnimation.CheckedChanged += new EventHandler(ChkPlayAnimation_CheckedChanged);
            TbFrames = new TrackBar();
            TbFrames.ValueChanged += new EventHandler(TbFrames_ValueChanged);
            GBOtherViews = new GroupBox();
            TVExplorer = new TreeView();
            TVExplorer.AfterSelect += new TreeViewEventHandler(TVExplorer_AfterSelect);
            TVExplorer.DoubleClick += new EventHandler(TVExplorer_DoubleClick);
            GBColors = new GroupBox();
            LblColorTool = new Label();
            LblColorDetails = new Label();
            LblColor = new Label();
            SsBar = new StatusStrip();
            ssFileName = new ToolStripStatusLabel();
            ssColor = new ToolStripStatusLabel();
            DgvPaletteMain = new DataGridView();
            DgvPaletteMain.CellDoubleClick += new DataGridViewCellEventHandler(DgvPaletteMain_CellDoubleClick);
            DgvPaletteMain.CellMouseClick += new DataGridViewCellMouseEventHandler(DgvPaletteMain_CellMouseClick);
            ColColor = new DataGridViewTextBoxColumn();
            DlgSave = new SaveFileDialog();
            TsFrame = new ToolStrip();
            TslFrame = new ToolStripLabel();
            TsbFrame_ImportPNG = new ToolStripButton();
            TsbFrame_ImportPNG.MouseDown += new MouseEventHandler(TsbFrame_ImportPNG_MouseDown);
            TsbFrame_ExportPNG = new ToolStripButton();
            TsbFrame_ExportPNG.Click += new EventHandler(TsbFrame_ExportPNG_Click);
            Tss_Frame_1 = new ToolStripSeparator();
            TsbFrame_Add = new ToolStripButton();
            TsbFrame_Add.Click += new EventHandler(TsbFrame_Add_Click);
            TsbFrame_Delete = new ToolStripButton();
            TsbFrame_Delete.Click += new EventHandler(TsbFrame_Delete_Click);
            Tss_Frame_2 = new ToolStripSeparator();
            TsbFrame_OffsetAll = new ToolStripButton();
            TsbFrame_OffsetAll.Click += new EventHandler(TsbFrame_OffsetAll_Click);
            TsbFrame_OffsetUp = new ToolStripButton();
            TsbFrame_OffsetUp.MouseDown += new MouseEventHandler(TsbFrame_OffsetUp_MouseDown);
            TsbFrame_OffsetDown = new ToolStripButton();
            TsbFrame_OffsetDown.MouseDown += new MouseEventHandler(TsbFrame_OffsetDown_MouseDown);
            TsbFrame_OffsetLeft = new ToolStripButton();
            TsbFrame_OffsetLeft.MouseDown += new MouseEventHandler(TsbFrame_OffsetLeft_MouseDown);
            TsbFrame_OffsetRight = new ToolStripButton();
            TsbFrame_OffsetRight.MouseDown += new MouseEventHandler(TsbFrame_OffsetRight_MouseDown);
            TslFrame_Offset = new ToolStripLabel();
            TstOffsetX = new ToolStripTextBox();
            TstOffsetX.KeyDown += new KeyEventHandler(TstOffsetX_KeyDown);
            TstOffsetX.Leave += new EventHandler(TstOffsetX_Leave);
            TstOffsetY = new ToolStripTextBox();
            TstOffsetY.KeyDown += new KeyEventHandler(TstOffsetY_KeyDown);
            TstOffsetY.Leave += new EventHandler(TstOffsetY_Leave);
            Tss_Frame_4 = new ToolStripSeparator();
            TsbFrame_IndexDecrease = new ToolStripButton();
            TsbFrame_IndexDecrease.Click += new EventHandler(TsbFrame_IndexDecrease_Click);
            TsbFrame_IndexIncrease = new ToolStripButton();
            TsbFrame_IndexIncrease.Click += new EventHandler(TsbFrame_IndexIncrease_Click);
            TslFrame_Index = new ToolStripLabel();
            TsTools = new ToolStrip();
            TslMisc = new ToolStripLabel();
            TsbOpenPalBldg8 = new ToolStripDropDownButton();
            TsbOpenPalBldg8.DropDownItemClicked += new ToolStripItemClickedEventHandler(TsbOpenPalBldg8_DropDownItemClicked);
            TsbOpenPalBldg16 = new ToolStripDropDownButton();
            TsbOpenPalBldg16.DropDownItemClicked += new ToolStripItemClickedEventHandler(TsbOpenPalBldg16_DropDownItemClicked);
            TssMisc_1 = new ToolStripSeparator();
            TsbCanvasBG = new ToolStripButton();
            TsbCanvasBG.Click += new EventHandler(TsbCanvasBG_Click);
            TsbPreview_BGGraphic = new ToolStripButton();
            TsbPreview_BGGraphic.Click += new EventHandler(TsbPreview_BGGraphic_Click);
            TssMisc_2 = new ToolStripSeparator();
            TsbBatchConversion = new ToolStripButton();
            TsbBatchConversion.Click += new EventHandler(TsbBatchConversion_Click);
            TsbBatchRotFix = new ToolStripButton();
            TsbBatchRotFix.Click += new EventHandler(TsbBatchRotFix_Click);
            TsbDelete_ZT1Files = new ToolStripButton();
            TsbDelete_ZT1Files.Click += new EventHandler(TsbDelete_ZT1Files_Click);
            TsbDelete_PNG = new ToolStripButton();
            TsbDelete_PNG.Click += new EventHandler(TsbDelete_PNG_Click);
            Tss_Misc_3 = new ToolStripSeparator();
            TslFrame_FP = new ToolStripLabel();
            TsbFrame_fpX = new ToolStripComboBox();
            TsbFrame_fpX.SelectedIndexChanged += new EventHandler(TsbFrame_fpX_SelectedIndexChanged);
            TsbFrame_fpY = new ToolStripComboBox();
            TsbFrame_fpY.SelectedIndexChanged += new EventHandler(TsbFrame_fpY_SelectedIndexChanged);
            TssMisc_4 = new ToolStripSeparator();
            TsbSettings = new ToolStripButton();
            TsbSettings.Click += new EventHandler(TsbSettings_Click);
            TsbAbout = new ToolStripButton();
            TsbAbout.Click += new EventHandler(TsbAbout_Click);
            MnuPal = new ContextMenuStrip(components);
            mnuPal_MoveEnd = new ToolStripMenuItem();
            mnuPal_MoveEnd.Click += new EventHandler(MnuPal_MoveEnd_Click);
            mnuPal_MoveUp = new ToolStripMenuItem();
            mnuPal_MoveUp.Click += new EventHandler(MnuPal_MoveUp_Click);
            mnuPal_MoveDown = new ToolStripMenuItem();
            mnuPal_MoveDown.Click += new EventHandler(MnuPal_MoveDown_Click);
            mnuPal_Replace = new ToolStripMenuItem();
            mnuPal_Replace.Click += new EventHandler(MnuPal_Replace_Click);
            mnuPal_Add = new ToolStripMenuItem();
            mnuPal_Add.Click += new EventHandler(MnuPal_Add_Click);
            mnuPal_SavePAL = new ToolStripMenuItem();
            mnuPal_SavePAL.Click += new EventHandler(MnuPal_SavePAL_Click);
            mnuPal_ExportPNG = new ToolStripMenuItem();
            mnuPal_ExportPNG.Click += new EventHandler(MnuPal_ExportPNG_Click);
            mnuPal_ImportPNG = new ToolStripMenuItem();
            mnuPal_ImportPNG.Click += new EventHandler(MnuPal_ImportPNG_Click);
            mnuPal_ImportGimpPalette = new ToolStripMenuItem();
            mnuPal_ImportGimpPalette.Click += new EventHandler(MnuPal_ImportGimpPalette_Click);
            CmdUpdateExplorerPane = new Button();
            CmdUpdateExplorerPane.Click += new EventHandler(CmdUpdateExplorerPane_Click);
            ((System.ComponentModel.ISupportInitialize)PicBox).BeginInit();
            TsZT1Graphic.SuspendLayout();
            Panel1.SuspendLayout();
            GBAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TbFrames).BeginInit();
            GBOtherViews.SuspendLayout();
            GBColors.SuspendLayout();
            SsBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvPaletteMain).BeginInit();
            TsFrame.SuspendLayout();
            TsTools.SuspendLayout();
            MnuPal.SuspendLayout();
            SuspendLayout();
            // 
            // PicBox
            // 
            PicBox.BackColor = SystemColors.ActiveCaption;
            PicBox.Dock = DockStyle.Top;
            PicBox.Location = new Point(0, 117);
            PicBox.Name = "PicBox";
            PicBox.Size = new Size(808, 512);
            PicBox.SizeMode = PictureBoxSizeMode.CenterImage;
            PicBox.TabIndex = 6;
            PicBox.TabStop = false;
            // 
            // TmrAnimation
            // 
            // 
            // DlgColor
            // 
            DlgColor.AnyColor = true;
            DlgColor.SolidColorOnly = true;
            // 
            // TsZT1Graphic
            // 
            TsZT1Graphic.Items.AddRange(new ToolStripItem[] { TslZT1Graphic, TsbZT1New, TsbZT1Open, TsbZT1Write, TsbZT1_OpenPal, Tss_Graphic_1, TsbGraphic_ExtraFrame, TslZT1_AnimSpeed, TstZT1_AnimSpeed });
            TsZT1Graphic.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            TsZT1Graphic.Location = new Point(0, 0);
            TsZT1Graphic.Name = "TsZT1Graphic";
            TsZT1Graphic.Size = new Size(1008, 39);
            TsZT1Graphic.TabIndex = 12;
            TsZT1Graphic.Text = "ToolStrip1";
            // 
            // TslZT1Graphic
            // 
            TslZT1Graphic.AutoSize = false;
            TslZT1Graphic.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            TslZT1Graphic.Name = "TslZT1Graphic";
            TslZT1Graphic.Size = new Size(100, 22);
            TslZT1Graphic.Text = "ZT1 Graphic";
            TslZT1Graphic.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TsbZT1New
            // 
            TsbZT1New.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbZT1New.Image = (Image)resources.GetObject("TsbZT1New.Image");
            TsbZT1New.ImageScaling = ToolStripItemImageScaling.None;
            TsbZT1New.ImageTransparentColor = Color.Magenta;
            TsbZT1New.Name = "TsbZT1New";
            TsbZT1New.Size = new Size(36, 36);
            TsbZT1New.Text = "New graphic";
            TsbZT1New.ToolTipText = "Create a new ZT1 Graphics File";
            // 
            // TsbZT1Open
            // 
            TsbZT1Open.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbZT1Open.Image = (Image)resources.GetObject("TsbZT1Open.Image");
            TsbZT1Open.ImageScaling = ToolStripItemImageScaling.None;
            TsbZT1Open.ImageTransparentColor = Color.Magenta;
            TsbZT1Open.Name = "TsbZT1Open";
            TsbZT1Open.Size = new Size(36, 36);
            TsbZT1Open.Text = "Open graphic";
            TsbZT1Open.ToolTipText = "Open a ZT1 Graphics File";
            // 
            // TsbZT1Write
            // 
            TsbZT1Write.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbZT1Write.Image = (Image)resources.GetObject("TsbZT1Write.Image");
            TsbZT1Write.ImageScaling = ToolStripItemImageScaling.None;
            TsbZT1Write.ImageTransparentColor = Color.Magenta;
            TsbZT1Write.Name = "TsbZT1Write";
            TsbZT1Write.Size = new Size(36, 36);
            TsbZT1Write.Text = "Save as ZT1 Graphic";
            // 
            // TsbZT1_OpenPal
            // 
            TsbZT1_OpenPal.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbZT1_OpenPal.Image = (Image)resources.GetObject("TsbZT1_OpenPal.Image");
            TsbZT1_OpenPal.ImageScaling = ToolStripItemImageScaling.None;
            TsbZT1_OpenPal.ImageTransparentColor = Color.Magenta;
            TsbZT1_OpenPal.Name = "TsbZT1_OpenPal";
            TsbZT1_OpenPal.Size = new Size(36, 36);
            TsbZT1_OpenPal.Text = "Open a ZT1 Color Palette (.pal)";
            // 
            // Tss_Graphic_1
            // 
            Tss_Graphic_1.Name = "Tss_Graphic_1";
            Tss_Graphic_1.Size = new Size(6, 39);
            // 
            // TsbGraphic_ExtraFrame
            // 
            TsbGraphic_ExtraFrame.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbGraphic_ExtraFrame.Image = (Image)resources.GetObject("TsbGraphic_ExtraFrame.Image");
            TsbGraphic_ExtraFrame.ImageScaling = ToolStripItemImageScaling.None;
            TsbGraphic_ExtraFrame.ImageTransparentColor = Color.Magenta;
            TsbGraphic_ExtraFrame.Name = "TsbGraphic_ExtraFrame";
            TsbGraphic_ExtraFrame.Size = new Size(36, 36);
            TsbGraphic_ExtraFrame.Text = "Use last frame as background frame";
            // 
            // TslZT1_AnimSpeed
            // 
            TslZT1_AnimSpeed.Name = "TslZT1_AnimSpeed";
            TslZT1_AnimSpeed.Size = new Size(100, 36);
            TslZT1_AnimSpeed.Text = "Animation speed:";
            // 
            // TstZT1_AnimSpeed
            // 
            TstZT1_AnimSpeed.Font = new Font("Segoe UI", 9.0f);
            TstZT1_AnimSpeed.Name = "TstZT1_AnimSpeed";
            TstZT1_AnimSpeed.Size = new Size(50, 39);
            TstZT1_AnimSpeed.ToolTipText = "Animation speed in milliseconds. Press [Enter] to confirm new values.";
            // 
            // Panel1
            // 
            Panel1.Controls.Add(GBAnimation);
            Panel1.Controls.Add(GBOtherViews);
            Panel1.Controls.Add(GBColors);
            Panel1.Controls.Add(SsBar);
            Panel1.Dock = DockStyle.Fill;
            Panel1.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            Panel1.Location = new Point(0, 629);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(808, 151);
            Panel1.TabIndex = 19;
            // 
            // GBAnimation
            // 
            GBAnimation.Controls.Add(LblFrames);
            GBAnimation.Controls.Add(LblAnimTime);
            GBAnimation.Controls.Add(ChkPlayAnimation);
            GBAnimation.Controls.Add(TbFrames);
            GBAnimation.Dock = DockStyle.Fill;
            GBAnimation.Font = new Font("Segoe UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            GBAnimation.Location = new Point(549, 0);
            GBAnimation.Name = "GBAnimation";
            GBAnimation.Size = new Size(259, 129);
            GBAnimation.TabIndex = 37;
            GBAnimation.TabStop = false;
            GBAnimation.Text = "Animation";
            // 
            // LblFrames
            // 
            LblFrames.AutoSize = true;
            LblFrames.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            LblFrames.Location = new Point(15, 90);
            LblFrames.Name = "LblFrames";
            LblFrames.Size = new Size(50, 13);
            LblFrames.TabIndex = 35;
            LblFrames.Text = "0 frames";
            // 
            // LblAnimTime
            // 
            LblAnimTime.AutoSize = true;
            LblAnimTime.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            LblAnimTime.Location = new Point(15, 103);
            LblAnimTime.Name = "LblAnimTime";
            LblAnimTime.Size = new Size(30, 13);
            LblAnimTime.TabIndex = 34;
            LblAnimTime.Text = "0 ms";
            // 
            // ChkPlayAnimation
            // 
            ChkPlayAnimation.AutoSize = true;
            ChkPlayAnimation.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            ChkPlayAnimation.Location = new Point(18, 69);
            ChkPlayAnimation.Name = "ChkPlayAnimation";
            ChkPlayAnimation.Size = new Size(101, 17);
            ChkPlayAnimation.TabIndex = 32;
            ChkPlayAnimation.Text = "Play animation";
            ChkPlayAnimation.UseVisualStyleBackColor = true;
            // 
            // TbFrames
            // 
            TbFrames.Dock = DockStyle.Top;
            TbFrames.Location = new Point(3, 18);
            TbFrames.Maximum = 1;
            TbFrames.Minimum = 1;
            TbFrames.Name = "TbFrames";
            TbFrames.Size = new Size(253, 45);
            TbFrames.TabIndex = 36;
            TbFrames.Value = 1;
            // 
            // GBOtherViews
            // 
            GBOtherViews.Controls.Add(TVExplorer);
            GBOtherViews.Controls.Add(CmdUpdateExplorerPane);
            GBOtherViews.Dock = DockStyle.Left;
            GBOtherViews.Font = new Font("Segoe UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            GBOtherViews.Location = new Point(240, 0);
            GBOtherViews.Name = "GBOtherViews";
            GBOtherViews.Size = new Size(309, 129);
            GBOtherViews.TabIndex = 38;
            GBOtherViews.TabStop = false;
            GBOtherViews.Text = "Explorer";
            // 
            // TVExplorer
            // 
            TVExplorer.Dock = DockStyle.Fill;
            TVExplorer.Location = new Point(3, 18);
            TVExplorer.Name = "TVExplorer";
            TVExplorer.Size = new Size(303, 85);
            TVExplorer.TabIndex = 0;
            // 
            // GBColors
            // 
            GBColors.Controls.Add(LblColorTool);
            GBColors.Controls.Add(LblColorDetails);
            GBColors.Controls.Add(LblColor);
            GBColors.Dock = DockStyle.Left;
            GBColors.Font = new Font("Segoe UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            GBColors.Location = new Point(0, 0);
            GBColors.Name = "GBColors";
            GBColors.Size = new Size(240, 129);
            GBColors.TabIndex = 36;
            GBColors.TabStop = false;
            GBColors.Text = "Color details";
            // 
            // LblColorTool
            // 
            LblColorTool.AutoSize = true;
            LblColorTool.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            LblColorTool.Location = new Point(18, 18);
            LblColorTool.Name = "LblColorTool";
            LblColorTool.Size = new Size(101, 13);
            LblColorTool.TabIndex = 37;
            LblColorTool.Text = "Move over a color.";
            // 
            // LblColorDetails
            // 
            LblColorDetails.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            LblColorDetails.Location = new Point(80, 33);
            LblColorDetails.Name = "LblColorDetails";
            LblColorDetails.Size = new Size(139, 70);
            LblColorDetails.TabIndex = 36;
            LblColorDetails.Text = "Color details";
            LblColorDetails.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LblColor
            // 
            LblColor.BorderStyle = BorderStyle.FixedSingle;
            LblColor.Location = new Point(15, 36);
            LblColor.Name = "LblColor";
            LblColor.Size = new Size(59, 43);
            LblColor.TabIndex = 35;
            LblColor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SsBar
            // 
            SsBar.Items.AddRange(new ToolStripItem[] { ssFileName, ssColor });
            SsBar.Location = new Point(0, 129);
            SsBar.Name = "SsBar";
            SsBar.Size = new Size(808, 22);
            SsBar.TabIndex = 27;
            // 
            // ssFileName
            // 
            ssFileName.Name = "ssFileName";
            ssFileName.Size = new Size(88, 17);
            ssFileName.Text = "No file opened.";
            // 
            // ssColor
            // 
            ssColor.Name = "ssColor";
            ssColor.Size = new Size(0, 17);
            // 
            // DgvPaletteMain
            // 
            DgvPaletteMain.AllowUserToAddRows = false;
            DgvPaletteMain.AllowUserToDeleteRows = false;
            DgvPaletteMain.AllowUserToResizeColumns = false;
            DgvPaletteMain.AllowUserToResizeRows = false;
            DgvPaletteMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            DgvPaletteMain.Columns.AddRange(new DataGridViewColumn[] { ColColor });
            DgvPaletteMain.Dock = DockStyle.Right;
            DgvPaletteMain.Location = new Point(808, 117);
            DgvPaletteMain.Name = "DgvPaletteMain";
            DgvPaletteMain.RowHeadersWidth = 75;
            DgvPaletteMain.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DgvPaletteMain.RowTemplate.Height = 20;
            DgvPaletteMain.Size = new Size(200, 663);
            DgvPaletteMain.TabIndex = 20;
            // 
            // ColColor
            // 
            ColColor.HeaderText = "Color";
            ColColor.Name = "ColColor";
            // 
            // TsFrame
            // 
            TsFrame.Items.AddRange(new ToolStripItem[] { TslFrame, TsbFrame_ImportPNG, TsbFrame_ExportPNG, Tss_Frame_1, TsbFrame_Add, TsbFrame_Delete, Tss_Frame_2, TsbFrame_OffsetAll, TsbFrame_OffsetUp, TsbFrame_OffsetDown, TsbFrame_OffsetLeft, TsbFrame_OffsetRight, TslFrame_Offset, TstOffsetX, TstOffsetY, Tss_Frame_4, TsbFrame_IndexDecrease, TsbFrame_IndexIncrease, TslFrame_Index });
            TsFrame.Location = new Point(0, 39);
            TsFrame.Name = "TsFrame";
            TsFrame.Size = new Size(1008, 39);
            TsFrame.TabIndex = 21;
            TsFrame.Text = "ToolStrip1";
            // 
            // TslFrame
            // 
            TslFrame.AutoSize = false;
            TslFrame.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold);
            TslFrame.Name = "TslFrame";
            TslFrame.Size = new Size(100, 22);
            TslFrame.Text = "Frame";
            TslFrame.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TsbFrame_ImportPNG
            // 
            TsbFrame_ImportPNG.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_ImportPNG.Image = (Image)resources.GetObject("TsbFrame_ImportPNG.Image");
            TsbFrame_ImportPNG.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_ImportPNG.ImageTransparentColor = Color.Magenta;
            TsbFrame_ImportPNG.Name = "TsbFrame_ImportPNG";
            TsbFrame_ImportPNG.Size = new Size(36, 36);
            TsbFrame_ImportPNG.Text = "Open .PNG to use in this frame. Right-click to import a .PNG as a new frame.";
            // 
            // TsbFrame_ExportPNG
            // 
            TsbFrame_ExportPNG.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_ExportPNG.Image = (Image)resources.GetObject("TsbFrame_ExportPNG.Image");
            TsbFrame_ExportPNG.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_ExportPNG.ImageTransparentColor = Color.Magenta;
            TsbFrame_ExportPNG.Name = "TsbFrame_ExportPNG";
            TsbFrame_ExportPNG.Size = new Size(36, 36);
            TsbFrame_ExportPNG.Text = "Save frame as .PNG";
            // 
            // Tss_Frame_1
            // 
            Tss_Frame_1.Name = "Tss_Frame_1";
            Tss_Frame_1.Size = new Size(6, 39);
            // 
            // TsbFrame_Add
            // 
            TsbFrame_Add.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_Add.Image = (Image)resources.GetObject("TsbFrame_Add.Image");
            TsbFrame_Add.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_Add.ImageTransparentColor = Color.Magenta;
            TsbFrame_Add.Name = "TsbFrame_Add";
            TsbFrame_Add.Size = new Size(36, 36);
            TsbFrame_Add.Text = "Add frame";
            // 
            // TsbFrame_Delete
            // 
            TsbFrame_Delete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_Delete.Image = (Image)resources.GetObject("TsbFrame_Delete.Image");
            TsbFrame_Delete.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_Delete.ImageTransparentColor = Color.Magenta;
            TsbFrame_Delete.Name = "TsbFrame_Delete";
            TsbFrame_Delete.Size = new Size(36, 36);
            TsbFrame_Delete.Text = "Delete frame";
            // 
            // Tss_Frame_2
            // 
            Tss_Frame_2.Name = "Tss_Frame_2";
            Tss_Frame_2.Size = new Size(6, 39);
            // 
            // TsbFrame_OffsetAll
            // 
            TsbFrame_OffsetAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_OffsetAll.Image = (Image)resources.GetObject("TsbFrame_OffsetAll.Image");
            TsbFrame_OffsetAll.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_OffsetAll.ImageTransparentColor = Color.Magenta;
            TsbFrame_OffsetAll.Name = "TsbFrame_OffsetAll";
            TsbFrame_OffsetAll.Size = new Size(36, 36);
            TsbFrame_OffsetAll.Text = "Force offset adjustments on all frames";
            // 
            // TsbFrame_OffsetUp
            // 
            TsbFrame_OffsetUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_OffsetUp.Image = (Image)resources.GetObject("TsbFrame_OffsetUp.Image");
            TsbFrame_OffsetUp.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_OffsetUp.ImageTransparentColor = Color.Magenta;
            TsbFrame_OffsetUp.Name = "TsbFrame_OffsetUp";
            TsbFrame_OffsetUp.Size = new Size(36, 36);
            TsbFrame_OffsetUp.Text = "Move up. Right-click to move up 16 pixels at a time.";
            // 
            // TsbFrame_OffsetDown
            // 
            TsbFrame_OffsetDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_OffsetDown.Image = (Image)resources.GetObject("TsbFrame_OffsetDown.Image");
            TsbFrame_OffsetDown.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_OffsetDown.ImageTransparentColor = Color.Magenta;
            TsbFrame_OffsetDown.Name = "TsbFrame_OffsetDown";
            TsbFrame_OffsetDown.Size = new Size(36, 36);
            TsbFrame_OffsetDown.Text = "Move down. Right-click to move down 16 pixels at a time.";
            // 
            // TsbFrame_OffsetLeft
            // 
            TsbFrame_OffsetLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_OffsetLeft.Image = (Image)resources.GetObject("TsbFrame_OffsetLeft.Image");
            TsbFrame_OffsetLeft.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_OffsetLeft.ImageTransparentColor = Color.Magenta;
            TsbFrame_OffsetLeft.Name = "TsbFrame_OffsetLeft";
            TsbFrame_OffsetLeft.Size = new Size(36, 36);
            TsbFrame_OffsetLeft.Text = "Move left. Right-click to move left 16 pixels at a time.";
            // 
            // TsbFrame_OffsetRight
            // 
            TsbFrame_OffsetRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_OffsetRight.Image = (Image)resources.GetObject("TsbFrame_OffsetRight.Image");
            TsbFrame_OffsetRight.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_OffsetRight.ImageTransparentColor = Color.Magenta;
            TsbFrame_OffsetRight.Name = "TsbFrame_OffsetRight";
            TsbFrame_OffsetRight.Size = new Size(36, 36);
            TsbFrame_OffsetRight.Text = "Move right. Right-click to move right 16 pixels at a time.";
            // 
            // TslFrame_Offset
            // 
            TslFrame_Offset.Name = "TslFrame_Offset";
            TslFrame_Offset.Size = new Size(68, 36);
            TslFrame_Offset.Text = "Offset: X, Y:";
            // 
            // TstOffsetX
            // 
            TstOffsetX.Font = new Font("Segoe UI", 9.0f);
            TstOffsetX.Name = "TstOffsetX";
            TstOffsetX.Size = new Size(50, 39);
            TstOffsetX.ToolTipText = "Offset X. Press [Enter] to confirm new values.";
            // 
            // TstOffsetY
            // 
            TstOffsetY.Font = new Font("Segoe UI", 9.0f);
            TstOffsetY.Name = "TstOffsetY";
            TstOffsetY.Size = new Size(50, 39);
            TstOffsetY.ToolTipText = "Offset Y. Press [Enter] to confirm new values.";
            // 
            // Tss_Frame_4
            // 
            Tss_Frame_4.Name = "Tss_Frame_4";
            Tss_Frame_4.Size = new Size(6, 39);
            // 
            // TsbFrame_IndexDecrease
            // 
            TsbFrame_IndexDecrease.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_IndexDecrease.Image = (Image)resources.GetObject("TsbFrame_IndexDecrease.Image");
            TsbFrame_IndexDecrease.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_IndexDecrease.ImageTransparentColor = Color.Magenta;
            TsbFrame_IndexDecrease.Name = "TsbFrame_IndexDecrease";
            TsbFrame_IndexDecrease.Size = new Size(36, 36);
            TsbFrame_IndexDecrease.Text = "Show frame earlier in animation";
            // 
            // TsbFrame_IndexIncrease
            // 
            TsbFrame_IndexIncrease.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbFrame_IndexIncrease.Image = (Image)resources.GetObject("TsbFrame_IndexIncrease.Image");
            TsbFrame_IndexIncrease.ImageScaling = ToolStripItemImageScaling.None;
            TsbFrame_IndexIncrease.ImageTransparentColor = Color.Magenta;
            TsbFrame_IndexIncrease.Name = "TsbFrame_IndexIncrease";
            TsbFrame_IndexIncrease.Size = new Size(36, 36);
            TsbFrame_IndexIncrease.Text = "Show frame later in animation";
            // 
            // TslFrame_Index
            // 
            TslFrame_Index.Name = "TslFrame_Index";
            TslFrame_Index.Size = new Size(65, 36);
            TslFrame_Index.Text = "Index: 0 / 0";
            // 
            // TsTools
            // 
            TsTools.Items.AddRange(new ToolStripItem[] { TslMisc, TsbOpenPalBldg8, TsbOpenPalBldg16, TssMisc_1, TsbCanvasBG, TsbPreview_BGGraphic, TssMisc_2, TsbBatchConversion, TsbBatchRotFix, TsbDelete_ZT1Files, TsbDelete_PNG, Tss_Misc_3, TslFrame_FP, TsbFrame_fpX, TsbFrame_fpY, TssMisc_4, TsbSettings, TsbAbout });
            TsTools.Location = new Point(0, 78);
            TsTools.Name = "TsTools";
            TsTools.Size = new Size(1008, 39);
            TsTools.TabIndex = 22;
            TsTools.Text = "Misc";
            // 
            // TslMisc
            // 
            TslMisc.AutoSize = false;
            TslMisc.Font = new Font("Segoe UI", 9.0f, FontStyle.Bold);
            TslMisc.Name = "TslMisc";
            TslMisc.Size = new Size(100, 22);
            TslMisc.Text = "Misc.";
            TslMisc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TsbOpenPalBldg8
            // 
            TsbOpenPalBldg8.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbOpenPalBldg8.Image = (Image)resources.GetObject("TsbOpenPalBldg8.Image");
            TsbOpenPalBldg8.ImageScaling = ToolStripItemImageScaling.None;
            TsbOpenPalBldg8.ImageTransparentColor = Color.Magenta;
            TsbOpenPalBldg8.Name = "TsbOpenPalBldg8";
            TsbOpenPalBldg8.Size = new Size(45, 36);
            TsbOpenPalBldg8.Text = "Quick access to 8-color palettes";
            // 
            // TsbOpenPalBldg16
            // 
            TsbOpenPalBldg16.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbOpenPalBldg16.Image = (Image)resources.GetObject("TsbOpenPalBldg16.Image");
            TsbOpenPalBldg16.ImageScaling = ToolStripItemImageScaling.None;
            TsbOpenPalBldg16.ImageTransparentColor = Color.Magenta;
            TsbOpenPalBldg16.Name = "TsbOpenPalBldg16";
            TsbOpenPalBldg16.Size = new Size(45, 36);
            TsbOpenPalBldg16.Text = "Quick access to 16-color palettes";
            // 
            // TssMisc_1
            // 
            TssMisc_1.Name = "TssMisc_1";
            TssMisc_1.Size = new Size(6, 39);
            // 
            // TsbCanvasBG
            // 
            TsbCanvasBG.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbCanvasBG.Image = (Image)resources.GetObject("TsbCanvasBG.Image");
            TsbCanvasBG.ImageScaling = ToolStripItemImageScaling.None;
            TsbCanvasBG.ImageTransparentColor = Color.Magenta;
            TsbCanvasBG.Name = "TsbCanvasBG";
            TsbCanvasBG.Size = new Size(36, 36);
            TsbCanvasBG.Text = "Change the canvas background";
            TsbCanvasBG.ToolTipText = "Background color of the image preview";
            // 
            // TsbPreview_BGGraphic
            // 
            TsbPreview_BGGraphic.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbPreview_BGGraphic.Image = (Image)resources.GetObject("TsbPreview_BGGraphic.Image");
            TsbPreview_BGGraphic.ImageScaling = ToolStripItemImageScaling.None;
            TsbPreview_BGGraphic.ImageTransparentColor = Color.Magenta;
            TsbPreview_BGGraphic.Name = "TsbPreview_BGGraphic";
            TsbPreview_BGGraphic.Size = new Size(36, 36);
            TsbPreview_BGGraphic.Text = "Open ZT1 Graphic and use it as background";
            // 
            // TssMisc_2
            // 
            TssMisc_2.Name = "TssMisc_2";
            TssMisc_2.Size = new Size(6, 39);
            // 
            // TsbBatchConversion
            // 
            TsbBatchConversion.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbBatchConversion.Image = (Image)resources.GetObject("TsbBatchConversion.Image");
            TsbBatchConversion.ImageScaling = ToolStripItemImageScaling.None;
            TsbBatchConversion.ImageTransparentColor = Color.Magenta;
            TsbBatchConversion.Name = "TsbBatchConversion";
            TsbBatchConversion.Size = new Size(36, 36);
            TsbBatchConversion.Text = "Batch graphic onversion: ZT1 Graphic <=> .PNG";
            TsbBatchConversion.ToolTipText = "Batch graphic graphic onversion: ZT1 Graphic <=> .PNG";
            // 
            // TsbBatchRotFix
            // 
            TsbBatchRotFix.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbBatchRotFix.Image = (Image)resources.GetObject("TsbBatchRotFix.Image");
            TsbBatchRotFix.ImageScaling = ToolStripItemImageScaling.None;
            TsbBatchRotFix.ImageTransparentColor = Color.Magenta;
            TsbBatchRotFix.Name = "TsbBatchRotFix";
            TsbBatchRotFix.Size = new Size(36, 36);
            TsbBatchRotFix.Text = "Batch offset fixing";
            // 
            // TsbDelete_ZT1Files
            // 
            TsbDelete_ZT1Files.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbDelete_ZT1Files.Image = (Image)resources.GetObject("TsbDelete_ZT1Files.Image");
            TsbDelete_ZT1Files.ImageScaling = ToolStripItemImageScaling.None;
            TsbDelete_ZT1Files.ImageTransparentColor = Color.Magenta;
            TsbDelete_ZT1Files.Name = "TsbDelete_ZT1Files";
            TsbDelete_ZT1Files.Size = new Size(36, 36);
            TsbDelete_ZT1Files.Text = "Delete all ZT1 Graphics, .ani-files and color palettes in the root folder";
            // 
            // TsbDelete_PNG
            // 
            TsbDelete_PNG.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbDelete_PNG.Image = (Image)resources.GetObject("TsbDelete_PNG.Image");
            TsbDelete_PNG.ImageScaling = ToolStripItemImageScaling.None;
            TsbDelete_PNG.ImageTransparentColor = Color.Magenta;
            TsbDelete_PNG.Name = "TsbDelete_PNG";
            TsbDelete_PNG.Size = new Size(36, 36);
            TsbDelete_PNG.Text = "Delete all .PNG files in the root folder";
            // 
            // Tss_Misc_3
            // 
            Tss_Misc_3.Name = "Tss_Misc_3";
            Tss_Misc_3.Size = new Size(6, 39);
            // 
            // TslFrame_FP
            // 
            TslFrame_FP.Name = "TslFrame_FP";
            TslFrame_FP.Size = new Size(79, 36);
            TslFrame_FP.Text = "Footprint X,Y:";
            // 
            // TsbFrame_fpX
            // 
            TsbFrame_fpX.AutoSize = false;
            TsbFrame_fpX.DropDownStyle = ComboBoxStyle.DropDownList;
            TsbFrame_fpX.Items.AddRange(new object[] { "2", "4", "6", "8", "10", "12", "14", "16", "18" });
            TsbFrame_fpX.Name = "TsbFrame_fpX";
            TsbFrame_fpX.Size = new Size(50, 23);
            // 
            // TsbFrame_fpY
            // 
            TsbFrame_fpY.AutoSize = false;
            TsbFrame_fpY.DropDownStyle = ComboBoxStyle.DropDownList;
            TsbFrame_fpY.Items.AddRange(new object[] { "2", "4", "6", "8", "10", "12", "14", "16", "18" });
            TsbFrame_fpY.Name = "TsbFrame_fpY";
            TsbFrame_fpY.Size = new Size(50, 23);
            // 
            // TssMisc_4
            // 
            TssMisc_4.Name = "TssMisc_4";
            TssMisc_4.Size = new Size(6, 39);
            // 
            // TsbSettings
            // 
            TsbSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbSettings.Image = (Image)resources.GetObject("TsbSettings.Image");
            TsbSettings.ImageScaling = ToolStripItemImageScaling.None;
            TsbSettings.ImageTransparentColor = Color.Magenta;
            TsbSettings.Name = "TsbSettings";
            TsbSettings.Size = new Size(36, 36);
            TsbSettings.Text = "Settings";
            // 
            // TsbAbout
            // 
            TsbAbout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsbAbout.Image = (Image)resources.GetObject("TsbAbout.Image");
            TsbAbout.ImageScaling = ToolStripItemImageScaling.None;
            TsbAbout.ImageTransparentColor = Color.Magenta;
            TsbAbout.Name = "TsbAbout";
            TsbAbout.Size = new Size(36, 36);
            TsbAbout.Text = "About";
            // 
            // MnuPal
            // 
            MnuPal.Items.AddRange(new ToolStripItem[] { mnuPal_MoveEnd, mnuPal_MoveUp, mnuPal_MoveDown, mnuPal_Replace, mnuPal_Add, mnuPal_SavePAL, mnuPal_ExportPNG, mnuPal_ImportPNG, mnuPal_ImportGimpPalette });
            MnuPal.Name = "MnuPal";
            MnuPal.Size = new Size(245, 202);
            // 
            // mnuPal_MoveEnd
            // 
            mnuPal_MoveEnd.Name = "mnuPal_MoveEnd";
            mnuPal_MoveEnd.Size = new Size(244, 22);
            mnuPal_MoveEnd.Text = "Move to end";
            // 
            // mnuPal_MoveUp
            // 
            mnuPal_MoveUp.Name = "mnuPal_MoveUp";
            mnuPal_MoveUp.Size = new Size(244, 22);
            mnuPal_MoveUp.Text = "Move up";
            // 
            // mnuPal_MoveDown
            // 
            mnuPal_MoveDown.Name = "mnuPal_MoveDown";
            mnuPal_MoveDown.Size = new Size(244, 22);
            mnuPal_MoveDown.Text = "Move down";
            // 
            // mnuPal_Replace
            // 
            mnuPal_Replace.Name = "mnuPal_Replace";
            mnuPal_Replace.Size = new Size(244, 22);
            mnuPal_Replace.Text = "Replace color";
            // 
            // mnuPal_Add
            // 
            mnuPal_Add.Name = "mnuPal_Add";
            mnuPal_Add.Size = new Size(244, 22);
            mnuPal_Add.Text = "Add color entry";
            // 
            // mnuPal_SavePAL
            // 
            mnuPal_SavePAL.Name = "mnuPal_SavePAL";
            mnuPal_SavePAL.Size = new Size(244, 22);
            mnuPal_SavePAL.Text = "Save as .PAL";
            // 
            // mnuPal_ExportPNG
            // 
            mnuPal_ExportPNG.Name = "mnuPal_ExportPNG";
            mnuPal_ExportPNG.Size = new Size(244, 22);
            mnuPal_ExportPNG.Text = "Export to PNG palette";
            // 
            // mnuPal_ImportPNG
            // 
            mnuPal_ImportPNG.Name = "mnuPal_ImportPNG";
            mnuPal_ImportPNG.Size = new Size(244, 22);
            mnuPal_ImportPNG.Text = "Replace with PNG palette";
            // 
            // mnuPal_ImportGimpPalette
            // 
            mnuPal_ImportGimpPalette.Name = "mnuPal_ImportGimpPalette";
            mnuPal_ImportGimpPalette.Size = new Size(244, 22);
            mnuPal_ImportGimpPalette.Text = "Replace with GIMP Color palette";
            // 
            // CmdUpdateExplorerPane
            // 
            CmdUpdateExplorerPane.Dock = DockStyle.Bottom;
            CmdUpdateExplorerPane.Location = new Point(3, 103);
            CmdUpdateExplorerPane.Name = "CmdUpdateExplorerPane";
            CmdUpdateExplorerPane.Size = new Size(303, 23);
            CmdUpdateExplorerPane.TabIndex = 1;
            CmdUpdateExplorerPane.Text = "Refresh";
            CmdUpdateExplorerPane.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 780);
            Controls.Add(Panel1);
            Controls.Add(PicBox);
            Controls.Add(DgvPaletteMain);
            Controls.Add(TsTools);
            Controls.Add(TsFrame);
            Controls.Add(TsZT1Graphic);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmMain";
            Text = "ZT Studio";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)PicBox).EndInit();
            TsZT1Graphic.ResumeLayout(false);
            TsZT1Graphic.PerformLayout();
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            GBAnimation.ResumeLayout(false);
            GBAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TbFrames).EndInit();
            GBOtherViews.ResumeLayout(false);
            GBColors.ResumeLayout(false);
            GBColors.PerformLayout();
            SsBar.ResumeLayout(false);
            SsBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DgvPaletteMain).EndInit();
            TsFrame.ResumeLayout(false);
            TsFrame.PerformLayout();
            TsTools.ResumeLayout(false);
            TsTools.PerformLayout();
            MnuPal.ResumeLayout(false);
            Load += new EventHandler(Form1_Load);
            FormClosing += new FormClosingEventHandler(FrmMain_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        internal PictureBox PicBox;
        internal Timer TmrAnimation;
        internal ColorDialog DlgColor;
        internal ToolStrip TsZT1Graphic;
        internal ToolStripButton TsbZT1Open;
        internal OpenFileDialog DlgOpen;
        internal Panel Panel1;
        internal DataGridView DgvPaletteMain;
        internal StatusStrip SsBar;
        internal ToolStripStatusLabel ssFileName;
        internal SaveFileDialog DlgSave;
        internal ToolStripStatusLabel ssColor;
        internal ToolStripButton TsbZT1_OpenPal;
        internal ToolStripButton TsbZT1Write;
        internal ToolStripLabel TslZT1Graphic;
        internal ToolStrip TsFrame;
        internal ToolStripLabel TslFrame;
        internal ToolStripButton TsbFrame_ExportPNG;
        internal ToolStrip TsTools;
        internal ToolStripLabel TslMisc;
        internal ToolStripButton TsbFrame_ImportPNG;
        internal ToolStripSeparator Tss_Frame_2;
        internal ToolStripButton TsbFrame_OffsetUp;
        internal ToolStripButton TsbFrame_OffsetDown;
        internal ToolStripButton TsbFrame_OffsetLeft;
        internal ToolStripButton TsbFrame_OffsetRight;
        internal ToolStripDropDownButton TsbOpenPalBldg8;
        internal ToolStripDropDownButton TsbOpenPalBldg16;
        internal ToolStripSeparator TssMisc_1;
        internal ToolStripButton TsbCanvasBG;
        internal ToolStripSeparator TssMisc_2;
        internal ToolStripButton TsbBatchConversion;
        internal ToolStripSeparator TssMisc_4;
        internal ToolStripButton TsbSettings;
        internal ToolStripButton TsbAbout;
        internal ToolStripSeparator Tss_Graphic_1;
        internal ToolStripLabel TslZT1_AnimSpeed;
        internal ToolStripTextBox TstZT1_AnimSpeed;
        internal ToolStripLabel TslFrame_Offset;
        internal ToolStripButton TsbFrame_IndexDecrease;
        internal ToolStripButton TsbFrame_IndexIncrease;
        internal ToolStripLabel TslFrame_Index;
        internal ToolStripButton TsbPreview_BGGraphic;
        internal ToolStripSeparator Tss_Frame_1;
        internal ToolStripButton TsbFrame_Add;
        internal ToolStripButton TsbFrame_Delete;
        internal ToolStripButton TsbGraphic_ExtraFrame;
        internal ToolStripButton TsbDelete_PNG;
        internal ToolStripButton TsbDelete_ZT1Files;
        internal ToolStripSeparator Tss_Frame_4;
        internal ContextMenuStrip MnuPal;
        internal ToolStripMenuItem mnuPal_MoveEnd;
        internal ToolStripMenuItem mnuPal_MoveUp;
        internal ToolStripMenuItem mnuPal_MoveDown;
        internal ToolStripMenuItem mnuPal_Replace;
        internal ToolStripMenuItem mnuPal_Add;
        internal ToolStripMenuItem mnuPal_ImportPNG;
        internal ToolStripMenuItem mnuPal_ExportPNG;
        internal ToolStripMenuItem mnuPal_SavePAL;
        internal ToolStripMenuItem mnuPal_ImportGimpPalette;
        internal ToolStripButton TsbBatchRotFix;
        internal ToolStripButton TsbZT1New;
        internal DataGridViewTextBoxColumn ColColor;
        internal GroupBox GBAnimation;
        internal Label LblFrames;
        internal Label LblAnimTime;
        internal CheckBox ChkPlayAnimation;
        internal GroupBox GBColors;
        internal Label LblColorTool;
        internal Label LblColorDetails;
        internal Label LblColor;
        internal TrackBar TbFrames;
        internal GroupBox GBOtherViews;
        internal TreeView TVExplorer;
        internal ToolStripTextBox TstOffsetX;
        internal ToolStripTextBox TstOffsetY;
        internal ToolStripSeparator Tss_Misc_3;
        internal ToolStripLabel TslFrame_FP;
        internal ToolStripComboBox TsbFrame_fpX;
        internal ToolStripComboBox TsbFrame_fpY;
        internal ToolStripButton TsbFrame_OffsetAll;
        internal Button CmdUpdateExplorerPane;
    }
}