using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{

    /// <summary>
/// Form which allows the user to change some settings.
/// </summary>
    public partial class FrmSettings
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        /// <summary>
    /// On unload form, save config.
    /// </summary>
    /// <param name="sender">ObjectObjectObjectObjectObject</param>
    /// <param name="e">FormClosingEventArgs</param>
        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {

            // Just re-load the settings here to apply them.
            MdlConfig.Write();

            // 20191005 Not recalling why reloading is necessary? Likely redundant.
            MdlConfig.Load();
        }

        /// <summary>
    /// On load form, initialize some controls to display the current configuration.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            Icon = My.MyProject.Forms.FrmMain.Icon;

            // Dynamically sets size.
            {
                var withBlock = CboPNGExport_Crop.Items;
                withBlock.Clear();
                withBlock.Add("Keep canvas size (" + MdlSettings.Cfg_Grid_NumPixels * 2 + " x " + MdlSettings.Cfg_Grid_NumPixels * 2 + ")");
                withBlock.Add("Crop to largest relevant width / height in this graphic");
                withBlock.Add("Crop to relevant pixels of this frame");
                withBlock.Add("Crop around center (fast but experimental)");
            }

            // Paths
            txtRootFolder.Text = MdlSettings.Cfg_Path_Root;
            txtFolderPal8.Text = MdlSettings.Cfg_Path_ColorPals8;
            txtFolderPal16.Text = MdlSettings.Cfg_Path_ColorPals16;

            // Export stuff (to PNG)
            ChkRenderFrame_BGGraphic.Checked = Conversions.ToBoolean(MdlSettings.Cfg_Export_PNG_RenderBGZT1);
            ChkPNGRenderBGFrame.Checked = MdlSettings.Cfg_Export_PNG_RenderBGFrame == 1;
            CboPNGExport_Crop.SelectedIndex = MdlSettings.Cfg_Export_PNG_CanvasSize;

            // Export to ZT1
            ChkExportZT1_Ani.Checked = MdlSettings.Cfg_Export_ZT1_Ani == 1;
            ChkExportZT1_AddZTAFBytes.Checked = MdlSettings.Cfg_Export_ZT1_AlwaysAddZTAFBytes == 1;

            // Conversion
            ChkConvert_DeleteOriginal.Checked = MdlSettings.Cfg_Convert_DeleteOriginal == 1;
            ChkConvert_SharedColorPalette.Checked = MdlSettings.Cfg_Convert_SharedPalette == 1;
            ChkConvert_Overwrite.Checked = MdlSettings.Cfg_Convert_Overwrite == 1;
            NumConvert_PNGStartIndex.Value = MdlSettings.Cfg_Convert_StartIndex;

            // exportOptions PNG
            ChkPNGTransparentBG.Checked = MdlSettings.Cfg_Export_PNG_TransparentBG == 1;
            ChkPNGRenderBGFrame.Checked = MdlSettings.Cfg_Export_PNG_RenderBGFrame == 1;

            // Graphic
            numFrameDefaultAnimSpeed.Value = MdlSettings.Cfg_Frame_DefaultAnimSpeed;

            // Palette
            ChkPalImportPNGForceAddAll.Checked = MdlSettings.Cfg_Palette_Import_PNG_Force_Add_Colors == 1;
        }

        /// <summary>
    /// Button click triggers select folder dialog, so user can select root project folder
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            {
                var withBlock = dlgBrowseFolder;
                withBlock.SelectedPath = txtRootFolder.Text;
                withBlock.ShowNewFolderButton = true;
                withBlock.Description = "Select the root folder which contains a ZT1-folder structure where graphics will come." + Constants.vbCrLf + "You are looking for something like this:" + Constants.vbCrLf + Constants.vbCrLf + @"[root folder]\animals\dolphin\..." + Constants.vbCrLf + @"[root folder]\objects\bamboo\...";
                withBlock.ShowDialog();
                txtRootFolder.Text = withBlock.SelectedPath;
                MdlSettings.Cfg_Path_Root = withBlock.SelectedPath;

                // Update Explorer pane on main window
                MdlZTStudioUI.UpdateExplorerPane();
            }
        }

        /// <summary>
    /// Button click triggers select folder dialog, so user can select a folder which contains color palettes with 8 (+1) colors in them.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnBrowsePal8_Click(object sender, EventArgs e)
        {
            {
                var withBlock = dlgBrowseFolder;
                withBlock.SelectedPath = txtFolderPal8.Text;
                withBlock.ShowNewFolderButton = true;
                withBlock.Description = "Select a folder which contains ZT1 Color Palettes (.pal)," + Constants.vbCrLf + "with 8 colors: (usually the filenames are blue8.pal etc)";
                withBlock.ShowDialog();
                txtFolderPal8.Text = withBlock.SelectedPath;
                MdlSettings.IniWrite(System.IO.Path.GetFullPath(Application.StartupPath) + @"\settings.ini", "paths", "pal8", withBlock.SelectedPath);
            }
        }

        /// <summary>
    /// Button click triggers select folder dialog, so user can select a folder which contains color palettes with 16 (+1) colors in them.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnBrowsePal16_Click(object sender, EventArgs e)
        {
            {
                var withBlock = dlgBrowseFolder;
                withBlock.SelectedPath = txtFolderPal16.Text;
                withBlock.ShowNewFolderButton = true;
                withBlock.Description = "Select a folder which contains ZT1 Color Palettes (.pal)," + Constants.vbCrLf + "with 16 colors: (usually the filenames are blue16.pal etc)";
                withBlock.ShowDialog();
                txtFolderPal16.Text = withBlock.SelectedPath;
                MdlSettings.IniWrite(System.IO.Path.GetFullPath(Application.StartupPath) + @"\settings.ini", "paths", "pal16", withBlock.SelectedPath);
            }
        }

        /// <summary>
    /// Handles selection of different PNG Export method
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void CboPNGExport_Crop_SelectedValueChanged(object sender, EventArgs e)
        {

            // 0 = normal
            // 1 = cropped to relevant pixels of the largest frame
            // 2 = cropped to relevant pixels of this frame
            // 3 = cropped around center (experimental)
            if (CboPNGExport_Crop.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Export_PNG_CanvasSize = CboPNGExport_Crop.SelectedIndex;
        }

        /// <summary>
    /// Handles toggling of whether there's a ZT1 Graphic selected which needs to be rendered as background
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkExportPNG_BGGraphic_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkRenderFrame_BGGraphic.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Export_PNG_RenderBGZT1 = (byte)(Conversions.ToInteger(ChkRenderFrame_BGGraphic.Checked) * -1);

            // Update preview in main window instantly
            MdlZTStudioUI.UpdatePreview(false, true);
        }

        /// <summary>
    /// Handles changes of the index numbering for PNG files
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void NumExportPNG_StartIndex_ValueChanged(object sender, EventArgs e)
        {
            if (NumConvert_PNGStartIndex.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Convert_StartIndex = (int)Math.Round(NumConvert_PNGStartIndex.Value);
            Debug.Print("Value changed");
        }



        /// <summary>
    /// Handles toggling of whether ZTAF bytes should always be added to the beginning of a ZT1 Graphic file on creation
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkConvert_AddZTAFBytes_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkExportZT1_AddZTAFBytes.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Export_ZT1_AlwaysAddZTAFBytes = (byte)(Conversions.ToInteger(ChkExportZT1_AddZTAFBytes.Checked) * -1);
        }

        /// <summary>
    /// Handles toggling of whether the source files should automatically be deleted after a conversion from one format to another (ZT1 - PNG)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkConvert_DeleteOriginal_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConvert_DeleteOriginal.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Convert_DeleteOriginal = (byte)(Conversions.ToInteger(ChkConvert_DeleteOriginal.Checked) * -1);
        }

        /// <summary>
    /// Handles toggling of whether files can automatically be overwritten
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkConvert_Overwrite_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConvert_Overwrite.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Convert_Overwrite = (byte)(Conversions.ToInteger(ChkConvert_Overwrite.Checked) * -1);
        }


        /// <summary>
    /// Handles toggling of whether an .ani-file should be generated
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkExportZT1_Ani_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkExportZT1_Ani.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Export_ZT1_Ani = (byte)(Conversions.ToInteger(ChkExportZT1_Ani.Checked) * -1);
        }


        /// <summary>
    /// Handles toggling of whether ZT Studio should try to use a shared color palette, if present, when creating ZT1 Graphics
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkConvert_SharedColorPalette_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConvert_SharedColorPalette.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Convert_SharedPalette = (byte)(Conversions.ToInteger(ChkConvert_SharedColorPalette.Checked) * -1);
        }

        /// <summary>
    /// Handles changes in the file name delimiter
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TxtConvert_fileNameDelimiter_TextChanged(object sender, EventArgs e)
        {
            MdlSettings.Cfg_Convert_FileNameDelimiter = TxtConvert_fileNameDelimiter.Text;
        }


        /// <summary>
    /// Handles toggling of whether PNGs should be exported with a transparent background or the chosen background color in ZT Studio
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkPNGTransparentBG_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPNGTransparentBG.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Export_PNG_TransparentBG = (byte)(Conversions.ToInteger(ChkPNGTransparentBG.Checked) * -1);
        }

        /// <summary>
    /// Handles toggling of whether PNGs should be exported with the last ("extra") frame as background
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkPNGRenderBGFrame_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPNGRenderBGFrame.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Export_PNG_RenderBGFrame = (byte)(Conversions.ToInteger(ChkPNGRenderBGFrame.Checked) * -1);

            // Update preview in main window instantly
            MdlZTStudioUI.UpdatePreview(true, true);
        }


        /// <summary>
    /// Handles changes in the default animation speed for new graphics
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void NumFrameAnimSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (numFrameDefaultAnimSpeed.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Frame_DefaultAnimSpeed = (int)Math.Round(numFrameDefaultAnimSpeed.Value);
        }


        /// <summary>
    /// Handles toggling of whether colors should always be added (rather than only when unique) on importing PNG files
    /// </summary>
    /// <remarks>After recolors, the palette may contain identical colors. However, hex indexes in the frame may not be updated yet!</remarks>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkPalImportPNGForceAddAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPalImportPNGForceAddAll.IsHandleCreated == false)
            {
                return;
            }

            MdlSettings.Cfg_Palette_Import_PNG_Force_Add_Colors = (byte)(Conversions.ToInteger(ChkPalImportPNGForceAddAll.Checked) * -1);
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void LblRootFolder_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(LblRootFolder, "This is the project folder. Common subfolders are 'animals', 'objects', ...");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void LblColorPal8_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(LblRootFolder, "The folder containing .pal-files which consist of 8 colors.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void LblColorPal16_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(LblRootFolder, "The folder containing .pal-files which consist of 16 colors.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkRenderFrame_BGGraphic_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkRenderFrame_BGGraphic, "" + "Allows to see two graphics combined. " + Constants.vbCrLf + "User can load the Orang Utan's swinging animation and use the Rope Swing toy as background." + Constants.vbCrLf + "Hint: using this technique, Blue Fang could use 2 color palettes = 2x 255 colors for this animation!");
        }



        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkConvert_DeleteOriginal_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkConvert_DeleteOriginal, "If enabled, the source files of any conversion will be deleted." + Constants.vbCrLf + "When converting from PNG to ZT1 Graphics, the PNG files will be deleted." + Constants.vbCrLf + "When converting from ZT1 Graphics to PNG, the ZT1 Graphics will be deleted.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void LblConvert_fileNameDelimiter_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(LblConvert_fileNameDelimiter, "The character used in filenames, between the name of the graphic And the frame." + Constants.vbCrLf + "For example, _ Is the delimiter in NE_0000.png ");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkConvert_SharedColorPalette_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkConvert_SharedColorPalette, "Rather than creating a separate color palette (.pal) for each view of each animation," + Constants.vbCrLf + "this feature checks if there's a shared palette (.pal, .gpl or .png - in this order) provided by the user." + Constants.vbCrLf + "Palette names should be the same as the folder they're in: for example 'm.pal' in 'animals/redpanda/m'" + Constants.vbCrLf + "This feature respects folder hierarchy: it uses the palette it can find at the closest level to the graphic. " + Constants.vbCrLf + "Warning: a color palette provides maximum 255 visible colors among all frames!");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkConvert_Overwrite_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkConvert_Overwrite, "Overwrites any existing files during conversions.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkExportZT1_AddZTAFBytes_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkExportZT1_AddZTAFBytes, "Always adds 'ZTAF' bytes at beginning of graphic file." + Constants.vbCrLf + "ZTAF-bytes are usually found at the beginning of graphic files which contain a background frame." + Constants.vbCrLf + "They don't seem to have any real function.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkExportZT1_Ani_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkExportZT1_Ani, "Tries to generate a .ani file containing information related to offsets." + Constants.vbCrLf + "It will do so based on the most commonly found filenames and try to determine the object type." + Constants.vbCrLf + "It should work for most graphics, although there are exceptions (such as dustcloud)" + Constants.vbCrLf + "Finds 'N': assumes icon" + Constants.vbCrLf + "Finds 'NE', 'SE', 'SW', 'NW': assumes object (foliage, scenery, building, ...)" + Constants.vbCrLf + "Finds 'N', 'NE', 'E', 'SE', 'S': assumes moving creature (animal, guest, staff, ...)" + Constants.vbCrLf + "Finds '1' , '2', ... , '20': assumes path");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkPNGTransparentBG_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkPNGTransparentBG, "Rather than exporting PNGs with the chosen background color in ZT Studio," + Constants.vbCrLf + "PNG files will be created with a transparent (invisible) background.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkPNGRenderBGFrame_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkPNGRenderBGFrame, "Some images (such as the Restaurant) have one frame which serves as background in all other frames." + Constants.vbCrLf + "Toggling this option will either render this background in each frame and NOT export the background frame;" + Constants.vbCrLf + "or it will NOT render the background and export this background frame as 'extra'");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void LblHowToExportPNG_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(LblHowToExportPNG, "Each of these methods has benefits and downsides." + Constants.vbCrLf + "Keep canvas size (" + MdlSettings.Cfg_Grid_NumPixels * 2 + " x " + MdlSettings.Cfg_Grid_NumPixels * 2 + "): slower export; keeps offsets on re-import; easy to animate in other programs" + Constants.vbCrLf + "Crop to largest relevant width / height in this graphic: faster export; offsets lost on re-import; easy to animate in other programs" + Constants.vbCrLf + "Crop to relevant pixels of this frame: fastest export; offsets lost on re-import; more difficult to animate in other programs" + Constants.vbCrLf + "Crop around center (fast but experimental) : fast export; keeps offsets on re-import; easy to animate in other programs");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void ChkPalImportPNGForceAddAll_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(ChkPalImportPNGForceAddAll, "Sometimes there may be duplicates in the color palette." + Constants.vbCrLf + "Usually only unique colors are added." + Constants.vbCrLf + "In some cases (such as recolors), it may be desired to forcefully add them to the color palette anyway.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void LblExportPNG_Index_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(LblExportPNG_Index, "This is meant for batch conversions. Some programs start numbering the first frame with 0, others with 1.");
        }

        /// <summary>
    /// Show help on mousehover
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void LblDefaultAnimSpeed_MouseHover(object sender, EventArgs e)
        {
            MdlZTStudioUI.ShowToolTip(LblDefaultAnimSpeed, "The animation speed (in milliseconds) determines the interval before the next frame is shown.");
        }
    }
}