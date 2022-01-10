using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ZTStudio
{

    /// <summary>
/// Custom class to perform faster bitmap operations
/// </summary>
    public class ClsDirectBitmap : IDisposable
    {
        private GCHandle DirectBitMap_BitsHandle;
        private int[] DirectBitMap_Bits;
        private Bitmap DirectBitmap_BitMap;
        private int DirectBitmap_Height;
        private int DirectBitMap_Width;
        private bool DirectBitMap_Disposed;

        /// <summary>
    /// Bitmap object
    /// </summary>
    /// <returns>Bitmap</returns>
        public Bitmap Bitmap
        {
            get
            {
                return DirectBitmap_BitMap;
            }

            set
            {
                DirectBitmap_BitMap = value;
            }
        }

        /// <summary>
    /// Bits
    /// </summary>
    /// <returns>Integer()</returns>
        public int[] Bits
        {
            get
            {
                return DirectBitMap_Bits;
            }

            set
            {
                DirectBitMap_Bits = value;
            }
        }

        /// <summary>
    /// Disposed
    /// </summary>
    /// <returns>Boolean</returns>
        public bool Disposed
        {
            get
            {
                return DirectBitMap_Disposed;
            }

            set
            {
                DirectBitMap_Disposed = value;
            }
        }

        /// <summary>
    /// Height
    /// </summary>
    /// <returns>Integer</returns>
        public int Height
        {
            get
            {
                return DirectBitmap_Height;
            }

            set
            {
                DirectBitmap_Height = value;
            }
        }

        /// <summary>
    /// Width
    /// </summary>
    /// <returns>Integer</returns>
        public int Width
        {
            get
            {
                return DirectBitMap_Width;
            }

            set
            {
                DirectBitMap_Width = value;
            }
        }

        /// <summary>
    /// Bitshandle
    /// </summary>
    /// <returns>GCHandle</returns>
        protected GCHandle BitsHandle
        {
            get
            {
                return DirectBitMap_BitsHandle;
            }

            set
            {
                DirectBitMap_BitsHandle = value;
            }
        }

        /// <summary>
    /// Initializes new instance
    /// </summary>
    /// <param name="IntWidth">Width</param>
    /// <param name="IntHeight">Height</param>
        public ClsDirectBitmap(int IntWidth, int IntHeight) : base()
        {
            Width = IntWidth;
            Height = IntHeight;
            Bits = new int[(IntWidth * IntHeight)];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(IntWidth, IntHeight, IntWidth * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        /// <summary>
    /// Initializes new instance
    /// </summary>
    /// <param name="ObjBitMap">Bitmap</param>
        public ClsDirectBitmap(Bitmap ObjBitMap) : base()
        {
            Width = ObjBitMap.Width;
            Height = ObjBitMap.Height;
            Bits = new int[(ObjBitMap.Width * ObjBitMap.Height)];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(ObjBitMap.Width, ObjBitMap.Height, ObjBitMap.Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());

            // Stupid workaround for now; can this be done more efficiently?
            // Only for loading PNGs. Better method to use this here is greatly appreciated! (contribute a pull request)
            int IntX;
            int IntY;
            var loopTo = ObjBitMap.Height - 1;
            for (IntY = 0; IntY <= loopTo; IntY++)
            {
                IntX = 0; // reset every loop
                var loopTo1 = ObjBitMap.Width - 1;
                for (IntX = 0; IntX <= loopTo1; IntX++)
                    SetPixel(IntX, IntY, ObjBitMap.GetPixel(IntX, IntY));
            }
        }

        /// <summary>
    /// Sets pixel on ClsDirectBitmap
    /// </summary>
    /// <param name="IntX">X Integer</param>
    /// <param name="IntY">Y Integer</param>
    /// <param name="ObjColor">Color</param>
        public void SetPixel(int IntX, int IntY, Color ObjColor)
        {
            int IntIndex = IntX + IntY * Width;
            int IntCol = ObjColor.ToArgb();
            Bits[IntIndex] = IntCol;
        }

        /// <summary>
    /// Sets pixel on ClsDirectBitmap
    /// </summary>
    /// <param name="IntX">X Integer</param>
    /// <param name="IntY">Y Integer</param>
    /// <returns>Color</returns>
        public Color GetPixel(int IntX, int IntY)
        {
            int IntIndex = IntX + IntY * Width;
            int IntCol = Bits[IntIndex];
            var ObjColor = Color.FromArgb(IntCol);
            return ObjColor;
        }

        /// <summary>
    /// Disposes object
    /// </summary>
        public void Dispose()
        {
            if (Disposed == true)
            {
                return;
            }

            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}