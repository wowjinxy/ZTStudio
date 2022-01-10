using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{
    [DesignerGenerated()]
    public partial class FrmSettings : Form
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
            dlgBrowseFolder = new FolderBrowserDialog();
            tpPalette = new TabPage();
            ChkPalImportPNGForceAddAll = new CheckBox();
            ChkPalImportPNGForceAddAll.CheckedChanged += new EventHandler(ChkPalImportPNGForceAddAll_CheckedChanged);
            ChkPalImportPNGForceAddAll.MouseHover += new EventHandler(ChkPalImportPNGForceAddAll_MouseHover);
            tpWritePNG = new TabPage();
            ChkPNGTransparentBG = new CheckBox();
            ChkPNGTransparentBG.CheckedChanged += new EventHandler(ChkPNGTransparentBG_CheckedChanged);
            ChkPNGTransparentBG.MouseHover += new EventHandler(ChkPNGTransparentBG_MouseHover);
            LblHowToExportPNG = new Label();
            LblHowToExportPNG.MouseHover += new EventHandler(LblHowToExportPNG_MouseHover);
            CboPNGExport_Crop = new ComboBox();
            CboPNGExport_Crop.SelectedValueChanged += new EventHandler(CboPNGExport_Crop_SelectedValueChanged);
            tpWriteZT1 = new TabPage();
            ChkExportZT1_Ani = new CheckBox();
            ChkExportZT1_Ani.CheckedChanged += new EventHandler(ChkExportZT1_Ani_CheckedChanged);
            ChkExportZT1_Ani.MouseHover += new EventHandler(ChkExportZT1_Ani_MouseHover);
            ChkExportZT1_AddZTAFBytes = new CheckBox();
            ChkExportZT1_AddZTAFBytes.CheckedChanged += new EventHandler(ChkConvert_AddZTAFBytes_CheckedChanged);
            ChkExportZT1_AddZTAFBytes.MouseHover += new EventHandler(ChkExportZT1_AddZTAFBytes_MouseHover);
            tpRenderingFrames = new TabPage();
            ChkPNGRenderBGFrame = new CheckBox();
            ChkPNGRenderBGFrame.CheckedChanged += new EventHandler(ChkPNGRenderBGFrame_CheckedChanged);
            ChkPNGRenderBGFrame.MouseHover += new EventHandler(ChkPNGRenderBGFrame_MouseHover);
            Label3 = new Label();
            numFrameDefaultAnimSpeed = new NumericUpDown();
            numFrameDefaultAnimSpeed.ValueChanged += new EventHandler(NumFrameAnimSpeed_ValueChanged);
            LblDefaultAnimSpeed = new Label();
            LblDefaultAnimSpeed.MouseHover += new EventHandler(LblDefaultAnimSpeed_MouseHover);
            ChkRenderFrame_BGGraphic = new CheckBox();
            ChkRenderFrame_BGGraphic.CheckedChanged += new EventHandler(ChkExportPNG_BGGraphic_CheckedChanged);
            ChkRenderFrame_BGGraphic.MouseHover += new EventHandler(ChkRenderFrame_BGGraphic_MouseHover);
            tpConversions = new TabPage();
            TxtConvert_fileNameDelimiter = new TextBox();
            TxtConvert_fileNameDelimiter.TextChanged += new EventHandler(TxtConvert_fileNameDelimiter_TextChanged);
            LblConvert_fileNameDelimiter = new Label();
            LblConvert_fileNameDelimiter.MouseHover += new EventHandler(LblConvert_fileNameDelimiter_MouseHover);
            ChkConvert_SharedColorPalette = new CheckBox();
            ChkConvert_SharedColorPalette.CheckedChanged += new EventHandler(ChkConvert_SharedColorPalette_CheckedChanged);
            ChkConvert_SharedColorPalette.MouseHover += new EventHandler(ChkConvert_SharedColorPalette_MouseHover);
            ChkConvert_Overwrite = new CheckBox();
            ChkConvert_Overwrite.CheckedChanged += new EventHandler(ChkConvert_Overwrite_CheckedChanged);
            ChkConvert_Overwrite.MouseHover += new EventHandler(ChkConvert_Overwrite_MouseHover);
            ChkConvert_DeleteOriginal = new CheckBox();
            ChkConvert_DeleteOriginal.CheckedChanged += new EventHandler(ChkConvert_DeleteOriginal_CheckedChanged);
            ChkConvert_DeleteOriginal.MouseHover += new EventHandler(ChkConvert_DeleteOriginal_MouseHover);
            LblExportPNG_Index = new Label();
            LblExportPNG_Index.MouseHover += new EventHandler(LblExportPNG_Index_MouseHover);
            NumConvert_PNGStartIndex = new NumericUpDown();
            NumConvert_PNGStartIndex.ValueChanged += new EventHandler(NumExportPNG_StartIndex_ValueChanged);
            tpFolders = new TabPage();
            BtnBrowsePal16 = new Button();
            BtnBrowsePal16.Click += new EventHandler(BtnBrowsePal16_Click);
            BtnBrowsePal8 = new Button();
            BtnBrowsePal8.Click += new EventHandler(BtnBrowsePal8_Click);
            BtnBrowse = new Button();
            BtnBrowse.Click += new EventHandler(BtnBrowse_Click);
            txtFolderPal16 = new TextBox();
            txtFolderPal8 = new TextBox();
            txtRootFolder = new TextBox();
            LblColorPal16 = new Label();
            LblColorPal16.MouseHover += new EventHandler(LblColorPal16_MouseHover);
            LblColorPal8 = new Label();
            LblColorPal8.MouseHover += new EventHandler(LblColorPal8_MouseHover);
            LblRootFolder = new Label();
            LblRootFolder.MouseHover += new EventHandler(LblRootFolder_MouseHover);
            TCSettings = new TabControl();
            LblHelp = new Label();
            LblHelpTopic = new Label();
            tpPalette.SuspendLayout();
            tpWritePNG.SuspendLayout();
            tpWriteZT1.SuspendLayout();
            tpRenderingFrames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFrameDefaultAnimSpeed).BeginInit();
            tpConversions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumConvert_PNGStartIndex).BeginInit();
            tpFolders.SuspendLayout();
            TCSettings.SuspendLayout();
            SuspendLayout();
            // 
            // tpPalette
            // 
            tpPalette.Controls.Add(ChkPalImportPNGForceAddAll);
            tpPalette.Location = new Point(4, 22);
            tpPalette.Name = "tpPalette";
            tpPalette.Size = new Size(710, 248);
            tpPalette.TabIndex = 6;
            tpPalette.Text = "Color palettes";
            tpPalette.UseVisualStyleBackColor = true;
            // 
            // ChkPalImportPNGForceAddAll
            // 
            ChkPalImportPNGForceAddAll.AutoSize = true;
            ChkPalImportPNGForceAddAll.Location = new Point(24, 15);
            ChkPalImportPNGForceAddAll.Name = "ChkPalImportPNGForceAddAll";
            ChkPalImportPNGForceAddAll.Size = new Size(344, 17);
            ChkPalImportPNGForceAddAll.TabIndex = 23;
            ChkPalImportPNGForceAddAll.Text = "Add all colors (even identical) when importing from .PNG files ";
            ChkPalImportPNGForceAddAll.UseVisualStyleBackColor = true;
            // 
            // tpWritePNG
            // 
            tpWritePNG.Controls.Add(ChkPNGTransparentBG);
            tpWritePNG.Controls.Add(LblHowToExportPNG);
            tpWritePNG.Controls.Add(CboPNGExport_Crop);
            tpWritePNG.Location = new Point(4, 22);
            tpWritePNG.Name = "tpWritePNG";
            tpWritePNG.Padding = new Padding(3);
            tpWritePNG.Size = new Size(710, 248);
            tpWritePNG.TabIndex = 4;
            tpWritePNG.Text = "Export as PNG";
            tpWritePNG.UseVisualStyleBackColor = true;
            // 
            // ChkPNGTransparentBG
            // 
            ChkPNGTransparentBG.AutoSize = true;
            ChkPNGTransparentBG.Location = new Point(24, 118);
            ChkPNGTransparentBG.Name = "ChkPNGTransparentBG";
            ChkPNGTransparentBG.Size = new Size(481, 17);
            ChkPNGTransparentBG.TabIndex = 15;
            ChkPNGTransparentBG.Text = "Use transparent background with alphachannel (instead of ZT Studio background col" + "or)";
            ChkPNGTransparentBG.UseVisualStyleBackColor = true;
            // 
            // LblHowToExportPNG
            // 
            LblHowToExportPNG.AutoSize = true;
            LblHowToExportPNG.Location = new Point(23, 39);
            LblHowToExportPNG.Name = "LblHowToExportPNG";
            LblHowToExportPNG.Size = new Size(108, 13);
            LblHowToExportPNG.TabIndex = 14;
            LblHowToExportPNG.Text = "PNG Export method";
            // 
            // CboPNGExport_Crop
            // 
            CboPNGExport_Crop.DropDownStyle = ComboBoxStyle.DropDownList;
            CboPNGExport_Crop.FormattingEnabled = true;
            CboPNGExport_Crop.Location = new Point(26, 55);
            CboPNGExport_Crop.Name = "CboPNGExport_Crop";
            CboPNGExport_Crop.Size = new Size(520, 21);
            CboPNGExport_Crop.TabIndex = 13;
            // 
            // tpWriteZT1
            // 
            tpWriteZT1.Controls.Add(ChkExportZT1_Ani);
            tpWriteZT1.Controls.Add(ChkExportZT1_AddZTAFBytes);
            tpWriteZT1.Location = new Point(4, 22);
            tpWriteZT1.Name = "tpWriteZT1";
            tpWriteZT1.Padding = new Padding(3);
            tpWriteZT1.Size = new Size(710, 248);
            tpWriteZT1.TabIndex = 3;
            tpWriteZT1.Text = "Saving as ZT1 Graphic";
            tpWriteZT1.UseVisualStyleBackColor = true;
            // 
            // ChkExportZT1_Ani
            // 
            ChkExportZT1_Ani.AutoSize = true;
            ChkExportZT1_Ani.Checked = true;
            ChkExportZT1_Ani.CheckState = CheckState.Checked;
            ChkExportZT1_Ani.Location = new Point(24, 38);
            ChkExportZT1_Ani.Name = "ChkExportZT1_Ani";
            ChkExportZT1_Ani.Size = new Size(142, 17);
            ChkExportZT1_Ani.TabIndex = 29;
            ChkExportZT1_Ani.Text = "Create/update .ani-file";
            ChkExportZT1_Ani.UseVisualStyleBackColor = true;
            // 
            // ChkExportZT1_AddZTAFBytes
            // 
            ChkExportZT1_AddZTAFBytes.AutoSize = true;
            ChkExportZT1_AddZTAFBytes.Location = new Point(24, 15);
            ChkExportZT1_AddZTAFBytes.Name = "ChkExportZT1_AddZTAFBytes";
            ChkExportZT1_AddZTAFBytes.Size = new Size(178, 17);
            ChkExportZT1_AddZTAFBytes.TabIndex = 28;
            ChkExportZT1_AddZTAFBytes.Text = "Add \"ZT Animation File\"-bytes";
            ChkExportZT1_AddZTAFBytes.UseVisualStyleBackColor = true;
            // 
            // tpRenderingFrames
            // 
            tpRenderingFrames.Controls.Add(ChkPNGRenderBGFrame);
            tpRenderingFrames.Controls.Add(Label3);
            tpRenderingFrames.Controls.Add(numFrameDefaultAnimSpeed);
            tpRenderingFrames.Controls.Add(LblDefaultAnimSpeed);
            tpRenderingFrames.Controls.Add(ChkRenderFrame_BGGraphic);
            tpRenderingFrames.Location = new Point(4, 22);
            tpRenderingFrames.Name = "tpRenderingFrames";
            tpRenderingFrames.Padding = new Padding(3);
            tpRenderingFrames.Size = new Size(710, 248);
            tpRenderingFrames.TabIndex = 2;
            tpRenderingFrames.Text = "Rendering frames";
            tpRenderingFrames.UseVisualStyleBackColor = true;
            // 
            // ChkPNGRenderBGFrame
            // 
            ChkPNGRenderBGFrame.AutoSize = true;
            ChkPNGRenderBGFrame.Location = new Point(24, 102);
            ChkPNGRenderBGFrame.Name = "ChkPNGRenderBGFrame";
            ChkPNGRenderBGFrame.Size = new Size(485, 17);
            ChkPNGRenderBGFrame.TabIndex = 38;
            ChkPNGRenderBGFrame.Text = "Render last frame as background in all frames (and do not export background separ" + "ately)";
            ChkPNGRenderBGFrame.UseVisualStyleBackColor = true;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(24, 23);
            Label3.Name = "Label3";
            Label3.Size = new Size(558, 13);
            Label3.TabIndex = 37;
            Label3.Text = "These settings are applied in both the UI (main window) and when exporting graphi" + "cs to different formats.";
            // 
            // numFrameDefaultAnimSpeed
            // 
            numFrameDefaultAnimSpeed.Location = new Point(24, 153);
            numFrameDefaultAnimSpeed.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numFrameDefaultAnimSpeed.Name = "numFrameDefaultAnimSpeed";
            numFrameDefaultAnimSpeed.Size = new Size(120, 22);
            numFrameDefaultAnimSpeed.TabIndex = 36;
            // 
            // LblDefaultAnimSpeed
            // 
            LblDefaultAnimSpeed.AutoSize = true;
            LblDefaultAnimSpeed.Location = new Point(21, 137);
            LblDefaultAnimSpeed.Name = "LblDefaultAnimSpeed";
            LblDefaultAnimSpeed.Size = new Size(137, 13);
            LblDefaultAnimSpeed.TabIndex = 34;
            LblDefaultAnimSpeed.Text = "Default animation speed:";
            // 
            // ChkRenderFrame_BGGraphic
            // 
            ChkRenderFrame_BGGraphic.AutoSize = true;
            ChkRenderFrame_BGGraphic.Checked = true;
            ChkRenderFrame_BGGraphic.CheckState = CheckState.Checked;
            ChkRenderFrame_BGGraphic.Location = new Point(24, 79);
            ChkRenderFrame_BGGraphic.Name = "ChkRenderFrame_BGGraphic";
            ChkRenderFrame_BGGraphic.Size = new Size(234, 17);
            ChkRenderFrame_BGGraphic.TabIndex = 17;
            ChkRenderFrame_BGGraphic.Text = "Use selected ZT1 Graphic as background";
            ChkRenderFrame_BGGraphic.UseVisualStyleBackColor = true;
            // 
            // tpConversions
            // 
            tpConversions.Controls.Add(TxtConvert_fileNameDelimiter);
            tpConversions.Controls.Add(LblConvert_fileNameDelimiter);
            tpConversions.Controls.Add(ChkConvert_SharedColorPalette);
            tpConversions.Controls.Add(ChkConvert_Overwrite);
            tpConversions.Controls.Add(ChkConvert_DeleteOriginal);
            tpConversions.Controls.Add(LblExportPNG_Index);
            tpConversions.Controls.Add(NumConvert_PNGStartIndex);
            tpConversions.Location = new Point(4, 22);
            tpConversions.Name = "tpConversions";
            tpConversions.Padding = new Padding(3);
            tpConversions.Size = new Size(710, 248);
            tpConversions.TabIndex = 0;
            tpConversions.Text = "Batch convert ZT1 <> PNG";
            tpConversions.UseVisualStyleBackColor = true;
            // 
            // TxtConvert_fileNameDelimiter
            // 
            TxtConvert_fileNameDelimiter.Location = new Point(21, 104);
            TxtConvert_fileNameDelimiter.Name = "TxtConvert_fileNameDelimiter";
            TxtConvert_fileNameDelimiter.Size = new Size(61, 22);
            TxtConvert_fileNameDelimiter.TabIndex = 33;
            // 
            // LblConvert_fileNameDelimiter
            // 
            LblConvert_fileNameDelimiter.AutoSize = true;
            LblConvert_fileNameDelimiter.Location = new Point(18, 88);
            LblConvert_fileNameDelimiter.Name = "LblConvert_fileNameDelimiter";
            LblConvert_fileNameDelimiter.Size = new Size(107, 13);
            LblConvert_fileNameDelimiter.TabIndex = 32;
            LblConvert_fileNameDelimiter.Text = "File name delimiter:";
            // 
            // ChkConvert_SharedColorPalette
            // 
            ChkConvert_SharedColorPalette.AutoSize = true;
            ChkConvert_SharedColorPalette.Checked = true;
            ChkConvert_SharedColorPalette.CheckState = CheckState.Checked;
            ChkConvert_SharedColorPalette.Location = new Point(21, 183);
            ChkConvert_SharedColorPalette.Name = "ChkConvert_SharedColorPalette";
            ChkConvert_SharedColorPalette.Size = new Size(341, 17);
            ChkConvert_SharedColorPalette.TabIndex = 30;
            ChkConvert_SharedColorPalette.Text = "Use shared color palette for each graphic's animations/views.";
            ChkConvert_SharedColorPalette.UseVisualStyleBackColor = true;
            // 
            // ChkConvert_Overwrite
            // 
            ChkConvert_Overwrite.AutoSize = true;
            ChkConvert_Overwrite.Checked = true;
            ChkConvert_Overwrite.CheckState = CheckState.Checked;
            ChkConvert_Overwrite.Enabled = false;
            ChkConvert_Overwrite.Location = new Point(21, 206);
            ChkConvert_Overwrite.Name = "ChkConvert_Overwrite";
            ChkConvert_Overwrite.Size = new Size(100, 17);
            ChkConvert_Overwrite.TabIndex = 29;
            ChkConvert_Overwrite.Text = "Overwrite files";
            ChkConvert_Overwrite.UseVisualStyleBackColor = true;
            ChkConvert_Overwrite.Visible = false;
            // 
            // ChkConvert_DeleteOriginal
            // 
            ChkConvert_DeleteOriginal.AutoSize = true;
            ChkConvert_DeleteOriginal.Checked = true;
            ChkConvert_DeleteOriginal.CheckState = CheckState.Checked;
            ChkConvert_DeleteOriginal.Location = new Point(21, 160);
            ChkConvert_DeleteOriginal.Name = "ChkConvert_DeleteOriginal";
            ChkConvert_DeleteOriginal.Size = new Size(224, 17);
            ChkConvert_DeleteOriginal.TabIndex = 28;
            ChkConvert_DeleteOriginal.Text = "Delete the source file after conversion.";
            ChkConvert_DeleteOriginal.UseVisualStyleBackColor = true;
            // 
            // LblExportPNG_Index
            // 
            LblExportPNG_Index.AutoSize = true;
            LblExportPNG_Index.Location = new Point(75, 33);
            LblExportPNG_Index.Name = "LblExportPNG_Index";
            LblExportPNG_Index.Size = new Size(354, 13);
            LblExportPNG_Index.TabIndex = 27;
            LblExportPNG_Index.Text = "Start numbering of .PNG-file series at either 0 (index) or 1 (frame #1)";
            // 
            // NumConvert_PNGStartIndex
            // 
            NumConvert_PNGStartIndex.Location = new Point(21, 31);
            NumConvert_PNGStartIndex.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            NumConvert_PNGStartIndex.Name = "NumConvert_PNGStartIndex";
            NumConvert_PNGStartIndex.Size = new Size(48, 22);
            NumConvert_PNGStartIndex.TabIndex = 26;
            // 
            // tpFolders
            // 
            tpFolders.Controls.Add(BtnBrowsePal16);
            tpFolders.Controls.Add(BtnBrowsePal8);
            tpFolders.Controls.Add(BtnBrowse);
            tpFolders.Controls.Add(txtFolderPal16);
            tpFolders.Controls.Add(txtFolderPal8);
            tpFolders.Controls.Add(txtRootFolder);
            tpFolders.Controls.Add(LblColorPal16);
            tpFolders.Controls.Add(LblColorPal8);
            tpFolders.Controls.Add(LblRootFolder);
            tpFolders.Location = new Point(4, 22);
            tpFolders.Name = "tpFolders";
            tpFolders.Padding = new Padding(3);
            tpFolders.Size = new Size(710, 248);
            tpFolders.TabIndex = 1;
            tpFolders.Text = "Folders";
            tpFolders.UseVisualStyleBackColor = true;
            // 
            // BtnBrowsePal16
            // 
            BtnBrowsePal16.Location = new Point(539, 152);
            BtnBrowsePal16.Name = "BtnBrowsePal16";
            BtnBrowsePal16.Size = new Size(78, 20);
            BtnBrowsePal16.TabIndex = 31;
            BtnBrowsePal16.Text = "Browse...";
            BtnBrowsePal16.UseVisualStyleBackColor = true;
            BtnBrowsePal16.Visible = false;
            // 
            // BtnBrowsePal8
            // 
            BtnBrowsePal8.Location = new Point(541, 104);
            BtnBrowsePal8.Name = "BtnBrowsePal8";
            BtnBrowsePal8.Size = new Size(78, 20);
            BtnBrowsePal8.TabIndex = 30;
            BtnBrowsePal8.Text = "Browse...";
            BtnBrowsePal8.UseVisualStyleBackColor = true;
            BtnBrowsePal8.Visible = false;
            // 
            // BtnBrowse
            // 
            BtnBrowse.Location = new Point(539, 30);
            BtnBrowse.Name = "BtnBrowse";
            BtnBrowse.Size = new Size(78, 20);
            BtnBrowse.TabIndex = 29;
            BtnBrowse.Text = "Browse...";
            BtnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtFolderPal16
            // 
            txtFolderPal16.Enabled = false;
            txtFolderPal16.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            txtFolderPal16.Location = new Point(27, 153);
            txtFolderPal16.Name = "txtFolderPal16";
            txtFolderPal16.Size = new Size(506, 20);
            txtFolderPal16.TabIndex = 28;
            txtFolderPal16.Visible = false;
            // 
            // txtFolderPal8
            // 
            txtFolderPal8.Enabled = false;
            txtFolderPal8.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            txtFolderPal8.Location = new Point(27, 105);
            txtFolderPal8.Name = "txtFolderPal8";
            txtFolderPal8.Size = new Size(506, 20);
            txtFolderPal8.TabIndex = 26;
            txtFolderPal8.Visible = false;
            // 
            // txtRootFolder
            // 
            txtRootFolder.Enabled = false;
            txtRootFolder.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            txtRootFolder.Location = new Point(27, 31);
            txtRootFolder.Name = "txtRootFolder";
            txtRootFolder.Size = new Size(506, 20);
            txtRootFolder.TabIndex = 24;
            // 
            // LblColorPal16
            // 
            LblColorPal16.AutoSize = true;
            LblColorPal16.Font = new Font("Segoe UI", 8.25f);
            LblColorPal16.Location = new Point(24, 137);
            LblColorPal16.Name = "LblColorPal16";
            LblColorPal16.Size = new Size(158, 13);
            LblColorPal16.TabIndex = 27;
            LblColorPal16.Text = "Folder with 16-color palettes:";
            LblColorPal16.Visible = false;
            // 
            // LblColorPal8
            // 
            LblColorPal8.AutoSize = true;
            LblColorPal8.Font = new Font("Segoe UI", 8.25f);
            LblColorPal8.Location = new Point(24, 88);
            LblColorPal8.Name = "LblColorPal8";
            LblColorPal8.Size = new Size(152, 13);
            LblColorPal8.TabIndex = 25;
            LblColorPal8.Text = "Folder with 8-color palettes:";
            LblColorPal8.Visible = false;
            // 
            // LblRootFolder
            // 
            LblRootFolder.AutoSize = true;
            LblRootFolder.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            LblRootFolder.Location = new Point(24, 15);
            LblRootFolder.Name = "LblRootFolder";
            LblRootFolder.Size = new Size(72, 13);
            LblRootFolder.TabIndex = 23;
            LblRootFolder.Text = "Root folder: ";
            // 
            // TCSettings
            // 
            TCSettings.Controls.Add(tpFolders);
            TCSettings.Controls.Add(tpRenderingFrames);
            TCSettings.Controls.Add(tpConversions);
            TCSettings.Controls.Add(tpWriteZT1);
            TCSettings.Controls.Add(tpWritePNG);
            TCSettings.Controls.Add(tpPalette);
            TCSettings.Dock = DockStyle.Top;
            TCSettings.Location = new Point(0, 0);
            TCSettings.Name = "TCSettings";
            TCSettings.SelectedIndex = 0;
            TCSettings.Size = new Size(718, 274);
            TCSettings.TabIndex = 29;
            // 
            // LblHelp
            // 
            LblHelp.AutoSize = true;
            LblHelp.Location = new Point(12, 307);
            LblHelp.Name = "LblHelp";
            LblHelp.Size = new Size(205, 13);
            LblHelp.TabIndex = 30;
            LblHelp.Text = "Move over options to see related help.";
            // 
            // LblHelpTopic
            // 
            LblHelpTopic.AutoSize = true;
            LblHelpTopic.Font = new Font("Segoe UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            LblHelpTopic.Location = new Point(12, 294);
            LblHelpTopic.Name = "LblHelpTopic";
            LblHelpTopic.Size = new Size(66, 13);
            LblHelpTopic.TabIndex = 31;
            LblHelpTopic.Text = "Need help?";
            // 
            // FrmSettings
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(718, 416);
            Controls.Add(LblHelpTopic);
            Controls.Add(LblHelp);
            Controls.Add(TCSettings);
            Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmSettings";
            Text = "Settings";
            tpPalette.ResumeLayout(false);
            tpPalette.PerformLayout();
            tpWritePNG.ResumeLayout(false);
            tpWritePNG.PerformLayout();
            tpWriteZT1.ResumeLayout(false);
            tpWriteZT1.PerformLayout();
            tpRenderingFrames.ResumeLayout(false);
            tpRenderingFrames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFrameDefaultAnimSpeed).EndInit();
            tpConversions.ResumeLayout(false);
            tpConversions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumConvert_PNGStartIndex).EndInit();
            tpFolders.ResumeLayout(false);
            tpFolders.PerformLayout();
            TCSettings.ResumeLayout(false);
            FormClosing += new FormClosingEventHandler(FrmSettings_FormClosing);
            Load += new EventHandler(FrmSettings_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        internal FolderBrowserDialog dlgBrowseFolder;
        internal TabPage tpPalette;
        internal CheckBox ChkPalImportPNGForceAddAll;
        internal TabPage tpWritePNG;
        internal CheckBox ChkPNGTransparentBG;
        internal Label LblHowToExportPNG;
        internal ComboBox CboPNGExport_Crop;
        internal TabPage tpWriteZT1;
        internal CheckBox ChkExportZT1_Ani;
        internal CheckBox ChkExportZT1_AddZTAFBytes;
        internal TabPage tpRenderingFrames;
        internal NumericUpDown numFrameDefaultAnimSpeed;
        internal Label LblDefaultAnimSpeed;
        internal CheckBox ChkRenderFrame_BGGraphic;
        internal TabPage tpConversions;
        internal TextBox TxtConvert_fileNameDelimiter;
        internal Label LblConvert_fileNameDelimiter;
        internal CheckBox ChkConvert_SharedColorPalette;
        internal CheckBox ChkConvert_Overwrite;
        internal CheckBox ChkConvert_DeleteOriginal;
        internal Label LblExportPNG_Index;
        internal NumericUpDown NumConvert_PNGStartIndex;
        internal TabPage tpFolders;
        internal Button BtnBrowsePal16;
        internal Button BtnBrowsePal8;
        internal Button BtnBrowse;
        internal TextBox txtFolderPal16;
        internal TextBox txtFolderPal8;
        internal TextBox txtRootFolder;
        internal Label LblColorPal16;
        internal Label LblColorPal8;
        internal Label LblRootFolder;
        internal TabControl TCSettings;
        internal Label Label3;
        internal Label LblHelp;
        internal Label LblHelpTopic;
        internal CheckBox ChkPNGRenderBGFrame;
    }
}