using System;
using System.Drawing;
using Microsoft.VisualBasic;

namespace ZTStudio
{
    /// <summary>
/// Contains functions related to bitmap operations
/// </summary>
    static class MdlBitMap
    {


        /// <summary>
    /// Combines two images into one. Always centers the images on each other, if different size. (For instance: grid + actual graphic)
    /// </summary>
    /// <param name="BitMapBack">ClsDirectBitmap background</param>
    /// <param name="BitMapFront">ClsDirectBitmap on top</param>
    /// <returns>ClsDirectBitmap</returns>
        public static ClsDirectBitmap CombineImages(ClsDirectBitmap BitMapBack, ClsDirectBitmap BitMapFront)
        {
            ;
#error Cannot convert OnErrorGoToStatementSyntax - see comment for details
        /* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo dBg' at character 648


        Input:

                On Error GoTo dBg

         */
        1:
            ;
            Image ImgBack = BitMapBack.Bitmap;
            Image ImgFront = BitMapFront.Bitmap;
            int IntMaxWidth = Math.Max(ImgBack.Width, ImgFront.Width);
            int IntMaxHeight = Math.Max(ImgBack.Height, ImgFront.Height);
            MdlZTStudio.Trace("MdlBitMap", "CombineImages", "Background w1 = " + ImgBack.Width + ", h1 = " + ImgBack.Height + " | Front w2 = " + ImgFront.Width + ", " + ImgFront.Height);
        2:
            ;
            var BmpOutput = new ClsDirectBitmap(IntMaxWidth, IntMaxHeight);
        3:
            ;
            var ObjGraphic = Graphics.FromImage(BmpOutput.Bitmap);
        11:
            ;
            ObjGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor; // Prevent softening
        21:
            ;
            ObjGraphic.DrawImage(ImgBack, (int)Math.Round((IntMaxWidth - ImgBack.Width) / 2d), (int)Math.Round((IntMaxHeight - ImgBack.Height) / 2d), ImgBack.Width, ImgBack.Height);
        31:
            ;
            ObjGraphic.DrawImage(ImgFront, (int)Math.Round((IntMaxWidth - ImgFront.Width) / 2d), (int)Math.Round((IntMaxHeight - ImgFront.Height) / 2d), ImgFront.Width, ImgFront.Height);
        41:
            ;
            ObjGraphic.Dispose();
            return BmpOutput;
        dBg:
            ;
            MdlZTStudio.UnhandledError("MdlBitMap", "CombineImages", Information.Err(), true);
        }

