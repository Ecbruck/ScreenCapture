using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenCapture
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Create an image object
            Image ScreenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            // Create graphics for the image
            Graphics graphImage = Graphics.FromImage(ScreenImage);
            // Create DC handle for the image
            IntPtr dcImage = graphImage.GetHdc();

            // Create DC handle for screen
            IntPtr dcScreen = CreateDC("DISPLAY", null, null, (IntPtr)null);

            // Copy current screen to bitmap
            BitBlt(dcImage, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, dcScreen, 0, 0, 13369376);

            // Dispose image DC
            graphImage.ReleaseHdc(dcImage);//释放位图句柄 

            // Save image file
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PNG Image File(*.png)|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ScreenImage.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);

                // Peek image file
                System.Diagnostics.Process.Start(dlg.FileName);
            }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool BitBlt(
        IntPtr hdcDest,
        int nXDest,
        int nYDest,
        int nWidth,
        int nHeight,
        IntPtr hdcSrc,
        int nXSrc,
        int nYSrc,
        int dwRop
        );

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateDC(
        string lpszDriver,
        string lpszDevice,
        string lpszOutput,
        IntPtr lpInitData
        );
    }
}
