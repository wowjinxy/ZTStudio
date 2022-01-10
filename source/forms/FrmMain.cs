using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{

    /// <summary>
/// Main user interface
/// </summary>
    public partial class FrmMain
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
    /// Sets some info on loading
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void Form1_Load(object sender, EventArgs e)
        {

            // Done to increase performance of adding colors to this palette.
            DgvPaletteMain.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, DgvPaletteMain, new object[] { true });

            // Always start with a new ZT1 Graphic with one frame
            MdlSettings.EditorFrame = new ClsFrame(MdlSettings.EditorGraphic);
            MdlSettings.EditorGraphic.Frames.Add(MdlSettings.EditorFrame);

            // Starting with an empty canvas
            MdlSettings.BMEmpty = new ClsDirectBitmap(MdlSettings.Cfg_Grid_NumPixels * 2, MdlSettings.Cfg_Grid_NumPixels * 2);
            {
                var withBlock = PicBox;
                withBlock.Width = MdlSettings.Cfg_Grid_NumPixels * 2;
                withBlock.Height = MdlSettings.Cfg_Grid_NumPixels * 2;
            }

            // Background color derived from settings (based on previously configured settings)
            PicBox.BackColor = MdlSettings.Cfg_Grid_BackGroundColor;

            // Set grid size (based on previously configured settings)
            TsbFrame_fpX.Text = MdlSettings.Cfg_Grid_FootPrintX.ToString();
            TsbFrame_fpY.Text = MdlSettings.Cfg_Grid_FootPrintY.ToString();
            TsbFrame_OffsetAll.Checked = Conversions.ToBoolean(MdlSettings.Cfg_Editor_RotFix_IndividualFrame * -1);

            // ZT1 Default color palettes
            // strPathBuildingColorPals

            FileInfo[] LstColorpalettes;
            LstColorpalettes = new DirectoryInfo(MdlSettings.Cfg_Path_ColorPals8).GetFiles();
            FileInfo ObjFileInfo;

            // List all files found in the directory with 8-color palettes
            foreach (var currentObjFileInfo in LstColorpalettes)
            {
                ObjFileInfo = currentObjFileInfo;
                TsbOpenPalBldg8.DropDownItems.Add(ObjFileInfo.Name);
            }

            LstColorpalettes = new DirectoryInfo(MdlSettings.Cfg_Path_ColorPals16).GetFiles();

            // List all files found in the directory with 16-color palettes
            foreach (var currentObjFileInfo1 in LstColorpalettes)
            {
                ObjFileInfo = currentObjFileInfo1;
                TsbOpenPalBldg16.DropDownItems.Add(ObjFileInfo.Name);
            }

            // Update Explorer Panel to show folder structure of root folder
            MdlZTStudioUI.UpdateExplorerPane();

            // If exists, load ZT1 Graphic. Won't decrease performance a lot and might be helpful while working on a project
            if (File.Exists(MdlSettings.Cfg_Path_RecentZT1) == true)
            {
                MdlZTStudioUI.LoadGraphic(MdlSettings.Cfg_Path_RecentZT1);
            }

            // Make sure everything is finished. Needed?
            Application.DoEvents();
        }

        /// <summary>
    /// Handles mouse movements. Shows
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 3801


        Input:

                On Error GoTo dBug

         */
        1:
            ;

            // Canvas is still entirely empty
            if (Information.IsNothing(PicBox.Image) == true)
            {
                MdlZTStudio.Trace(GetType().FullName, "MouseMove", "Picture box empty");
                return;
            }

        2:
            ;

            // Frame might have been just initiated
            if (Information.IsNothing(MdlSettings.EditorFrame.CoreImageBitmap) & Information.IsNothing(MdlSettings.EditorFrame.CoreImageHex))
            {
                MdlZTStudio.Trace(GetType().FullName, "MouseMove", "EditorFrame has no CoreImageBitmap or CoreImageHex");
                return;
            }

        3:
            ;

            // This is a bit of a dilemma. 
            // If using PicBox.image, it also shows the grid color on hovering. Ignoring the grid color is a bad idea since the color may be present in the image too.
            // If just using editorFrame.GetImage(), it won't show colors of any background graphic (toy for Orang Utan).
            // Todo: combining them makes more sense but is more intensive. Should be cached somewhere.
            // Images_Combine(editorFrame.GetImage(), editorBgGraphic.getimage())
            Bitmap BmTmp;
            BmTmp = (Bitmap)PicBox.Image; // Used to be EditorFrame.GetImage()
        20:
            ;

            // Find out which pixel area matters within the PicBox
            // Offset compared to PicBox Left/Top
            // Keep in mind: BmTmp is NOT necessarily the entire PicBox (adjusts to window)
            int IntOffsetX = (int)Math.Round((PicBox.Width - BmTmp.Width) / 2d); // Left = positive; right = negative
            int IntOffsetY = (int)Math.Round((PicBox.Height - BmTmp.Height) / 2d); // Top = positive; bottom = negative
        100:
            ;
            MdlZTStudio.Trace(GetType().FullName, "MouseMove", "e.X = " + e.X + ", Y = " + e.Y);
            MdlZTStudio.Trace(GetType().FullName, "MouseMove", "Offset X = " + IntOffsetX + ", Y = " + IntOffsetY);
            MdlZTStudio.Trace(GetType().FullName, "MouseMove", "Bmp width = " + BmTmp.Width + ", Height = " + BmTmp.Height);
            if (e.X > IntOffsetX & e.X < BmTmp.Width + IntOffsetX & e.Y > IntOffsetY & e.Y < BmTmp.Height + IntOffsetY)
            {
            101:
                ;

                // Image might be smaller, while PicBox appears larger due to background color.
                var ObjColor = BmTmp.GetPixel(e.X - IntOffsetX, e.Y - IntOffsetY);

                // Alpha channel is not set to 0 (transparent). This check is still from when using BmpTmp = EditorFrame.GetImage()
                // Display color info
                if (ObjColor.A != 0)
                {
                102:
                    ;
                    LblColor.BackColor = ObjColor;
                    LblColorDetails.Text = "" + "Coordinates: x: " + (e.X - IntOffsetX) + " , y: " + (e.Y - IntOffsetY) + Constants.vbCrLf + "RGB: " + ObjColor.R + "," + ObjColor.G + "," + ObjColor.B + Constants.vbCrLf + "Index in .pal file: # " + MdlSettings.EditorGraphic.ColorPalette.Colors.IndexOf(ObjColor) + Constants.vbCrLf + "VB.Net: " + ObjColor.ToArgb();
                }
                else
                {
                112:
                    ;

                    // Definitely just the background color.
                    LblColor.BackColor = PicBox.BackColor;
                    LblColorDetails.Text = Constants.vbNullString;
                }
            }
            else
            {
            122:
                ;
                LblColor.BackColor = PicBox.BackColor;
                LblColorDetails.Text = Constants.vbNullString;
            }

            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "PicBox_MouseMove", Information.Err(), true);
        }

        /// <summary>
    /// This is unfinished, but it was meant to be a zoom function.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void PicBox_MouseWheel(object sender, MouseEventArgs e)
        {
            Debug.Print("Picbox Wheel");
            return;

            // it should be the image, not the picbox!
            if (e.Delta != 0)
            {
                if (e.Delta <= 0)
                {
                    if (PicBox.Width < 500)
                    {
                        return; // minimum 500?
                    }
                }
                else if (PicBox.Width > 2000)
                {
                    return; // maximum 2000?
                }

                PicBox.Width += (int)Math.Round(PicBox.Width * e.Delta / 1000d);
                PicBox.Height += (int)Math.Round(PicBox.Height * e.Delta / 1000d);
            }
        }

        /// <summary>
    /// Handles value changes in frame slider control.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TTbFrames_ValueChanged(object sender, EventArgs e)
        {

            // Update canvas. 
            // UI: update frame info; don't update button states
            MdlZTStudioUI.UpdatePreview(true, false, TbFrames.Value - 1);
        }

        /// <summary>
    /// If "play animation" has been checked, the timer updates the preview. Timer interval = graphic animation speed.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TmrAnimation_Tick(object sender, EventArgs e)
        {
            MdlZTStudio.Trace("TmrAnimation", "Tick", "Interval = " + TmrAnimation.Interval.ToString());

            // Reset if maximum value has already been reached
            // Else, show next frame
            if (TbFrames.Value == TbFrames.Maximum)
            {
                TbFrames.Value = 1;
            }
            else
            {
                TbFrames.Value += 1;
            }

            // Update canvas.
            // UI: update frame info; don't update button states
            MdlZTStudioUI.UpdatePreview(true, false, TbFrames.Value - 1);
        }


        /// <summary>
    /// Handles toolbar button click to change background color
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbCanvasBG_Click(object sender, EventArgs e)
        {
            {
                var withBlock = DlgColor;
                withBlock.Color = MdlSettings.Cfg_Grid_BackGroundColor;
                withBlock.ShowDialog();

                // Remember this color
                MdlSettings.Cfg_Grid_BackGroundColor = withBlock.Color;
            }

            // Save this change
            MdlConfig.Write();

            // Update UI in preview
            MdlZTStudioUI.UpdatePreview(false, false);
        }

        /// <summary>
    /// Handles toolbar button click to open ZT1 Graphic
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbZT1Open_Click(object sender, EventArgs e)
        {
            MdlZTStudio.Trace(GetType().FullName, "TsbZT1Open_Click", "Open ZT1 file dialog.");
            MdlZTStudio.Trace(GetType().FullName, "TsbZT1Open_Click", "Last used file: " + MdlSettings.Cfg_Path_RecentZT1);

            // Show dialog to open a ZT1 Graphic
            {
                var withBlock = DlgOpen;
                withBlock.Title = "Pick a ZT1 Graphic";
                withBlock.DefaultExt = "";
                withBlock.Filter = "All files|*.*";

                // Default to path of last used graphic
                withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentZT1).Directory.FullName;

                // If that path doesn't exist: attempt fallback to default game locations on x86 and x64 systems
                if (string.IsNullOrEmpty(DlgOpen.InitialDirectory) | Directory.Exists(DlgOpen.InitialDirectory) == false)
                {
                    if (Directory.Exists(MdlSettings.Cfg_Path_Root))
                    {
                        MdlZTStudio.Trace(GetType().FullName, "TsbZT1Open_Click", "Open ZT1 file dialog. Fallback to root: " + MdlSettings.Cfg_Path_Root);
                        withBlock.InitialDirectory = MdlSettings.Cfg_Path_Root;
                    }
                    else if (Directory.Exists(@"C:\Program Files (x86)\Microsoft Games\Zoo Tycoon"))
                    {
                        MdlZTStudio.Trace(GetType().FullName, "TsbZT1Open_Click", "Open ZT1 file dialog. Fallback to Program Files (x86)");
                        withBlock.InitialDirectory = @"C:\Program Files (x86)\Microsoft Games\Zoo Tycoon";
                    }
                    else if (Directory.Exists(@"C:\Program Files\Microsoft Games\Zoo Tycoon"))
                    {
                        MdlZTStudio.Trace(GetType().FullName, "TsbZT1Open_Click", "Open ZT1 file dialog. Fallback to Program Files");
                        withBlock.InitialDirectory = @"C:\Program Files\Microsoft Games\Zoo Tycoon";
                    }
                }

                // User did NOT cancel
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {

                    // Load ZT1 Graphic
                    MdlZTStudioUI.LoadGraphic(DlgOpen.FileName);
                }
            }
        }

        /// <summary>
    /// Handles value change of trackbar (slider) and updates preview accordingly
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TbFrames_ValueChanged(object sender, EventArgs e)
        {

            // Update current editor frame value
            // Do this before anything else or offsets might appear incorrectly!
            MdlSettings.EditorFrame = MdlSettings.EditorGraphic.Frames[TbFrames.Value - 1];

            // Sometimes called because of buttons changing the TbFrames value (increase, decrease)
            // Update preview in UI
            // Frame info should also be updated at this point
            MdlZTStudioUI.UpdatePreview(true, true, TbFrames.Value - 1);
        }

        /// <summary>
    /// Handles toolbar button click to show about info (credits, version info)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbAbout_Click(object sender, EventArgs e)
        {
            string StrMessage = "About " + Application.ProductName + " " + Application.ProductVersion + Constants.vbCrLf + Strings.StrDup(50, "_") + Constants.vbCrLf + "© since 2015 by Jeffrey Bostoen" + Constants.vbCrLf + MdlSettings.Cfg_GitHub_URL + Constants.vbCrLf + Constants.vbCrLf + Constants.vbCrLf + "" + "Bugs? " + Constants.vbCrLf + Strings.StrDup(25, "-") + Constants.vbCrLf + "- You can report them at GitHub or Zoo Tek Phoenix. " + Constants.vbCrLf + "- Support not guaranteed. " + Constants.vbCrLf + "- Include the graphic files which are causing the problem. " + Constants.vbCrLf + Constants.vbCrLf + "" + "Credits? " + Constants.vbCrLf + Strings.StrDup(25, "-") + Constants.vbCrLf + "- Blue Fang for creating Zoo Tycoon 1 (and maybe the graphic format)." + Constants.vbCrLf + "- Microsoft for publishing the game." + Constants.vbCrLf + "- Rapan Studios for the animal designs." + Constants.vbCrLf + "- MadScientist and Jay for explaining the file format." + Constants.vbCrLf + "- Vondell for providing new PNG graphics to experiment with." + Constants.vbCrLf + "- HENDRIX for some contributions to the source code.";
            MdlZTStudio.InfoBox(GetType().FullName, "TsbAbout_Click", StrMessage);
        }

        /// <summary>
    /// Handles toolbar button click to export frame as PNG
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_ExportPNG_Click(object sender, EventArgs e)
        {

            // Show save dialog
            {
                var withBlock = DlgSave;
                withBlock.Title = "Save current frame as .PNG";
                withBlock.DefaultExt = ".png";
                withBlock.AddExtension = true;
                withBlock.Filter = "PNG files (*.png)|*.png|All files|*.*";

                // Suggest path based on most recently saved PNG
                withBlock.InitialDirectory = Path.GetDirectoryName(MdlSettings.Cfg_Path_RecentPNG);

                // User did not cancel? Then save.
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {

                    // Save current frame as PNG
                    MdlSettings.EditorFrame.SavePNG(DlgSave.FileName);

                    // Remember most recent PNG path
                    MdlSettings.Cfg_Path_RecentPNG = new FileInfo(DlgSave.FileName).Directory.FullName;
                    MdlConfig.Write();
                }
            }
        }


        /// <summary>
    /// Handles toolbar button click to open ZT1 Color palette (.pal file)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbZT1_OpenPal_Click(object sender, EventArgs e)
        {
            {
                var withBlock = DlgOpen;
                withBlock.Title = "Pick a ZT1 Color Palette";
                withBlock.DefaultExt = ".pal";
                withBlock.Filter = "ZT1 Color Palette files (*.pal)|*.pal|All files|*.*";

                // Set directory by default to where a ZT1 Graphic was last opened
                withBlock.InitialDirectory = Path.GetDirectoryName(MdlSettings.Cfg_Path_RecentZT1);

                // If the user didn't press cancel, load palette.
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {
                    MdlColorPalette.LoadPalette(DlgOpen.FileName);
                }
            }
        }

        /// <summary>
    /// Handles toolbar button click to modify Settings
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbSettings_Click(object sender, EventArgs e)
        {

            // Show Settings window
            My.MyProject.Forms.FrmSettings.ShowDialog();
        }

        /// <summary>
    /// Handles clicking a menu item in the list of 8-color palettes
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">ToolStripItemClickedEventArgs</param>
        private void TsbOpenPalBldg8_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            // Load color palette (in its own window)
            MdlColorPalette.LoadPalette(MdlSettings.Cfg_Path_ColorPals8 + @"\" + e.ClickedItem.Text);
        }

        /// <summary>
    /// Handles clicking a menu item in the list of 16-color palettes
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">TooLStripItemClickedEventArgs</param>
        private void TsbOpenPalBldg16_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            // Load color palette (in its own window)
            MdlColorPalette.LoadPalette(MdlSettings.Cfg_Path_ColorPals16 + @"\" + e.ClickedItem.Text);
        }

        /// <summary>
    /// Handles toolbar button click to start batch conversion (ZT1 Graphic - PNG)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbBatchConversion_Click(object sender, EventArgs e)
        {

            // Show window to start batch conversion
            My.MyProject.Forms.FrmBatchConversion.ShowDialog(this);
        }

        /// <summary>
    /// Handles toolbar button click to pick a ZT1 Graphic to use as background
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbPreview_BGGraphic_Click(object sender, EventArgs e)
        {

            // Show dialog
            {
                var withBlock = DlgOpen;
                withBlock.Title = "Pick a ZT1 Graphic";
                withBlock.DefaultExt = "";
                withBlock.Filter = "All files|*.*";
                if (Directory.Exists(new FileInfo(MdlSettings.Cfg_Path_RecentZT1).Directory.FullName) == false)
                {

                    // If this directory does not exist, try default game directory on x86 and x64 systems
                    if (Directory.Exists(@"C:\Program Files\Microsoft Games\Zoo Tycoon"))
                    {
                        withBlock.InitialDirectory = @"C:\Program Files\Microsoft Games\Zoo Tycoon";
                    }
                    else if (Directory.Exists(@"C:\Program Files (x86)\Microsoft Games\Zoo Tycoon"))
                    {
                        withBlock.InitialDirectory = @"C:\Program Files (x86)\Microsoft Games\Zoo Tycoon";
                    }
                }
                else
                {

                    // As for initial directory, use the one from the last picked ZT1 Graphic
                    withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentZT1).Directory.FullName;
                }

                // If user didn't cancel, load background graphic.
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {
                    if (File.Exists(DlgOpen.FileName) == true)
                    {
                        if (!string.IsNullOrEmpty(Path.GetExtension(DlgOpen.FileName)))
                        {

                            // The file path has an extension.
                            // So it's not a ZT1 Graphic
                            string StrMessage = "You selected a file with the extension '" + Path.GetExtension(DlgOpen.FileName) + "'." + Constants.vbCrLf + "With ZT1 graphic, we mean a ZT1 graphics file without extension.";
                            MdlZTStudio.HandledError(GetType().FullName, "TsbPreview_BGGraphic_Click", StrMessage, false);
                        }
                        else
                        {

                            // Add background graphic
                            MdlSettings.EditorBgGraphic.Read(DlgOpen.FileName);

                            // Update preview. No need to update frame info, button states (GUI)
                            MdlZTStudioUI.UpdatePreview(false, false);

                            // Remember this graphic as last selected
                            MdlSettings.Cfg_Path_RecentZT1 = DlgOpen.FileName;
                            MdlConfig.Write();
                        }
                    }
                    else
                    {

                        // File does not exist (for some reason)
                        MdlZTStudio.HandledError(GetType().FullName, "TsbPreview_BGGraphic_Click", "File does not exist.", false);
                    }
                }
            }
        }


        /// <summary>
    /// Handles toolbar button click to add a new empty frame. (or on right click: to immediately add PNG as new frame)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_Add_Click(object sender, EventArgs e)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 25068


        Input:

                On Error GoTo dBug

         */
        0:
            ;

            // New ClsFrame
            var ObjFrame = new ClsFrame(MdlSettings.EditorGraphic);
        2:
            ;
        10:
            ;

            // Add it to the list of frames (after the currently displayed one)
            MdlSettings.EditorGraphic.Frames.Insert(TbFrames.Value, ObjFrame);


            // Update preview. Update frame info and other GUI elements (button states, offsets, ...)
            MdlZTStudioUI.UpdatePreview(true, true, TbFrames.Value);
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "TsbFrame_Add_Click", Information.Err(), true);
        }


        /// <summary>
    /// Handles toolbar button click to delete an existing frame
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_Delete_Click(object sender, EventArgs e)
        {

            // Remove the frame
            MdlSettings.EditorGraphic.Frames.RemoveAt(TbFrames.Value - 1);

            // Update preview. Update frame info and other GUI elements (button states, offsets, ...)
            MdlZTStudioUI.UpdatePreview(true, true, TbFrames.Value - 1);
        }

        /// <summary>
    /// Handles toolbar button click to show the next frame
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_IndexIncrease_Click(object sender, EventArgs e)
        {

            // Change handled in slider control (will trigger GUI and frameinfo updates)
            TbFrames.Value += 1;
        }


        /// <summary>
    /// Handles toolbar button click to move the frame contents up
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void TsbFrame_OffsetUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(0, 16));
            }
            else
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(0, 1));
            }

            MdlZTStudioUI.UpdatePreview(true, false);
        }

        /// <summary>
    /// Handles toolbar button click to move the frame contents down
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void TsbFrame_OffsetDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(0, -16));
            }
            else
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(0, -1));
            }

            MdlZTStudioUI.UpdatePreview(true, false);
        }

        /// <summary>
    /// Handles toolbar button click to move the frame contents to the left
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void TsbFrame_OffsetLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(16, 0));
            }
            else
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(1, 0));
            }

            MdlZTStudioUI.UpdatePreview(true, false);
        }

        /// <summary>
    /// Handles toolbar button click to move the frame contents to the right
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void TsbFrame_OffsetRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(-16, 0));
            }
            else
            {
                MdlSettings.EditorFrame.UpdateOffsets(new Point(-1, 0));
            }

            MdlZTStudioUI.UpdatePreview(true, false);
        }

        /// <summary>
    /// Handles toolbar button click to indicate whether this graphic has an extra frame or not
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbGraphic_ExtraFrame_Click(object sender, EventArgs e)
        {
            MdlSettings.EditorGraphic.HasBackgroundFrame = (byte)Math.Abs(MdlSettings.EditorGraphic.HasBackgroundFrame - 1);

            // Quick fix: on change, revert to frame 1.
            int IntFrameNumber = MdlSettings.EditorGraphic.Frames.IndexOf(MdlSettings.EditorFrame);

            // Was displaying last frame?
            if (IntFrameNumber == MdlSettings.EditorGraphic.Frames.Count - 1)
            {
                // Show first one instead
                MdlZTStudioUI.UpdatePreview(true, true, 0);
            }
            else
            {
                // Update current frame
                MdlZTStudioUI.UpdatePreview(true, true);
            }
        }

        /// <summary>
    /// Handles toolbar button click to show the previous frame
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_IndexDecrease_Click(object sender, EventArgs e)
        {

            // Change handled in slider control (will trigger GUI and frameinfo updates)
            TbFrames.Value -= 1;
        }


        /// <summary>
    /// Handles toolbar button click to delete PNG files in the root folder
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbDelete_PNG_Click(object sender, EventArgs e)
        {

            // Clean up files is generic, hence the specific messagebox needs to be shown here
            MdlTasks.CleanUpFiles(MdlSettings.Cfg_Path_Root, ".png");
            MdlZTStudio.InfoBox("MdlTasks", "CleanUpFiles", "Deleted all .PNG files in the root folder.");
        }

        /// <summary>
    /// Handles toolbar button click to delete ZT1 Graphic files and color palettes in the root folder
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbDelete_ZT1Files_Click(object sender, EventArgs e)
        {

            // Cleanup ZT1 Graphics and color palettes
            MdlTasks.CleanUpFiles(MdlSettings.Cfg_Path_Root, "");
            MdlTasks.CleanUpFiles(MdlSettings.Cfg_Path_Root, ".pal");
            MdlTasks.CleanUpFiles(MdlSettings.Cfg_Path_Root, ".ani");
            MdlZTStudio.InfoBox("MdlTasks", "CleanUpFiles", "Deleted all ZT1 Graphic files (including .ani and .pal) in the root folder.");
        }



        /// <summary>
    /// Handles footprint (X) selection change
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_fpX_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Update setting and remember
            MdlSettings.Cfg_Grid_FootPrintX = Conversions.ToByte(TsbFrame_fpX.Text);
            MdlConfig.Write();

            // Update preview. Update frame info, not GUI (button states etc)
            MdlZTStudioUI.UpdatePreview(true, false);
        }

        /// <summary>
    /// Handles footprint (Y) selection change
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_fpY_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Update setting and remember
            MdlSettings.Cfg_Grid_FootPrintY = Conversions.ToByte(TsbFrame_fpY.Text);
            MdlConfig.Write();

            // Update preview. Update frame info, not GUI (button states etc)
            MdlZTStudioUI.UpdatePreview(true, false);
        }


        /// <summary>
    /// Handles double clicking a color in the datagridview (color palette on the right)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">DataGridViewCellEventArgs</param>
        private void DgvPaletteMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            // Actual row rather than header. Avoid crash.
            if (e.RowIndex > -1)
            {

                // Replace colors
                MdlColorPalette.ReplaceColor(e.RowIndex);
            }
        }


        /// <summary>
    /// Handles clicking a color in the datagridview (color palette on the right), triggers menu
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">DataGridViewCellEventArgs</param>
        private void DgvPaletteMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                // Set selected
                DgvPaletteMain.Rows[e.RowIndex].Selected = true;
            }

            if (e.Button == MouseButtons.Right)
            {

                // Which options to move the color up, down or to the end are enabled?
                mnuPal_MoveDown.Visible = e.RowIndex != DgvPaletteMain.Rows.Count - 1;
                mnuPal_MoveEnd.Visible = e.RowIndex != DgvPaletteMain.Rows.Count - 1;
                mnuPal_MoveUp.Visible = e.RowIndex != 0;

                // Display menu
                MnuPal.Show(Cursor.Position);
            }
        }

        /// <summary>
    /// Handles clicking 'move color up' in the palette menu.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_MoveUp_Click(object sender, EventArgs e)
        {
            MdlColorPalette.MoveColor(DgvPaletteMain.SelectedRows[0].Index, DgvPaletteMain.SelectedRows[0].Index - 1);
        }

        /// <summary>
    /// Handles clicking 'move color down' in the palette menu.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_MoveDown_Click(object sender, EventArgs e)
        {
            MdlColorPalette.MoveColor(DgvPaletteMain.SelectedRows[0].Index, DgvPaletteMain.SelectedRows[0].Index + 1);
        }


        /// <summary>
    /// Handles clicking 'replace color' in the palette menu.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_Replace_Click(object sender, EventArgs e)
        {
            MdlColorPalette.ReplaceColor(DgvPaletteMain.SelectedRows[0].Index);
        }

        /// <summary>
    /// Handles clicking 'move color to the end' in the palette menu.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_MoveEnd_Click(object sender, EventArgs e)
        {
            MdlColorPalette.MoveColor(DgvPaletteMain.SelectedRows[0].Index, MdlSettings.EditorGraphic.ColorPalette.Colors.Count - 1);
        }

        /// <summary>
    /// Handles clicking 'add color' in the palette menu.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_Add_Click(object sender, EventArgs e)
        {

            // Add after this entry
            MdlColorPalette.AddColor(DgvPaletteMain.SelectedRows[0].Index);
        }


        /// <summary>
    /// Handles toolbar button click to import a PNG image into the current frame (or a new one if right click)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void TsbFrame_ImportPNG_MouseDown(object sender, MouseEventArgs e)
        {

            // Shortcut to create a new frame first, then import the PNG to it.

            if (e.Button == MouseButtons.Right)
            {
            // Add frame
            0:
                ;
                var ObjFrame = new ClsFrame(MdlSettings.EditorGraphic);
            10:
                ;

                // Add the frame after the existing one(s)
                MdlSettings.EditorGraphic.Frames.Insert(TbFrames.Value, ObjFrame);
            15:
                ;

                // not sure if this is right if an extra frame is applied?
                TbFrames.Maximum = MdlSettings.EditorGraphic.Frames.Count - MdlSettings.EditorGraphic.HasBackgroundFrame;
            16:
                ;
                TbFrames.Value += 1;

                // Update current preview. Update frame info (offsets), GUI (button states, counts etc)
                MdlZTStudioUI.UpdatePreview(true, true, TbFrames.Value - 1);
            }

        100:
            ;
            {
                var withBlock = DlgOpen;
                withBlock.Title = "Pick a .PNG file";
                withBlock.DefaultExt = "";
                withBlock.Filter = "PNG files|*.png";

                // Suggest directory of most recently opened PNG
                withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentPNG).Directory.FullName;


                // If most recent directory does not exist anymore:
                if (string.IsNullOrEmpty(DlgOpen.InitialDirectory) | Directory.Exists(DlgOpen.InitialDirectory) == false)
                {
                    withBlock.InitialDirectory = MdlSettings.Cfg_Path_Root;
                }

                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {
                    if (File.Exists(DlgOpen.FileName) == true)
                    {
                        if (Path.GetExtension(DlgOpen.FileName).ToLower() != ".png")
                        {
                            string StrMessage = "" + "You selected a file with the extension '" + Path.GetExtension(DlgOpen.FileName) + "'." + Constants.vbCrLf + "You need a file with a .PNG extension.";
                            MdlZTStudio.HandledError(GetType().FullName, "TsbFrame_ImportPNG_MouseDown", StrMessage);
                        }
                        else
                        {

                            // OK
                            MdlSettings.EditorFrame.LoadPNG(DlgOpen.FileName);

                            // Draw first frame 
                            MdlZTStudioUI.UpdatePreview(true, true);

                            // Show main color palette (new colors may have been added)
                            MdlSettings.EditorFrame.Parent.ColorPalette.FillPaletteGrid(DgvPaletteMain);

                            // Not sure why we had this. It's the color palette of the background graphic.
                            // editorBgGraphic.colorPalette.fillPaletteGrid(dgvPaletteMain)

                            // Remember
                            MdlSettings.Cfg_Path_RecentPNG = new FileInfo(DlgOpen.FileName).Directory.FullName;
                            MdlConfig.Write();
                        }
                    }
                    else
                    {
                        MdlZTStudio.HandledError(GetType().FullName, "TsbFrame_ImportPNG_MouseDown", "File does not exist.", false);
                    }
                }
            }
        }




        /// <summary>
    /// Handles toolbar button click to import a PNG image into the current frame (or a new one if right click)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_ExportPNG_Click(object sender, EventArgs e)
        {
            {
                var withBlock = DlgSave;
                withBlock.Title = "Save as a PNG Color Palette";
                withBlock.DefaultExt = ".png";
                withBlock.Filter = "PNG Color Palette files (*.png)|*.png|All files|*.*";

                // By default same directory as most recently picked ZT1 Graphic
                withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentZT1).Directory.FullName;
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {

                    // Export the frame to a PNG image
                    MdlSettings.EditorGraphic.ColorPalette.ExportToPNG(DlgSave.FileName);
                }
            }
        }



        /// <summary>
    /// Handles menu click to import a PNG image as a color palette
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_ImportPNG_Click(object sender, EventArgs e)
        {
            {
                var withBlock = DlgOpen;
                withBlock.Title = "Pick a PNG Color Palette";
                withBlock.DefaultExt = ".png";
                withBlock.Filter = "PNG Color Palette files (*.png)|*.png|All files|*.*";

                // By default, specify directory of last chosen PNG file
                withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentPNG).Directory.FullName;
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {

                    // Replace palette file (should trigger a re-draw AFTERWARDS )
                    // Forcefully add colors (some might be the same, after a recolor)
                    MdlSettings.EditorGraphic.ColorPalette.ImportFromPNG(DlgOpen.FileName);

                    // Update color list on the right
                    MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(DgvPaletteMain);


                    // Now after the color palette has been replaced, our preview must be updated
                    MdlZTStudioUI.UpdatePreview(true, true);
                }
            }
        }


        /// <summary>
    /// Handles menu click to save as a ZT1 Color palette
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_SavePAL_Click(object sender, EventArgs e)
        {
            {
                var withBlock = DlgSave;
                withBlock.Title = "Save as a ZT1 Color Palette";
                withBlock.DefaultExt = ".pal";
                withBlock.Filter = "ZT1 Color Palette files (*.pal)|*.pal|All files|*.*";
                withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentZT1).Directory.FullName;

                // If user didn't cancel, create ZT1 Color palette
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {
                    MdlSettings.EditorGraphic.ColorPalette.WritePal(DlgSave.FileName, true);
                }
            }
        }

        /// <summary>
    /// Handles menu click to import a GIMP Color Palette
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void MnuPal_ImportGimpPalette_Click(object sender, EventArgs e)
        {
            {
                var withBlock = DlgOpen;
                withBlock.Title = "Pick a GIMP Color Palette";
                withBlock.DefaultExt = ".gpl";
                withBlock.Filter = "GIMP Color Palette (*.gpl)|*.gpl|All files|*.*";

                // Uses most recent ZT1 Graphic path
                withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentZT1).Directory.FullName;

                // If user didn't cancel, import GIMP Palette
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {

                    // Replace palette file (should trigger a redraw of coreImageBitmap)
                    // Forcefully add colors (some might be the same, after a recolor)
                    MdlSettings.EditorGraphic.ColorPalette.ImportFromGIMPPalette(DlgOpen.FileName);

                    // Update color list on the right
                    MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(DgvPaletteMain);
                }
            }
        }


        /// <summary>
    /// Handles toolbar button click to batch rotation fix a set of graphics
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbBatchRotFix_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.FrmBatchOffsetFix.ShowDialog(this);
        }


        /// <summary>
    /// Handles toolbar button click to create a new ZT1 Graphic from scratch
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>

        private void TsbZT1New_Click(object sender, EventArgs e)
        {

            // New ZT1 Graphic
            MdlSettings.EditorGraphic = new ClsGraphic(null);

            // Always start with one frame
            MdlSettings.EditorFrame = new ClsFrame(MdlSettings.EditorGraphic);
            MdlSettings.EditorGraphic.Frames.Add(MdlSettings.EditorFrame);

            // Update/reset color palette
            MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(DgvPaletteMain);

            // This is the only (or one of the few) cases where frame reset happens:
            TbFrames.Value = 1;

            // Update preview
            MdlZTStudioUI.UpdatePreview(true, true, 0);
        }


        /// <summary>
    /// Handles toolbar button click to save as a ZT1 Graphic
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void TsbZT1Write_MouseDown(object sender, MouseEventArgs e)
        {
            if (MdlSettings.EditorGraphic.Frames.Count == 0)
            {
                MdlZTStudio.HandledError(GetType().FullName, "TsbZT1Write_MouseDown", "You can't create a ZT1 Graphic without adding a frame first.");
                return;
            }

            if (e.Button == MouseButtons.Right)
            {

                // Shortcut to saving directly
                if (File.Exists(MdlSettings.EditorGraphic.FileName) == true)
                {

                    // Save graphic (existing graphic, overwrite)
                    MdlTasks.SaveGraphic(MdlSettings.EditorGraphic.FileName);
                    MdlConfig.Write();

                    // No need to continue
                    return;
                }
            }

            // Shortcut above failed, go over entire saving process

            // Where shall we save this ZT1 Graphic?
            {
                var withBlock = DlgSave;
                withBlock.Title = "Save ZT1 Graphic";
                withBlock.DefaultExt = "";
                withBlock.AddExtension = true;
                withBlock.FileName = MdlSettings.Cfg_Path_RecentZT1;
                withBlock.Filter = "ZT1 Graphics|*";
                withBlock.InitialDirectory = new FileInfo(MdlSettings.Cfg_Path_RecentZT1).Directory.FullName;
                if (withBlock.ShowDialog() != DialogResult.Cancel)
                {
                    if (!string.IsNullOrEmpty(Path.GetExtension(DlgSave.FileName).ToLower()))
                    {
                        MdlZTStudio.HandledError(GetType().FullName, "TsbZT1Write_MouseDown", "A ZT1 Graphic file does not have a file extension.");
                        return;
                    }

                51:
                    ;
                    MdlTasks.SaveGraphic(DlgSave.FileName);
                60:
                    ;

                    // Remember
                    MdlSettings.Cfg_Path_RecentZT1 = DlgSave.FileName;
                    MdlConfig.Write();

                    // What has been opened, might need to be saved.
                    DlgOpen.FileName = DlgSave.FileName;
                65:
                    ;

                    // Might be a new file, so update root folder and select this.
                    MdlZTStudioUI.UpdateExplorerPane();
                }
            }
        }



        /// <summary>
    /// Handles check change to start/stop playing animation
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">MouseEventArgs</param>
        private void ChkPlayAnimation_CheckedChanged(object sender, EventArgs e)
        {

            // Set timer to the specified graphic's animationspeed
            // Enable/disable timer
            TmrAnimation.Interval = MdlSettings.EditorGraphic.AnimationSpeed;
            TmrAnimation.Enabled = ChkPlayAnimation.Checked;
        }



        /// <summary>
    /// Handles selection of a graphic in the Explorer Pane
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">TreeViewEventArgs</param>
        private void TVExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MdlZTStudio.Trace(GetType().FullName, "TVExplorer_AfterSelect", "Selected node " + e.Node.Text + " -> " + e.Node.Name);

            // If the selected item is a ZT1 Graphic file, load?
            if (Regex.IsMatch(e.Node.Text, "[0-9A-z]") == true & e.Node.ImageIndex == 0)
            {

                // Same handling as ZT1 open graphic button (but don't do loop: selection also happens on form load)
                if ((Strings.LCase(MdlSettings.EditorGraphic.FileName) ?? "") != (Strings.LCase(MdlSettings.Cfg_Path_Root + @"\" + e.Node.Name) ?? ""))
                {
                    MdlZTStudioUI.LoadGraphic(MdlSettings.Cfg_Path_Root + @"\" + e.Node.Name);
                }
            }
        }



        /// <summary>
    /// Handles Leave event of toolstrip textbox animation speed. Resets animation speed textbox.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TstZT1_AnimSpeed_Leave(object sender, EventArgs e)
        {

            // If nothing has been confirmed ([Enter]), reset original value
            TstZT1_AnimSpeed.Text = MdlSettings.EditorGraphic.AnimationSpeed.ToString();
        }

        /// <summary>
    /// Handles confirmation of new offset (on Enter)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">KeyEventArgs</param>
        private void TstOffsetX_KeyDown(object sender, KeyEventArgs e)
        {

            // On enter
            if ((int)e.KeyCode != 13)
            {
                return;
            }

            if (string.IsNullOrEmpty(TstOffsetX.Text))
            {
                // Suspend checking, user is most likely still busy changing this value
                return;
            }
            else if (Information.IsNumeric(TstOffsetX.Text) == false)
            {
                MdlZTStudio.HandledError(GetType().FullName, "TstOffsetX_TextChanged", "Offset should be a numerical value between -32767 and 32767");
                return;
            }
            else if (Conversions.ToInteger(TstOffsetX.Text) < -32767 | Conversions.ToInteger(TstOffsetX.Text) > 32767)
            {
                MdlZTStudio.HandledError(GetType().FullName, "TstOffsetX_TextChanged", "Offset should be a numerical value between -32767 and 32767");
                return;
            }


            // UpdateOffsets() takes changes, not final coordinates
            // Get the difference
            int IntDifference = Conversions.ToInteger(TstOffsetX.Text) - MdlSettings.EditorFrame.OffsetX;
            MdlSettings.EditorFrame.UpdateOffsets(new Point(IntDifference, 0));
            MdlZTStudioUI.UpdatePreview(true, false);
        }


        /// <summary>
    /// Handles confirmation of new offset (on Enter)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">KeyEventArgs</param>
        private void TstOffsetY_KeyDown(object sender, KeyEventArgs e)
        {

            // On enter
            if ((int)e.KeyCode != 13)
            {
                return;
            }

            if (string.IsNullOrEmpty(TstOffsetY.Text))
            {
                // Suspend checking, user is most likely still busy changing this value
                return;
            }
            else if (Information.IsNumeric(TstOffsetY.Text) == false)
            {
                MdlZTStudio.HandledError(GetType().FullName, "TstOffsetY_TextChanged", "Offset should be a numerical value between -32767 and 32767");
                return;
            }
            else if (Conversions.ToInteger(TstOffsetY.Text) < -32767 | Conversions.ToInteger(TstOffsetY.Text) > 32767)
            {
                MdlZTStudio.HandledError(GetType().FullName, "TstOffsetY_TextChanged", "Offset should be a numerical value between -32767 and 32767");
                return;
            }


            // UpdateOffsets() takes changes, not final coordinates
            // Get the difference
            int IntDifference = Conversions.ToInteger(TstOffsetY.Text) - MdlSettings.EditorFrame.OffsetY;
            MdlSettings.EditorFrame.UpdateOffsets(new Point(0, IntDifference));
            MdlZTStudioUI.UpdatePreview(true, false);
        }


        /// <summary>
    /// Handles Leave event of toolstrip textbox offset X. Resets offset value in textbox.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TstOffsetX_Leave(object sender, EventArgs e)
        {

            // If nothing has been confirmed ([Enter]), reset original value
            TstOffsetX.Text = MdlSettings.EditorFrame.OffsetX.ToString();
        }



        /// <summary>
    /// Handles Leave event of toolstrip textbox offset Y. Resets offset
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TstOffsetY_Leave(object sender, EventArgs e)
        {

            // If nothing has been confirmed ([Enter]), reset original value
            TstOffsetY.Text = MdlSettings.EditorFrame.OffsetY.ToString();
        }


        /// <summary>
    /// Handles confirmation of new animation speed (on Enter)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">KeyEventArgs</param>
        private void TstZT1_AnimSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode != 13)
            {
                // Not confirming by pressing [Enter]
            }

            if (string.IsNullOrEmpty(TstZT1_AnimSpeed.Text))
            {
                // User is just changing value, don't be too strict on empty values.
                return;
            }
            else if (Information.IsNumeric(TstZT1_AnimSpeed.Text) == false)
            {

                // Not numeric = invalid
                MdlZTStudio.HandledError(GetType().FullName, "TstZT1_AnimSpeed_TextChanged", "The animation speed should be a number of milliseconds.");
                return;
            }
            else if (Conversions.ToInteger(TstZT1_AnimSpeed.Text) < 1 | Conversions.ToInteger(TstZT1_AnimSpeed.Text) > 1000)
            {

                // Not in a valid range. Theoretically, the interval could be much higher.
                // In practical ways, it should never be.
                MdlZTStudio.HandledError(GetType().FullName, "TstZT1_AnimSpeed_TextChanged", "Invalid value for animation speed. Expecting a value between 1 and 1000 milliseconds.");
                return;
            }


            // Seems to be okay, numeric and within range.
            MdlSettings.EditorGraphic.AnimationSpeed = Conversions.ToInteger(TstZT1_AnimSpeed.Text);
        }

        /// <summary>
    /// Toggle option to enforce offsets on each frame in the loaded ZT1 Graphic
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TsbFrame_OffsetAll_Click(object sender, EventArgs e)
        {
            TsbFrame_OffsetAll.Checked = !TsbFrame_OffsetAll.Checked; // Change toggle
            MdlSettings.Cfg_Editor_RotFix_IndividualFrame = (byte)(Conversions.ToInteger(TsbFrame_OffsetAll.Checked) * -1);
            MdlConfig.Write();
        }

        /// <summary>
    /// Doubleclick on Explorer Pane may open .pal file
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void TVExplorer_DoubleClick(object sender, EventArgs e)
        {
            if (Information.IsNothing(TVExplorer.SelectedNode) == false)
            {

                // Open .pal file
                if (Regex.IsMatch(TVExplorer.SelectedNode.Name, @".*\.pal$", RegexOptions.IgnoreCase) == true)
                {
                    MdlColorPalette.LoadPalette(MdlSettings.Cfg_Path_Root + @"\" + TVExplorer.SelectedNode.Name);
                }
            }
        }

        /// <summary>
    /// Events on form closing (ZT Studio exiting)
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">FormClosingEventArgs</param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            // Due to new implementations, some processes might still be running
            Environment.Exit(0);
        }

        /// <summary>
    /// Handles manual refreshing of Explorer Pane
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void CmdUpdateExplorerPane_Click(object sender, EventArgs e)
        {
            MdlZTStudioUI.UpdateExplorerPane();
        }
    }
}