        /// <summary>
    /// Draws an isometric grid (squares).
    /// </summary>
    /// <param name="IntFootPrintX">Sets the amount of squares (X)</param>
    /// <param name="IntFootPrintY">Sets the amount of squares (Y)</param>
    /// <returns>Bitmap of a grid</returns>
    /// <remarks>
    /// Could be simplified: IntFootPrintX and IntFootPrintY are always simply the config parameters up till now.
    /// However, for future use, do not change this.
    /// </remarks>
        public static ClsDirectBitmap DrawGridFootPrintXY(int IntFootPrintX, int IntFootPrintY)
        {

            // Draws a certain amount of squares.
            // ZT1 uses either 1/4th of a square, or complete squares from there on. 
            // Anything else doesn't seem to be too reliable!

            // This function calculates where to put the center of the image.
            // View:
            // 0 = SE
            // 1 = SW
            // 2 = NE
            // 3 = NW

            // SE, cFootPrintX = 10, cFootPrintY = 8 ---> Front view of an object, 5 squares. Side: 4 squares.

            // Draw bitmap with squares first.
            // To do so, calculate the top left pixel of the center of the grid.
            int IntWidth = (IntFootPrintX + IntFootPrintY) * 16;
            int IntHeight = (int)Math.Round(IntWidth / 2d);

            // Every grid square adds this much for X and Y - consider both directions to be efficient!
            int x_dim = IntFootPrintX * 16 + IntFootPrintY * 16;
            int y_dim = IntFootPrintY * 8 + IntFootPrintX * 8;
            var BmInput = new ClsDirectBitmap(x_dim * 2, y_dim * 2);

            // Iirst point of the generated grid: intFootprintX * 32, +16px Y (center), 

            // Find the center of the center of the generated grid bitmap.
            // Next, align it with the center of the image.

            // Starting info: coordinate, number of squares, even or odd (= extra row) 
            // Keep track of how many squares are drawn
            // Do not draw more squares than the max width

            var Coord = new Point((int)Math.Round(IntFootPrintX / 2d * 32d), 0);

            // Think with X=10,Y=8
            int IntCurFootPrintX;
            int IntCurFootPrintY;
            var loopTo = IntFootPrintX;
            for (IntCurFootPrintX = 2; IntCurFootPrintX <= loopTo; IntCurFootPrintX += 2)
            {

                // Starting point:
                Coord.X = (int)Math.Round(x_dim - IntWidth / 2d + IntFootPrintX / 2d * 32d);
                Coord.X = (int)Math.Round(x_dim - IntWidth / 2d);  // Move to the left
                Coord.X = (int)Math.Round(Coord.X + (IntFootPrintX - IntCurFootPrintX) / 2d * 32d);  // What can we add?
                Coord.Y = (int)Math.Round(y_dim - IntHeight / 2d + 16d * (IntCurFootPrintX / 2d));
                Coord.Y -= 16;

                // Draw the first square, which is easy.
                // Footprints step by 2
                var loopTo1 = IntFootPrintY;
                for (IntCurFootPrintY = 2; IntCurFootPrintY <= loopTo1; IntCurFootPrintY += 2)
                {

                    // For each
                    Coord.X += 32;
                    Coord.Y += 16;
                    DrawGridSquare(Coord, BmInput);
                }
            }

            return BmInput;
        }

        /// <summary>
    /// Draws a square (for a grid)
    /// </summary>
    /// <param name="ObjCoordTopLeft">The top left coordinate</param>
    /// <param name="BmInput">The ClsDirectBitmap to drawn on. If not specified</param>
    /// <returns>ClsDirectBitmap of a full square</returns>
        public static ClsDirectBitmap DrawGridSquare(Point ObjCoordTopLeft, ClsDirectBitmap BmInput = null)
        {

            // Todo: replace SetPixel()?

            if (Information.IsNothing(BmInput) == true)
            {
                BmInput = MdlSettings.BMEmpty;
            }

            int IntX;
            int IntY = 0;

            // === Top left
            for (IntX = -31; IntX <= 0; IntX++)
            {
                BmInput.SetPixel(ObjCoordTopLeft.X + IntX, ObjCoordTopLeft.Y + IntY, MdlSettings.Cfg_Grid_ForeGroundColor);

                // Mirror to the right
                BmInput.SetPixel(ObjCoordTopLeft.X + 1 - IntX, ObjCoordTopLeft.Y + IntY, MdlSettings.Cfg_Grid_ForeGroundColor);

                // Mirror bottom part for bottom
                BmInput.SetPixel(ObjCoordTopLeft.X + IntX, ObjCoordTopLeft.Y - IntY + 1, MdlSettings.Cfg_Grid_ForeGroundColor);
                BmInput.SetPixel(ObjCoordTopLeft.X + 1 - IntX, ObjCoordTopLeft.Y - IntY + 1, MdlSettings.Cfg_Grid_ForeGroundColor);

                // Width = 32; height = 16
                // This means the height decreases slower
                if (IntX % 2 == 0)
                {
                    IntY -= 1;
                }
            }

            // Center consists of 4px
            BmInput.SetPixel(ObjCoordTopLeft.X, ObjCoordTopLeft.Y, MdlSettings.Cfg_Grid_ForeGroundColor);
            BmInput.SetPixel(ObjCoordTopLeft.X, ObjCoordTopLeft.Y + 1, MdlSettings.Cfg_Grid_ForeGroundColor);
            BmInput.SetPixel(ObjCoordTopLeft.X + 1, ObjCoordTopLeft.Y, MdlSettings.Cfg_Grid_ForeGroundColor);
            BmInput.SetPixel(ObjCoordTopLeft.X + 1, ObjCoordTopLeft.Y + 1, MdlSettings.Cfg_Grid_ForeGroundColor);
            return BmInput;
        }



