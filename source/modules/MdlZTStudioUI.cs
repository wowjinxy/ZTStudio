using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{



    /// <summary>
/// Methods related to ZT Studio UI
/// </summary>

    static class MdlZTStudioUI
    {

        /// <summary>
    /// Load graphic and show
    /// </summary>
    /// <param name="StrFileName">Source file name</param>
        public static void LoadGraphic(string StrFileName)
        {
            MdlZTStudio.Trace("MdlZTStudioUI", "LoadGraphic", "Loading " + StrFileName);
            if (File.Exists(StrFileName) == false)
            {
                string StrMessage = "File does not exist.";
                MdlZTStudio.HandledError("MdlZTStudioUI", "LoadGraphic", StrMessage);
                return;
            }
            else if (!string.IsNullOrEmpty(Path.GetExtension(StrFileName)))
            {
                string StrErrorMessage = "" + "You selected a file with the extension '" + Path.GetExtension(StrFileName) + "'." + Constants.vbCrLf + "ZT Studio expects you to select a ZT1 Graphic file, which shouldn't have a file extension.";
                MdlZTStudio.HandledError("MdlZTStudioUI", "LoadGraphic", StrErrorMessage);
                return;
            }
            else if (StrFileName.ToLower().Contains(MdlSettings.Cfg_Path_Root.ToLower()) == false)
            {
                string StrErrorMessage = "" + "Only select a file in the root directory, which is currently:" + Constants.vbCrLf + MdlSettings.Cfg_Path_Root + Constants.vbCrLf + Constants.vbCrLf + "Would you like to change the root directory?";
                if (Interaction.MsgBox(StrErrorMessage, (MsgBoxStyle)((int)MsgBoxStyle.YesNo + (int)MsgBoxStyle.Critical + (int)MsgBoxStyle.ApplicationModal), "ZT1 Graphic not within root folder") == MsgBoxResult.Yes)
                {

                    // Allow user to quickly change settings -> root directory
                    My.MyProject.Forms.FrmSettings.Show();
                }

                return;
            }
            else
            {



                // Reset any previous info.
                string StrFileNamePalette_Original = MdlSettings.EditorGraphic.ColorPalette.FileName;
                MdlSettings.EditorGraphic = new ClsGraphic(MdlSettings.EditorGraphic.ColorPalette);

                // OK
                MdlSettings.EditorGraphic.Read(StrFileName);

                // Keep filename
                My.MyProject.Forms.FrmMain.ssFileName.Text = DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": opened " + StrFileName;

                // Show default palette
                if ((StrFileNamePalette_Original ?? "") != (MdlSettings.EditorGraphic.ColorPalette.FileName ?? ""))
                {
                    MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(My.MyProject.Forms.FrmMain.DgvPaletteMain);
                }


                // Set editorframe
                MdlSettings.EditorFrame = MdlSettings.EditorGraphic.Frames[0];
                My.MyProject.Forms.FrmMain.TbFrames.Value = 1;

                // Draw first frame. Must be done after setting EditorFrame!
                UpdatePreview(true, true, 0);

                // Remember
                MdlSettings.Cfg_Path_RecentZT1 = StrFileName;
                MdlConfig.Write();
            }

            // Select in Explorer Pane (if not the case yet)
            var ObjNodeSet = My.MyProject.Forms.FrmMain.TVExplorer.Nodes.Find(Strings.LCase(Regex.Replace(MdlSettings.Cfg_Path_RecentZT1, "^" + Regex.Escape(MdlSettings.Cfg_Path_Root) + @"\\", "")), true);
            if (ObjNodeSet.Count() == 1)
            {
                My.MyProject.Forms.FrmMain.TVExplorer.SelectedNode = ObjNodeSet[0];
            }
        }


        /// <summary>
    /// <para>
    ///     Updates info in main window.
    /// </para>
    /// <para>
    ///     Updates shown info such as animation speed, number of frames, current frame, ...
    /// </para>
    /// <para>
    ///     Enables/disables certain controls (for example, button to render background frame)
    /// </para>
    /// </summary>
    /// <param name="StrReason"></param>
        public static void UpdateGUI(string StrReason)
        {

            // Displays updated info.
            // 20190816: note: before today, it relied on .indexOf(), which might return incorrect results if there are similar frames. Now intFrameIndex is added and required.

            MdlZTStudio.Trace("MdlZTStudioUI", "UpdateGUI", "Reason: " + StrReason + ". Non-background frames: " + (MdlSettings.EditorGraphic.Frames.Count - MdlSettings.EditorGraphic.HasBackgroundFrame) + " - background frame: " + MdlSettings.EditorGraphic.HasBackgroundFrame.ToString());
            int IntFrameIndex = My.MyProject.Forms.FrmMain.TbFrames.Value - 1;
            {
                var withBlock = My.MyProject.Forms.FrmMain;
                withBlock.TstZT1_AnimSpeed.Text = MdlSettings.EditorGraphic.AnimationSpeed.ToString();

                // == Graphic
                withBlock.TsbGraphic_ExtraFrame.Enabled = MdlSettings.EditorGraphic.Frames.Count > 1; // Background frame can only be enabled if there's more than one frame
                withBlock.TsbGraphic_ExtraFrame.Checked = MdlSettings.EditorGraphic.HasBackgroundFrame == 1; // Is background frame enabled for this graphic? Then toggle button.

                // == Frame
                withBlock.TsbFrame_Delete.Enabled = MdlSettings.EditorGraphic.Frames.Count > 1;
                withBlock.TsbFrame_ExportPNG.Enabled = false;

                // (IsNothing(editorGraphic.frames(0).cachedFrame) = False)

                if (Information.IsNothing(MdlSettings.EditorFrame) == false)
                {
                    if (MdlSettings.EditorFrame.CoreImageHex.Count > 0)
                    {
                        withBlock.TsbFrame_ExportPNG.Enabled = true;
                    }
                }

                withBlock.TsbFrame_ImportPNG.Enabled = MdlSettings.EditorGraphic.Frames.Count > 0;
                withBlock.TsbFrame_OffsetDown.Enabled = MdlSettings.EditorGraphic.Frames.Count > 0;
                withBlock.TsbFrame_OffsetUp.Enabled = MdlSettings.EditorGraphic.Frames.Count > 0;
                withBlock.TsbFrame_OffsetLeft.Enabled = MdlSettings.EditorGraphic.Frames.Count > 0;
                withBlock.TsbFrame_OffsetRight.Enabled = MdlSettings.EditorGraphic.Frames.Count > 0;
                withBlock.TsbFrame_IndexIncrease.Enabled = MdlSettings.EditorGraphic.Frames.Count > 1 & IntFrameIndex < MdlSettings.EditorGraphic.Frames.Count - 1 - MdlSettings.EditorGraphic.HasBackgroundFrame;
                withBlock.TsbFrame_IndexDecrease.Enabled = MdlSettings.EditorGraphic.Frames.Count > 1 & IntFrameIndex > 0;
                withBlock.PicBox.BackColor = MdlSettings.Cfg_Grid_BackGroundColor;
            }

        105:
            ;


            // Add time indication
            My.MyProject.Forms.FrmMain.LblAnimTime.Text = (MdlSettings.EditorGraphic.Frames.Count - MdlSettings.EditorGraphic.HasBackgroundFrame) * MdlSettings.EditorGraphic.AnimationSpeed + " ms ";
            My.MyProject.Forms.FrmMain.LblFrames.Text = MdlSettings.EditorGraphic.Frames.Count - MdlSettings.EditorGraphic.HasBackgroundFrame + " frames. ";
        205:
            ;
            if (!string.IsNullOrEmpty(MdlSettings.EditorGraphic.FileName))
            {

                // Get path
                var ObjFileInfo = new FileInfo(MdlSettings.EditorGraphic.FileName);
                string StrDirectoryName = ObjFileInfo.Directory.FullName;
                MdlZTStudio.Trace("MdlZTStudioUI", "UpdateInfo", "Path of graphic is " + StrDirectoryName);
            }
        }

        /// <summary>
    /// Updates explorer pane
    /// </summary>
        public static void UpdateExplorerPane()
        {
            MdlZTStudio.Trace("MdlZTStudio", "UpdateExplorerPane", "Updating Explorer pane");
            var TVExplorer = My.MyProject.Forms.FrmMain.TVExplorer;
            var StackDirectories = new Stack<string>();
            StackDirectories.Push(MdlSettings.Cfg_Path_Root);
            var ObjImageList = new ImageList();
            var ObjNodeCollection = TVExplorer.Nodes;
            ObjImageList.Images.Add(My.Resources.Resources.icon_ZT1_Graphic);
            ObjImageList.Images.Add(My.Resources.Resources.icon_folder);
            ObjImageList.Images.Add(My.Resources.Resources.icon_file);
            ObjImageList.Images.Add(My.Resources.Resources.icon_ZT1_palette);
            TVExplorer.ImageList = ObjImageList;
            TVExplorer.BeginUpdate();
            TVExplorer.Nodes.Clear();


            // Continue processing for each stacked directory
            while (StackDirectories.Count > 0)
            {

                // Get top directory string
                var ObjNode = new TreeNode();
                string StrDirectoryName = StackDirectories.Pop();
                Debug.Print(StrDirectoryName);
                if ((StrDirectoryName ?? "") != (MdlSettings.Cfg_Path_Root ?? ""))
                {
                    ObjNode.Name = Regex.Replace(StrDirectoryName, "^" + Regex.Escape(MdlSettings.Cfg_Path_Root) + @"\\", "");
                    ObjNode.Text = Regex.Match(ObjNode.Name, @"(?=[^\\]*$).*$").Value;
                    ObjNode.ImageIndex = 1;
                    ObjNode.SelectedImageIndex = 1;

                    // Parent node?
                    string StrParentDirectory = Regex.Replace(ObjNode.Name, @"\\(?=[^\\]*$).*$", "");
                    var ObjParentNode = ObjNodeCollection.Find(StrParentDirectory, true);
                    if (ObjParentNode.Count() == 1)
                    {
                        ObjParentNode[0].Nodes.Add(ObjNode);
                    }
                    else
                    {
                        ObjNodeCollection.Add(ObjNode);
                    }

                    // Loop through all subdirectories and add them to the stack.
                }

                foreach (var StrSubDirectoryName in Directory.GetDirectories(StrDirectoryName).Reverse())

                    // Subdirectories will be processed later. But as for current dir...

                    // Loop through all files and add them to the node
                    StackDirectories.Push(StrSubDirectoryName);
                foreach (var StrSubFileName in Directory.GetFiles(StrDirectoryName))
                {
                    var ObjFileNode = new TreeNode();
                    ObjFileNode.Name = Regex.Replace(StrSubFileName, "^" + Regex.Escape(MdlSettings.Cfg_Path_Root) + @"\\", "");
                    ObjFileNode.Text = Regex.Match(ObjFileNode.Name, @"(?=[^\\]*$).*$").Value;

                    // Guess if it's a graphic or not
                    if (Regex.IsMatch(ObjFileNode.Text, "^[0-9A-z]{1,}$", RegexOptions.Singleline))
                    {
                        ObjFileNode.ImageIndex = 0;
                        ObjFileNode.SelectedImageIndex = 0;
                    }
                    else if (Regex.IsMatch(ObjFileNode.Text, @"^.*\.pal$", RegexOptions.Singleline))
                    {
                        ObjFileNode.ImageIndex = 3;
                        ObjFileNode.SelectedImageIndex = 3;
                    }
                    else
                    {
                        ObjFileNode.ImageIndex = 2;
                        ObjFileNode.SelectedImageIndex = 2;
                    }

                    ObjNode.Nodes.Add(ObjFileNode);
                }


                // Make sure everything is finished. Needed?
                Application.DoEvents();
            }

            TVExplorer.EndUpdate();
        }

        /// <summary>
    /// Updates shown info such as number of frames, current frame, ...
    /// </summary>
    /// <param name="StrReason"></param>
        public static void UpdateFrameInfo(string StrReason)
        {
            MdlZTStudio.Trace("MdlZTStudioUI", "UpdateFrameInfo", "Reason: " + StrReason + ". Non-background frames: " + (MdlSettings.EditorGraphic.Frames.Count - MdlSettings.EditorGraphic.HasBackgroundFrame) + " - background frame: " + MdlSettings.EditorGraphic.HasBackgroundFrame.ToString());
            int IntFrameIndex = My.MyProject.Forms.FrmMain.TbFrames.Value - 1;
            {
                var withBlock = My.MyProject.Forms.FrmMain;

                // NOT using a 0-based frame index visual indication, to avoid confusing
                withBlock.TslFrame_Index.Text = Conversions.ToString(Interaction.IIf(MdlSettings.EditorGraphic.Frames.Count == 0, "-", IntFrameIndex + 1 + " / " + (MdlSettings.EditorGraphic.Frames.Count - MdlSettings.EditorGraphic.HasBackgroundFrame)));
                {
                    var withBlock1 = withBlock.TbFrames;
                    withBlock1.Minimum = 1;
                    withBlock1.Maximum = MdlSettings.EditorGraphic.Frames.Count - MdlSettings.EditorGraphic.HasBackgroundFrame; // for actually generated files: - editorGraphic.extraFrame)
                    if (withBlock1.Maximum < 1)
                    {
                        withBlock1.Minimum = 1;
                        withBlock1.Maximum = 1;
                    }

                    if (withBlock1.Value < withBlock1.Minimum)
                    {
                        withBlock1.Value = withBlock1.Minimum;
                    }
                }

                if (Information.IsNothing(MdlSettings.EditorFrame))
                {
                    withBlock.TstOffsetX.Text = "0";
                    withBlock.TstOffsetY.Text = "0";
                }
                else
                {
                    // .tbFrames.Value = editorGraphic.frames.IndexOf(editorFrame) + 1
                    withBlock.TstOffsetX.Text = MdlSettings.EditorFrame.OffsetX.ToString();
                    withBlock.TstOffsetY.Text = MdlSettings.EditorFrame.OffsetY.ToString();
                }
            }
        }

        /// <summary>
    /// Updates all sort of info.
    /// </summary>
    /// <param name="BlnUpdateFrameInfo">Boolean. Update frame info.</param>
    /// <param name="BlnUpdateUI">Boolean. Update UI (buttons), animation speed, file list...).</param>
    /// <param name="IntIndexFrameNumber">Optional frame index number. Defaults to value of slider in main window.</param>
        public static void UpdatePreview(bool BlnUpdateFrameInfo, bool BlnUpdateUI, int IntIndexFrameNumber = -1)
        {
        10:
            ;

            // Can't update if there are no frames.
            if (MdlSettings.EditorGraphic.Frames.Count == 0)
            {
                // Add time indication
                My.MyProject.Forms.FrmMain.LblAnimTime.Text = "0 ms";
                My.MyProject.Forms.FrmMain.LblFrames.Text = "0 frames.";
                return;
            }

        20:
            ;

            // Shortcut. If no index number for the frame was specified, assume the currently visible frame needs to be updated.
            if (IntIndexFrameNumber == -1)
            {
                IntIndexFrameNumber = My.MyProject.Forms.FrmMain.TbFrames.Value - 1;
            }

        125:
            ;

            // 20190816: some aspects weren't managed properly, for instance when toggling extra frame or adding/removing frames.
            // Previous/next frame; current And max value of progress bar, ...
            // Update preview is called from lots of places, so this may be a bit of an overkill, but better safe.
            if (BlnUpdateFrameInfo == true)
            {
                UpdateFrameInfo("MdlZTStudioUI_UpdatePreview()");
            }

        126:
            ;
            if (BlnUpdateUI == true)
            {
                UpdateGUI("MdlZTStudioUI_UpdatePreview()");
            }

        130:
            ;
            MdlSettings.EditorFrame = MdlSettings.EditorGraphic.Frames[IntIndexFrameNumber];
        300:
            ;

            // The sub gets triggered when a new frame has been added, but no .PNG has been loaded yet, so frame contains no data.
            // However, the picbox may need to be cleared (previous frame would still be shown otherwise)
            if (MdlSettings.EditorGraphic.Frames[IntIndexFrameNumber].CoreImageHex.Count == 0)
            {
            310:
                ;
                My.MyProject.Forms.FrmMain.PicBox.Image = MdlBitMap.DrawGridFootPrintXY(MdlSettings.Cfg_Grid_FootPrintX, MdlSettings.Cfg_Grid_FootPrintY).Bitmap;
            }
            else
            {
            320:
                ;
                My.MyProject.Forms.FrmMain.PicBox.Image = MdlSettings.EditorGraphic.Frames[IntIndexFrameNumber].GetImage(true).Bitmap;
            }
        }


        /// <summary>
    /// Shows tooltip when hovering above element
    /// </summary>
    /// <param name="ObjControl">Control which owns this tooltip</param>
    /// <param name="StrMessage">Message</param>
        public static void ShowToolTip(Control ObjControl, string StrMessage)
        {
            My.MyProject.Forms.FrmSettings.LblHelpTopic.Text = ObjControl.Text;
            My.MyProject.Forms.FrmSettings.LblHelp.Text = StrMessage;
        }
    }
}