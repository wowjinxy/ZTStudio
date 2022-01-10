using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{
    /// <summary>
/// Color Palette window
/// </summary>
    public partial class FrmPal
    {
        public FrmPal()
        {
            InitializeComponent();
        }

        /// <summary>
    /// Handles color replacement in main color palette (in main window). 
    /// This allows to quickly preview how graphics would look like too, with a different color pattern.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnUseInMainPal_Click(object sender, EventArgs e)
        {

            // If no main graphic has been loaded or created with at least 1 frame, there is nothing to replace.
            if (Information.IsNothing(MdlSettings.EditorGraphic.Frames))
            {
                MdlZTStudio.HandledError(GetType().FullName, "BtnUseInMainPal_Click", "You will need to open a ZT1 graphic file first before you can use this recolor feature.");
                return;
            }

            int IntMaxColorIndex = My.MyProject.Forms.FrmMain.DgvPaletteMain.Rows.Count - DgvPal.Rows.Count + 1;

            // Can't be negative (main color palette color count is smaller than the count of this palette)
            if (IntMaxColorIndex < 1)
            {
                MdlZTStudio.HandledError(GetType().FullName, "BtnUseInMainPal_Click", "The color palette in the main window has less colors than this palette. Replacing is not possible.");
                return;
            }

            string StrMessage = "" + "Index of first color to replace (can not be 0 since the transparent color is ignored)." + Constants.vbCrLf + Constants.vbCrLf + "For example, the index for the Restaurant would be 248 (roof 8 colors) or 232 (other roof 16 colors)" + Constants.vbCrLf + Constants.vbCrLf + "With the current palette, the index to start from should be between 1 and " + IntMaxColorIndex;
            string strInput = Interaction.InputBox(StrMessage, "Index of the first color to replace", "1");
            if (string.IsNullOrEmpty(strInput))
            {
                return; // user pressed cancel
            }
            else if (Information.IsNumeric(strInput) == false)
            {

                // Verify
                MdlZTStudio.HandledError(GetType().FullName, "BtnUseInMainPal_Click", "You need to specify a positive number, at least 1 and maximum " + IntMaxColorIndex + ".");
                return;
            }
            else if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(Conversion.Int(strInput), IntMaxColorIndex, false)))
            {
                MdlZTStudio.HandledError(GetType().FullName, "BtnUseInMainPal_Click", "You need to specify a positive number, at least 1 and maximum " + IntMaxColorIndex + ".");
                return;
            }

            // Replace
            foreach (DataGridViewRow ObjDataRow in DgvPal.Rows)
            {

                // Careful: the game uses color palettes consisting of 1 transparent color; followed by 8 or 16 shades of a color for color customization.

                if (ObjDataRow.Index != 0) // Transparent color, ignore this, it doesn't matter.
                {
                    if (MdlSettings.EditorGraphic.ColorPalette.Colors.Count == Conversions.ToInteger(strInput) + ObjDataRow.Index - 1)
                    {
                        // Color index does not exist.
                        MdlSettings.EditorGraphic.ColorPalette.Colors.Add(ObjDataRow.DefaultCellStyle.BackColor);
                    }
                    else
                    {
                        // Color index already existed. Overwrite.
                        MdlSettings.EditorGraphic.ColorPalette.Colors[Conversions.ToInteger(strInput) + ObjDataRow.Index - 1] = ObjDataRow.DefaultCellStyle.BackColor;
                    }
                }
            }

            // The color palette in the main window has been changed.
            // This means cached versions of images (CoreImageBitmap) should be regenerated from hex values.
            // SO, set CoreImageBitMap to Nothing (will be rendered when needed; not instantly!)
            foreach (ClsFrame ObjFrame in MdlSettings.EditorGraphic.Frames)
                ObjFrame.CoreImageBitmap = null;
            MdlSettings.EditorGraphic.LastUpdated = DateAndTime.Now.ToString("yyyyMMddHHmmss");
            MdlZTStudioUI.UpdatePreview(true, false, My.MyProject.Forms.FrmMain.TbFrames.Value - 1);

            // Show palette again?
            MdlSettings.EditorGraphic.ColorPalette.FillPaletteGrid(My.MyProject.Forms.FrmMain.DgvPaletteMain);
        }
    }
}