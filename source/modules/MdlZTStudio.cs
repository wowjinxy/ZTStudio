using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ZTStudio
{
    /// <summary>
/// Handles various tasks related to the program
/// </summary>
    static class MdlZTStudio
    {


        /// <summary>
        /// Loads settings
        /// Processes command line parameters
        /// </summary>
        public static void StartUp()
        {
            try
            {
                MdlConfig.Load();

                // Configure parameters.
                string strArgAction = string.Empty;
                string strArgActionValue = string.Empty;

                // Skipping the first argument as it's the executable path
                var args = Environment.GetCommandLineArgs().Skip(1);

                foreach (string arg in args)
                {
                    Debug.Print(arg);

                    string[] parts = arg.ToLower().Split(new[] { ':' }, 2);
                    string argKey = parts[0];
                    string argValue = parts.Length > 1 ? parts[1] : string.Empty;

                    ProcessArgument(argKey, argValue, ref strArgAction, ref strArgActionValue);
                }

                // Execute action if specified
                ExecuteAction(strArgAction, strArgActionValue);
            }
            catch (Exception ex)
            {
                UnhandledError("MdlZTStudio", "StartUp", ex, true);
            }
        }

        private static void ProcessArgument(string argKey, string argValue, ref string strArgAction, ref string strArgActionValue)
        {
            switch (argKey)
            {
                case "/preview.bgcolor":
                    MdlSettings.Cfg_Grid_BackGroundColor = Color.FromArgb(Convert.ToInt32(argValue));
                    break;

                case "/preview.fgcolor":
                    MdlSettings.Cfg_Grid_ForeGroundColor = Color.FromArgb(Convert.ToInt32(argValue));
                    break;

                case "/preview.zoom":
                    MdlSettings.Cfg_Grid_Zoom = Convert.ToInt32(argValue);
                    break;

                case "/preview.footprintX":
                    MdlSettings.Cfg_Grid_FootPrintX = Convert.ToByte(argValue);
                    break;

                case "/preview.footprinty":
                    MdlSettings.Cfg_Grid_FootPrintY = Convert.ToByte(argValue);
                    break;

                // Paths
                case "/paths.root":
                    MdlSettings.Cfg_Path_Root = argValue;
                    break;

                // Export options
                case "/exportoptions.pngcrop":
                    MdlSettings.Cfg_Export_PNG_CanvasSize = Convert.ToByte(argValue);
                    break;

                case "/exportoptions.pngrenderextraframe":
                    MdlSettings.Cfg_Export_PNG_RenderBGFrame = Convert.ToByte(argValue);
                    break;

                case "/exportoptions.pngrenderextragraphic":
                    MdlSettings.Cfg_Export_PNG_RenderBGZT1 = Convert.ToByte(argValue);
                    break;

                case "/exportoptions.pngrendertransparentbg":
                    MdlSettings.Cfg_Export_PNG_TransparentBG = Convert.ToByte(argValue);
                    break;

                case "/exportoptions.zt1alwaysaddztafbytes":
                    MdlSettings.Cfg_Export_ZT1_AlwaysAddZTAFBytes = Convert.ToByte(argValue);
                    break;

                case "/exportoptions.zt1ani":
                    MdlSettings.Cfg_Export_ZT1_Ani = Convert.ToByte(argValue);
                    break;

                // Conversion options
                case "/conversionoptions.deleteoriginal":
                    MdlSettings.Cfg_Convert_DeleteOriginal = Convert.ToByte(argValue);
                    break;

                case "/conversionoptions.filenamedelimiter":
                    // Assuming this should be a string, not a byte
                    MdlSettings.Cfg_Convert_FileNameDelimiter = argValue;
                    break;

                case "/conversionoptions.overwrite":
                    MdlSettings.Cfg_Convert_Overwrite = Convert.ToByte(argValue);
                    break;

                case "/conversionoptions.pngfilesindex":
                    MdlSettings.Cfg_Convert_StartIndex = Convert.ToByte(argValue);
                    break;

                case "/conversionoptions.sharedpalette":
                    MdlSettings.Cfg_Convert_SharedPalette = Convert.ToByte(argValue);
                    break;

                // Editing options
                case "/editing.animationspeed":
                    MdlSettings.Cfg_Frame_DefaultAnimSpeed = Convert.ToInt32(argValue);
                    break;

                case "/editing.individualrotationfix":
                    MdlSettings.Cfg_Editor_RotFix_IndividualFrame = Convert.ToByte(argValue);
                    break;

                // Not remembered but can be supplied
                case "/extra.colorquantization":
                    MdlSettings.Cfg_Palette_Quantization = Convert.ToByte(argValue);
                    break;

                // Actions
                case "/action.convertfolder.topng":
                    strArgAction = "convertfolder.topng";
                    strArgActionValue = argValue;
                    break;

                case "/action.convertfolder.tozt1":
                    strArgAction = "convertfolder.tozt1";
                    strArgActionValue = argValue;
                    break;

                case "/action.convertfile.topng":
                    strArgAction = "convertfile.topng";
                    strArgActionValue = argValue;
                    break;

                case "/action.convertfile.tozt1":
                    strArgAction = "convertfile.tozt1";
                    strArgActionValue = argValue;
                    break;

                case "/action.listhashes":
                    strArgAction = "listhashes";
                    strArgActionValue = argValue;
                    break;

                case "/action.saveconfig":
                    strArgAction = "saveconfig";
                    strArgActionValue = argValue;
                    break;
            }
        }

        private static void ExecuteAction(string strArgAction, string strArgActionValue)
        {
            switch (strArgAction)
            {
                case "convertfile.topng":
                    // Do conversion.
                    // Then exit.
                    MdlTasks.ConvertFileZT1ToPNG(strArgActionValue);
                    Application.DoEvents();
                    Environment.Exit(0);
                    break;

                case "convertfile.tozt1":
                    // Do conversion.
                    // Then exit.
                    MdlTasks.ConvertFilePNGToZT1(strArgActionValue);
                    Application.DoEvents();
                    Environment.Exit(0);
                    break;

                case "convertfolder.topng":
                    // Do conversion.
                    // Then exit.
                    MdlTasks.ConvertFolderZT1ToPNG(strArgActionValue);
                    Application.DoEvents();
                    Environment.Exit(0);
                    break;

                case "convertfolder.tozt1":
                    // Do conversion.
                    // Then exit.
                    MdlTasks.ConvertFolderPNGToZT1(strArgActionValue);
                    Application.DoEvents();
                    Environment.Exit(0);
                    break;

                case "listhashes":
                    MdlTests.GetHashesOfFilesInFolder(strArgActionValue, strArgActionValue + @"\hashes.cfg");
                    Application.DoEvents();
                    Environment.Exit(0);
                    break;

                case "saveconfig":
                    if (Convert.ToDouble(strArgActionValue) == 1d)
                    {
                        MdlConfig.Write();
                        Application.DoEvents();
                        Environment.Exit(0);
                    }
                    break;
            }

        }

        public class ZTStudioException : Exception
        {
            // Define your custom exception class here
            public ZTStudioException(string strClass, string strMethod, Exception ex)
                : base($"Error in {strClass}::{strMethod} - {ex.Message}", ex)
            {
            }
        }

        public static void UnhandledError(string strClass, string strMethod, Exception ex, bool blnRaiseException)
        {
            Trace(strClass, strMethod, "Unexpected error occurred in " + strClass + "::" + strMethod + "()");

            string strMessage = $"Sorry, but an unexpected error occurred in {strClass}::{strMethod}.\nError: {ex.Message}\n\n------------------------------------\nAs a precaution, {Application.ProductName} will close.\nIf you can repeat this error, feel free to report it at {MdlSettings.Cfg_GitHub_URL}.\nAdd as many details (steps to reproduce) as possible, include relevant files in your report.";

            MessageBox.Show(strMessage, "Unexpected error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (blnRaiseException)
            {
                throw new ZTStudioException(strClass, strMethod, ex);
            }

            Environment.Exit(0);
        }

        public static void HandledError(string strClass, string strMethod, string strMessage, bool blnFatal = false, Exception ex = null)
        {
            if (blnFatal)
            {
                strMessage += $"\n\nSince this error may lead to other issues, {Application.ProductName} will now close completely.";
            }

            // Tracing info was provided
            if (ex != null)
            {
                Trace(strClass, strMethod, "Expected error occurred in " + strClass + "::" + strMethod + "()");
            }

            MessageBox.Show(strMessage, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (blnFatal)
            {
                Environment.Exit(0);
            }
        }

        public static void Trace(string strClass, string strMethod, string strMessage)
        {
            if (MdlSettings.Cfg_Trace == 1)
            {
                Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {strClass}::{strMethod}(): {strMessage}");
            }
        }

        public static void InfoBox(string strClass, string strMethod, string strMessage)
        {
            Trace(strClass, strMethod, "Information shown by " + strClass + "::" + strMethod + "()");
            MessageBox.Show(strMessage, "ZT Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}