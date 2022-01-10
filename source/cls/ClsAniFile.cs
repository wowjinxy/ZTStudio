using System;
using System.IO;
using Microsoft.VisualBasic;

namespace ZTStudio
{

    /// <summary>
/// ClsAniFile manages information about the .ani file.
/// This file contains info about offsets.
/// </summary>
    public class ClsAniFile
    {

        // The .ani file is mostly used for icons in ZT1.
        // Every graphic, with 1 or multiple views (N, NE, NW, SE, SW, S, E, ...) has one .ani-file.
        // It contains a header, [Animation]
        // It contains a dir line for each directory in the path (eg objects/bamboo/idle => objects, bamboo, idle )
        // It contains an animation line for each view for this graphic ( N, NE, NW, SE, SW, S, E ...)
        // How the order is defined, is unclear for most part - for now.
        // It contains x0, y0 coordinates and x1, y1 coordinates.
        // Those seem to define the upper left and bottom right pixels, defining the maximum canvas size, 
        // for all graphics belonging to this 'object' (consider anything: guests, staff, animals, objects, paths ... )


        private int Ani_X0 = 0; // canvas: top left
        private int Ani_Y0 = 0; // canvas: top left
        private int Ani_X1 = 0; // canvas: bottom right
        private int Ani_Y1 = 0; // canvas: bottom right
        private List<string> Ani_RelativeDirectories = new List<string>(); // lists the directories in the relative location to this file
        private List<string> Ani_Views = new List<string>(); // lists the Views in this file
        private string ani_FileName = ""; // filename of .ani-file

        /// <summary>
    /// Offset (X) of top left pixel
    /// </summary>
    /// <returns>Integer</returns>
        public int X0
        {
            get
            {
                return Ani_X0;
            }

            set
            {
                Ani_X0 = value;
            }
        }

        /// <summary>
    /// Offset (X) of bottom right pixel
    /// </summary>
    /// <returns>Integer</returns>
        public int X1
        {
            get
            {
                return Ani_X1;
            }

            set
            {
                Ani_X1 = value;
            }
        }

        /// <summary>
    /// Offset (Y) of top left pixel
    /// </summary>
    /// <returns>Integer</returns>
        public int Y0
        {
            get
            {
                return Ani_Y0;
            }

            set
            {
                Ani_Y0 = value;
            }
        }

        /// <summary>
    /// Offset (Y) of bottom right pixel
    /// </summary>
    /// <returns>Integer</returns>
        public int Y1
        {
            get
            {
                return Ani_Y1;
            }

            set
            {
                Ani_Y1 = value;
            }
        }

        /// <summary>
    /// List of directories (tree structure) relative to root
    /// </summary>
    /// <returns></returns>
        public List<string> RelativeDirectories
        {
            get
            {
                return Ani_RelativeDirectories;
            }

            set
            {
                Ani_RelativeDirectories = value;
            }
        }

        /// <summary>
    /// List of all views 
    /// </summary>
    /// <returns></returns>
        public List<string> Views
        {
            get
            {
                return Ani_Views;
            }

            set
            {
                Ani_Views = value;
            }
        }

        /// <summary>
    /// Filename of the .ani file
    /// </summary>
    /// <returns>String</returns>
        public string FileName
        {
            get
            {
                return ani_FileName;
            }

            set
            {
                ani_FileName = value;
            }
        }

        // Functions. 

        /// <summary>
    /// Writes a .ani file, based on the info in this object
    /// </summary>
    /// <param name="strFileName">Destination filename</param>
    /// <returns></returns>
        public object Write(string strFileName = null)
        {
            if (Information.IsNothing(strFileName) == false)
            {
                FileName = strFileName;

                // This function will write out the .ani-file.


            };
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
            /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 4299


            Input:

                    ' This function will write out the .ani-file.


                    On Error GoTo dBug

             */
            string StrAni = "[animation]" + Constants.vbCrLf;
        1:
            ;

