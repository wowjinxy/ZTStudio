using Microsoft.VisualBasic;

namespace ZTStudio
{
    /// <summary>
/// <para>ClsDrawingInstr is used to manage a drawing instruction.</para>
/// <para>The instruction specifies which colors to draw, from left to right.</para>
/// <para>This typically consists of an offset (how many transparent pixels are there before drawing starts? Could be 0) followed by one or more references to colors.</para>
/// <para>The references to colors are made using their index number in the color palette.</para>
/// </summary>
    public class ClsDrawingInstr
    {

        // A drawing instruction block.
        // This consists of a simple pattern:
        // [1 byte offset] [1 byte number of colors] [if present: indexes of colors]

        private int di_offset = 0; // only one byte. This is actually: 'skip X pixels in this line'. Max 255 at once.
        private List<int> di_lstColors = new List<int>();  // refers to the index of the color in a palette. Num colors = 0-255.

        /// <summary>
    /// Offset. This determines how many pixels to skip horizontally (from left to right) before actually starting to draw colored pixels.
    /// </summary>
    /// <returns>Integer</returns>
        public int Offset
        {
            // How many transparent pixels are there first?
            get
            {
                return di_offset;
            }

            set
            {
                di_offset = value;
            }
        }

        /// <summary>
    /// A list of color references. These colors will be drawn horizontally (from left to right), after the offset has been applied.
    /// The colors are referenced by their index number in the palette.
    /// </summary>
    /// <returns>List(Of Integer) - color references</returns>
        public List<int> PixelColors
        {
            // Contains the pixels which will be drawn horizontally (row) and their color.
            get
            {
                return di_lstColors;
            }

            set
            {
                di_lstColors = value;
            }
        }

        /// <summary>
    /// Returns the hex form of this drawing instruction.
    /// It consist of the offset (X2), the number of colored pixels (X2) and finally the index numbers of each color (X2 per color).
    /// </summary>
    /// <returns>List(Of String)</returns>

        // Returns the hex code for this drawing block.
        public List<string> GetHex()
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBg' at character 2453


        Input:

                ' Returns the hex code for this drawing block.
                On Error GoTo dBg

         */
        0:
            ;
            var opHex = new List<string>();
        1:
            ;

            // Offset.
            opHex.Add(Offset.ToString("X2"), false);
        2:
            ;

            // Number of colors. 0 - 255
            opHex.Add(di_lstColors.Count.ToString("X2"), false);
        3:
            ;

            // Indexes of colors (~ colorpalette)
            foreach (int c in di_lstColors)
                opHex.Add(c.ToString("X2"), false);
            5:
            ;
            return opHex;
            return default;
        dBg:
            ;
            MdlZTStudio.UnhandledError(GetType().FullName, "GetHex", Information.Err(), true);
        }
    }
}