using System.IO;
using Microsoft.VisualBasic;

namespace ZTStudio
{


    /// <summary>
/// Some methods related to color palettes
/// </summary>
    static class MdlColorPalette
    {

        /// <summary>
    /// Replaces color (specified by index) in the main color palette
    /// </summary>
    /// <param name="IntIndex">Index of color to be replaced</param>
        public static void ReplaceColor(int IntIndex)
        {
            {
                var withBlock = My.MyProject.Forms.FrmMain.DlgColor;
                withBlock.Color = My.MyProject.Forms.FrmMain.DgvPaletteMain.Rows[IntIndex].DefaultCellStyle.BackColor;
                withBlock.AllowFullOpen = true;
                withBlock.FullOpen = true;
                withBlock.SolidColorOnly = true;
                withBlock.ShowDialog();
            }

            MdlSettings.EditorGraphic.ColorPalette.Colors[IntIndex] = My.MyProject.Forms.FrmMain.DlgColor.Color;

            // Update entire palette (easy)
            MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(My.MyProject.Forms.FrmMain.DgvPaletteMain);
        }

        /// <summary>
    /// Moves color in the palette to a new position.
    /// This has repercussions: order of colors changes, hex values (color index) need to be updated!
    /// </summary>
    /// <param name="IntIndexNow">Current index</param>
    /// <param name="IntIndexDest">Wanted index</param>
        public static void MoveColor(int IntIndexNow, int IntIndexDest)
        {

            // Get color
            var ObjColorToMove = MdlSettings.EditorGraphic.ColorPalette.Colors[IntIndexNow];

            // Delete the original.
            MdlSettings.EditorGraphic.ColorPalette.Colors.RemoveAt(IntIndexNow);

            // We had the color. Insert it at the position we want.
            MdlSettings.EditorGraphic.ColorPalette.Colors.Insert(IntIndexDest, ObjColorToMove);

            // Refresh
            MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(My.MyProject.Forms.FrmMain.DgvPaletteMain);

            // Update coreImageHex for each frame. Color indexes have changed.
            foreach (ClsFrame ztFrame in MdlSettings.EditorGraphic.Frames)
            {
                ztFrame.CoreImageHex = null;
                ztFrame.BitMapToHex(); // 20170519 - is it necessary to update this already? It could be generated when called.
            }
        }

        /// <summary>
    /// Adds a new color entry at the specified index. 
    /// The color hasn't been picked yet, so by default it's transparent.
    /// </summary>
    /// <param name="IntIndexNow">Index</param>
        public static void AddColor(int IntIndexNow)
        {
            if (MdlSettings.EditorGraphic.ColorPalette.Colors.Count == 256)
            {
                MdlZTStudio.HandledError("MdlColorPalette", "AddColor", "You can't add any more colors to this palette." + Constants.vbCrLf + "The maximum of 255 (+1 transparent) colors has been reached.");
            }

            // Get color
            var ObjColor = MdlSettings.Cfg_Grid_BackGroundColor;
            {
                var withBlock = My.MyProject.Forms.FrmMain.DlgColor;
                withBlock.Color = ObjColor;
                withBlock.AllowFullOpen = true;
                withBlock.FullOpen = true;
                withBlock.SolidColorOnly = true;
                withBlock.ShowDialog();
            }

            ObjColor = My.MyProject.Forms.FrmMain.DlgColor.Color;

            // Insert it at the position we want.
            MdlSettings.EditorGraphic.ColorPalette.Colors.Insert(IntIndexNow + 1, ObjColor);

            // Refresh
            MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(My.MyProject.Forms.FrmMain.DgvPaletteMain);
        }


        /// <summary>
    /// Opens a color palette, displays it as a separate window.
    /// </summary>
    /// <param name="StrFileName">Source filename of the ZT1 color palette (.pal)</param>
        public static void LoadPalette(string StrFileName)
        {
            if (File.Exists(StrFileName))
            {
                if (Path.GetExtension(StrFileName) != ".pal")
                {
                    MdlZTStudio.HandledError("MdlColorPalette", "LoadPalette", "You did not select a ZT1 Color Palette (.pal file).");
                }
                else
                {

                    // Show a dedicated window
                    var CpPallete = new ClsPalette(null);
                    var FrmColPal = new FrmPal();

                    // Read the .pal file
                    CpPallete.ReadPal(StrFileName);
                    CpPallete.FillPaletteGrid(FrmColPal.DgvPal);
                    FrmColPal.SsFileName.Text = Path.GetFileName(StrFileName);
                    FrmColPal.Show();
                }
            }
            else
            {
                MdlZTStudio.HandledError("MdlColorPalette", "LoadPalette", "Could not find '" + StrFileName + "'");
            }
        }
    }
}