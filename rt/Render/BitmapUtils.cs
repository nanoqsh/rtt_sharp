using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RT.Render
{
    static class BitmapUtils
    {
        public static void CopyArea(this Bitmap dest, Bitmap src, int x, int y, int size)
        {
            BitmapData srcData = src.LockBits(new Rectangle(0, 0, size, size), ImageLockMode.ReadOnly, src.PixelFormat);
            BitmapData destData = dest.LockBits(new Rectangle(x, y, size, size), ImageLockMode.WriteOnly, dest.PixelFormat);

            long srcScan0 = srcData.Scan0.ToInt64();
            long resScan0 = destData.Scan0.ToInt64();
            int srcStride = srcData.Stride;
            int resStride = destData.Stride;
            int rowLength = Math.Abs(srcData.Stride);

            try
            {
                byte[] buffer = new byte[rowLength];
                for (int i = 0; i < srcData.Height; i++)
                {
                    Marshal.Copy(new IntPtr(srcScan0 + i * srcStride), buffer, 0, rowLength);
                    Marshal.Copy(buffer, 0, new IntPtr(resScan0 + i * resStride), rowLength);
                }
            }
            finally
            {
                src.UnlockBits(srcData);
                dest.UnlockBits(destData);
            }
        }
    }
}
