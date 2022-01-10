using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{

    /// <summary>
/// ClsPalette is a class to process the ZT1 Color Palette (.pal file)
/// </summary>
    public class ClsPalette
    {
        private string Pal_FileName = Constants.vbNullString; // The filename
        private List<Color> Pal_Colors = new List<Color>(); // The actual list of colors. Should be 256 maximum, with the first one signifying the color that will be considered 'transparent'.
        private ClsGraphic Pal_Parent = null; // The graphic which is owner of this palette (not always set!)

        /// <summary>
    /// Creates a new instance of this class. Sets the parent graphic.
    /// </summary>
    /// <param name="ObjParent">ClsGraphic or Nothing. The parent of this color palette.</param>
        public ClsPalette(ClsGraphic ObjParent)
        {
            Pal_Parent = ObjParent;
        }

        /// <summary>
    /// The parent of this graphic. A ClsGraphic or nothing.
    /// </summary>
    /// <returns>ClsGraphic or Nothing</returns>
        public ClsGraphic Parent
        {
            get
            {
                return Pal_Parent;
            }

            set
            {
                Pal_Parent = value;
            }
        }

        /// <summary>
    /// Filename of this color palette
    /// </summary>
    /// <returns></returns>
        public string FileName
        {
            get
            {
                return Pal_FileName;
            }

            set
            {
                Pal_FileName = value;
            }
        }

        /// <summary>
    /// The color palette. Maximum 256 colors in total, the first one being a color that will be rendered entirely transparent.
    /// </summary>
    /// <returns></returns>
        public List<Color> Colors
        {
            get
            {
                return Pal_Colors;
            }

            set
            {
                Pal_Colors = value;
            }
        }


        /// <summary>
    /// Reads a ZT1 Color palette (.pal file).
    /// </summary>
    /// <param name="StrFileName">Optional source filename. If not specified, defaults to filename property if already set</param>
        public void ReadPal(string StrFileName = Constants.vbNullString)
        {
            if (!string.IsNullOrEmpty(StrFileName))
            {
                FileName = StrFileName;
            }

            // File does not exist.
            if (File.Exists(FileName) == false)
            {
                // Fatal error if used for a graphic. Any further processing of graphics could lead to issues.
                MdlZTStudio.HandledError(GetType().FullName, "ReadPal", "Could not find '" + Pal_FileName + "'", Information.IsNothing(Parent) == false);
            }

            // Read full file.
            var ArrBytes = File.ReadAllBytes(Pal_FileName);
            var ArrHex = Array.ConvertAll(ArrBytes, b => b.ToString("X2"));

            // Now, the first bytes tell us how many colors there are.
            // ZT1 Graphics only support a limited amount of colors (255?)
            // So only the first 2 bytes (rather than the first 4) signal how many blocks of 4 bytes will follow.
            int Pal_NumberOfColors = Conversions.ToInteger("&H" + Conversion.Hex(1) + Conversion.Hex(0)); // - 1 

            // Jump to what matters.
            ArrHex = ArrHex.Skip(4).ToArray();
            Colors = new List<Color>();

            // Read number of colors. Only 3 bytes per color are relevant. So starting from byte 8, then 12, 16, 20...
            // One byte can be ignored safely as it is nearly always (FF). Refers to opacity of a color, but unused in the game.
            while (ArrHex.Length > 0)
            {

                // Turn bytes/hex values into a color
                var ObjColor = Color.FromArgb(Conversions.ToInteger("&H" + ArrHex[3]), Conversions.ToInteger("&H" + ArrHex[0]), Conversions.ToInteger("&H" + ArrHex[1]), Conversions.ToInteger("&H" + ArrHex[2]));
                MdlZTStudio.Trace(GetType().FullName, "ReadPal", "Color is " + ObjColor.ToArgb().ToString());
                Colors.Add(ObjColor, false);

                // Remove these bytes
                ArrHex = ArrHex.Skip(4).ToArray();
            }
        }

        /// <summary>
    /// Fills a datagridview with all colors. Changes the row heading to match the color. Implements some performance-boosts.
    /// </summary>
    /// <param name="ObjDataGridView">The datagridview where colors should be shown in</param>
        public void FillPaletteGrid(DataGridView ObjDataGridView)
        {
            MdlZTStudio.Trace(GetType().FullName, "FillPaletteGrid", "Preparing to add all colors to datagridview");

            // This is done to greatly improve the speed of drawing.
            // Something weird is going on though. Later in this code, all colors will be processed
            ObjDataGridView.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, ObjDataGridView, new object[] { true });

            // Clear previous colors
            // (this might be necessary if a palette was rendered previously in the same DataGridView)
            ObjDataGridView.Rows.Clear();

            // In the past, the rows were created and added into an array. The benefit is that all rows could be added at once.
            // However, it turned out the AddRange() method took 5-6 seconds; while adding it to the DataGridView right away takes only 2/3 secs
            int IntAutoNumber = 0; // Using this for autonumbering. Alternative would have been .IndexOf , but this is quicker.

            // Prevent visible updates in between.
            ObjDataGridView.Visible = false;
            foreach (Color ObjColor in Colors)
            {
                var ObjRow = new DataGridViewRow();

                // The first color is actually transparent, but show it opaque in the DataGridView 
                ObjRow.DefaultCellStyle.BackColor = Color.FromArgb(255, ObjColor);
                ObjRow.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, ObjColor);
                ObjRow.CreateCells(ObjDataGridView);
                ObjRow.HeaderCell.Value = IntAutoNumber.ToString("0");
                ObjRow.Cells[0].Value = IntAutoNumber.ToString("X2");
                ObjDataGridView.Rows.Add(ObjRow);
                IntAutoNumber += 1;
            }

            // Make the DataGridView visible again, everything has bene added.
            ObjDataGridView.Visible = true;
            MdlZTStudio.Trace(GetType().FullName, "FillPaletteGrid", "Added all colors to datagridview");
        }

        /// <summary>
    /// Returns the index of a color within this palette.
    /// </summary>
    /// <param name="ObjColor">The color of which the index in this palette should be returned</param>
    /// <param name="BlnAddToPalette">Add the color to the palette if it's not present</param>
    /// <returns></returns>
        public int GetColorIndex(Color ObjColor, bool BlnAddToPalette = true)
        {
            if (Colors.Count == 0)
            {
                // This is a new color palette with no colors defined yet.
                // Define the first color (transparent color) in this palette.
                Colors.Add(Color.FromArgb(0, MdlSettings.Cfg_Grid_BackGroundColor), false);
            }

            // Store so we don't need to call both .Contains() and .LastIndexOf()
            int IntColorIndex = Colors.LastIndexOf(ObjColor);
            if (IntColorIndex >= 0)
            {

                // Color has been found, return the index
                // restrant.pal has a color listed twice.
                // The graphic seems to rely on the last index.
                return IntColorIndex;
            }
            else if (ObjColor.A == 0)
            {

                // This color palette uses a different transparent color.
                // However, the .PNG contained a color with with alpha = 0 (transparent)
                return 0;
            }
            else if (ObjColor == MdlSettings.Cfg_Grid_BackGroundColor)
            {

                // The images being imported use a color which has been explicitly set as the background (or transparent) color in ZT Studio.
                return 0;
            }
            else if (ObjColor.A == 255 & ObjColor.R == Colors[0].R & ObjColor.G == Colors[0].G & ObjColor.B == Colors[0].B)
            {

                // Hotfix for opacity issue
                // The specified color is opaque, but RGB-values are identical to the transparent color within this palette
                return 0;
            }

            // The color was not found in this palette. Add it?

            else if (Colors.Count < 256 & BlnAddToPalette == true)
            {

                // Add the color: the maximum number of colors has not been reached and it's been explicitly allowed to do so.
                Colors.Add(ObjColor, false);
                return Colors.Count - 1;  // Return last item index
            }
            else if (Colors.Count == 256)
            {
                MdlZTStudio.Trace(GetType().FullName, "GetColorIndex", "Reached maximum amount of colors.");

                // No decision made yet
                if (MdlSettings.Cfg_Palette_Quantization == 0)
                {
                    if (Interaction.MsgBox("The current palette (" + FileName + ") already contains the maximum amount of colors (" + Colors.Count + ")." + Constants.vbCrLf + Constants.vbCrLf + "Color: " + ObjColor.ToString() + Constants.vbCrLf + "Transparent color: " + Colors[0].ToString() + Constants.vbCrLf + "Graphic: " + Parent.FileName + Constants.vbCrLf + Constants.vbCrLf + "ZT Studio can pick the closest matching color used so far." + Constants.vbCrLf + "You can expect a degradation in graphic quality." + Constants.vbCrLf + "Press [Yes] to ignore all warnings until you close ZT Studio." + Constants.vbCrLf + "Press [No] to quit ZT Studio and fix things first.", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbCritical + (int)Constants.vbApplicationModal), "Too many colors!") == Constants.vbYes)
                    {
                        MdlSettings.Cfg_Palette_Quantization = 1;
                        MdlZTStudio.Trace(GetType().FullName, "GetColorIndex", "User opted to use color quantization.");
                    }
                    else
                    {

                        // Quit ZT Studio, there will be too many errors popping up after this.
                        MdlZTStudio.Trace(GetType().FullName, "GetColorIndex", "User opted to quit ZT Studio.");
                        Environment.Exit(0);
                    }
                }

                // Color quantization method by HENDRIX 
                // Checking in HSV space to find the closest color in the full palette.
                float h1;
                float S1;
                float v1;
                float h2;
                float S2;
                float v2;
                var LstDistances = new List<short>();
                h1 = ObjColor.GetHue();
                S1 = ObjColor.GetSaturation();
                v1 = ObjColor.GetBrightness();
                foreach (Color ObjColorInPalette in Colors)
                {
                    h2 = h1 - ObjColorInPalette.GetHue();
                    S2 = S1 - ObjColorInPalette.GetSaturation();
                    v2 = v1 - ObjColorInPalette.GetBrightness();

                    // In HSV it's possible to use simple euclidean distance. Results are reasonably good.
                    LstDistances.Add((short)Math.Round(Math.Sqrt(h2 * h2 + S2 * S2 + v2 * v2)));
                }

                // See at which index in the existing color palette the least distance occured
                return LstDistances.LastIndexOf(LstDistances.Min());
            }
            else
            {
                MdlZTStudio.HandledError(GetType().FullName, "GetColorIndex", "Unexpected case: not allowed to add colors, but only " + Colors.Count + " colors in the palette?", true);
            }

            return -1; // Will never be reached, just for the sake of not getting a warning about this function not returning anything in some paths.
        }


        /// <summary>
    /// Writes the color palette to the specified filename.
    /// </summary>
    /// <param name="StrFileName">Destination filename. Must be specified.</param>
    /// <param name="BlnOverwrite">Overwrite the destination file.</param>
    /// <remarks>
    /// This sub always overwrites the destination file at this point.
    /// </remarks>
        public void WritePal(string StrFileName, bool BlnOverwrite)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 14009


        Input:

                On Error GoTo dBug

         */
        1:
            ;

            // This check is redundant as of now (24th of August 2019), but could be re-implemented in the future.
            if (File.Exists(StrFileName) == true & BlnOverwrite == false)
            {
                MdlZTStudio.HandledError(GetType().FullName, "WritePal", "Can not overwrite the color palette file '" + StrFileName + "'.", true);
            }

            FileName = StrFileName;
            MdlZTStudio.Trace(GetType().FullName, "WritePal", "Writing color palette of " + Colors.Count + " colors to " + FileName);
        10:
            ;
            var LstOutputHexValues = new List<string>(); // output hex
            int IntX; // used to loop through the colors

            // Start with the number of colors to process
            LstOutputHexValues.Add(Strings.Right(Colors.Count.ToString("X4"), 2), false);
            LstOutputHexValues.Add(Strings.Left(Colors.Count.ToString("X4"), 2), false);
            LstOutputHexValues.Add("00", false);
            LstOutputHexValues.Add("00", false);
        20:
            ;
            var loopTo = Colors.Count - 1;
            for (IntX = 0; IntX <= loopTo; IntX++)
            {
                LstOutputHexValues.Add(Colors[IntX].R.ToString("X2"), false);
                LstOutputHexValues.Add(Colors[IntX].G.ToString("X2"), false);
                LstOutputHexValues.Add(Colors[IntX].B.ToString("X2"), false);

                // Only the first color is transparent (00), all others are opaque (FF)
                if (IntX == 0)
                {
                    LstOutputHexValues.Add("00", false);
                }
                else
                {
                    LstOutputHexValues.Add("FF", false);
                }
            }

        1000:
            ;
            var ObjFileStream = new FileStream(StrFileName, FileMode.OpenOrCreate, FileAccess.Write);
            foreach (string StrHexValue in LstOutputHexValues)
                ObjFileStream.WriteByte(Conversions.ToByte("&H" + StrHexValue));
            ObjFileStream.Close();
            ObjFileStream.Dispose();
            MdlZTStudio.Trace(GetType().FullName, "WritePal", "Finished writing .pal file");
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "WritePal", Information.Err(), true);
        }


        // === extra functions ===

        /// <summary>
    /// Unused function. Should be moved to a module instead.
    /// Was originally intended for a feature where ZT Studio would automatically improve graphics, also by combining color palettes (shared palette)
    /// </summary>
    /// <param name="LstColorPalettes">List of ClsPalette objects to combine</param>
    /// <returns>ObjColorPalette - a new color palette (combination of all the source palettes)</returns>
        public ClsPalette CombineColorPalettes(List<ClsPalette> LstColorPalettes)
        {

            // This function should allow to create/combine color palettes.
            // There needs to be a check if there aren't too many colors! 

            Debug.Print("Combine color palettes.");
            var ObjCombinedPalette = new ClsPalette(null);

            // for each color palette: check if color exists in the new palette.
            foreach (ClsPalette ObjColorPalette in LstColorPalettes)
            {
                foreach (Color ObjColor in ObjColorPalette.Colors)

                    // Add color if it's new
                    ObjCombinedPalette.GetColorIndex(ObjColor, true);
            }

            return ObjCombinedPalette;
        }

        /// <summary>
    /// Exports color palette to a .PNG file. Dimensions are 16x16, which leaves room for 256 colors. 
    /// <para>Warning: overwrites file without asking</para>
    /// </summary>
    /// <remarks>
    /// Recoloring is popular in ZT1 circles to create "new" animals, but they often relied on two main methods:
    /// * recoloring each frame individually, then re-import it into APE
    /// * recolor one frame which contains most colors used in the animal, then use this to replace the .pal file
    /// 
    /// The idea is that the .PNG can easily be colored with any third party graphic image manipulation program (such as GIMP, but also Paint.NET, PhotoShop etc.)
    /// The entire palette of an existing animal can be recolored at once. And it can be reimported in a later step.
    /// </remarks>
    /// <param name="StrExportFileName">Destination file name</param>
        public void ExportToPNG(string StrExportFileName)
        {
            var Bmp = new Bitmap(16, 16);

            // Perform Drawing here
            int IntX = 0; // Will be used to process a bitmap from left to right
            int IntY = 0; // Will be used to process a bitmap from top to bottom
            var IntColor = default(int);
            MdlZTStudio.Trace(GetType().FullName, "ExportToPNG", "Exporting color palette as .PNG to " + StrExportFileName);

            // Todo: optimize SetPixel()
            // For each row
            while (IntY < 16)
            {

                // for each col
                while (IntX < 16 & IntColor < Colors.Count)
                {
                    Bmp.SetPixel(IntX, IntY, Colors[IntColor]);
                    IntColor += 1;
                    IntX += 1;
                }

                // reset, next line
                IntX = 0;
                IntY += 1;
            }

            if (File.Exists(StrExportFileName) == true)
            {
                MdlZTStudio.Trace(GetType().FullName, "ExportToPNG", "Overwriting existing file!");
                File.Delete(StrExportFileName);
            }

            Bmp.Save(StrExportFileName, System.Drawing.Imaging.ImageFormat.Png);
            Bmp.Dispose();
            MdlZTStudio.Trace(GetType().FullName, "ExportToPNG", "Finished exporting color palette as .PNG");
        }

        /// <summary>
    /// Imports color palette from a specially prepared .PNG file
    /// </summary>
    /// <remarks>See ExportToPNG()</remarks>
    /// <param name="StrFileName">Source file name</param>
        public void ImportFromPNG(string StrFileName)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
            /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBg' at character 20366


            Input:

                    On Error GoTo dBg

             */
            Bitmap BmpSource = (Bitmap)Image.FromFile(StrFileName);
            MdlZTStudio.Trace(GetType().FullName, "ImportFromPNG", "Importing color palette from .PNG: " + StrFileName);
            MdlZTStudio.Trace(GetType().FullName, "ImportFromPNG", "Forcefully add colors: " + MdlSettings.Cfg_Palette_Import_PNG_Force_Add_Colors);
            int IntX = 0; // Used to process bitmap from left to right
            int IntY = 0; // Used to process bitmap from top to bottom

            // Todo: implement better method than GetPixel(), although performance boost will be minimal here.

            // Clear current palette (please prevent redraws at this point)
            Colors.Clear(false);

            // Row by row
            while (IntY < BmpSource.Height)
            {
                while (IntX < BmpSource.Width)
                {

                    // Do not add duplicate colors, e.g. transparent stuff etc; UNLESS it's forced (= a user setting)
                    // Use case: After recoloring, some colors are suddenly identical (especially after they're made brighter or darker). 
                    // Keep in mind that the graphics still refer to the original indexes of their colors.
                    // Adding duplicate colors in that case causes least problems.

                    // Color is unknown or it's forcefully added
                    if (Colors.IndexOf(BmpSource.GetPixel(IntX, IntY)) < 0 | MdlSettings.Cfg_Palette_Import_PNG_Force_Add_Colors == 1)
                    {
                        Colors.Add(BmpSource.GetPixel(IntX, IntY), false);
                    }

                    IntX += 1;
                }

                // Reset and start next line
                IntX = 0;
                IntY += 1;
            }

        200:
            ;


            // There's actually two possibilities here.
            // Either regenerate the list of hex values for each frame in the parent graphic, since colors might have switched places.
            // Or regenerate the image, since it might just be a recolor (relying on this option for now) 
            if (Information.IsNothing(Parent) == false)
            {
                foreach (ClsFrame ObjFrame in Parent.Frames)
                {
                    ObjFrame.CoreImageBitmap = null;
                    ObjFrame.GetCoreImageBitmap();
                }
            }

            MdlZTStudio.Trace(GetType().FullName, "ImportFromPNG", "Finished importing color palette from .PNG");
            return;
        dBg:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "ImportFromGPL", Information.Err(), true);
        }

        /// <summary>
    /// Import colors from a GIMP Color Palette (.gpl)
    /// </summary>
    /// <remarks>
    /// This is specifically developed due to the original author's preference for GIMP and since it's open source.
    /// It is not intended to support other file formats at this point.
    /// </remarks>
    /// <param name="StrFileName">Source filename</param>
        public void ImportFromGIMPPalette(string StrFileName)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBg' at character 23561


        Input:

                On Error GoTo dBg

         */
        0:
            ;

        // Typical file contents of a .GPL file: 

        // GIMP Palette
        // Name:   NameOfPaletteGoesHere
        // Columns: 16
        // #
        // 0   1   0	#0
        // <line for each color>
        // 254 255 252	#254

        10:
            ;
            var ObjReader = new StreamReader(StrFileName);
            string StrTextLine = "";
            int IntLine = 1; // Keep in mind, started line numbering, so starting from 1 !

            // Clear current palette (please prevent redraws at this point)
            Colors.Clear(false);

            // Read file.
            while (ObjReader.Peek() != -1)
            {
            11:
                ;
                StrTextLine = ObjReader.ReadLine();

                // Remove double white spaces etc 
                StrTextLine = Strings.Trim(System.Text.RegularExpressions.Regex.Replace(StrTextLine, @"\s+", " "));

                // Ignore the first few lines of the GPL file (5 in that GIMP version) AND the transparent color
                if (IntLine == 5 & !string.IsNullOrEmpty(StrTextLine))
                {
                21:
                    ;

                    // The GetColorIndex() method would add a transparent color if called.
                    // Transparent color must be added manually, without looking up.
                    Colors.Add(Color.FromArgb(Conversions.ToInteger(Strings.Split(StrTextLine, " ")[0]), Conversions.ToInteger(Strings.Split(StrTextLine, " ")[1]), Conversions.ToInteger(Strings.Split(StrTextLine, " ")[2])));
                }
                else if (IntLine > 5 & !string.IsNullOrEmpty(StrTextLine))
                {
                22:
                    ;

                    // Add to this color palette. Using GetColorIndex(color, True), it will prevent duplicates.
                    GetColorIndex(Color.FromArgb(Conversions.ToInteger(Strings.Split(StrTextLine, " ")[0]), Conversions.ToInteger(Strings.Split(StrTextLine, " ")[1]), Conversions.ToInteger(Strings.Split(StrTextLine, " ")[2])), true);
                }

                // Next
                IntLine += 1;
            }

        200:
            ;

            // There's actually two possibilities here.
            // Either regenerate the list of hex values for each frame in the parent graphic, since colors might have switched places.
            // Or regenerate the image, since it might just be a recolor (relying on this option for now) 
            if (Information.IsNothing(Parent) == false)
            {
                foreach (ClsFrame ObjFrame in Parent.Frames)
                {
                    ObjFrame.CoreImageBitmap = null;
                    ObjFrame.GetCoreImageBitmap();
                }
            }

            MdlZTStudio.Trace(GetType().FullName, "ImportFromPNG", "Finished importing color palette from .GPL");
            return;
        dBg:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "ImportFromGPL", Information.Err(), true);
        }
    }
}