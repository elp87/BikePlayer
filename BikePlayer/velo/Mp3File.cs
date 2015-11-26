using System;
using System.Runtime.InteropServices;
using System.Text;
using elp87.TagReader;

namespace elp87.VeloAudio
{
    public class Mp3File : IAudioFile
    {
        private Mp3Tag _tag;
        private readonly string _filename;
        //private readonly string _artist, _album, _title;
        //private readonly int _year;

        public Mp3File(string filename)
        {
            if (filename == "") return;
            _filename = filename;
            _tag = new Mp3Tag(_filename);
        }

        #region Methods
        #region Public
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
        #endregion

        #region Private
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        #endregion
        #endregion

        #region Properties
        public string Artist
        {
            get { return _tag.Performer; }
        }

        public string Album
        {
            get { return _tag.Album; }
        }

        public string Year
        {
            get { return _tag.Year; }
        }

        public string Title
        {
            get { return _tag.Title; }
        } 
        #endregion
    }
}
