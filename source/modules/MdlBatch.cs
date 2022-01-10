using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ZTStudio
{

    /// <summary>
/// Groups batch operations
/// </summary>
    static class MdlBatch
    {


        /// <summary>
    /// Attempts to create .ani file for each animation. Experimental.
    /// </summary>
    /// <param name="StrPath">Path to folder</param>
        public static void WriteAniFile(string StrPath)
        {
            if (MdlSettings.Cfg_Export_ZT1_Ani == 0)
            {
                MdlZTStudio.Trace("MdlBatch", "WriteAniFile", "Option to create .ani not enabled. Skipping main folder " + StrPath);
                return;
            }

            MdlZTStudio.Trace("MdlBatch", "WriteAniFile", "Processing main folder " + StrPath);
            var StackDirectories = new Stack<string>();
            StackDirectories.Push(StrPath);

            // Continue processing for each stacked directory
            while (StackDirectories.Count > 0)
            {

                // Get top directory string
                string StrDirectoryName = StackDirectories.Pop();
                var ObjAniFile = new ClsAniFile(StrDirectoryName + @"\" + Path.GetFileName(StrDirectoryName) + ".ani");
                MdlZTStudio.Trace("MdlBatch", "WriteAniFile", "Attempting to create " + Path.GetFileName(StrDirectoryName) + ".ani");

                // Loop through all subdirectories and add them to the stack.
                ObjAniFile.CreateAniConfig();
                foreach (var StrSubDirectoryName in Directory.GetDirectories(StrDirectoryName))
                    StackDirectories.Push(StrSubDirectoryName);

                // Make sure everything is finished. Needed?
                Application.DoEvents();
            }

            // Make sure everything is finished. Needed?
            Application.DoEvents();
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError("MdlBatch", "WriteAniFile", Information.Err(), true);
        }
    }
}