using System.Drawing;
using static System.IO.File;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{


    /// <summary>
/// Contains methods related to ZT Studio's configuration
/// </summary>
    static class MdlConfig
    {


        /// <summary>
    /// Initializes the configuration settings, read from the .INI file
    /// </summary>

        // This tasks reads all settings from the .INI-file.
        // For an explanation of these parameters: check mMlSettings.vb

        public static void Load()
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 420


        Input:

                ' This tasks reads all settings from the .INI-file.
                ' For an explanation of these parameters: check mMlSettings.vb

                On Error GoTo dBug

         */
        10:
            ;
            string StrSettingsFile = System.IO.Path.GetFullPath(Application.StartupPath) + @"\settings.cfg";
            if (Exists(StrSettingsFile) == false)
            {
                string StrErrorMessage = "" + "ZT Studio is missing the settings.cfg file." + Constants.vbCrLf + "It should be in the same folder as ZTStudio.exe" + Constants.vbCrLf + Constants.vbCrLf + "Get the file at:" + Constants.vbCrLf + MdlSettings.Cfg_GitHub_URL;
                MdlZTStudio.HandledError("MdlConfig", "Load", StrErrorMessage, true, null);
            }
        // On Error Resume Next


        20:
            ;

            // Preview
            MdlSettings.Cfg_Grid_BackGroundColor = Color.FromArgb(Conversions.ToInteger(MdlSettings.IniRead(StrSettingsFile, "preview", "bgColor", "")));
            MdlSettings.Cfg_Grid_ForeGroundColor = Color.FromArgb(Conversions.ToInteger(MdlSettings.IniRead(StrSettingsFile, "preview", "fgColor", "")));
            MdlSettings.Cfg_Grid_NumPixels = Conversions.ToInteger(MdlSettings.IniRead(StrSettingsFile, "preview", "numPixels", ""));
            MdlSettings.Cfg_Grid_Zoom = Conversions.ToInteger(MdlSettings.IniRead(StrSettingsFile, "preview", "zoom", ""));
            MdlSettings.Cfg_Grid_FootPrintX = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "preview", "footPrintX", ""));
            MdlSettings.Cfg_Grid_FootPrintY = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "preview", "footPrintY", ""));
        30:
            ;

            // Reads from ini and configures all.
            MdlSettings.Cfg_Path_Root = MdlSettings.IniRead(StrSettingsFile, "paths", "root", "");
            MdlSettings.Cfg_Path_RecentPNG = MdlSettings.IniRead(StrSettingsFile, "paths", "recentPNG", "");
            MdlSettings.Cfg_Path_RecentZT1 = MdlSettings.IniRead(StrSettingsFile, "paths", "recentZT1", "");
            MdlSettings.Cfg_Path_ColorPals8 = System.IO.Path.GetFullPath(Application.StartupPath) + @"\pal8";
            MdlSettings.Cfg_Path_ColorPals16 = System.IO.Path.GetFullPath(Application.StartupPath) + @"\pal16";
        40:
            ;

            // Export (PNG)
            MdlSettings.Cfg_Export_PNG_CanvasSize = Conversions.ToInteger(MdlSettings.IniRead(StrSettingsFile, "exportOptions", "pngCrop", ""));
            MdlSettings.Cfg_Export_PNG_RenderBGZT1 = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "exportOptions", "pngRenderExtraGraphic", ""));
            MdlSettings.Cfg_Export_PNG_RenderBGFrame = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "exportOptions", "pngRenderExtraFrame", ""));
            MdlSettings.Cfg_Export_PNG_TransparentBG = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "exportOptions", "pngRenderTransparentBG", ""));

            // Export (ZT1)
            MdlSettings.Cfg_Export_ZT1_Ani = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "exportOptions", "zt1Ani", "1"));
            MdlSettings.Cfg_Export_ZT1_AlwaysAddZTAFBytes = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "exportOptions", "zt1AlwaysAddZTAFBytes", ""));
        50:
            ;

            // Convert ( ZT1 <=> PNG, other way around )
            MdlSettings.Cfg_Convert_StartIndex = Conversions.ToInteger(MdlSettings.IniRead(StrSettingsFile, "conversionOptions", "pngFilesIndex", ""));
            MdlSettings.Cfg_Convert_DeleteOriginal = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "conversionOptions", "deleteOriginal", ""));
            MdlSettings.Cfg_Convert_Overwrite = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "conversionOptions", "overwrite", ""));
            MdlSettings.Cfg_Convert_SharedPalette = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "conversionOptions", "sharedPalette", ""));
            MdlSettings.Cfg_Convert_FileNameDelimiter = MdlSettings.IniRead(StrSettingsFile, "conversionOptions", "fileNameDelimiter", "");
        60:
            ;

            // Frame editing
            MdlSettings.Cfg_Editor_RotFix_IndividualFrame = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "editing", "individualRotationFix", ""));
            MdlSettings.Cfg_Frame_DefaultAnimSpeed = Conversions.ToInteger(MdlSettings.IniRead(StrSettingsFile, "editing", "animationSpeed", ""));
        70:
            ;

            // Palette
            MdlSettings.Cfg_Palette_Import_PNG_Force_Add_Colors = Conversions.ToByte(MdlSettings.IniRead(StrSettingsFile, "palette", "importPNGForceAddColors", ""));
        100:
            ;


            // Now, if our path is no longer valid, pop up 'Settings'-window automatically
            if (System.IO.Directory.Exists(MdlSettings.Cfg_Path_Root) == false)
            {


                // But let's give some suggestions.
                MdlSettings.Cfg_Path_Root = System.IO.Path.GetFullPath(Application.StartupPath);

                // Also give suggestions for color palettes.
                if (System.IO.Directory.Exists(MdlSettings.Cfg_Path_ColorPals8) == false & System.IO.Directory.Exists(Application.StartupPath + @"\pal8") == true)
                {
                    MdlSettings.Cfg_Path_ColorPals8 = MdlSettings.Cfg_Path_Root + @"\pal8";
                }

                if (System.IO.Directory.Exists(MdlSettings.Cfg_Path_ColorPals16) == false & System.IO.Directory.Exists(Application.StartupPath + @"\pal16") == true)
                {
                    MdlSettings.Cfg_Path_ColorPals8 = MdlSettings.Cfg_Path_Root + @"\pal16";
                }

                // Now show the settings dialog.
                My.MyProject.Forms.FrmSettings.ShowDialog();
            }

        200:
            ;


            // No recent paths yet?
            if (string.IsNullOrEmpty(MdlSettings.Cfg_Path_RecentPNG))
            {
                MdlSettings.Cfg_Path_RecentPNG = MdlSettings.Cfg_Path_Root;
            }

            if (string.IsNullOrEmpty(MdlSettings.Cfg_Path_RecentZT1))
            {
                MdlSettings.Cfg_Path_RecentZT1 = MdlSettings.Cfg_Path_Root;
            }

            // Paths invalid?
            if (Exists(MdlSettings.Cfg_Path_RecentPNG) == false)
            {
                MdlSettings.Cfg_Path_RecentPNG = MdlSettings.Cfg_Path_Root;
            }

            if (Exists(MdlSettings.Cfg_Path_RecentZT1) == false)
            {
                MdlSettings.Cfg_Path_RecentZT1 = MdlSettings.Cfg_Path_Root;
            }

        205:
            ;



            // Only now should the objects be created, if they don't exist yet
            // 20190817: wait, there were no conditions here. So on saving settings, editorGraphic and editorBgGraphic were reset?
            if (Information.IsNothing(MdlSettings.EditorGraphic) == true)
            {
                MdlSettings.EditorGraphic = new ClsGraphic(null); // The ClsGraphic object
            }

            if (Information.IsNothing(MdlSettings.EditorBgGraphic) == true)
            {
                MdlSettings.EditorBgGraphic = new ClsGraphic(null); // The background graphic, e.g. toy
            }

            return;
        dBug:
            ;
            MdlZTStudio.HandledError("MdlConfig", "Load", "Error while processing ZT Studio Settings", true, Information.Err());
        }

        /// <summary>
    /// Saves configuration to .INI file
    /// </summary>
        public static void Write()
        {

            // This tasks writes all settings to the .ini-file.
            // For an explanation of these parameters: check MdlSettings.vb

            string StrSettingsFile = System.IO.Path.GetFullPath(Application.StartupPath) + @"\settings.cfg";

            // Preview
            MdlSettings.IniWrite(StrSettingsFile, "preview", "bgColor", MdlSettings.Cfg_Grid_BackGroundColor.ToArgb().ToString());
            MdlSettings.IniWrite(StrSettingsFile, "preview", "fgColor", MdlSettings.Cfg_Grid_ForeGroundColor.ToArgb().ToString());
            MdlSettings.IniWrite(StrSettingsFile, "preview", "numPixels", MdlSettings.Cfg_Grid_NumPixels.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "preview", "zoom", MdlSettings.Cfg_Grid_Zoom.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "preview", "footPrintX", MdlSettings.Cfg_Grid_FootPrintX.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "preview", "footPrintY", MdlSettings.Cfg_Grid_FootPrintY.ToString());


            // Reads from ini and configures all.
            MdlSettings.IniWrite(StrSettingsFile, "paths", "root", MdlSettings.Cfg_Path_Root);
            MdlSettings.IniWrite(StrSettingsFile, "paths", "recentPNG", MdlSettings.Cfg_Path_RecentPNG);
            MdlSettings.IniWrite(StrSettingsFile, "paths", "recentZT1", MdlSettings.Cfg_Path_RecentZT1);


            // Export PNG (frames)
            MdlSettings.IniWrite(StrSettingsFile, "exportOptions", "pngCrop", MdlSettings.Cfg_Export_PNG_CanvasSize.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "exportOptions", "pngRenderExtraFrame", MdlSettings.Cfg_Export_PNG_RenderBGFrame.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "exportOptions", "pngRenderExtraGraphic", MdlSettings.Cfg_Export_PNG_RenderBGZT1.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "exportOptions", "pngRenderTransparentBG", MdlSettings.Cfg_Export_PNG_TransparentBG.ToString());

            // Export ZT1 (entire graphic)
            MdlSettings.IniWrite(StrSettingsFile, "exportOptions", "zt1Ani", MdlSettings.Cfg_Export_ZT1_Ani.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "exportOptions", "zt1AlwaysAddZTAFBytes", MdlSettings.Cfg_Export_ZT1_AlwaysAddZTAFBytes.ToString());

            // Convert options ( ZT1 <=> PNG )
            MdlSettings.IniWrite(StrSettingsFile, "conversionOptions", "pngFilesIndex", MdlSettings.Cfg_Convert_StartIndex.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "conversionOptions", "deleteOriginal", MdlSettings.Cfg_Convert_DeleteOriginal.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "conversionOptions", "overwrite", MdlSettings.Cfg_Convert_Overwrite.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "conversionOptions", "sharedPalette", MdlSettings.Cfg_Convert_SharedPalette.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "conversionOptions", "fileNameDelimiter", MdlSettings.Cfg_Convert_FileNameDelimiter);

            // Frame editing
            MdlSettings.IniWrite(StrSettingsFile, "editing", "individualRotationFix", MdlSettings.Cfg_Editor_RotFix_IndividualFrame.ToString());
            MdlSettings.IniWrite(StrSettingsFile, "editing", "animationSpeed", MdlSettings.Cfg_Frame_DefaultAnimSpeed.ToString());

            // Palette
            MdlSettings.IniWrite(StrSettingsFile, "palette", "importPNGForceAddColors", MdlSettings.Cfg_Palette_Import_PNG_Force_Add_Colors.ToString());
        }
    }
}