            // If there's a .ani-file present, delete it first.
            if (File.Exists(FileName) == true)
            {
                File.Delete(FileName);
            }

        2:
            ;

            // Write out dirs
            foreach (string s in RelativeDirectories)
                StrAni = StrAni + "dir" + RelativeDirectories.IndexOf(s) + " = " + s + Constants.vbCrLf;
            3:
            ;

            // Write out views
            foreach (string s in Views)
                StrAni = StrAni + "animation = " + s + Constants.vbCrLf;
            4:
            ;

            // Now, the coordinates
            StrAni = StrAni + "x0 = " + X0 + Constants.vbCrLf + "y0 = " + Y0 + Constants.vbCrLf + "x1 = " + X1 + Constants.vbCrLf + "y1 = " + Y1 + Constants.vbCrLf;
        10:
            ;

            // Write.
            if (Views.Count > 0 & RelativeDirectories.Count > 0)
            {
                if (File.Exists(FileName) == true)
                {
                    File.Delete(FileName);
                }

                using (var outfile = new StreamWriter(FileName))
                {
                    outfile.Write(StrAni.ToString());
                }
            }

            return 0;
            return default;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "Write", Information.Err(), false);
        }


        /// <summary>
    /// This sub tries to create a .ani-file. It does so based on the offsets of graphics it detects.
    /// This is experimental, but it should work for the majority of graphics.
    /// </summary>
    /// <param name="StrFileName">Destination filename</param>
        public void CreateAniConfig(string StrFileName = null)
        {

            // This function needs a filename for the .ani-file, since it derives its directory from it.
            // It will take note of the 'dirs'
            // It will try to find out whether it is dealing with one of these 4 types:
            // 
            // N                 icons
            // NE/NW/SE/SW       objects
            // N/NE/E/SE/S       animals, guests, staff...
            // 1-20              paths

            if (Information.IsNothing(StrFileName) == false)
            {
                FileName = StrFileName.Replace("/", @"\");
            }

        1:
            ;
            if (string.IsNullOrEmpty(FileName))
            {

                // Is there any path which leads up to this error?
                MdlZTStudio.HandledError(GetType().FullName, "CreateAniConfig", "Unexpected error: filename for .ani file is empty?", true, Information.Err());
            }
            else
            {
            2:
                ;


                // This is the full path and the relative path of the .ani file
                string StrPath = Path.GetDirectoryName(FileName);
                string StrPathRelative;
                StrPathRelative = System.Text.RegularExpressions.Regex.Replace(StrPath, System.Text.RegularExpressions.Regex.Escape(MdlSettings.Cfg_Path_Root) + @"(\\|)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                var ObjGraphic = new ClsGraphic(null);
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Root path: * " + MdlSettings.Cfg_Path_Root);
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Ani path: * " + StrPath + " -> " + StrPathRelative);

                // Set dirs. If this function is called multiple times, it won't do any harm.
                RelativeDirectories.Clear(false);
                RelativeDirectories.AddRange(Strings.Split(StrPathRelative, @"\"), false);
            10:
                ;

                // Set views.
                Views.Clear(false);
            11:
                ;
                if (File.Exists(StrPath + @"\N") == true & File.Exists(StrPath + @"\NE") == true & File.Exists(StrPath + @"\E") == true & File.Exists(StrPath + @"\SE") == true & File.Exists(StrPath + @"\S") == true)
                {


                    // This is typical for animals, guests, staff...
                    {
                        var withBlock = Views;
                        withBlock.Add("N", false);
                        withBlock.Add("NE", false);
                        withBlock.Add("E", false);
                        withBlock.Add("SE", false);
                        withBlock.Add("S", false);
                    }

                    MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'animals', 'guests', 'staff', ...");
                12:
                    ;
                }
                else if (File.Exists(StrPath + @"\NE") == true & File.Exists(StrPath + @"\SE") == true & File.Exists(StrPath + @"\SW") == true & File.Exists(StrPath + @"\NW") == true)
                {

                    // This is typical for objects
                    {
                        var withBlock1 = Views;
                        withBlock1.Add("NE", false);
                        withBlock1.Add("SE", false);
                        withBlock1.Add("SW", false);
                        withBlock1.Add("NW", false);
                    }

                    MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'object'");
                13:
                    ;
                }
                else if (File.Exists(StrPath + @"\N") == true)
                {

                    // This is typical for icons
                    {
                        var withBlock2 = Views;
                        withBlock2.Add("N", false);
                    }

                    MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'icon'");
                14:
                    ;
                }
                else if (File.Exists(StrPath + @"\1") == true & File.Exists(StrPath + @"\2") == true & File.Exists(StrPath + @"\3") == true & File.Exists(StrPath + @"\4") == true & File.Exists(StrPath + @"\5") == true & File.Exists(StrPath + @"\6") == true & File.Exists(StrPath + @"\7") == true & File.Exists(StrPath + @"\8") == true & File.Exists(StrPath + @"\9") == true & File.Exists(StrPath + @"\10") == true & File.Exists(StrPath + @"\11") == true & File.Exists(StrPath + @"\12") == true & File.Exists(StrPath + @"\13") == true & File.Exists(StrPath + @"\14") == true & File.Exists(StrPath + @"\15") == true & File.Exists(StrPath + @"\16") == true & File.Exists(StrPath + @"\17") == true & File.Exists(StrPath + @"\18") == true & File.Exists(StrPath + @"\19") == true & File.Exists(StrPath + @"\20") == true)
                {

                    // This is typical for paths
                    {
                        var withBlock3 = Views;
                        int IntX = 1;
                        while (IntX <= 20)
                        {
                            withBlock3.Add(IntX.ToString("0"), false);
                            IntX += 1;
                        }
                    }

                    MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'path'");
                15:
                    ;
                }
                else if (File.Exists(StrPath + @"\N") == true & File.Exists(StrPath + @"\H") == true & File.Exists(StrPath + @"\S") == true & File.Exists(StrPath + @"\G") == true)
                {

                    // This is typical for objects
                    {
                        var withBlock4 = Views;
                        withBlock4.Add("N", false);
                        withBlock4.Add("H", false);
                        withBlock4.Add("S", false);
                        withBlock4.Add("G", false);
                    }

                    MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'ui button'");
                99:
                    ;
                }
                else
                {
                    MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: unable to determine type of graphic in " + Path.GetDirectoryName(FileName));
                }

            100:
                ;

                // Will only do something if views were detected, in a similar fashion to what's known.
                // For instance, if one graphic (SE) is used for 4 sides, ZTStudio will NOT recognize it and do nothing.
                if (Views.Count > 0)
                {
                    foreach (var StrAni in Views)
                    {

                        // We need to read every view for this graphic in the folder.
                        ObjGraphic.Read(StrPath.Replace(@"\", "/") + "/" + StrAni);
                        foreach (ClsFrame ObjFrame in ObjGraphic.Frames)
                        {

                            // Get original hex
                            ObjFrame.RenderCoreImageFromHex();

                            // Passes the bamboo.ani-test
                            X0 = Math.Min(X0, -ObjFrame.OffsetX);
                            Y0 = Math.Min(Y0, -ObjFrame.OffsetY);
                            X1 = Math.Max(X1, -ObjFrame.OffsetX + ObjFrame.CoreImageBitmap.Width);
                            Y1 = Math.Max(Y1, -ObjFrame.OffsetY + ObjFrame.CoreImageBitmap.Height);
                        }
                    }
                }
            }

        50:
            ;
            Write();
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "CreateAniConfig", Information.Err(), true);
        }

        public ClsAniFile(string myFileName)
        {
            FileName = myFileName;
        }
    }
}