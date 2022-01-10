using System;
using System.ComponentModel;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{

    /// <summary>
/// Class to handle the main graphic, which consist of one or multiple frames which share the same color palette.
/// </summary>

    public class ClsGraphic : INotifyPropertyChanged
    {

        // This class handles ZT1 graphic files, e.g. "N". 
        // All handling of palette files is done by a different class

        private string ClsGraphic_FileName = Constants.vbNullString; // File name of graphic
        private ClsPalette ClsGraphic_Palette;
        private int ClsGraphic_AnimationSpeed = MdlSettings.Cfg_Frame_DefaultAnimSpeed; // Speed in milliseconds for this animation
        private byte ClsGraphic_HasBackgroundFrame = 0; // Basic files, FATZ-files with byte 9 = 0: no extra background frame. Byte 9 = 1: graphic contain a background frame.
        private List<ClsFrame> ClsGraphic_Frames = new List<ClsFrame>();
        private string ClsGraphic_LastUpdated = DateAndTime.Now.ToString("yyyyMMddHHmmss"); // For caching purposes for larger frames.

        /// <summary>
    /// Filename of the ZT1 Graphic. Contrary to a regular filename, it has no file extension.
    /// </summary>
    /// <returns>String</returns>
        public string FileName
        {
            get
            {
                return ClsGraphic_FileName;
            }

            set
            {
                ClsGraphic_FileName = value.ToLower();
                NotifyPropertyChanged("FileName");
            }
        }

        /// <summary>
    /// The color palette used in this graphic and shared among its frames.
    /// </summary>
    /// <returns>ClsPalette - color palette</returns>
        public ClsPalette ColorPalette
        {
            get
            {
                return ClsGraphic_Palette;
            }

            set
            {
                ClsGraphic_Palette = value;
                NotifyPropertyChanged("ColorPalette");
            }
        }

        /// <summary>
    /// Array of frames (ClsFrame) in this graphic. Includes background frame (as last frame), if present.
    /// </summary>
    /// <returns>List(Of ClsFrame) - list of ZT1 frames</returns>
        public List<ClsFrame> Frames
        {
            get
            {
                return ClsGraphic_Frames;
            }

            set
            {
                ClsGraphic_Frames = value;
                NotifyPropertyChanged("Frames");
            }
        }

        /// <summary>
    /// Animation speed of the frame, in milliseconds. How much time passes before the next frame is shown?
    /// </summary>
    /// <returns>Integer - number of milli seconds</returns>
        public int AnimationSpeed
        {
            get
            {
                return ClsGraphic_AnimationSpeed;
            }

            set
            {
                ClsGraphic_AnimationSpeed = value;
                NotifyPropertyChanged("AnimationSpeed");
            }
        }

        /// <summary>
    /// <para>Whether this graphic contains an extra background frame.</para>
    /// <para>In some cases, such as the Restaurant, only changing pixels are in the regular set of frames. The last frame is always rendered as a background in this case.</para>
    /// </summary>
    /// <returns></returns>
    /// <remarks>Kept as a byte for calculations</remarks>
        public byte HasBackgroundFrame
        {
            get
            {
                return ClsGraphic_HasBackgroundFrame;
            }

            set
            {
                ClsGraphic_HasBackgroundFrame = value;
                NotifyPropertyChanged("ExtraFrame");
            }
        }

        /// <summary>
    /// Timestamp of last update.
    /// </summary>
    /// <returns></returns>
        public string LastUpdated
        {
            get
            {
                return ClsGraphic_LastUpdated;
            }

            set
            {
                ClsGraphic_LastUpdated = value;
                NotifyPropertyChanged("LastUpdated");
            }
        }

        /// <summary>
    /// Reads the graphic (from a file)
    /// </summary>
    /// <param name="StrFileName">Source file name</param>
        public void Read(string StrFileName = Constants.vbNullString)
        {

        // On Error GoTo dBg

        1:
            ;

            // 20190815 Before this just set the filename; but not the (assumed) .pal file
            if (!string.IsNullOrEmpty(StrFileName))
            {
                FileName = StrFileName;
            }

            int IntX = 0;
            int IntCurByte = 0;
            int IntTemplength = 0;
            int IntNumberOfFrames = 0; // Number of frames for this animation (at least 1)
            string StrOriginalColorPaletteFileName = ColorPalette.FileName;
            string StrNewColorPaletteFileName = MdlSettings.Cfg_Path_Root + "/";
            MdlZTStudio.Trace(GetType().FullName, "Read", "Reading graphic " + FileName + " ...");
        5:
            ;

            // Read full file.
            var Bytes = File.ReadAllBytes(ClsGraphic_FileName);
            var LstBytesToHex = Array.ConvertAll(Bytes, b => b.ToString("X2"));
            var LstHexValues = new List<string>();
            LstHexValues.AddRange(LstBytesToHex);
        10:
            ;

            // Here at least 3 variants of ZT1 Graphic format, which can be identified by the first 9 bytes of the file.
            if (LstHexValues[0] == "46" & LstHexValues[1] == "41" & LstHexValues[2] == "54" & LstHexValues[3] == "5A")
            {
                MdlZTStudio.Trace(GetType().FullName, "Read", "FATZ-file (ZT Animation File)");
                // 46 41 54 5A 00 | 00 00 00 01
                MdlZTStudio.Trace(GetType().FullName, "Read", "Background frame: " + LstHexValues[8]);
                HasBackgroundFrame = Conversions.ToByte(LstHexValues[8]);
                LstHexValues.Skip(9);
            }
            else
            {
                MdlZTStudio.Trace(GetType().FullName, "Read", "Basic graphic format");
            }

        15:
            ;

            // === ANIMATION SPEED ===
            AnimationSpeed = Conversions.ToInteger("&H" + LstHexValues[3] + LstHexValues[2] + LstHexValues[1] + LstHexValues[0]);
            MdlZTStudio.Trace(GetType().FullName, "Read", "Animation speed: " + AnimationSpeed);
        20:
            ;

            // === FILENAME ===
            // The next bytes contain the length of the filename of the color palette
            IntTemplength = Conversions.ToInteger("&H" + LstHexValues[7] + LstHexValues[6] + LstHexValues[5] + LstHexValues[4]) - 1;
        30:
            ;
            IntX = 0;
            while (IntX < IntTemplength)
            {
                StrNewColorPaletteFileName += Conversions.ToString(Strings.Chr(Conversions.ToInteger("&H" + LstHexValues[8 + IntX])));
                IntX += 1;
            }

            MdlZTStudio.Trace(GetType().FullName, "Read", "Color palette filename '" + ColorPalette.FileName + "' (length: " + IntTemplength + ")");
        35:
            ;

            // Remove all processed bytes
            LstHexValues.Skip(8 + IntTemplength + 1);
        40:
            ;

            // === READ COLOR PALETTE ===

            // Read the color palette
            // In case of failure, such as missing palette file, ZTStudio will throw a fatal error.
            // Only necessary if different name! (optimize performance)
            if ((StrNewColorPaletteFileName ?? "") != (StrOriginalColorPaletteFileName ?? ""))
            {
                MdlZTStudio.Trace(GetType().FullName, "Read", "Reading color palette...");

                // Read palette
                ColorPalette.Colors.Clear();
                ColorPalette.ReadPal(StrNewColorPaletteFileName);
            }
            else
            {

                // Graphic uses same palette; no need to reload
                MdlZTStudio.Trace(GetType().FullName, "Read", "Color palette already loaded for previous graphic...");
            }

        50:
            ;

            // === NUMBER OF FRAMES ===
            // This is actually not used anymore, although it could be considered as a check at the very end to see if the expected amount of frames has been processed
            IntNumberOfFrames = Conversions.ToInteger("&H" + LstHexValues[IntCurByte + 3] + LstHexValues[IntCurByte + 2] + LstHexValues[IntCurByte + 1] + LstHexValues[IntCurByte]);
            MdlZTStudio.Trace(GetType().FullName, "Read", "Number of frames: " + IntNumberOfFrames);

            // Remove all processed bytes
            LstHexValues.Skip(4);
        100:
            ;

            // ==================================== FOR EACH FRAME... ===================================
            int IntCurrentFrame = 0;
            int IntFrameBytes = 0;
            int IntFrameBytesCurrent = 0;
            Frames = new List<ClsFrame>(); // List of hex values will be stored here, for each frame
            while (LstHexValues.Count > 0)
            {
            101:
                ;

                // The next 4 bytes determine the length of bytes to follow for one of the frames in this graphic
                IntFrameBytes = Conversions.ToInteger("&H" + LstHexValues[IntCurByte + 3] + LstHexValues[IntCurByte + 2] + LstHexValues[IntCurByte + 1] + LstHexValues[IntCurByte]);

                // Remove all processed bytes.
                LstHexValues.Skip(4);
                MdlZTStudio.Trace(GetType().FullName, "Read", "Number of bytes for frame " + Frames.Count + ":  " + IntFrameBytes);
            102:
                ;
                var ObjFrame = new ClsFrame(this);
                var LstHexForOneFrame = new List<string>();

                // Build  hex string first.
                var loopTo = IntFrameBytes - 1;
                for (IntFrameBytesCurrent = 0; IntFrameBytesCurrent <= loopTo; IntFrameBytesCurrent++)
                    LstHexForOneFrame.Add(LstHexValues[IntFrameBytesCurrent]);
                103:
                ;

                // Set the hex values of this frame object
                ObjFrame.CoreImageHex = LstHexForOneFrame;
            104:
                ;

                // Render the bitmap. This also sets offsets etc.
                ObjFrame.RenderCoreImageFromHex();
            105:
                ;

                // Add to the frame collection
                Frames.Add(ObjFrame, false);
            110:
                ;

                // Remove all processed bytes.
                LstHexValues.Skip(IntFrameBytes);
            155:
                ;
                IntCurrentFrame += 1;
            }

        205:
            ;
            LastUpdated = DateAndTime.Now.ToString("yyyyMMddHHmmss");
            return;
        dBg:
            ;

            // Unexpected error
            MdlZTStudio.UnhandledError(GetType().FullName, "Read", Information.Err(), true);
        }


        /// <summary>
    /// Writing/saving a ZT1 Graphic File
    /// </summary>
    /// <param name="StrFileName">Destination file name</param>
    /// <param name="BlnOverwrite">Overwrite without warning</param>
        public void Write(string StrFileName = Constants.vbNullString, bool BlnOverwrite = true)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
            /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 11238


            Input:

                    On Error GoTo dBug

             */
            var LstHexGraphic = new List<string>();
        1:
            ;
            if (!string.IsNullOrEmpty(StrFileName))
            {
                FileName = StrFileName;
            }

            MdlZTStudio.Trace(GetType().FullName, "Write", "Outputting to " + FileName);
        2:
            ;

            // 20190815: Set default .pal filename, even if it doesn't exist (for when 'write' occurs)
            if (string.IsNullOrEmpty(ColorPalette.FileName))
            {
                MdlZTStudio.Trace(GetType().FullName, "Write", "No filename for color palette specified. Defaulting to " + FileName + ".pal");
                ColorPalette.FileName = FileName + ".pal";
            }

        5:
            ;
            if (File.Exists(StrFileName) == true & BlnOverwrite == false)
            {
                string StrErrorMessage = "Error: could Not create ZT1 Graphic." + Constants.vbCrLf + "There is already a file at this location:  " + Constants.vbCrLf + "'" + StrFileName + "'";
                MdlZTStudio.HandledError(GetType().FullName, "Write", StrErrorMessage, false, null);
                return;
            }

        10:
            ;

            // === Currently only output of basic files is supported. ===
            // Simply output frames as hex etc
            // set path to use '/' instead of '\'

            string StrPalName = ColorPalette.FileName;
            StrPalName = Strings.Replace(StrPalName, @"\", "/");
            StrPalName = Strings.Replace(StrPalName, Strings.Replace(MdlSettings.Cfg_Path_Root, @"\", "/") + "/", "", Compare: CompareMethod.Text);

            // === Always ZTAF? Or background frame ===
            if (HasBackgroundFrame == 1 | MdlSettings.Cfg_Export_ZT1_AlwaysAddZTAFBytes == 1)
            {

                // "FATZ" - reversed hex for Zoo  Tycoon Animation File.
                LstHexGraphic.Add("46", false);
                LstHexGraphic.Add("41", false);
                LstHexGraphic.Add("54", false);
                LstHexGraphic.Add("5A", false);
                LstHexGraphic.Add("00", false);
                LstHexGraphic.Add("00", false);
                LstHexGraphic.Add("00", false);
                LstHexGraphic.Add("00", false);

                // If the file is marked FATZ, then there are two possibilities.
                // Either there's an extra frame (background), e.g. for the restaurant; 
                // or there simply isn't. This is reflected in the 9th byte
                if (HasBackgroundFrame == 1)
                {
                    LstHexGraphic.Add("01", false);
                }
                else
                {
                    LstHexGraphic.Add("00", false);
                }
            }

            // === Animation speed ===
            LstHexGraphic.AddRange(Strings.Split(AnimationSpeed.ToString("X8").ReverseHex(), " "), false);

            // === Palette file name length ===
            LstHexGraphic.AddRange(Strings.Split((StrPalName.Length + 1).ToString("X8").ReverseHex(), " "), false);

            // === Palette file name ===
            foreach (char StrChar in StrPalName)
                LstHexGraphic.Add(Convert.ToString(Convert.ToInt32(StrChar), 16), false);
            LstHexGraphic.Add("00", false); // Add null character.

            // === Number of frames ====
            // Limit - todo: find out if the theoretical number of frames is 255 (FF - X2) or the number can be larger (other bytes?)
            LstHexGraphic.AddRange(Strings.Split((Frames.Count - HasBackgroundFrame).ToString("X8").ReverseHex(), " "), false);

            // === Find out the total length. This could be a lot. Todo: determine limit. ===

            // === Now, for each frame ===
            var LstHexSub = new List<string>();
            var LstHexFrame = new List<string>();
            foreach (ClsFrame ObjFrame in Frames)
            {

                // Get the amount of bytes for each frame
                // But: the ZT1 Graphic format also expects to specify FIRST how many bytes there are for the frame

                // 20190823: the comment below doesn't make sense, together with LastUpdated. CoreImageHex is a property.
                // Just to make sure: force re-rendering
                // ObjFrame.LastUpdated = vbNullString
                LstHexFrame = ObjFrame.CoreImageHex;

                // Specify number of bytes of this frame first.
                LstHexSub.AddRange(Strings.Split(LstHexFrame.Count.ToString("X8").ReverseHex(), " "), false);

                // Add those frame bytes.
                LstHexSub.AddRange(LstHexFrame, false);
            }

        800:
            ;
            LstHexGraphic.AddRange(LstHexSub, false);
            MdlZTStudio.Trace(GetType().FullName, "Write", "Processed all hex values, ready To write file");
        1000:
            ;

            // Working around a possible bug?
            // 20190823 - which bug? Warning? Or nothing at all?
            File.Delete(FileName);
            var ObjFileStream = new FileStream(FileName, FileMode.CreateNew, FileAccess.Write);
        1001:
            ;
            foreach (string StrHexValue in LstHexGraphic)
            {
            1002:
                ;
                ObjFileStream.WriteByte(Conversions.ToByte("&H" + StrHexValue));
            }

        1003:
            ;
            ObjFileStream.Close();
            ObjFileStream.Dispose();

        // Do not forget: color palette must also be created!
        // This is only done if it has the same name (to avoid messing up shared palettes)
        1100:
            ;
            if ((ColorPalette.FileName ?? "") == (FileName + ".pal" ?? ""))
            {
                MdlZTStudio.Trace(GetType().FullName, "Write", "Graphic uses its own color palette. Write.");
                ColorPalette.WritePal(ColorPalette.FileName, true);
            }

        1200:
            ;
            MdlZTStudio.Trace(GetType().FullName, "Write", "Output complete");
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "Write", Information.Err(), true);
        }

        public void RenderFrames()
        {

            // This will render all frames
            foreach (ClsFrame ObjFrame in Frames)
                ObjFrame.GetImage();
        }

        /// <summary>
    /// Just an event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
    /// Some updates to the graphic object should result in an update of displayed information.
    /// </summary>
    /// <param name="StrProperty"></param>
        private void NotifyPropertyChanged(string StrProperty)
        {
            var LstProperties = new List<string>() { "CoreImageBitmap", "CoreImageHex", "FileName" };
            if (LstProperties.Contains(StrProperty) == true)
            {
                // no need to change LastUpdated here?
                return;
            }

            // This will trigger a refresh if it's the main graphic
            if (MdlSettings.BlnTaskRunning == false)
            {
                MdlZTStudioUI.UpdateFrameInfo("Property of graphic changed: " + StrProperty);
            }

            // RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
        }



        /// <summary>
    /// On initializing, set parent of color palette
    /// </summary>
        public ClsGraphic(ClsPalette ObjPalette)
        {
            ClsGraphic_Palette = new ClsPalette(this); // Main color palette
            if (Information.IsNothing(ObjPalette) == false)
            {
                ColorPalette = ObjPalette;
            }

            // Make sure the color palette knows this graphic is it's parent
            // (not remembered by the actual .pal file)
            ColorPalette.Parent = this;
        }

        /// <summary>
    /// Experimental method. Writes info about graphic and each frame to a .nfo file (same name as graphic).
    /// To be used for re-importing (keeping correct offsets), perhaps discovering mystery bytes etc.
    /// </summary>
        public void WriteInfo()
        {
            string StrDestinationFileName = FileName + ".nfo";
            int IntFrameIndex = 0;
            MdlSettings.IniWrite(StrDestinationFileName, "Graphic", "animationSpeed", AnimationSpeed.ToString());
            MdlSettings.IniWrite(StrDestinationFileName, "Graphic", "frameCount", Frames.Count.ToString());
            MdlSettings.IniWrite(StrDestinationFileName, "Graphic", "palFile", ColorPalette.FileName);
            MdlSettings.IniWrite(StrDestinationFileName, "Graphic", "hasBackgroundFrame", HasBackgroundFrame.ToString());
            foreach (var ObjFrame in Frames)
            {
                // todo: check if this works with identical frames? Still proper index?
                string StrSection = "Frame" + IntFrameIndex.ToString();
                MdlSettings.IniWrite(StrDestinationFileName, StrSection, "offsetX", ObjFrame.OffsetX.ToString());
                MdlSettings.IniWrite(StrDestinationFileName, StrSection, "offsetY", ObjFrame.OffsetY.ToString());
                MdlSettings.IniWrite(StrDestinationFileName, StrSection, "height", ObjFrame.CoreImageBitmap.Height.ToString());
                MdlSettings.IniWrite(StrDestinationFileName, StrSection, "width", ObjFrame.CoreImageBitmap.Width.ToString());
                MdlSettings.IniWrite(StrDestinationFileName, StrSection, "numBytes", ObjFrame.CoreImageHex.Count.ToString());
                MdlSettings.IniWrite(StrDestinationFileName, StrSection, "mysteryBytes", string.Join(" ", ObjFrame.MysteryHEX));
                IntFrameIndex += 1;
            }
        }
    }
}