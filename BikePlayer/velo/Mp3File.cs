using System;
using elp87.TagReader;
using NAudio.Wave;

namespace elp87.VeloAudio
{
    public class Mp3File : IAudioFile
    {
        private readonly Mp3Tag _tag;
        private IWavePlayer _waveOutDevice;
        private AudioFileReader _audioFileReader;

        public Mp3File(string filename)
        {
            if (filename == "") return;
            Filename = filename;

            
            _tag = new Mp3Tag(Filename);
            
        }

        #region Methods
        #region Public
        public void Play()
        {
            _waveOutDevice = new WaveOut();
            _audioFileReader = new AudioFileReader(Filename);
            _waveOutDevice.PlaybackStopped += _waveOutDevice_PlaybackStopped;
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

        public override string ToString()
        {
            return _tag.Performer + " - " + _tag.Title;
        }

        #endregion

        #region EventHandlers
        private void _waveOutDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            StopEventArgs.StopCases stopCase;
            if (_audioFileReader.Position == _audioFileReader.Length) stopCase = StopEventArgs.StopCases.Finished;
            else stopCase = StopEventArgs.StopCases.UserStoped;

            if (Stopped != null) Stopped(this, new StopEventArgs { StopCase = stopCase });
        }
        #endregion
        #endregion

        #region Properties
        public string Filename { get; private set; }
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

        public TimeSpan Length
        {
            get { return _audioFileReader.TotalTime; }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                if (_waveOutDevice.PlaybackState != PlaybackState.Playing ) return new TimeSpan(0);
                return _audioFileReader.CurrentTime;
            }
            set { _audioFileReader.CurrentTime = value; }
        }
        #endregion

        public event EventHandler<StopEventArgs> Stopped;
    }
}
