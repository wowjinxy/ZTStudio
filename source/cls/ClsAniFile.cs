using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            try
            {
                if (strFileName != null)
                {
                    FileName = strFileName;
                    // This function will write out the .ani-file.
                }

                StringBuilder strAni = new StringBuilder("[animation]\n");

                // If there's a .ani-file present, delete it first.
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                // Write out dirs
                foreach (string s in RelativeDirectories)
                {
                    strAni.AppendLine($"dir{RelativeDirectories.IndexOf(s)} = {s}");
                }

                // Write out views
                foreach (string s in Views)
                {
                    strAni.AppendLine($"animation = {s}");
                }

                // Now, the coordinates
                strAni.AppendLine($"x0 = {X0}\ny0 = {Y0}\nx1 = {X1}\ny1 = {Y1}");

                // Write.
                if (Views.Count > 0 && RelativeDirectories.Count > 0)
                {
                    using (StreamWriter outfile = new StreamWriter(FileName))
                    {
                        outfile.Write(strAni.ToString());
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                MdlZTStudio.UnhandledError(GetType().FullName, "Write", ex, false);
                return default;
            }
        }


        /// <summary>
        /// This sub tries to create a .ani-file. It does so based on the offsets of graphics it detects.
        /// This is experimental, but it should work for the majority of graphics.
        /// </summary>
        /// <param name="StrFileName">Destination filename</param>
        public void CreateAniConfig(string strFileName = null)
        {
            try
            {
                if (strFileName != null)
                {
                    FileName = strFileName.Replace("/", @"\");
                }

                if (string.IsNullOrEmpty(FileName))
                {
                    MdlZTStudio.HandledError(GetType().FullName, "CreateAniConfig", "Unexpected error: filename for .ani file is empty?", true);
                    return;
                }

                // Full and relative path of the .ani file
                string strPath = Path.GetDirectoryName(FileName);
                string strPathRelative = System.Text.RegularExpressions.Regex.Replace(
                    strPath,
                    System.Text.RegularExpressions.Regex.Escape(MdlSettings.Cfg_Path_Root) + @"(\\|)",
                    "",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );

                var objGraphic = new ClsGraphic(null);

                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Root path: * " + MdlSettings.Cfg_Path_Root);
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Ani path: * " + strPath + " -> " + strPathRelative);

                // Set dirs and views
                RelativeDirectories.Clear();
                RelativeDirectories.AddRange(strPathRelative.Split('\\'));

                Views.Clear();

                // Determine the type of graphic
                DetermineGraphicType(strPath);

                if (Views.Count > 0)
                {
                    ProcessGraphicViews(strPath, objGraphic);
                }

                Write();
            }
            catch (Exception ex)
            {
                MdlZTStudio.UnhandledError(GetType().FullName, "CreateAniConfig", ex, true);
            }
        }

        private void DetermineGraphicType(string path)
        {
            // Checks for animals, guests, staff
            if (CheckFilesExist(path, new string[] { "N", "NE", "E", "SE", "S" }))
            {
                Views.AddRange(new string[] { "N", "NE", "E", "SE", "S" });
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'animals', 'guests', 'staff', ...");
            }
            // Checks for objects
            else if (CheckFilesExist(path, new string[] { "NE", "SE", "SW", "NW" }))
            {
                Views.AddRange(new string[] { "NE", "SE", "SW", "NW" });
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'object'");
            }
            // Checks for icons
            else if (File.Exists(Path.Combine(path, "N")))
            {
                Views.Add("N");
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'icon'");
            }
            // Checks for paths
            else if (Enumerable.Range(1, 20).All(i => File.Exists(Path.Combine(path, i.ToString()))))
            {
                Views.AddRange(Enumerable.Range(1, 20).Select(i => i.ToString()));
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: 'path'");
            }
            else
            {
                MdlZTStudio.Trace(GetType().FullName, "CreateAniConfig", "Determination: unable to determine type of graphic in " + path);
            }
        }

        private bool CheckFilesExist(string path, IEnumerable<string> fileNames)
        {
            return fileNames.All(fileName => File.Exists(Path.Combine(path, fileName)));
        }

        private void ProcessGraphicViews(string path, ClsGraphic graphic)
        {
            foreach (string view in Views)
            {
                graphic.Read(Path.Combine(path, view).Replace(@"\", "/"));

                foreach (ClsFrame frame in graphic.Frames)
                {
                    frame.RenderCoreImageFromHex();
                    X0 = Math.Min(X0, -frame.OffsetX);
                    Y0 = Math.Min(Y0, -frame.OffsetY);
                    X1 = Math.Max(X1, -frame.OffsetX + frame.CoreImageBitmap.Width);
                    Y1 = Math.Max(Y1, -frame.OffsetY + frame.CoreImageBitmap.Height);
                }
            }
        }

        public ClsAniFile(string myFileName)
        {
            FileName = myFileName;
        }
    }
}