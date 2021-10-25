using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace LykatekXamarinApp.Util
{
    class ImageConverter
    {
        public void byteArrayToImage(byte[] byteArrayImage)
        {
            var stream = new MemoryStream(byteArrayImage);
        }
    }
}
