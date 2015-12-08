﻿using System;
using elp87.TagReader;
using NAudio.Wave;

namespace elp87.VeloAudio
{
    public class Mp3File : IAudioFile
    {
        private readonly Mp3Tag _tag;
        private readonly IWavePlayer _waveOutDevice;
        private readonly AudioFileReader _audioFileReader;

        public Mp3File(string filename)
        {
            if (filename == "") return;

            _waveOutDevice = new WaveOut();
            _audioFileReader = new AudioFileReader(filename);
            _tag = new Mp3Tag(filename);
        }

        #region Methods
        #region Public
        public void Play()
        {
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
        }
        #endregion
    }
}
