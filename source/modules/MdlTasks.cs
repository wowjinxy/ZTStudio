using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{

    // This module contains several methods.

    static class MdlTasks
    {

        /// <summary>
    /// Cleans up files in a path, based on extension.
    /// </summary>
    /// <remarks>
    /// Used to clean up .pal-files and files without a file extension (=ZT1 Graphic files)
    /// </remarks>
    /// <param name="StrPath"></param>
    /// <param name="StrExtension"></param>
    /// <returns></returns>
        public static int CleanUpFiles(string StrPath, string StrExtension)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 639


        Input:

                On Error GoTo dBug

         */
        0:
            ;
        5:
            ;

            // Creating a recursive list.

            // This list stores the results.
            var LstResult = new List<string>();

            // This stack stores the directories within the <root> folder to process.
            // Then process each subdirectory.
            var Stack = new Stack<string>();

            // Add the initial directory
            Stack.Push(StrPath);
        10:
            ;

            // Continue processing for each stacked directory
            while (Stack.Count > 0)
            {
            15:
                ;

                // Get top directory name
                string StrDirectoryName = Stack.Pop();
            20:
                ;

                // Get all files and check if they match the extension (.pal, .png) or have no extension (ZT1 graphic)
                // In this 'for' construction the wildcard '*' is used; which may also match other files WITH extension.
                foreach (string f in Directory.GetFiles(StrDirectoryName, "*"))
                {
                    // Does the extension match? (or for ZT1 Graphic files: this should match an empty string)
                    if ((Path.GetExtension(f) ?? "") == (StrExtension ?? ""))
                    {
                        LstResult.Add(f);
                    }
                }

            // Loop through all subdirectories and add them to the stack, so they're processed as well.
            25:
                ;
                foreach (var StrSubDirectoryName in Directory.GetDirectories(StrDirectoryName))
                    Stack.Push(StrSubDirectoryName);
            }

        1000:
            ;

            // For each file that matched the specified extension/pattern
            foreach (string StrFileName in LstResult)
            {
                MdlZTStudio.Trace("MdlTasks", "CleanUpFiles", "Delete file: " + StrFileName);
                File.Delete(StrFileName);
            }

            Application.DoEvents();
        1010:
            ;
            MdlZTStudioUI.UpdateExplorerPane();
            return default;
        dBug:
            ;
            string StrMessage = "An error occured while trying to clean up ZT1 Graphic files in this folder: " + Constants.vbCrLf + StrPath;
            MdlZTStudio.HandledError("MdlTasks", "CleanUpFiles", StrMessage, false, Information.Err());
        }

        /// <summary>
    /// Task to convert a ZT1 Graphic file to one or more PNG files.
    /// </summary>
    /// <param name="StrSourceFileName">Filename of ZT1 Graphic</param>
        public static void ConvertFileZT1ToPNG(string StrSourceFileName)
        {

            // It will first render the ZT1 Graphic and then it will export it to a set of PNG files.
            // Warning: do NOT implement a clean up of files here (ZT1 Graphic/ZT1 Color Palette).
            // Reason: The color palette could be shared with other images, which would cause issues during a batch conversion!

            MdlSettings.BlnTaskRunning = true;
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
            /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBg' at character 3681


            Input:

                    ' It will first render the ZT1 Graphic and then it will export it to a set of PNG files.
                    ' Warning: do NOT implement a clean up of files here (ZT1 Graphic/ZT1 Color Palette).
                    ' Reason: The color palette could be shared with other images, which would cause issues during a batch conversion!

                    On Error GoTo dBg

             */
            MdlZTStudio.Trace("MdlTasks", "ConvertFileZT1ToPNG", "Convert ZT1 to PNG: " + StrSourceFileName);
        5:
            ;

            // Create a new instance of a ZT1 Graphic object.
            var ObjGraphic = new ClsGraphic(null);

            // Read the ZT1 Graphic
            ObjGraphic.Read(StrSourceFileName);

        // Render the set of frames within this ZT1 Graphic.
        // There are some options when exporting.
        // - canvas size options
        // - render background frame or export it separately

        10:
            ;

            // Loop over each frame of the ZT1 Graphic
            foreach (ClsFrame ObjFrame in ObjGraphic.Frames)
            {
            11:
                ;


                // The bitmap's save function does not overwrite, nor warn that the file already exists.
                // So it is safer to delete any existing files.
                File.Delete(StrSourceFileName + MdlSettings.Cfg_Convert_FileNameDelimiter + (ObjGraphic.Frames.IndexOf(ObjFrame) + MdlSettings.Cfg_Convert_StartIndex).ToString("0000") + ".png");

                // Save frames as PNG, just autonumber the frames.
                // Exception: if there is an extra frame which should be rendered separately rather than as background. 
                // In that case, output a .PNG-file named <graphicname>_extra.png
                // Since this is a batch process, (currently) not offering the option to render a background ZT1 Graphic.
                // This might however make a nice addition :)

                // RenderBGFrame: this is read as: 'render this as BG for every frame'
                if (MdlSettings.Cfg_Export_PNG_RenderBGFrame == 0 & ObjGraphic.HasBackgroundFrame == 1)
                {
                    if (ObjGraphic.Frames.IndexOf(ObjFrame) == ObjGraphic.Frames.Count - 1)
                    {
                        ObjFrame.SavePNG(StrSourceFileName + MdlSettings.Cfg_Convert_FileNameDelimiter + "extra.png");
                    }
                    else
                    {
                        ObjFrame.SavePNG(StrSourceFileName + MdlSettings.Cfg_Convert_FileNameDelimiter + (ObjGraphic.Frames.IndexOf(ObjFrame) + MdlSettings.Cfg_Convert_StartIndex).ToString("0000") + ".png");
                    }
                }
                else
                {
                    ObjFrame.SavePNG(StrSourceFileName + MdlSettings.Cfg_Convert_FileNameDelimiter + (ObjGraphic.Frames.IndexOf(ObjFrame) + MdlSettings.Cfg_Convert_StartIndex).ToString("0000") + ".png");
                }

                // Experimental. Export info such as offsets, height, width, mystery bytes...
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(MdlSettings.Cfg_Convert_Write_Graphic_Data_To_Text_File, 1, false)))
                {
                    MdlZTStudio.Trace("MdlTasks", "ConvertFileZT1ToPNG", "Export graphic details to text file...");
                    ObjFrame.WriteDetailsToTextFile();
                }
            }

        13:
            ;
            MdlZTStudio.Trace("MdlTasks", "ConvertFileZT1ToPNG", "Conversion finished.");
            MdlSettings.BlnTaskRunning = false;

            // Paint job
            Application.DoEvents();
            return;
        dBg:
            ;
            string StrErrorMessage = "An error occurred while converting a ZT1 Graphics file to PNG files:" + Constants.vbCrLf + StrSourceFileName;
            MdlZTStudio.HandledError("MdlTasks", "ConvertFileZT1ToPNG", StrErrorMessage, false, Information.Err());
            MdlSettings.BlnTaskRunning = false;
        }

        /// <summary>
    /// Task to convert one or more PNG files to one ZT1 Graphic
    /// </summary>
    /// <param name="StrDestinationFileName"></param>
    /// <param name="BlnSingleConversion"></param>
        public static void ConvertFilePNGToZT1(string StrDestinationFileName, bool BlnSingleConversion = true)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
            /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBg' at character 7671


            Input:

                    On Error GoTo dBg

             */
            MdlSettings.BlnTaskRunning = true;

        // Get the name(s) of the PNG file(s) that will be combined into the ZT1 Graphic.
        // Find out what the final name of the ZT1 Graphic will be.
        // Note: Cleanup of .PNG files only happens automatically in batch conversions (if enabled in Settings)

        0:
            ;

            // Convert to lower (force similar filenames everywhere)
            StrDestinationFileName = Strings.LCase(StrDestinationFileName);
            string StrPathDir = Path.GetDirectoryName(StrDestinationFileName); // Gets the path where the graphic is stored
            string[] LstPNGFiles; // Will be used to build a list of the filenames of all the frames (PNG set)
            var ObjGraphic = new ClsGraphic(null);
            ClsFrame ObjFrame;
            string StrGraphicName = Path.GetFileName(StrDestinationFileName);
            string StrFrameGraphicPath = Strings.Left(StrDestinationFileName, StrDestinationFileName.Length - StrGraphicName.Length);
            string StrErrorMessage; // For error details
            string StrPngName;
        10:
            ;
            MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Path: " + StrFrameGraphicPath);
            MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Graphic name: " + StrGraphicName);

            // Get the entire list of .PNG files matching the naming convention for this graphic.
            // Any filename not matching this pattern is irrelevant to process.
            LstPNGFiles = Directory.GetFiles(StrFrameGraphicPath, StrGraphicName + MdlSettings.Cfg_Convert_FileNameDelimiter + "????.png");
        11:
            ;

            // Check if files match the expected pattern, so far
            int IntIndex = 0;
            foreach (string StrPNGFile in LstPNGFiles)
            {
                if ((StrPNGFile.ToLower() ?? "") != ((StrFrameGraphicPath + StrGraphicName + MdlSettings.Cfg_Convert_FileNameDelimiter + (IntIndex + MdlSettings.Cfg_Convert_StartIndex).ToString("0000") + ".png").ToLower() ?? ""))
                {
                    StrErrorMessage = "The numbering in the PNG file(s) does not seem to be consecutive." + Constants.vbCrLf + "Your settings specify that the first PNG file should be " + StrGraphicName + MdlSettings.Cfg_Convert_FileNameDelimiter + MdlSettings.Cfg_Convert_StartIndex.ToString("0000") + " .png" + Constants.vbCrLf + "Avoid storing any other PNG files in the directory (except for " + StrGraphicName + MdlSettings.Cfg_Convert_FileNameDelimiter + "extra.png if required).";
                    MdlZTStudio.HandledError("MdlTasks", "ConvertFilePNGToZT1", StrErrorMessage, false);
                    return;
                }

                IntIndex += 1;
            }

        20:
            ;


            // Now if there is a background frame (ends in extra.png), add this as well.
            if (File.Exists(StrFrameGraphicPath + StrGraphicName + MdlSettings.Cfg_Convert_FileNameDelimiter + "extra.png") == true)
            {
                LstPNGFiles.Append(StrFrameGraphicPath + StrGraphicName + MdlSettings.Cfg_Convert_FileNameDelimiter + "extra.png");
                ObjGraphic.HasBackgroundFrame = 1;
            }

        21:
            ;

            // There should be at least two frames if a background frame is specified
            if (ObjGraphic.HasBackgroundFrame == 1)
            {
                if (LstPNGFiles.Count() == 1)
                {
                    MdlZTStudio.HandledError("MdlTasks", "ConvertFilePNGToZT1", "A ZT1 Graphic needs at least one frame, if a background frame (extra.png) is specified.", false, null);
                    return;
                }
            }

        100:
            ;
            foreach (string StrPNGFile in LstPNGFiles)
            {
            105:
                ;

                // Extract the index of the frame (or _extra) from the filename
                if (Strings.Right(Path.GetFileName(StrPNGFile).ToLower(), 9) == "extra.png")
                {
                    StrPngName = "extra";
                }
                else
                {
                    StrPngName = Strings.Right(Path.GetFileNameWithoutExtension(StrPNGFile), 4);
                }

            120:
                ;
                if (StrPngName == "extra")
                {
                    // There's an extra background frame.
                    ObjGraphic.HasBackgroundFrame = 1;
                }

            200:
                ;
                ObjFrame = new ClsFrame(ObjGraphic);
            201:
                ;

            // In case of a batch conversion, it's possible a shared color palette (.pal) is enforced.
            // usually, this would be something like this:
            // objects/restrant/restrant.pal
            // animals/ibex/ibex.pal 

            // To make it a bit more simple for the users of ZT Studio and to allow for easier recoloring 
            // (for example: lighter graphics of Red Panda will be used for the female), 
            // it would be better if the palette is not under animals/redpanda/redpanda.pal but animals/redpanda/m/redpanda.pal
            // This should work for fences etc as well.

            202:
                ;
                if (MdlSettings.Cfg_Convert_SharedPalette == 1 & BlnSingleConversion == false)
                {

                    // 20170513: changed behavior for even more flexibility. 
                    // ZT Studio tries to detect a color palette:
                    // - in the same folder as the graphic (animals/redpanda/m/walk - walk.pal) - in case this animation uses colors not used anywhere else.
                    // - in the folder one level up (animals/redpanda/m - m.pal) - in case a palette is shared for the gender (male, female, young)
                    // - in the folder two levels up (animals/redpanda - redpanda.pal) - in case a palette is shared for (most of) the animal
                    // This method should also work just fine for objects.

                    string StrPath0;
                    string StrPath1;
                    string StrPath2;
                    StrPath0 = Path.GetDirectoryName(StrPathDir);
                    StrPath1 = Path.GetDirectoryName(StrPath0);
                    StrPath2 = Path.GetDirectoryName(StrPath1);

                    // Basically the filename also reflects the name of the folder the graphic is in.
                    // Using .NETs Path.GetFileName() method, the last part of the directory derived above is retrieved and appended.
                    // Only thing missing for a full filename, is the extension (see below)
                    StrPath0 = StrPath0 + @"\" + Path.GetFileName(StrPath0);
                    StrPath1 = StrPath1 + @"\" + Path.GetFileName(StrPath1);
                    StrPath2 = StrPath2 + @"\" + Path.GetFileName(StrPath2);

                    // The current graphic should not be the only view (icon etc) in this processed folder.
                    // If it does seem to be the only view (for instance an icon/graphic 'N'), this method should NOT fall back on higher level.
                    // An icon is NOT animated and often contains very different colors (plaque, icon in menu) than the actual animations.
                    // An exception to this rule could be the list icon, but it's not worth making an exception for it in this code.
                    // One way to find out, is if there are any other PNG files in this folder and not just for this particular graphic.
                    if (LstPNGFiles.Count() != Directory.GetFiles(StrPathDir, "*.png").Count())
                    {

                        // 20170502 Optimized by Hendrix.
                        var StrColorPaletteFileNamesWithoutExt = new string[] { StrPath0, StrPath1, StrPath2 };
                        var StrExtensions = new string[] { ".pal", ".gpl", ".png" };

                        // No palette has been saved/set yet for this graphic.
                        if (string.IsNullOrEmpty(ObjGraphic.FileName))
                        {

                            // Figure out if there is a preferred palette (perhaps already prepared by the user) to be used.
                            // Two ideas come to mind here:
                            // 
                            // (1) Palette at deeper level folder gets priority over palette in higher level folder
                            // For example: an animal might use one palette for nearly all animations, except one
                            // 
                            // (2) Palette of certain type (file extension) gets priority over another one.
                            // Order: .pal(ZT1 Graphic) > .gpl (GIMP Palette) > .png

                            MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Batch conversion and shared palette = 1. Trying to find existing palette.");
                            do
                            {
                                bool exitFor3 = false;
                                bool exitFor4 = false;
                                bool exitFor5 = false;
                                foreach (string StrColorPaletteFileNameWithoutExt in StrColorPaletteFileNamesWithoutExt)
                                {
                                    bool exitFor = false;
                                    bool exitFor1 = false;
                                    bool exitFor2 = false;
                                    foreach (string StrExtension in StrExtensions)
                                    {
                                        if (File.Exists(StrColorPaletteFileNameWithoutExt + StrExtension) == true)
                                        {
                                            {
                                                var withBlock = ObjGraphic.ColorPalette;
                                                // Read a new palette once
                                                // Ignore different extensions, so reloading within the loop is skipped

                                                // Set filename.
                                                withBlock.FileName = StrColorPaletteFileNameWithoutExt + ".pal";

                                                // Now go by priority.
                                                // Go-to is usually a bad practice, but it's good here to break out of the 2 (!) loops.
                                                bool exitSelect = false;
                                                bool exitSelect1 = false;
                                                bool exitSelect2 = false;
                                                switch (StrExtension ?? "")
                                                {
                                                    case ".pal":
                                                        {
                                                            withBlock.ReadPal(withBlock.FileName);
                                                            exitFor3 = exitFor = exitSelect = true;
                                                            break;
                                                        }

                                                    case ".gpl":
                                                        {
                                                            withBlock.ImportFromGIMPPalette(StrColorPaletteFileNameWithoutExt + StrExtension);
                                                            withBlock.WritePal(withBlock.FileName, true);
                                                            exitFor4 = exitFor1 = exitSelect1 = true;
                                                            break;
                                                        }

                                                    case ".png":
                                                        {
                                                            withBlock.ImportFromPNG(StrColorPaletteFileNameWithoutExt + StrExtension);
                                                            withBlock.WritePal(withBlock.FileName, true);
                                                            exitFor5 = exitFor2 = exitSelect2 = true;
                                                            break;
                                                        }
                                                }

                                                if (exitSelect)
                                                {
                                                    break;
                                                }

                                                if (exitSelect1)
                                                {
                                                    break;
                                                }

                                                if (exitSelect2)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (exitFor)
                                    {
                                        break;
                                    }

                                    if (exitFor1)
                                    {
                                        break;
                                    }

                                    if (exitFor2)
                                    {
                                        break;
                                    }
                                }

                                if (exitFor3)
                                {
                                    break;
                                }

                                if (exitFor4)
                                {
                                    break;
                                }

                                if (exitFor5)
                                {
                                    break;
                                }

                                // Todo: does this lead to issues?
                                MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Warning: no shared palette found.");
                                MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Procedure will continue and use specific stand-alone palette.");
                            }
                            while (false);
                        }
                        else
                        {
                            // Color palette has already been set for this graphic.
                            // No further action needed.
                            MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Skip. Specific color stand-alone palette defined.");
                        }
                    }
                }

            245:
                ;

                // Add this frame to the graphic's frame collection 
                ObjGraphic.Frames.Add(ObjFrame);
            250:
                ;

                // Create a frame from the .PNG-file
                ObjFrame.LoadPNG(StrPNGFile);
            }

        1530:
            ;
            MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Write graphic...");

            // Create the ZT1 Graphic. 
            ObjGraphic.Write(StrDestinationFileName);
        1555:
            ;
            if (MdlSettings.Cfg_Export_ZT1_Ani == 1 & BlnSingleConversion == true)
            {
                MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Generate .ani file");

                // Only 1 graphic file is being generated (example: icon)
                // A .ani-file can be generated automatically.       
                // [folder path] + \ + [folder name] + .ani
                var ObjAniFile = new ClsAniFile(StrPathDir + @"\" + Path.GetFileName(StrPathDir) + ".ani");
                ObjAniFile.CreateAniConfig();
            }

            MdlZTStudio.Trace("MdlTasks", "ConvertFilePNGToZT1", "Converted PNG-set to ZT1 Graphic");
        9999:
            ;

            // Clear everything.
            ObjGraphic = null;
            MdlSettings.BlnTaskRunning = false;
            return;
        dBg:
            ;
            MdlZTStudio.UnhandledError("MdlTasks", "ConvertFolderPNGToZT1", Information.Err(), true);
        }

        /// <summary>
    /// Task to convert a whole set of folders containing ZT1 Graphics to PNG sets
    /// </summary>
    /// <param name="StrPath">Path to search recursively for ZT1 Graphics</param>
    /// <param name="ObjProgressBar">Progress bar to show progress in</param>
        public static void ConvertFolderZT1ToPNG(string StrPath, ProgressBar ObjProgressBar = null)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 21627


        Input:

                On Error GoTo dBug

         */
        0:
            ;

            // Create a recursive list of files

            // This list stores the results.
            var LstResult = new List<string>();

            // This stack stores the directories to process.
            var Stack = new Stack<string>();

            // Add the initial directory
            Stack.Push(StrPath);
        10:
            ;

            // Continue processing for each stacked directory
            while (Stack.Count > 0)
            {
            // Get top directory string

            15:
                ;
                string StrDirectoryName = Stack.Pop();
            20:
                ;
                foreach (string StrFileName in Directory.GetFiles(StrDirectoryName, "*"))
                {
                    // Only ZT1 files
                    if (string.IsNullOrEmpty(Path.GetExtension(StrFileName)))
                    {
                        LstResult.Add(StrFileName);
                    }
                }

            // Loop through all subdirectories and add them to the stack.
            25:
                ;
                foreach (var StrSubDirectoryName in Directory.GetDirectories(StrDirectoryName))
                    Stack.Push(StrSubDirectoryName);
            }

            // Set the initial configuration for a (optional) progress bar.
            // Max value should be the number of ZT1 Graphics found.
            if (Information.IsNothing(ObjProgressBar) == false)
            {
                ObjProgressBar.Minimum = 0;
                ObjProgressBar.Value = 0;
                ObjProgressBar.Maximum = LstResult.Count;
            }

        1000:
            ;

            // For each file that is a ZT1 Graphic:
            foreach (string StrZT1GraphicFileName in LstResult)
            {
                ConvertFileZT1ToPNG(StrZT1GraphicFileName);
                if (Information.IsNothing(ObjProgressBar) == false)
                {
                    ObjProgressBar.Value += 1;
                }
            }

        1050:
            ;

            // Clean up original ZT1 Graphic files? (includes palette, does not include .ani file for now!)
            if (MdlSettings.Cfg_Convert_DeleteOriginal == 1)
            {
                // Currently clean up of ZT1 Graphics and ZT1 Color palettes is called seperately.
                // It might be possible to merge them at some point and you could even gain a small performance boost.
                CleanUpFiles(StrPath, "");
                CleanUpFiles(StrPath, ".pal");
            }

            return;
        dBug:
            ;
            MdlZTStudio.HandledError("MdlTasks", "ConvertFolderZT1ToPNG", "Unexpected error occurred.", true, Information.Err());
        }

        /// <summary>
    /// Task to convert files in a folder (recursively) from PNG sets to ZT1 Graphics
    /// </summary>
    /// <param name="StrSourcePath">Folder (recursive) containing PNG sets</param>
    /// <param name="ObjProgressBar">ProgressBar</param>
        public static void ConvertFolderPNGToZT1(string StrSourcePath, ProgressBar ObjProgressBar = null)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 24830


        Input:

                On Error GoTo dBug

         */
        0:
            ;
        5:
            ;

            // Create a recursive list.

            // This list stores the results.
            var LstFiles = new List<string>();

            // This stack stores the directories to process.
            var StackDirectories = new Stack<string>();

            // Longer error messages
            string StrErrorMessage;

            // Add the initial directory
            StackDirectories.Push(StrSourcePath);
        10:
            ;


            // Continue processing for each stacked directory
            while (StackDirectories.Count > 0)
            {
            // Get top directory string

            15:
                ;
                string StrDirectory = StackDirectories.Pop();
                string StrGraphicName;

            // Add all immediate file paths 

            20:
                ;
                foreach (var StrFileName in Directory.GetFiles(StrDirectory, "*.png"))
                {

                    // Add future graphic name ("full" path, eg animals/redpanda/m/walk/NE)
                    if ((Strings.Right(Path.GetFileNameWithoutExtension(StrFileName).ToLower(), 5 + Strings.Len(MdlSettings.Cfg_Convert_FileNameDelimiter)) ?? "") == (MdlSettings.Cfg_Convert_FileNameDelimiter + "extra" ?? ""))
                    {
                        // 5 (extra) + 4 (.png) + x (delimiter) = 9 + x characters.
                        // eg objects/yourobj/NE_extra.png 
                        StrGraphicName = Strings.Left(StrFileName, Strings.Len(StrFileName) - 9 - Strings.Len(MdlSettings.Cfg_Convert_FileNameDelimiter));
                    }
                    else
                    {
                        // 4 (0000) + 4 (.png) = 8 chars. 
                        // eg objects/yourobj/NE_0001.png 
                        StrGraphicName = Strings.Left(StrFileName, Strings.Len(StrFileName) - 8 - Strings.Len(MdlSettings.Cfg_Convert_FileNameDelimiter));
                    }

                    if (LstFiles.Contains(StrGraphicName) == false)
                    {
                        LstFiles.Add(StrGraphicName);
                    }
                }

            // Loop through all subdirectories and add them to the stack.
            25:
                ;
                foreach (var StrDirectoryName in Directory.GetDirectories(StrDirectory))
                {

                    // Just a warning, so users don't accidentally have "sitscratch" as animation name.
                    // Actually '-' is supported as well.
                    if (Path.GetFileName(StrDirectoryName).Length > 8 | System.Text.RegularExpressions.Regex.IsMatch(Strings.Replace(Path.GetFileName(StrDirectoryName), "-", ""), "^[a-zA-Z0-9_-]+$") == false)
                    {
                        StrErrorMessage = "Directory name '" + Path.GetFileName(StrDirectoryName) + "' is invalid." + Constants.vbCrLf + "The limit of a folder name is a maximum of 8 alphanumeric characters." + Constants.vbCrLf + "You will need to rename the folder manually and then retry.";
                        MdlZTStudio.HandledError("MdlTasks", "ConvertFolderPNGToZT1", StrErrorMessage, true, Information.Err());
                    }

                    StackDirectories.Push(StrDirectoryName);
                }
            }

        101:
            ;
            if (Information.IsNothing(ObjProgressBar) == false)
            {
                ObjProgressBar.Minimum = 0;
                ObjProgressBar.Value = 0;
                ObjProgressBar.Maximum = LstFiles.Count;
            }

        1000:
            ;

            // For each file that is a ZT1 Graphic:
            foreach (string StrDestinationGraphicName in LstFiles)
            {
                ConvertFilePNGToZT1(StrDestinationGraphicName, false);
                if (Information.IsNothing(ObjProgressBar) == false)
                {
                    ObjProgressBar.Value += 1;
                }

                Application.DoEvents();
            }

        1100:
            ;

            // Generate a .ani-file in each directory. 
            // Add the initial directory
            MdlBatch.WriteAniFile(StrSourcePath);
        1150:
            ;

            // Do a clean up of .PNG files if conversion was successful and setting is enabled
            if (MdlSettings.Cfg_Convert_DeleteOriginal == 1)
            {
                CleanUpFiles(StrSourcePath, ".png");
            }

            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError("MdlTasks", "ConvertFolderPNGToZT1", Information.Err(), true);
        }


        /// <summary>
    /// Saves the main graphic as a ZT1 Graphic file (simple, using UI)
    /// Saves as the specified filename.
    /// </summary>
    /// <param name="StrFileName">Filename</param>
        public static void SaveGraphic(string StrFileName)
        {

            // 20150624. Assume having <filename>.pal here. 
            // This was done to avoid issues with shared color palettes, if users are NOT familiar with them.
            // Pro users will only tweak and use the batch conversion.
            {
                ref var withBlock = ref MdlSettings.EditorGraphic;
                withBlock.FileName = StrFileName;
                withBlock.ColorPalette.FileName = MdlSettings.EditorGraphic.FileName + ".pal";
                withBlock.Write(StrFileName, true);
            }

        50:
            ;
            if (MdlSettings.Cfg_Export_ZT1_Ani == 1)
            {
                MdlZTStudio.Trace("MdlTasks", "SaveGraphic", "Try .ani");
                // Get the folder + name of the folder + .ani
                var CAni = new ClsAniFile(Path.GetDirectoryName(StrFileName) + @"\" + Path.GetFileName(Path.GetDirectoryName(StrFileName)) + ".ani");
                CAni.CreateAniConfig();
            }

        60:
            ;
            My.MyProject.Forms.FrmMain.ssFileName.Text = DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": saved " + StrFileName;
        }



        /// <summary>
    /// Batch rotation fixes all animations in a selected folder.
    /// This sub will find all ZT1 Graphics in the folder and adjust the offsets of each frame.
    ///  
    /// It's especially useful when importing frames from another program, such as Blender, and the user sees the animal should just be a bit more central (up/down).
    /// </summary>
    /// <param name="StrPath">Path to folder</param>
    /// <param name="PntOffset">The offsets to apply</param>
    /// <param name="ObjProgressBar">The bar which will indicate progress</param>

        // Todo: check needed to see if strPath is subfolder of Cfg_Path_Root ?


        public static void BatchOffsetFixFolderZT1(string StrPath, Point PntOffset, ProgressBar ObjProgressBar = null)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 32065


        Input:

                ' Todo: check needed to see if strPath is subfolder of Cfg_Path_Root ?


                On Error GoTo dBug

         */
        0:
            ;


            // Creating a recursive file list.

            // This list stores the results.
            var LstFiles = new List<string>();

            // This stack stores the directories to process.
            var StackDirectories = new Stack<string>();

            // Add the initial directory
            StackDirectories.Push(StrPath);
        10:
            ;


            // Continue processing for each stacked directory
            while (StackDirectories.Count > 0)
            {
            // Get top directory string

            15:
                ;
                string StrDirectory = StackDirectories.Pop();
            20:
                ;
                foreach (string strFile in Directory.GetFiles(StrDirectory, "*"))
                {
                    // Only ZT1 files
                    if (string.IsNullOrEmpty(Path.GetExtension(strFile)))
                    {
                        LstFiles.Add(strFile);
                    }
                }

            // Loop through all subdirectories and add them to the stack.
            25:
                ;
                foreach (var StrSubDirectoryName in Directory.GetDirectories(StrDirectory))
                    StackDirectories.Push(StrSubDirectoryName);
            }

            // Set the initial configuration for a (optional) progress bar.
            // The max value should be the number of ZT1 Graphics
            if (Information.IsNothing(ObjProgressBar) == false)
            {
                ObjProgressBar.Minimum = 0;
                ObjProgressBar.Value = 0;
                ObjProgressBar.Maximum = LstFiles.Count;
            }

        1000:
            ;

            // For each file that is a ZT1 Graphic:
            foreach (string StrCurrentFile in LstFiles)
            {
                MdlZTStudio.Trace("MdlTasks", "BatchOffsetFixFolderZT1", "Processing file " + StrCurrentFile);

                // Read graphic, update offsets of frames, save.
                var ObjGraphic = new ClsGraphic(null);
            1100:
                ;
                ObjGraphic.Read(StrCurrentFile);
            1105:
                ;
                ObjGraphic.Frames[0].UpdateOffsets(PntOffset, true);
            1110:
                ;
                ObjGraphic.Write(StrCurrentFile);
                if (Information.IsNothing(ObjProgressBar) == false)
                {
                    ObjProgressBar.Value += 1;
                }
            }

        1200:
            ;

            // Generate a .ani-file in each directory. 
            // Add the initial directory
            MdlBatch.WriteAniFile(StrPath);
        1950:
            ;
            MdlZTStudio.InfoBox("MdlTasks", "BatchOffsetFixFolderZT1", "Finished batch rotation fixing.");
            return;
        dBug:
            ;
            MdlZTStudio.HandledError("MdlTasks", "BatchOffsetFixFolderZT1", "Unexpected error.", false, null);
        }
    }
}