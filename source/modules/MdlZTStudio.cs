using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

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
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 240


        Input:

                On Error GoTo dBug

         */
        10:
            ;

            // Load the initial config. 
            // settings.cfg contains the default values.
            // Some parameters can be overwritten by the command line parameters; but they are not stored permanently.
            MdlConfig.Load();
        20:
            ;


            // Configure parameters.
            string StrArgAction = Constants.vbNullString;
            string StrArgActionValue = Constants.vbNullString;
            string argK;
            string argV;
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                Debug.Print(arg);

                // Arguments are specified as:  ZTStudio.exe /arg1:<val1> /argN:<valN>
                // Expecting valid arguments.
                argK = Strings.Split(arg.ToLower() + ":", ":")[0];
                argV = Strings.Replace(arg, argK + ":", "", Compare: CompareMethod.Text);
            25:
                ;

                // set arguments etc
                switch (argK ?? "")
                {

                    // These are actual settings.
                    // If specified; they take priority over the values defined in settings.cfg

                    // Preview
                    case "/preview.bgcolor":
                        {
                            MdlSettings.Cfg_Grid_BackGroundColor = Color.FromArgb(Conversions.ToInteger(argV));
                            break;
                        }

                    case "/preview.fgcolor":
                        {
                            MdlSettings.Cfg_Grid_ForeGroundColor = Color.FromArgb(Conversions.ToInteger(argV));
                            break;
                        }

                    case "/preview.zoom":
                        {
                            MdlSettings.Cfg_Grid_Zoom = Conversions.ToInteger(argV);
                            break;
                        }

                    case "/preview.footprintX":
                        {
                            MdlSettings.Cfg_Grid_FootPrintX = Conversions.ToByte(argV);
                            break;
                        }

                    case "/preview.footprinty":
                        {
                            MdlSettings.Cfg_Grid_FootPrintY = Conversions.ToByte(argV);
                            break;
                        }

                    // Paths
                    case "/paths.root":
                        {
                            MdlSettings.Cfg_Path_Root = argV;
                            break;
                        }
                    // ignore recent paths


                    // Export options
                    case "/exportoptions.pngcrop":
                        {
                            MdlSettings.Cfg_Export_PNG_CanvasSize = Conversions.ToByte(argV);
                            break;
                        }

                    case "/exportoptions.pngrenderextraframe":
                        {
                            MdlSettings.Cfg_Export_PNG_RenderBGFrame = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    case "/exportoptions.pngrenderextragraphic":
                        {
                            MdlSettings.Cfg_Export_PNG_RenderBGZT1 = Conversions.ToByte(Conversions.ToByte(argV) == 1); // this would require to supply the BG graphic. To implement.
                            break;
                        }

                    case "/exportoptions.pngrendertransparentbg":
                        {
                            MdlSettings.Cfg_Export_PNG_TransparentBG = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    case "/exportoptions.zt1alwaysaddztafbytes":
                        {
                            MdlSettings.Cfg_Export_ZT1_AlwaysAddZTAFBytes = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    case "/exportoptions.zt1ani":
                        {
                            MdlSettings.Cfg_Export_ZT1_Ani = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    // Conversion options
                    case "/conversionoptions.deleteoriginal":
                        {
                            MdlSettings.Cfg_Convert_DeleteOriginal = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    case "/conversionoptions.filenamedelimiter":
                        {
                            MdlSettings.Cfg_Convert_DeleteOriginal = Conversions.ToByte(argV);
                            break;
                        }

                    case "/conversionoptions.overwrite":
                        {
                            MdlSettings.Cfg_Convert_Overwrite = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    case "/conversionoptions.pngfilesindex":
                        {
                            MdlSettings.Cfg_Convert_StartIndex = Conversions.ToByte(argV);
                            break;
                        }

                    case "/conversionoptions.sharedpalette":
                        {
                            MdlSettings.Cfg_Convert_SharedPalette = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    // Editing options
                    case "/editing.animationspeed":
                        {
                            MdlSettings.Cfg_Frame_DefaultAnimSpeed = Conversions.ToInteger(argV);
                            break;
                        }

                    case "/editing.individualrotationfix":
                        {
                            MdlSettings.Cfg_Editor_RotFix_IndividualFrame = Conversions.ToByte(Conversions.ToByte(argV) == 1);
                            break;
                        }

                    // Not remembered but can be supplied:  
                    case "/extra.colorquantization":
                        {
                            MdlSettings.Cfg_Palette_Quantization = Conversions.ToByte(argV);
                            break;
                        }

                    // These are actions. 
                    // An action can be an automated process doing lots of stuff (e.g. convertfolder)
                    case "/action.convertfolder.topng":
                        {
                            StrArgAction = "convertfolder.topng";
                            StrArgActionValue = argV;
                            break;
                        }

                    case "/action.convertfolder.tozt1":
                        {
                            StrArgAction = "convertfolder.tozt1";
                            StrArgActionValue = argV;
                            break;
                        }

                    case "/action.convertfile.topng":
                        {
                            StrArgAction = "convertfile.topng";
                            StrArgActionValue = argV;
                            break;
                        }

                    case "/action.convertfile.tozt1":
                        {
                            StrArgAction = "convertfile.tozt1";
                            StrArgActionValue = argV;
                            break;
                        }

                    case "/action.listhashes":
                        {
                            StrArgAction = "listhashes";
                            StrArgActionValue = argV;
                            break;
                        }

                    case "/action.saveconfig":
                        {
                            StrArgAction = "saveconfig";
                            StrArgActionValue = argV;
                            break;
                        }
                }
                // Parameters?


                // Process action


            }

        30:
            ;

            // See which action was specified and only do the conversion now.
            // Users could assume the order of parameters doesn't matter, for instance:
            // ZTStudio.exe /convertFolder:<path> /ZTAF:1 -> would have been converted already while not respecting this configuration option. 
            // ZTStudio.exe /ZTAF:1 /convertFolder:<path> -> would correctly apply the configuration option.
            // Assume users are unaware and make it easy for them not to get frustrated, so only convert at the en:

            switch (StrArgAction ?? "")
            {
                case "convertfile.topng":
                    {
                        // Do conversion.
                        // Then exit.
                        MdlTasks.ConvertFileZT1ToPNG(StrArgActionValue);
                        Application.DoEvents();
                        Environment.Exit(0);
                        break;
                    }

                case "convertfile.tozt1":
                    {
                        // Do conversion.
                        // Then exit.
                        MdlTasks.ConvertFilePNGToZT1(StrArgActionValue);
                        Application.DoEvents();
                        Environment.Exit(0);
                        break;
                    }

                case "convertfolder.topng":
                    {
                        // Do conversion.
                        // Then exit.
                        MdlTasks.ConvertFolderZT1ToPNG(StrArgActionValue);
                        Application.DoEvents();
                        Environment.Exit(0);
                        break;
                    }

                case "convertfolder.tozt1":
                    {

                        // Do conversion.
                        // Then exit.
                        MdlTasks.ConvertFolderPNGToZT1(StrArgActionValue);
                        Application.DoEvents();
                        Environment.Exit(0);
                        break;
                    }

                case "listhashes":
                    {
                        MdlTests.GetHashesOfFilesInFolder(StrArgActionValue, StrArgActionValue + @"\hashes.cfg");
                        Application.DoEvents();
                        Environment.Exit(0);
                        break;
                    }

                case "saveconfig":
                    {
                        if (Conversions.ToDouble(StrArgActionValue) == 1d)
                        {
                            MdlConfig.Write();
                            Application.DoEvents();
                            Environment.Exit(0);
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
                    // Default.
                    // Just load.

            }

            return;
        dBug:
            ;
            UnhandledError("MdlZTStudio", "StartUp", Information.Err(), true);
        }



        /// <summary>
    /// To make unexpected errors look more generic, most of them are now handled by this method.
    /// </summary>
    /// <param name="StrClass">Class </param>
    /// <param name="StrMethod">Method</param>
    /// <param name="ObjError">Error object (contains number and message)</param>
    /// <param name="BlnRaiseException">Boolean</param>
        public static void UnhandledError(string StrClass, string StrMethod, ErrObject ObjError, bool BlnRaiseException)
        {
            Trace(StrClass, StrMethod, "Unexpected error occurred in " + StrClass + "::" + StrMethod + "()");
            string StrMessage = "" + "Sorry, but an unexpected error occurred in " + StrClass + "::" + StrMethod + "() at line " + ObjError.Erl.ToString() + Constants.vbCrLf + "Error code: " + ObjError.Number.ToString() + Constants.vbCrLf + ObjError.Description + Constants.vbCrLf + Constants.vbCrLf + "------------------------------------" + Constants.vbCrLf + "As a precaution, " + Application.ProductName + " will close." + Constants.vbCrLf + "If you can repeat this error, feel free to report it at " + MdlSettings.Cfg_GitHub_URL + "." + Constants.vbCrLf + "Add as many details (steps to reproduce) as possible, include relevant files in your report.";
            if (Interaction.MsgBox(StrMessage, (MsgBoxStyle)((int)MsgBoxStyle.ApplicationModal + (int)MsgBoxStyle.OkOnly + (int)MsgBoxStyle.Critical), "Unexpected error occurred") == MsgBoxResult.Ok)
            {
                Environment.Exit(0);
            }

            if (BlnRaiseException == true)
            {
                throw new ZTStudioException(StrClass, StrMethod, ObjError);
            }
        }


        /// <summary>
    /// To make expected errors look more generic, most of them are now handled by this method. Some parameters are only meant for tracing details.
    /// </summary>
    /// <param name="StrClass">Class </param>
    /// <param name="StrMethod">Method</param>
    /// <param name="StrMessage">Message</param>
    /// <param name="BlnFatal">Fatal error. Defaults to false.</param>
    /// <param name="ObjError">Error object (contains number and message). Defaults to Nothing.</param>
        public static void HandledError(string StrClass, string strMethod, string StrMessage, bool BlnFatal = false, ErrObject ObjError = null)
        {
            if (BlnFatal == true)
            {
                StrMessage = StrMessage + Constants.vbCrLf + Constants.vbCrLf + "Since this error may lead to other issues, " + Application.ProductName + " will now close completely.";
            }

            // Tracing info was provided
            if (Information.IsNothing(ObjError) == false)
            {
                Trace(StrClass, strMethod, "Expected error occurred in " + StrClass + "::" + strMethod + "()");
            }

            if (Interaction.MsgBox(StrMessage, (MsgBoxStyle)((int)MsgBoxStyle.ApplicationModal + (int)MsgBoxStyle.OkOnly + (int)MsgBoxStyle.Critical), "Error occurred") == MsgBoxResult.Ok)
            {
                if (BlnFatal == true)
                {
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
    /// To make tracing look more generic
    /// </summary>
    /// <param name="StrClass">Class</param>
    /// <param name="StrMethod">Method</param>
    /// <param name="StrMessage">Message</param>
        public static void Trace(string StrClass, string StrMethod, string StrMessage)
        {
            if (MdlSettings.Cfg_Trace == 1)
            {
                Debug.Print(DateAndTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + StrClass + "::" + StrMethod + "(): " + StrMessage);
            }
        }


        /// <summary>
    /// To make information message boxes more generic, most of them are now handled by this method. Some parameters are only meant for tracing details.
    /// </summary>
    /// <param name="StrClass">Class </param>
    /// <param name="StrMethod">Method</param>
    /// <param name="StrMessage">Message</param>
        public static void InfoBox(string StrClass, string StrMethod, string StrMessage)
        {
            Trace(StrClass, StrMethod, "Information shown by " + StrClass + "::" + StrMethod + "()");
            Interaction.MsgBox(StrMessage, (MsgBoxStyle)((int)MsgBoxStyle.Information + (int)MsgBoxStyle.ApplicationModal + (int)MsgBoxStyle.OkOnly), "ZT Studio");
        }
    }
}