        /// <summary>
    /// Returns a cropped version of the given bitmap
    /// </summary>
    /// <param name="BmInput">Bitmap image</param>
    /// <param name="RectCropArea">Rectangle used to crop the bitmap</param>
    /// <returns>Bitmap</returns>
        public static Bitmap GetCroppedVersion(ClsDirectBitmap BmInput, Rectangle RectCropArea)
        {
            return BmInput.Bitmap.Clone(RectCropArea, BmInput.Bitmap.PixelFormat);
        }

        /// <summary>
    /// Returns the defining rectangle for this bitmap.
    /// This means the rectangle which contains all colored (non-transparent) pixels
    /// </summary>
    /// <param name="BmInput">ClsDirectBitmap image</param>
    /// <returns>Rectangle - dimensions of relevant part</returns>
        public static Rectangle GetDefiningRectangle(ClsDirectBitmap BmInput)
        {
            ;

        // This new method using LockBits is much faster than a previous version where GetPixel() was used. 
        // This is a big performance boost when loading 512x512 (canvas size) images.
        // For now, the old function still exists to make sure regressions can be detected.

        101:
            ;

            // Find most top/left
            // Find most bottom/right

            var ObjCoordTopLeft = new Point(BmInput.Width, BmInput.Height);
            var ObjCoordBottomRight = new Point(0, 0);
            var ObjCurrentTransparentColor = BmInput.GetPixel(0, 0);
            int IntX = 0;
            int IntY = 0;
        251:
            ;
            var loopTo = BmInput.Height - 1;
            for (IntY = 0; IntY <= loopTo; IntY++)
            {
                var loopTo1 = BmInput.Width - 1;
                for (IntX = 0; IntX <= loopTo1; IntX++)
                {
                    var ObjColor = BmInput.GetPixel(IntX, IntY);

                    // Non-transparent
                    if (ObjColor.A == 255 & ObjColor.B != ObjCurrentTransparentColor.B & ObjColor.G != ObjCurrentTransparentColor.G & ObjColor.R != ObjCurrentTransparentColor.R)
                    {

                        // Detected a non-transparent color
                        if (IntX < ObjCoordTopLeft.X)
                            ObjCoordTopLeft.X = IntX; // Topleft: move to left
                        if (IntY < ObjCoordTopLeft.Y)
                            ObjCoordTopLeft.Y = IntY; // Topleft: move to top
                        if (IntY > ObjCoordBottomRight.Y)
                            ObjCoordBottomRight.Y = IntY; // Bottomright: move to bottom 
                        if (IntX > ObjCoordBottomRight.X)
                            ObjCoordBottomRight.X = IntX; // Bottomright: move to right
                    }
                }
            }

        901:
            ;

            // The width/height are +1.
            ObjCoordBottomRight.X += 1;
            ObjCoordBottomRight.Y += 1;
        999:
            ;

            // 20170512 
            // HENDRIX found out that completely transparent frames can cause issues.
            // This is a simple fix: it seems that a 1x1 frame is valid in ZT1, even if it's transparent.
            if (ObjCoordTopLeft.X == BmInput.Width & ObjCoordTopLeft.Y == BmInput.Height)
            {
                ObjCoordTopLeft = new Point(0, 0);
                ObjCoordBottomRight = new Point(1, 1);
            }

            return new Rectangle(ObjCoordTopLeft.X, ObjCoordTopLeft.Y, ObjCoordBottomRight.X - ObjCoordTopLeft.X, ObjCoordBottomRight.Y - ObjCoordTopLeft.Y);
            return default;
        dBug:
            ;
            MdlZTStudio.UnhandledError("MdlBitMap", "GetDefiningRectangle", Information.Err(), true);
        }
    }
}