using elp87.TagReader;
using NAudio.Wave;

namespace elp87.VeloAudio
{
    public class Mp3File : IAudioFile
    {
        private readonly Mp3Tag _tag;
        private readonly string _filename;
        private IWavePlayer _waveOutDevice;
        private AudioFileReader _audioFileReader;

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
            _waveOutDevice = new WaveOut();
            _audioFileReader = new AudioFileReader(_filename);
            _waveOutDevice.Init(_audioFileReader);
            _waveOutDevice.Play();
        }

        public void Pause()
        {
            if (_waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                _waveOutDevice.Pause();
            }
            else
            {
                _waveOutDevice.Play();
            }
        }

        public void Stop()
        {
            _waveOutDevice.Stop();
        }
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
