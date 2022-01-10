using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{

    /// <summary>
/// <para>ClsFrame is an object to handle a ZT1 Frame.</para>
/// <para>A frame is one still picture of view of an animation.</para>
/// </summary>
/// <remarks>
/// It contains some properties such as width, height, offset; as well as a series of drawing instructions.
/// In 2019, there are still two "mystery bytes" of which the function is still unknown.
/// It's assumed the purpose of all other bytes has been discovered.
/// </remarks>
    public class ClsFrame : INotifyPropertyChanged
    {

        // CoreImage means the actual frame's content. No background canvas, no grid, no 'extra frame'.
        // The bitmap also implictly contains the width and height of this 'core' image. 
        private ClsDirectBitmap fr_CoreImageBitmap = null;
        private List<string> fr_CoreImageHex = new List<string>(); // contains height/width and offsets after all.
        private int fr_OffsetX = -9999;
        private int fr_OffsetY = -9999;
        private ClsGraphic fr_Parent = new ClsGraphic(null);
        private List<string> fr_MysteryHEX = new List<string>();
        private string fr_LastUpdated = DateAndTime.Now.ToString("yyyyMMddHHmmss");                // for caching purposes.
        private bool fr_IsShadowFormat = false;



        // === Regular

        /// <summary>
    /// New frame object has been created.
    /// </summary>
    /// <param name="myParent">ClsGraphic - the graphic (view) of which this frame is part</param>
        public ClsFrame(ClsGraphic myParent)
        {

            // Set parent
            Parent = myParent;

            // 20170512 - consider automatically adding this frame to the parent's frame collection?

        }

        /// <summary>
    /// Hex values for this ZT1 frame (if set)
    /// </summary>
    /// <returns>List(Of String)</returns>
        public List<string> CoreImageHex
        {
            get
            {
                return fr_CoreImageHex;
            }

            set
            {
                fr_CoreImageHex = value;
                NotifyPropertyChanged("CoreImageHex");
            }
        }


        /// <summary>
    /// Image bitmap (if set)
    /// </summary>
    /// <returns>Bitmap</returns>
        public ClsDirectBitmap CoreImageBitmap
        {
            // What is the core image bitmap?
            get
            {
                return fr_CoreImageBitmap;
            }

            set
            {
                fr_CoreImageBitmap = value;
                NotifyPropertyChanged("CoreImageBitmap");
            }
        }

        /// <summary>
    /// Parent graphic of this ZT1 frame
    /// </summary>
    /// <returns>ClsGraphic</returns>
        public ClsGraphic Parent
        {
            // What is the parent object (ClsGraphic) of our frame? 
            // Or in other words: which ZT1 Graphic does this frame belong to?
            get
            {
                return fr_Parent;
            }

            set
            {
                fr_Parent = value;
                NotifyPropertyChanged("Parent");
            }
        }

        /// <summary>
    /// Horizontal offset (X) of this frame.
    /// How much should the image be moved to the left (+)/right (-), compared to the center of the square? (center is based on ZT1's cFootPrintX and cFootPrintY settings)
    /// </summary>
    /// <returns>Integer</returns>
        public int OffsetX
        {
            get
            {
                return fr_OffsetX;
            }

            set
            {
                fr_OffsetX = value;
                NotifyPropertyChanged("OffsetX");
            }
        }

        /// <summary>
    /// Vertical offset (Y) of this frame.
    /// How much should the image be moved to the top (+)/bottom (-), compared to the center of the square? (center is based on ZT1's cFootPrintX and cFootPrintY settings)
    /// </summary>
    /// <returns>Integer</returns>
        public int OffsetY
        {
            get
            {
                return fr_OffsetY;
            }

            set
            {
                fr_OffsetY = value;
                NotifyPropertyChanged("OffsetY");
            }
        }

        /// <summary>
    /// Timestamp of last update. Used to see if re-rendering the frame is needed.
    /// </summary>
    /// <returns>Timestamp</returns>
        public string LastUpdated
        {
            // Used to see if re-rendering is needed.
            get
            {
                return fr_LastUpdated;
            }

            set
            {
                fr_LastUpdated = value;
                // NotifyPropertyChanged("LastUpdated") -> would cause a loop
            }
        }

        /// <summary>
    /// Whether this is the Marine Mania shadow format or not
    /// </summary>
    /// <returns>Boolean</returns>
        public bool IsShadowFormat
        {
            get
            {
                return fr_IsShadowFormat;
            }

            set
            {
                fr_IsShadowFormat = value;
            }
        }

        /// <summary>
    /// <para>Mystery bytes/hex is the term used in this project to refer to two bytes present in the hex values of each frame.</para>
    /// <para>Until now (16th of August 2019), the purpose of these 2 bytes is unknown. It's not been discovered yet if they have any purpose in the game.</para>
    /// </summary>
    /// <remarks>
    /// Are they:
    /// - another counter?
    /// - another integrity check?
    /// - a signature?
    /// - some value used in the program used by Blue Fang/Rapan Animation Studio (Rapan LLC. Sofia) to create the graphics?
    /// - ...
    /// 
    /// If anyone ever finds out, please let me know!
    /// </remarks>
    /// <returns>List(Of String)</returns>
        public List<string> MysteryHEX
        {
            // This is used to store our currently 2 unknown bytes. We call them our mystery bytes.
            get
            {
                return fr_MysteryHEX;
            }

            set
            {
                fr_MysteryHEX = value;
                NotifyPropertyChanged("MysteryHEX");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
    /// This sub is used to inform the object that an important property has changed.
    /// By calling this sub, the LastUpdated property will be set.
    /// </summary>
        private void NotifyPropertyChanged(string StrSource)
        {
            LastUpdated = Parent.LastUpdated; // Now.ToString("yyyyMMddHHmmss")

            // clsTasks.update_Info("Property clsFrame." & info & " changed.") -> for debugging
            // RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))

            // Important events to detect are:
            // - if CoreImageHex changed (which happens when a ZT1 Graphic is read), 
            // --->  it should trigger an update of CoreImageBitmap too - but without causing that one to trigger a new unwanted change.
            // - if CoreImageBitmap changed (after a .PNG is loaded), coreImageHex should be updated without triggering another property change.


        }

        /// <summary>
    /// <para>Returns the core bitmap image.</para>
    /// <para>If the ZT1 Graphic has been rendered (CoreImageBitmap is set), it returns this cached version.</para>
    /// <para>If the ZT1 Graphic hasn't been rendered yet, it renders the ZT1 Graphic from the hex values.</para>
    /// </summary>
    /// <returns>ClsDirectBitmap</returns>
        public ClsDirectBitmap GetCoreImageBitmap()
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 8049


        Input:

                On Error GoTo dBug

         */
        11:
            ;
            if (Information.IsNothing(CoreImageBitmap) == true)
            {
            12:
                ;

                // Rendering the frame from hex will store it in the CoreImageBitmap.
                MdlZTStudio.Trace(GetType().FullName, "GetCoreImageBitmap", "Need to render bitmap...");
                return RenderCoreImageFromHex();
            }
            else
            {
            13:
                ;
                MdlZTStudio.Trace(GetType().FullName, "GetCoreImageBitmap", "Using cached bitmap, w = " + CoreImageBitmap.Width + ", h = " + CoreImageBitmap.Height + "...");
                return CoreImageBitmap;
            }

        21:
            ;
            return default;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "GetCoreImageBitmap", Information.Err(), true);
        }

        /// <summary>
    /// Returns the image bitmap, on a transparent canvas.
    /// </summary>
    /// <remarks>
    /// Todo: this needs more documentation. Take the time again to see how blnDrawInCenter works.
    /// </remarks>
    /// <param name="BlnDrawInCenter">Boolean</param>
    /// <returns>ClsDirectBitmap</returns>
        public ClsDirectBitmap GetCoreImageBitmapOnTransparentCanvas(bool BlnDrawInCenter = false)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 9392


        Input:

                On Error GoTo dBug

         */
        1:
            ;
            var IntWidth = default(int);
            var IntHeight = default(int);
        2:
            ;


            // Retrieve the pure bitmap
            var BmCoreImageBitMap = GetCoreImageBitmap();
        3:
            ;
            if (Information.IsNothing(BmCoreImageBitMap) == true)
            {
                return null;
            }

        4:
            ;


            // Determine how big the canvas will be.
            // Continue reading below, as only the width/height are determined first, but only multiplied by 2 later!
            switch (BlnDrawInCenter)
            {
                case false:
                    {
                        // Draw on transparent canvas (most common scenario)
                        // It's important to keep in mind that the canvas is by default 2 * Cfg_grid_numPixels, both in width and in height.
                        // So these variables will actually contain the top left pixel of the (four pixel) center.
                        IntWidth = MdlSettings.Cfg_Grid_NumPixels;
                        IntHeight = MdlSettings.Cfg_Grid_NumPixels;
                        break;
                    }

                case true:
                    {
                        // Convert everything relative to the center. Method contributed by HENDRIX.
                        // The idea is to generate a canvas around the center. By adding some spacing to the left/right or top/bottom, 
                        // ZT Studio's import mechanisms will automatically apply the correct offsets again in ClsFrame::LoadPNG()
                        // There, the initial offset (before being corrected) is already determined by width /2 and height / 2
                        // Pick whatever is bigger: the absolute value of the  actual offset compared to the center; or of the offset (+ or -) minus the width/height
                        // 
                        // Some examples (based on width) make this easier to understand.
                        // Basically it tries to find which side (left or right of the center) is the largest.
                        // Left part is easy to understand; as for right part here are some examples based on abs(offset-width)
                        // Positive offset = to left of center; negative = to right of center
                        // Offset abs(0) = 0 | abs(0 - 50) = 50 ---> max is 50
                        // Offset abs(5) = 5 | abs(5-50) = 45 ---> max is 45
                        // Offset abs(5) = 5 | abs(5-4) = 1 ---> max is 5
                        // Offset abs(-5) = 5 | abs(-5-50) = 55 ---> max is 55
                        // Offset abs(-5) = 5 | abs(-5-4) = 9 ---> max is 9

                        // It seems the + and -1 are needed to avoid changing the image size
                        // Keep in mind this is multiplied by 2 further in the code!

                        // 20191005: 
                        // Why would + 1 And -1 be needed here? They cause a bug with the 1x1 canvas of objects/restrant/idle/NE. 
                        // Removed.If anything; both should be +1
                        IntWidth = Math.Max(Math.Abs(OffsetX), Math.Abs(OffsetX - BmCoreImageBitMap.Width));
                        IntHeight = Math.Max(Math.Abs(OffsetY), Math.Abs(OffsetY - BmCoreImageBitMap.Height));
                        break;
                    }
            }

        5:
            ;
            MdlZTStudio.Trace(GetType().FullName, "GetCoreImageBitmapOnTransparentCanvas", "Create bitmap: w = " + IntWidth + " * 2, h = " + IntHeight + " * 2");

            // Draw this retrieved bitmap on a transparent canvas
            var BmOutput = new ClsDirectBitmap(IntWidth * 2, IntHeight * 2); // Creating the output canvas
            var ObjGraphic = Graphics.FromImage(BmOutput.Bitmap); // Preparing to manipulate this empty canvas
        25:
            ;
            ObjGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor; // Prevent softening: set InterpolationMode to NearestNeighbour
        30:
            ;

            // Onto this canvas, draw the CoreImageBitmap of this frame.
            int IntStartingPointX = IntWidth - OffsetX + 1;
            int IntStartingPointY = IntHeight - OffsetY + 1;
            ObjGraphic.DrawImage(BmCoreImageBitMap.Bitmap, IntStartingPointX, IntStartingPointY, BmCoreImageBitMap.Width, BmCoreImageBitMap.Height);
        31:
            ;
            ObjGraphic.Dispose(); // Dispose as recommended. Output has been stored in BmOutput anyway.
            return BmOutput;
            return default;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "GetCoreImageBitmapOnTransparentCanvas", Information.Err(), true);
        }

        /// <summary>
    /// <para>Returns a bitmap image for this frame.</para>
    /// <para>If a background frame within this graphic exists, it will also be rendered if enabled. (for example: Restaurant)</para>
    /// <para>If a background graphic has been set, it will also be rendered if enabled. (for example: Orang Utan toy)</para>
    /// </summary>
    /// <param name="BlnDrawGrid">Add grid. Defaults to false.</param>
    /// <param name="BlnCentered">Centered</param>
    /// <returns>ClsDirectBitmap</returns>


        // It will render the core image in this frame; and then add backgrounds.
        // There's an option to render the image on top of a visible grid (as you have in ZT1).

        public ClsDirectBitmap GetImage(bool BlnDrawGrid = false, bool BlnCentered = false)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 14800


        Input:


                ' It will render the core image in this frame; and then add backgrounds.
                ' There's an option to render the image on top of a visible grid (as you have in ZT1).

                On Error GoTo dBug

         */
        1:
            ;

            // Draw frame.
            var BmOutput = GetCoreImageBitmapOnTransparentCanvas(BlnCentered);
            ClsDirectBitmap BmFront;
        11:
            ;

            // Draw 'extra' background frame, e.g. restaurants?
            if (Parent.HasBackgroundFrame == 1 & MdlSettings.Cfg_Export_PNG_RenderBGFrame == 1)
            {
                BmFront = Parent.Frames[Parent.Frames.Count - 1].GetCoreImageBitmapOnTransparentCanvas(BlnCentered);
            12:
                ;
                MdlZTStudio.Trace(GetType().FullName, "GetImage", "Combine core bitmap with background frame");
                BmOutput = MdlBitMap.CombineImages(BmFront, BmOutput);
            }

        21:
            ;

            // Optional background ZT1 Graphic frame, e.g. animal + toy?
            if (MdlSettings.EditorBgGraphic.Frames.Count > 0 & MdlSettings.Cfg_Export_PNG_RenderBGZT1 == 1)
            {
                BmFront = MdlSettings.EditorBgGraphic.Frames[0].GetCoreImageBitmapOnTransparentCanvas(BlnCentered);
            22:
                ;
                MdlZTStudio.Trace(GetType().FullName, "GetImage", "Combine core bitmap with optional ZT1 Graphic backround");
                BmOutput = MdlBitMap.CombineImages(BmFront, BmOutput);
            }

        31:
            ;

            // Draw grid?
            if (BlnDrawGrid == true)
            {
                BmFront = MdlBitMap.DrawGridFootPrintXY(MdlSettings.Cfg_Grid_FootPrintX, MdlSettings.Cfg_Grid_FootPrintY);
            32:
                ;
                MdlZTStudio.Trace(GetType().FullName, "GetImage", "Combine core bitmap with grid");
                BmOutput = MdlBitMap.CombineImages(BmFront, BmOutput);
            }

        41:
            ;
            return BmOutput;
            return default;
        dBug:
            ;

            // Not expecting an error here
            MdlZTStudio.UnhandledError(GetType().FullName, "GetImage", Information.Err(), true);
        }


        /// <summary>
    /// <para>Renders the core image of this frame (without grid, offsets etc.) as a bitmap, based on the supplied hex (CoreImageHex).</para>
    /// <para>Warning: while processing the hex, this method (re)sets the offsets for this frame!</para>
    /// <para>For other renderings, use another render method.</para>
    /// </summary>
    /// <returns>Bitmap or Nothing</returns>
        public ClsDirectBitmap RenderCoreImageFromHex()
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
            /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug2' at character 17352


            Input:

                    On Error GoTo dBug2

             */
            MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Start rendering from hex...");
        10:
            ;

            // Only one thing matters: do we actually have HEX?
            if (CoreImageHex.Count == 0)
            {
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "There Is no hex! Returning Nothing instead of Bitmap");
                CoreImageBitmap = null;
                return null;
            }

        30:
            ;

            // Create a copy of this frame's bytes.
            var LstFrameHex = new List<string>();
            LstFrameHex.AddRange(CoreImageHex);
            ClsDirectBitmap ObjFrameCoreImageBitmap; // Contains the core image that will be rendered
            var ZtPal = Parent.ColorPalette;

        // 20191006
        // Height, width is now processed further on; 
        // since this allows the code to simply set offsets to 0 And still detect mystery bytes in case of a short transparent frame.


        41:
            ;

            // Offsets.
            // In case of unknown offsets
            if (LstFrameHex[5] == "FF")
            {
                // Large size images. Needs some adjustment.
                // Todo: add an example image here.
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Byte index 5 = FF -> This graphic should be large in size (offset Y). Add this as an example in the code!");
                OffsetY = (256 * 256 - Conversions.ToInteger("&H" + LstFrameHex[5] + LstFrameHex[4])) * -1;
            }
            else
            {
                // Normal offsets
                OffsetY = Conversions.ToInteger("&H" + LstFrameHex[5] + LstFrameHex[4]);
            }

        42:
            ;

            // In case of unknown offsets and HEX = FF (large size image)
            if (LstFrameHex[7] == "FF")
            {
                // Large size images. Needs some adjustment.
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Byte index 7 = FF -> This graphic should be large in size (offset X). Add this as an example in the code!");
                OffsetX = (256 * 256 - Conversions.ToInteger("&H" + LstFrameHex[7] + LstFrameHex[6])) * -1;
            }
            else
            {
                // Normal offsets
                OffsetX = Conversions.ToInteger("&H" + LstFrameHex[7] + LstFrameHex[6]);
            }

        45:
            ;

            // Entire ZT1 Graphic format is documented, EXCEPT for these 2 bytes.
            // APE usually sets them to 00 00 (or was it 00 01? Needs verification).
            // Anyhow, in ZT1 you will see that these mysterious bytes may vary.
            // In the swinglog graphics (idle, used) (likely one of the earliest animations since the shadow is lacking), it's always 00 01
            {
                var withBlock = MysteryHEX;
                withBlock.Clear(false);
                withBlock.Add(LstFrameHex[8], false);
                withBlock.Add(LstFrameHex[9], false);
            }

            MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Byte index 8, 9 -> the mystery bytes are " + LstFrameHex[8] + ", " + LstFrameHex[9]);
        46:
            ;


            // This case is weird. It's for the Restaurant (objects/restrant/idle/NE).
            // Some views contain 10 bytes: (00 00) (00 00) (00 00) (00 00) (D0 10)
            // The idle view supposedly uses an extraFrame
            // Which means no height/width, no offsets, and yet some weird mystery bytes.
            // The mystery bytes might be the same for similar graphics, which have an empty first frame and an extra frame as background?

            // Perhaps it could be identified by mystery bytes?
            if (LstFrameHex.Count == 10)
            {


                // Basically height = width = 0.
                if (Conversions.ToDouble(LstFrameHex[0]) == 0d & Conversions.ToDouble(LstFrameHex[1]) == 0d & Conversions.ToDouble(LstFrameHex[2]) == 0d & Conversions.ToDouble(LstFrameHex[3]) == 0d)
                {
                    MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Short transparent frame: " + string.Join(" ", LstFrameHex.ToArray()));

                    // It should actually be 0, 0 (empty/nothing); but this is a work around.
                    CoreImageBitmap = new ClsDirectBitmap(1, 1);
                    return CoreImageBitmap;
                }
                else
                {

                    // Unexpected/unknown so far: dimensions but still a "short" frame?
                    MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Short transparent frame look-alike (unhandled): " + string.Join(" ", LstFrameHex.ToArray()));
                    return default;
                }
            }

            // Regular height, width

            // Usually hex(1) = 00. But sometimes, it is "80". This seems to be an indicator introduced in Marine Mania.
            // Example: dolphin's "ssurfswi"-animations. The frames are actually compressed. For shadows, it's only offsets and black.
            // Surprisingly enough, not all shadow animations (even of the dolphin) use this format.
            // Probably due to HEX 80 00 being -32678, which is very unlikely to happen?
            else if (LstFrameHex[1] == "80")
            {
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Byte index 1 = 80 -> assuming this is the compressed shadow format (Marine Mania)");
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Byte index 2 = " + LstFrameHex[2] + " -> still signifies height");
                IsShadowFormat = true;

                // Of course, 80 doesn't make any sense here. Ignore byte index 1.
                // Also ignoring byte index 3 to be consistent, although it could probably work?
                // 20191005: actually animals/dolphin/m/swatring has a larger width than 255 pixels, so index 3 is needed in this width!
                ObjFrameCoreImageBitmap = new ClsDirectBitmap(Conversions.ToInteger("&H" + LstFrameHex[3] + LstFrameHex[2]), Conversions.ToInteger("&H" + LstFrameHex[0]));
            }
            else
            {
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Dealing with a regular sized graphic");
                // All normal cases
                // Canvas size determined: height and width are specified in the first few bytes (reversed).
                ObjFrameCoreImageBitmap = new ClsDirectBitmap(Conversions.ToInteger("&H" + LstFrameHex[3] + LstFrameHex[2]), Conversions.ToInteger("&H" + LstFrameHex[1] + LstFrameHex[0]));
            }

        51:
            ;

            // Above covered the first 10 bytes (height, width, offset Y, offset X, mystery bytes). 
            // Remove them now to speed up further processing. 
            // This is something which will be done a couple of times later.
            // No worries, this is on a copy of the bytes.
            LstFrameHex.Skip(10);
        1000:
            ;

            // === Color instructions ===
            // For clarity, declarations only happen here.
            // Keep in mind that an image is rendered from left to right, from top to bottom.

            int IntX = 0; // which 'row' of pixels is being drawn?
            int IntY = 0; // which 'column' of pixels is being drawn?
            int IntNumDrawingInstructions; // How many drawing instructions are there for this 'row' of pixels?
            int IntNumDrawingInstructions_current; // Which drawing instruction is being processed?
            int IntNumDrawingInstructions_colors; // How many pixels to color?
            int IntNumDrawingInstructions_colors_current; // which is the current pixel being processed/colored?
            Color ObjColor; // this is the color we'll draw. 
        1005:
            ;

            // This 'while'-loop should prevent any side-effects from APE junk bytes.
            // Often, when analyzing graphics generated by APE, you'll notice some unneccessary bytes at the very end.

            while (LstFrameHex.Count > 0 & IntY < ObjFrameCoreImageBitmap.Height)
            {

            // First byte for each row contains the number of pixel sets.
            // That's at least 1 block (a transparent line would give [offset][0 colors] -
            // Otherwise, it's a 'drawing instruction': 
            // [offset/number of pixels to remain transparent][numColorPixels][pixels]

            1100:
                ;

                // Number of drawing instructions. How many are there for this 'row' of pixels?
                // Limitation: theoretically: 0 to 255 drawing instructions per row
                IntNumDrawingInstructions = Conversions.ToInteger("&H" + LstFrameHex[0]);
                LstFrameHex.Skip(1);
            1120:
                ;

                // Process this set of drawing instructions
                var loopTo = IntNumDrawingInstructions - 1;
                for (IntNumDrawingInstructions_current = 0; IntNumDrawingInstructions_current <= loopTo; IntNumDrawingInstructions_current++)
                {
                1300:
                    ;

                    // Starting with color byte( [offset] ). 
                    // If this is 00, starting all the way to the left. 
                    // If this is 01, skipping 1 pixel and leave it transparent.
                    // If this is 02, skipping 2 pixels and leave them transparent
                    // And so on.
                    IntX += Conversions.ToInteger("&H" + LstFrameHex[0]);
                1301:
                    ;

                    // Number of pixels to color ([num of pixels to draw])
                    IntNumDrawingInstructions_colors = Conversions.ToInteger("&H" + LstFrameHex[1]);
                1309:
                    ;

                    // Remove [offset] and [num of pixels to draw] instructions.
                    LstFrameHex.Skip(2);
                1400:
                    ;

                    // The hex code mentioned how many colors there will be
                    var loopTo1 = IntNumDrawingInstructions_colors - 1;
                    for (IntNumDrawingInstructions_colors_current = 0; IntNumDrawingInstructions_colors_current <= loopTo1; IntNumDrawingInstructions_colors_current++)
                    {
                    1410:
                        ;
                        if (IsShadowFormat == true)
                        {
                            // Marine Mania's underwater shadow format (compressed ZT1 Graphic)
                            // It does not rely on the palette.
                            ObjColor = Color.Black;
                        }
                        else
                        {
                            // In the traditional format, the color is referenced by it's index number in the color palette. Get it.
                            byte IntColorIndex = (byte)Conversions.ToInteger("&H" + LstFrameHex[IntNumDrawingInstructions_colors_current]);
                            ObjColor = ZtPal.Colors[IntColorIndex];
                        }

                    1413:
                        ;

                        // Color the pixel.
                        ObjFrameCoreImageBitmap.SetPixel(IntX, IntY, ObjColor);
                    1450:
                        ;

                        // Be ready to draw next pixel.
                        IntX += 1;
                    }

                1455:
                    ;

                    // Rather than individually deleting those colors one by one from the bytes that still need to be processed, do it at once now.
                    if (IsShadowFormat == false)
                    {
                        LstFrameHex.Skip(IntNumDrawingInstructions_colors_current);
                    }

                2040:
                    ;
                }

            2050:
                ;
                IntX = 0; // Start all the way on the left of the canvas again.
                IntY += 1; // Ready to process next line.
            }

        2100:
            ;


            // Implemented a check for APE junk bytes and remove if any are left.
            // Theoretically, there shouldn't be. But APE has the tendency to generate crap. (Sorry Blue Fang, but I'm sure you can agree by now?)
            if (LstFrameHex.Count > 0)
            {
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Detected APE junk bytes!");
            }
            // In the past, this used to be cleaned up. For now, don't even bother.
            // Me.coreImageHex.RemoveRange(Me.coreImageHex.Count - LstFrameHex.Count - 1, LstFrameHex.Count)
            else
            {
                MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "No APE junk bytes.");
            }

        2110:
            ;

            // The actual bitmap won't be changed unless a .PNG is loaded.
            // If a .PNG is loaded, this frame's CoreImageHex should be updated as well
            // Me.CoreImageBitmap = ObjFrameCoreImageBitmap.Bitmap
            CoreImageBitmap = ObjFrameCoreImageBitmap;
            MdlZTStudio.Trace(GetType().FullName, "RenderCoreImageFromHex", "Finished frame rendering.");
        9999:
            ;
            return CoreImageBitmap;
            return default;
        dBug2:
            ;
            string StrErrorMessage = "Width, height: " + ObjFrameCoreImageBitmap.Width + ", " + ObjFrameCoreImageBitmap.Height + Constants.vbCrLf + "Offset x, y: " + OffsetX + ", " + OffsetY + Constants.vbCrLf + "Colors: Currently at drawing instruction " + IntNumDrawingInstructions_current + "/" + IntNumDrawingInstructions + ", color " + IntNumDrawingInstructions_colors_current + "/" + IntNumDrawingInstructions_colors + Constants.vbCrLf + "Last referenced x, y: " + IntX + ", " + IntY + Constants.vbCrLf + "Current length of LstFrameHex: " + LstFrameHex.Count + Constants.vbCrLf + "Current length of colors: " + ZtPal.Colors.Count;
            MdlZTStudio.HandledError(GetType().FullName, "RenderCoreImageFromHex", StrErrorMessage, false, null);
        }

        /// <summary>
    /// Sets the offsets for this frame. By default, changes are applied to all frames in this graphic rather than just to one frame.
    /// </summary>
    /// <param name="PntCoordOffsetChanges">Contains offset values</param>
    /// <param name="BlnBatchFix">Fix all frames at once (is batch operation)</param>
        public void UpdateOffsets(Point PntCoordOffsetChanges, bool BlnBatchFix = false)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 31158


        Input:

                On Error GoTo dBug

         */
        200:
            ;

            // By default, this applies to all frames (config setting)
            if (MdlSettings.Cfg_Editor_RotFix_IndividualFrame != 1 | BlnBatchFix == true)
            {

                // Process every frame
                foreach (ClsFrame ztFrame in Parent.Frames)
                {

                    // Update hex
                    if (CoreImageHex.Count > 0)
                    {

                        // Assuming in all cases offsets have been set.

                        // Update offsets of this frame
                        ztFrame.OffsetY += PntCoordOffsetChanges.Y;
                        ztFrame.OffsetX += PntCoordOffsetChanges.X;

                        // Valid offsets?
                        string StrHintOffset = "Problem with a frame. Valid {0} offset should (theoretically, still untested) be between -32768 and 32767.";
                        if (ztFrame.OffsetX < -32768 | ztFrame.OffsetX > 32767)
                        {
                            MdlZTStudio.HandledError(GetType().FullName, "UpdateOffsets", string.Format(StrHintOffset, "X"), true);
                            return;
                        }

                        if (ztFrame.OffsetY < -32768 | ztFrame.OffsetY > 32767)
                        {
                            MdlZTStudio.HandledError(GetType().FullName, "UpdateOffsets", string.Format(StrHintOffset, "Y"), true);
                            return;
                        }

                        // .CoreImageHex(4) and (5) make up offset Y (top/bottom)
                        if (ztFrame.OffsetY >= 0)
                        {
                            // Positive offsets work just fine.
                            ztFrame.CoreImageHex[4] = Strings.Split(ztFrame.OffsetY.ToString("X4").ReverseHex())[0];
                            ztFrame.CoreImageHex[5] = Strings.Split(ztFrame.OffsetY.ToString("X4").ReverseHex())[1];
                        }
                        else
                        {
                            // Negative offsets need a different approach.
                            // 256 (FF) * 256  (FF) = 65536. Despite the plus sign below, offset is negative, so it's substracted.
                            // There's some great explanations on the internet:
                            // HEX FFFF (-1) -> 8000 (-32768) are negative;
                            // HEX 0000 (0) -> 7FFF (32767) are positive
                            // Explained at 
                            ztFrame.CoreImageHex[4] = Strings.Split((256 * 256 + ztFrame.OffsetY).ToString("X4").ReverseHex())[0];
                            ztFrame.CoreImageHex[5] = Strings.Split((256 * 256 + ztFrame.OffsetY).ToString("X4").ReverseHex())[1];
                        }

                        // .CoreImageHex(6) and (7) make up offset X (left/right)
                        // Logic: see above
                        if (ztFrame.OffsetX >= 0)
                        {
                            ztFrame.CoreImageHex[6] = Strings.Split(ztFrame.OffsetX.ToString("X4").ReverseHex())[0];
                            ztFrame.CoreImageHex[7] = Strings.Split(ztFrame.OffsetX.ToString("X4").ReverseHex())[1];
                        }
                        else
                        {
                            ztFrame.CoreImageHex[6] = Strings.Split((256 * 256 + ztFrame.OffsetX).ToString("X4").ReverseHex())[0];
                            ztFrame.CoreImageHex[7] = Strings.Split((256 * 256 + ztFrame.OffsetX).ToString("X4").ReverseHex())[1];
                        }
                    }
                }
            }

            // Correct the offsets and nothing else.
            // Same logic as above.

            // Update hex
            else if (CoreImageHex.Count > 0)
            {

                // Change offsets of this frame 
                OffsetY += PntCoordOffsetChanges.Y;
                OffsetX += PntCoordOffsetChanges.X;

                // Simply change the hex
                if (OffsetY >= 0)
                {
                    CoreImageHex[4] = Strings.Split(OffsetY.ToString("X4").ReverseHex())[0];
                    CoreImageHex[5] = Strings.Split(OffsetY.ToString("X4").ReverseHex())[0];
                }
                else
                {
                    CoreImageHex[4] = Strings.Split((256 * 256 + OffsetY).ToString("X4").ReverseHex())[0];
                    CoreImageHex[5] = Strings.Split((256 * 256 + OffsetY).ToString("X4").ReverseHex())[0];
                }

                if (OffsetX >= 0)
                {
                    CoreImageHex[6] = Strings.Split(OffsetX.ToString("X4").ReverseHex())[0];
                    CoreImageHex[7] = Strings.Split(OffsetX.ToString("X4").ReverseHex())[0];
                }
                else
                {
                    CoreImageHex[6] = Strings.Split((256 * 256 + OffsetX).ToString("X4").ReverseHex())[0];
                    CoreImageHex[7] = Strings.Split((256 * 256 + OffsetX).ToString("X4").ReverseHex())[0];
                }
            }

        21:
            ;
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "UpdateOffsets", Information.Err(), true);
        }

        /// <summary>
    /// Moves frame to a different position
    /// </summary>
    /// <remarks>
    /// No longer in use, but still kept just in case.
    /// </remarks>
    /// <param name="IntNewIndex">Integer</param>
    /// <param name="ZtFrame">ClsFrame. Defaults to editorFrame</param>
    /// <param name="ZtGraphic">ClsGraphic. Defaults to editorGraphic</param>
        public void UpdateIndex(int IntNewIndex, ClsFrame ZtFrame = null, ClsGraphic ZtGraphic = null)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 37352


        Input:

                On Error GoTo dBug

         */
        1:
            ;
            if (Information.IsNothing(ZtFrame))
            {
                ZtFrame = MdlSettings.EditorFrame;
            }

        2:
            ;
            if (Information.IsNothing(ZtGraphic))
            {
                ZtGraphic = MdlSettings.EditorGraphic;
            }

        5:
            ;

            // Get current list, remove item, add to new
            ZtGraphic.Frames.Remove(ZtFrame);
        6:
            ;

            // Add to wanted place
            ZtGraphic.Frames.Insert(IntNewIndex, ZtFrame);
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "UpdateIndex", Information.Err(), true);
        }

        /// <summary>
    /// Loads a .PNG file and converts it to HEX.
    /// </summary>
    /// <param name="StrFileName">File name of PNG to load</param>
        public void LoadPNG(string StrFileName)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 38300


        Input:

                On Error GoTo dBug

         */
        5:
            ;
            Bitmap BmpDrawTemp = (Bitmap)Image.FromFile(StrFileName);
            ClsDirectBitmap BmpDraw;
        10:
            ;

            // Prevent a file lock on .PNG files.
            // If files are locked, the files can't be automatically deleted after batch conversion.
            using (BmpDrawTemp)
            {
            11:
                ;
                BmpDraw = new ClsDirectBitmap(BmpDrawTemp);
            12:
                ;
                BmpDrawTemp = null;
            }

        20:
            ;

            // The offsets should be set here first!
            // They should NOT be changed in ClsFrame::BitMapToHex(), since they might overwrite/change updated offsets!

            // Easy to start with: 
            // * Define the offsets
            // * Define the dimensions (height, width). Calculate by top/left and bottom/right pixel
            // The offset is difficult. "Zoot" (program by MadScientist) *seemed* to handle it by setting the offset to half the height/width.
            // That approach at least centers your image, but it might still not be desired.
            // Following the same approach though, as it's impossible to know the correct/desired offsets at this point.
            OffsetX = (int)Math.Round(Math.Ceiling(BmpDraw.Width / 2d) + 1d);
            OffsetY = (int)Math.Round(Math.Ceiling(BmpDraw.Height / 2d) + 1d);
        21:
            ;

            // Get defining rectangle (dimensions)
            var RectCrop = MdlBitMap.GetDefiningRectangle(BmpDraw);
        22:
            ;

            // Get cropped version of bitmap based on this rectangle
            var BmpCropped = new ClsDirectBitmap(MdlBitMap.GetCroppedVersion(BmpDraw, RectCrop));
        23:
            ;

            // Improvement: by cropping to the relevant area, the offset should in most cases be better.
            OffsetX -= RectCrop.X; // Originally centered based on width. The offset was to the left (positive). Substract from offset to move it right a bit (closer to center)
            OffsetY -= RectCrop.Y; // Originally centered based on height. The offset was to the top (positive). Substract from offset to  move it down a bit (closer to center)
        25:
            ;

            // Like APE and ZOOT, by default assume the top left pixel of the imported PNG determines the transparent color of the image.
            // So it is necessary to add the color from the original image and add that as transparent color to the palette (if still empty) 
            // This should've been avoided by setting the background color properly, but it's easily overlooked.
            // 
            // The condition below is (mostly) meant for plaques, to AVOID this from happening.
            // Use cases: importing an icon or plaque, centered on a transparent background.
            // Image how they're imported: usually a photo or other visual, with no transparent colors around the borders (or in case of icon, mistakenly the top left)
            // The cropped version will be the plaque (rectangle).
            // This means: the top left pixel Of the cropped area IS relevant (gray/black -> (0, 0)) and should NOT be transparent. 
            // 
            if (RectCrop.X != 0 & RectCrop.Y != 0 & Parent.ColorPalette.Colors.Count == 0)
            {
                MdlZTStudio.Trace(GetType().FullName, "LoadPng", "Defining rectangle is not starting at (0,0), color palette is empty. Add top left pixel of bitmap (input PNG) as transparent color.");
                Parent.ColorPalette.Colors.Add(BmpDraw.GetPixel(0, 0));
            }

        30:
            ;

            // ZT Studio has cropped the image.
            // Generate hex from bitmap.
            BitMapToHex(BmpCropped);
        31:
            ;

            // Force re-rendering based on HEX (offsets, image, ...)
            CoreImageBitmap = null;
            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "LoadPNG", Information.Err(), true);
        }

        /// <summary>
    /// Converts a bitmap to hex values. Important: offsets should have been set already!
    /// </summary>
    /// <remarks>
    /// In the past, an alternative method using LockBits was implemented to find the defining rectangle.
    /// According to the comments, it was much faster than the old method.
    /// Could this also be applied here?
    /// </remarks>
    /// <param name="BmImage">ClsDirectBitmap. Defaults to CoreImageBitmap</param>
    /// <returns>List(Of String) - Hex values for the ZT1 rendering engine (single frame)</returns>
        public List<string> BitMapToHex(ClsDirectBitmap BmImage = null)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 42944


        Input:

                On Error GoTo dBug

         */
        1:
            ;
            var LstGeneratedHex = new List<string>();
        2:
            ;
            if (Information.IsNothing(BmImage) == true)
            {

                // Fall back to CoreImageBitmap, if available.
                if (Information.IsNothing(CoreImageBitmap) == true)
                {
                    // Can this happen?
                    MdlZTStudio.HandledError(GetType().FullName, "BitMapToHex", "Not bitmap given Async input. Fallback TypeOf CoreImageBitmap was impossible.", false, Information.Err());
                    return LstGeneratedHex; // Prevents further processing
                }
                else
                {
                    BmImage = CoreImageBitmap;
                }
            }

            // APE / ZOOT: top left color = transparent.
            // Rely only on that method if no colors are known in the color palette yet.
            // Reason: it works differently in batch conversions (20190820: todo: needs more explaining. Why again? Shared pelette?)
            // 20170519: beware: this might not work properly for (cropped) plaques!
            else if (Parent.ColorPalette.Colors.Count == 0)
            {
                Parent.ColorPalette.Colors.Add(BmImage.GetPixel(0, 0));

                // 20170519. Store BmImage. The hex values are stored as well.
                // 20190817:
                // * Do NOT store BmImage. It may cache a bitmap with the transparent color, such as blue.
                // * However, on changing ZT Studio's background color to green, the blue will still be shown.
                // * Already setting the core image bitmap here may result in unwanted side-effects!
                // Me.CoreImageBitmap = BmImage

            }

        // === Rewrite.

        // Get the defining rectangle of the .PNG image.
        // Next, generate the hex code. That should be enough for now.

        200:
            ;


            // Reason: instead of the top left pixel, transparency could already have been determined in some (batch) processes

            var LstHexRows = new List<string>(); // Store bytes as strings for now. Actual drawing instructions.

            // Use intX, intY to move over every pixel.
            int IntX = 0;
            int IntY = 0;
            Color ObjColor; // Will be used to go over each pixel and determine the color

            // Take the palette from the parent
            var ZtPal = Parent.ColorPalette;
            var LstDrawingInstructions = new List<ClsDrawingInstr>();
            var ObjDrawingInstr = new ClsDrawingInstr();
        1000:
            ;

        // Here it gets a bit more tricky. There's some information to process, 
        // but some information needs to be switched around in the final output.
        // - per drawn line (lines go from left to right, they're drawn below each other): 
        // [number of drawing instruction blocks] [drawing instruction blocks, if any (could be 0?)]
        // - remember how many drawing instruction blocks ([offset] + [number of color indexes to follow] + [color indexes]) there are per line;
        // - for each drawing instruction:
        // -- remember the offset;
        // -- count the colors;
        // -- keep track of the color indexes: find them in a palette, or add them. Max 255 colors, warn user if there are more colors in the Bitmap (PNG)!
        // --> this will only have been changed if the color palette has been altered in some way.

        // Keep in mind: a one pixel image would be pixel [0,0] but width = 1 and height = 1.

        // Todo: implement hashes to check for regressions; improve this with LockBits instead of GetPixel().

        3005:
            ;


            // From top to bottom, from left to right
            while (IntY < BmImage.Height)
            {

                // Restart.
                IntX = 0;
                ObjDrawingInstr = new ClsDrawingInstr();
                LstDrawingInstructions.Clear(false);
                while (IntX < BmImage.Width)
                {
                3010:
                    ;

                    // Read the color.
                    ObjColor = BmImage.GetPixel(IntX, IntY);

                    // If the index of the color is 0, assume it's a transparent pixel.
                    if (Parent.ColorPalette.GetColorIndex(ObjColor) == 0)
                    {
                    3100:
                        ;

                        // Assuming transparent pixel.
                        // This can happen at the very start of the row;
                        // This can happen after a series of colored pixels.
                        if (ObjDrawingInstr.Offset == 0 & ObjDrawingInstr.PixelColors.Count == 0)
                        {
                        3101:
                            ;
                        }

                        // Most likely getting this at the very start of the row. 
                        // No action required.

                        else if (ObjDrawingInstr.PixelColors.Count > 0)
                        {
                        3102:
                            ;

                            // Colors were detected before.
                            // Now processing a transparent pixel again.
                            // Close the previous drawing instruction, start a new one.
                            LstDrawingInstructions.Add(ObjDrawingInstr, false);
                            ObjDrawingInstr = new ClsDrawingInstr();
                        }
                        else
                        {
                        3108:
                            ;

                            // In this case, the offset is bigger than 0 (previous pixel was also transparent) and the color count is 0.
                            // Don't do anything.
                            // 20190816: shouldn't this case be merged with the first one?

                        }

                    // The current pixel is transparent.
                    // Increase offset by 1.
                    3110:
                        ;
                        ObjDrawingInstr.Offset += 1;
                    3115:
                        ;

                        // Rare exception: if the offset is now 255 (limit), end this drawing instruction and start a new one.
                        if (ObjDrawingInstr.Offset == 255)
                        {
                            MdlZTStudio.Trace(GetType().FullName, "BitMapToHex", "This graphic has an example of a drawing instruction with a large offset (255). Add in documentation.");
                            LstDrawingInstructions.Add(ObjDrawingInstr, false);
                            ObjDrawingInstr = new ClsDrawingInstr();
                        }
                    }
                    else
                    {
                    3200:
                        ;

                        // Detected a colored pixel.
                        // Get the index of this color from the palette and add it to the drawing instruction.
                        int tmpColorIndex = Parent.ColorPalette.GetColorIndex(ObjColor, true);
                        ObjDrawingInstr.PixelColors.Add(tmpColorIndex, false);
                    3399:
                        ;

                        // Rare exception: if the number of colored pixels is now 255 (limit), end this drawing instruction and start a new one.
                        if (ObjDrawingInstr.PixelColors.Count == 255)
                        {
                            MdlZTStudio.Trace(GetType().FullName, "BitMapToHex", "This graphic is an example has an example of a drawing instruction with a color offset (255). Add in documentation.");
                            LstDrawingInstructions.Add(ObjDrawingInstr, false);
                            ObjDrawingInstr = new ClsDrawingInstr();
                        }
                    }

                    IntX += 1;
                }


            // === END OF LINE ===

            3400:
                ;

                // All pixels have been processed.
                // This means the last drawing instruction is likely still open (unless it was just ended after hitting one of the above limits).
                // Check if this drawing instruction contains an offset or color.
                if (ObjDrawingInstr.Offset != 0 | ObjDrawingInstr.PixelColors.Count > 0)
                {
                    LstDrawingInstructions.Add(ObjDrawingInstr, false);
                }

            3405:
                ;

                // All drawing instructions for this line are prepared.
                // So to LstHexRows, add:
                // Number of instruction blocks [between 0 - 255]
                LstHexRows.Add(LstDrawingInstructions.Count.ToString("X2"), false);
            3406:
                ;
                foreach (ClsDrawingInstr diInstruction in LstDrawingInstructions)
                    // For each block: 
                    // - get hex: [offset][num colors][color indexes of pixels]
                    LstHexRows.AddRange(diInstruction.GetHex(), false);
                3450:
                ;
                IntY += 1;
            }

        9000:
            ;
        9001:
            ;

            // Easier to build it this way. Start by writing the dimensions: height, width.
            LstGeneratedHex.AddRange(Strings.Split(BmImage.Height.ToString("X4").ReverseHex(), " "), false);
            LstGeneratedHex.AddRange(Strings.Split(BmImage.Width.ToString("X4").ReverseHex(), " "), false);
        9002:
            ;
            if (OffsetY >= 0)
            {
                LstGeneratedHex.AddRange(Strings.Split(OffsetY.ToString("X4").ReverseHex(), " "), false);
            }
            else
            {
                LstGeneratedHex.AddRange(Strings.Split((256 * 256 + OffsetY).ToString("X4").ReverseHex(), " "), false);
            }

            if (OffsetX >= 0)
            {
                LstGeneratedHex.AddRange(Strings.Split(OffsetX.ToString("X4").ReverseHex(), " "), false);
            }
            else
            {
                LstGeneratedHex.AddRange(Strings.Split((256 * 256 + OffsetX).ToString("X4").ReverseHex(), " "), false);
            }

        9003:
            ;

            // Issue: two unknown bytes. ('mystery bytes')
            // For bamboo, frame 1 = 1. 
            // Always seems to be 1 in APE.
            LstGeneratedHex.Add("01", false);
            LstGeneratedHex.Add("00", false);
        9020:
            ;

            // Now add the drawing instructions for the frame.
            LstGeneratedHex.AddRange(LstHexRows, false);
        9502:
            ;

            // Reset. Should be regenerated from the hex.
            // Me.CoreImageBitmap = Nothing - 20170519 - what was the point again in setting this to nothing?
            CoreImageHex = LstGeneratedHex;
            return CoreImageHex;
            return default;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "BitMapToHex", Information.Err(), true);
        }

        /// <summary>
    /// Saves the frame of a ZT1 Graphic to a .PNG file
    /// </summary>
    /// <param name="StrFileName">Destination filename</param>
        public void SavePNG(string StrFileName)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 53566


        Input:

                On Error GoTo dBug

         */
        10:
            ;
            var BmRect = new Rectangle(-9999, -9999, 0, 0);
            Bitmap BmCropped;

            // 0 = canvas size
            // 1 = relevant pixel area of graphic
            // 2 = relevant pixel area of frame
            // 3 = around the grid origin of frame

            switch (MdlSettings.Cfg_Export_PNG_CanvasSize)
            {
                case 0:
                    {
                    // Save PNG image. Complete canvas size.
                    21:
                        ;
                        var ImgComb = new ClsDirectBitmap(MdlSettings.Cfg_Grid_NumPixels * 2, MdlSettings.Cfg_Grid_NumPixels * 2);
                    32:
                        ;

                        // Use ZT Studio's main window background color (transparent) or export with an entirely transparent background (user's choice)
                        using (var ObjGraphic = Graphics.FromImage(ImgComb.Bitmap))
                        {
                            ObjGraphic.Clear((Color)Interaction.IIf(MdlSettings.Cfg_Export_PNG_TransparentBG == 0, MdlSettings.Cfg_Grid_BackGroundColor, Color.Transparent));
                        }

                    35:
                        ;
                        ImgComb = MdlBitMap.CombineImages(ImgComb, GetImage());
                        ImgComb.Bitmap.Save(StrFileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    }

                case 1:
                    {
                    // Save PNG image. Relevant pixel area of graphic
                    131:
                        ;

                        // Cheap trick: combine all images into 1, then get the relevant rectangle.
                        // Some caching might be in order in the future :)

                        var ImgComb = new ClsDirectBitmap(MdlSettings.Cfg_Grid_NumPixels * 2, MdlSettings.Cfg_Grid_NumPixels * 2);
                    132:
                        ;

                        // Use ZT Studio's main window background color (transparent) or export with an entirely transparent background (user's choice)
                        using (var ObjGraphic = Graphics.FromImage(ImgComb.Bitmap))
                        {
                            ObjGraphic.Clear((Color)Interaction.IIf(MdlSettings.Cfg_Export_PNG_TransparentBG == 0, MdlSettings.Cfg_Grid_BackGroundColor, Color.Transparent));
                        }

                    135:
                        ;


                        // Combine all images. Basically put them all on top of each other.
                        // That way, it's easy to determine the most relevant pixel top/left and bottom/right
                        foreach (ClsFrame ObjFrame in Parent.Frames)
                            ImgComb = MdlBitMap.CombineImages(ImgComb, ObjFrame.GetImage());

                        // Apply to this particular frame.
                        BmRect = MdlBitMap.GetDefiningRectangle(ImgComb);
                        BmCropped = MdlBitMap.GetCroppedVersion(GetImage(), BmRect);
                        BmCropped.Save(StrFileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    }

                case 2:
                    {
                    // Save PNG image. Relevant area of frame.

                    141:
                        ;
                        BmRect = MdlBitMap.GetDefiningRectangle(GetImage());
                        BmCropped = MdlBitMap.GetCroppedVersion(GetImage(), BmRect);
                        BmCropped.Save(StrFileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    }

                case 3:
                    {
                        // Center around the origin. This method has been contributed by HENDRIX
                        // This is much faster and avoids all cropping, but preserves the offset
                        GetImage(false, true).Bitmap.Save(StrFileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    }
            }

            return;
        dBug:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "SavePNG", Information.Err(), true);
        }

        /// <summary>
    /// Writes details of frame (width, height, offsetX, offsetY, mystery bytes) to a text file.
    /// The text file has the name of the ZT1 Graphic and a .txt extension.
    /// </summary>
        public object WriteDetailsToTextFile()
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBug' at character 57859


        Input:

                On Error GoTo dBug

         */
        1:
            ;
            if (!string.IsNullOrEmpty(Parent.FileName))
            {
            11:
                ;
                CoreImageBitmap = GetCoreImageBitmap();
            21:
                ;
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "width", CoreImageBitmap.Width.ToString());
            22:
                ;
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "height", CoreImageBitmap.Height.ToString());
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "offsetX", OffsetX.ToString());
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "offsetY", OffsetY.ToString());
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "numberOfBytes", CoreImageHex.Count.ToString());
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "mysteryBytes", string.Join(" ", MysteryHEX));
            25:
                ;
            30:
                ;
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "mysteryByte0_Integer", Conversions.ToInteger("&H" + MysteryHEX[0]).ToString());
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "mysteryByte1_Integer", Conversions.ToInteger("&H" + MysteryHEX[1]).ToString());
                MdlSettings.IniWrite(Parent.FileName + ".txt", "Frame" + Parent.Frames.IndexOf(this), "mysteryBytes_Integer", Conversions.ToInteger("&H" + MysteryHEX[1] + MysteryHEX[0]).ToString());
            }
            else
            {
                string StrMessage = "" + "Filename of graphic has not been determined yet." + Constants.vbCrLf + "Can't write details of one of its frames to a file.";
                MdlZTStudio.HandledError(GetType().FullName, "WriteDetailsToTextFile", StrMessage, true, null);
            }

            return default;
        dBug:
            ;
            MdlZTStudio.HandledError(GetType().FullName, "WriteDetailsToTextFile", "Unexpected error.", false, null);
        }
    }
}