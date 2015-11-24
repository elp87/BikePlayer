using System;
using System.Runtime.InteropServices;
using System.Text;

namespace elp87.VeloAudio
{
    public class Mp3File
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        private readonly string _filename;

        public Mp3File(string filename)
        {
            if (filename == "") return;
            _filename = filename;
        }

        public void Play()
        {
            Stop();
            mciSendString("open \"" + _filename + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile", null, 0, IntPtr.Zero);
        }

        public void Stop()
        {
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }

        public void Pause()
        {
            mciSendString("pause MediaFile", null, 0, IntPtr.Zero);
        }
    }
}
