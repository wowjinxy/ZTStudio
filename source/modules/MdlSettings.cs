using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace ZTStudio
{
    static class MdlSettings
    {
        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, string lpReturnedString, int nSize, string lpFileName);

        public static ClsDirectBitmap BMEmpty; // Empty bitmap on initializing. PictureBox's image sometimes gets reset to this. Todo: review if actually needed...
        public static ClsGraphic EditorGraphic;             // The ClsGraphic object we use.
        public static ClsGraphic EditorBgGraphic;           // The background graphic, e.g. toy
        public static ClsFrame EditorFrame;                 // The ClsFrame we are currently viewing/editing
        public static bool BlnTaskRunning = false;          // Prevents certain UI updates if a task is running
        public static Color Cfg_Grid_BackGroundColor = Color.White; // The default background color?
        public static Color Cfg_Grid_ForeGroundColor = Color.Black; // The default foreground color for the grid lines?
        public static int Cfg_Grid_NumPixels = 256; // 256 - The maximum number of pixels
        public static int Cfg_Grid_Zoom = 1; // Default zoom level
        public static string Cfg_Path_Root = @"C:\Program Files (x86)\Microsoft Games\Zoo Tycoon"; // Default location of our project folder. The user should select a <root>folder with contents similar to <root>/animals/ibex/m/
        public static string Cfg_Path_ColorPals8 = @"C:\Users\root\Documents\GitHub\ZTStudio\source\bin\Release\pal8"; // Location of color palettes (for color replacement) - 8 shades
        public static string Cfg_Path_ColorPals16 = @"C:\Users\root\Documents\GitHub\ZTStudio\source\bin\Release\pal16"; // Location of color palettes (for color replacement) - 16 shades

        // Export
        public static byte Cfg_Export_PNG_RenderBGFrame = 1; // If a background frame is present: should it be rendered in all PNG output files (or separately?)
        public static int Cfg_Export_PNG_CanvasSize = 0; // Should the PNG be the size (height/width) of the canvas, or cropped? 
        public static byte Cfg_Export_PNG_RenderBGZT1 = 0; // If a background ZT1 Graphic was chosen, should it be rendered in the PNG output files?
        public static byte Cfg_Export_PNG_TransparentBG = 0; // 0 = use ZT Studio background color; 1 = write transparent color


        // Write ZT1
        public static byte Cfg_Export_ZT1_Ani = 1; // Try to generate an .ani file
        public static byte Cfg_Export_ZT1_AlwaysAddZTAFBytes = 0; // Should we add ZTAF-bytes even for a simple object? 


        // Convert
        public static byte Cfg_Convert_DeleteOriginal = 1; // Should the original image(s) be deleted upon conversion?
        public static byte Cfg_Convert_Overwrite = 1; // Should we overwrite existing files?
        public static int Cfg_Convert_StartIndex = 0; // Does the index start at for example N_0000.png or N_0001.png ?
        public static byte Cfg_Convert_SharedPalette = 1; // Do we (try to) share a color palette?
        public static string Cfg_Convert_FileNameDelimiter = "_"; // The file name delimiter. eg _ in NE_0000.png

        // Experimental
        public static object Cfg_Convert_Write_Graphic_Data_To_Text_File = 0; // Should a text file be created? Contains info on frames (offsets, mystery bytes, width, height...)

        // Frame
        public static byte Cfg_Editor_RotFix_IndividualFrame = 0; // determines whether we are fixing the position of an object in 1 frame or in the entire graphic
        public static int Cfg_Frame_DefaultAnimSpeed = 125; // Default animation speed


        // Palette
        public static byte Cfg_Palette_Quantization = 0; // Set to 1 to allow quantization
        public static byte Cfg_Palette_Import_PNG_Force_Add_Colors = 0; // Set to 1 to force duplicate colors to be processed (recommended after recolors - some colors, especially when making things brighter or darker, may end up the same.)


        // Grid
        public static byte Cfg_Grid_FootPrintX = 2; // the X-footprint in Zoo Tycoon.
        public static byte Cfg_Grid_FootPrintY = 2; // the Y-footprint in Zoo Tycoon.


        // Recent files
        public static string Cfg_Path_RecentZT1 = ""; // Most recent path to select a ZT1 Graphic (file!)
        public static string Cfg_Path_RecentPNG = ""; // Most recent path to select a PNG graphic (file!)


        // GitHub
        public static string Cfg_GitHub_URL = "https://github.com/jbostoen/ZTStudio";

        // Debugging
        public static int Cfg_Trace = 0; // Set to 1 for detailed logging


        // Todo:
        // - on load png frame (either UI or conversion): check max size. = 00 00 ? = 256 + 256 ?

        // - move over pixel, get color. then find the color index in a palette.
        // - render and save all frames in an animation. Suggest name pattern.
        // - allow crop to image (to either relevant pixels in frame, or graphic's relevant pixels)



        // - combine 2 different graphics?
        // -- eg bounce (guest) + bounce (object)
        // -- eg swing (guest) + swing (object)
        // -- ringtoss (guest) + ringtoss (object)


        // - detect missing color palettes (=crash)
        // cachedfame <=> writeFrame()




        // Public Sub DoubleBuffered(ByVal Obj As Object, ByVal setting As Boolean)
        // Dim ObjType As Type = Obj.[GetType]()
        // Dim pi As PropertyInfo = ObjType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        // pi.SetValue(Obj, setting, Nothing)
        // End Sub



        public static int IniWrite(string iniFileName, string Section, string ParamName, string ParamVal)
        {
            int Result = MdlSettings.WritePrivateProfileString(ref Section, ref ParamName, ref ParamVal, ref iniFileName);
            return 0;
        }

        public static string IniRead(string IniFileName, string Section, string ParamName, string ParamDefault)
        {
            string IniReadRet = default;
            string ParamVal = Strings.Space(1024);
            int LenParamVal = MdlSettings.GetPrivateProfileString(ref Section, ref ParamName, ref ParamDefault, ref ParamVal, Strings.Len(ParamVal), ref IniFileName);
            IniReadRet = Strings.Left(ParamVal, LenParamVal);
            return IniReadRet;
        }
    }
}